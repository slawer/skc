using System;
using System.Xml;
using System.Threading;
using System.Collections.Generic;

namespace SKC
{
    /// <summary>
    /// Список расходомеров
    /// </summary>
    public class RgrList : List<Rgr>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public RgrList()
            : base()
        {
        }

        /// <summary>
        /// Сохранить список расходомеров
        /// </summary>
        /// <param name="doc">документ в который осуществляется сохранение</param>
        /// <returns></returns>
        public XmlNode Save(XmlDocument doc)
        {
            try
            {
                XmlNode root = doc.CreateElement("RgrList");
                foreach (Rgr rgr in this)
                {
                    if (rgr != null)
                    {
                        XmlNode node = rgr.Save(doc);
                        if (node != null)
                        {
                            root.AppendChild(node);
                        }
                    }
                }

                return root;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Загрузить список расходомеров
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static RgrList Load(XmlNode root)
        {
            try
            {
                if (root != null && root.Name == "RgrList")
                {
                    if (root.HasChildNodes)
                    {
                        RgrList lst = new RgrList();
                        foreach (XmlNode child in root.ChildNodes)
                        {
                            if (child.Name == "rgr")
                            {
                                Rgr rgr = Rgr.Load(child);
                                if (rgr != null)
                                {
                                    lst.Add(rgr);
                                }
                            }
                        }

                        return lst;
                    }
                }
            }
            catch { }
            return null;
        }
    }

    /// <summary>
    /// Реализует расходомер
    /// </summary>
    public class Rgr
    {
        protected TechParameter _volume;            // объем источник
        protected TechParameter _consumption;       // расход источник

        protected float consumption;                // расход
        protected float volume;                     // объем

        protected Mutex _mutex;                     // синхронизатор
        protected ReaderWriterLockSlim _slim;       // синхронизатор свойств

        // ----- при изменении поправочного коэффициента пересчет ----

        protected float w, w0, s;
        protected bool is_main;                     // расходомер является ведущим или нет

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Rgr()
        {
            _volume = new TechParameter();
            _consumption = new TechParameter();

            _mutex = new Mutex();
            _slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Объем расходомера
        /// </summary>
        public TechParameter Volume
        {
            get { return _volume; }
        }

        /// <summary>
        /// Расход
        /// </summary>
        public TechParameter Consumption
        {
            get { return _consumption; }
        }

        /// <summary>
        /// Определяет расходомер является ведущим или нет
        /// </summary>
        public Boolean IsMain
        {
            get
            {
                if (_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return is_main;
                    }
                    finally
                    {
                        _slim.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        is_main = value;
                    }
                    finally
                    {
                        _slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Текущий объем
        /// </summary>
        public float CurrentVolume
        {
            get
            {
                if (_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return volume;
                    }
                    finally
                    {
                        _slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            protected set
            {
                if (_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        volume = value;
                    }
                    finally
                    {
                        _slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Текущий расход
        /// </summary>
        public float CurrentConsumption
        {
            get
            {
                if (_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return consumption;
                    }
                    finally
                    {
                        _slim.ExitReadLock();
                    }
                }

                return float.NaN;

            }

            protected set
            {
                if (_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        consumption = value;
                    }
                    finally
                    {
                        _slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Константа объема
        /// </summary>
        public float ConstVolume
        {
            get
            {
                if (_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return constVolume;
                    }
                    finally
                    {
                        _slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        constVolume = value;
                    }
                    finally
                    {
                        _slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет поправочный коэффициент для расходомера
        /// </summary>
        public float CorrectionFactor
        {
            get
            {
                if (_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return constKoef;
                    }
                    finally
                    {
                        _slim.ExitReadLock();
                    }
                }

                return 1.0f;
            }

            protected set
            {
                if (_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        constKoef = value;
                    }
                    finally
                    {
                        _slim.ExitWriteLock();
                    }
                }
            }
        }

        private float lastVolume = float.NaN;
        private float constVolume;                  // констОб
        private float constKoef = 1;                // поправочный коэффициент

        /// <summary>
        /// Вычислить объем расходомера
        /// </summary>
        public bool Calculate(StateRGR rgrState)
        {
            bool blocked = false;
            bool retValue = false;

            try
            {
                if (_mutex.WaitOne(100))
                {
                    blocked = true;
                    switch (rgrState)
                    {
                        case StateRGR.Pressed:

                            if (float.IsNaN(lastVolume) || float.IsInfinity(lastVolume) ||
                                float.IsPositiveInfinity(lastVolume) || float.IsNegativeInfinity(lastVolume))
                            {
                                lastVolume = _volume.Value;
                            }

                            float currentVolume = _volume.Value;
                            if (lastVolume > currentVolume)
                            {
                                ConstVolume = ConstVolume - (lastVolume - currentVolume);
                                lastVolume = currentVolume;

                                retValue = true;
                            }

                            CurrentConsumption = _consumption.Value * CorrectionFactor;

                            float deltaVolume = (currentVolume - constVolume);
                            if (deltaVolume >= 0)
                            {
                                CurrentVolume = deltaVolume * CorrectionFactor + s;
                                lastVolume = currentVolume;
                            }
                            break;

                        case StateRGR.Unpressed:

                            CurrentVolume = 0.0f;
                            CurrentConsumption = 0.0f;

                            break;

                        default:
                            break;
                    }
                }
            }
            finally
            {
                if (blocked) _mutex.ReleaseMutex();
            }

            return retValue;
        }

        /// <summary>
        /// Сбросить объем расходомера
        /// </summary>
        public void Reset()
        {            
            bool blocked = false;
            try
            {
                if (_mutex.WaitOne(100))
                {
                    blocked = true;
                    ConstVolume = _volume.Value;
                    CorrectionFactor = 1.0f;
                    s = 0.0f;
                }
            }
            finally
            {
                if (blocked) _mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Обновить поправочный коэффициент для расходомера
        /// </summary>
        /// <param name="nKoef"></param>
        public void UpdateKoef(float nKoef)
        {
            bool blocked = false;
            try
            {
                if (_mutex.WaitOne(100))
                {
                    blocked = true;

                    w = _volume.Value;
                    w0 = ConstVolume;

                    s = (w - w0) * constKoef + s;

                    ConstVolume = w;
                    CorrectionFactor = nKoef;
                }
            }
            finally
            {
                if (blocked) _mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Завершить этап
        /// </summary>
        public void FinishStage()
        {
            bool blocked = false;
            try
            {
                if (_mutex.WaitOne(100))
                {
                    blocked = true;
                    CurrentConsumption = 0.0f;
                }
            }
            finally
            {
                if (blocked) _mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Сохранить расходомер
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public XmlNode Save(XmlDocument doc)
        {
            try
            {
                XmlNode root = doc.CreateElement("rgr");

                XmlNode _volumeNode = _volume.SerializeToXml(doc);
                XmlNode _consumptionNode = _consumption.SerializeToXml(doc);

                XmlNode consumptionNode = doc.CreateElement("consumptionNode");
                XmlNode volumeNode = doc.CreateElement("volumeNode");

                XmlNode is_mainNode = doc.CreateElement("is_mainNode");
                XmlNode constKoefNode = doc.CreateElement("correctionFactor");

                consumptionNode.InnerText = consumption.ToString();
                volumeNode.InnerText = volume.ToString();

                is_mainNode.InnerText = is_main.ToString();
                constKoefNode.InnerText = constKoef.ToString();

                root.AppendChild(_volumeNode);
                root.AppendChild(_consumptionNode);
                root.AppendChild(consumptionNode);
                root.AppendChild(volumeNode);
                root.AppendChild(is_mainNode);
                root.AppendChild(constKoefNode);

                return root;

            }
            catch { }
            return null;
        }

        /// <summary>
        /// Загрузить расходомер
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static Rgr Load(XmlNode root)
        {
            try
            {
                if (root != null && root.Name == "rgr")
                {
                    if (root.HasChildNodes)
                    {
                        Rgr rgr = new Rgr();
                        Boolean isVolume = true;

                        foreach (XmlNode child in root.ChildNodes)
                        {
                            switch (child.Name)
                            {
                                case TechParameter.ParameterRootName:

                                    if (isVolume)
                                    {
                                        isVolume = false;
                                        rgr.Volume.DeserializeFromXml(child);
                                    }
                                    else
                                    {
                                        rgr.Consumption.DeserializeFromXml(child);
                                    }
                                    break;

                                case "consumptionNode":

                                    try
                                    {
                                        rgr.CurrentConsumption = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "volumeNode":

                                    try
                                    {
                                        rgr.CurrentVolume = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "is_mainNode":

                                    try
                                    {
                                        rgr.IsMain = bool.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "correctionFactor":

                                    try
                                    {

                                    }
                                    catch { }
                                    break;
                                
                                default:
                                    break;
                            }
                        }

                        return rgr;
                    }
                }
            }
            catch { }
            return null;
        }
    }
}