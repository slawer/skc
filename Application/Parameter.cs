using System;
using System.Xml;
using System.Threading;

using WCF;
using DeviceManager;
using WCF.WCF_Client;

namespace SKC
{
    /// <summary>
    /// Реализует параметр СКЦ
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// узел в который сохраняется параметр
        /// </summary>
        private const string parameterRoot = "parameter";

        /// <summary>
        /// узел в котором хранится название параметра
        /// </summary>
        private const string parameterName = "name";

        /// <summary>
        /// узел в котором хранится описание параметра (краткое имя параметра)
        /// </summary>
        private const string descriptionName = "desc";

        /// <summary>
        /// узел в котором храниться единицы измерения параметра
        /// </summary>
        private const string unitsName = "units";

        /// <summary>
        /// узел в котором хранится аварийное значение
        /// </summary>
        private const string alarmValueName = "alarm";

        /// <summary>
        /// узел в котором хранится блокировочное значение
        /// </summary>
        private const string blockingValueName = "blocking";

        /// <summary>
        /// узел в котором хранится контролировать или нет аварийное значение параметра
        /// </summary>
        private const string controlAlarmName = "iscontrolalarm";

        /// <summary>
        /// узел в котором хранится контролировать или нет блокировочное значение параметра
        /// </summary>
        private const string controlBlockingName = "iscontrolblocking";

        /// <summary>
        /// имя узла в котором хранится интервал значений параметра
        /// </summary>
        private const string rangeName = "ParameterRange";

        /// <summary>
        /// имя узла в котором хранится сохранять параметр в БД или нет
        /// </summary>
        private const string saveToDBName = "issavetodb";

        /// <summary>
        /// имя узла в котором хранится интервал записи параметра в БД
        /// </summary>
        private const string intervalToSaveName = "intervaltosave";

        /// <summary>
        /// имя узла в котором хранится пороговое значение
        /// </summary>
        private const string thresholdToBDName = "threashold";

        /// <summary>
        /// имя узла в котором хранится номер канала который ассоциирован с параметром(источник данных для параметра)
        /// </summary>
        private const string channelNumberName = "channelnumber";

        /// <summary>
        /// имя узла в котором хранится текущее значение параметра
        /// </summary>
        private const string currentValueName = "currentvalue";

        /// <summary>
        /// имя узла в котором хранится собственный индекс
        /// </summary>
        private const string selfIndexName = "selfindex";

        /// <summary>
        /// имя узла в котором хранится идентификатор параметра
        /// </summary>
        private const string guidName = "guid";

        /// <summary>
        /// имя узла в котором хранится номер параметра в списке от devMan
        /// </summary>
        private const string devManindexName = "devManindex";
        
        /// <summary>
        /// имя узла в котором хранится текстовое описание параметра от devMan
        /// </summary>
        private const string devManDescriptionName = "devManDescription";

        // --------------------------------------

        protected string name;                          // имя параметра
        protected string description;                   // описание параметра (краткое имя параметра)

        protected string units;                         // единицы измерения параметра

        protected float alarmValue;                     // аварийное значение
        protected float blockingValue;                  // блокировочное значение

        protected bool controlAlarm;                    // контролировать или нет аварийное значение параметра
        protected bool controlBlocking;                 // контролировать или нет блокировочное значение параметра

        protected ParameterRange range;                 // интервал значений параметра

        protected bool saveToDB;                        // сохранять параметр в БД или нет
        protected int intervalToSave;                   // интервал записи параметра в БД
        protected DateTime db_time;                     // время последнего сохранения параметра в БД

        protected int thresholdToBD;                    // пороговое значение
        protected ReaderWriterLockSlim slim;            // синхронизатор

        // -----------
                
        protected int channelNumber;                    // номер канала который ассоциирован с параметром(источник данных для параметра)
        protected ReaderWriterLockSlim c_slim;          // синхронизатор для номера канала

        protected float currentValue;                   // текущее значение параметра
        protected float clearValue;                     // чистое значение, не откалиброванное

        protected ReaderWriterLockSlim v_slim;          // синхронизатор текущего параметра

        protected PDescription channel;                 // канал от devMan
        protected int selfIndex = -1;                   // собственный индекс

        protected Guid guid;                            // идентификатор параметра

        private int devManindex = -1;                   // номер параметра в списке от devMan
        private string devManDescription;               // текстовое описание параметра от devMan        

