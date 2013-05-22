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
    public partial class ProjectStagesForm : Form
    {
        Project edited;

        public ProjectStagesForm(Project selected)
        {
            InitializeComponent();
            edited = selected;
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectStagesForm_Load(object sender, EventArgs e)
        {
            if (edited != null)
            {
                foreach (ProjectStage stage in edited.Stages)
                {
                    InsertStageInList(stage);
                }
            }
        }

        /// <summary>
        /// добавить новый этап работы в проект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewStage_Click(object sender, EventArgs e)
        {
            InsertStageForm frm = new InsertStageForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                ProjectStage stage = new ProjectStage();

                stage.Koef = frm.StageKoef;
                stage.StageName = frm.StageName;

                try
                {
                    stage.Plan_consumption = float.Parse(frm.Plan_consumption);
                    stage.Plan_volume = float.Parse(frm.Plan_volume);

                    stage.Plan_density = float.Parse(frm.Plan_density);
                    stage.Plan_pressure = float.Parse(frm.Plan_pressure);
                }
                catch { }

                edited.Stages.Add(stage);
                InsertStageInList(stage);
            }
        }

        /// <summary>
        /// удаляем этап
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeStage_Click(object sender, EventArgs e)
        {
            if (listViewStages.SelectedItems != null &&
                listViewStages.SelectedItems.Count > 0)
            {
                foreach (ListViewItem s_item in listViewStages.SelectedItems)
                {
                    ProjectStage selected = s_item.Tag as ProjectStage;// listViewStages.SelectedItems[0].Tag as ProjectStage;
                    if (selected != null)
                    {
                        edited.Stages.Remove(selected);
                        listViewStages.Items.Remove(listViewStages.SelectedItems[0]);

                        int index = 1;
                        foreach (ListViewItem item in listViewStages.Items)
                        {
                            item.Text = index++.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// редактируем этап
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editStage_Click(object sender, EventArgs e)
        {
            if (listViewStages.SelectedItems != null &&
                listViewStages.SelectedItems.Count > 0)
            {
                ProjectStage selected = listViewStages.SelectedItems[0].Tag as ProjectStage;
                if (selected != null)
                {
                    InsertStageForm frm = new InsertStageForm();
                    
                    frm.StageName = selected.StageName;
                    frm.StageKoef = selected.Koef;

                    frm.Plan_consumption = string.Format("{0:F2}", selected.Plan_consumption);
                    frm.Plan_volume = string.Format("{0:F2}", selected.Plan_volume);

                    frm.Plan_density = string.Format("{0:F2}", selected.Plan_density);
                    frm.Plan_pressure = string.Format("{0:F2}", selected.Plan_pressure);

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        selected.Koef = frm.StageKoef;
                        selected.StageName = frm.StageName;

                        try
                        {
                            selected.Plan_consumption = float.Parse(frm.Plan_consumption);
                            selected.Plan_volume = float.Parse(frm.Plan_volume);

                            selected.Plan_density = float.Parse(frm.Plan_density);
                            selected.Plan_pressure = float.Parse(frm.Plan_pressure);
                        }
                        catch { }

                        listViewStages.SelectedItems[0].SubItems[1].Text = selected.StageName;
                        listViewStages.SelectedItems[0].SubItems[2].Text = selected.Koef.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Добавить этап работы
        /// </summary>
        /// <param name="stage">добавляемый этап работы</param>
        protected void InsertStageInList(ProjectStage stage)
        {
            ListViewItem item = new ListViewItem((listViewStages.Items.Count + 1).ToString());

            ListViewItem.ListViewSubItem name = new ListViewItem.ListViewSubItem(item, stage.StageName);
            ListViewItem.ListViewSubItem koef = new ListViewItem.ListViewSubItem(item, stage.Koef.ToString());

            item.Tag = stage;

            item.SubItems.Add(name);
            item.SubItems.Add(koef);

            listViewStages.Items.Add(item);
        }
    }
}