using System;
using System.Xml;
using System.Threading;

namespace SKC
{
    /// <summary>
    /// реализует хранение информации об эиапах работы для оператора
    /// </summary>
    public class ProjectStage
    {
        protected string stage_name;            // текстовое описание этапа
        protected float stage_volume;           // объем на этапе

        protected float koef;                   // поправочный коэффициент
        protected ReaderWriterLockSlim slim;    // синхронизатор

        protected float plan_consumption;       // плановый расход
        protected float plan_volume;            // плановый объем

        protected float plan_pressure;          // плановое давление
        protected float plan_density;           // плановая плотность

        protected DateTime start_time;          // начало этапа
        protected DateTime finish_time;         // конец этапа

        // синхронизатор максимальных и минимальных значений параметров
        protected ReaderWriterLockSlim ext_slim;    

        protected float min_volume;             // минимальный объем
        protected float max_volume;             // максимальный объем

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public ProjectStage()
        {
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            ext_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            stage_name = string.Empty;
            stage_volume = 0.0f;

            koef = float.NaN;

            min_volume = float.MaxValue;
            max_volume = float.MinValue;

            start_time = DateTime.MinValue;
            finish_time = DateTime.MaxValue;
        }

        /// <summary>
        /// определяет текстовое описание этапа
        /// </summary>
        public string StageName
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return stage_name;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        stage_name = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет объем этапа
        /// </summary>
        public float StageVolume
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return stage_volume;
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
                        stage_volume = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// определяет поправочный коэффициент
        /// </summary>
        public float Koef
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return koef;
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
                        koef = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет плановый расход
        /// </summary>
        public float Plan_consumption
        {
            get { return plan_consumption; }
            set { plan_consumption = value; }
        }

        /// <summary>
        /// Определяет плановый объем
        /// </summary>
        public float Plan_volume
        {
            get { return plan_volume; }
            set { plan_volume = value; }
        }

        /// <summary>
        /// Определяет плановое давление
        /// </summary>
        public float Plan_pressure
        {
            get { return plan_pressure; }
            set { plan_pressure = value; }
        }

        /// <summary>
        /// Определяет плановая плотность
        /// </summary>
        public float Plan_density
        {
            get { return plan_density; }
            set { plan_density = value; }
        }


        /// <summary>
        /// Определяет минимальный объем
        /// </summary>
        public float Min_volume
        {
            get
            {
                if (ext_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return min_volume;
                    }
                    finally
                    {
                        ext_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (ext_slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        min_volume = value;
                    }
                    finally
                    {
                        ext_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет максимальный объем
        /// </summary>
        public float Max_volume
        {
            get
            {
                if (ext_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return max_volume;
                    }
                    finally
                    {
                        ext_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (ext_slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        max_volume = value;
                    }
                    finally
                    {
                        ext_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// определяет время начала этапа
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                if (ext_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return start_time;
                    }
                    finally
                    {
                        ext_slim.ExitReadLock();
                    }
                }

                return DateTime.MinValue;
            }

            set
            {
                if (ext_slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        start_time = value;
                    }
                    finally
                    {
                        ext_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// определяет время завершения этапа
        /// </summary>
        public DateTime FinishTime
        {
            get
            {
                if (ext_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return finish_time;
                    }
                    finally
                    {
                        ext_slim.ExitReadLock();
                    }
                }

                return DateTime.MaxValue;
            }

            set
            {
                if (ext_slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        finish_time = value;
                    }
                    finally
                    {
                        ext_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// имя узла в котором находится этап работы
        /// </summary>
        public const string StageRootName = "stage";

        /// <summary>
        /// Сохранить этап в узел Xml
        /// </summary>
        /// <param name="doc">докумен для которого осуществляется сохранение проекта</param>
        /// <returns></returns>
        public XmlNode SerializeToXmlNode(XmlDocument doc)
        {
            try
            {
                if (doc != null)
                {
                    XmlNode root = doc.CreateElement(StageRootName);

                    XmlNode stage_nameNode = doc.CreateElement("stage_name");       // текстовое описание этапа
                    XmlNode stage_volumeNode = doc.CreateElement("stage_volume");   // объем на этапе

                    XmlNode koefNode = doc.CreateElement("koef");                   // поправочный коэффициент      
          
                    XmlNode plan_consumptionNode = doc.CreateElement("plan_consumption");           // плановый расход
                    XmlNode plan_volumeNode = doc.CreateElement("plan_volume");                // плановый объем

                    XmlNode plan_pressureNode = doc.CreateElement("plan_pressure");              // плановое давление
                    XmlNode plan_densityNode = doc.CreateElement("plan_density");               // плановая плотность

                    XmlNode start_timeNode = doc.CreateElement("start_time");                 // начало этапа
                    XmlNode finish_timeNode = doc.CreateElement("finish_time");                // конец этапа

                    stage_nameNode.InnerText = stage_name;
                    stage_volumeNode.InnerText = stage_volume.ToString();

                    koefNode.InnerText = koef.ToString();

                    plan_consumptionNode.InnerText = plan_consumption.ToString();
                    plan_volumeNode.InnerText = plan_volume.ToString();

                    plan_pressureNode.InnerText = plan_pressure.ToString();
                    plan_densityNode.InnerText = plan_density.ToString();

                    start_timeNode.InnerText = start_time.ToString();
                    finish_timeNode.InnerText = finish_time.ToString();

                    root.AppendChild(stage_nameNode);
                    root.AppendChild(stage_volumeNode);

                    root.AppendChild(koefNode);

                    root.AppendChild(plan_consumptionNode);
                    root.AppendChild(plan_volumeNode);

                    root.AppendChild(plan_pressureNode);
                    root.AppendChild(plan_densityNode);

                    root.AppendChild(start_timeNode);
                    root.AppendChild(finish_timeNode);

                    return root;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Сохранить этап в узел Xml
        /// </summary>
        /// <param name="doc">докумен для которого осуществляется сохранение проекта</param>
        /// <returns></returns>
        public void DeserializeFromXmlNode(XmlNode root)
        {
            try
            {
                if (root != null && root.Name == StageRootName)
                {
                    if (root.HasChildNodes)
                    {
                        foreach (XmlNode child in root.ChildNodes)
                        {
                            switch (child.Name)
                            {
                                case "stage_name":

                                    try
                                    {
                                        stage_name = child.InnerText;
                                    }
                                    catch { }
                                    break;

                                case "stage_volume":

                                    try
                                    {
                                        stage_volume = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "koef":

                                    try
                                    {
                                        koef = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "plan_consumption":

                                    try
                                    {
                                        plan_consumption = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "plan_volume":

                                    try
                                    {
                                        plan_volume = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "plan_pressure":

                                    try
                                    {
                                        plan_pressure = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "plan_density":

                                    try
                                    {
                                        plan_density = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "start_time":

                                    try
                                    {
                                        start_time = DateTime.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "finish_time":

                                    try
                                    {
                                        finish_time = DateTime.Parse(child.InnerText);
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
            catch { }
        }
    }
}