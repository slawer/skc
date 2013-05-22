using System;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;

namespace DisplayComponent
{
    /// <summary>
    /// Реализует отрисовку графика
    /// </summary>
    public class GraphicDrawter
    {
        protected BufferedGraphics graphicBuffer = null;          // графический буфер, для двойной буферизации
        protected BufferedGraphicsContext graphicContext = null;  // методы сознания графичечких буферов

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="g">Повехность на которой необходимо выполнять рисование</param>
        /// <param name="FrameToDraw">Область и положение, занимаемое графиком калибровки на форме</param>
        public GraphicDrawter(Graphics g, Rectangle FrameToDraw)
        {
            graphicContext = BufferedGraphicsManager.Current;
            graphicBuffer = graphicContext.Allocate(g, FrameToDraw);

            //graphicBuffer.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //graphicBuffer.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        /// <summary>
        /// Поверхность на которой рисовать график
        /// </summary>
        public Graphics Graphics
        {
            get { return graphicBuffer.Graphics; }
        }

        /// <summary>
        /// Осуществляет вывод графики на поверхность
        /// </summary>
        public void Present()
        {
            try
            {
                graphicBuffer.Render();
            }
            catch { }
        }

        /// <summary>
        /// очистка буфера
        /// </summary>
        public void Clear(Color color)
        {
            graphicBuffer.Graphics.Clear(color);
        }
    }
}