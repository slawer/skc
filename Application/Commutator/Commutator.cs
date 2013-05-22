using System;
using System.Xml;
using System.Threading;

using Buffering;

using WCF;
using WCF.WCF_Client;

namespace SKC
{
    /// <summary>
    /// реализует коммутацию и передачу данных в системе
    /// </summary>
    public class Commutator
    {
        protected Tech tech;                            // технология
        protected Parameter[] parameters = null;        // список параметров СКЦ 2

        protected RSliceBuffer buffer = null;           // буфер, который хранит срезы данных пришедших от DevMan2;

        /// <summary>
        /// Возникает когда параметры получили значения от devMan
        /// </summary>
        public event EventHandler onParameterUpdated;

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="_tech">Технологическая примочка</param>
        /// <param name="_parameters">Список параметров системы</param>
        /// <param name="_buffer">Буфер в который сохраняются данные</param>
        public Commutator(Tech _tech, Parameter[] _parameters, RSliceBuffer _buffer)
        {
            if (_tech != null && _parameters != null && _buffer != null)
            {
                tech = _tech;
                buffer = _buffer;

                parameters = _parameters;

                tech.onCalculate += new EventHandler(tech_onCalculate);
                tech.onJop += new EventHandler(tech_onJop);
                DevManClient.onReceive += new ReceivedEventHandler(DevManClient_onReceive);
            }
            else
                ErrorHandler.WriteToLog(this, new ErrorArgs("Коммутатор не смог выполнить инициализацию", ErrorType.Fatal));
        }

        /// <summary>
        /// Очистить данные
        /// </summary>
        public void ClearData()
        {
            buffer.Clear();
        }

        /// <summary>
        /// Возвращяет список параметров
        /// </summary>
        public Parameter[] Parameters
        {
            get
            {
                return parameters;
            }
        }

        /// <summary>
        /// Возвращяет технологию
        /// </summary>
        public Tech Technology
        {
            get
            {
                return tech;
            }
        }

        /// <summary>
        /// технологическая примочка выполнила вычисления
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        protected void tech_onCalculate(object sender, EventArgs e)
        {
            try
            {
                TechStage current = tech.Stages.Current;
                if (current != null)
                {
                    /*DevManClient.UpdateParameter(tech.Consumption.IndexToSave, current.Consumption);
                    DevManClient.UpdateParameter(tech.Volume.IndexToSave, current.Volume);*/
                    DevManClient.UpdateParameter(tech.ProccessVolume.IndexToSave, tech.Stages.ProccessVolume);

                    // ----------- тестовое -----------

                    foreach (Rgr rgr in tech.Rgrs)
                    {
                        DevManClient.UpdateParameter(rgr.Consumption.IndexToSave, rgr.CurrentConsumption);
                        DevManClient.UpdateParameter(rgr.Volume.IndexToSave, rgr.CurrentVolume);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(sender, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        /// <summary>
        /// наладческая прижмочка выполнила
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tech_onJop(object sender, EventArgs e)
        {
            try
            {
                /*DevManClient.UpdateParameter(tech.Consumption.IndexToSave, tech.Consumption.Value);
                DevManClient.UpdateParameter(tech.Volume.IndexToSave, tech.Volume.Value);*/

                // ----------- тестовое -----------

                foreach (Rgr rgr in tech.Rgrs)
                {
                    DevManClient.UpdateParameter(rgr.Consumption.IndexToSave, rgr.Consumption.Value);
                    DevManClient.UpdateParameter(rgr.Volume.IndexToSave, rgr.Volume.Value);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(sender, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        protected DateTime lastTime = DateTime.Now; // DateTime.MinValue;
        protected TimeSpan tInterval = new TimeSpan(0, 0, 0, 0, 300);

        /// <summary>
        /// Предоставляет доступ к времени последнего чтения данных из DevMan
        /// </summary>
        public DateTime LastTime
        {
            get { return lastTime; }
            set { lastTime = value; }
        }


        /// <summary>
        /// Получили данные от devMan
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        protected void DevManClient_onReceive(object sender, ReceivedEventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                if (now > lastTime)
                {
                    TimeSpan interval = now - lastTime;
                    if (interval.Ticks > tInterval.Ticks)
                    {
                        lastTime = now;
                        //buffer.Append(new Slice(DateTime.Now, e.Slice));

                        foreach (Parameter parameter in parameters)
                        {
                            PDescription channel = parameter.Channel;
                            if (channel != null)
                            {
                                if (channel.Number >= 0 && channel.Number < e.Slice.Length)
                                {
                                    parameter.setCurrent(e.Slice[channel.Number]);
                                    e.Slice[channel.Number] = parameter.CurrentValue;
                                }
                            }
                        }

                        buffer.Append(new Slice(DateTime.Now, e.Slice));

                        tech.Updated(parameters);
                        if (onParameterUpdated != null)
                        {
                            onParameterUpdated(this, EventArgs.Empty);
                        }
                    }
                }
                else
                    lastTime = now;
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(sender, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        /// <summary>
        /// получить минимальное время параметра
        /// </summary>
        /// <param name="number">Номер параметра</param>
        /// <returns>Найденное время параметра</returns>
        public DateTime MinTimeParameter()
        {
            try
            {
                return buffer.GetMinTime();
            }
            catch { }
            return DateTime.MinValue;
        }

        /// <summary>
        /// Загрузить настройки
        /// </summary>
        public void Load(XmlNode root)
        {
            try
            {
                if (root != null)
                {
                    XmlNode root_node = root;
                    if (root_node != null)
                    {
                        XmlNodeList childs = root_node.ChildNodes;
                        if (childs != null)
                        {
                            foreach (XmlNode child in childs)
                            {
                                switch (child.Name)
                                {
                                    case "parameter":

                                        Parameter parameter = new Parameter(-1);
                                        parameter.DeserializeFromXmlNode(child);

                                        if (parameter.SelfIndex > -1 && parameter.SelfIndex < parameters.Length)
                                        {
                                            parameters[parameter.SelfIndex] = parameter;
                                        }
                                        break;

                                    case Tech.TechRoot:

                                        tech.Load(child);
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

        /// <summary>
        /// Сохранить настройки
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение параметров</param>
        /// <param name="root">Узел в который сохранять настройки</param>
        /// <param name="rootName">имя узла в который сохранять настроки</param>
        public void Save(XmlDocument doc, String rootName)
        {
            try
            {
                if (doc != null && rootName != string.Empty)
                {
                    XmlNode root = doc.CreateElement(rootName);
                    if (parameters != null)
                    {
                        foreach (Parameter parameter in parameters)
                        {
                            XmlNode xml_parameter = parameter.SerializeToXmlNode(doc);
                            if (xml_parameter != null)
                            {
                                root.AppendChild(xml_parameter);
                            }
                        }
                    }

                    tech.Save(doc, root);
                    doc.DocumentElement.AppendChild(root);
                }
            }
            catch { }
        }
    }
}