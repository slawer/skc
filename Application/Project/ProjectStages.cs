using System;
using System.Xml;
using System.Threading;

namespace SKC
{
    /// <summary>
    /// реализует хранение информации об эиапах работы для оператора
    /// </summary>
    /*public class ProjectStage
    {
        protected string stage_name;            // текстовое описание этапа
        protected float stage_volume;           // объем на этапе

        protected float koef;                   // поправочный коэффициент
        protected ReaderWriterLockSlim slim;    // синхронизатор

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public ProjectStage()
        {
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            stage_name = string.Empty;
            stage_volume = 0.0f;

            koef = float.NaN;
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

                    XmlNode stage_nameNode = doc.CreateElement("stage_name");            // текстовое описание этапа
                    XmlNode stage_volumeNode = doc.CreateElement("stage_volume");           // объем на этапе

                    XmlNode koefNode = doc.CreateElement("koef");                   // поправочный коэффициент                

                    stage_nameNode.InnerText = stage_name;
                    stage_volumeNode.InnerText = stage_volume.ToString();

                    koefNode.InnerText = koef.ToString();

                    root.AppendChild(stage_nameNode);
                    root.AppendChild(stage_volumeNode);

                    root.AppendChild(koefNode);

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
                                        //stage_volume = float.NaN;
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

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch { }
        }


    }*/
}