using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace DisplayComponent
{
    public partial class DigitDisplay : UserControl
    {
        private List<Digit> digits;                             // числа для отрисовки
        private GraphicDrawter drawter;                         // 

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DigitDisplay()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            digits = new List<Digit>();
            drawter = new GraphicDrawter(CreateGraphics(), ClientRectangle);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Render()
        {
            try
            {
                drawter.Clear(SystemColors.Control);

                if (digits != null)
                {
                    float offset = 4.0f;
                    float digInt = 2.0f;

                    float baseX = 4.0f;

                    float textWidth = Width * 0.6f;
                    float digWidth = (Width - textWidth) - 15.0f;

                    foreach (Digit digit in digits)
                    {
                        if (digit.Index > -1)
                        {
                            Font d_font = digit.Font;
                            Color d_color = digit.Color;

                            String format = digit.Format;

                            string text_str = digit.Description;
                            string dig_str = digit.FormatedValue;

                            SizeF textSize = drawter.Graphics.MeasureString(text_str, d_font);
                            SizeF digSize = drawter.Graphics.MeasureString(dig_str, d_font);

                            using (SolidBrush brush = new SolidBrush(d_color))
                            {
                                StringFormat str_format = new StringFormat();

                                str_format.Alignment = StringAlignment.Far;

                                drawter.Graphics.DrawString(text_str, d_font, brush, new RectangleF(baseX, offset, textWidth, textSize.Height));
                                drawter.Graphics.DrawString(dig_str, d_font, brush, new RectangleF(baseX + textWidth + 2.0f, offset, digWidth, digSize.Height), str_format);

                                using (Pen pen = new Pen(Color.DarkGray))
                                {
                                    pen.DashPattern = new float[] { 5.0F, 1.0F, 3.0F, 2.0F };
                                    drawter.Graphics.DrawLine(pen, baseX, textSize.Height + offset, baseX + textWidth + 2 + digWidth, textSize.Height + offset);
                                }
                            }

                            offset += textSize.Height + digInt;
                        }
                    }
                }

                drawter.Present();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Возвращяет список чисел для отображения
        /// </summary>
        public List<Digit> Digits
        {
            get { return digits; }
        }

        private void DigitDisplay_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }
    }
}