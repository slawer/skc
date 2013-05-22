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
    public partial class ParametersListForm : Form
    {
        private Application _app = null;        // контекст приложения

        public ParametersListForm()
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
        private void ParametersListForm_Load(object sender, EventArgs e)
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
            int count = listViewParameters.Items.Count + 1;
            ListViewItem item = new ListViewItem(count.ToString());
            ListViewItem.ListViewSubItem des = new ListViewItem.ListViewSubItem(item, parameter.Name);

            item.Tag = parameter;
            item.SubItems.Add(des);

            listViewParameters.Items.Add(item);
        }

        /// <summary>
        /// Определяет выбранный параметр
        /// </summary>
        public Parameter SelectedParameter
        {
            get
            {
                if (listViewParameters.SelectedItems != null)
                {
                    if (listViewParameters.SelectedItems.Count > 0)
                    {
                        Parameter selected = listViewParameters.SelectedItems[0].Tag as Parameter;
                        return selected;
                    }
                }

                return null;
            }

            set
            {
            }
        }
    }
}