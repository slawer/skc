using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SKC
{
    public partial class EditParameterPanelForm : Form
    {
        public EditParameterPanelForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Определяет описание параметра
        /// </summary>
        public string ParameterDescription
        {
            get
            {
                return textBoxParameterDescription.Text;
            }

            set
            {
                textBoxParameterDescription.Text = value;
            }
        }

        /// <summary>
        /// Определяет минимальное значение параметра
        /// </summary>
        public float ParameterMin
        {
            get
            {
                try
                {
                    return float.Parse(textBoxMininumValue.Text);
                }
                catch { }
                return float.NaN;
            }

            set
            {
                textBoxMininumValue.Text = value.ToString();
            }
        }

        /// <summary>
        /// Определяет максимальное значение параметра
        /// </summary>
        public float ParameterMax
        {
            get
            {
                try
                {
                    return float.Parse(textBoxMaximumValue.Text);
                }
                catch { }
                return float.NaN;
            }

            set
            {
                textBoxMaximumValue.Text = value.ToString();
            }
        }

        /// <summary>
        /// Определяет цвет которым отрисовывать параметр
        /// </summary>
        public Color ParameterColor
        {
            get { return buttonParameterColor.BackColor; }
            set { buttonParameterColor.BackColor = value; }
        }

        /// <summary>
        /// Определяем цвет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonParameterColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                buttonParameterColor.BackColor = colorDialog.Color;
            }
        }
    }
}