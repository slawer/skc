using System;
using System.Xml;
using System.Drawing;
using System.Threading;

using GraphicComponent;

namespace SKC
{
    /// <summary>
    /// Реализует панель в которой храняться параметры для отображения
    /// </summary>
    public class ParametersViewPanel
    {
        // ---- константы ----

        /// <summary>
        /// узел в котором сохраняется панель
        /// </summary>
        protected const string panelName = "panel";

        /// <summary>
        /// имя узла в котром сохраняется первый параметр
        /// </summary>
        protected const string p1Name = "parameter1";

        /// <summary>
        /// имя узла в котром сохраняется второй параметр
        /// </summary>
        protected const string p2Name = "parameter2";

        /// <summary>
        /// имя узла в котром сохраняется третий параметр
        /// </summary>
        protected const string p3Name = "parameter3";

        /// <summary>
        /// имя узла в котром сохраняется четвертый параметр
        /// </summary>
        protected const string p4Name = "parameter4";

        /// <summary>
        /// имя узла в котром сохраняется пятый параметр
        /// </summary>
        protected const string p5Name = "parameter5";

        // -------------------

        protected GraphicPanelParameter parameter_1 = null;     // первый параметр
        protected GraphicPanelParameter parameter_2 = null;     // второй параметр

        protected GraphicPanelParameter parameter_3 = null;     // третий параметр
        protected GraphicPanelParameter parameter_4 = null;     // четвертый параметр

        protected GraphicPanelParameter parameter_5 = null;     // пятый параметр

        protected String panelDescription = string.Empty;       // текстовое описание панели

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public ParametersViewPanel()
        {
            parameter_1 = new GraphicPanelParameter();
            parameter_2 = new GraphicPanelParameter();

            parameter_3 = new GraphicPanelParameter();
            parameter_4 = new GraphicPanelParameter();

            parameter_5 = new GraphicPanelParameter();
        }

        /// <summary>
        /// определяет первый отображаемый параметр
        /// </summary>
        public GraphicPanelParameter Parameter1
        {
            get { return parameter_1; }
            set { parameter_1 = value; }
        }

        /// <summary>
        /// определяет первый отображаемый параметр
        /// </summary>
        public GraphicPanelParameter Parameter2
        {
            get { return parameter_2; }
            set { parameter_2 = value; }
        }

        /// <summary>
        /// определяет первый отображаемый параметр
        /// </summary>
        public GraphicPanelParameter Parameter3
        {
            get { return parameter_3; }
            set { parameter_3 = value; }
        }

        /// <summary>
        /// определяет первый отображаемый параметр
        /// </summary>
        public GraphicPanelParameter Parameter4
        {
            get { return parameter_4; }
            set { parameter_4 = value; }
        }

        /// <summary>
        /// определяет первый отображаемый параметр
        /// </summary>
        public GraphicPanelParameter Parameter5
        {
            get { return parameter_5; }
            set { parameter_5 = value; }
        }

        /// <summary>
        /// Определяет текстовое описание панели отображения параметров
        /// </summary>
        public string Description
        {
            get { return panelDescription; }
            set { panelDescription = value; }
        }

