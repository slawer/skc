using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WCF;
using WCF.WCF_Client;

namespace SKC
{
    public partial class EditTechParameterForm1 : Form
    {
        private Application _app = null;        // контекст приложения

        public EditTechParameterForm1()
        {
            InitializeComponent();
            
            _app = Application.CreateInstance();
            if (_app == null)
            {
                MessageBox.Show("Не удалось получить доступ к параметрам приложения");
            }
        }

        /// <summary>
        /// определяет источник значений для расхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTechConsumption_Click(object sender, EventArgs e)
        {
            ParametersListForm frm = new ParametersListForm();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Parameter parameter = frm.SelectedParameter;
                if (parameter != null)
                {                    
                    textBoxTechConsumption.Text = parameter.Name;
                    _app.Commutator.Technology.Consumption.Index = parameter.SelfIndex;

                    _app.Graphic_consumption.Description = parameter.Description;                    
                }
            }
        }

        /// <summary>
        /// определяем объем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTechVolume_Click(object sender, EventArgs e)
        {
            ParametersListForm frm = new ParametersListForm();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Parameter parameter = frm.SelectedParameter;
                if (parameter != null)
                {
                    textBoxTechVolume.Text = parameter.Name;
                    _app.Commutator.Technology.Volume.Index = parameter.SelfIndex;

                    _app.Graphic_volume.Description = parameter.Description;
                }
            }
        }

        /// <summary>
        /// определяем плотность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTechDensity_Click(object sender, EventArgs e)
        {
            ParametersListForm frm = new ParametersListForm();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Parameter parameter = frm.SelectedParameter;
                if (parameter != null)
                {
                    textBoxTechDensity.Text = parameter.Name;
                    _app.Commutator.Technology.Density.Index = parameter.SelfIndex;

                    _app.Graphic_density.Description = parameter.Description;
                }
            }
        }

        /// <summary>
        /// Определяем давление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTechPressure_Click(object sender, EventArgs e)
        {
            ParametersListForm frm = new ParametersListForm();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Parameter parameter = frm.SelectedParameter;
                if (parameter != null)
                {
                    textBoxTechPressure.Text = parameter.Name;
                    _app.Commutator.Technology.Pressure.Index = parameter.SelfIndex;

                    _app.Graphic_pressure.Description = parameter.Description;
                }
            }
        }

        /// <summary>
        /// определяем температуру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTechTemperature_Click(object sender, EventArgs e)
        {
            ParametersListForm frm = new ParametersListForm();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Parameter parameter = frm.SelectedParameter;
                if (parameter != null)
                {
                    textBoxTechTemperature.Text = parameter.Name;
                    _app.Commutator.Technology.Temperature.Index = parameter.SelfIndex;

                    _app.Graphic_temperature.Description = parameter.Description;
                    _app.Graphic_temperature.Units = string.Format("[{0}]", parameter.Units);
                }
            }
        }

        /// <summary>
        /// закрываем форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeFrom_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTechParameterForm_Load(object sender, EventArgs e)
        {
            try
            {
                Parameter[] parameters = _app.Commutator.Parameters;
                if (parameters != null)
                {
                    if (_app.Commutator.Technology.Consumption.Index > -1 &&
                        _app.Commutator.Technology.Consumption.Index < parameters.Length)
                    {
                        textBoxTechConsumption.Text = parameters[_app.Commutator.Technology.Consumption.Index].Name;
                    }

                    if (_app.Commutator.Technology.Volume.Index > -1 &&
                        _app.Commutator.Technology.Volume.Index < parameters.Length)
                    {
                        textBoxTechVolume.Text = parameters[_app.Commutator.Technology.Volume.Index].Name;
                    }

                    if (_app.Commutator.Technology.Density.Index > -1 &&
                        _app.Commutator.Technology.Density.Index < parameters.Length)
                    {
                        textBoxTechDensity.Text = parameters[_app.Commutator.Technology.Density.Index].Name;
                    }

                    if (_app.Commutator.Technology.Pressure.Index > -1 &&
                        _app.Commutator.Technology.Pressure.Index < parameters.Length)
                    {
                        textBoxTechPressure.Text = parameters[_app.Commutator.Technology.Pressure.Index].Name;
                    }

                    if (_app.Commutator.Technology.Temperature.Index > -1 &&
                        _app.Commutator.Technology.Temperature.Index < parameters.Length)
                    {
                        textBoxTechTemperature.Text = parameters[_app.Commutator.Technology.Temperature.Index].Name;
                    }

                    int i = 0;
                    foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                    {
                        if (rgr.IsMain)
                        {
                            if (i < comboBox1.Items.Count)
                            {
                                comboBox1.SelectedIndex = i;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    checkBox1.Checked = _app.AutoStartConsumption;
                }
            }
            catch { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index > -1 && index < _app.Commutator.Technology.Rgrs.Count)
            {
                foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                {
                    rgr.IsMain = false;
                }
                
                _app.Commutator.Technology.Rgrs[index].IsMain = true;

                _app.Commutator.Technology.Consumption.Index = _app.Commutator.Technology.Rgrs[index].Consumption.Index;
                _app.Commutator.Technology.Consumption.IndexToSave = _app.Commutator.Technology.Rgrs[index].Consumption.IndexToSave;

                _app.Commutator.Technology.Volume.Index = _app.Commutator.Technology.Rgrs[index].Volume.Index;
                _app.Commutator.Technology.Volume.IndexToSave = _app.Commutator.Technology.Rgrs[index].Volume.IndexToSave;

                Parameter[] parameters = _app.Commutator.Parameters;
                if (parameters != null)
                {
                    if (_app.Commutator.Technology.Rgrs[index].Consumption.Index > -1 && 
                        _app.Commutator.Technology.Rgrs[index].Consumption.Index < parameters.Length)
                    {
                        _app.Graphic_consumption.Description = parameters[_app.Commutator.Technology.Rgrs[index].Consumption.Index].Description;
                    }

                    if (_app.Commutator.Technology.Rgrs[index].Volume.Index > -1 &&
                        _app.Commutator.Technology.Rgrs[index].Volume.Index < parameters.Length)
                    {
                        _app.Graphic_volume.Description = parameters[_app.Commutator.Technology.Rgrs[index].Volume.Index].Description;
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _app.AutoStartConsumption = checkBox1.Checked;
        }
    }
}