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
    public partial class GraphicsTurnerForm : Form
    {
        Application _app = null;
        GraphicComponent.GraphicManager manager = null;

        public GraphicsTurnerForm(GraphicComponent.GraphicManager man)
        {
            InitializeComponent();

            _app = Application.CreateInstance();
            manager = man;
        }

        private void buttonConsumptionColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.Graphic_consumption.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void buttonVolumeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.Graphic_volume.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void buttonDensityColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.Graphic_density.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void buttonPressureColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.Graphic_pressure.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void buttonTemperatureColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.Graphic_temperature.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicsTurnerForm_Load(object sender, EventArgs e)
        {
            buttonConsumptionColor.BackColor = _app.Graphic_consumption.Color;
            lb_consumptionMin.Text = _app.Graphic_consumption.Range.Min.ToString();
            lb_consumptionMax.Text = _app.Graphic_consumption.Range.Max.ToString();
            
            buttonVolumeColor.BackColor = _app.Graphic_volume.Color;
            lb_volumeMin.Text = _app.Graphic_volume.Range.Min.ToString();
            lb_volumeMax.Text = _app.Graphic_volume.Range.Max.ToString();

            buttonDensityColor.BackColor = _app.Graphic_density.Color;
            lb_densityMin.Text = _app.Graphic_density.Range.Min.ToString();
            lb_densityMax.Text = _app.Graphic_density.Range.Max.ToString();

            buttonPressureColor.BackColor = _app.Graphic_pressure.Color;
            lb_pressureMin.Text = _app.Graphic_pressure.Range.Min.ToString();
            lb_pressureMax.Text = _app.Graphic_pressure.Range.Max.ToString();

            buttonTemperatureColor.BackColor = _app.Graphic_temperature.Color;
            lb_temperatureMin.Text = _app.Graphic_temperature.Range.Min.ToString();
            lb_temperatureMax.Text = _app.Graphic_temperature.Range.Max.ToString();

            if (manager.Orientation == GraphicComponent.Orientation.Horizontal)
            {
                comboBoxOrient.SelectedIndex = 0;
            }
            else
                comboBoxOrient.SelectedIndex = 1;

            numericUpDown2.Value = manager.UpdatePeriod;
            numericUpDownGridCount.Value = manager.GrinCount;

        }

        /// <summary>
        /// минимальное значение расхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_consumptionMin_TextChanged(object sender, EventArgs e)
        {
            float min = Validate(sender as TextBox);
            if (float.IsNaN(min) == false)
            {
                _app.Graphic_consumption.Range.Min = min;
            }
        }

        /// <summary>
        /// максимальное значение расхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_consumptionMax_TextChanged(object sender, EventArgs e)
        {
            float max = Validate(sender as TextBox);
            if (float.IsNaN(max) == false)
            {
                _app.Graphic_consumption.Range.Max = max;
            }
        }

        /// <summary>
        /// минимальное значение объема
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_volumeMin_TextChanged(object sender, EventArgs e)
        {
            float min = Validate(sender as TextBox);
            if (float.IsNaN(min) == false)
            {
                _app.Graphic_volume.Range.Min = min;
            }
        }

        /// <summary>
        /// максимальное значение объема
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_volumeMax_TextChanged(object sender, EventArgs e)
        {
            float max = Validate(sender as TextBox);
            if (float.IsNaN(max) == false)
            {
                _app.Graphic_volume.Range.Max = max;
            }
        }

        /// <summary>
        /// минимальное значение плотности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_densityMin_TextChanged(object sender, EventArgs e)
        {
            float min = Validate(sender as TextBox);
            if (float.IsNaN(min) == false)
            {
                _app.Graphic_density.Range.Min = min;
            }
        }

        /// <summary>
        /// максимальное значение плотности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_densityMax_TextChanged(object sender, EventArgs e)
        {
            float max = Validate(sender as TextBox);
            if (float.IsNaN(max) == false)
            {
                _app.Graphic_density.Range.Max = max;
            }
        }

        /// <summary>
        /// минимальное значение давления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_pressureMin_TextChanged(object sender, EventArgs e)
        {
            float min = Validate(sender as TextBox);
            if (float.IsNaN(min) == false)
            {
                _app.Graphic_pressure.Range.Min = min;
            }
        }

        /// <summary>
        /// максимальное значение давления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_pressureMax_TextChanged(object sender, EventArgs e)
        {
            float max = Validate(sender as TextBox);
            if (float.IsNaN(max) == false)
            {
                _app.Graphic_pressure.Range.Max = max;
            }
        }

        /// <summary>
        /// минимальное значение температуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_temperatureMin_TextChanged(object sender, EventArgs e)
        {
            float min = Validate(sender as TextBox);
            if (float.IsNaN(min) == false)
            {
                _app.Graphic_temperature.Range.Min = min;
            }
        }

        /// <summary>
        /// максимальное значение температуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_temperatureMax_TextChanged(object sender, EventArgs e)
        {
            float max = Validate(sender as TextBox);
            if (float.IsNaN(max) == false)
            {
                _app.Graphic_temperature.Range.Max = max;
            }
        }

        /// <summary>
        /// применить настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicsTurnerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _app.Graphic_consumption.Range.Min = float.Parse(lb_consumptionMin.Text);
                _app.Graphic_consumption.Range.Max = float.Parse(lb_consumptionMax.Text);
            }
            catch { }

            try
            {
                _app.Graphic_volume.Range.Min = float.Parse(lb_volumeMin.Text);
                _app.Graphic_volume.Range.Max = float.Parse(lb_volumeMax.Text);
            }
            catch { }

            try
            {
                _app.Graphic_density.Range.Min = float.Parse(lb_densityMin.Text);
                _app.Graphic_density.Range.Max = float.Parse(lb_densityMax.Text);
            }
            catch { }

            try
            {
                _app.Graphic_pressure.Range.Min = float.Parse(lb_pressureMin.Text);
                _app.Graphic_pressure.Range.Max = float.Parse(lb_pressureMax.Text);
            }
            catch { }

            try
            {
                _app.Graphic_temperature.Range.Min = float.Parse(lb_temperatureMin.Text);
                _app.Graphic_temperature.Range.Max = float.Parse(lb_temperatureMax.Text);
            }
            catch { }
        }

        private float Validate(TextBox textBoxKoef)
        {
            try
            {
                if (textBoxKoef != null && textBoxKoef.Text != string.Empty)
                {
                    float koef;
                    bool result = false;

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
                                return koef;
                            }
                        }
                        else
                            return koef;
                    }
                    else
                        return koef;
                }
            }
            catch
            {
                MessageBox.Show(this, "Введено не корректное число", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return float.NaN;
        }

        private void numericUpDownGridCount_ValueChanged(object sender, EventArgs e)
        {
            manager.GrinCount = (int)numericUpDownGridCount.Value;
            manager.Update();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            manager.UpdatePeriod = (int)numericUpDown2.Value;
        }

        private void comboBoxOrient_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxOrient.SelectedIndex)
            {
                case 0:

                    if (manager.Orientation != GraphicComponent.Orientation.Horizontal)
                    {
                        do
                        {
                            manager.Orientation = GraphicComponent.Orientation.Horizontal;
                        }
                        while (manager.Orientation != GraphicComponent.Orientation.Horizontal);
                    }
                    break;

                case 1:

                    if (manager.Orientation != GraphicComponent.Orientation.Vertical)
                    {
                        do
                        {
                            manager.Orientation = GraphicComponent.Orientation.Vertical;
                        }
                        while (manager.Orientation != GraphicComponent.Orientation.Vertical);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}