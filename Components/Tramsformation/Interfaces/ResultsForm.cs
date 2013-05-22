using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SKC;

namespace DeviceManager
{
    public partial class ResultsForm : Form
    {
        private SKC.Application app = null;         // контекст в котором выполняется программа
        private Guid p_identifier;                  // идентификатор выделенного параметра

        public ResultsForm(SKC.Application _app)
        {
            app = _app;
            InitializeComponent();
        }

        /// <summary>
        /// Номер позиции выделяемого элемента
        /// </summary>
        public Guid Position
        {
            get { return p_identifier; }
            set { p_identifier = value; }
        }

        /// <summary>
        /// Определяет выбранный параметр
        /// </summary>
        public Parameter SelectedParameter
        {
            get
            {
                if (listViewResults.SelectedItems != null)
                {
                    foreach (ListViewItem item in listViewResults.SelectedItems)
                    {
                        if (item.Tag != null)
                        {
                            if (item.Tag is Parameter)
                            {
                                return (item.Tag as Parameter);
                            }
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultsForm_Load(object sender, EventArgs e)
        {
            if (app != null)
            {
                foreach (var item in app.Commutator.Parameters)
                {
                    InsertParameter(item);
                }
            }

            if (listViewResults.SelectedItems != null && listViewResults.SelectedItems.Count > 0)
            {
                listViewResults.EnsureVisible(listViewResults.SelectedItems[0].Index);
            }
        }

        /// <summary>
        /// Добавить параметр в список
        /// </summary>
        /// <param name="parameter">Добавляемый параметр</param>
        private void InsertParameter(Parameter parameter)
        {
            ListViewItem item = new ListViewItem((listViewResults.Items.Count + 1).ToString());
            ListViewItem.ListViewSubItem desc = new ListViewItem.ListViewSubItem(item, parameter.Name);

            item.SubItems.Add(desc);

            listViewResults.Items.Add(item);
            item.Tag = parameter;

            if (parameter.Identifier == p_identifier)
            {
                item.Selected = true;
            }
        }
    }
}