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
    public partial class EditProjectForm : Form
    {
        protected SKC.Application _app = null;

        public EditProjectForm()
        {
            InitializeComponent();

            _app = SKC.Application.CreateInstance();
        }

        /// <summary>
        /// Определяет месторождение проекта
        /// </summary>
        public string ProjectPlace
        {
            get
            {
                return comboBoxPlace.Text;
            }

            set
            {
                comboBoxPlace.Text = value;
            }
        }

        /// <summary>
        /// Определяет куст проекта
        /// </summary>
        public string ProjectBush
        {
            get
            {
                return comboBoxBush.Text;
            }

            set
            {
                comboBoxBush.Text = value;
            }
        }

        /// <summary>
        /// Определяет скважину проекта
        /// </summary>
        public string ProjectWell
        {
            get
            {
                return comboBoxWell.Text;
            }

            set
            {
                comboBoxWell.Text = value;
            }
        }

        /// <summary>
        /// Определяет задание на работу проекта
        /// </summary>
        public string ProjectJob
        {
            get
            {
                return textBoxJob.Text;
            }

            set
            {
                textBoxJob.Text = value;
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertProjectForm_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (Project project in _app.Projects)
                {
                    if (!comboBoxPlace.Items.Contains(project.Place))
                    {
                        comboBoxPlace.Items.Add(project.Place);
                    }
                }
            }
            catch { }
        }
    }
}