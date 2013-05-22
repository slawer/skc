using System;
using System.IO;
using System.Xml;
using Microsoft.Win32;
using System.Globalization;
using System.Collections.Generic;
using System.Net.NetworkInformation;

using DataBase;
using Buffering;

using WCF;
using WCF.WCF_Client;

using GraphicComponent;

namespace SKC
{
    /// <summary>
    /// Реализует приложение СКЦ 2
    /// </summary>
    public class Application
    {
        // ---- узлы в которые сохраняются настройки программы ----

        /// <summary>
        /// узел в котором храняться основные настройки программы
        /// </summary>
        protected const string rootName = "configuration";

        /// <summary>
        /// узел в котором храняться настройки коммутатора
        /// </summary>
        protected const string commutatorName = "commutator";

        /// <summary>
        /// узел в котором храняться настройки команд БО
        /// </summary>
        protected const string commandsName = "commands";

        /// <summary>
        /// узел в котором сохраняются настройки панелей
        /// </summary>
        protected const string panelsName = "panels";

        /// <summary>
        /// узел в котором сохраняется панель
        /// </summary>
        protected const string panelName = "panel";

        /// <summary>
        /// узел котором сохраняется адрес сервера БД
        /// </summary>
        protected const string dataSourceName = "datasource";

        /// <summary>
        /// имя узла в котором сохраняется имя пользователя дл БД
        /// </summary>
        protected const string userIDName = "userid";

        /// <summary>
        /// имя узла в котором сохраняется пароль пользователя БД
        /// </summary>
        protected const string passwordName = "password";

        /// <summary>
        /// имя файла в котором сохранены этапы работы
        /// </summary>
        protected string configStages = "{87333C0C-C0D2-45C5-A56C-68050851173A}.stg.xml";
        
        // --------------------------------------------------------

        protected static Application _app = null;      // приложение

        // ----- данные класса -----

        protected RSliceBuffer buffer = null;           // буфер, который хранит срезы данных пришедших от DevMan2;
        protected Parameter[] parameters = null;        // список параметров СКЦ 2

        protected List<Project> projects;               // проекты СКЦ
        protected List<BlockViewCommand> commands;      // команды блока отображения

        protected Tech tech = null;                     // технологическая примочка

        protected Commutator commutator;                // осуществляет прием и передачу данных в системе
        protected DataBaseManager manager;              // осуществляет работу с БД

        protected List<ParametersViewPanel> panels;     // панели отображения параметров

        protected Graphic graphic_consumption;          // отображаемый график расхода
        protected Graphic graphic_volume;               // отображаемый график объема

        protected Graphic graphic_density;              // отображаемый график плотности
        protected Graphic graphic_pressure;             // отображаемый график давления

        protected Graphic graphic_temperature;          // отображаемый график температуры

        public /*protected*/ devTcpManager client;                 // работает по старому TCP каналу обмена данными
        protected Uri devManUri = null;                 // адрес подключения к devMan
        protected Uri devManUri2 = null;                // адрес подключения к devMan на удалённой машине (для правильного сохранения настроек)

        protected bool autostartcon = false;            // автоматически включать запись расхода

        protected bool autolocalhost = false;           // автоматически подключаться к локальному серверу данных

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        protected Application()
        {
            try
            {
                ErrorHandler.InitializeErrorHandler();
                ErrorHandler.OnExit += new EventHandler(ErrorHandler_OnExit);

                buffer = new RSliceBuffer(72000);
                manager = new DataBaseManager();

                projects = new List<Project>();
                commands = new List<BlockViewCommand>();

                parameters = new Parameter[256];
                for (int index = 0; index < parameters.Length; index++)
                {
                    parameters[index] = new Parameter(index);
                    parameters[index].Identifier = Identifiers.GetGuid(index);
                }

                tech = new Tech();
                commutator = new Commutator(tech, parameters, buffer);

                panels = new List<ParametersViewPanel>();
//                devManUri = new Uri("net.tcp://localhost:57000");
                devManUri = new Uri("net.tcp://127.0.0.1:57000");

                CheckRegistry();

                LoadUri();

                DevManClient.Uri = devManUri;

                DevManClient.Context.Mode = UserMode.Passive;
                DevManClient.Context.Role = Role.Common;

                client = new devTcpManager();
                
                Ping ping = new Ping();
                PingOptions options = new PingOptions();

                try
                {
                    PingReply reply = ping.Send(devManUri.Host);
                    if (reply.Status == IPStatus.Success)
                    {
                        /*DevManClient.Uri = devManUri;

                        DevManClient.Context.Mode = UserMode.Passive;
                        DevManClient.Context.Role = Role.Common;*/

                        DevManClient.Connect();

                        // client = new devTcpManager();

                        string ip = System.Net.Dns.Resolve(devManUri.Host).AddressList[0].ToString();
                        //string ip = System.Net.Dns.GetHostEntry(devManUri.Host).AddressList[0].ToString();

                        client.Client.Host = ip;
                        client.Client.Port = 56000;

                        client.Client.Connect();
                    }
                    else
                    {
                        /*try
                        {
                            client = null;
                        }
                        catch { }*/
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.Fatal));
            }
        }