        /// <summary>
        /// Сохранить панель в узел XML
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public XmlNode SerializeToXmlNode(XmlDocument doc)
        {
            try
            {
                XmlNode root = doc.CreateElement(panelName);

                XmlNode descNode = doc.CreateElement("description");

                XmlNode parameter1Node = parameter_1.SerializeToXmlNode(doc, p1Name);
                XmlNode parameter2Node = parameter_2.SerializeToXmlNode(doc, p2Name);

                XmlNode parameter3Node = parameter_3.SerializeToXmlNode(doc, p3Name);
                XmlNode parameter4Node = parameter_4.SerializeToXmlNode(doc, p4Name);

                XmlNode parameter5Node = parameter_5.SerializeToXmlNode(doc, p5Name);

                descNode.InnerText = panelDescription;

                root.AppendChild(descNode);

                root.AppendChild(parameter1Node);
                root.AppendChild(parameter2Node);

                root.AppendChild(parameter3Node);
                root.AppendChild(parameter4Node);

                root.AppendChild(parameter5Node);

                return root;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Загрузить панель из узела XML
        /// </summary>
        public void DeSerializeToXmlNode(XmlNode root)
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
                                case "description":

                                    try
                                    {
                                        panelDescription = child.InnerText;
                                    }
                                    catch { }
                                    break;

                                case p1Name:

                                    try
                                    {
                                        parameter_1.DeSerializeToXmlNode(child);
                                    }
                                    catch { }
                                    break;

                                case p2Name:

                                    try
                                    {
                                        parameter_2.DeSerializeToXmlNode(child);
                                    }
                                    catch { }
                                    break;

                                case p3Name:

                                    try
                                    {
                                        parameter_3.DeSerializeToXmlNode(child);
                                    }
                                    catch { }
                                    break;

                                case p4Name:

                                    try
                                    {
                                        parameter_4.DeSerializeToXmlNode(child);
                                    }
                                    catch { }
                                    break;

                                case p5Name:

                                    try
                                    {
                                        parameter_5.DeSerializeToXmlNode(child);
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

    /// <summary>
    /// Реализует отображаемый параметр панели отображения параметров
    /// </summary>
    public class GraphicPanelParameter
    {
        // ---- константы ----

        /// <summary>
        /// имя узла в котором сохраняется отображаемый параметр
        /// </summary>
        protected const string rootName = "GraphicPanelParameter";

        /// <summary>
        /// имя узла в котором сохраняется минимальное значение параметра
        /// </summary>
        protected const string minName = "min";

        /// <summary>
        /// имя узла в котором сохраняется максимальное значение параметра
        /// </summary>
        protected const string maxName = "max";

        /// <summary>
        /// 
        /// </summary>
        protected const string colorName = "color";

        // -------------------

        private Graphic graphic = null;             // график отображаемого параметра
        private Parameter parameter = null;         // отображаемый параметр

        private Color color = Color.Black;          // цвет которым отрисовывать график
        private float min = 0, max = 65535;         // диапазон отображаемых значений


        private ReaderWriterLockSlim slim = null;   // синхронизатор

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        public GraphicPanelParameter()
        {
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="View">Используемы параметр</param>
        public GraphicPanelParameter(Parameter View) :
            this()
        {
            if (View != null)
            {
                parameter = View;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="View">Параметр для отображения</param>
        /// <param name="gView">График который отображает параметр</param>
        public GraphicPanelParameter(Parameter View, Graphic gView) :
            this()
        {
            graphic = gView;
            parameter = View;

            InitializeGraphic();
        }

        /// <summary>
        /// Определяет отображаемый параметр
        /// </summary>
        public Parameter Parameter
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return parameter;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return null;
            }

            set
            {
                if (slim.TryEnterWriteLock(350))
                {
                    try
                    {
                        parameter = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет график отображаемого параметра
        /// </summary>
        public Graphic Graphic
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return graphic;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return null;
            }

            set
            {
                if (slim.TryEnterWriteLock(350))
                {
                    try
                    {
                        graphic = value;
                        InitializeGraphic();
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет минимальное значение отображения параметра
        /// </summary>
        public float Min
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return min;
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
                if (slim.TryEnterWriteLock(350))
                {
                    try
                    {
                        min = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет максимальное значение отображения параметра
        /// </summary>
        public float Max
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return max;
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
                if (slim.TryEnterWriteLock(350))
                {
                    try
                    {
                        max = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет цвет которым отрисовывать график
        /// </summary>
        public Color Color
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return color;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return Color.Black;
            }

            set
            {
                if (slim.TryEnterWriteLock(350))
                {
                    try
                    {
                        color = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Сохранить параметр панели в узел XML
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public XmlNode SerializeToXmlNode(XmlDocument doc, String rootName)
        {
            try
            {
                XmlNode root = doc.CreateElement(rootName);

                XmlNode minNode = doc.CreateElement(minName);
                XmlNode maxNode = doc.CreateElement(maxName);

                XmlNode colorNode = doc.CreateElement(colorName);

                minNode.InnerText = min.ToString();
                maxNode.InnerText = max.ToString();

                colorNode.InnerText = color.ToArgb().ToString();

                root.AppendChild(minNode);
                root.AppendChild(maxNode);

                root.AppendChild(colorNode);

                if (parameter != null)
                {
                    XmlNode parameterNode = parameter.SerializeToXmlNode(doc);
                    root.AppendChild(parameterNode);
                }

                return root;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Загрузить параметр панели из узела XML
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public void DeSerializeToXmlNode(XmlNode root)
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
                                case minName:

                                    try
                                    {
                                        min = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case maxName:

                                    try
                                    {
                                        max = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case colorName:

                                    try
                                    {
                                        int argb = int.Parse(child.InnerText);
                                        color = System.Drawing.Color.FromArgb(argb);
                                    }
                                    catch { }
                                    break;

                                case "parameter":

                                    try
                                    {
                                        if (parameter == null)
                                        {
                                            parameter = new Parameter();
                                        }
                                        parameter.DeserializeFromXmlNode(child);
                                    }
                                    catch 
                                    {
                                        parameter = null;
                                    }
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
        /// Синхронизировать график и параметры параметры для отображения
        /// </summary>
        protected void InitializeGraphic()
        {
            try
            {
                if (slim.TryEnterWriteLock(350))
                {
                    try
                    {
                        if (graphic != null)
                        {
                            graphic.Color = color;

                            graphic.Range.Min = min;
                            graphic.Range.Max = max;

                            if (parameter != null)
                            {
                                graphic.Description = parameter.Description;
                            }
                        }
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
            catch { }
        }
    }
}