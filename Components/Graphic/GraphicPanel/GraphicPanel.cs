using System;
using System.Drawing;

namespace GraphicComponent
{
    /// <summary>
    /// Реализует панель на которой отображаются графики
    /// </summary>
    partial class GraphicPanel : IElement
    {
        protected IElement parent = null;                   // родительская панель для текущей панели
        protected Color color = Color.AliceBlue;       // цвет которым отрисовывать панель

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="panel">Родительская панель для текущей панели</param>
        public GraphicPanel(IElement panel)
        {
            parent = panel;
            //color = Color.WhiteSmoke;
        }

        /// <summary>
        /// Возвращяет родительский элемент для текущего компонента
        /// </summary>
        public IElement Parent 
        {
            get
            {
                return parent;
            }
        }

        /// <summary>
        /// Возвращяет графический объект ассоциированный с областью отрисовки компонента
        /// </summary>
        public GraphicDrawter Drawter
        {
            get
            {
                if (parent != null)
                {
                    return parent.Drawter;
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
                if (parent != null)
                {
                    //return parent.ClientRectangle;
                    return new RectangleF(point.X - 1, point.Y - 1, size.Width + 2, size.Height + 2);
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
                if (parent != null)
                {
                    return parent.GridHeight;
                }

                return float.NaN;
            }

            set { }
        }

        /// <summary>
        /// Определяет интервал времени отображаемый в одной ячейке
        /// </summary>
        public TimeSpan IntervalInCell
        {
            get
            {
                if (parent != null)
                {
                    return parent.IntervalInCell;
                }

                return TimeSpan.Zero;
            }

            set { }
        }

        /// <summary>
        /// Определяет базовую величину ширины
        /// </summary>
        public float BaseWidth
        {
            get
            {
                if (parent != null)
                {
                    return parent.BaseWidth;
                }

                return float.NaN;
            }

            set { }
        }

        /// <summary>
        /// Определяет базовую величину высоты
        /// </summary>
        public float BaseHeight
        {
            get
            {
                if (parent != null)
                {
                    return parent.BaseHeight;
                }

                return float.NaN;
            }

            set { }
        }

        /// <summary>
        /// Определяет базовое значение коэффициента для вычисления ширины
        /// </summary>
        public float WidthCoef
        {
            get
            {
                if (parent != null)
                {
                    return parent.WidthCoef;
                }

                return float.NaN;
            }

            set { }
        }

        /// <summary>
        /// Определяет базовое значение коэффициента для вычисления высоты
        /// </summary>
        public float HeightCoef
        {
            get
            {
                if (parent != null)
                {
                    return parent.HeightCoef;
                }

                return float.NaN;
            }

            set { }
        }

        /// <summary>
        /// Определяет количество делений сетки
        /// </summary>
        public int GradCount
        {
            get
            {
                if (parent != null)
                {
                    return parent.GradCount;
                }

                return -1;
            }

            set { }
        }

        /// <summary>
        /// Возвращяет список графиков
        /// </summary>
        public Graphic[] Graphics
        {
            get
            {
                if (parent != null)
                {
                    return parent.Graphics;
                }

                return null;
            }
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
        /// Определяет размер надписи шкалы времени
        /// </summary>
        public SizeF TimeLabelSize
        {
            get
            {
                if (parent != null)
                {
                    return parent.TimeLabelSize;
                }

                return SizeF.Empty;
            }

            set { }
        }

        /// <summary>
        /// Определяет размер области вывода шкалы времени
        /// </summary>
        public SizeF TimeAreaSizeF
        {
            get
            {
                if (parent != null)
                {
                    return parent.TimeAreaSizeF;
                }

                return SizeF.Empty;
            }

            set { }
        }

        /// <summary>
        /// Размер области в которую выводятся линии шкал графиков
        /// </summary>
        public RectangleF ScaleLineSize
        {
            get
            {
                if (parent != null)
                {
                    return parent.ScaleLineSize;
                }

                return RectangleF.Empty;
            }

            set { }
        }

        /// <summary>
        /// Определяет каким образом отрисовывать график
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                if (parent != null)
                {
                    return parent.Orientation;
                }

                return GraphicComponent.Orientation.Default;
            }

            set { }
        }

        /// <summary>
        /// Определяет стартовое время отображаемое на панели
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                if (parent != null)
                {
                    return parent.StartTime;
                }

                return DateTime.MinValue;
            }

            set { }
        }

        /// <summary>
        /// Определяет конечное время отображаемое на панели
        /// </summary>
        public DateTime FinishTime
        {
            get
            {
                if (parent != null)
                {
                    return parent.FinishTime;
                }

                return DateTime.MaxValue;
            }

            set { }
        }

        /// <summary>
        /// Определяет фактический размер области вывода меток времени
        /// </summary>
        public SizeF ActualTimeSize
        {
            get
            {
                if (parent != null)
                {
                    return parent.ActualTimeSize;
                }

                return SizeF.Empty;
            }

            set { }
        }

        /// <summary>
        /// Делает недействительной всю поверхность элемента управления
        /// и вызывает его перерисовку.
        /// </summary>
        public void InvalidatePanel()
        {
            try
            {
                if (parent != null)
                {
                    Paint();
                }
            }
            catch { }
        }

        /// <summary>
        /// Делает недействительной всю поверхность элемента управления
        /// и вызывает его перерисовку.
        /// </summary>
        public void InvalidatePanelTimer(DateTime currentTime)
        {
            try
            {
                if (parent != null)
                {
                    PaintTimer(currentTime);
                }
            }
            catch { }
        }
    }
}