        private Transformation transformation;          // реализует калибровку значения параметра

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Parameter(int i_index)
        {
            slim = new ReaderWriterLockSlim();
            c_slim = new ReaderWriterLockSlim();

            v_slim = new ReaderWriterLockSlim();

            name = "Параметр не определен";
            description = "-----";

            range = new ParameterRange();
            intervalToSave = 500;

            selfIndex = i_index;

            guid = Guid.NewGuid();
            db_time = DateTime.MinValue;

            transformation = new Transformation();

            Transformation.TCondition t1 = new Transformation.TCondition();
            Transformation.TCondition t2 = new Transformation.TCondition();

            t1.Result = 0;
            t1.Signal = 0;

            t2.Result = 65535;
            t2.Signal = 65535;

            transformation.Insert(t1);
            transformation.Insert(t2);

        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Parameter()
        {
            slim = new ReaderWriterLockSlim();
            c_slim = new ReaderWriterLockSlim();

            v_slim = new ReaderWriterLockSlim();

            name = "Параметр не определен";
            description = "-----";

            range = new ParameterRange();
            intervalToSave = 100;

            selfIndex = -1;

            guid = Guid.NewGuid();
        }

        /// <summary>
        /// Возвращяет функцию калибровки параметра
        /// </summary>
        public Transformation Transformation
        {
            get
            {
                return transformation;
            }
        }

        /// <summary>
        /// Возвращяет номер параметра в списке
        /// </summary>
        public int SelfIndex
        {
            get
            {
                return selfIndex;
            }
        }

        /// <summary>
        /// Определяет имя параметра(полное).
        /// </summary>
        public string Name
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return name;
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
                        name = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет текстовое описание параметра(краткое).
        /// </summary>
        public string Description
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return description;
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
                        description = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет идентификатор параметра
        /// </summary>
        public Guid Identifier
        {
            get
            {
                return guid;
            }

            set
            {
                guid = value;
            }
        }

        /// <summary>
        /// Определяет текстовое описание единиц в которых измеряется параметр
        /// </summary>
        public string Units
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return units;
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
                        units = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет аварийное значение параметра
        /// </summary>
        public float Alarm
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return alarmValue;
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
                        alarmValue = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет блокировочное значение параметра
        /// </summary>
        public float Blocking
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return blockingValue;
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
                        blockingValue = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет контролировать аварийное значение или нет
        /// </summary>
        public bool IsControlAlarm
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return controlAlarm;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        controlAlarm = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет контролировать блокировочное значение или нет
        /// </summary>
        public bool IsControlBlocking
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return controlBlocking;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        controlBlocking = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет канал от devMan
        /// </summary>
        public PDescription Channel
        {
            get
            {
                if (c_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        if (channel == null)
                        {
                            if (devManindex > -1)
                            {
                                channel = new PDescription(devManindex, devManDescription, DeviceManager.FormulaType.Default);
                            }
                        }

                        return channel;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return null;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        channel = value;
                        if (channel != null)
                        {
                            devManindex = channel.Number;
                            devManDescription = channel.Description;
                        }
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет диапазон возможных значений параметра
        /// </summary>
        public ParameterRange Range
        {
            get
            {
                return range;
            }
        }

        /// <summary>
        /// Определяет сохранять параметр в Бд или нет
        /// </summary>
        public bool SaveToDB
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return saveToDB;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        saveToDB = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет интервал сохранения параметра в БД
        /// </summary>
        public int IntervalToSaveToDB
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return intervalToSave;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }                    
                }

                return 100;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        intervalToSave = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Время последней записи параметра в БД
        /// </summary>
        public DateTime DB_Time
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return db_time;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return DateTime.Now;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        db_time = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет пороговое значение
        /// </summary>
        public int ThresholdToBD
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return thresholdToBD;
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
                        thresholdToBD = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Текущее значение параметра
        /// </summary>
        public float CurrentValue
        {
            get
            {
                if (v_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return currentValue;
                    }
                    finally
                    {
                        v_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }
        }

        /// <summary>
        /// Чистое, не откалиброванное значение параметра
        /// </summary>
        public float ClearValue
        {
            get
            {
                if (v_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return clearValue;
                    }
                    finally
                    {
                        v_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }
        }

        /// <summary>
        /// Определяет корректно или нет значение параметра
        /// </summary>
        public bool IsValidValue
        {
            get
            {
                try
                {
                    float curve = CurrentValue;
                    if (!float.IsNaN(curve) && !float.IsInfinity(curve) &&
                            !float.IsNegativeInfinity(curve) && !float.IsPositiveInfinity(curve))
                    {
                        return true;
                    }
                }
                catch { }
                return false;
            }
        }

        /// <summary>
        /// Присвоить значение параметру
        /// </summary>
        /// <param name="_value">Присваиваемое значение параметру</param>
        public void setCurrent(float _value)
        {
            if (v_slim.TryEnterWriteLock(500))
            {
                try
                {
                    clearValue = _value;
                    if (transformation != null)
                    {
                        transformation.Arg = _value;
                        currentValue = transformation.Calculate();
                    }
                    else
                    {
                        currentValue = _value;
                    }

                }
                finally
                {
                    v_slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Сериализовать параметр параметр
        /// </summary>
        /// <param name="doc">Документ в который сохраняется параметр</param>
        /// <returns>Узел в котором сохранен параметр</returns>
        public XmlNode SerializeToXmlNode(XmlDocument doc)
        {
            try
            {
                XmlNode root = doc.CreateElement(parameterRoot);

                XmlNode nameNode = doc.CreateElement(parameterName);
                XmlNode descNode = doc.CreateElement(descriptionName);

                XmlNode unitsNode = doc.CreateElement(unitsName);
                XmlNode alarmNode = doc.CreateElement(alarmValueName);

                XmlNode blockingNode = doc.CreateElement(blockingValueName);
                XmlNode controlAlarmNode = doc.CreateElement(controlAlarmName);

                XmlNode controlBlockingNode = doc.CreateElement(controlBlockingName);
                XmlNode rangeNode = range.SerializeToXml(doc);

                XmlNode isSaveToDBNode = doc.CreateElement(saveToDBName);
                XmlNode intervalToSaveNode = doc.CreateElement(intervalToSaveName);

                XmlNode thresholdToBDNode = doc.CreateElement(thresholdToBDName);
                XmlNode channelNumberNode = doc.CreateElement(channelNumberName);

                XmlNode currentValueNode = doc.CreateElement(currentValueName);
                XmlNode selfIndexNode = doc.CreateElement(selfIndexName);

                XmlNode guidNode = doc.CreateElement(guidName);

                XmlNode devManindexNode = doc.CreateElement(devManindexName);
                XmlNode devManDescriptionNode = doc.CreateElement(devManDescriptionName);

                XmlNode transformationNode = null;
                if (transformation != null)
                {
                    transformationNode = transformation.CreateXmlNode(doc);
                }

                nameNode.InnerText = name;
                descNode.InnerText = description;

                unitsNode.InnerText = units;
                alarmNode.InnerText = alarmValue.ToString();

                blockingNode.InnerText = blockingValue.ToString();
                controlAlarmNode.InnerText = controlAlarm.ToString();

                controlBlockingNode.InnerText = controlBlocking.ToString();
                isSaveToDBNode.InnerText = saveToDB.ToString();

                intervalToSaveNode.InnerText = intervalToSave.ToString();
                thresholdToBDNode.InnerText = thresholdToBD.ToString();

                channelNumberNode.InnerText = channelNumber.ToString();
                currentValueNode.InnerText = currentValue.ToString();

                selfIndexNode.InnerText = selfIndex.ToString();
                guidNode.InnerText = guid.ToString();

                devManindexNode.InnerText = devManindex.ToString();
                devManDescriptionNode.InnerText = devManDescription;

                root.AppendChild(nameNode);
                root.AppendChild(descNode);

                root.AppendChild(unitsNode);
                root.AppendChild(alarmNode);

                root.AppendChild(blockingNode);
                root.AppendChild(controlAlarmNode);

                root.AppendChild(controlBlockingNode);
                if (rangeNode != null)
                {
                    root.AppendChild(rangeNode);
                }

                root.AppendChild(isSaveToDBNode);
                root.AppendChild(intervalToSaveNode);

                root.AppendChild(thresholdToBDNode);
                root.AppendChild(channelNumberNode);

                root.AppendChild(currentValueNode);
                root.AppendChild(selfIndexNode);

                root.AppendChild(devManindexNode);
                root.AppendChild(devManDescriptionNode);

                root.AppendChild(guidNode);
                if (transformationNode != null)
                {
                    root.AppendChild(transformationNode);
                }

                return root;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Десериализовать параметр
        /// </summary>
        /// <param name="node">Узел в котором сохранен параметр</param>
        public void DeserializeFromXmlNode(XmlNode node)
        {
            try
            {
                if (node != null && node.HasChildNodes)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case parameterRoot:

                                break;

                            case parameterName:

                                try
                                {
                                    name = child.InnerText;
                                }
                                catch { }
                                break;

                            case descriptionName:

                                try
                                {
                                    description = child.InnerText;
                                }
                                catch { }
                                break;

                            case unitsName:

                                try
                                {
                                    units = child.InnerText;
                                }
                                catch { }
                                break;

                            case alarmValueName:

                                try
                                {
                                    alarmValue = float.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case blockingValueName:

                                try
                                {
                                    blockingValue = float.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case controlAlarmName:

                                try
                                {
                                    controlAlarm = bool.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case controlBlockingName:

                                try
                                {
                                    controlBlocking = bool.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case rangeName:

                                try
                                {
                                    range.DeserializeFromXmlNode(child);
                                }
                                catch { }
                                break;

                            case saveToDBName:

                                try
                                {
                                    saveToDB = bool.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case intervalToSaveName:

                                try
                                {
                                    intervalToSave = int.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case thresholdToBDName:

                                try
                                {
                                    thresholdToBD = int.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case channelNumberName:

                                try
                                {
                                    channelNumber = int.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case currentValueName:

                                try
                                {
                                    currentValue = float.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case selfIndexName:

                                try
                                {
                                    int preSelf = int.Parse(child.InnerText);
                                    if (preSelf > -1)
                                    {
                                        selfIndex = preSelf;
                                    }
                                }
                                catch { }
                                break;

                            case guidName:

                                try
                                {
                                    guid = new Guid(child.InnerText);
                                }
                                catch { }
                                break;

                            case devManindexName:

                                try
                                {
                                    devManindex = int.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case devManDescriptionName:

                                try
                                {
                                    devManDescription = child.InnerText;
                                }
                                catch { }
                                break;

                            case "macros":

                                try
                                {
                                    transformation.InstanceMacrosFromXmlNode(child);
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
    }

    /// <summary>
    /// Реализует хранение диапазона допустимых значений для параметра
    /// </summary>
    public class ParameterRange
    {
        protected ReaderWriterLockSlim slim;     // синхронизатор

        protected float _min;                   // минимальное значение диапазона
        protected float _max;                   // максимальное значение диапазона

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public ParameterRange()
        {
            slim = new ReaderWriterLockSlim();

            _min = 0;
            _max = 65535;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="MinValue">Минимальное значение диапазона</param>
        /// <param name="MaxValue">Максимальное значение диапазона</param>
        public ParameterRange(float MinValue, float MaxValue)
        {
            slim = new ReaderWriterLockSlim();

            if (MinValue <= MaxValue)
            {
                _min = MinValue;
                _max = MaxValue;
            }
            else
            {
                _min = MaxValue;
                _max = MinValue;
            }
        }

        /// <summary>
        /// Определяет минимальное значение диапазона
        /// </summary>
        public float Min
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return _min;
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
                        if (value <= _max)
                        {
                            _min = value;
                        }
                        else
                        {
                            _min = _max;
                            _max = value;
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
        /// Определяет максимальное значение диапазона
        /// </summary>
        public float Max
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return _max;
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
                        if (value >= _min)
                        {
                            _max = value;
                        }
                        else
                        {
                            _max = _min;
                            _min = value;
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
        /// Сохранить текущее состояние объекта в XML узел
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public XmlNode SerializeToXml(XmlDocument doc)
        {
            try
            {
                if (doc != null)
                {
                    XmlNode root = doc.CreateElement("ParameterRange");

                    XmlNode minNode = doc.CreateElement("min");
                    XmlNode maxNode = doc.CreateElement("max");

                    minNode.InnerText = Min.ToString();
                    maxNode.InnerText = Max.ToString();

                    root.AppendChild(minNode);
                    root.AppendChild(maxNode);

                    return root;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Десериализовать параметр
        /// </summary>
        /// <param name="node">Узел в котором сохранен параметр</param>
        public void DeserializeFromXmlNode(XmlNode node)
        {
            try
            {
                if (node != null)
                {
                    if (node.Name == "ParameterRange")
                    {
                        if (node.HasChildNodes)
                        {
                            foreach (XmlNode child in node.ChildNodes)
                            {
                                switch (child.Name)
                                {
                                    case "min":

                                        try
                                        {
                                            _min = float.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "max":

                                        try
                                        {
                                            _max = float.Parse(child.InnerText);
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