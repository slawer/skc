using System;
using System.Threading;

namespace GraphicComponent
{
    public class GraphicManager
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

            mode = DrawMode.Passive;           

            if (GPanel != null)
            {
                panel = GPanel;
                panel.StartTime = DateTime.Now;

                panel.Sheet.Resize += new EventHandler(Sheet_Resize);

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
            if (panel != null)
            {
                panel.Redraw();
            }
        }

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

                            if (panel.FinishTime < DateTime.Now)
                            {
                                TimeSpan n = DateTime.Now - panel.FinishTime;

                                long lg = n.Ticks;
                                long lgIn = panel.IntervalInCell.Ticks;

                                while (lg > 0)
                                {
                                    panel.StartTime += panel.IntervalInCell;
                                    lg -= lgIn;
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
            finally
            {
                if (blocked) mutex.ReleaseMutex();
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
        /// Режим отисовки по умолчанию (Пассивный).
        /// </summary>
        Default
    }
}