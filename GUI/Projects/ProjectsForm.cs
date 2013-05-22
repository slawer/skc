using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SKC
{
    public partial class ProjectsForm : Form
    {
        protected SKC.Application _app = null;
        public List<Project> projects = null;            // Список проектов

        public ProjectsForm()
        {
            InitializeComponent();

            _app = SKC.Application.CreateInstance();
            if (_app != null)
            {
                projects = _app.Projects;
            }
        }

        /// <summary>
        /// Загружаемся (строим дерево проектов)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectsForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (projects != null)
                {
                    foreach (Project project in projects)
                    {
                        if (project != null)
                        {
                            InsertProjectToTree(project);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка во время построения дерева проектов",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                treeViewProjects.Nodes.Clear();
            }
        }

        /// <summary>
        /// Добавляем новый проек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertNewProject_Click(object sender, EventArgs e)
        {
            InsertProjectForm frm = new InsertProjectForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Project project = new Project();

                project.Place = frm.ProjectPlace;
                project.Bush = frm.ProjectBush;

                project.Well = frm.ProjectWell;
                project.Job = frm.ProjectJob;

                DateTime now = DateTime.Now;
                project.DB_Name = string.Format("db_{0:00}_{1:00}_{2:00}_{3:00}_{4:00}_{5:00}", 
                    now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                project.Created = now;
                project.Dir = string.Format("{0:00}_{1:00}_{2:00}_{3:00}_{4:00}_{5:00}", now.Year, now.Month, 
                    now.Day, now.Hour, now.Minute, now.Second);


                InsertStages(project);
                projects.Add(project);



                InsertProjectToTree(project);
            }
        }

        /// <summary>
        /// Добавить этапы работы с активного проекта
        /// </summary>
        /// <param name="_prj"></param>
        protected void InsertStages(Project _prj)
        {
            try
            {
                Project prj = _app.CurrentProject;
                if (prj != null)
                {
                    foreach (ProjectStage stage in prj.Stages)
                    {
                        ProjectStage new_stage = new ProjectStage();

                        new_stage.StageName = stage.StageName;

                        new_stage.Koef = stage.Koef;

                        new_stage.Plan_consumption = stage.Plan_consumption;
                        new_stage.Plan_density = stage.Plan_density;

                        new_stage.Plan_pressure = stage.Plan_pressure;
                        new_stage.Plan_volume = stage.Plan_volume;

                        _prj.Stages.Add(new_stage);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// редактируем роект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditProject_Click(object sender, EventArgs e)
        {
            if (treeViewProjects.SelectedNode != null)
            {
                TreeNode selected = treeViewProjects.SelectedNode;
                Project prj = selected.Tag as Project;

                if (prj != null)
                {
                    EditProjectForm frm = new EditProjectForm();

                    frm.ProjectPlace = prj.Place;
                    frm.ProjectBush = prj.Bush;

                    frm.ProjectWell = prj.Well;
                    frm.ProjectJob = prj.Job;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        prj.Place = frm.ProjectPlace;
                        prj.Bush = frm.ProjectBush;

                        prj.Well = frm.ProjectWell;
                        prj.Job = frm.ProjectJob;
                        
                        UpdateProjectTree();
                    }
                }
                else
                {
                    MessageBox.Show(this, "Выберите работу и повторите попытку", 
                        "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Выбрать проект для работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenProject_Click(object sender, EventArgs e)
        {
            if (treeViewProjects.SelectedNode != null)
            {
                TreeNode selected = treeViewProjects.SelectedNode;
                Project prj = selected.Tag as Project;

                if (prj != null)
                {
                    foreach (Project project in _app.Projects)
                    {
                        project.Actived = false;
                    }

                    prj.Actived = true;
                    if (prj.Stages.Count == 0)
                    {
                        ProjectStage stage = new ProjectStage();

                        stage.Koef = 1;
                        stage.StageName = "Этап #1";

                        prj.Stages.Add(stage);
                    }

                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show(this, "Выберите работу и повторите попытку", 
                    "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// редактируем этапы проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editStages_Click(object sender, EventArgs e)
        {
            if (treeViewProjects.SelectedNode != null)
            {
                TreeNode selected = treeViewProjects.SelectedNode;
                Project prj = selected.Tag as Project;

                if (prj != null)
                {
                    ProjectStagesForm frm = new ProjectStagesForm(prj);
                    frm.ShowDialog(this);
                }
            }
            else
            {
                MessageBox.Show(this, "Выберите работу и повторите попытку",
                    "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Перерисовать дерево проектов
        /// </summary>
        protected void UpdateProjectTree()
        {
            try
            {
                treeViewProjects.Nodes.Clear();
                ProjectsForm_Load(this, EventArgs.Empty);
            }
            catch { }
        }

        /// <summary>
        /// Втавить проект в пустое дерево проектов
        /// </summary>
        /// <param name="project">Втавляемый в пустое дерево проектов проект</param>
        protected void InsertFirstProjectToTree(Project project)
        {
            try
            {
                TreeNode root = new TreeNode(project.Place, 0, 0);

                TreeNode bushNode = new TreeNode(project.Bush, 1, 1);
                TreeNode wellNode = new TreeNode(project.Well, 2, 2);

                TreeNode jobNode = new TreeNode(project.Job, 3, 3);
                jobNode.Tag = project;

                wellNode.Nodes.Add(jobNode);
                bushNode.Nodes.Add(wellNode);

                root.Nodes.Add(bushNode);
                treeViewProjects.Nodes.Add(root);
            }
            catch { }
        }

        /// <summary>
        /// Добавить новый куст в месторождение
        /// </summary>
        /// <param name="project">Втавляемый проект</param>
        /// <param name="node">Узел в который вставлять</param>
        protected void InsertNewBushNode(Project project, TreeNode node)
        {
            try
            {
                TreeNode bushNode = new TreeNode(project.Bush, 1, 1);
                TreeNode wellNode = new TreeNode(project.Well, 2, 2);

                TreeNode jobNode = new TreeNode(project.Job, 3, 3);
                jobNode.Tag = project;

                wellNode.Nodes.Add(jobNode);
                bushNode.Nodes.Add(wellNode);

                node.Nodes.Add(bushNode);
            }
            catch { }
        }

        /// <summary>
        /// Добавить новую скважину в куст
        /// </summary>
        /// <param name="project">Втавляемый проект</param>
        /// <param name="node">Узел в который вставлять</param>
        protected void InsertNewWellNode(Project project, TreeNode node)
        {
            try
            {
                TreeNode wellNode = new TreeNode(project.Well, 2, 2);
                TreeNode jobNode = new TreeNode(project.Job, 3, 3);
                jobNode.Tag = project;

                wellNode.Nodes.Add(jobNode);
                node.Nodes.Add(wellNode);
            }
            catch { }
        }

        /// <summary>
        /// Добавить новую работу в скважину
        /// </summary>
        /// <param name="project"></param>
        /// <param name="node"></param>
        protected void InsertNewJobNode(Project project, TreeNode node)
        {
            try
            {
                TreeNode jobNode = new TreeNode(project.Job, 3, 3);
                jobNode.Tag = project;

                node.Nodes.Add(jobNode);
            }
            catch { }
        }

        /// <summary>
        /// Добавить проект в дерево проектов
        /// </summary>
        /// <param name="project"></param>
        protected void InsertProjectToTree(Project project)
        {
            try
            {
                if (treeViewProjects.Nodes != null && treeViewProjects.Nodes.Count > 0)
                {
                    foreach (TreeNode node in treeViewProjects.Nodes)
                    {
                        if (node.Text == project.Place)
                        {
                            InsertProjectToProjectNode(project, node);
                            return;
                        }
                    }

                    InsertFirstProjectToTree(project);
                }
                else
                {
                    InsertFirstProjectToTree(project);
                }
            }
            catch { }
        }

        /// <summary>
        /// Вставить проект в существующее месторождение
        /// </summary>
        /// <param name="project">Вставляемый проект</param>
        /// <param name="node">Узел в который вставить проект</param>
        protected void InsertProjectToProjectNode(Project project, TreeNode node)
        {
            if (node != null)
            {
                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (TreeNode child in node.Nodes)  // просматириваем кусты дерева проектов
                    {
                        if (child != null)
                        {
                            if (child.Text == project.Bush)
                            {
                                InsertProjectToBushNode(project, child);
                                return;
                            }
                        }
                    }

                    // ----- вставляем в дерево проектов новый куст ----

                    InsertNewBushNode(project, node);
                }
                else
                {
                    // ----- нету ни одного куста в проекте -----
                }
            }
        }

        /// <summary>
        /// Вставить в дерево проектов в существующий куст
        /// </summary>
        /// <param name="project">Вставляемый проект</param>
        /// <param name="node">Узел в который вставлять</param>
        protected void InsertProjectToBushNode(Project project, TreeNode node)
        {
            if (node != null)
            {
                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (TreeNode child in node.Nodes)  // просматириваем кусты дерева проектов
                    {
                        if (child != null)
                        {
                            if (child.Text == project.Well)
                            {
                                InsertNewJobNode(project, child);
                                return;
                            }
                        }
                    }

                    // ----- вставляем в дерево проектов новый куст ----

                    InsertNewWellNode(project, node);
                }
                else
                {
                    // ----- нету ни одного куста в проекте -----
                }
            }
        }

        /// <summary>
        /// Вставить в дерево проектов новый куст
        /// </summary>
        /// <param name="project">Вставляемый проект</param>
        /// <param name="node">Узел в который вставлять</param>
        protected void InsertProjectToWellNode(Project project, TreeNode node)
        {
            if (node != null)
            {
                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (TreeNode child in node.Nodes)  // просматириваем кусты дерева проектов
                    {
                        if (child != null)
                        {
                            if (child.Text == project.Well)
                            {
                                InsertNewJobNode(project, child);
                                return;
                            }
                        }
                    }

                    // ----- вставляем в дерево проектов новый куст ----

                    InsertNewWellNode(project, node);
                }
                else
                {
                    // ----- нету ни одного куста в проекте -----
                }
            }
        }

        private void treeViewProjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewProjects.SelectedNode != null)
            {
                TreeNode selected = treeViewProjects.SelectedNode;
                Project prj = selected.Tag as Project;

                if (prj != null)
                {
                    // ---- выводим информацию о проекте ----


                }
            }
        }
    }
}