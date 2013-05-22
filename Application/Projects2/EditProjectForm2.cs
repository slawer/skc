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
    public partial class EditProjectForm2 : Form
    {
        public EditProjectForm2()
        {
            InitializeComponent();
        }

        public Project newProject = new Project(); // Новое значение параметров проекта

        // ==========
        // 07.12.2011
        public void SetOpis(string[] opis, Projects ps, Project sellNode)
        // ==========
        {
            if (ps != null)
            {
                string Place = string.Empty, Bush = string.Empty, Well = string.Empty;
                for (int j = 0; j < ps.Count; j++)
                {
                    Project p = ps.getProject(j);
                    if (p.Place != Place)
                    {
                        Place = p.Place;
                        this.comboBoxPlace.Items.Add(Place);
                    }
                    if (p.Bush != Bush)
                    {
                        Bush = p.Bush;
                        this.comboBoxBush.Items.Add(Bush);
                    }
                    if (p.Well != Well)
                    {
                        Well = p.Well;
                        this.comboBoxWell.Items.Add(Well);
                    }
                }
            }

            if (opis != null)
            {
                if (opis.Length > 0)
                {
                    this.comboBoxPlace.Text = opis[0];
                    if (opis.Length > 1)
                    {
                        this.comboBoxBush.Text = opis[1];
                        if (opis.Length > 2)
                        {
                            this.comboBoxWell.Text = opis[2];
                            if (opis.Length > 3)
                            {
                                this.textBoxJob.Text = opis[3];
                            }
                        }
                    }
                }
            }

            // ==========
            // 07.12.2011
            if (sellNode != null)
            {
                if (sellNode.Stages != null)
                {
                    for (int j = 0; j < sellNode.Stages.Count; j++)
                    {
                        this.newProject.Stages.Add(sellNode.Stages[j]);
                    }
                }
            }
            // ==========
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            newProject.Place = this.comboBoxPlace.Text.Trim();
            newProject.Bush = this.comboBoxBush.Text.Trim();
            newProject.Well = this.comboBoxWell.Text.Trim();
            newProject.Job = this.textBoxJob.Text.Trim();
            if ((newProject.Place == string.Empty) || 
                (newProject.Bush == string.Empty) || 
                (newProject.Well == string.Empty) || 
                (newProject.Job == string.Empty))
            {
                MessageBox.Show("Необходимо заполнить все поля формы!");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }
    }
}
