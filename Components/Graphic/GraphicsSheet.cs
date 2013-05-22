using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GraphicComponent;
using System.Threading;

namespace GraphicComponent
{
    public partial class GraphicsSheet : UserControl
    {
        /// <summary>
        /// Возникает когда изменили интервал времени в клетке
        /// </summary>
        public event EventHandler onIntervalInCellChange;

        /// <summary>
        /// Возникает когда изменили ориентацию отображения графиков
        /// </summary>
        public event EventHandler onOrientationChange;

        protected Panel panel = null;       // осуществляет отрисовку

        /// <summary>
        /// Одна секуда в тиках
        /// </summary>
        protected const long second = 10000000;

        /// <summary>
        /// Десять сенунд в тиках
        /// </summary>
        protected const long _10second = 100000000;

        /// <summary>
        /// Дридцать секунд в тиках
        /// </summary>
        protected const long _30second = 300000000;

        /// <summary>
        /// 1 минута в клетке
        /// </summary>
        protected const long _1minit = 600000000;

        /// <summary>
        /// 3 минута в клетке
        /// </summary>
        protected const long _3minit = 1800000000;

        /// <summary>
        /// 5 минута в клетке
        /// </summary>
        protected const long _5minit = 3000000000;

        /// <summary>
        /// 10 минут в клетке
        /// </summary>
        protected const long _10minits = 6000000000;

        /// <summary>
        /// 15 минут в клетке
        /// </summary>
        protected const long _15minits = 9000000000;

        /// <summary>
        /// 30 минут в клетке
        /// </summary>
        protected const long _30minits = 18000000000;

        /// <summary>
        /// 1 час в клетке
        /// </summary>
        protected const long _1hour = 36000000000;

        protected VScrollBar v_scroll = null;               // вертикальная полоса прокрутки
        protected HScrollBar h_scroll = null;               // горизонталья полоса прокрутки

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public GraphicsSheet()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            panel = new Panel(this);
            if (panel != null)
            {
                panel.InitializePanel();
            }
        }

        /// <summary>
        /// Определяет вертикальную полосу прокрутки
        /// </summary>
        public VScrollBar ScrollVertical
        {
            get { return v_scroll; }
            set
            {
                if (value == null)
                {
                    if (v_scroll != null)
                    {
                        Controls.Remove(v_scroll);
                    }

                    v_scroll = null;
                }
                else
                {
                    if (v_scroll != null)
                    {
                        Controls.Remove(v_scroll);
                        v_scroll = null;
                    }

                    v_scroll = value;
                    Controls.Add(v_scroll);
                }
            }
        }

        /// <summary>
        /// Определяет вертикальную полосу прокрутки
        /// </summary>
        public HScrollBar ScrollHorizontal
        {
            get { return h_scroll; }
            set
            {
                if (value == null)
                {
                    if (h_scroll != null)
                    {
                        Controls.Remove(h_scroll);
                    }

                    h_scroll = null;
                }
                else
                {
                    if (h_scroll != null)
                    {
                        Controls.Remove(h_scroll);
                        h_scroll = null;
                    }

                    h_scroll = value;
                    Controls.Add(h_scroll);
                }
            }
        }

        /// <summary>
        /// Получить управляющего отрисовкой компонента
        /// </summary>
        /// <returns></returns>
        public GraphicManager InstanceManager()
        {
            return new GraphicManager(panel);
        }

