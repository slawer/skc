using System;
using System.Threading;

namespace GraphicComponent
{
    /// <summary>
    /// Реализует управление графическим компонентом
    /// </summary>
    public partial class GraphicManager
    {
        protected Panel panel;                  // панель через которую осуществляется управление графическим компонентом
        protected Timer timer;                  // таймер который инициирует отрисовку компонента при активном режиме отрисовки

        protected DrawMode mode;                // режим отрисовки графиков
        protected int timerPerion;              // частота отрисовки графиков в активном режиме        

        protected Mutex mutex;                  // синхронизирует работу таймера
        protected ReaderWriterLockSlim slim;    // синхронизатор

        /// <summary>
        /// Возникает когда необходимы данные для отрисовки
        /// </summary>
        public event EventHandler OnDataNeed;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="GPanel">Панель которую будет обслуживать манеджер</param>
        public GraphicManager(Panel GPanel)
        {
            mutex = new Mutex();
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            mode = DrawMode.Default;           

            if (GPanel != null)
            {
                panel = GPanel;
                //panel.StartTime = DateTime.Now;

                panel.OnResize += new EventHandler(Sheet_Resize);

                panel.Sheet.onOrientationChange += new EventHandler(Sheet_onOrientationChange);
                panel.Sheet.onIntervalInCellChange += new EventHandler(Sheet_onIntervalInCellChange);

                timerPerion = 500;
                timer = new Timer(TimerCallback, null, Timeout.Infinite, timerPerion);                
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Возникает когда графический компопнет изменил свой размер
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Sheet_Resize(object sender, EventArgs e)
        {
            try
            {
                switch (Mode)
                {
                    case DrawMode.Activ:

                        break;

                    case DrawMode.Passive:

                        panel.Redraw();
                        break;

                    case DrawMode.PassivScroll:

                        InitializeExistScroll();
                        panel.Redraw();

                        break;
                }
            }
            catch { }
        }

        /// <summary>
        /// Возникает когда изменили интервал времени в клетке
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        protected void Sheet_onIntervalInCellChange(object sender, EventArgs e)
        {
            try
            {
                if (Mode == DrawMode.Activ)
                {
                    if (OnData != null)
                    {
                        OnData(this, new GraphicEventArgs());
                    }
                    updTime();                    
                }
                else
                {
                    switch (panel.Orientation)
                    {
                        case GraphicComponent.Orientation.Vertical:

                            InitializeExistVScroll();
                            break;

                        case GraphicComponent.Orientation.Horizontal:

                            InitializeExistHScroll();
                            break;

                        default:
                            break;
                    }
                }

                panel.Redraw();
            }
            catch { }
        }

        /// <summary>
        /// Изменили ориентацию отображаемых графиков
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Sheet_onOrientationChange(object sender, EventArgs e)
        {
            try
            {
                switch (Mode)
                {
                    case DrawMode.Activ:

                        break;

                    case DrawMode.Passive:

                        panel.Redraw();
                        break;

                    case DrawMode.PassivScroll:

                        reInitializeScroll();
                        panel.Redraw();

                        break;

                    default:
                        break;
                }
            }
            catch { }
        }

        /// <summary>
        /// Определяет какое время учитывать для отображения
        /// </summary>
        public bool HardTime
        {
            get
            {
                return hardTime;
            }

            set
            {
                hardTime = value;
            }
        }

        /// <summary>
        /// Определяет время которое учитывать при отображении
        /// </summary>
        public DateTime HardStartTime
        {
            get { return hadrStart; }
            set { hadrStart = value; }
        }

        /// <summary>
        /// Определяет частоту отрисовки графиков в активном режиме
        /// </summary>
        public int UpdatePeriod
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return timerPerion;
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
                        if (value >= 50)
                        {
                            if (timerPerion != value)
                            {
                                switch (Mode)
                                {
                                    case DrawMode.Activ:

                                        timer.Change(Timeout.Infinite, timerPerion);

                                        timerPerion = value;
                                        timer.Change(0, timerPerion);

                                        break;

                                    case DrawMode.Passive:
                                        break;
                                    default:
                                        break;
                                }

                                timerPerion = value;
                            }
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
        /// Определяет режим отрисовки графиков
        /// </summary>
        public DrawMode Mode
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return mode;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return DrawMode.Default;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (value != mode)
                        {
                            mode = value;
                            switch (mode)
                            {
                                case DrawMode.Activ:

                                    timer.Change(0, UpdatePeriod);
                                    break;

                                case DrawMode.Passive:

                                    timer.Change(Timeout.Infinite, UpdatePeriod);
                                    break;

                                case DrawMode.PassivScroll:

                                    timer.Change(Timeout.Infinite, UpdatePeriod);
                                    InitializeForDoScrolling();

                                    break;

                                default:

                                    timer.Change(Timeout.Infinite, UpdatePeriod);
                                    break;
                            }
                        }                        
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        public TimeSpan IntervalInCell
        {
            get
            {
                if (panel != null)
                {
                    return panel.IntervalInCell;
                }
                else
                    return TimeSpan.Zero;
            }

            set
            {
                if (panel != null)
                {
                    panel.IntervalInCell = value;
                }
            }
        }

        /// <summary>
        /// Определяет ориентацию отрисовки графика
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                if (panel != null)
                {
                    return panel.Orientation;
                }
                else
                    return Orientation.Default;
            }

            set
            {
                if (panel != null)
                {
                    panel.Orientation = value;
                }
            }
        }

        /// <summary>
        /// Определяет стартовое время отображаемое на панели
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                if (panel != null)
                {
                    return panel.StartTime;
                }

                return DateTime.MinValue;
            }

