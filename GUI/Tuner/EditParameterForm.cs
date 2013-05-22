using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WCF;

namespace SKC
{
    public partial class EditParameterForm : Form
    {
        private Parameter edited = null;                // редактируемый параметр
        private PDescription channel = null;            // канал

        public EditParameterForm(Parameter parameter)
        {
            InitializeComponent();

            if (parameter != null)
            {
                edited = parameter;
                channel = edited.Channel;
            }
            else
            {
                MessageBox.Show("jnjnjnj");
                this.Close();
            }
        }

        private void EditParameterForm_Load(object sender, EventArgs e)
        {
            if (edited != null)
            {
                textBoxParameterName.Text = edited.Name;
                textBoxParameterDesc.Text = edited.Description;

                if (edited.Units == string.Empty)
                {
                    comboBoxParameterUnits.SelectedItem = "Единицы измерения не определены";
                }
                else
                {
                    comboBoxParameterUnits.Text = edited.Units;
                    comboBoxParameterUnits.SelectedItem = edited.Units;
                }

                textBoxMininumValue.Text = edited.Range.Min.ToString();
                textBoxMaximumValue.Text = edited.Range.Max.ToString();

                textBoxAlarmValue.Text = edited.Alarm.ToString();
                textBoxBlockingValue.Text = edited.Blocking.ToString();

                checkBoxControlAlarm.Checked = edited.IsControlAlarm;
                checkBoxControlBlocking.Checked = edited.IsControlBlocking;

                textBoxPorogToDB.Text = edited.ThresholdToBD.ToString();
                numericUpDownIntervalToSaveToDB.Value = edited.IntervalToSaveToDB;

                checkBoxIsSaveToDB.Checked = edited.SaveToDB;
                if (edited.Channel != null)
                {
                    textBoxParameterChannelName.Text = edited.Channel.Description;
                }
            }
        }

        /// <summary>
        /// Выбрать канал для параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectChannel_Click(object sender, EventArgs e)
        {
            DevManParametersForm frm = new DevManParametersForm(false);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                channel = frm.SelectedParameter;
                textBoxParameterChannelName.Text = channel.Description;
            }
        }

        /// <summary>
        /// Сохранить результат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            edited.Channel = channel;
            
            edited.Name = textBoxParameterName.Text;
            edited.Description = textBoxParameterDesc.Text;
            
            if (comboBoxParameterUnits.SelectedItem != null)
            {
                edited.Units = comboBoxParameterUnits.SelectedItem.ToString();
                if (edited.Units == "Единицы измерения не определены")
                {
                    edited.Units = string.Empty;
                }
            }

            edited.Range.Min = GetValue(textBoxMininumValue);
            edited.Range.Max = GetValue(textBoxMaximumValue);

            edited.Alarm = GetValue(textBoxAlarmValue);
            edited.Blocking = GetValue(textBoxBlockingValue);

            edited.IsControlAlarm = checkBoxControlAlarm.Checked;
            edited.IsControlBlocking = checkBoxControlBlocking.Checked;

            edited.ThresholdToBD = int.Parse(textBoxPorogToDB.Text);
            edited.IntervalToSaveToDB = (int)numericUpDownIntervalToSaveToDB.Value;

            edited.SaveToDB = checkBoxIsSaveToDB.Checked;
        }

        /// <summary>
        /// получить значение и TextBox
        /// </summary>
        /// <param name="box">TextBox из которого извлекать значение</param>
        /// <returns>Значение извлеченное из TextBox</returns>
        private float GetValue(TextBox box)
        {
            try
            {
                float koef;
                bool result = false;
                
                string number = box.Text;

                result = float.TryParse(number, out koef);
                if (result == false)
                {
                    number = number.Replace(".", ",");
                    result = float.TryParse(number, out koef);
                    if (!result)
                    {
                        number = box.Text.Replace(",", ".");
                        result = float.TryParse(number, out koef);

                        if (!result)
                        {
                            MessageBox.Show(this, "Введено не корректное число", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            DialogResult = System.Windows.Forms.DialogResult.None;
                            return float.NaN;
                        }

                        return koef;
                    }
                    else
                        return koef;
                }
                else
                    return koef;
            }
            catch
            {
                MessageBox.Show(this, "Введено не корректное число", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = System.Windows.Forms.DialogResult.None;
            }

            return float.NaN;
        }
    }
}