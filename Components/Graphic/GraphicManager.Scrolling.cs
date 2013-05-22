using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicComponent
{
    /// <summary>
    /// Реализует управление графическим компонентом
    /// </summary>
    public partial class GraphicManager
    {
        /// <summary>
        /// Возникает когда необходимо загрузить данные для отображения
        /// </summary>
        public event OnDataEventHander OnData;

        protected DateTime startTime;
        protected DateTime finishTime;

        protected bool hardTime = false;
        protected DateTime hadrStart = DateTime.MinValue;

        /// <summary>
        /// Подготовить компонент для работы в режиме прокрутки графиков.
        /// Очищаем все данные в буферах графика.
        /// Запрашиваем по новой все данные для отображения.
        /// Настраиваем стартовое время отображения графиков.
        /// </summary>
        protected void InitializeForDoScrolling()
        {
            try
            {
                Graphic[] graphics = panel.Graphics;
                if (graphics != null)
                {
                    for (int index = 0; index < graphics.Length; index++)
                    {
                        Graphic graphic = graphics[index];
                        if (graphic != null)
                        {
                            graphic.Clear();
                        }
                    }
                }

                if (OnData != null)
                {
                    OnData(this, new GraphicEventArgs());
                }

                InitializeScrollBar();
            }
            catch { }
        }

        /// <summary>
        /// Переподготовить компонент для работы в режиме прокрутки графиков.
        /// Настраиваем стартовое время отображения графиков.
        /// </summary>
        protected void reInitializeForDoScrolling()
        {
            try
            {
                InitializeScrollBar();                
            }
            catch { }
        }

        /// <summary>
        /// переинициализировать сколл, если изменили ориентацию графиков
        /// </summary>
        protected void reInitializeScroll()
        {
            panel.Sheet.ScrollVertical = null;
            panel.Sheet.ScrollHorizontal = null;

            reInitializeForDoScrolling();
        }

        /// <summary>
        /// Пересчитать коэффициенты для сколла
        /// </summary>
        protected void InitializeExistScroll()
        {
            switch (panel.Orientation)
            {
                case GraphicComponent.Orientation.Horizontal:

                    InitializeExistHScroll();
                    break;

                case GraphicComponent.Orientation.Vertical:

                    InitializeExistVScroll();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Пересчитать коэффициенты для скролла
        /// </summary>
        protected void InitializeExistVScroll()
        {
            try
            {
                VScrollBar scroll = panel.Sheet.ScrollVertical;
                //if (scroll != null)
                {
                    long graphicsInCells = (finishTime.Ticks - startTime.Ticks) / panel.IntervalInCell.Ticks;
                    long cellsInPanel = (panel.FinishTime.Ticks - panel.StartTime.Ticks) / panel.IntervalInCell.Ticks;

                    if (graphicsInCells > 0)
                    {
                        if (graphicsInCells > cellsInPanel)
                        {
                            if (scroll != null)
                            {
                                scroll.Maximum = (int)graphicsInCells;

                                scroll.SmallChange = (int)(cellsInPanel / 2);
                                scroll.LargeChange = (int)(cellsInPanel / 1);

                                panel.WidthCoef = (1.0f - (float)scroll.Width / panel.ClientRectangle.Width);
                                panel.InitializePanel();
                            }
                            else
                                InitializeScrollBar();
                        }
                        else
                        {
                            panel.WidthCoef = 1.0f;
                            if (startTime != DateTime.MinValue && startTime != DateTime.MaxValue)
                            {
                                panel.StartTime = startTime;
                            }

                            panel.InitializePanel();
                            panel.Sheet.ScrollVertical.Scroll -= VScrollBar_Scroll;

                            panel.Sheet.ScrollVertical = null;
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Пересчитать коэффициенты для скролла
        /// </summary>
        protected void InitializeExistHScroll()
        {
            try
            {
                HScrollBar scroll = panel.Sheet.ScrollHorizontal;
                //if (scroll != null)
                {
                    long graphicsInCells = (finishTime.Ticks - startTime.Ticks) / panel.IntervalInCell.Ticks;
                    long cellsInPanel = (panel.FinishTime.Ticks - panel.StartTime.Ticks) / panel.IntervalInCell.Ticks;

                    if (graphicsInCells > 0)
                    {
                        if (graphicsInCells > cellsInPanel)
                        {
                            if (scroll != null)
                            {
                                scroll.Maximum = (int)graphicsInCells;

                                scroll.SmallChange = (int)(cellsInPanel / 2);
                                scroll.LargeChange = (int)(cellsInPanel / 1);

                                panel.WidthCoef = (1.0f - (float)scroll.Height / panel.ClientRectangle.Width);
                                panel.InitializePanel();
                            }
                            else
                                InitializeScrollBar();
                        }
                        else
                        {
                            panel.WidthCoef = 1.0f;
                            if (startTime != DateTime.MinValue && startTime != DateTime.MaxValue)
                            {
                                panel.StartTime = startTime;
                            }

                            panel.InitializePanel();
                            panel.Sheet.ScrollHorizontal.Scroll -= HScrollBar_Scroll;

                            panel.Sheet.ScrollHorizontal = null;
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// настраивает скроллБар для графической панели
        /// </summary>
        protected void InitializeScrollBar()
        {
            switch (panel.Orientation)
            {
                case GraphicComponent.Orientation.Horizontal:

                    InstanceHScroll();
                    break;

                case GraphicComponent.Orientation.Vertical:

                    InstanceVScroll();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Установить вертикальную полосу прокрутки
        /// </summary>
        protected void InstanceVScroll()
        {
            DateTime _startTime = DateTime.MaxValue;
            DateTime _finishTime = DateTime.MinValue;

            Graphic[] graphics = panel.Graphics;
            if (graphics != null)
            {
                for (int i = 0; i < graphics.Length; i++)
                {
                    Graphic graphic = graphics[i];
                    if (graphic != null)
                    {
                        if (graphic.StartTimeOfGraphic < _startTime) _startTime = graphic.StartTimeOfGraphic;
                        if (graphic.FinishTimeOfGraphic > _finishTime) _finishTime = graphic.FinishTimeOfGraphic;
                    }
                }

                if (hardTime)
                {
                    startTime = hadrStart;
                    finishTime = _finishTime;
                }
                else
                {
                    startTime = _startTime;
                    finishTime = _finishTime;
                }
                                
                long graphicsInCells = (finishTime.Ticks - startTime.Ticks) / panel.IntervalInCell.Ticks;
                long cellsInPanel = (panel.FinishTime.Ticks - panel.StartTime.Ticks) / panel.IntervalInCell.Ticks;

                if (graphicsInCells > 0)
                {
                    if (graphicsInCells > cellsInPanel)
                    {
                        VScrollBar scroll = new VScrollBar();
                        scroll.Dock = DockStyle.Right;

                        scroll.Maximum = (int)graphicsInCells;

                        scroll.SmallChange = (int)(cellsInPanel / 2);
                        scroll.LargeChange = (int)(cellsInPanel / 1);

                        panel.Sheet.ScrollVertical = scroll;
                        scroll.Scroll += new ScrollEventHandler(VScrollBar_Scroll);

                        panel.WidthCoef = (1.0f - (float)scroll.Width / panel.ClientRectangle.Width);

                        panel.StartTime = _startTime;
                        panel.InitializePanel();
                    }
                    else
                    {
                        panel.WidthCoef = 1.0f;
                        if (startTime != DateTime.MinValue && startTime != DateTime.MaxValue)
                        {
                            panel.StartTime = startTime;
                        }

                        panel.InitializePanel();
                        panel.Sheet.ScrollVertical.Scroll -= VScrollBar_Scroll;

                        panel.Sheet.ScrollVertical = null;
                    }
                }
            }
        }

        /// <summary>
        /// Установить вертикальную полосу прокрутки
        /// </summary>
        protected void InstanceHScroll()
        {
            DateTime _startTime = DateTime.MaxValue;
            DateTime _finishTime = DateTime.MinValue;

            Graphic[] graphics = panel.Graphics;
            if (graphics != null)
            {
                for (int i = 0; i < graphics.Length; i++)
                {
                    Graphic graphic = graphics[i];
                    if (graphic != null)
                    {
                        if (graphic.StartTimeOfGraphic < _startTime) _startTime = graphic.StartTimeOfGraphic;
                        if (graphic.FinishTimeOfGraphic > _finishTime) _finishTime = graphic.FinishTimeOfGraphic;
                    }
                }

                if (hardTime)
                {
                    startTime = hadrStart;
                    finishTime = _finishTime;
                }
                else
                {
                    startTime = _startTime;
                    finishTime = _finishTime;
                }

                long graphicsInCells = (finishTime.Ticks - startTime.Ticks) / panel.IntervalInCell.Ticks;
                long cellsInPanel = (panel.FinishTime.Ticks - panel.StartTime.Ticks) / panel.IntervalInCell.Ticks;

                if (graphicsInCells > 0)
                {
                    if (graphicsInCells > cellsInPanel)
                    {
                        HScrollBar scroll = new HScrollBar();
                        scroll.Dock = DockStyle.Top;

                        scroll.Maximum = (int)graphicsInCells;

                        scroll.SmallChange = (int)(cellsInPanel / 2);
                        scroll.LargeChange = (int)(cellsInPanel / 1);

                        panel.Sheet.ScrollHorizontal = scroll;
                        scroll.Scroll += new ScrollEventHandler(HScrollBar_Scroll);

                        panel.WidthCoef = (1.0f - (float)scroll.Height / panel.ClientRectangle.Width);

                        panel.StartTime = startTime;
                        panel.InitializePanel();
                    }
                    else
                    {
                        panel.WidthCoef = 1.0f;
                        if (startTime != DateTime.MinValue && startTime != DateTime.MaxValue)
                        {
                            panel.StartTime = startTime;
                        }

                        panel.InitializePanel();
                        panel.Sheet.ScrollHorizontal.Scroll -= HScrollBar_Scroll;

                        panel.Sheet.ScrollHorizontal = null;
                    }
                }
            }
        }

        /// <summary>
        /// Прокрутили вертикальную полосу прокрутки
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        protected void VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                VScrollBar scroll = panel.Sheet.ScrollVertical;
                if (scroll != null)
                {
                    float panelTimeRange = (finishTime.Ticks - startTime.Ticks);
                    float ratioScroll = ((float)e.NewValue / (float)scroll.Maximum);

                    DateTime newTime = DateTime.FromBinary(startTime.Ticks + (long)(panelTimeRange * ratioScroll));

                    panel.StartTime = newTime;
                    panel.Redraw();
                }
            }
            catch { }
        }

        /// <summary>
        /// Прокрутили вертикальную полосу прокрутки
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        protected void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                HScrollBar scroll = panel.Sheet.ScrollHorizontal;
                if (scroll != null)
                {
                    float panelTimeRange = (finishTime.Ticks - startTime.Ticks);
                    float ratioScroll = ((float)e.NewValue / (float)scroll.Maximum);

                    DateTime newTime = DateTime.FromBinary(startTime.Ticks + (long)(panelTimeRange * ratioScroll));

                    panel.StartTime = newTime;
                    panel.Redraw();
                }
            }
            catch { }
        }
    }

    /// <summary>
    /// Представляет метод, который будет обрабатывать событие, запроса данных для отображения
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Объек, содержащий данные события.</param>
    public delegate void OnDataEventHander(Object sender, GraphicEventArgs e);

    /// <summary>
    /// Реализует передачу параметров при обработке событий от графической панели
    /// </summary>
    public class GraphicEventArgs : EventArgs
    {
        protected DateTime __start;                 // стартовое время в запросе данных 
        protected DateTime __finish;                // конечное время в запросе поиска

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public GraphicEventArgs()
            : base()
        {
            __start = DateTime.MinValue;
            __finish = DateTime.MaxValue;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="_startTime">Стартовое время</param>
        /// <param name="_finishTime">Финишное время</param>
        public GraphicEventArgs(DateTime _startTime, DateTime _finishTime)
            : base()
        {
            __start = _startTime;
            __finish = _finishTime;
        }

        /// <summary>
        /// Возвращяет стартовое время
        /// </summary>
        public DateTime StartTime
        {
            get { return __start; }
        }

        /// <summary>
        /// Возвращяет финишное время
        /// </summary>
        public DateTime FinishTime
        {
            get { return __finish; }
        }
    }
}