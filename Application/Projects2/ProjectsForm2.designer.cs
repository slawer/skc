namespace SKC
{
    partial class ProjectsForm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectsForm2));
            this.treeViewProjects = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.editStages = new System.Windows.Forms.Button();
            this.UpdateFiltr = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxFiltr = new System.Windows.Forms.CheckBox();
            this.OpenProject = new System.Windows.Forms.Button();
            this.EditProject = new System.Windows.Forms.Button();
            this.InsertNewProject = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CopyFromProject = new System.Windows.Forms.Button();
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
            this.treeViewProjects.Size = new System.Drawing.Size(408, 430);
            this.treeViewProjects.TabIndex = 0;
            this.treeViewProjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewProjects_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "008_Reminder_32x42_72.png");
            this.imageList.Images.SetKeyName(1, "112_ArrowCurve_Blue_Right_32x32_72.png");
            this.imageList.Images.SetKeyName(2, "Stuffed_Folder.png");
            this.imageList.Images.SetKeyName(3, "1446_envelope_stamp_clsd_48.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "FolderHL.bmp");
            this.imageList1.Images.SetKeyName(1, "OpenHL.bmp");
            this.imageList1.Images.SetKeyName(2, "OpenPL.bmp");
            this.imageList1.Images.SetKeyName(3, "TaskHL.bmp");
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerTo.Location = new System.Drawing.Point(609, 319);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(147, 20);
            this.dateTimePickerTo.TabIndex = 7;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerFrom.Location = new System.Drawing.Point(609, 292);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(147, 20);
            this.dateTimePickerFrom.TabIndex = 6;
            // 
            // editStages
            // 
            this.editStages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editStages.Location = new System.Drawing.Point(426, 371);
            this.editStages.Name = "editStages";
            this.editStages.Size = new System.Drawing.Size(135, 31);
            this.editStages.TabIndex = 3;
            this.editStages.Text = "Редактировать этапы";
            this.editStages.UseVisualStyleBackColor = true;
            this.editStages.Click += new System.EventHandler(this.editStages_Click);
            // 
            // UpdateFiltr
            // 
            this.UpdateFiltr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateFiltr.Location = new System.Drawing.Point(681, 345);
            this.UpdateFiltr.Name = "UpdateFiltr";
            this.UpdateFiltr.Size = new System.Drawing.Size(75, 23);
            this.UpdateFiltr.TabIndex = 9;
            this.UpdateFiltr.Text = "Обновить";
            this.UpdateFiltr.UseVisualStyleBackColor = true;
            this.UpdateFiltr.Click += new System.EventHandler(this.UpdateFiltr_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(568, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "До:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(570, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "От:";
            // 
            // checkBoxFiltr
            // 
            this.checkBoxFiltr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxFiltr.AutoSize = true;
            this.checkBoxFiltr.Location = new System.Drawing.Point(580, 349);
            this.checkBoxFiltr.Name = "checkBoxFiltr";
            this.checkBoxFiltr.Size = new System.Drawing.Size(95, 17);
            this.checkBoxFiltr.TabIndex = 8;
            this.checkBoxFiltr.Text = "Фильтровать";
            this.checkBoxFiltr.UseVisualStyleBackColor = true;
            this.checkBoxFiltr.Click += new System.EventHandler(this.checkBoxFiltr_Click);
            // 
            // OpenProject
            // 
            this.OpenProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenProject.Location = new System.Drawing.Point(426, 297);
            this.OpenProject.Name = "OpenProject";
            this.OpenProject.Size = new System.Drawing.Size(135, 31);
            this.OpenProject.TabIndex = 1;
            this.OpenProject.Text = "Открыть проект";
            this.OpenProject.UseVisualStyleBackColor = true;
            this.OpenProject.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // EditProject
            // 
            this.EditProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditProject.Location = new System.Drawing.Point(426, 334);
            this.EditProject.Name = "EditProject";
            this.EditProject.Size = new System.Drawing.Size(135, 31);
            this.EditProject.TabIndex = 2;
            this.EditProject.Text = "Редактировать проект";
            this.EditProject.UseVisualStyleBackColor = true;
            this.EditProject.Click += new System.EventHandler(this.EditProject_Click);
            // 
            // InsertNewProject
            // 
            this.InsertNewProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.InsertNewProject.Location = new System.Drawing.Point(426, 408);
            this.InsertNewProject.Name = "InsertNewProject";
            this.InsertNewProject.Size = new System.Drawing.Size(135, 31);
            this.InsertNewProject.TabIndex = 4;
            this.InsertNewProject.Text = "Добавить проект";
            this.InsertNewProject.UseVisualStyleBackColor = true;
            this.InsertNewProject.Click += new System.EventHandler(this.InsertNewProject_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(426, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(330, 274);
            this.textBox1.TabIndex = 8;
            this.textBox1.TabStop = false;
            // 
            // CopyFromProject
            // 
            this.CopyFromProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyFromProject.Location = new System.Drawing.Point(567, 408);
            this.CopyFromProject.Name = "CopyFromProject";
            this.CopyFromProject.Size = new System.Drawing.Size(185, 31);
            this.CopyFromProject.TabIndex = 5;
            this.CopyFromProject.Text = "Копировать выделенный проект";
            this.CopyFromProject.UseVisualStyleBackColor = true;
            this.CopyFromProject.Click += new System.EventHandler(this.CopyFromProject_Click);
            // 
            // ProjectsForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 451);
            this.Controls.Add(this.CopyFromProject);
            this.Controls.Add(this.UpdateFiltr);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.checkBoxFiltr);
            this.Controls.Add(this.treeViewProjects);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editStages);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InsertNewProject);
            this.Controls.Add(this.EditProject);
            this.Controls.Add(this.OpenProject);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectsForm2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Проекты";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewProjects;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button EditProject;
        private System.Windows.Forms.Button InsertNewProject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxFiltr;
        private System.Windows.Forms.Button OpenProject;
        private System.Windows.Forms.Button UpdateFiltr;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button editStages;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Button CopyFromProject;
    }
}