using System;
using System.Xml;
using System.Threading;

using WCF;
using WCF.WCF_Client;

namespace SKC
{
    /// <summary>
    /// Реализует этап работы
    /// </summary>
    public class TechStage
    {
        // ---- константы ----

        /// <summary>
        /// узел в котором сохраняется этап
        /// </summary>
        private const string rootName = "stage";

        /// <summary>
        /// узел в котором сохраняется номер этапа
        /// </summary>
        private const string numberName = "number";

        /// <summary>
        /// узел в котором сохраняется время начала этапа
        /// </summary>
        private const string data_beginName = "data_begin";

        /// <summary>
        /// узел в котором сохраняется время завершения этапа
        /// </summary>
        private const string data_endName = "data_end";

        /// <summary>
        /// узел в котором сохраняется название этапа
        /// </summary>
        private const string name_stgName = "name_stg";

        /// <summary>
        /// узел в котором сохраняется объем этапа
        /// </summary>
        private const string volume_stgName = "volume_stg";

        /// <summary>
        /// узел в котором сохраняется объем процесса
        /// </summary>
        private const string volume_allName = "volume_all";

        /// <summary>
        /// узел в котором значение объема, на момент нового этапа или записи РГР
        /// </summary>
        private const string const_volName = "const_vol";

        /// <summary>
        /// узел в котором суммарный объем завершенных этапов
        /// </summary>
        private const string const_sumName = "const_sum";

        // -------------------
        private int number;             // номер этапа

        private DateTime started;       // время начало этапа
        private DateTime finished;      // время завершения этапа

        private StageState state;       // текущее состояние этапа
        private StateRGR stateRgr;      // текущее состояние РГР

        private float constVolume;      // констОб

        private float ProccessVolume;   // объем процесса
        private float TotalVolume;      // суммарный объем

        private Tech _tech;             // технология
        private Mutex mutex = null;     // заблокировать калкулейт

        // ---- технологические параметры ----

        private float consumption;      // расход
        private string f_consumption;    // форматированное значение расхода

        private float volume;           // объем
        private string f_volume;        // форматированное значение объема

        private float density;          // плотность
        private string f_density;       // форматированное значение плотности

        private float pressure;         // давление
        private string f_pressure;      // форматированное значение давления

        private float temperature;      // температура
        private string f_temperature;   // форматированное значение температуры

        private string name_stg;        // имя этапа

        private ReaderWriterLockSlim slim_num;              // синхронизатор номера этапа
        private ReaderWriterLockSlim slim_time;             // синхронизатор времени этапа

        private ReaderWriterLockSlim slim_state;            // синхронизатор состояния этапа

