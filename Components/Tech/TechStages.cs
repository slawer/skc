using System;
using System.Xml;
using System.Threading;
using System.Collections.Generic;

namespace SKC
{
    /// <summary>
    /// Реализует управение этапами
    /// </summary>
    public class TechStages
    {
        // ---- константы ----

        /// <summary>
        /// корневой узел
        /// </summary>
        private const string rootName = "work";

        /// <summary>
        /// узел в котором время запуска РГР
        /// </summary>
        private const string rootRGROn = "data_rgr_on";

        /// <summary>
        /// имя узла в котором текущий поправочный коэффициент
        /// </summary>
        private const string constCoefName = "const_koef_vol";

        // -----------------------------------------------------------

        private List<TechStage> stages;             // этапы работы
        
        private ReaderWriterLockSlim slim;          // синхронизатор списка этапов и др переменных
        private ReaderWriterLockSlim slimVolume;    // синхронизатор суммарного объема

        // -------- данные текущего этапа --------
 
        private float totalVolume;                  // суммарный объем
        private float proccessVolume;               // объем процесса

        private float constKoef = 1;                // поправочный коэффициент
        private bool isfinished = false;            // завершена работа или нет

        private DateTime rgrOn;                     // время запуска РГР
        private Tech _tech = null;                  // технология

        // ----- при изменении поправочного коэффициента пересчет ----

