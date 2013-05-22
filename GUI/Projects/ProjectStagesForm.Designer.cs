namespace SKC
{
    partial class ProjectStagesForm
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
            this.listViewStages = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addNewStage = new System.Windows.Forms.Button();
            this.removeStage = new System.Windows.Forms.Button();
            this.editStage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewStages
            // 
            this.listViewStages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewStages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewStages.FullRowSelect = true;
            this.listViewStages.GridLines = true;
            this.listViewStages.Location = new System.Drawing.Point(12, 12);
            this.listViewStages.Name = "listViewStages";
            this.listViewStages.Size = new System.Drawing.Size(509, 371);
            this.listViewStages.TabIndex = 0;
            this.listViewStages.UseCompatibleStateImageBehavior = false;
            this.listViewStages.View = System.Windows.Forms.View.Details;
            this.listViewStages.DoubleClick += new System.EventHandler(this.editStage_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 38;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Название этапа";
            this.columnHeader2.Width = 169;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Поправочный коэффициент";
            this.columnHeader3.Width = 159;
            // 
            // addNewStage
            // 
            this.addNewStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addNewStage.Location = new System.Drawing.Point(12, 389);
            this.addNewStage.Name = "addNewStage";
            this.addNewStage.Size = new System.Drawing.Size(122, 23);
            this.addNewStage.TabIndex = 1;
            this.addNewStage.Text = "Добавить этап";
            this.addNewStage.UseVisualStyleBackColor = true;
            this.addNewStage.Click += new System.EventHandler(this.addNewStage_Click);
            // 
            // removeStage
            // 
            this.removeStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeStage.Location = new System.Drawing.Point(140, 389);
            this.removeStage.Name = "removeStage";
            this.removeStage.Size = new System.Drawing.Size(122, 23);
            this.removeStage.TabIndex = 2;
            this.removeStage.Text = "Удалить этап";
            this.removeStage.UseVisualStyleBackColor = true;
            this.removeStage.Click += new System.EventHandler(this.removeStage_Click);
            // 
            // editStage
            // 
            this.editStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editStage.Location = new System.Drawing.Point(268, 389);
            this.editStage.Name = "editStage";
            this.editStage.Size = new System.Drawing.Size(122, 23);
            this.editStage.TabIndex = 3;
            this.editStage.Text = "Редактировать этап";
            this.editStage.UseVisualStyleBackColor = true;
            this.editStage.Click += new System.EventHandler(this.editStage_Click);
            // 
            // ProjectStagesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 424);
            this.Controls.Add(this.editStage);
            this.Controls.Add(this.removeStage);
            this.Controls.Add(this.addNewStage);
            this.Controls.Add(this.listViewStages);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectStagesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Этапы работы проекта";
            this.Load += new System.EventHandler(this.ProjectStagesForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewStages;
        private System.Windows.Forms.Button addNewStage;
        private System.Windows.Forms.Button removeStage;
        private System.Windows.Forms.Button editStage;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}