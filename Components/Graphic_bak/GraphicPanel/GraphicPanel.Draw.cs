using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicComponent
{
    partial class GraphicPanel
    {
        protected SizeF size;               // размер обрасти 
        protected PointF point;             // расположение области

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

                    drawter.Graphics.Clear(SystemColors.Control);      // очистить область вывода графиков
                                        
                    DrawAreaRectangle();

                    DrawAreaGrid();
                    DrawGraphics();

                    // -----------------------------

                    RestoreRegion(drawter);
                }
            }
        }

        /// <summary>
        /// Инициализировать панель шкал графиков
        /// </summary>
        public void InitializePanel()
        {
            if (parent != null)
            {
                point = new PointF(parent.ScaleLineSize.X, 
                    parent.BaseHeight * parent.HeightCoef + (parent.TimeLabelSize.Height / 2.0f));

                size = new SizeF(parent.ScaleLineSize.Width, 
                    parent.TimeAreaSizeF.Height - (parent.TimeLabelSize.Height / 2.0f));
            }
        }

        // ---- вспомогательные функции ----

        private Region based, self;

        private void ClearRegion()
        {

        }

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
                rect = new RectangleF(point.X - 2, point.Y - 1, size.Width + 6, size.Height + 3);

                self = new Region(rect);
                drawter.Graphics.Clip = self;
            }
            finally { }
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
        /// Отрисовать ограничивающую рамку
        /// </summary>
        protected void DrawAreaRectangle()
        {            
            using (Pen pen = new Pen(Color.RoyalBlue))
            {
                parent.Drawter.Graphics.DrawRectangle(pen, point.X - 1, point.Y - 1, size.Width + 2, size.Height + 2);
                parent.Drawter.Graphics.FillRectangle(new SolidBrush(color), new RectangleF(point, size));
            }
        }

        /// <summary>
        /// Отрисовать сетку области в которую выводятся графики
        /// </summary>
        protected void DrawAreaGrid()
        {
            PointF _pt = new PointF(point.X, point.Y);
            float step = (float)Math.Round(size.Width / (float)parent.GradCount);

            using (Pen pen = new Pen(Color.CornflowerBlue))
            {
                PointF pt1 = new PointF(point.X, point.Y);
                PointF pt2 = new PointF(point.X, (float)Math.Round(point.Y + size.Height));

                // ----------- отрисовать вертикальную шкалу -----------

                for (int i = 0; i < parent.GradCount; i++)
                {
                    if (i > 0 && i < parent.GradCount)
                    {
                        parent.Drawter.Graphics.DrawLine(pen, pt1, pt2);
                    }

                    pt1.X += step;
                    pt2.X += step;
                }

                // ----------- отрисовать горизонтальную шкалу -----------

                float countLinesInGrig = size.Height / parent.GridHeight;
                float koef = (float)Math.Round(size.Height / countLinesInGrig);

                pt1.X = (float)Math.Round(point.X);
                pt1.Y = (float)Math.Round(point.Y);

                pt2.X = (float)Math.Round(point.X + size.Width);
                pt2.Y = (float)Math.Round(point.Y);

                for (int i = 0; i <= (int)countLinesInGrig; i++)
                {
                    if (i > 0 && i < countLinesInGrig)
                    {
                        parent.Drawter.Graphics.DrawLine(pen, pt1, pt2);
                    }

                    pt1.Y += koef;
                    pt2.Y += koef;
                }
            }
        }

        /// <summary>
        /// Отрисовать графики
        /// </summary>
        private void DrawGraphics()
        {
            Graphic[] graphics = parent.Graphics;
            if (graphics != null)
            {
                foreach (Graphic graphic in graphics)
                {
                    PointF[] pts = graphic.Calculate(point, size, Parent as Panel);
                    if (pts != null)
                    {
                        using (Pen pen = new Pen(graphic.Color))
                        {
                            Parent.Drawter.Graphics.DrawLines(pen, pts);
                        }
                    }
                }
            }
        }
    }
}