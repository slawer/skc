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
    public partial class InsertStageForm : Form
    {
        protected float koef = 1.0f;

        public InsertStageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// определяет текстовое описание этапа
        /// </summary>
        public string StageName
        {
            get
            {
                return textBoxStageName.Text;
            }

            set
            {
                textBoxStageName.Text = value;
            }
        }

        /// <summary>
        /// Определяет поправочный коэффициент для этапа
        /// </summary>
        public float StageKoef
        {
            get
            {
                return koef;
            }

            set
            {
                koef = value;
            }
        }

        /// <summary>
        /// Определяет плановый расход
        /// </summary>
        public string Plan_consumption
        {
            get { return textBoxPlanRashod.Text; }
            set { textBoxPlanRashod.Text = value; }
        }

        /// <summary>
        /// Определяет плановый объем
        /// </summary>
        public string Plan_volume
        {
            get { return textBoxPlanObem.Text; }
            set { textBoxPlanObem.Text = value; }
        }

        /// <summary>
        /// Определяет плановое давление
        /// </summary>
        public string Plan_pressure
        {
            get { return textBoxPlanDavlenie.Text; }
            set { textBoxPlanDavlenie.Text = value; }
        }

        /// <summary>
        /// Определяет плановая плотность
        /// </summary>
        public string Plan_density
        {
            get { return textBoxPlanPlotnost.Text; }
            set { textBoxPlanPlotnost.Text = value; }
        }

        /// <summary>
        /// Загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertStageForm_Load(object sender, EventArgs e)
        {
            try
            {
                textBoxKoef.Text = koef.ToString();
            }
            catch { }
        }

        /// <summary>
        /// проверям введенные данные на корректность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            float _tmp = Validate(textBoxPlanRashod);
            if (float.IsNaN(_tmp) == true)
            {
                MessageBox.Show(this, "Введен не коррекно плановый расход", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = DialogResult.None;

                textBoxPlanRashod.Focus();
                textBoxPlanRashod.SelectAll();

                return;
            }
            else
                textBoxPlanRashod.Text = _tmp.ToString();

            _tmp = Validate(textBoxPlanDavlenie);
            if (float.IsNaN(_tmp) == true)
            {
                MessageBox.Show(this, "Введено не коррекно плановое давление", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = DialogResult.None;

                textBoxPlanDavlenie.Focus();
                textBoxPlanDavlenie.SelectAll();

                return;
            }
            else
                textBoxPlanDavlenie.Text = _tmp.ToString();

            _tmp = Validate(textBoxPlanObem);
            if (float.IsNaN(_tmp) == true)
            {
                MessageBox.Show(this, "Введен не коррекно плановый объем", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = DialogResult.None;

                textBoxPlanObem.Focus();
                textBoxPlanObem.SelectAll();

                return;
            }
            else
                textBoxPlanObem.Text = _tmp.ToString();

            _tmp = Validate(textBoxPlanPlotnost);
            if (float.IsNaN(_tmp) == true)
            {
                MessageBox.Show(this, "Введена не коррекно плановая плотность", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = DialogResult.None;

                textBoxPlanPlotnost.Focus();
                textBoxPlanPlotnost.SelectAll();

                return;
            }
            else
                textBoxPlanPlotnost.Text = _tmp.ToString();

            _tmp = Validate(textBoxKoef);
            if (float.IsNaN(_tmp) == true)
            {
                MessageBox.Show(this, "Введен не коррекно поправочный коэффициент", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = DialogResult.None;

                textBoxKoef.Focus();
                textBoxKoef.SelectAll();

                return;
            }
            else
                koef = _tmp;
        }

        private float Validate(TextBox _textBox)
        {
            try
            {
                if (_textBox != null && _textBox.Text != string.Empty)
                {
                    float _koef;
                    bool result = false;

                    string number = _textBox.Text;

                    result = float.TryParse(number, out _koef);
                    if (result == false)
                    {
                        number = number.Replace(".", ",");
                        result = float.TryParse(number, out _koef);
                        if (!result)
                        {
                            number = textBoxKoef.Text.Replace(",", ".");
                            result = float.TryParse(number, out _koef);

                            if (!result)
                            {
                                return float.NaN;
                            }
                        }
                        else
                            return _koef;
                    }
                    else
                        return _koef;
                }
            }
            catch { }
            return float.NaN;
        }
    }
}