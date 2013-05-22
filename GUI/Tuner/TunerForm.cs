using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WCF;
using DeviceManager;
using WCF.WCF_Client;

namespace SKC
{
    public partial class TunerForm : Form
    {
        private Application _app = null;        // контекст приложения

        public TunerForm()
        {
            InitializeComponent();

            _app = Application.CreateInstance();
            if (_app == null)
            {
                MessageBox.Show("Не удалось получить доступ к параметрам приложения");
            }
        }

        /// <summary>
        /// Загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunerForm_Load(object sender, EventArgs e)
        {
            Parameter[] parameters = _app.Commutator.Parameters;
            if (parameters != null)
            {
                foreach (Parameter parameter in parameters)
                {
                    InsertParameterToList(parameter);
                }
            }
        }

        /// <summary>
        /// добавить параметр в список
        /// </summary>
        /// <param name="parameter">Добавляемый параметр</param>
        private void InsertParameterToList(Parameter parameter)
        {
            ListViewItem item = new ListViewItem((parameter.SelfIndex + 1).ToString());
            ListViewItem.ListViewSubItem des = new ListViewItem.ListViewSubItem(item, parameter.Name);

            item.Tag = parameter;
            item.SubItems.Add(des);            

            listViewParameters.Items.Add(item);
        }

        /// <summary>
        /// Редактируем параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editParameter_Click(object sender, EventArgs e)
        {
            if (listViewParameters.SelectedItems != null)
            {
                if (listViewParameters.SelectedItems.Count > 0)
                {
                    Parameter selected = listViewParameters.SelectedItems[0].Tag as Parameter;
                    if (selected != null)
                    {
                        EditParameterForm frm = new EditParameterForm(selected);
                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            // ------- тут необходимо применить в силу внесенные изменения -------

                            if (listViewParameters.SelectedItems != null)
                            {
                                if (listViewParameters.SelectedItems.Count > 0)
                                {
                                    ListViewItem selItem = listViewParameters.SelectedItems[0];
                                    if (selItem != null)
                                    {
                                        Parameter selPar = selItem.Tag as Parameter;
                                        if (selPar != null)
                                        {
                                            selItem.SubItems[1].Text = selPar.Name;

                                            PDescription[] pars = DevManClient.Parameters;
                                            try
                                            {
                                                foreach (PDescription param in pars)
                                                {
                                                    if (param.Number == selPar.Channel.Number)
                                                    {
                                                        selPar.Channel.Type = param.Type;
                                                        break;
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                }
                            }

                            _app.UpdateTechGraphics();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Настроить технологические параметры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void techParameters_Click(object sender, EventArgs e)
        {
            EditTechParameterForm frm = new EditTechParameterForm();
            frm.ShowDialog(this);
        }

        /// <summary>
        /// два раза кликнули. нужно открыть для редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewParameters_DoubleClick(object sender, EventArgs e)
        {
            editParameter_Click(null, EventArgs.Empty);
        }

        /// <summary>
        /// нажали ентер на клавиатуре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewParameters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                editParameter_Click(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Настраиваем команды сброса/перехода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonResetterOfVolume_Click(object sender, EventArgs e)
        {
            ResetterOfVolumeForm frm = new ResetterOfVolumeForm();
            frm.ShowDialog(this);


        }
    }
}