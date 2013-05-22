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
    public partial class RgrsForm : Form
    {
        Application _app = null;

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        public RgrsForm()
        {
            InitializeComponent();
            _app = Application.CreateInstance();
        }

        /// <summary>
        /// настраиваем расход первого расходомера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetConsumtion_Click(object sender, EventArgs e)
        {
            int rgr_ind = tabControl1.SelectedIndex;
            if (rgr_ind > -1 && rgr_ind < _app.Commutator.Technology.Rgrs.Count)
            {
                ParametersListForm frm = new ParametersListForm();
                if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    Parameter parameter = frm.SelectedParameter;
                    if (parameter != null)
                    {
                        SetConsumption(parameter.Name, rgr_ind);                        
                        _app.Commutator.Technology.Rgrs[rgr_ind].Consumption.Index = parameter.SelfIndex;
                    }
                }
            }
        }

        /// <summary>
        /// настраиваем объем первого расходомера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetVolume_Click(object sender, EventArgs e)
        {
            int rgr_ind = tabControl1.SelectedIndex;
            if (rgr_ind > -1 && rgr_ind < _app.Commutator.Technology.Rgrs.Count)
            {
                ParametersListForm frm = new ParametersListForm();
                if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    Parameter parameter = frm.SelectedParameter;
                    if (parameter != null)
                    {
                        SetVolume(parameter.Name, rgr_ind);
                        _app.Commutator.Technology.Rgrs[rgr_ind].Volume.Index = parameter.SelfIndex;
                    }
                }
            }
        }

        protected void SetVolume(string name, int index)
        {
            switch (index)
            {
                case 0:

                    textBox2.Text = name;
                    break;

                case 1:

                    textBox7.Text = name;
                    break;

                case 2:

                    textBox11.Text = name;
                    break;

                case 3:

                    textBox15.Text = name;
                    break;

                case 4:

                    textBox19.Text = name;
                    break;

                case 5:

                    textBox23.Text = name;
                    break;

                case 6:

                    textBox27.Text = name;
                    break;

                case 7:

                    textBox31.Text = name;
                    break;

                default:
                    break;
            }
        }

        protected void SetConsumption(string name, int index)
        {
            switch (index)
            {
                case 0:

                    textBox1.Text = name;
                    break;

                case 1:

                    textBox8.Text = name;
                    break;

                case 2:

                    textBox12.Text = name;
                    break;

                case 3:

                    textBox16.Text = name;
                    break;

                case 4:

                    textBox20.Text = name;
                    break;

                case 5:

                    textBox24.Text = name;
                    break;

                case 6:

                    textBox28.Text = name;
                    break;

                case 7:

                    textBox32.Text = name;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// сохранение расхода первого расходомера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveConsumption_Click(object sender, EventArgs e)
        {
            int rgr_ind = tabControl1.SelectedIndex;
            if (rgr_ind > -1 && rgr_ind < _app.Commutator.Technology.Rgrs.Count)
            {
                DevManParametersForm frm = new DevManParametersForm(true);
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    PDescription selected = frm.SelectedParameter;
                    if (selected != null)
                    {   
                        SetSaveConsumption(selected.Description, rgr_ind);
                        _app.Commutator.Technology.Rgrs[rgr_ind].Consumption.IndexToSave = selected.Number;
                    }
                }
            }
        }

        /// <summary>
        /// сохранение объема первого расходомера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveVolume_Click(object sender, EventArgs e)
        {
            int rgr_ind = tabControl1.SelectedIndex;
            if (rgr_ind > -1 && rgr_ind < _app.Commutator.Technology.Rgrs.Count)
            {
                DevManParametersForm frm = new DevManParametersForm(true);
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    PDescription selected = frm.SelectedParameter;
                    if (selected != null)
                    {
                        SetSaveVolume(selected.Description, rgr_ind);
                        _app.Commutator.Technology.Rgrs[rgr_ind].Volume.IndexToSave = selected.Number;
                    }
                }
            }
        }

        protected void SetSaveVolume(string name, int index)
        {
            switch (index)
            {
                case 0:

                    textBox3.Text = name;                    
                    break;

                case 1:

                    textBox5.Text = name;
                    break;

                case 2:

                    textBox9.Text = name;
                    break;

                case 3:

                    textBox13.Text = name;
                    break;

                case 4:

                    textBox17.Text = name;
                    break;

                case 5:

                    textBox21.Text = name;
                    break;

                case 6:

                    textBox25.Text = name;
                    break;

                case 7:

                    textBox29.Text = name;
                    break;

                default:
                    break;
            }
        }

        protected void SetSaveConsumption(string name, int index)
        {
            switch (index)
            {
                case 0:

                    textBox4.Text = name;
                    break;

                case 1:

                    textBox6.Text = name;
                    break;

                case 2:

                    textBox10.Text = name;
                    break;

                case 3:

                    textBox14.Text = name;
                    break;

                case 4:

                    textBox18.Text = name;
                    break;

                case 5:

                    textBox22.Text = name;
                    break;

                case 6:

                    textBox26.Text = name;
                    break;

                case 7:

                    textBox30.Text = name;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RgrsForm_Load(object sender, EventArgs e)
        {
            try
            {
                Parameter[] parameters = _app.Commutator.Parameters;
                PDescription[] p_desc = DevManClient.Parameters;

                if (parameters != null && p_desc != null)
                {
                    int index = 1;
                    foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                    {
                        switch (index)
                        {
                            case 1:

                                if (rgr.Consumption.Index > -1 && rgr.Consumption.Index < parameters.Length)
                                {
                                    textBox1.Text = parameters[rgr.Consumption.Index].Name;
                                }

                                if (rgr.Volume.Index > -1 && rgr.Volume.Index < parameters.Length)
                                {
                                    textBox2.Text = parameters[rgr.Volume.Index].Name;
                                }

                                textBox4.Text = GetDescription(p_desc, rgr.Consumption.IndexToSave);
                                textBox3.Text = GetDescription(p_desc, rgr.Volume.IndexToSave); 
                                
                                break;

                            case 2:

                                if (rgr.Consumption.Index > -1 && rgr.Consumption.Index < parameters.Length)
                                {
                                    textBox8.Text = parameters[rgr.Consumption.Index].Name;
                                }

                                if (rgr.Volume.Index > -1 && rgr.Volume.Index < parameters.Length)
                                {
                                    textBox7.Text = parameters[rgr.Volume.Index].Name;
                                }

                                textBox5.Text = GetDescription(p_desc, rgr.Volume.IndexToSave);
                                textBox6.Text = GetDescription(p_desc, rgr.Consumption.IndexToSave);

                                break;

                            case 3:

                                if (rgr.Consumption.Index > -1 && rgr.Consumption.Index < parameters.Length)
                                {
                                    textBox12.Text = parameters[rgr.Consumption.Index].Name;
                                }

                                if (rgr.Volume.Index > -1 && rgr.Volume.Index < parameters.Length)
                                {
                                    textBox11.Text = parameters[rgr.Volume.Index].Name;
                                }

                                textBox9.Text = GetDescription(p_desc, rgr.Volume.IndexToSave);
                                textBox10.Text = GetDescription(p_desc, rgr.Consumption.IndexToSave);

                                break;

                            case 4: 

                                if (rgr.Consumption.Index > -1 && rgr.Consumption.Index < parameters.Length)
                                {
                                    textBox16.Text = parameters[rgr.Consumption.Index].Name;
                                }

                                if (rgr.Volume.Index > -1 && rgr.Volume.Index < parameters.Length)
                                {
                                    textBox15.Text = parameters[rgr.Volume.Index].Name;
                                }

                                textBox13.Text = GetDescription(p_desc, rgr.Volume.IndexToSave);
                                textBox14.Text = GetDescription(p_desc, rgr.Consumption.IndexToSave);

                                break;

                            case 5:

                                if (rgr.Consumption.Index > -1 && rgr.Consumption.Index < parameters.Length)
                                {
                                    textBox20.Text = parameters[rgr.Consumption.Index].Name;
                                }

                                if (rgr.Volume.Index > -1 && rgr.Volume.Index < parameters.Length)
                                {
                                    textBox19.Text = parameters[rgr.Volume.Index].Name;
                                }

                                textBox17.Text = GetDescription(p_desc, rgr.Volume.IndexToSave);
                                textBox18.Text = GetDescription(p_desc, rgr.Consumption.IndexToSave);

                                break;

                            case 6:

                                if (rgr.Consumption.Index > -1 && rgr.Consumption.Index < parameters.Length)
                                {
                                    textBox24.Text = parameters[rgr.Consumption.Index].Name;
                                }

                                if (rgr.Volume.Index > -1 && rgr.Volume.Index < parameters.Length)
                                {
                                    textBox23.Text = parameters[rgr.Volume.Index].Name;
                                }

                                textBox21.Text = GetDescription(p_desc, rgr.Volume.IndexToSave);
                                textBox22.Text = GetDescription(p_desc, rgr.Consumption.IndexToSave);

                                break;

                            case 7:

                                if (rgr.Consumption.Index > -1 && rgr.Consumption.Index < parameters.Length)
                                {
                                    textBox28.Text = parameters[rgr.Consumption.Index].Name;
                                }

                                if (rgr.Volume.Index > -1 && rgr.Volume.Index < parameters.Length)
                                {
                                    textBox27.Text = parameters[rgr.Volume.Index].Name;
                                }

                                textBox25.Text = GetDescription(p_desc, rgr.Volume.IndexToSave);
                                textBox26.Text = GetDescription(p_desc, rgr.Consumption.IndexToSave);

                                break;

                            case 8:

                                if (rgr.Consumption.Index > -1 && rgr.Consumption.Index < parameters.Length)
                                {
                                    textBox32.Text = parameters[rgr.Consumption.Index].Name;
                                }

                                if (rgr.Volume.Index > -1 && rgr.Volume.Index < parameters.Length)
                                {
                                    textBox31.Text = parameters[rgr.Volume.Index].Name;
                                }

                                textBox29.Text = GetDescription(p_desc, rgr.Volume.IndexToSave);
                                textBox30.Text = GetDescription(p_desc, rgr.Consumption.IndexToSave);

                                break;


                            default:
                                break;
                        }

                        index = index + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// найти параметр по его номеру
        /// </summary>
        /// <param name="p_desc"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        protected string GetDescription(PDescription[] p_desc, int Index)
        {
            foreach (PDescription p_d in p_desc)
            {
                if (p_d.Number == Index)
                {
                    return p_d.Description;
                }
            }

            return string.Empty;
        }
    }
}