        private ReaderWriterLockSlim slim_consumption;      // синхронизатор расхода
        private ReaderWriterLockSlim slim_volume;           // синхронизатор объема
        private ReaderWriterLockSlim slim_density;          // синхронизатор плотности
        private ReaderWriterLockSlim slim_pressure;         // синхронизатор давления
        private ReaderWriterLockSlim slim_temperature;      // синхронизатор температуры
        private ReaderWriterLockSlim slim_namestage;        // синхронизатор имени этапа

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public TechStage(Tech tech)
        {
            if (tech != null)
            {
                _tech = tech;
                mutex = new Mutex();

                name_stg = string.Empty;

                slim_num = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
                slim_time = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

                slim_state = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
                slim_consumption = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

                slim_volume = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
                slim_density = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

                slim_pressure = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
                slim_temperature = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

                slim_namestage = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

                state = StageState.Default;
                stateRgr = StateRGR.Unpressed;
            }
            else
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs("Не удалось получить экземпляр приложения этапу", ErrorType.Fatal));
            }
        }

        /// <summary>
        /// Определяет расход
        /// </summary>
        public float Consumption
        {
            get
            {
                if (slim_consumption.TryEnterReadLock(300))
                {
                    try
                    {
                        return consumption;
                    }
                    finally
                    {
                        slim_consumption.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim_consumption.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (!float.IsNaN(value) && !float.IsInfinity(value) &&
                                !float.IsNegativeInfinity(value) && !float.IsPositiveInfinity(value))
                        {
                            consumption = value;
                        }
                    }
                    finally
                    {
                        slim_consumption.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет отформатированное значение расхода
        /// </summary>
        public string FormattedConsumption
        {
            get
            {
                if (slim_consumption.TryEnterReadLock(300))
                {
                    try
                    {
                        return f_consumption;
                    }
                    finally
                    {
                        slim_consumption.ExitReadLock();
                    }
                }

                return "-----";
            }

            set
            {
                if (slim_consumption.TryEnterWriteLock(500))
                {
                    try
                    {
                        f_consumption = value;
                    }
                    finally
                    {
                        slim_consumption.ExitWriteLock();
                    }
                }
            }

        }

        /// <summary>
        /// Определяет объем
        /// </summary>
        public float Volume
        {
            get
            {
                if (slim_volume.TryEnterReadLock(300))
                {
                    try
                    {
                        return volume;
                    }
                    finally
                    {
                        slim_volume.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim_volume.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (!float.IsNaN(value) && !float.IsInfinity(value) &&
                                !float.IsNegativeInfinity(value) && !float.IsPositiveInfinity(value))
                        {
                            volume = value;
                        }
                    }
                    finally
                    {
                        slim_volume.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет форматированное значение объема
        /// </summary>
        public string FormattedVolume
        {
            get
            {
                if (slim_volume.TryEnterReadLock(300))
                {
                    try
                    {
                        return f_volume;
                    }
                    finally
                    {
                        slim_volume.ExitReadLock();
                    }
                }

                return "-----";
            }

            set
            {
                if (slim_volume.TryEnterWriteLock(500))
                {
                    try
                    {
                        f_volume = value;
                    }
                    finally
                    {
                        slim_volume.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет плотность
        /// </summary>
        public float Density
        {
            get
            {
                if (slim_density.TryEnterReadLock(300))
                {
                    try
                    {
                        return density;
                    }
                    finally
                    {
                        slim_density.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim_density.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (!float.IsNaN(value) && !float.IsInfinity(value) &&
                                !float.IsNegativeInfinity(value) && !float.IsPositiveInfinity(value))
                        {
                            density = value;
                        }
                    }
                    finally
                    {
                        slim_density.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет форматированное значение плотности
        /// </summary>
        public string FormattedDensity
        {
            get
            {
                if (slim_density.TryEnterReadLock(300))
                {
                    try
                    {
                        return f_density;
                    }
                    finally
                    {
                        slim_density.ExitReadLock();
                    }
                }

                return "-----";
            }

            set
            {
                if (slim_density.TryEnterWriteLock(500))
                {
                    try
                    {
                        f_density = value;
                    }
                    finally
                    {
                        slim_density.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет давление
        /// </summary>
        public float Pressure
        {
            get
            {
                if (slim_pressure.TryEnterReadLock(300))
                {
                    try
                    {
                        return pressure;
                    }
                    finally
                    {
                        slim_pressure.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim_pressure.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (!float.IsNaN(value) && !float.IsInfinity(value) &&
                                !float.IsNegativeInfinity(value) && !float.IsPositiveInfinity(value))
                        {
                            pressure = value;
                        }
                    }
                    finally
                    {
                        slim_pressure.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет форматированное значение давления
        /// </summary>
        public string FormattedPressure
        {
            get
            {
                if (slim_pressure.TryEnterReadLock(300))
                {
                    try
                    {
                        return f_pressure;
                    }
                    finally
                    {
                        slim_pressure.ExitReadLock();
                    }
                }

                return "-----";
            }

            set
            {
                if (slim_pressure.TryEnterWriteLock(500))
                {
                    try
                    {
                        f_pressure = value;
                    }
                    finally
                    {
                        slim_pressure.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет температуру
        /// </summary>
        public float Temperature
        {
            get
            {
                if (slim_temperature.TryEnterReadLock(300))
                {
                    try
                    {
                        return temperature;
                    }
                    finally
                    {
                        slim_temperature.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim_temperature.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (!float.IsNaN(value) && !float.IsInfinity(value) &&
                                !float.IsNegativeInfinity(value) && !float.IsPositiveInfinity(value))
                        {
                            temperature = value;
                        }
                    }
                    finally
                    {
                        slim_temperature.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет форматированное значение температуры
        /// </summary>
        public string FormattedTemperature
        {
            get
            {
                if (slim_temperature.TryEnterReadLock(300))
                {
                    try
                    {
                        return f_temperature;
                    }
                    finally
                    {
                        slim_temperature.ExitReadLock();
                    }
                }

                return "-----";
            }

            set
            {
                if (slim_temperature.TryEnterWriteLock(500))
                {
                    try
                    {
                        f_temperature = value;
                    }
                    finally
                    {
                        slim_temperature.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет константу объема
        /// </summary>
        public float ConstVolume
        {
            get
            {
                if (slim_consumption.TryEnterReadLock(300))
                {
                    try
                    {
                        return constVolume;
                    }
                    finally
                    {
                        slim_consumption.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim_consumption.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (!float.IsNaN(value) && !float.IsInfinity(value) &&
                                !float.IsNegativeInfinity(value) && !float.IsPositiveInfinity(value))
                        {
                            constVolume = value;
                        }
                    }
                    finally
                    {
                        slim_consumption.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет имя этапа
        /// </summary>
        public string NameStage
        {
            get
            {
                if (slim_namestage.TryEnterReadLock(300))
                {
                    try
                    {
                        return name_stg;
                    }
                    finally
                    {
                        slim_namestage.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim_namestage.TryEnterWriteLock(500))
                {
                    try
                    {
                        name_stg = value;
                    }
                    finally
                    {
                        slim_namestage.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет время когда начался этап
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                if (slim_time.TryEnterReadLock(300))
                {
                    try
                    {
                        return started;
                    }
                    finally
                    {
                        slim_time.ExitReadLock();
                    }
                }

                return DateTime.MinValue;
            }

            protected set
            {
                if (slim_time.TryEnterWriteLock(500))
                {
                    try
                    {
                        started = value;
                    }
                    finally
                    {
                        slim_time.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет время завершения этапа
        /// </summary>
        public DateTime FinishedTime
        {
            get
            {
                if (slim_time.TryEnterReadLock(300))
                {
                    try
                    {
                        return finished;
                    }
                    finally
                    {
                        slim_time.ExitReadLock();
                    }
                }

                return DateTime.MinValue;
            }

            protected set
            {
                if (slim_time.TryEnterWriteLock(500))
                {
                    try
                    {
                        finished = value;
                    }
                    finally
                    {
                        slim_time.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет текущее состояние этапа
        /// </summary>
        public StageState State
        {
            get
            {
                if (slim_state.TryEnterReadLock(300))
                {
                    try
                    {
                        return state;
                    }
                    finally
                    {
                        slim_state.ExitReadLock();
                    }
                }

                return StageState.Default;
            }

            set
            {
                if (slim_state.TryEnterWriteLock(500))
                {
                    try
                    {
                        state = value;
                    }
                    finally
                    {
                        slim_state.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет текущее сотояние РГР
        /// </summary>
        public StateRGR StateRGR
        {
            get
            {
                if (slim_state.TryEnterReadLock(300))
                {
                    try
                    {
                        return stateRgr;
                    }
                    finally
                    {
                        slim_state.ExitReadLock();
                    }
                }

                return StateRGR.Default;
            }

            internal set
            {
                if (slim_state.TryEnterWriteLock(500))
                {
                    try
                    {
                        stateRgr = value;
                    }
                    finally
                    {
                        slim_state.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет номер этапа
        /// </summary>
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        // ------------------------- методы класса -------------------------

        /// <summary>
        /// Начать этап
        /// </summary>
        public void StartStage()
        {
            if (State == StageState.Default)
            {
                StartTime = DateTime.Now;
                TotalVolume = _tech.Stages.TotalVolume;

                State = StageState.Started;
            }
        }

        /// <summary>
        /// Завершить этап
        /// </summary>
        public void FinishStage()
        {
            if (State == StageState.Started)
            {
                FinishedTime = DateTime.Now;
                State = StageState.Finished;

                ProccessVolume = _tech.Stages.ProccessVolume;
                TotalVolume = _tech.Stages.TotalVolume;

                _tech.Stages.TotalVolume += Volume;
            }
        }

        /// <summary>
        /// Запись РГР
        /// </summary>
        public void RecordRGR()
        {
            ConstVolume = _tech.Volume.Value;
            stateRgr = StateRGR.Pressed;
        }
        
        public void RecoredRGRFromLoad()
        {
            //constVolume = _app.Tech_volume;
            stateRgr = StateRGR.Pressed;
        }

        /// <summary>
        /// Заблокировать процесс вычисления параметров этапа
        /// </summary>
        /// <param name="time">Время в течении корого пытаться захватить блокировку</param>
        /// <returns>true - если удалось захватить блокировку</returns>
        public bool Block(int time)
        {
            try
            {
                return mutex.WaitOne(time);
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Разблокировать процесс вычисления параметров этапа
        /// </summary>
        public void Release()
        {
            try
            {
                mutex.ReleaseMutex();
            }
            catch { }
        }

        //private float lastVolume = float.NaN;

        /// <summary>
        /// Выполнить копирование данных с учетом технологии этапа.
        /// Вычислить параметры этапа.
        /// </summary>
        public void Calculate(RgrList rgrs)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(100))
                {
                    blocked = true;
                    switch (stateRgr)
                    {
                        case StateRGR.Pressed:

                            /*if (float.IsNaN(lastVolume) || float.IsInfinity(lastVolume) ||
                                float.IsPositiveInfinity(lastVolume) || float.IsNegativeInfinity(lastVolume))
                            {
                                lastVolume = _tech.Volume.Value;
                            }

                            float currentVolume = _tech.Volume.Value;
                            if (lastVolume > currentVolume)
                            {
                                constVolume = constVolume - (lastVolume - currentVolume);
                                lastVolume = currentVolume;

                                Application app = Application.CreateInstance();
                                if (app != null)
                                {
                                    app.SaveStages();
                                }
                            }

                            Consumption = _tech.Consumption.Value * _tech.Stages.CorrectionFactor;

                            float deltaVolume = (currentVolume - ConstVolume);
                            if (deltaVolume >= 0)
                            {
                                Volume = deltaVolume * _tech.Stages.CorrectionFactor + _tech.Stages.s;
                                lastVolume = currentVolume;
                            }*/

                            foreach (Rgr rgr in rgrs)
                            {
                                if (rgr.Calculate(StateRGR))
                                {
                                    Application app = Application.CreateInstance();
                                    if (app != null)
                                    {
                                        app.SaveStages();
                                    }
                                }

                                if (rgr.IsMain)
                                {
                                    Volume = rgr.CurrentVolume;
                                    Consumption = rgr.CurrentConsumption;
                                }
                            }

                            break;

                        case StateRGR.Unpressed:

                            foreach (Rgr rgr in rgrs)
                            {   
                                rgr.Calculate(StateRGR);
                            }

                            Consumption = 0.0f;
                            Volume = 0.0f;

                            break;

                        default:
                            break;
                    }

                    Density = _tech.Density.Value;
                    FormattedDensity = _tech.Density.FormattedValue;

                    Pressure = _tech.Pressure.Value;
                    FormattedPressure = _tech.Pressure.FormattedValue;

                    Temperature = _tech.Temperature.Value;
                    FormattedTemperature = _tech.Temperature.FormattedValue;

                    _tech.Stages.ProccessVolume = _tech.Stages.TotalVolume + Volume;
                }
            }
            catch { }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Сохранить этапы
        /// </summary>
        /// <param name="doc">Докумен в который выполнять сохранение этапов</param>
        /// <returns>Узел в котором сохранены этапы работы</returns>
        public XmlNode Serialize(XmlDocument doc)
        {
            try
            {
                XmlNode root = doc.CreateElement(rootName);

                XmlNode numberNode = doc.CreateElement(numberName);
                XmlNode data_beginNode = doc.CreateElement(data_beginName);

                XmlNode data_endNode = doc.CreateElement(data_endName);
                XmlNode name_stgNode = doc.CreateElement(name_stgName);

                XmlNode volume_stgNode = doc.CreateElement(volume_stgName);
                XmlNode volume_allNode = doc.CreateElement(volume_allName);

                XmlNode const_volNode = doc.CreateElement(const_volName);
                XmlNode const_sumNode = doc.CreateElement(const_sumName);

                numberNode.InnerText = number.ToString();
                data_beginNode.InnerText = StartTime.AddSeconds(3).ToString();

                if (State == StageState.Finished)
                {
                    data_endNode.InnerText = FinishedTime.ToString();
                }

                volume_stgNode.InnerText = Volume.ToString();
                volume_allNode.InnerText = ProccessVolume.ToString();

                const_volNode.InnerText = constVolume.ToString();
                const_sumNode.InnerText = TotalVolume.ToString();

                name_stgNode.InnerText = name_stg;

                root.AppendChild(numberNode);
                root.AppendChild(data_beginNode);

                root.AppendChild(data_endNode);
                root.AppendChild(name_stgNode);

                root.AppendChild(volume_stgNode);
                root.AppendChild(volume_allNode);

                root.AppendChild(const_volNode);
                root.AppendChild(const_sumNode);

                return root;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// загрузить данные этапа
        /// </summary>
        /// <param name="root">Узел в котором содержаться данные этапа</param>
        public void Deserialize(XmlNode root)
        {
            try
            {
                if (root != null)
                {
                    if (root.Name == rootName)
                    {
                        XmlNodeList childs = root.ChildNodes;
                        if (childs != null)
                        {
                            foreach (XmlNode child in childs)
                            {
                                switch (child.Name)
                                {
                                    case numberName:

                                        try
                                        {
                                            number = int.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case data_beginName:

                                        try
                                        {
                                            StartTime = DateTime.Parse(child.InnerText);
                                        }
                                        catch
                                        {
                                            StartTime = DateTime.MinValue;
                                        }
                                        break;

                                    case data_endName:

                                        try
                                        {
                                            FinishedTime = DateTime.Parse(child.InnerText);
                                        }
                                        catch
                                        {
                                            FinishedTime = DateTime.MaxValue;
                                        }
                                        break;

                                    case name_stgName:

                                        try
                                        {
                                            name_stg = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case volume_stgName:

                                        try
                                        {
                                            Volume = float.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case volume_allName:

                                        try
                                        {                                            
                                            ProccessVolume = float.Parse(child.InnerText);
                                            //_app.Stages.ProccessVolume = float.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case const_volName:

                                        try
                                        {
                                            constVolume = float.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case const_sumName:

                                        try
                                        {
                                            TotalVolume = float.Parse(child.InnerText);
                                            //_app.Stages.TotalVolume = float.Parse(child.InnerText);
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

    /// <summary>
    /// перечисляет возможные состояния этапа
    /// </summary>
    public enum StageState
    {
        /// <summary>
        /// Запущен
        /// </summary>
        Started,

        /// <summary>
        /// Закончен
        /// </summary>
        Finished,

        /// <summary>
        /// по умолчанию
        /// </summary>
        Default
    }

    /// <summary>
    /// перечисляет возможные состояния РГР
    /// </summary>
    public enum StateRGR
    {
        /// <summary>
        /// Расход РГР нажат
        /// </summary>
        Pressed,

        /// <summary>
        /// Расход РГР отжат
        /// </summary>
        Unpressed,

        /// <summary>
        /// По умолчанию
        /// </summary>
        Default
    }
}