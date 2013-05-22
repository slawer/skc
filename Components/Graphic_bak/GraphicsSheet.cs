using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GraphicComponent;

namespace GraphicComponent
{
    public partial class GraphicsSheet : UserControl
    {
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
        }

        private void edToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 0, 10);
        }

        private void секундВКленткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 0, 30);
        }

        private void минутаВКлеткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 1, 0);
        }

        private void минутВКлеткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 10, 0);
        }

        private void минутВКлеткеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 15, 0);
        }

        private void минутВКлеткеToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(0, 30, 0);
        }

        private void часВКлеткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.IntervalInCell = new TimeSpan(1, 0, 0);
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
        }
    }
}