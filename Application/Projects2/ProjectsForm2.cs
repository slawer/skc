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
    public partial class ProjectsForm2 : Form
    {
        protected SKC.Application _app = null;
//        public List<Project> projects = null; // Список проектов
        public Projects projects = new Projects(); // Список проектов

        private const string _frm_Plase0 = "Месторождение: {0}";
        private const string _frm_Bush0 = "Куст: {0}";
        private const string _frm_Well0 = "Скважина: {0}";
        private const string _frm_Job0 = "Задание: {0}";

        private const string _frm_Plase1 = "Месторождение: {0}";
        private const string _frm_Bush1  = "         Куст: {0}";
        private const string _frm_Well1  = "     Скважина: {0}";
        private const string _frm_Job1   = "      Задание: {0}";
        private const string _frm_Splt1  = " ============";
        private const string _frm_Creat1 = "Дата создания Задания : {0}";
        private const string _frm_Workd1 = "Дата начала работы    : {0}";
        private const string _frm_Finsh1 = "Дата завершения работы: {0}";
        private const string _frm_DBnam1 = "Имя Базы Данных: {0}";

// ==========
// 07.12.2011
        private const string _frm_Splt2 = " == Список этапов {0}:";
        private const string _frm_Stage1 = "№={0} Имя=<{1}> Поправка={2}";
// ==========

        protected DateTime created;         // время создания проекта
        protected DateTime worked;          // время начала работы
        protected DateTime finish;          // Дата конца работы
        protected string db_name;           // база данных в которую сохраняются даные данного проекта

        public TreeNode _selnode = null;    // ссылка на выделенный (раскрытый, текущий) узел

        private bool _filtr = false;        // Признак фильтрации задания по датам от и до
        
        DateTime from = DateTime.MinValue;
        DateTime to = DateTime.MaxValue;

        public ProjectsForm2()
        {
            InitializeComponent();

            _app = SKC.Application.CreateInstance();
            if (_app != null)
            {
                for (int j = 0; j < _app.Projects.Count; j++)
                {
                    Project q=_app.Projects[j];
                    q.IsSelect = q.Actived;
                    q.InternalNmb = j;
                    projects.AddProject(q, false);
                }

                Project p = projects.GetSelectProject();
                if (p == null)
                {
                    projects.SetSelectProject(projects.Count - 1);
                }

                this.projects.SortByName();
                this.BuildProjectsTreeView();
                this.ShowSelNode();
                this.treeViewProjects.Select();
            }
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        /// <summary>
        /// Создаёт текстовое описание узла
        /// </summary>
        /// <param name="Node">Узел дерева проектов</param>
        /// <returns>Массив строк с описанием</returns>
        protected string[] GetNodeAttribute(TreeNode Node)
        {
            if (Node == null) return null;

            List<string> tmp = new List<string>();
            Project p = (Project)Node.Tag;
            if (p != null)
            {
                tmp.Add(p.Place);
                tmp.Add(p.Bush);
                tmp.Add(p.Well);
                tmp.Add(p.Job);
            }
            else
            {
                string[] frms = { _frm_Plase1, _frm_Bush1, _frm_Well1 };
                string[] capthions = new string[3];
                int j = 0;
                capthions[j] = Node.Text;
                TreeNode t1 = Node.Parent;
                if (t1 != null)
                {
                    capthions[++j] = t1.Text;
                    t1 = t1.Parent;
                    if (t1 != null)
                    {
                        capthions[++j] = t1.Text;
                        t1 = t1.Parent;
                        if (t1 != null)
                        {
                            capthions[++j] = t1.Text;
                        }
                    }
                }
                for (int nmb = 0; nmb < j + 1; nmb++)
                {
                    int pos = capthions[j - nmb].IndexOf(':') + 1;
                    string val = capthions[j - nmb].Substring(pos).Trim();
                    tmp.Add(val);
                }
            }
            return tmp.ToArray();
        }

        /// <summary>
        /// Добавляем новый проект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertNewProject_Click(object sender, EventArgs e)
        {
            InsertProjectForm2 frm = new InsertProjectForm2();

            if (_selnode != null)
            {
                string[] opis = GetNodeAttribute(_selnode);
                int j = opis.Length;
                // ==========
                // 07.12.2011
                frm.SetOpis(opis, projects, _selnode.Tag as Project);
            }

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                if (_app != null)
                {
                    frm.newProject.IsModify = true;

                    Project prj = frm.newProject;

                    prj.Stages.Clear();
                    ProjectStage stage = new ProjectStage();
                    stage.Koef = 1;
                    stage.StageName = "Этап #1";
                    prj.Stages.Add(stage);

                    DateTime now = DateTime.Now;
                    prj.Created = now;
                    prj.Dir = string.Format("{0:00}_{1:00}_{2:00}_{3:00}_{4:00}_{5:00}", 
                        now.Year, 
                        now.Month,
                        now.Day, 
                        now.Hour, 
                        now.Minute, 
                        now.Second);
                    prj.DB_Name = string.Format("db_{0:00}_{1:00}_{2:00}_{3:00}_{4:00}_{5:00}",
                        now.Year, 
                        now.Month, 
                        now.Day, 
                        now.Hour, 
                        now.Minute, 
                        now.Second);

                    prj.InternalNmb = projects.maxNmb + 1;
                    prj.IsModify = true;
                    projects.AddProject(prj, true);
                    _app.Projects.Add(prj);

                    projects.SortByName();
                    BuildProjectsTreeView();
                    this.ShowSelNode();
                    this.treeViewProjects.Select();
                }
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

        public void BuildProjectsTreeView()
        {
            string sPlase = string.Empty;
            string sBush = string.Empty;
            string sWell = string.Empty;
            string sJob = string.Empty;
            TreeNode t1 = null, t2 = null, t3 = null, t4 = null; // узлы дерева соответствующего уровня
            int l1 = -1; int l2 = -1; int l3 = -1; int l4 = -1;

            this.treeViewProjects.BeginUpdate();
            this.treeViewProjects.Nodes.Clear();

            _selnode = null;

            for (int j = 0; j < projects.Count; j++)
            {
                Project p = this.projects.getProject(j);
                if (this._filtr)
                {
//                    if (this.from > p.Finish) continue;
//                    if (this.to < p.Created) continue;
                    if(!(
                        ((this.from <= p.Created) && (p.Created <= this.to)) || 
                        ((this.from <= p.Worked) && (p.Worked <= this.to)) || 
                        ((this.from <= p.Finish) && (p.Finish <= this.to))
                        )
                      )
                    {
                        continue;
                    }
                }

                if (sPlase != p.Place)
                {
                    sPlase = p.Place;
                    l1 = this.treeViewProjects.Nodes.Add(new TreeNode(string.Format(_frm_Plase0, p.Place), 2, 2));
                    t1 = this.treeViewProjects.Nodes[l1];
                    sBush = string.Empty;
                    sWell = string.Empty;
                    sJob = string.Empty;
                }

                if (sBush != p.Bush)
                {
                    sBush = p.Bush;
                    l2 = t1.Nodes.Add(new TreeNode(string.Format(_frm_Bush0, sBush), 2, 2));
                    t2 = t1.Nodes[l2];
                    sWell = string.Empty;
                    sJob = string.Empty;
                }

                if (sWell != p.Well)
                {
                    sWell = p.Well;
                    l3 = t2.Nodes.Add(new TreeNode(string.Format(_frm_Well0, sWell), 2, 2));
                    t3 = t2.Nodes[l3];
                    sJob = string.Empty;
                }

                {
                    sJob = p.Job;
                    l4 = t3.Nodes.Add(new TreeNode(string.Format(_frm_Job0, sJob), 3, 3));
                    t4 = t3.Nodes[l4];
                    t4.Tag = p;
                    if (p.IsSelect) _selnode = t4;
                }
            }

            this.treeViewProjects.EndUpdate();

            this.ShowSelNode();

        }

        private void ShowSelNode()
        {
            if (_selnode != null)
            {
                _selnode.EnsureVisible();
                treeViewProjects.SelectedNode = _selnode;

                ShowNode(_selnode);
            }
            else
            {
                this.textBox1.Text = string.Empty;
            }
        }

        private void ShowNode(TreeNode Node)
        {
            Project p = (Project)Node.Tag;
            if (p != null)
            {
                /*
                        protected DateTime created;     // время создания проекта
                        protected DateTime worked;      // время проведения работы
                        protected DateTime finish;      // Дата конца работы
                        protected string db_name;       // база данных в которую сохраняются даные данного проекта
 
                 */
                string s1 = p.Created.ToString();
                string s2 = string.Empty; if (p.Worked != DateTime.MinValue) s2 = p.Worked.ToString();
                string s3 = string.Empty; if (p.Finish != DateTime.MinValue) s3 = p.Finish.ToString();

                // ==========
                // 07.12.2011
                string tb1 = string.Concat(string.Format(_frm_Plase1, p.Place),
                                                    Constants.vbCrLf, string.Format(_frm_Bush1, p.Bush),
                                                    Constants.vbCrLf, string.Format(_frm_Well1, p.Well),
                                                    Constants.vbCrLf, string.Format(_frm_Job1, p.Job),
                                                    Constants.vbCrLf, _frm_Splt1,
                                                    Constants.vbCrLf, string.Format(_frm_Creat1, s1),
                                                    Constants.vbCrLf, string.Format(_frm_Workd1, s2),
                                                    Constants.vbCrLf, string.Format(_frm_Finsh1, s3),
                                                    Constants.vbCrLf, _frm_Splt1,
                                                    Constants.vbCrLf, string.Format(_frm_DBnam1, p.DB_Name)
                                                   );
                if (p.Stages.Count > 0)
                {
                    string tb2 = string.Concat(Constants.vbCrLf, string.Format(_frm_Splt2, p.Stages.Count));
                    for (int j = 0; j < p.Stages.Count; j++)
                    {
                        string sTmp = string.Format(_frm_Stage1, j + 1, p.Stages[j].StageName, p.Stages[j].Koef);
                        tb2 = string.Concat(tb2, Constants.vbCrLf, sTmp);
                    }
                    this.textBox1.Text = string.Concat(tb1, tb2);
                }
                else
                {
                    this.textBox1.Text = tb1;
                }
                // ==========
            }
            else
            {
                string[] frms = {_frm_Plase1, _frm_Bush1, _frm_Well1};
                string[] capthions = new string[3];
                int j = 0;
                capthions[j] = Node.Text;
                TreeNode t1 = Node.Parent;
                if (t1 != null)
                {
                    capthions[++j] = t1.Text;
                    t1 = t1.Parent;
                    if (t1 != null)
                    {
                        capthions[++j] = t1.Text;
                        t1 = t1.Parent;
                        if (t1 != null)
                        {
                            capthions[++j] = t1.Text;
                        }
                    }
                }
                string s1 = string.Empty;
                for (int nmb = 0; nmb < j+1; nmb++)
                {
                    int pos = capthions[j-nmb].IndexOf(':') + 1;
                    string val = string.Format(frms[nmb], capthions[j - nmb].Substring(pos).Trim());
                    s1 = string.Concat(s1, val, Constants.vbCrLf);
                }
                this.textBox1.Text = s1;
            }
        }

        private void treeViewProjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _selnode = e.Node;
            ShowNode(e.Node);
        }

        /// <summary>
        /// Редактировать описание проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditProject_Click(object sender, EventArgs e)
        {
            if (_selnode == null)
            {
                MessageBox.Show(this, "Пожалуйста, выберите задание и повторите команду", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (_selnode.Tag == null)
            {
                MessageBox.Show(this, "Пожалуйста, выберите задание и повторите команду", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            EditProjectForm2 frm = new EditProjectForm2();
            {
                string[] opis = GetNodeAttribute(_selnode);
                // ==========
                // 07.12.2011
                frm.SetOpis(opis, projects, _selnode.Tag as Project);
            }

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                if (_app != null)
                {
                    Project p = (Project)_selnode.Tag;
                    if ((p.Place != frm.newProject.Place) || (p.Bush != frm.newProject.Bush) || 
                        (p.Well != frm.newProject.Well) || (p.Job != frm.newProject.Job))
                    {
                        p.IsModify = true;
                        p.Place = frm.newProject.Place;
                        p.Bush = frm.newProject.Bush;
                        p.Well = frm.newProject.Well;
                        p.Job = frm.newProject.Job;
                        projects.ClearSelect();
                        p.IsSelect = true;
                        projects.SortByName();
                        BuildProjectsTreeView();
                        this.ShowSelNode();
                        this.treeViewProjects.Select();
                    }
                }
            }
        }

        /// <summary>
        /// Разбор интервала фильтрации
        /// </summary>
        /// <returns>Результат разбора</returns>
        private bool dtsParse()
        {
            try
            { 
                DateTime d1 = (dateTimePickerFrom.Value).Date; // DateTime.Parse(this.textBoxFrom.Text);
                DateTime d2 = (dateTimePickerTo.Value).Date.AddDays(1); // DateTime.Parse(this.textBoxTo.Text);
                this.from = d1;
                this.to = d2;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void checkBoxFiltr_Click(object sender, EventArgs e)
        {
            bool newFiltr = this.checkBoxFiltr.Checked;
            if (newFiltr != this._filtr)
            {
                if (newFiltr)
                {
                    // Парсим новые значения дат поиска
                    if (!dtsParse())
                    {
                        MessageBox.Show(this, "Обнаружена ошибка в датах интервала поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.checkBoxFiltr.Checked = false;
                        return;
                    }
                }
                this._filtr = newFiltr;
                this.BuildProjectsTreeView();
            }
        }

        private void UpdateFiltr_Click(object sender, EventArgs e)
        {
            // Парсим новые значения дат поиска
            if (!dtsParse())
            {
                MessageBox.Show(this, "Обнаружена ошибка в датах интервала поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this._filtr = this.checkBoxFiltr.Checked;
            this.BuildProjectsTreeView();
        }

        private void OpenProject_Click(object sender, EventArgs e)
        {
            if (_selnode.Tag == null)
            {
                MessageBox.Show(this, "Необходимо предварительно выбрать Задание", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Project prj = _selnode.Tag as Project;

            if (prj.Stages.Count == 0)
            {
                ProjectStage stage = new ProjectStage();

                stage.Koef = 1;
                stage.StageName = "Этап #1";

                prj.Stages.Add(stage);
            }

            if (_app != null)
            {
                for (int j = 0; j < _app.Projects.Count; j++)
                {
                    _app.Projects[j].Actived = false;
                }

                prj.Actived = true;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void editStages_Click(object sender, EventArgs e)
        {
            if (_selnode.Tag == null)
            {
                MessageBox.Show(this, "Необходимо предварительно выбрать Задание", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Project prj = _selnode.Tag as Project;

            if (prj != null)
            {
                ProjectStagesForm frm = new ProjectStagesForm(prj);
                frm.ShowDialog(this);
                this.ShowSelNode();
                this.treeViewProjects.Select();
            }
        }

        private void CloseWnd_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CopyFromProject_Click(object sender, EventArgs e)
        {
            InsertProjectForm2 frm = new InsertProjectForm2();

            if (_selnode != null)
            {
                string[] opis = GetNodeAttribute(_selnode);
                int j = opis.Length;
                // ==========
                // 07.12.2011
                frm.SetOpis(opis, projects, _selnode.Tag as Project);
            }

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                if (_app != null)
                {
                    frm.newProject.IsModify = true;

                    Project prj = frm.newProject;

                    if (prj.Stages.Count == 0)
                    {
                        ProjectStage stage = new ProjectStage();

                        stage.Koef = 1;
                        stage.StageName = "Этап #1";

                        prj.Stages.Add(stage);
                    }

                    DateTime now = DateTime.Now;
                    prj.Created = now;
                    prj.Dir = string.Format("{0:00}_{1:00}_{2:00}_{3:00}_{4:00}_{5:00}",
                        now.Year,
                        now.Month,
                        now.Day,
                        now.Hour,
                        now.Minute,
                        now.Second);
                    prj.DB_Name = string.Format("db_{0:00}_{1:00}_{2:00}_{3:00}_{4:00}_{5:00}",
                        now.Year,
                        now.Month,
                        now.Day,
                        now.Hour,
                        now.Minute,
                        now.Second);

                    prj.InternalNmb = projects.maxNmb + 1;
                    prj.IsModify = true;
                    projects.AddProject(prj, true);
                    _app.Projects.Add(prj);

                    projects.SortByName();
                    BuildProjectsTreeView();
                    this.ShowSelNode();
                    this.treeViewProjects.Select();
                }
            }
        }

        /// <summary>
        /// Создать и выбрать новый проект
        /// </summary>
        public bool CreateNewAndSelectProject(Form frame)
        {
            try
            {
                InsertProjectForm2 frm = new InsertProjectForm2();

                if (_selnode != null)
                {
                    string[] opis = GetNodeAttribute(_selnode);
                    int j = opis.Length;
                    // ==========
                    // 07.12.2011
                    frm.SetOpis(opis, projects, _selnode.Tag as Project);
                }

                if (frm.ShowDialog(frame) == DialogResult.OK)
                {
                    if (_app != null)
                    {
                        frm.newProject.IsModify = true;

                        Project prj = frm.newProject;

                        prj.Stages.Clear();
                        ProjectStage stage = new ProjectStage();
                        stage.Koef = 1;
                        stage.StageName = "Этап #1";
                        prj.Stages.Add(stage);

                        DateTime now = DateTime.Now;
                        prj.Created = now;
                        prj.Dir = string.Format("{0:00}_{1:00}_{2:00}_{3:00}_{4:00}_{5:00}",
                            now.Year,
                            now.Month,
                            now.Day,
                            now.Hour,
                            now.Minute,
                            now.Second);
                        prj.DB_Name = string.Format("db_{0:00}_{1:00}_{2:00}_{3:00}_{4:00}_{5:00}",
                            now.Year,
                            now.Month,
                            now.Day,
                            now.Hour,
                            now.Minute,
                            now.Second);

                        prj.InternalNmb = projects.maxNmb + 1;
                        prj.IsModify = true;
                        projects.AddProject(prj, true);
                        _app.Projects.Add(prj);

                        projects.SortByName();
                        BuildProjectsTreeView();
                        this.ShowSelNode();
                        this.treeViewProjects.Select();

                        OpenProject_Click(null, EventArgs.Empty);
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }
    }

    public class Projects
    {
        protected List<Project> _projects = new List<Project>();
        //        public List<Project> _projects = null;

        /// <summary>
        /// возвращает максимальное значение внутреннего номера
        /// </summary>
        /// <returns></returns>
        public int maxNmb
        {
            get
            {
                int max = -1;
                for (int j = 0; j < this._projects.Count; j++)
                {
                    int m = this._projects[j].InternalNmb;
                    if (m > max) max = m;
                }
                return max;
            }
        }

        /// <summary>
        /// Возвращает длину массива проектов
        /// </summary>
        /// <returns></returns>
        public int Count
        {
            get
            {
                return this._projects.Count();
            }
        }
        /// <summary>
        /// Метод сортировки проектов по алфавиту
        /// </summary>
        public void SortByName()
        {
            _projects.Sort(new Project.SortByName());
        }

        /// <summary>
        /// Метод сортировки проектов по внутреннему номеру
        /// </summary>
        public void SortByNmb()
        {
            _projects.Sort(new Project.SortByNmb());
        }

        /// <summary>
        /// Удалить признак выделенности
        /// </summary>
        public void ClearSelect()
        {
            for (int j = 0; j < _projects.Count; j++)
            {
                _projects[j].IsSelect = false;
            }
        }

        public void SetSelectProject(int Number)
        {
            ClearSelect();
            if ((Number < 0) || (Number >= _projects.Count)) return;
            _projects[Number].IsSelect = true;
        }

        /// <summary>
        /// возвращает выбранный проект
        /// </summary>
        /// <returns>Ссылка на выделенный объект или 0</returns>
        public Project GetSelectProject()
        {
            for (int j = 0; j < _projects.Count; j++)
            {
                if (_projects[j].IsSelect) return _projects[j];
            }
            return null;
        }

        public int GetSelectProjectNmb()
        {
            for (int j = 0; j < _projects.Count; j++)
            {
                if (_projects[j].IsSelect) return j;
            }
            return -1;
        }

        /// <summary>
        /// Добавляет задание в массив заданий
        /// </summary>
        /// <param name="Val">Новое задание</param>
        /// <param name="Sel">Если true, то помечает текущий проект как selected</param>
        public void AddProject(Project Val, bool Sel)
        {
            _projects.Add(Val);
            if (Sel)
            {
                SetSelectProject(_projects.Count - 1);
            }
        }

        /// <summary>
        /// Возвращает задание по его индексу в массиве заданий
        /// </summary>
        /// <param name="Number">индекс задания в массиве заданий</param>
        /// <returns></returns>
        public Project getProject(int Number)
        {
            if ((Number < 0) || (Number >= _projects.Count)) return null;
            return _projects[Number];
        }
    }
}