        public float w, w0, s;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public TechStages(Tech tech)
        {
            if (tech != null)
            {
                _tech = tech;
                stages = new List<TechStage>();

                slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
                slimVolume = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

                totalVolume = 0.0f;
                proccessVolume = 0.0f;

                rgrOn = DateTime.MinValue;
            }
            else
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs("Не передали экземпляр технологии в TechStages", ErrorType.Fatal));
            }
        }

        /// <summary>
        /// Определяет поправочный коэффициент
        /// </summary>
        public float CorrectionFactor
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return constKoef;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (!float.IsNaN(value) && !float.IsInfinity(value) &&
                                !float.IsNegativeInfinity(value) && !float.IsPositiveInfinity(value))
                        {
                            constKoef = value;
                        }
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет суммарный объем
        /// </summary>
        public float TotalVolume
        {
            get
            {
                if (slimVolume.TryEnterReadLock(300))
                {
                    try
                    {
                        return totalVolume;
                    }
                    finally
                    {
                        slimVolume.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slimVolume.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (!float.IsNaN(value) && !float.IsInfinity(value) &&
                                !float.IsNegativeInfinity(value) && !float.IsPositiveInfinity(value))
                        {
                            totalVolume = value;
                        }
                    }
                    finally
                    {
                        slimVolume.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет объем процесса
        /// </summary>
        public float ProccessVolume
        {
            get
            {
                if (slimVolume.TryEnterReadLock(300))
                {
                    try
                    {
                        return proccessVolume;
                    }
                    finally
                    {
                        slimVolume.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slimVolume.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (!float.IsNaN(value) && !float.IsInfinity(value) &&
                                !float.IsNegativeInfinity(value) && !float.IsPositiveInfinity(value))
                        {
                            proccessVolume = value;
                        }
                    }
                    finally
                    {
                        slimVolume.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Показывает работают этапы или нет
        /// </summary>
        public bool IsWork
        {
            get
            {
                if (Current != null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Возвращяет текущий этап работы
        /// </summary>
        public TechStage Current
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        foreach (TechStage stage in stages)
                        {
                            if (stage.State == StageState.Started)
                            {
                                return stage;
                            }
                        }
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return null;
            }
        }

        // --------------- методы управления этапами ---------------

        /// <summary>
        /// новый этап работы
        /// </summary>
        public void NextStage()
        {
            if (isfinished)
            {
                stages.Clear();
                isfinished = false;
            }

            int number = 0;
            TechStage current = Current;

            if (current != null)
            {
                Current.FinishStage();
                number = current.Number;
            }

            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    TechStage stage = new TechStage(_tech);
                    stage.Number = number + 1;

                    if (current != null && current.StateRGR == StateRGR.Pressed)
                    {
                        stage.RecordRGR();
                    }

                    stages.Add(stage);
                    stage.StartStage();

                    s = 0;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Завершить работу этапов
        /// </summary>
        public void Finish()
        {
            isfinished = true;
        }

        /// <summary>
        /// Запустили РГР
        /// </summary>
        public void RgrOn()
        {
            TechStage st = Current;
            if (st != null)
            {
                st.RecordRGR();
                rgrOn = DateTime.Now;

                w0 = st.Volume;
            }
        }

        /// <summary>
        /// Сбросить запись РГР
        /// </summary>
        public void resetRGROn()
        {
            rgrOn = DateTime.MinValue;
        }

        /// <summary>
        /// Обновить поправочный коэффициент
        /// </summary>
        public void UpdateKoef(float nKoef)
        {
            try
            {
                //DevManClient.SendToCom(string.Empty);
                TechStage st = Current;

                if (st != null)
                {
                    if (st.StateRGR == StateRGR.Pressed)
                    {
                        if (st.Block(300))
                        {
                            w = _tech.Volume.Value;
                            w0 = st.ConstVolume;

                            s = (w - w0) * constKoef + s;

                            st.ConstVolume = w;
                            st.Release();
                        }
                    }
                }
                constKoef = nKoef;
            }
            catch { }
        }

        /// <summary>
        /// Инициализировать этапы
        /// </summary>
        public void InitializeStages()
        {
            try
            {
                foreach (TechStage stage in stages)
                {
                    if (stage.StartTime != DateTime.MinValue && stage.FinishedTime != DateTime.MaxValue)
                    {
                        isfinished = true;
                        if (stage.StartTime < stage.FinishedTime)
                        {
                            stage.State = StageState.Finished;
                        }
                    }
                    else
                    {
                        if (stage.StartTime != DateTime.MinValue && stage.FinishedTime == DateTime.MaxValue)
                        {
                            stage.State = StageState.Started;
                        }
                        isfinished = false;
                    }
                }

                if (Current != null)
                {
                    if (rgrOn != DateTime.MinValue)
                    {
                        Current.RecoredRGRFromLoad();
                    }
                }
                else
                {
                    constKoef = 1.0f;
                }
            }
            catch { }
        }


        /// <summary>
        /// Сохранить этапы
        /// </summary>
        /// <param name="doc">Докумен в который выполнять сохранение этапов</param>
        /// <returns>Узел в котором сохранены этапы работы</returns>
        public XmlNode Serialize(XmlDocument doc, XmlNode root)
        {
            try
            {
                //XmlNode root = doc.CreateElement(rootName);

                XmlNode rgrOnNode = doc.CreateElement(rootRGROn);
                XmlNode constCoefNode = doc.CreateElement(constCoefName);

                XmlNode sNode = doc.CreateElement("shift");

                constCoefNode.InnerText = constKoef.ToString();
                if (rgrOn != DateTime.MinValue)
                {
                    rgrOnNode.InnerText = rgrOn.ToString();
                }

                sNode.InnerText = s.ToString();

                root.AppendChild(rgrOnNode);
                root.AppendChild(constCoefNode);

                root.AppendChild(sNode);

                foreach (TechStage stage in stages)
                {
                    XmlNode stageNode = stage.Serialize(doc);
                    if (stageNode != null)
                    {
                        root.AppendChild(stageNode);
                    }
                }

                Application _app = Application.CreateInstance();
                if (_app != null)
                {
                    _app.Commutator.Technology.Save(doc, root);
                }

                return root;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Загрузить этапы
        /// </summary>
        /// <param name="root">Узел в котором искать параметры СКПБ</param>
        public void Deserialize(XmlNode root)
        {
            try
            {
                if (root != null)
                {
                    if (root != null && root.Name == rootName)
                    {
                        XmlNodeList childs = root.ChildNodes;
                        if (childs != null)
                        {
                            foreach (XmlNode child in childs)
                            {
                                switch (child.Name)
                                {
                                    case rootRGROn:

                                        try
                                        {
                                            rgrOn = DateTime.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case constCoefName:

                                        try
                                        {
                                            constKoef = float.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "shift":

                                        try
                                        {
                                            s = float.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "stage":

                                        try
                                        {
                                            TechStage stage = new TechStage(_tech);
                                            stage.Deserialize(child);

                                            stages.Add(stage);
                                        }
                                        catch { }
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}