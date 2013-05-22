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
    public partial class InsertProjectForm : Form
    {
        protected SKC.Application _app = null;

        public InsertProjectForm()
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