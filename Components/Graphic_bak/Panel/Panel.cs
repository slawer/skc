using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace GraphicComponent
{
    /// <summary>
    /// Реализует общую панель, на котороу осущесвляется вывод всей информации
    /// </summary>
    public class Panel : IElement
    {
        protected GraphicsSheet sheet = null;           // графический компонент на котором выполняется отрисовка
        protected GraphicDrawter drawter = null;        // осуществлят буферизацию области отрисовки

        // --------------- данные ---------------

        protected ReaderWriterLockSlim locker = null;   // синхронизатор
        protected TimeSpan interval = TimeSpan.Zero;    // интервал времени отображаемый в одной клетке

        protected SizeF timeSizeF;                      // размер области в которую выводится метка времени
        protected SizeF timeAreaF;                      // размер области в которую выводиться шкала времени

        protected RectangleF scaleSizeF;                // размер и положение области в которую выводится линия графика на шкале
        protected Color color = SystemColors.Control;   // цвет панели

        protected float gridHeight = 50.0f;             // высота в пикселах одной клетки сетки
        protected List<IElement> panels = null;         // список панелей для отрисовки

        protected DateTime startTime;                   // стартовое время отображаемое на пенили
        protected DateTime finishTime;                  // конечное время отображаемое на панели

        protected SizeF actualTimeSize;                 // реальная область отрисовки меток времени

        // --------------- базовые значения для всех панелей ---------------

        protected float base_width = 60.0f;             // базовое значение ширины
        protected float base_height = 120.0f;           // базовое значение высоты

        protected float base_width_koef = 1.0f;         // значение коэффициента для ширины
        protected float base_height_koef = 1.0f;        // значение коэффициента для высоты

        protected int grad_count = 5;                   // количество делений на сетке

        // --------------------------------------------------------------
                
        protected List<Graphic> graphics;               // графики для отрисовки
        protected ReaderWriterLockSlim g_locker;        // синхронизатор для графиков

        protected RectangleF bufRect;                   // тестовая переменная для проверки производительности
        protected Orientation orientation;              // ориентация графика

        // ------------------

        protected Mutex drawMutex;                      // синхронизирует отрисовку

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="_sheet"></param>
        public Panel(GraphicsSheet _sheet)
        {
            try
            {
                bufRect = RectangleF.Empty;

                drawMutex = new Mutex();
                locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

                graphics = new List<Graphic>();
                g_locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

                if (_sheet != null)
                {
                    sheet = _sheet;

                    sheet.Resize += new EventHandler(sheet_Resize);
                    sheet.Paint += new PaintEventHandler(sheet_Paint);

                    interval = new TimeSpan(0, 0, 10);          // интервал времени в одной клетке
                    drawter = new GraphicDrawter(sheet.CreateGraphics(), sheet.ClientRectangle);

                    panels = new List<IElement>();

                    panels.Add(CreateTimePanel());
                    panels.Add(CreateScalePanel());

                    panels.Add(CreateGraphicsPanel());
                    orientation = Orientation.Vertical;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Перерисовать панель
        /// </summary>
        public void Redraw()
        {
            bool blocked = false;
            try
            {
                if (drawMutex.WaitOne(100))
                {
                    blocked = true;
                    drawter.Clear(color);                 // очищаем область отрисовки

                    InvalidatePanel();                    // отрисовывем панели
                    drawter.Present();                    // выполняем отрисовку в окне графического элемента
                }
            }
            finally
            {
                if (blocked) drawMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Возвращяет графический компонент на котором осуществляется отрисовка
        /// </summary>
        public GraphicsSheet Sheet
        {
            get
            {
                return sheet;
            }
        }

        /// <summary>
        /// Необходимо перерисовать графическую панель
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void sheet_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            bool blocked = false;
            try
            {
                if (drawMutex.WaitOne(200))
                {
                    blocked = true;
                    drawter.Clear(color);                 // очищаем область отрисовки

                    InvalidatePanel();                    // отрисовывем панели
                    drawter.Present();                    // выполняем отрисовку в окне графического элемента
                }
            }
            finally
            {
                if (blocked) drawMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Область вывода изменила размер
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void sheet_Resize(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (drawMutex.WaitOne(300))
                {
                    blocked = true;
                    if (sheet != null)
                    {
                        if (sheet.Size.IsEmpty == false)
                        {
                            // общий графический объект на всю область вывода
                            drawter = new GraphicDrawter(sheet.CreateGraphics(), sheet.ClientRectangle);
                            if (Orientation == Orientation.Horizontal)
                            {
                                drawter.Graphics.RotateTransform(270.0f);
                                drawter.Graphics.TranslateTransform(-sheet.ClientRectangle.Height, 0);
                            }

                            InitializePanel();
                            sheet.Invalidate();
                        }
                    }
                }
            }
            finally
            {
                if (blocked) drawMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Возвращяет родительский элемент для текущего компонента
        /// </summary>
        public IElement Parent 
        {
            get { return null; }
        }

        /// <summary>
        /// Определяет цвет панели
        /// </summary>
        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        /// <summary>
        /// Возвращяет графический объект ассоциированный с областью отрисовки компонента
        /// </summary>
        public GraphicDrawter Drawter 
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return drawter;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Возвращяет размер области родительского элемента
        /// </summary>
        public RectangleF ClientRectangle
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        if (sheet != null)
                        {
                            if (Orientation == Orientation.Horizontal)
                            {
                                RectangleF rect = new RectangleF();
                                rect.Location = new PointF(sheet.ClientRectangle.Location.X, sheet.ClientRectangle.Location.Y);

                                rect.Width = sheet.ClientRectangle.Height;
                                rect.Height = sheet.ClientRectangle.Width;

                                return rect;
                            }

                            return sheet.ClientRectangle;
                        }
                    }
                    catch { }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return RectangleF.Empty;
            }
        }

        /// <summary>
        /// Определяет размер одной ячейки в пикселах
        /// </summary>
        public float GridHeight 
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return gridHeight;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return float.NaN;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        gridHeight = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет интервал времени отображаемый в одной ячейке
        /// </summary>
        public TimeSpan IntervalInCell 
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return interval;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return TimeSpan.Zero;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        interval = value;
                        Redraw();
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет базовую величину ширины на основе 
        /// которой вычисляется ширина панели вывода шкалы времени
        /// </summary>
        public float BaseWidth 
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return base_width;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return float.NaN;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        base_width = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет базовую величину высоты
        /// </summary>
        public float BaseHeight
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return base_height;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return float.NaN;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        base_height = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет базовое значение коэффициента для вычисления ширины
        /// </summary>
        public float WidthCoef 
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return base_width_koef;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return float.NaN;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        base_width_koef = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет базовое значение коэффициента для вычисления высоты
        /// </summary>
        public float HeightCoef
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return base_height_koef;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return float.NaN;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        base_height_koef = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет количество делений сетки
        /// </summary>
        public int GradCount
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return grad_count;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return -1;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (value > 0)
                        {
                            grad_count = value;
                        }
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет размер надписи шкалы времени
        /// </summary>
        public SizeF TimeLabelSize
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return timeSizeF;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return SizeF.Empty;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        timeSizeF = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет размер области вывода шкалы времени
        /// </summary>
        public SizeF TimeAreaSizeF
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return timeAreaF;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return SizeF.Empty;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        timeAreaF = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Размер области в которую выводятся линии шкал графиков
        /// </summary>
        public RectangleF ScaleLineSize
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return scaleSizeF;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return RectangleF.Empty;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        scaleSizeF = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }                    
                }
            }
        }

        /// <summary>
        /// Определяет каким образом отрисовывать график
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return orientation;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }                
                return GraphicComponent.Orientation.Default;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (orientation != value)
                        {
                            orientation = value;
                            switch (value)
                            {
                                case Orientation.Vertical:

                                    drawter.Graphics.ResetTransform();
                                    sheet_Resize(sheet, EventArgs.Empty);

                                    break;

                                case Orientation.Horizontal:

                                    sheet_Resize(sheet, EventArgs.Empty);
                                    break;

                                default:
                                    break;
                            }

                            InitializePanel();
                            sheet.Invalidate();
                        }
                    }
                    catch { }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
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
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return startTime;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return DateTime.MinValue;
            }
            
            set 
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        startTime = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
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
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return finishTime;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return DateTime.MaxValue;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        finishTime = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет фактический размер области вывода меток времени
        /// </summary>
        public SizeF ActualTimeSize
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return actualTimeSize;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return Size.Empty;
            }


            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        actualTimeSize = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }
        /*
        /// <summary>
        /// Определяет интервал времени отображаемый в одной клетке
        /// </summary>
        public TimeSpan IntervalInCell
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return interval;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
            }
        }
        */
        /// <summary>
        /// Возвращяет список графиков
        /// </summary>
        public Graphic[] Graphics 
        {
            get
            {
                if (g_locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return graphics.ToArray();
                    }
                    finally
                    {
                        g_locker.ExitReadLock();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Делает недействительной всю поверхность элемента управления
        /// и вызывает его перерисовку.
        /// </summary>
        public void InvalidatePanel()
        {
            if (panels != null)
            {
                foreach (IElement panel in panels)
                {
                    if (panel != null)
                    {
                        panel.InvalidatePanel();
                    }
                }
            }
        }

        /// <summary>
        /// Инициализировать панель
        /// </summary>
        public void InitializePanel()
        {
            if (panels != null)
            {
                foreach (IElement panel in panels)
                {
                    if (panel != null)
                    {
                        panel.InitializePanel();
                    }
                }
            }
        }

        // ----------- интерфейс для работы с графиками -----------

        /// <summary>
        /// Возвращяет количество графиков которые отрисовываются
        /// </summary>
        public int CountGraphics
        {
            get
            {
                if (g_locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return graphics.Count;
                    }
                    finally
                    {
                        g_locker.ExitReadLock();
                    }
                }
                return -1;
            }
        }

        /// <summary>
        /// Возвращяет максимально возможное количество графиком для компонента
        /// </summary>
        public int MaxGraphics
        {
            get { return 5; }
        }

        /// <summary>
        /// Инициализировать новый график
        /// </summary>
        /// <returns>Если график создан и инициализирован то график, в противном случае null</returns>
        public Graphic InstanceGraphic()
        {
            if (g_locker.TryEnterWriteLock(500))
            {
                try
                {
                    if (graphics.Count < MaxGraphics)
                    {
                        Graphic graphic = new Graphic();
                        graphics.Add(graphic);

                        return graphic;
                    }
                }
                finally
                {
                    g_locker.ExitWriteLock();
                }
            }
            return null;
        }

        /// <summary>
        /// Удалить график
        /// </summary>
        /// <param name="graphic">Удаляемый график</param>
        public void DeleteGraphic(Graphic graphic)
        {
            if (g_locker.TryEnterWriteLock(500))
            {
                try
                {
                    graphics.Remove(graphic);
                }
                finally
                {
                    g_locker.ExitWriteLock();
                }
            }
            else
            {
                throw new TimeoutException("Не удалось удалить график!");
            }
        }

        /// <summary>
        /// Получить график по его индексу
        /// </summary>
        /// <param name="At">Индекс запрашиваемого графика</param>
        /// <returns>График с запрашиваемым индексом или null если такого графика нету в списке</returns>
        public Graphic GetGraphic(int At)
        {
            if (g_locker.TryEnterReadLock(300))
            {
                try
                {
                    if (At < graphics.Count)
                    {
                        return graphics[At];
                    }
                }
                finally
                {
                    g_locker.ExitReadLock();
                }
            }
            return null;
        }

        // ---------- вспомогательные функции ----------

        /// <summary>
        /// Создать панель отображующую шкалу времени
        /// </summary>
        /// <returns>Панель отображающая шкалу времени</returns>
        private IElement CreateTimePanel()
        {
            try
            {
                TimePanel panel = new TimePanel(this);
                panel.InitializePanel();

                return panel;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Создать панель отображающую шкалы графиков
        /// </summary>
        /// <returns></returns>
        private IElement CreateScalePanel()
        {
            try
            {
                ScalePanel panel = new ScalePanel(this);
                panel.InitializePanel();

                return panel;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Создать панель отображающую графики
        /// </summary>
        /// <returns>Панель отображающая графики</returns>
        private IElement CreateGraphicsPanel()
        {
            try
            {
                GraphicPanel panel = new GraphicPanel(this);
                panel.InitializePanel();

                return panel;
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Перечисляет варианты перечисления отрисовки графика
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// Отрисовывать вертикально
        /// </summary>
        Vertical,

        /// <summary>
        /// Отрисовывать горизонтально
        /// </summary>
        Horizontal,

        /// <summary>
        /// Отрисовывать по умолчанию
        /// </summary>
        Default
    }
}