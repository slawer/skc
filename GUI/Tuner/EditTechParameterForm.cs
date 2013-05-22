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
    public partial class EditTechParameterForm : Form
    {
        private Application _app = null;        // контекст приложения

        public EditTechParameterForm()
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
                }
            }
        }

        /// <summary>
        /// Определяем место куда сохранять расход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTechSaveConsumption_Click(object sender, EventArgs e)
        {
            DevManParametersForm frm = new DevManParametersForm(true);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                PDescription selected = frm.SelectedParameter;
                if (selected != null)
                {
                    textBoxTechSaveConsumption.Text = selected.Description;
                    _app.Commutator.Technology.Consumption.IndexToSave = selected.Number;
                }
            }
        }

        /// <summary>
        /// определяем место куда сохранять объем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTechSaveVolume_Click(object sender, EventArgs e)
        {
            DevManParametersForm frm = new DevManParametersForm(true);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                PDescription selected = frm.SelectedParameter;
                if (selected != null)
                {
                    textBoxTechSaveVolume.Text = selected.Description;
                    _app.Commutator.Technology.Volume.IndexToSave = selected.Number;
                }
            }
        }

        /// <summary>
        /// определяем место куда сохранять объем процесса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTechSaveProccessVolume_Click(object sender, EventArgs e)
        {
            DevManParametersForm frm = new DevManParametersForm(true);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                PDescription selected = frm.SelectedParameter;
                if (selected != null)
                {
                    textBoxTechSaveProccessVolume.Text = selected.Description;
                    _app.Commutator.Technology.ProccessVolume.IndexToSave = selected.Number;
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

                    PDescription[] p_desc = DevManClient.Parameters;
                    if (p_desc != null)
                    {
                        foreach (PDescription p_d in p_desc)
                        {
                            if (p_d.Number == _app.Commutator.Technology.Consumption.IndexToSave)
                            {
                                textBoxTechSaveConsumption.Text = p_d.Description;
                            }
                            else
                                if (p_d.Number == _app.Commutator.Technology.Volume.IndexToSave)
                                {
                                    textBoxTechSaveVolume.Text = p_d.Description;
                                }
                                else
                                    if (p_d.Number == _app.Commutator.Technology.ProccessVolume.IndexToSave)
                                    {
                                        textBoxTechSaveProccessVolume.Text = p_d.Description;
                                    }
                        }
                    }
                }
            }
            catch { }
        }
    }
}