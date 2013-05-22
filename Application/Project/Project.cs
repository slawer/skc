using System;
using System.Xml;
using System.Collections.Generic;

namespace SKC
{
    /// <summary>
    /// Реализует проек на выполнение работы
    /// </summary>
    /*public class Project
    {
        protected string place;                     // месторождение
        protected string bush;                      // куст

        protected string well;                      // скважина
        protected string job;                       // задание

        protected DateTime created;                 // время создания проекта
        protected DateTime worked;                  // время проведения работы

        protected string db_name;                   // база данных в которую сохраняются даные данного проекта
        protected string dir;                       // папака в которой храняться файлы для БД

        protected bool actived = false;             // активен проект или нет        

        protected List<ProjectStage> stages;       // этапы работы для проекта

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Project()
        {
            place = string.Empty;
            bush = string.Empty;

            well = string.Empty;
            job = string.Empty;

            db_name = string.Empty;

            created = DateTime.MinValue;
            worked = DateTime.MinValue;

            stages = new List<ProjectStage>();
        }

        /// <summary>
        /// Определяет месторождение
        /// </summary>
        public string Place
        {
            get
            {
                return place;
            }

            set
            {
                place = value;
            }
        }

        /// <summary>
        /// Определяет куст
        /// </summary>
        public string Bush
        {
            get
            {
                return bush;
            }

            set
            {
                bush = value;
            }
        }

        /// <summary>
        /// Определяет скважину
        /// </summary>
        public string Well
        {
            get
            {
                return well;
            }

            set
            {
                well = value;
            }
        }

        /// <summary>
        /// Определяет задание на работу
        /// </summary>
        public string Job
        {
            get
            {
                return job;
            }

            set
            {
                job = value;
            }
        }

        /// <summary>
        /// Определяет имя БД в которую сохраняются данные
        /// </summary>
        public string DB_Name
        {
            get
            {
                return db_name;
            }

            set
            {
                db_name = value;   
            }
        }

        /// <summary>
        /// Определяет время создания проекта
        /// </summary>
        public DateTime Created
        {
            get
            {
                return created;
            }

            set
            {
                created = value;
            }
        }

        /// <summary>
        /// Определяет время проведения работы
        /// </summary>
        public DateTime Worked
        {
            get
            {
                return worked;
            }

            set
            {
                worked = value;
            }
        }

        /// <summary>
        /// Определяет проект активен или нет
        /// </summary>
        public bool Actived
        {
            get
            {
                return actived;
            }

            set
            {
                actived = value;
            }
        }

        /// <summary>
        /// Название папки в которой сохраняются данные проекта
        /// </summary>
        public string Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        /// <summary>
        /// Возвращяет этапы работы для проекта
        /// </summary>
        public List<ProjectStage> Stages
        {
            get
            {
                return stages;
            }
        }

        /// <summary>
        /// имя узла содержащего данные проекта
        /// </summary>
        public const string ProjectRootName = "Project";

        /// <summary>
        /// Сохранить прект в узел Xml
        /// </summary>
        /// <param name="doc">докумен для которого осуществляется сохранение проекта</param>
        /// <returns></returns>
        public XmlNode SerializeToXmlNode(XmlDocument doc)
        {
            try
            {
                if (doc != null)
                {
                    XmlNode root = doc.CreateElement(ProjectRootName);

                    XmlNode placeNode = doc.CreateElement("place");                     // месторождение
                    XmlNode bushNode = doc.CreateElement("bush");                      // куст

                    XmlNode wellNode = doc.CreateElement("well");                      // скважина
                    XmlNode jobNode = doc.CreateElement("job");                       // задание

                    XmlNode createdNode = doc.CreateElement("created");                 // время создания проекта
                    XmlNode workedNode = doc.CreateElement("worked");                  // время проведения работы

                    XmlNode db_nameNode = doc.CreateElement("db_name");                   // база данных в которую сохраняются даные данного проекта
                    XmlNode activedNode = doc.CreateElement("actived");             // активен проект или нет        

                    XmlNode dirNode = doc.CreateElement("dir");

                    placeNode.InnerText = place;
                    bushNode.InnerText = bush;

                    wellNode.InnerText = well;
                    jobNode.InnerText = job;

                    createdNode.InnerText = created.ToString();
                    workedNode.InnerText = worked.ToString();

                    db_nameNode.InnerText = db_name;
                    activedNode.InnerText = actived.ToString();

                    dirNode.InnerText = dir;

                    root.AppendChild(placeNode);
                    root.AppendChild(bushNode);

                    root.AppendChild(wellNode);
                    root.AppendChild(jobNode);

                    root.AppendChild(createdNode);
                    root.AppendChild(workedNode);

                    root.AppendChild(db_nameNode);
                    root.AppendChild(activedNode);

                    root.AppendChild(dirNode);

                    // ---- сохраняем этапы ----

                    XmlNode stagesNode = doc.CreateElement("stages");
                    foreach (ProjectStage stage in stages)
                    {
                        XmlNode s_node = stage.SerializeToXmlNode(doc);
                        if (s_node != null)
                        {
                            stagesNode.AppendChild(s_node);
                        }
                    }

                    root.AppendChild(stagesNode);
                    return root;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Сохранить прект в узел Xml
        /// </summary>
        /// <param name="doc">докумен для которого осуществляется сохранение проекта</param>
        /// <returns></returns>
        public void DeserializeFromXmlNode(XmlNode root)
        {
            try
            {
                if (root != null)
                {
                    if (root.Name == ProjectRootName)
                    {
                        if (root.HasChildNodes)
                        {
                            foreach (XmlNode child in root.ChildNodes)
                            {
                                switch (child.Name)
                                {
                                    case "place":

                                        try
                                        {
                                            place = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case "bush":

                                        try
                                        {
                                            bush = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case "well":

                                        try
                                        {
                                            well = child.InnerText;
                                        }
                                        catch { }                                        
                                        break;

                                    case "job":

                                        try
                                        {
                                            job = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case "created":

                                        try
                                        {
                                            created = DateTime.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "worked":

                                        try
                                        {
                                            worked = DateTime.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "db_name":

                                        try
                                        {
                                            db_name = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case "actived":

                                        try
                                        {
                                            actived = bool.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "dir":

                                        try
                                        {
                                            dir = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case "stages":
                                        
                                        try
                                        {
                                            if (child.HasChildNodes)
                                            {
                                                foreach (XmlNode s_child in child.ChildNodes)
                                                {
                                                    if (s_child.Name == ProjectStage.StageRootName)
                                                    {
                                                        ProjectStage stage = new ProjectStage();
                                                        stage.DeserializeFromXmlNode(s_child);

                                                        stages.Add(stage);
                                                    }
                                                }
                                            }
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

    }*/
}