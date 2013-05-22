using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

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
        /// Отрисовать панель
        /// </summary>
        protected void PaintTimer(DateTime currentTime)
        {
            if (parent != null)
            {
                GraphicDrawter drawter = parent.Drawter;
                if (drawter != null)
                {
                    InitializeRegion(drawter);

                    // ---- выполняем отрисовку ----

                    //drawter.Graphics.Clear(SystemColors.Control);      // очистить область вывода графиков
                    //DrawAreaRectangle();

                    DrawAreaGridTimer(currentTime);
                    DrawGraphicsTimer(currentTime);                    

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
        /// Получить стартовое время отрисовываемой ячейки
        /// </summary>
        /// <param name="nowTime">Текущее время</param>
        /// <returns>Стартовое время</returns>
        protected int GetStartTime(Panel panel, DateTime nowTime)
        {
            try
            {
                int number = 0;
                DateTime startTime = panel.StartTime;
                do
                {
                    if (nowTime >= startTime)
                    {
                        DateTime finishTime = startTime + panel.IntervalInCell;
                        if (nowTime <= finishTime)
                        {
                            return number;
                        }
                    }

                    number = number + 1;
                    startTime = startTime + panel.IntervalInCell;
                }
                while (startTime < panel.FinishTime);
            }
            catch { }
            return -1;
        }

        /// <summary>
        /// Получить номер отрисовываемой клетки
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        protected int GetGridCell(DateTime currentTime)
        {
            try
            {
                Panel panel = parent as Panel;
                if (panel != null)
                {
                    int number = GetStartTime(panel, currentTime);
                    if (number > -1)
                    {
                        return number;
                    }
                }
            }
            catch { }
            return -1;
        }

        /// <summary>
        /// Отрисовать сетку области в которую выводятся графики
        /// </summary>
        protected void DrawAreaGridTimer(DateTime currentTime)
        {
            int number = GetStartTime(parent as Panel, currentTime);
            if (number > -1)
            {
                PointF _pt = new PointF(point.X, point.Y);
                float step = (float)Math.Round(size.Width / (float)parent.GradCount);

                using (Pen pen = new Pen(Color.CornflowerBlue))
                {
                    PointF pt1 = new PointF(point.X, (float)Math.Round(point.Y + parent.GridHeight * (float)number));
                    PointF pt2 = new PointF(point.X, (float)Math.Round(point.Y + parent.GridHeight * (float)(number + 1)));

                    float countLinesInGrig = size.Height / parent.GridHeight;
                    float koef = (float)Math.Round(size.Height / countLinesInGrig);

                    using (Pen _pen = new Pen(Color.RoyalBlue))
                    {
                        if (number == 0) parent.Drawter.Graphics.FillRectangle(new SolidBrush(color), pt1.X, pt1.Y + 1.8f, size.Width, koef - 2.4f);
                        else
                            parent.Drawter.Graphics.FillRectangle(new SolidBrush(color), pt1.X, pt1.Y + 1.8f, size.Width, koef - 2.4f);
                    }

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
                for (int index = 0; index < graphics.Length; index++)
                {
                    Graphic graphic = graphics[index];
                    if (graphic != null)
                    {
                        //if (graphic.Actived)
                        {
                            //PointF[] pts = graphic.Calculate(point, size, Parent as Panel);
                            PointF[] pts = graphic.CalculateReduce(point, size, Parent as Panel);

                            /*List<PointF[]> pts = graphic.CalculateWithParts(point, size, Parent as Panel);
                            if (pts != null && pts.Count > 0)
                            {
                                foreach (PointF[] _pts in pts)
                                {
                                    using (Pen pen = new Pen(graphic.Color))
                                    {
                                        if (_pts.Length > 1)
                                        {
                                            Parent.Drawter.Graphics.DrawLines(pen, _pts);
                                        }
                                    }
                                }
                            }*/

                            if (pts != null)
                            {
                                List<Intervals> Intrv = getIntervals(pts);
                                if (Intrv.Count > 0)
                                {
                                    foreach (Intervals i in Intrv)
                                    {
                                        if ((i.end - i.beg) > 0)
                                        {
                                            int cLng = i.end - i.beg + 1;
                                            PointF[] pts2 = new PointF[cLng];
                                            int cJ = i.beg;
                                            for (int j = 0; j < cLng; j++)
                                            {
                                                pts2[j] = pts[cJ++];
                                            }
                                            using (Pen pen = new Pen(graphic.Color, graphic.Width))
                                            {
                                                Parent.Drawter.Graphics.DrawLines(pen, pts2);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Отрисовать графики
        /// </summary>
        private void DrawGraphicsTimer(DateTime currentTime)
        {
            Graphic[] graphics = parent.Graphics;
            if (graphics != null)
            {
                for (int index = 0; index < graphics.Length; index++)
                {
                    Graphic graphic = graphics[index];
                    if (graphic != null)
                    {
                        PointF[] pts = graphic.CalculateReduceTimer(point, size, Parent as Panel, currentTime);
                        if (pts != null)
                        {
                            List<Intervals> Intrv = getIntervals(pts);
                            if (Intrv.Count > 0)
                            {
                                foreach (Intervals i in Intrv)
                                {
                                    if ((i.end - i.beg) > 0)
                                    {
                                        int cLng = i.end - i.beg + 1;
                                        PointF[] pts2 = new PointF[cLng];
                                        int cJ = i.beg;
                                        for (int j = 0; j < cLng; j++)
                                        {
                                            pts2[j] = pts[cJ++];
                                        }

                                        using (Pen pen = new Pen(graphic.Color, graphic.Width))
                                        {
                                            Parent.Drawter.Graphics.DrawLines(pen, pts2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// прлучить прямоугольную область в которой будет отрисовываться график
        /// </summary>
        /// <param name="pts"></param>
        /// <returns></returns>
        protected RectangleF getDrawRect(PointF[] pts)
        {
            try
            {
                float tx, ty, tx1, ty1;

                tx = float.MaxValue;
                ty = float.MaxValue;

                ty1 = float.MinValue;
                tx1 = float.MinValue;

                foreach (PointF pt in pts)
                {
                    if (pt.X < tx) tx = pt.X;
                    if (pt.Y < ty) ty = pt.Y;

                    if (pt.X > tx1) tx1 = pt.X;
                    if (pt.Y > ty1) ty1 = pt.Y;
                }

                if (tx < tx1)
                {
                    if (ty < ty1)
                    {
                        RectangleF rectF = new RectangleF(tx, ty, tx1 - tx, ty1 - ty);
                        return rectF;
                    }
                }
            }
            catch { }
            return RectangleF.Empty;
        }

        /// <summary>
        /// Разбивает график на интервалы непрерывных значений (анализ тайм-аутов по NaN)
        /// </summary>
        /// <param name="pts">Анализируемый массив</param>
        /// <returns>Массив интервалов, где [][1] - индекс первой точки интервала, [][2] - индекс последней</returns>
        private List<Intervals> getIntervals(PointF[] pts)
        {
            if (pts.Length == 0) return null;

            List<Intervals> tmpIntrv = new List<Intervals>();
            bool first = true;
            int ind = -1;

            for (int j = 0; j < pts.Length; j++)
            {
                if (float.IsNaN(pts[j].X))
                {
                    first = true;
                }
                else
                {
                    if (first)
                    {
                        Intervals Interval = new Intervals();
                        Interval.beg = j;
                        Interval.end = j;
                        tmpIntrv.Add(Interval);
                        ind++;
                        first = false;
                    }
                    else
                    {
                        tmpIntrv[ind].end = j;
                    }
                }
            }
            return tmpIntrv;
        }

        class Intervals
        {
            public int beg, end;
        }
    }
}