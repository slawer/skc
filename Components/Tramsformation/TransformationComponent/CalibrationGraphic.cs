using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace DeviceManager
{
    /// <summary>
    /// Реализует компонент, осуществляющий отрисовку график кусочно-линейной апроксимации
    /// </summary>
    public partial class CalibrationGraphic : UserControl
    {
        private BufferedGraphics graphicBuffer = null;          // графический буфер, для двойной буферизации
        private BufferedGraphicsContext graphicContext = null;  // методы сознания графичечких буферов

        private RectangleF Frame;                               // определяет область и положение, занимаемое графиком калибровки
        private RectangleF Axes;                                // определяет область и положение, осей X,Y графика калибровки

        private string textX = "Сигнал";                        // определяет надпись оси X
        private string textY = "Значение";                      // определяет надпись оси Y

        private float logicalPixelX = 65535;                    // логическое значение масштаба по X в пикселах
        private float logicalPixelY = 65535;                    // логическое значение масштаба по Y в пикселах

        private PointF point;                                    // точка значения калибровочного значения
        private PointF[] points;                                 // массив точек, определяющих график калибровочной кривой

        float ptsScaleX = 0.0f;                                 // соотношение по оси X
        float ptsScaleY = 0.0f;                                 // соотношение по оси Y

        private Matrix matrixForInnerLines = null;              // матрица для афинных преобразований при отрисовке 
                                                                // калибровочной кривой и калибровочного параметра

        private Draw whatDraw = Draw.Nothing;                   // что рисовать
        private Mutex mutex = null;                             // синхронизатор

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public CalibrationGraphic()
        {
            mutex = new Mutex();
            InitializeComponent();

            Rectangle FrameToDraw = new Rectangle(0, 0, Width, Height);

            Frame = new RectangleF(FrameToDraw.Location, FrameToDraw.Size);
            Axes = new RectangleF(Frame.X + 30.0f, Frame.Y + 30.0f, Frame.Width - 60.0f, Frame.Height - 60.0f);

            graphicContext = BufferedGraphicsManager.Current;

            Rectangle rect = new Rectangle((int)Frame.X, (int)Frame.Y, (int)Frame.Width, (int)Frame.Height);
            graphicBuffer = graphicContext.Allocate(CreateGraphics(), rect);

            graphicBuffer.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphicBuffer.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            graphicBuffer.Graphics.TextContrast = 1;
            graphicBuffer.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            points = null;
        }

        /// <summary>
        /// определяет что выводить на график
        /// </summary>
        public Draw Draw
        {
            get { return whatDraw; }
            set { whatDraw = value; }
        }

        /// <summary>
        /// Определяет логическое значение масштаба по X в пикселах
        /// </summary>
        public float LogicalPixelX
        {
            get { return logicalPixelX; }
            set { logicalPixelX = value; }
        }

        /// <summary>
        /// Определяет логическое значение масштаба по Y в пикселах
        /// </summary>
        public float LogicalPixelY
        {
            get { return logicalPixelY; }
            set { logicalPixelY = value; }
        }

        /// <summary>
        /// Пересчитать соотношения графика
        /// </summary>
        public void CalculateScale()
        {
            ptsScaleX = Axes.Width / logicalPixelX;
            ptsScaleY = Axes.Height / logicalPixelY;

            matrixForInnerLines = new Matrix(ptsScaleX, 0, 0, -ptsScaleY, Axes.Left, Axes.Bottom);
        }

        /// <summary>
        /// Отрисовывает линии калибровочной кривой
        /// </summary>
        private void DrawPoints()
        {
            if (points != null && points.Length > 1)
            {
                foreach (PointF pt in points)
                {
                    if (!CheckPoint(pt)) return;
                }

                using (Pen pen = new Pen(Color.Red))
                {
                    PointF[] pts = new PointF[points.Length];

                    points.CopyTo(pts, 0);

                    matrixForInnerLines.TransformPoints(pts);
                    graphicBuffer.Graphics.DrawLines(pen, pts);
                }

                for (int index = 1; index < points.Length - 1; index++)
                {
                    DrawPointsLines(points[index]);
                }
            }
        }

        /// <summary>
        /// украшаем прорисовку точек на линии
        /// </summary>
        /// <param name="pt">Массив точек для отрисовки</param>
        private void DrawPointsLines(PointF pt)
        {
            using (Pen pen = new Pen(Color.Silver))
            {
                if (pt.X != 0 && pt.Y != 0)
                {
                    PointF[] pts = new PointF[2];

                    pts[0] = new PointF(pt.X, 0);
                    pts[1]= new PointF(pt.X, logicalPixelY);

                    matrixForInnerLines.TransformPoints(pts);
                    graphicBuffer.Graphics.DrawLine(pen, pts[0], pts[1]);

                    pts[0].X = 0; pts[0].Y = pt.Y;
                    pts[1].X = logicalPixelX; pts[1].Y = pt.Y;

                    matrixForInnerLines.TransformPoints(pts);
                    graphicBuffer.Graphics.DrawLine(pen, pts[0], pts[1]);                   
                }
            }
        }

        /// <summary>
        /// Отрисовывает точку калибровочного параметра
        /// </summary>
        private void DrawPoint()
        {
            if (CheckPoint(point))
            {
                using (Pen pen = new Pen(Color.Green))
                {
                    if (point.X != 0 && point.Y != 0)
                    {
                        PointF[] pts = new PointF[2];
                        
                        pts[0] = new PointF(point.X, 0);
                        pts[1] = new PointF(point.X, logicalPixelY);

                        matrixForInnerLines.TransformPoints(pts);
                        graphicBuffer.Graphics.DrawLine(pen, pts[0], pts[1]);

                        pts[0].X = 0; pts[0].Y = point.Y;
                        pts[1].X = (int)logicalPixelX; pts[1].Y = pts[0].Y;

                        matrixForInnerLines.TransformPoints(pts);
                        graphicBuffer.Graphics.DrawLine(pen, pts[0], pts[1]);                        
                    }
                }
            }
        }

        /// <summary>
        /// Добавить калибруемую точку
        /// </summary>
        /// <param name="pt">Калибруемая точка</param>
        public void InsertPoint(PointF pt)
        {
            point.X = pt.X;
            point.Y = pt.Y;
        }

        /// <summary>
        /// Определить отрисовываемые точки
        /// </summary>
        /// <param name="pts"></param>
        public void InsertPoints(PointF[] pts)
        {
            points = new PointF[pts.Length];
            pts.CopyTo(points, 0);
        }

        /// <summary>
        /// Осуществляет вывод графики на поверхность
        /// </summary>
        public void Present()
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(100, false))
                {
                    blocked = true;

                    // Рисует рамку и оси
                    graphicBuffer.Graphics.Clear(Color.White);
                    using (Pen pen = new Pen(Brushes.Black))
                    {
                        graphicBuffer.Graphics.DrawRectangle(pen, Frame.X, Frame.Y, Frame.Width, Frame.Height);
                        using (Pen mPen = new Pen(Brushes.Gray))
                        {
                            using (Pen xyPen = new Pen(Brushes.Navy))
                            {
                                Point[] xyPoints = new Point[3];
                                Point[] points = new Point[3];

                                xyPoints[0] = new Point((int)Axes.Right, (int)Axes.Bottom);
                                xyPoints[1] = new Point((int)Axes.Left, (int)Axes.Bottom);
                                xyPoints[2] = new Point((int)Axes.Left, (int)Axes.Top);

                                points[0] = new Point((int)Axes.Left, (int)Axes.Top);
                                points[1] = new Point((int)Axes.Right, (int)Axes.Top);
                                points[2] = new Point((int)Axes.Right, (int)Axes.Bottom);

                                graphicBuffer.Graphics.DrawLines(mPen, points);
                                graphicBuffer.Graphics.DrawLines(xyPen, xyPoints);
                            }
                        }
                    }

                    // Осуществляет вывод текстовых меток на графике
                    Font fnt = new Font("Times New Roman", 12.0f);

                    SizeF signalStringSize = graphicBuffer.Graphics.MeasureString(textX, fnt);
                    SizeF parameterStringSize = graphicBuffer.Graphics.MeasureString(textY, fnt);

                    float signalX = (Axes.Width / 2.0f) - (signalStringSize.Width / 2.0f) + Axes.Left;
                    float signalY = Axes.Bottom;

                    StringFormat format = new StringFormat();
                    format.FormatFlags = StringFormatFlags.DirectionVertical;

                    Matrix transform = new Matrix(-1, 0, 0, -1, -1, -1);

                    float parameterX = -Axes.Left;
                    float parameterY = -(Axes.Bottom - (Axes.Height / 2.0f)) - (parameterStringSize.Width / 2.0f);

                    float zeroX = Axes.Left - (graphicBuffer.Graphics.MeasureString("0", fnt).Width);

                    graphicBuffer.Graphics.DrawString(textX, fnt, Brushes.Black, signalX, signalY);
                    graphicBuffer.Graphics.DrawString("0", fnt, Brushes.Gray, zeroX, Axes.Bottom);

                    float maxX = Axes.Right - (graphicBuffer.Graphics.MeasureString(logicalPixelX.ToString(), fnt).Width);
                    graphicBuffer.Graphics.DrawString(logicalPixelX.ToString(), fnt, Brushes.Gray, maxX, Axes.Bottom);

                    Matrix oldTransform = graphicBuffer.Graphics.Transform;

                    graphicBuffer.Graphics.Transform = transform;
                    graphicBuffer.Graphics.DrawString(textY, fnt, Brushes.Black, parameterX, parameterY, format);

                    float maxY = Axes.Top + (graphicBuffer.Graphics.MeasureString(logicalPixelY.ToString(), fnt).Width);
                    graphicBuffer.Graphics.DrawString(logicalPixelY.ToString(), fnt, Brushes.Gray, -Axes.Left, -maxY, format);

                    graphicBuffer.Graphics.Transform = oldTransform;
                    fnt.Dispose();

                    switch (whatDraw)
                    {
                        case Draw.PointOnly:

                            DrawPoint();
                            break;

                        case Draw.PointsOnly:

                            DrawPoints();
                            break;

                        case Draw.PointAndPoints:

                            DrawPoints();

                            DrawPoint();
                            break;

                        default: break;
                    }

                    graphicBuffer.Render();
                }
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Отрисовать график
        /// </summary>
        /// <param name="e">Данные события отрисовки</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(100, false))
                {
                    blocked = true;

                    base.OnPaint(e);
                    Present();
                }
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Проверить точку на корректность
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool CheckPoint(PointF point)
        {
            if (point != null)
            {
                if (!float.IsNaN(point.X) && !float.IsInfinity(point.X)
                    && !float.IsNegativeInfinity(point.X) && !float.IsPositiveInfinity(point.X))
                {
                    if (!float.IsNaN(point.Y) && !float.IsInfinity(point.Y)
                        && !float.IsNegativeInfinity(point.Y) && !float.IsPositiveInfinity(point.Y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}