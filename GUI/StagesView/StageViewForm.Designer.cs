namespace SKC
{
    partial class StageViewForm
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
            this.graphicsSheet1 = new GraphicComponent.GraphicsSheet();
            this.listViewParameters = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStartTime = new System.Windows.Forms.TextBox();
            this.textBoxFinishTime = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // graphicsSheet1
            // 
            this.graphicsSheet1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.graphicsSheet1.BackColor = System.Drawing.SystemColors.Window;
            this.graphicsSheet1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphicsSheet1.Location = new System.Drawing.Point(12, 12);
            this.graphicsSheet1.Name = "graphicsSheet1";
            this.graphicsSheet1.ScrollHorizontal = null;
            this.graphicsSheet1.ScrollVertical = null;
            this.graphicsSheet1.Size = new System.Drawing.Size(771, 576);
            this.graphicsSheet1.TabIndex = 0;
            // 
            // listViewParameters
            // 
            this.listViewParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewParameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewParameters.FullRowSelect = true;
            this.listViewParameters.GridLines = true;
            this.listViewParameters.Location = new System.Drawing.Point(12, 594);
            this.listViewParameters.Name = "listViewParameters";
            this.listViewParameters.Size = new System.Drawing.Size(498, 124);
            this.listViewParameters.TabIndex = 1;
            this.listViewParameters.UseCompatibleStateImageBehavior = false;
            this.listViewParameters.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Параметр";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Минимальное";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 83;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Максимальное";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 89;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Среднее";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 81;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Плановое";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader5.Width = 79;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(516, 606);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Время начала этапа";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(516, 632);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Время завершения этапа";
            // 
            // textBoxStartTime
            // 
            this.textBoxStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxStartTime.Location = new System.Drawing.Point(659, 603);
            this.textBoxStartTime.Name = "textBoxStartTime";
            this.textBoxStartTime.ReadOnly = true;
            this.textBoxStartTime.Size = new System.Drawing.Size(124, 20);
            this.textBoxStartTime.TabIndex = 4;
            this.textBoxStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxFinishTime
            // 
            this.textBoxFinishTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFinishTime.Location = new System.Drawing.Point(659, 629);
            this.textBoxFinishTime.Name = "textBoxFinishTime";
            this.textBoxFinishTime.ReadOnly = true;
            this.textBoxFinishTime.Size = new System.Drawing.Size(124, 20);
            this.textBoxFinishTime.TabIndex = 5;
            this.textBoxFinishTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StageViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 730);
            this.Controls.Add(this.textBoxFinishTime);
            this.Controls.Add(this.textBoxStartTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewParameters);
            this.Controls.Add(this.graphicsSheet1);
            this.Name = "StageViewForm";
            this.Text = "Просмотр этапа:";
            this.Load += new System.EventHandler(this.StageViewForm_Load);
            this.Shown += new System.EventHandler(this.StageViewForm_Shown);
            this.SizeChanged += new System.EventHandler(this.StageViewForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphicComponent.GraphicsSheet graphicsSheet1;
        private System.Windows.Forms.ListView listViewParameters;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxStartTime;
        private System.Windows.Forms.TextBox textBoxFinishTime;
    }
}