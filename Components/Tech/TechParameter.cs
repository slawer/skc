using System;
using System.Xml;
using System.Threading;

namespace SKC
{
    /// <summary>
    /// Реализует структуру в которой храняться данные для технологического параметра
    /// </summary>
    public class TechParameter
    {
        private float _value;                       // текущее значение технологического параметра
        private float _correct;                     // поправочный коэффициент для технологического параметра

        private int _index;                         // номер параметра в списке параметров приложения из которого извлекать данные
        private int _indexToSave;                   // номер параметра в который сохранять значение данного параметра в devMan

        private string g_type;                      // идентификатор параметра
        private ReaderWriterLockSlim slim;          // синхронизирует доступ к полям структуры

        private string format = "{0:F2}";           // формат выводимого числа
        private string f_value;                     // отформатированное значение параметра

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public TechParameter()
        {
            slim = new ReaderWriterLockSlim();

            _correct = 1.0f;
            _value = float.NaN;
            
            _index = -1;
            _indexToSave = -1;            
        }

        /// <summary>
        /// Определяет текущее значение параметра
        /// </summary>
        public float Value
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return _value;
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
                        _value = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет формат выводимого числа
        /// </summary>
        public string Format
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return format;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return "{0:F2}";
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        format = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет отформатированное значение параметра
        /// </summary>
        public string FormattedValue
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        if (f_value == null)
                        {
                            f_value = "-----";
                        }
                        return f_value;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return "{0:F2}";
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        f_value = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет поправочный коэффициент для параметра
        /// </summary>
        public float CorrectValue
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return _correct;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return _correct;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        _correct = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет номер параметра из списка параметров в 
        /// приложении который поставляет данные для технологического параметра
        /// </summary>
        public int Index
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return _index;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        _index = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет номер параметра в который сохранять
        /// значение данного параметра в devMan.
        /// </summary>
        public int IndexToSave
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return _indexToSave;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        _indexToSave = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Идентификатор параметра для рапорта
        /// </summary>
        public string gType
        {
            get { return g_type; }
            set { g_type = value; }
        }

        /// <summary>
        /// Имя корневого узла в который сохраняется параметр
        /// </summary>
        public const string ParameterRootName = "TechParameter";

        /// <summary>
        /// Сохранить параметр в Xml узел
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранения параметра</param>
        /// <returns></returns>
        public XmlNode SerializeToXml(XmlDocument doc)
        {
            try
            {
                if (doc != null)
                {
                    XmlNode root = doc.CreateElement(ParameterRootName);

                    XmlNode _valueNode = doc.CreateElement("value");
                    XmlNode _correctNode = doc.CreateElement("correct");

                    XmlNode _indexNode = doc.CreateElement("index");
                    XmlNode _indexToSaveNode = doc.CreateElement("indextosave");

                    XmlNode _g_typeNode = doc.CreateElement("type");

                    _valueNode.InnerText = _value.ToString();
                    _correctNode.InnerText = _correct.ToString();

                    _indexNode.InnerText = _index.ToString();
                    _indexToSaveNode.InnerText = _indexToSave.ToString();

                    _g_typeNode.InnerText = g_type;

                    root.AppendChild(_valueNode);
                    root.AppendChild(_correctNode);

                    root.AppendChild(_indexNode);
                    root.AppendChild(_indexToSaveNode);

                    root.AppendChild(_g_typeNode);

                    return root;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Десериализовать параметр из Xml узла
        /// </summary>
        /// <param name="root"></param>
        public void DeserializeFromXml(XmlNode root)
        {
            try
            {
                if (root != null && root.Name == ParameterRootName)
                {
                    if (root.HasChildNodes)
                    {
                        foreach (XmlNode child in root.ChildNodes)
                        {
                            switch (child.Name)
                            {
                                case "value":

                                    try
                                    {
                                        _value = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "correct":

                                    try
                                    {
                                        _correct = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "index":

                                    try
                                    {
                                        _index = int.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "indextosave":

                                    try
                                    {
                                        _indexToSave = int.Parse(child.InnerText);
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