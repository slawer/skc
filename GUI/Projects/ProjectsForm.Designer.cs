namespace SKC
{
    partial class ProjectsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectsForm));
            this.treeViewProjects = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.OpenProject = new System.Windows.Forms.Button();
            this.EditProject = new System.Windows.Forms.Button();
            this.InsertNewProject = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.editStages = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewProjects
            // 
            this.treeViewProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewProjects.ImageIndex = 0;
            this.treeViewProjects.ImageList = this.imageList;
            this.treeViewProjects.Location = new System.Drawing.Point(12, 12);
            this.treeViewProjects.Name = "treeViewProjects";
            this.treeViewProjects.SelectedImageIndex = 0;
            this.treeViewProjects.Size = new System.Drawing.Size(321, 438);
            this.treeViewProjects.TabIndex = 0;
            this.treeViewProjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewProjects_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "base_map.png");
            this.imageList.Images.SetKeyName(1, "filecabinet.png");
            this.imageList.Images.SetKeyName(2, "Flag.png");
            this.imageList.Images.SetKeyName(3, "Page.png");
            // 
            // OpenProject
            // 
            this.OpenProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenProject.Location = new System.Drawing.Point(339, 419);
            this.OpenProject.Name = "OpenProject";
            this.OpenProject.Size = new System.Drawing.Size(146, 31);
            this.OpenProject.TabIndex = 8;
            this.OpenProject.Text = "Открыть проект";
            this.OpenProject.UseVisualStyleBackColor = true;
            this.OpenProject.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // EditProject
            // 
            this.EditProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditProject.Location = new System.Drawing.Point(491, 382);
            this.EditProject.Name = "EditProject";
            this.EditProject.Size = new System.Drawing.Size(146, 31);
            this.EditProject.TabIndex = 7;
            this.EditProject.Text = "Редактировать проект";
            this.EditProject.UseVisualStyleBackColor = true;
            this.EditProject.Click += new System.EventHandler(this.EditProject_Click);
            // 
            // InsertNewProject
            // 
            this.InsertNewProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.InsertNewProject.Location = new System.Drawing.Point(339, 382);
            this.InsertNewProject.Name = "InsertNewProject";
            this.InsertNewProject.Size = new System.Drawing.Size(146, 31);
            this.InsertNewProject.TabIndex = 6;
            this.InsertNewProject.Text = "Добавить проект";
            this.InsertNewProject.UseVisualStyleBackColor = true;
            this.InsertNewProject.Click += new System.EventHandler(this.InsertNewProject_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(339, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(296, 364);
            this.textBox1.TabIndex = 8;
            // 
            // editStages
            // 
            this.editStages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editStages.Location = new System.Drawing.Point(491, 419);
            this.editStages.Name = "editStages";
            this.editStages.Size = new System.Drawing.Size(146, 31);
            this.editStages.TabIndex = 9;
            this.editStages.Text = "Редактировать этапы";
            this.editStages.UseVisualStyleBackColor = true;
            this.editStages.Click += new System.EventHandler(this.editStages_Click);
            // 
            // ProjectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 462);
            this.Controls.Add(this.editStages);
            this.Controls.Add(this.OpenProject);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.EditProject);
            this.Controls.Add(this.InsertNewProject);
            this.Controls.Add(this.treeViewProjects);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(664, 500);
            this.Name = "ProjectsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Проекты";
            this.Load += new System.EventHandler(this.ProjectsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewProjects;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button EditProject;
        private System.Windows.Forms.Button InsertNewProject;
        private System.Windows.Forms.Button OpenProject;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button editStages;
    }
}