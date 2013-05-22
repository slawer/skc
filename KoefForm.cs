using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SKC
{
    public partial class KoefForm : Form
    {
        public float koef;
        public KoefForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// проверяем введенное чило
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = false;
                //float koef = float.Parse(textBoxKoef.Text);
                string number = textBoxKoef.Text;

                result = float.TryParse(number, out koef);
                if (result == false)
                {
                    number = number.Replace(".", ",");
                    result = float.TryParse(number, out koef);
                    if (!result)
                    {
                        number = textBoxKoef.Text.Replace(",", ".");
                        result = float.TryParse(number, out koef);

                        if (!result)
                        {
                            MessageBox.Show(this, "Введено не корректное число", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            DialogResult = System.Windows.Forms.DialogResult.None;
                            return;
                        }
                    }
                }                

                if (!(koef >= 0.5f && koef <= 3.0f))
                {
                    MessageBox.Show(this, "Введеное число должно быть в диапазоне от 0,5 до 3", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    DialogResult = System.Windows.Forms.DialogResult.None;
                }
            }
            catch 
            {
                MessageBox.Show(this, "Введено не корректное число", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }
    }
}