        /// <summary>
        /// Попытка установить связь с DevMan
        /// </summary>
        public void attemptConnect()
        {
            Ping ping = new Ping();
            PingOptions options = new PingOptions();

            try
            {
                PingReply reply = ping.Send(devManUri.Host);
                if (reply.Status == IPStatus.Success)
                {
                    /*DevManClient.Uri = devManUri;

                    DevManClient.Context.Mode = UserMode.Passive;
                    DevManClient.Context.Role = Role.Common;*/

                    DevManClient.Connect();
/*
                    try
                    {
                        if (client.Client.Connected)
                        {
                            client.Client.Disconnect();
                        }
                    }
                    catch { }

                    try
                    {
                        client.Client.CloseSocket();
                    }
                    catch { }

                    try
                    {
                        client = null;
                    }
                    catch { }

                    client = new devTcpManager();
*/
                    string ip = System.Net.Dns.Resolve(devManUri.Host).AddressList[0].ToString();
                    //string ip = System.Net.Dns.GetHostEntry(devManUri.Host).AddressList[0].ToString();

                    /*client.Client.Host = ip;
                    client.Client.Port = 56000;

                    client.Client.Connect();*/
                }
                else
                {
                    /*try
                    {
                        client = null;
                    }
                    catch {}*/
                }
            }
            catch { }
        }

        /// <summary>
        /// Возвращяет управляющего БД
        /// </summary>
        public DataBaseManager Manager
        {
            get
            {
                return manager;
            }
        }

        /// <summary>
        /// Возвращяет работающего по старому TCP каналу обмена данными
        /// </summary>
        public devTcpManager devTcpManager
        {
            get { return client; }
        }

        /// <summary>
        /// Возвращяет коммутатор
        /// </summary>
        public Commutator Commutator
        {
            get
            {
                return commutator;
            }
        }

        /// <summary>
        /// Определяет проекты СКЦ
        /// </summary>
        public List<Project> Projects
        {
            get
            {
                return projects;
            }
        }

        /// <summary>
        /// Признак подключения к серверу на localhost
        /// </summary>
        public bool AutoLocalhost
        {
            get { return autolocalhost; }
            set { autolocalhost = value; }
        }

        /// <summary>
        /// Включать автоматически запись расхода или нет
        /// </summary>
        public bool AutoStartConsumption
        {
            get { return autostartcon; }
            set { autostartcon = value; }
        }

        /// <summary>
        /// Возвращяет команды блока отображения
        /// </summary>
        public List<BlockViewCommand> Commands
        {
            get
            {
                return commands;
            }
        }

        /// <summary>
        /// текущий проект с которым осуществляется работа
        /// </summary>
        public Project CurrentProject
        {
            get
            {
                try
                {
                    if (projects != null)
                    {
                        foreach (Project prj in projects)
                        {
                            if (prj.Actived)
                            {
                                return prj;
                            }
                        }
                    }
                }
                catch { }
                return null;
            }
        }

        /// <summary>
        /// Возвращяет панели отображающие параметры
        /// </summary>
        public List<ParametersViewPanel> Panels
        {
            get
            {
                return panels;
            }
        }

        /// <summary>
        /// Определяет отображаемый график расхода
        /// </summary>
        public Graphic Graphic_consumption
        {
            get
            {
                return graphic_consumption;
            }

            set
            {
                graphic_consumption = value;
            }
        }