        /// <summary>
        /// Отображать 1 секунду в одной клетке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dedeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 0, 1);
            if (onIntervalInCellChange != null)
            {
                onIntervalInCellChange(this, EventArgs.Empty);
            }
        }

        private void edToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 0, 10);
            if (onIntervalInCellChange != null)
            {
                onIntervalInCellChange(this, EventArgs.Empty);
            }
        }

        private void секундВКленткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 0, 30);
            if (onIntervalInCellChange != null)
            {
                onIntervalInCellChange(this, EventArgs.Empty);
            }
        }

        private void минутаВКлеткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 1, 0);
            if (onIntervalInCellChange != null)
            {
                onIntervalInCellChange(this, EventArgs.Empty);
            }
        }

        private void минутВКлеткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 10, 0);
            if (onIntervalInCellChange != null)
            {
                onIntervalInCellChange(this, EventArgs.Empty);
            }
        }

        private void минутВКлеткеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 15, 0);
            if (onIntervalInCellChange != null)
            {
                onIntervalInCellChange(this, EventArgs.Empty);
            }
        }

        private void минутВКлеткеToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 30, 0);
            if (onIntervalInCellChange != null)
            {
                onIntervalInCellChange(this, EventArgs.Empty);
            }
        }

        private void часВКлеткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(1, 0, 0);
            if (onIntervalInCellChange != null)
            {
                onIntervalInCellChange(this, EventArgs.Empty);
            }
        }

        private void минутыВКлеткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 3, 0);
            if (onIntervalInCellChange != null)
            {
                onIntervalInCellChange(this, EventArgs.Empty);
            }
        }

        private void минутВКлеткеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 5, 0);
            if (onIntervalInCellChange != null)
            {
                onIntervalInCellChange(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// установить выбранный диапазон отображаемых данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuIntervalInCell_Opening(object sender, CancelEventArgs e)
        {
            menuItemSecond.Checked = false;
            menuItem30Second.Checked = false;

            menuItem30minits.Checked = false;
            menuItem1Minits.Checked = false;

            menuItem3Minits.Checked = false;
            menuItem5Minits.Checked = false;

            menuItem1hours.Checked = false;
            menuItem15minits.Checked = false;

            menuItem10Second.Checked = false;
            menuItem10minits.Checked = false;

            switch (panel.IntervalInCell.Ticks)
            {
                case second:

                    menuItemSecond.Checked = true;
                    break;

                case _10second:

                    menuItem10Second.Checked = true;
                    break;

                case _30second:

                    menuItem30Second.Checked = true;
                    break;

                case _1minit:

                    menuItem1Minits.Checked = true;
                    break;

                case _3minit:

                    menuItem3Minits.Checked = true;
                    break;

                case _5minit:

                    menuItem5Minits.Checked = true;
                    break;

                case _10minits:

                    menuItem10minits.Checked = true;
                    break;

                case _15minits:

                    menuItem15minits.Checked = true;
                    break;

                case _30minits:

                    menuItem30minits.Checked = true;
                    break;

                case _1hour:

                    menuItem1hours.Checked = true;
                    break;

                default:
                    break;
            }

            отрисовыватьЧислаНаШкалеToolStripMenuItem.Checked = Panel.DrawNumericInScale;
        }

        /// <summary>
        /// отображать графики вертикально
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void вертикальноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (panel.Orientation != Orientation.Vertical)
                {
                    do
                    {
                        panel.Orientation = Orientation.Vertical;
                    }
                    while (panel.Orientation != Orientation.Vertical);

                    if (onOrientationChange != null)
                    {
                        onOrientationChange(this, EventArgs.Empty);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// отображать графики горизонтально
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void горизонтальноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (panel.Orientation != Orientation.Horizontal)
                {
                    do
                    {
                        panel.Orientation = Orientation.Horizontal;
                    }
                    while (panel.Orientation != Orientation.Horizontal);

                    if (onOrientationChange != null)
                    {
                        onOrientationChange(this, EventArgs.Empty);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// открыли меню в котором указан способ отображения графиков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void отображатьГрафикиToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            вертикальноToolStripMenuItem.Checked = false;
            горизонтальноToolStripMenuItem.Checked = false;

            switch (panel.Orientation)
            {
                case Orientation.Vertical:

                    вертикальноToolStripMenuItem.Checked = true;
                    break;

                case Orientation.Horizontal:

                    горизонтальноToolStripMenuItem.Checked = true;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// открываем меню отображения количество делений сетки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ччвчвToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            toolStripComboBox1.Text = panel.GradCount.ToString();
        }

        /// <summary>
        /// выбрали количество сеток 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                panel.GradCount = int.Parse(toolStripComboBox1.Text);
                panel.Redraw();
            }
            catch { }
        }

        private void отрисовыватьЧислаНаШкалеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (panel.ScaleMode == false)
            {
            }
            else
            {
                if (отрисовыватьЧислаНаШкалеToolStripMenuItem.Checked)
                {
                    Panel.DrawNumericInScale = false;
                    отрисовыватьЧислаНаШкалеToolStripMenuItem.Checked = false;
                }
                else
                {
                    Panel.DrawNumericInScale = true;
                    отрисовыватьЧислаНаШкалеToolStripMenuItem.Checked = true;
                }
            }
        }

        protected struct info
        {
            public DateTime time;
            
            public string gr_1_name;
            public float gr_1_value;
            public Color gr_1_color;

            public string gr_2_name;
            public float gr_2_value;
            public Color gr_2_color;

            public string gr_3_name;
            public float gr_3_value;
            public Color gr_3_color;

            public string gr_4_name;
            public float gr_4_value;
            public Color gr_4_color;

            public string gr_5_name;
            public float gr_5_value;
            public Color gr_5_color;
        }

        private void GraphicsSheet_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (panel.ScaleMode == false)
                {
                    if (panel.RectangleGraphicsTransform.Contains(e.Location))
                    {
                        TimeSpan span = new TimeSpan(panel.FinishTime.Ticks - panel.StartTime.Ticks);

                        float a = float.NaN;
                        float b = float.NaN;

                        switch (panel.Orientation)
                        {
                            case Orientation.Horizontal:

                                a = Math.Abs(e.Location.X - panel.RectangleGraphicsTransform.X);
                                b = span.Ticks / panel.RectangleGraphicsTransform.Width;

                                break;

                            case Orientation.Vertical:

                                a = Math.Abs(e.Location.Y - panel.RectangleGraphicsTransform.Y);
                                b = span.Ticks / panel.RectangleGraphicsTransform.Height;

                                break;

                            default:
                                break;
                        }

                        float c = (a * b);
                        if (!float.IsNaN(c) && !float.IsInfinity(c) &&
                            !float.IsNegativeInfinity(c) && !float.IsPositiveInfinity(c))
                        {
                            long d = panel.StartTime.Ticks + (long)c;

                            DateTime ttime = new DateTime(d);
                            //if (panel.ScaleTime != ttime)
                            {
                                Graphic gr1 = panel.GetGraphic(0);
                                Graphic gr2 = panel.GetGraphic(1);
                                Graphic gr3 = panel.GetGraphic(2);
                                Graphic gr4 = panel.GetGraphic(3);
                                Graphic gr5 = panel.GetGraphic(4);

                                if (gr1 != null) gr1.Passive = gr1.GetValueFromTime(ttime);
                                if (gr2 != null) gr2.Passive = gr2.GetValueFromTime(ttime);
                                if (gr3 != null) gr3.Passive = gr3.GetValueFromTime(ttime);
                                if (gr4 != null) gr4.Passive = gr4.GetValueFromTime(ttime);
                                if (gr5 != null) gr5.Passive = gr5.GetValueFromTime(ttime);

                                panel.ScaleTime = ttime;
                                panel.RedrawScalePanel();
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private bool lastregim;
        private void режимПросмотраЗначенийПараметровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (режимПросмотраЗначенийПараметровToolStripMenuItem.Checked)
            {
                panel.ScaleMode = true;
                режимПросмотраЗначенийПараметровToolStripMenuItem.Checked = false;

                Panel.DrawNumericInScale = lastregim;
                отрисовыватьЧислаНаШкалеToolStripMenuItem.Checked = Panel.DrawNumericInScale;
            }
            else
            {   
                lastregim = Panel.DrawNumericInScale;

                panel.ScaleMode = false;
                Panel.DrawNumericInScale = true;

                отрисовыватьЧислаНаШкалеToolStripMenuItem.Checked = true;
                режимПросмотраЗначенийПараметровToolStripMenuItem.Checked = true;
            }
        }
    }
}