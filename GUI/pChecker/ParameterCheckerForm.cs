using System;
using System.Threading;
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
    public partial class ParameterCheckerForm : Form
    {
        private Application _app;

        public ParameterCheckerForm()
        {
            InitializeComponent();

            _app = Application.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParameterCheckerForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_app != null)
                {
                    Parameter[] parameters = _app.Commutator.Parameters;
                    if (parameters != null)
                    {
                        foreach (Parameter parameter in parameters)
                        {
                            if (parameter.Description != "-----" && parameter.Name != "Параметр не определен")
                            {
                                PDescription param = parameter.Channel;
                                if (param.Type != DeviceManager.FormulaType.Capture)
                                {
                                    InsertToList(parameter);
                                }
                            }
                        }
                    }
                }

                timer1_Tick(timer1, EventArgs.Empty);
                timer1.Start();
            }
            catch { }
        }

        /// <summary>
        /// добавить параметр в список
        /// </summary>
        /// <param name="parameter"></param>
        private void InsertToList(Parameter parameter)
        {
            int count = listViewParameters.Items.Count + 1;

            ListViewItem item = new ListViewItem(count.ToString());

            ListViewItem.ListViewSubItem des = new ListViewItem.ListViewSubItem(item, parameter.Name);
            ListViewItem.ListViewSubItem status = new ListViewItem.ListViewSubItem(item, "-----");

            item.Tag = parameter;

            item.SubItems.Add(des);
            item.SubItems.Add(status);

            listViewParameters.Items.Add(item);
        }

        private Mutex cMutex = new Mutex();

        /// <summary>
        /// проверяем текущее состояние параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (cMutex.WaitOne(50))
                {
                    foreach (ListViewItem item in listViewParameters.Items)
                    {
                        Parameter parameter = item.Tag as Parameter;
                        if (parameter != null)
                        {
                            if (parameter.IsValidValue == false)
                            {
                                if (item.BackColor != Color.Salmon || item.SubItems[2].Text != "Отключен")
                                {
                                    item.BackColor = Color.Salmon;
                                    item.SubItems[2].Text = "Отключен";
                                }
                            }
                            else
                            {
                                if (item.BackColor != SystemColors.Window || item.SubItems[2].Text != "Включен")
                                {
                                    item.BackColor = SystemColors.Window;
                                    item.SubItems[2].Text = "Включен";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
            finally
            {
                if (blocked) cMutex.ReleaseMutex();
            }
        }
    }
}