        /// <summary>
        /// Определяет отображаемый график объема
        /// </summary>
        public Graphic Graphic_volume
        {
            get
            {
                return graphic_volume;
            }

            set
            {
                graphic_volume = value;
            }
        }

        /// <summary>
        /// Определяет отображаемый график плотности
        /// </summary>
        public Graphic Graphic_density
        {
            get
            {
                return graphic_density;
            }

            set
            {
                graphic_density = value;
            }
        }

        /// <summary>
        /// Определяет отображаемый график давления
        /// </summary>
        public Graphic Graphic_pressure
        {
            get
            {
                return graphic_pressure;
            }

            set
            {
                graphic_pressure = value;
            }
        }

        /// <summary>
        /// Определяет отображаемый график температуры
        /// </summary>
        public Graphic Graphic_temperature
        {
            get
            {
                return graphic_temperature;
            }

            set
            {
                graphic_temperature = value;
            }
        }

        /// <summary>
        /// Адрес источника данных
        /// </summary>
        public Uri DevManUri
        {
            get { return devManUri; }
            set { devManUri = value; }
        }

        /// <summary>
        /// Адрес источника данных
        /// </summary>
        public Uri DevManUri2
        {
            get { return devManUri2; }
            set { devManUri2 = value; }
        }

        /// <summary>
        /// Обработать реестр
        /// </summary>
        protected void CheckRegistry()
        {
            try
            {
                RegistryKey software = Registry.CurrentUser.OpenSubKey("SOFTWARE\\SKB Oreol\\SKC", true);
                if (software != null)
                {
                    using (software)
                    {
                        software.SetValue("ConfigApp", string.Format("{0}\\configApp.xml", Environment.CurrentDirectory), RegistryValueKind.String);
                        software.SetValue("Projects", string.Format("{0}\\projects\\projects.xml", Environment.CurrentDirectory), RegistryValueKind.String);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// настроить технологические графики
        /// </summary>
        public void UpdateTechGraphics()
        {
            try
            {
                if (_app.Commutator.Technology.Consumption.Index > -1 &&
                    _app.Commutator.Technology.Consumption.Index < _app.Commutator.Parameters.Length)
                {
                    int c_index = _app.Commutator.Technology.Consumption.Index;
                    Parameter p_consumption = _app.Commutator.Parameters[c_index];

                    if (p_consumption != null)
                    {
                        _app.Graphic_consumption.Description = p_consumption.Description;
                    }
                }

                if (_app.Commutator.Technology.Volume.Index > -1 &&
                    _app.Commutator.Technology.Volume.Index < _app.Commutator.Parameters.Length)
                {
                    int v_index = _app.Commutator.Technology.Volume.Index;
                    Parameter p_volume = _app.Commutator.Parameters[v_index];

                    if (p_volume != null)
                    {
                        _app.Graphic_volume.Description = p_volume.Description;
                    }
                }

                if (_app.Commutator.Technology.Density.Index > -1 &&
                    _app.Commutator.Technology.Density.Index < _app.Commutator.Parameters.Length)
                {
                    int d_index = _app.Commutator.Technology.Density.Index;
                    Parameter p_density = _app.Commutator.Parameters[d_index];

                    if (p_density != null)
                    {
                        _app.Graphic_density.Description = p_density.Description;
                    }
                }

                if (_app.Commutator.Technology.Pressure.Index > -1 &&
                    _app.Commutator.Technology.Pressure.Index < _app.Commutator.Parameters.Length)
                {
                    int pr_index = _app.Commutator.Technology.Pressure.Index;
                    Parameter p_pressure = _app.Commutator.Parameters[pr_index];

                    if (p_pressure != null)
                    {
                        _app.Graphic_pressure.Description = p_pressure.Description;
                    }
                }

                if (_app.Commutator.Technology.Temperature.Index > -1 &&
                    _app.Commutator.Technology.Temperature.Index < _app.Commutator.Parameters.Length)
                {
                    int t_index = _app.Commutator.Technology.Temperature.Index;
                    Parameter t_parameter = _app.Commutator.Parameters[t_index];

                    if (t_parameter != null)
                    {
                        _app.Graphic_temperature.Description = t_parameter.Description;
                        _app.Graphic_temperature.Units = string.Format("[{0}]", t_parameter.Units);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Извлечь данные из указанного диапазона времени
        /// </summary>
        /// <param name="startTime">Начало временного диапазона</param>
        /// <param name="finishTime">Конец временного диапазона</param>
        /// <returns>
        /// Массив срезов данных за указанный диапазон времени
        /// или же null если данных за указанный диапазон времени нет.
        /// </returns>
        public Slice[] getData(DateTime startTime, DateTime finishTime)
        {
            try
            {
                Slice[] slices = buffer.FindFromEnd(startTime, finishTime);
                if (slices != null)
                {
                    return slices;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }

            return null;
        }

        /// <summary>
        /// Сохранить этапы работы
        /// </summary>
        public void SaveStages()
        {
            XmlDocument doc = null;
            try
            {
                Project project = CurrentProject;
                if (project != null)
                {
                    string stagesPath = string.Format("{0}\\projects\\{1}", Environment.CurrentDirectory, project.Dir);
                    if (stagesPath != string.Empty && Directory.Exists(stagesPath))
                    {
                        doc = new XmlDocument();
                        XmlElement root = doc.CreateElement("work");

                        XmlAttribute attrib = doc.CreateAttribute("Ver");
                        attrib.InnerText = "1";

                        root.Attributes.Append(attrib);

                        doc.AppendChild(root);

                        SaveStage(doc, root);
                        doc.Save(string.Format("{0}\\{1}", stagesPath, configStages));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Сохранить этапы
        /// </summary>
        /// <param name="document">Докумен в который осуществляется запись настроек приложения</param>
        private void SaveStage(XmlDocument document, XmlNode root)
        {
            try
            {
                XmlNode stagesNode = Commutator.Technology.Stages.Serialize(document, root);
                if (stagesNode != null)
                {
                    document.DocumentElement.AppendChild(stagesNode);
                }
            }
            catch { }
        }

        /// <summary>
        /// Загрузить параметры этапов
        /// </summary>
        /// <param name="root"></param>
        public void LoadStages()
        {
            XmlDocument document = null;
            try
            {
                Project project = CurrentProject;
                if (project != null)
                {
                    string stagesPath = string.Format("{0}\\projects\\{1}", Environment.CurrentDirectory, project.Dir);
                    string totalPathCfg = string.Format("{0}\\{1}", stagesPath, configStages);

                    if (System.IO.File.Exists(totalPathCfg))
                    {
                        document = new XmlDocument();

                        document.Load(totalPathCfg);
                        XmlNode root = document.FirstChild;

                        if (root != null)
                        {
                            if (root.Name == "work")
                            {
                                Commutator.Technology.Stages.Deserialize(root);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Загрузить строку подключения к devMan
        /// </summary>
        private void LoadUri()
        {
            XmlDocument document = null;
            try
            {
                string path = Environment.CurrentDirectory;
                string totalPathCfg = string.Format("{0}\\{1}", path, "configApp.xml");

                if (System.IO.File.Exists(totalPathCfg))
                {
                    document = new XmlDocument();

                    document.Load(totalPathCfg);
                    XmlNode root = document.FirstChild;

                    if (root != null)
                    {
                        if (root.Name == rootName)
                        {
                            if (root.HasChildNodes)
                            {
                                foreach (XmlNode child in root.ChildNodes)
                                {
                                    switch (child.Name)
                                    {
                                        case "autolocalhost":

                                            try
                                            {
                                                autolocalhost = bool.Parse(child.InnerText); ;
                                            }
                                            catch { }
                                            break;

                                        default:
                                            break;
                                    }
                                }

                                foreach (XmlNode child in root.ChildNodes)
                                {
                                    switch (child.Name)
                                    {
                                        case "uri":

                                            try
                                            {
                                                devManUri2 = new Uri(child.InnerText);
                                                if (!autolocalhost)
                                                {
                                                    devManUri = devManUri2;
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
            }
            catch { }
        }

        /// <summary>
        /// Загрузить конфигурацию для программы
        /// </summary>
        public void Load()
        {
            XmlDocument document = null;
            try
            {
                string path = Environment.CurrentDirectory;
                string totalPathCfg = string.Format("{0}\\{1}", path, "configApp.xml");

                if (System.IO.File.Exists(totalPathCfg))
                {
                    document = new XmlDocument();

                    document.Load(totalPathCfg);
                    XmlNode root = document.FirstChild;

                    if (root != null)
                    {
                        if (root.Name == rootName)
                        {
                            if (root.HasChildNodes)
                            {
                                foreach (XmlNode child in root.ChildNodes)
                                {
                                    switch (child.Name)
                                    {
                                        case commutatorName:

                                            commutator.Load(child);
                                            break;

                                        case dataSourceName:

                                            try
                                            {
                                                manager.DataSource = child.InnerText;
                                            }
                                            catch { }
                                            break;

                                        case userIDName:

                                            try
                                            {
                                                manager.UserID = child.InnerText;
                                            }
                                            catch { }
                                            break;

                                        case passwordName:

                                            try
                                            {
                                                manager.Password = child.InnerText;
                                            }
                                            catch { }
                                            break;

                                        case "uri":

                                            try
                                            {
                                                devManUri = new Uri(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case panelsName:

                                            LoadPanels(child);
                                            break;

                                        case "g_tech":

                                            try
                                            {
                                                LoadTechGraphics(child);
                                            }
                                            catch { }
                                            break;

                                        case commandsName:

                                            LoadCommands(child);
                                            break;

                                        case "autostartconsumption":

                                            try
                                            {
                                                autostartcon = bool.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "autolocalhost":

                                            try
                                            {
                                                autolocalhost = bool.Parse(child.InnerText);
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

                LoadProjects();
            }
            catch { }
        }

        /// <summary>
        /// Загрузить команды БО
        /// </summary>
        /// <param name="root">Узел в котором находятся команды БО</param>
        private void LoadCommands(XmlNode root)
        {
            try
            {
                if (root != null)
                {
                    if (root.HasChildNodes && root.Name == commandsName)
                    {
                        foreach (XmlNode child in root.ChildNodes)
                        {
                            BlockViewCommand command = new BlockViewCommand();
                            command.DeserializeFromXml(child);

                            commands.Add(command);
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Загрузить панели
        /// </summary>
        /// <param name="root"></param>
        private void LoadPanels(XmlNode root)
        {
            try
            {
                if (root != null)
                {
                    if (root.HasChildNodes)
                    {
                        foreach (XmlNode child in root.ChildNodes)
                        {
                            switch (child.Name)
                            {
                                case panelName:

                                    try
                                    {
                                        ParametersViewPanel panel = new ParametersViewPanel();
                                        panel.DeSerializeToXmlNode(child);

                                        panels.Add(panel);
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

        /// <summary>
        /// Сохранить настройки программы
        /// </summary>
        public void Save()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                XmlElement root = doc.CreateElement(rootName);

                doc.AppendChild(root);

                XmlNode dataSourceNode = doc.CreateElement(dataSourceName);
                XmlNode userIDNode = doc.CreateElement(userIDName);

                XmlNode passwordNode = doc.CreateElement(passwordName);
                XmlNode uriNode = doc.CreateElement("uri");

                XmlNode autostartNode = doc.CreateElement("autostartconsumption");

                XmlNode autoLocalhost = doc.CreateElement("autolocalhost");

                dataSourceNode.InnerText = manager.DataSource;
                userIDNode.InnerText = manager.UserID;

                passwordNode.InnerText = manager.Password;
                uriNode.InnerText = devManUri2.ToString();

                autostartNode.InnerText = autostartcon.ToString();

                autoLocalhost.InnerText = autolocalhost.ToString();

                root.AppendChild(dataSourceNode);
                root.AppendChild(userIDNode);

                root.AppendChild(passwordNode);
                root.AppendChild(uriNode);

                root.AppendChild(autostartNode);

                root.AppendChild(autoLocalhost);

                commutator.Save(doc, commutatorName);

                SavePanels(doc, root);
                SaveCommands(doc, root);

                SaveGraphigTechParameters(doc, root);

                doc.Save(string.Format("{0}\\{1}", Environment.CurrentDirectory, "configApp.xml"));
                SaveProjects();
            }
            catch { }
        }

        /// <summary>
        /// Сохранить команды БО
        /// </summary>
        /// <param name="doc">Документ в который сохранить настроки команд БО</param>
        private void SaveCommands(XmlDocument doc, XmlNode root)
        {
            try
            {
                if (doc != null && root != null)
                {
                    XmlNode commandRoot = doc.CreateElement(commandsName);
                    foreach (BlockViewCommand command in commands)
                    {
                        XmlNode node = command.SerializeToXml(doc);
                        if (node != null)
                        {
                            commandRoot.AppendChild(node);
                        }
                    }

                    doc.DocumentElement.AppendChild(commandRoot);
                }
            }
            catch { }
        }

        /// <summary>
        /// Сохранить настройки графиков технологических параметров
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение</param>
        private void SaveGraphigTechParameters(XmlDocument doc, XmlNode root)
        {
            try
            {
                if (doc != null)
                {
                    XmlNode root_g = doc.CreateElement("g_tech");
                    
                    XmlNode consumptionNode = graphic_consumption.SerializeToXml(doc, "consumption");
                    XmlNode volumeNode = graphic_volume.SerializeToXml(doc, "volume");

                    XmlNode densityNode = graphic_density.SerializeToXml(doc, "density");
                    XmlNode pressureNode = graphic_pressure.SerializeToXml(doc, "pressure");

                    XmlNode temperatureNode = graphic_temperature.SerializeToXml(doc, "temperature");

                    if (consumptionNode != null) root_g.AppendChild(consumptionNode);
                    if (volumeNode != null) root_g.AppendChild(volumeNode);

                    if (densityNode != null) root_g.AppendChild(densityNode);
                    if (pressureNode != null) root_g.AppendChild(pressureNode);

                    if (temperatureNode != null) root_g.AppendChild(temperatureNode);

                    root.AppendChild(root_g);
                }
            }
            catch { }
        }

        /// <summary>
        /// Загрузить настройки графиков
        /// </summary>
        /// <param name="root"></param>
        private void LoadTechGraphics(XmlNode root)
        {
            try
            {
                if (root != null && root.HasChildNodes)
                {
                    foreach (XmlNode child in root.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "consumption":
                                
                                try
                                {
                                    graphic_consumption.DeSerializeToXml(child);
                                }
                                catch { }
                                break;

                            case "volume":

                                try
                                {
                                    graphic_volume.DeSerializeToXml(child);
                                }
                                catch { }
                                break;

                            case "density":

                                try
                                {
                                    graphic_density.DeSerializeToXml(child);
                                }
                                catch { }
                                break;

                            case "pressure":

                                try
                                {
                                    graphic_pressure.DeSerializeToXml(child);
                                }
                                catch { }
                                break;

                            case "temperature":

                                try
                                {
                                    graphic_temperature.DeSerializeToXml(child);
                                }
                                catch { }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Сохранить панели
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение панелей</param>
        /// <param name="root">Корневой узел</param>
        private void SavePanels(XmlDocument doc, XmlNode root)
        {
            try
            {
                XmlNode root_panels = doc.CreateElement(panelsName);

                foreach (ParametersViewPanel panel in panels)
                {
                    XmlNode panelNode = panel.SerializeToXmlNode(doc);
                    if (panelNode != null)
                    {
                        root_panels.AppendChild(panelNode);
                    }
                }

                root.AppendChild(root_panels);
            }
            catch { }
        }

        /// <summary>
        /// Сохранить проекты
        /// </summary>
        protected void SaveProjects()
        {
            try
            {
                string projectFolderPath = string.Format("{0}\\{1}", Environment.CurrentDirectory, "projects");
                DirectoryInfo info = new DirectoryInfo(projectFolderPath);

                if (info.Exists)
                {
                    SaveProjectList(info);
                }
                else
                {
                    // ---- нету папаки с проектами ----
                    
                    info.Create();          // создаем папку с проектами
                    SaveProjectListFull(info);
                }

                CheckProjectsFolders(info);
            }
            catch { }
        }

        /// <summary>
        /// Сохранить проекты в корневом каталоге. только файл каталог папки проетов не трогать
        /// </summary>
        /// <param name="d_info"></param>
        protected void SaveProjectList(DirectoryInfo d_info)
        {
            XmlDocument document = null;
            try
            {
                document = new XmlDocument();
                XmlNode root = document.CreateElement("projects");

                foreach (Project project in projects)
                {
                    XmlNode p_node = project.SerializeToXmlNode(document);
                    if (p_node != null)
                    {
                        root.AppendChild(p_node);
                    }
                }

                document.AppendChild(root);
                document.Save(string.Format("{0}\\{1}", d_info.FullName, "projects.xml"));
            }
            catch { }
        }

        /// <summary>
        /// Сохранить проекты в корневом каталоге и создавать для каждого пректа папку проекта
        /// </summary>
        /// <param name="d_info"></param>
        protected void SaveProjectListFull(DirectoryInfo d_info)
        {
            SaveProjectList(d_info);
            foreach (Project project in projects)
            {
                if (project != null)
                {
                    if (project.DB_Name != string.Empty)
                    {
                        try
                        {
                            d_info.CreateSubdirectory(project.Dir);
                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// проверить папки для проектов
        /// </summary>
        /// <param name="d_info"></param>
        protected void CheckProjectsFolders(DirectoryInfo d_info)
        {
            try
            {
                if (d_info != null)
                {
                    if (projects.Count > 0)
                    {
                        string[] folders = Directory.GetDirectories(d_info.FullName);
                        if (folders != null)
                        {
                            foreach (Project project in projects)
                            {
                                bool find = false;
                                foreach (string folder in folders)
                                {
                                    if (project.Dir == folder)
                                    {
                                        find = true;
                                        break;
                                    }
                                }

                                if (find == false)
                                {
                                    d_info.CreateSubdirectory(project.Dir);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Загрузить проекты
        /// </summary>
        protected void LoadProjects()
        {
            XmlDocument document = null;
            try
            {
                string projectFolderPath = string.Format("{0}\\{1}", Environment.CurrentDirectory, "projects");
                DirectoryInfo info = new DirectoryInfo(projectFolderPath);

                if (info.Exists)
                {
                    if (File.Exists(string.Format("{0}\\{1}", info.FullName, "projects.xml")))
                    {
                        document = new XmlDocument();
                        document.Load(string.Format("{0}\\{1}", info.FullName, "projects.xml"));

                        XmlNode root = document.FirstChild;
                        if (root != null && root.Name == "projects")
                        {
                            if (root.HasChildNodes)
                            {
                                foreach (XmlNode child in root.ChildNodes)
                                {
                                    switch (child.Name)
                                    {
                                        case Project.ProjectRootName:

                                            try
                                            {
                                                Project project = new Project();
                                                project.DeserializeFromXmlNode(child);

                                                projects.Add(project);
                                            }
                                            catch { }
                                            break;


                                        default:
                                            break;
                                    }
                                }
                            }

                            CheckProjectsFolders(info);
                        }
                    }
                }
                else
                {
                }
            }
            catch { }
        }

        /// <summary>
        /// Создать экземпляр класса реализующего СКЦ 2
        /// </summary>
        /// <returns>Экземпляр класса реализующего приложение СКЦ 2</returns>
        public static Application CreateInstance()
        {
            if (_app == null)
            {
                _app = new Application();
                ErrorHandler.InitializeErrorHandler();
            }

            return _app;
        }

        /// <summary>
        /// Обрабатывает событие завершение приложения
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        protected static void ErrorHandler_OnExit(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch { }
        }

        /// <summary>
        /// Выделить число с плавающей точкой из строки
        /// </summary>
        /// <param name="single">Строка содержащая число</param>
        /// <returns>Значение или Nan если не удалось выполнить преобразование</returns>
        public static float ParseSingle(string single)
        {
            try
            {
                string ds = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                string value = single;

                value = value.Replace(".", ds);
                value = value.Replace(",", ds);

                return float.Parse(value);
            }
            catch
            { }

            return float.NaN;
        }

        /// <summary>
        /// Выделить число с плавающей точкой из строки
        /// </summary>
        /// <param name="single">Строка содержащая число</param>
        /// <returns>Значение или Nan если не удалось выполнить преобразование</returns>
        public static double ParseDouble(string single)
        {
            try
            {
                string ds = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                string value = single;

                value = value.Replace(".", ds);
                value = value.Replace(",", ds);

                return double.Parse(value);
            }
            catch
            { }

            return double.NaN;
        }
    }
}