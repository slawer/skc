using System;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;

namespace GraphicComponent
{
    /// <summary>
    /// Реализует панель шкал графиков
    /// </summary>
    partial class ScalePanel
    {
        protected SizeF size;                   // размер обрасти 
        protected PointF point;                 // расположение области

        protected Color color;                  // цвет панели
        protected float areaHeight;             // высота одного поля вывода графика на шкале

        protected DateTime _time;               // время для просотре
        protected bool mode = true;             // режим в котором работает компонент

        protected ReaderWriterLockSlim sync;    // синхронизатор

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
        /// Отображает режим прорисовки елемента
        /// </summary>
        public bool Mode
        {
            get
            {
                if (sync.TryEnterReadLock(100))
                {
                    try
                    {
                        return mode;
                    }
                    finally
                    {
                        sync.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (sync.TryEnterWriteLock(500))
                {
                    try
                    {
                        mode = value;
                    }
                    finally
                    {
                        sync.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Отображает режим прорисовки елемента
        /// </summary>
        public DateTime _Time
        {
            get
            {
                if (sync.TryEnterReadLock(100))
                {
                    try
                    {
                        return _time;
                    }
                    finally
                    {
                        sync.ExitReadLock();
                    }
                }

                return DateTime.MinValue;
            }

            set
            {
                if (sync.TryEnterWriteLock(500))
                {
                    try
                    {
                        _time = value;
                    }
                    finally
                    {
                        sync.ExitWriteLock();
                    }
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
                point = new PointF(0.0f, 0.0f);
                size = new SizeF(parent.ClientRectangle.Width * parent.WidthCoef, parent.BaseHeight * parent.HeightCoef);

                areaHeight = (size.Height - 5.0f) / 5.0f;    
           
                // ----------------------------------------------------

                PointF pt = new PointF((point.X + parent.BaseWidth * parent.WidthCoef) + 5.0f, point.Y);
                SizeF sz = new SizeF( (float)Math.Round((size.Width - pt.X) - 8.0f), size.Height);

                parent.ScaleLineSize = new RectangleF(pt, sz);
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
                    DrawGraphics();

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
            Matrix matrix = null;
            RectangleF rect = RectangleF.Empty;

            try
            {
                based = drawter.Graphics.Clip;
                rect = new RectangleF(point, size);

                self = new Region(rect);
                drawter.Graphics.Clip = self;
            }
            finally
            {
                if (matrix != null) matrix.Dispose();
            }
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
        /// Выполнить отрисовку графиков
        /// </summary>
        protected void DrawGraphics()
        {
            try
            {
                if (parent != null)
                {
                    Graphic[] graphics = parent.Graphics;
                    if (graphics != null)
                    {
                        int index = 1;
                        foreach (Graphic graphic in graphics)
                        {
                            DrawDescription(graphic, index);
                            DrawLine(graphic, index);

                            index = index + 1;
                        }
                    }
                    else
                    {
                        //TestDrawGraphics();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// отрисовать графики в тестовом режиме. то есть просто нарисовать ченить
        /// </summary>
        protected void TestDrawGraphics()
        {
            Color[] cols = { Color.Red, Color.Green, Color.Blue, Color.Brown, Color.Black };
            for (int i = 1; i < 6; i++)
            {
                if (parent != null)
                {
                    Graphic graphic = new Graphic();

                    graphic.Range.Min = 0;
                    graphic.Range.Max = 50;

                    graphic.Current = 3;

                    graphic.Color = cols[i - 1];

                    graphic.Units = "[кг/см]";
                    graphic.Description = "Объем" + i.ToString();

                    DrawDescription(graphic, i);
                    DrawLine(graphic, i);
                }                
            }
        }

        /// <summary>
        /// Отрисовать описание графика на шкале
        /// </summary>
        /// <param name="graphic">График описание которого вывести на шкалу</param>
        /// <param name="number">Порядковый номер графика на шкале</param>
        protected void DrawDescription(Graphic graphic, int number)
        {
            SizeF g_size = new SizeF(parent.BaseWidth * parent.WidthCoef, areaHeight);
            PointF g_point = new PointF(point.X, size.Height - areaHeight * number);

            StringFormat format = new StringFormat();

            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            RectangleF rect = new RectangleF(g_point, g_size);
            parent.Drawter.Graphics.DrawString(graphic.Description, graphic.Font, graphic.Brush, rect, format);

            /*//if (graphic.Actived)
            {
                //parent.Drawter.Graphics.DrawString(graphic.Description, graphic.Font, graphic.Brush, rect, format);
            }
            //else
            {
                using (SolidBrush brush = new SolidBrush(color))
                {
                    parent.Drawter.Graphics.DrawString(graphic.Description, graphic.Font, brush, rect, format);
                }
            }*/
        }

        /// <summary>
        /// Отрисовать линию
        /// <param name="graphic">График который выводится на шкале</param>
        /// <param name="number">Порядковый номер графика на шкале</param>
        /// </summary>
        private void DrawLine(Graphic graphic, int number)
        {
            using (Pen pen = new Pen(graphic.Color))
            {
                // ------------ получить область в которую выводить шкалу для графика ------------

                PointF g_point = new PointF(point.X, size.Height - areaHeight * number);
                SizeF g_size = new SizeF(parent.BaseWidth * parent.WidthCoef, areaHeight);

                PointF pt = new PointF(g_point.X + g_size.Width, g_point.Y);
                SizeF sz = new SizeF(size.Width - g_size.Width, areaHeight);

                RectangleF rect = new RectangleF(pt, sz);       // область в которую необходимо выводить шкалу графика

                //if (graphic.Actived)
                {
                    // ------------ отрисовать шкалу графика ------------

                    float Y = (float)Math.Round((double)(rect.Location.Y + (rect.Height / 1.6f)));  // Y координата
                    // линии шкалы графика
                    parent.Drawter.Graphics.DrawLine(pen, new PointF(rect.Location.X + 5, Y),
                        new PointF((rect.Location.X + rect.Width) - 10, Y));

                    // ------------ отрисовать единицы в которых измеряется параметр ------------

                    using (Font fnt = new Font(FontFamily.GenericSansSerif, 7.9f, FontStyle.Regular))
                    {
                        // определяем размер строки

                        int ost = parent.GradCount % 2;

                        PointF uPt = new PointF();
                        SizeF uSize = parent.Drawter.Graphics.MeasureString(graphic.Units, fnt);

                        uPt.Y = Y - uSize.Height - 3;

                        if (ost > 0)
                        {
                            uPt.X = (((rect.Width - 15) / 2.0f) - (uSize.Width / 2.0f)) + rect.X;
                        }
                        else
                        {
                            float del = parent.GradCount + 1;
                            uPt.X = ((((rect.Width - 15) / parent.GradCount) / 2) - ((uSize.Width / parent.GradCount) / 2)) + rect.X;
                        }

                        parent.Drawter.Graphics.DrawString(graphic.Units, fnt, graphic.Brush, uPt);

                        // ----------------------------------

                        DrawRange(pen, fnt, rect, Y, graphic);
                    }
                }
                /*else
                {
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        parent.Drawter.Graphics.FillRectangle(brush, rect);
                    }
                }*/
            }
        }

        /// <summary>
        /// Отрисовать диапазон шкалы для графика
        /// </summary>
        /// <param name="pen">Объек используемый для отрисовки линий</param>
        /// <param name="fnt">Шрифт которым выводить значения интервала</param>
        /// <param name="rect">Область в которой выводится шкала графика</param>
        /// <param name="Y">Y координата линии шкалы графика</param>
        /// <param name="graphic">График для которого отрисовывется шкала</param>
        protected void DrawRange(Pen pen, Font fnt, RectangleF rect, Single Y, Graphic graphic)
        {
            PointF _pt = new PointF(rect.Location.X + 5, Y + 2);
            float step = (float)Math.Round((((rect.Location.X + rect.Width) - 10) - (rect.Location.X + 5)) / parent.GradCount);

            for (int i = 0; i < parent.GradCount; i++)
            {
                if (i == 0)
                {
                    PointF pt_f = new PointF(_pt.X, _pt.Y - 4);
                    parent.Drawter.Graphics.DrawLine(pen, _pt, pt_f);

                    DrawRangeValues(pen, fnt, rect, Y, graphic, i);
                    _pt.X += step;
                }
                else
                {
                    _pt.Y = Y + 1;

                    PointF pt = new PointF(_pt.X, _pt.Y - 2);
                    parent.Drawter.Graphics.DrawLine(pen, _pt, pt);

                    DrawRangeValues(pen, fnt, rect, Y, graphic, i);
                    _pt.X += step;
                }
            }

            _pt.Y = Y + 2;
     
            PointF __pt = new PointF(rect.Location.X + rect.Width - 10, _pt.Y - 4);
            _pt.X = __pt.X;

            parent.Drawter.Graphics.DrawLine(pen, _pt, __pt);
            DrawRangeValues(pen, fnt, rect, Y, graphic, parent.GradCount);

            // ------------ отрисовать текущее значение графика ------------

            string pred = string.Empty;
            float curveValue = float.NaN;

            if (Mode)
            {
                curveValue = graphic.Current;
            }
            else
            {
                pred = _Time.ToLongTimeString().Trim();
                curveValue = graphic.Passive;
            }

            if (!float.IsNaN(curveValue))
            {
                float curveCalc = curveValue;
                if (curveCalc < graphic.Range.Min) curveCalc = graphic.Range.Min;
                else
                    if (curveCalc > graphic.Range.Max) curveCalc = graphic.Range.Max;

                float pixels = (((rect.Location.X + rect.Width) - 10) - (rect.Location.X + 5)) /
                    (graphic.Range.Max - graphic.Range.Min);

                //float curve = pixels * (graphic.Current - graphic.Range.Min) + (rect.Location.X + 5);
                float curve = pixels * (curveCalc - graphic.Range.Min) + (rect.Location.X + 5);

                PointF[] pts = new PointF[3];

                pts[0] = new PointF(curve, Y);
                pts[1] = new PointF(curve - 3, Y - 7);
                pts[2] = new PointF(curve + 3, Y - 9);

                parent.Drawter.Graphics.FillPolygon(graphic.Brush, pts);
                parent.Drawter.Graphics.DrawLine(pen, new PointF(pts[0].X, Y), new PointF(pts[0].X, Y - 11));

                if (Panel.DrawNumericInScale)
                {
                    using (SolidBrush curBrush = new SolidBrush(parent.Color))
                    {
                        using (Font sFnt = new Font(FontFamily.GenericSansSerif, 7.15f, FontStyle.Regular))
                        {
                            string curVal = string.Empty;
                            if (Mode)
                            {
                                curVal = string.Format("{0:F2}", curveValue).Trim();
                            }
                            else
                                curVal = string.Format("{0:F2} Время {1}", curveValue, pred).Trim();
                            
                            PointF ptCur = new PointF(pts[2].X, pts[2].Y - 10);
                            SizeF szText = parent.Drawter.Graphics.MeasureString(curVal, sFnt);

                            if ((rect.Location.X + rect.Width - 5) < (ptCur.X + szText.Width))
                            {
                                ptCur.X = ptCur.X - szText.Width - 8;
                            }

                            if (Mode == false)
                            {
                                using (Pen pin = new Pen(graphic.Color))
                                {
                                    parent.Drawter.Graphics.DrawRectangle(pin, ptCur.X, ptCur.Y, szText.Width + 1, szText.Height);
                                }
                            }

                            parent.Drawter.Graphics.FillRectangle(curBrush, new RectangleF(ptCur, szText));
                            parent.Drawter.Graphics.DrawString(curVal, sFnt, graphic.Brush, ptCur);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Отрисовать значения шкал для графика
        /// </summary>
        /// <param name="pen">Объект используемый для отрисоки линий</param>
        /// <param name="fnt">Шрифт которым выводить отрисовку значений</param>
        /// <param name="rect">Область в которую необходимо выводить значения диапазона</param>
        /// <param name="Y">Координата Y по которой вычислять координаты Y для вывода значений</param>
        /// <param name="graphic">График для которого осуществляется вывод значений<param>
        /// <param name="index">Номер выводимого значения из диапазона</param>
        protected void DrawRangeValues(Pen pen, Font fnt, RectangleF rect, Single Y, Graphic graphic, int index)
        {
            if (index == 0)     // отрисовываем первое значение из диапазона
            {
                string s_val = string.Format("{0:F1}", graphic.Range.Min);

                SizeF _size = parent.Drawter.Graphics.MeasureString(s_val, graphic.Font);
                PointF _pt = new PointF(rect.Location.X + 5, Y - _size.Height);

                parent.Drawter.Graphics.DrawString(s_val, fnt, graphic.Brush, _pt);
            }
            else
                if (index == parent.GradCount)      // отрисовываем последнее значение из диапазона
                {
                    string s_val = string.Format("{0:F1}", graphic.Range.Max);

                    SizeF _size = parent.Drawter.Graphics.MeasureString(s_val, graphic.Font);
                    PointF _pt = new PointF((rect.Location.X + rect.Width) - _size.Width - 5, Y - _size.Height);

                    parent.Drawter.Graphics.DrawString(s_val, fnt, graphic.Brush, _pt);
                }
                else
                {
                    // отрисовываем промежуточные значения из диапазона

                    float step = (graphic.Range.Max - graphic.Range.Min) / parent.GradCount;
                    float current = graphic.Range.Min + step * index;

                    float stepPt = (float)Math.Round((((rect.Location.X + rect.Width) - 10) - (rect.Location.X + 5)) 
                        / parent.GradCount);

                    string s_val = string.Format("{0:F1}", current);

                    SizeF _size = parent.Drawter.Graphics.MeasureString(s_val, graphic.Font);
                    PointF _pt = new PointF(((rect.Location.X + 5) + stepPt * index) - (float)Math.Round(_size.Width / 2.0f),
                        Y - _size.Height);

                    parent.Drawter.Graphics.DrawString(s_val, fnt, graphic.Brush, _pt);
                }
        }
    }
}