            set
            {
                if (panel != null)
                {
                    panel.StartTime = value;
                    updTime();
                }
            }
        }

        /// <summary>
        /// Определяет конечное время отображаемое на панели
        /// </summary>
        public DateTime FinishTime
        {
            get
            {
                if (panel != null)
                {
                    return panel.FinishTime;
                }

                return DateTime.MaxValue;
            }

            set
            {
                if (panel != null)
                {
                    panel.FinishTime = value;
                }
            }
        }

        /// <summary>
        /// Определяет количество делений сетки
        /// </summary>
        public int GrinCount
        {
            get { return panel.GradCount; }
            set { panel.GradCount = value; }
        }

        /// <summary>
        /// Инициализировать новый график
        /// </summary>
        /// <returns>Если график создан и инициализирован то график, в противном случае null</returns>
        public Graphic InstanceGraphic()
        {
            try
            {
                if (panel != null)
                {
                    Graphic graphic = panel.InstanceGraphic();
                    if (graphic != null)
                    {                        
                        return graphic;
                    }
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Обновить компонент
        /// </summary>
        public void Update()
        {
            try
            {
                switch (Mode)
                {
                    case DrawMode.Activ:
                        break;

                    case DrawMode.Passive:

                        break;

                    case DrawMode.PassivScroll:
                        
                        reInitializeForDoScrolling();
                        break;
                }

                panel.Redraw();
            }
            catch { }
        }

        /// <summary>
        /// Сделать недействительной всю область отрисовки
        /// </summary>
        public void Invalidate()
        {
            try
            {
                panel.InvalidatePanel();
            }
            catch { }
        }

        protected DateTime lastStartTime = DateTime.MaxValue;

        /// <summary>
        /// Процедура таймера
        /// </summary>
        /// <param name="state">Не используется</param>
        protected void TimerCallback(object state)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(0))
                {
                    blocked = true;
                    switch (Mode)
                    {
                        case DrawMode.Activ:

                            DateTime now = DateTime.Now;
                            if (panel.FinishTime < now)
                            {
                                TimeSpan n = DateTime.Now - panel.FinishTime;

                                long lg = n.Ticks;
                                long lgIn = panel.IntervalInCell.Ticks;

                                if (lg > 0)
                                {
                                    while (lg > 0)
                                    {
                                        panel.StartTime += panel.IntervalInCell;
                                        lg -= lgIn;
                                    }
                                }
                            }
                            
                            if (OnDataNeed != null)
                            {
                                OnDataNeed(this, EventArgs.Empty);
                            }
                                    
                            panel.Redraw();
                            break;

                        case DrawMode.Passive:

                            break;

                        default:
                            break;
                    }

                }
            }
            catch { }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Установить время отображения графиков
        /// </summary>
        public void updTime()
        {
            Graphic[] graphics = panel.Graphics;
            if (graphics != null)
            {
                if (graphics.Length > 0)
                {
                    DateTime _minTime = DateTime.MaxValue;
                    DateTime _maxTime = DateTime.MinValue;

                    for (int i = 0; i < graphics.Length; i++)
                    {
                        DateTime minTimeGraphic = graphics[i].Tmin;
                        //DateTime minTimeGraphic = graphics[i].StartTimeOfGraphic;
                        DateTime maxTimeGraphic = graphics[i].FinishTimeOfGraphic;

                        if (minTimeGraphic != DateTime.MinValue && minTimeGraphic != DateTime.MaxValue)
                        {
                            if (minTimeGraphic < _minTime) _minTime = minTimeGraphic;
                            if (maxTimeGraphic > _maxTime) _maxTime = maxTimeGraphic;
                        }
                    }

                    if (_minTime != DateTime.MinValue && _minTime != DateTime.MaxValue)
                    {
                        if (_maxTime != DateTime.MinValue && _maxTime != DateTime.MaxValue)
                        {
                            //if (_maxTime < panel.FinishTime)
                            {
                                panel.StartTime = _minTime;                                
                            }
                        }                        
                    }
                }
            }
        }
    }

    /// <summary>
    /// перечисляет возможные режимы отрисовки графиков
    /// </summary>
    public enum DrawMode
    {
        /// <summary>
        /// активный режим отрисовки
        /// </summary>
        Activ,

        /// <summary>
        /// Пассивный режим отрисовки
        /// </summary>
        Passive,

        /// <summary>
        /// Пассивный режим отрисовки со скроллиногом
        /// </summary>
        PassivScroll,

        /// <summary>
        /// Режим отисовки по умолчанию (Пассивный).
        /// </summary>
        Default
    }
}