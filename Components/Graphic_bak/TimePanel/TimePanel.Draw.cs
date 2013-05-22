using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicComponent
{
    partial class TimePanel
    {
        protected DateTime start;           // начало интервала
        protected Font font;                // шрифт которым отрисовывать

        protected SizeF size;               // размер обрасти 
        protected PointF point;             // расположение области

        protected Color color;              // цвет панели

        /// <summary>
        /// определяет стартовое время интервала времени
        /// </summary>
        public DateTime CurrentTime
        {
            get { return start; }
            set { start = value; }
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
        /// Инициализировать панель времени
        /// </summary>
        public void InitializePanel()
        {
            if (parent != null)
            {
                point = new PointF(0.0f, parent.BaseHeight * parent.HeightCoef);

                float bw = parent.BaseWidth;
                float bw1 = parent.WidthCoef;
                RectangleF rt = parent.ClientRectangle;

                size = new SizeF(parent.BaseWidth * parent.WidthCoef, parent.ClientRectangle.Height - point.Y - 5);

                if (font != null)
                {
                    parent.TimeLabelSize = parent.Drawter.Graphics.MeasureString(DateTime.Now.ToLongTimeString(), font);
                }

                parent.TimeAreaSizeF = new SizeF(size);
            }
        }

        /// <summary>
        /// Отрисовать панель
        /// </summary>
        protected void Paint()
        {
            if (parent != null)
            {
                GraphicDrawter drawter = parent.Drawter;
                if (drawter != null)
                {
                    InitializeRegion(drawter);

                    // ---- выполняем отрисовку ----

                    drawter.Graphics.Clear(color);
                    DrawTimeValues(drawter);

                    // -----------------------------

                    RestoreRegion(drawter);
                }
            }
        }

        // ---- вспомогательные функции ----

        private Region based, self;

        /// <summary>
        /// Инициализировать регион отсечения для страницы
        /// </summary>
        /// <param name="drawter">Область на которой осуществляется отрисовка</param>
        protected void InitializeRegion(GraphicDrawter drawter)
        {
            RectangleF rect = RectangleF.Empty;
            try
            {
                based = drawter.Graphics.Clip;
                rect = new RectangleF(point, size);

                self = new Region(rect);
                drawter.Graphics.Clip = self;
            }
            catch { }
        }

        /// <summary>
        /// Восстановить оюласть осечения для страницы
        /// </summary>
        /// <param name="drawter">Область на которой осуществляется отрисовка</param>
        protected void RestoreRegion(GraphicDrawter drawter)
        {
            try
            {
                drawter.Graphics.Clip = based;

                self.Dispose();
                based.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// Отрисовать время
        /// </summary>
        /// <param name="drawter">Область на которой осуществляется отрисовка</param>
        protected void DrawTimeValues(GraphicDrawter drawter)
        {
            if (parent != null)
            {
                DateTime now = StartTime;
                SolidBrush brush = new SolidBrush(Color.Black);

                PointF pt = point; pt.X += 3;

                float countLinesInGrig = size.Height / parent.GridHeight;
                float koef = size.Height / countLinesInGrig;

                for (int i = 0; i <= (int)countLinesInGrig; i++)
                {
                    if (i == (int)countLinesInGrig)
                    {
                        drawter.Graphics.DrawString(now.ToLongTimeString(), font, brush, pt);
                    }
                    else
                    {
                        drawter.Graphics.DrawString(now.ToLongTimeString(), font, brush, pt);
                        now = now.Add(parent.IntervalInCell);

                        pt.Y += koef;
                    }
                }

                parent.FinishTime = now;               
            }
        }
    }
}