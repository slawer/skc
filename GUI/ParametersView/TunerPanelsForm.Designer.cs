namespace SKC
{
    partial class TunerPanelsForm
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
            this.listViewPanels = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonEditP5 = new System.Windows.Forms.Button();
            this.buttonEditP4 = new System.Windows.Forms.Button();
            this.buttonEditP3 = new System.Windows.Forms.Button();
            this.buttonEditP2 = new System.Windows.Forms.Button();
            this.buttonEditP1 = new System.Windows.Forms.Button();
            this.buttonP5 = new System.Windows.Forms.Button();
            this.buttonP4 = new System.Windows.Forms.Button();
            this.buttonP3 = new System.Windows.Forms.Button();
            this.buttonP2 = new System.Windows.Forms.Button();
            this.buttonP1 = new System.Windows.Forms.Button();
            this.textBoxP5 = new System.Windows.Forms.TextBox();
            this.textBoxP4 = new System.Windows.Forms.TextBox();
            this.textBoxP3 = new System.Windows.Forms.TextBox();
            this.textBoxP2 = new System.Windows.Forms.TextBox();
            this.textBoxP1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonInsertNewPanel = new System.Windows.Forms.Button();
            this.buttonRemovePanel = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.buttonClearOptions = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewPanels
            // 
            this.listViewPanels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewPanels.FullRowSelect = true;
            this.listViewPanels.GridLines = true;
            this.listViewPanels.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPanels.Location = new System.Drawing.Point(12, 12);
            this.listViewPanels.Name = "listViewPanels";
            this.listViewPanels.Size = new System.Drawing.Size(577, 300);
            this.listViewPanels.TabIndex = 0;
            this.listViewPanels.UseCompatibleStateImageBehavior = false;
            this.listViewPanels.View = System.Windows.Forms.View.Details;
            this.listViewPanels.SelectedIndexChanged += new System.EventHandler(this.listViewPanels_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 46;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Описание панели";
            this.columnHeader2.Width = 502;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Описание панели";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(124, 318);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(465, 20);
            this.textBoxDescription.TabIndex = 2;
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxDescription_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonEditP5);
            this.groupBox1.Controls.Add(this.buttonEditP4);
            this.groupBox1.Controls.Add(this.buttonEditP3);
            this.groupBox1.Controls.Add(this.buttonEditP2);
            this.groupBox1.Controls.Add(this.buttonEditP1);
            this.groupBox1.Controls.Add(this.buttonP5);
            this.groupBox1.Controls.Add(this.buttonP4);
            this.groupBox1.Controls.Add(this.buttonP3);
            this.groupBox1.Controls.Add(this.buttonP2);
            this.groupBox1.Controls.Add(this.buttonP1);
            this.groupBox1.Controls.Add(this.textBoxP5);
            this.groupBox1.Controls.Add(this.textBoxP4);
            this.groupBox1.Controls.Add(this.textBoxP3);
            this.groupBox1.Controls.Add(this.textBoxP2);
            this.groupBox1.Controls.Add(this.textBoxP1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 167);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройка отображаемых параметров";
            // 
            // buttonEditP5
            // 
            this.buttonEditP5.Location = new System.Drawing.Point(357, 129);
            this.buttonEditP5.Name = "buttonEditP5";
            this.buttonEditP5.Size = new System.Drawing.Size(92, 20);
            this.buttonEditP5.TabIndex = 19;
            this.buttonEditP5.Text = "Редактировать";
            this.buttonEditP5.UseVisualStyleBackColor = true;
            this.buttonEditP5.Click += new System.EventHandler(this.buttonEditP5_Click);
            // 
            // buttonEditP4
            // 
            this.buttonEditP4.Location = new System.Drawing.Point(357, 103);
            this.buttonEditP4.Name = "buttonEditP4";
            this.buttonEditP4.Size = new System.Drawing.Size(92, 20);
            this.buttonEditP4.TabIndex = 18;
            this.buttonEditP4.Text = "Редактировать";
            this.buttonEditP4.UseVisualStyleBackColor = true;
            this.buttonEditP4.Click += new System.EventHandler(this.buttonEditP4_Click);
            // 
            // buttonEditP3
            // 
            this.buttonEditP3.Location = new System.Drawing.Point(357, 77);
            this.buttonEditP3.Name = "buttonEditP3";
            this.buttonEditP3.Size = new System.Drawing.Size(92, 20);
            this.buttonEditP3.TabIndex = 17;
            this.buttonEditP3.Text = "Редактировать";
            this.buttonEditP3.UseVisualStyleBackColor = true;
            this.buttonEditP3.Click += new System.EventHandler(this.buttonEditP3_Click);
            // 
            // buttonEditP2
            // 
            this.buttonEditP2.Location = new System.Drawing.Point(357, 51);
            this.buttonEditP2.Name = "buttonEditP2";
            this.buttonEditP2.Size = new System.Drawing.Size(92, 20);
            this.buttonEditP2.TabIndex = 16;
            this.buttonEditP2.Text = "Редактировать";
            this.buttonEditP2.UseVisualStyleBackColor = true;
            this.buttonEditP2.Click += new System.EventHandler(this.buttonEditP2_Click);
            // 
            // buttonEditP1
            // 
            this.buttonEditP1.Location = new System.Drawing.Point(357, 25);
            this.buttonEditP1.Name = "buttonEditP1";
            this.buttonEditP1.Size = new System.Drawing.Size(92, 20);
            this.buttonEditP1.TabIndex = 15;
            this.buttonEditP1.Text = "Редактировать";
            this.buttonEditP1.UseVisualStyleBackColor = true;
            this.buttonEditP1.Click += new System.EventHandler(this.buttonEditP1_Click);
            // 
            // buttonP5
            // 
            this.buttonP5.Location = new System.Drawing.Point(324, 129);
            this.buttonP5.Name = "buttonP5";
            this.buttonP5.Size = new System.Drawing.Size(27, 20);
            this.buttonP5.TabIndex = 14;
            this.buttonP5.Text = "...";
            this.buttonP5.UseVisualStyleBackColor = true;
            this.buttonP5.Click += new System.EventHandler(this.buttonP5_Click);
            // 
            // buttonP4
            // 
            this.buttonP4.Location = new System.Drawing.Point(324, 103);
            this.buttonP4.Name = "buttonP4";
            this.buttonP4.Size = new System.Drawing.Size(27, 20);
            this.buttonP4.TabIndex = 13;
            this.buttonP4.Text = "...";
            this.buttonP4.UseVisualStyleBackColor = true;
            this.buttonP4.Click += new System.EventHandler(this.buttonP4_Click);
            // 
            // buttonP3
            // 
            this.buttonP3.Location = new System.Drawing.Point(324, 77);
            this.buttonP3.Name = "buttonP3";
            this.buttonP3.Size = new System.Drawing.Size(27, 20);
            this.buttonP3.TabIndex = 12;
            this.buttonP3.Text = "...";
            this.buttonP3.UseVisualStyleBackColor = true;
            this.buttonP3.Click += new System.EventHandler(this.buttonP3_Click);
            // 
            // buttonP2
            // 
            this.buttonP2.Location = new System.Drawing.Point(324, 51);
            this.buttonP2.Name = "buttonP2";
            this.buttonP2.Size = new System.Drawing.Size(27, 20);
            this.buttonP2.TabIndex = 11;
            this.buttonP2.Text = "...";
            this.buttonP2.UseVisualStyleBackColor = true;
            this.buttonP2.Click += new System.EventHandler(this.buttonP2_Click);
            // 
            // buttonP1
            // 
            this.buttonP1.Location = new System.Drawing.Point(324, 25);
            this.buttonP1.Name = "buttonP1";
            this.buttonP1.Size = new System.Drawing.Size(27, 20);
            this.buttonP1.TabIndex = 10;
            this.buttonP1.Text = "...";
            this.buttonP1.UseVisualStyleBackColor = true;
            this.buttonP1.Click += new System.EventHandler(this.buttonP1_Click);
            // 
            // textBoxP5
            // 
            this.textBoxP5.Location = new System.Drawing.Point(130, 129);
            this.textBoxP5.Name = "textBoxP5";
            this.textBoxP5.ReadOnly = true;
            this.textBoxP5.Size = new System.Drawing.Size(188, 20);
            this.textBoxP5.TabIndex = 9;
            this.textBoxP5.TabStop = false;
            // 
            // textBoxP4
            // 
            this.textBoxP4.Location = new System.Drawing.Point(130, 103);
            this.textBoxP4.Name = "textBoxP4";
            this.textBoxP4.ReadOnly = true;
            this.textBoxP4.Size = new System.Drawing.Size(188, 20);
            this.textBoxP4.TabIndex = 8;
            this.textBoxP4.TabStop = false;
            // 
            // textBoxP3
            // 
            this.textBoxP3.Location = new System.Drawing.Point(130, 77);
            this.textBoxP3.Name = "textBoxP3";
            this.textBoxP3.ReadOnly = true;
            this.textBoxP3.Size = new System.Drawing.Size(188, 20);
            this.textBoxP3.TabIndex = 7;
            this.textBoxP3.TabStop = false;
            // 
            // textBoxP2
            // 
            this.textBoxP2.Location = new System.Drawing.Point(130, 51);
            this.textBoxP2.Name = "textBoxP2";
            this.textBoxP2.ReadOnly = true;
            this.textBoxP2.Size = new System.Drawing.Size(188, 20);
            this.textBoxP2.TabIndex = 6;
            this.textBoxP2.TabStop = false;
            // 
            // textBoxP1
            // 
            this.textBoxP1.Location = new System.Drawing.Point(130, 25);
            this.textBoxP1.Name = "textBoxP1";
            this.textBoxP1.ReadOnly = true;
            this.textBoxP1.Size = new System.Drawing.Size(188, 20);
            this.textBoxP1.TabIndex = 5;
            this.textBoxP1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Пятый параметр";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Четвертый параметр";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Третий параметр";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Второй параметр";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Первый параметр";
            // 
            // buttonInsertNewPanel
            // 
            this.buttonInsertNewPanel.Location = new System.Drawing.Point(479, 362);
            this.buttonInsertNewPanel.Name = "buttonInsertNewPanel";
            this.buttonInsertNewPanel.Size = new System.Drawing.Size(108, 23);
            this.buttonInsertNewPanel.TabIndex = 4;
            this.buttonInsertNewPanel.Text = "Добавить панель";
            this.buttonInsertNewPanel.UseVisualStyleBackColor = true;
            this.buttonInsertNewPanel.Click += new System.EventHandler(this.buttonInsertNewPanel_Click);
            // 
            // buttonRemovePanel
            // 
            this.buttonRemovePanel.Location = new System.Drawing.Point(479, 391);
            this.buttonRemovePanel.Name = "buttonRemovePanel";
            this.buttonRemovePanel.Size = new System.Drawing.Size(108, 23);
            this.buttonRemovePanel.TabIndex = 5;
            this.buttonRemovePanel.Text = "Удалить панель";
            this.buttonRemovePanel.UseVisualStyleBackColor = true;
            this.buttonRemovePanel.Click += new System.EventHandler(this.buttonRemovePanel_Click);
            // 
            // close
            // 
            this.close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close.Location = new System.Drawing.Point(514, 488);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 7;
            this.close.Text = "Закрыть";
            this.close.UseVisualStyleBackColor = true;
            // 
            // buttonClearOptions
            // 
            this.buttonClearOptions.Location = new System.Drawing.Point(479, 420);
            this.buttonClearOptions.Name = "buttonClearOptions";
            this.buttonClearOptions.Size = new System.Drawing.Size(108, 23);
            this.buttonClearOptions.TabIndex = 6;
            this.buttonClearOptions.Text = "Очистить";
            this.buttonClearOptions.UseVisualStyleBackColor = true;
            this.buttonClearOptions.Click += new System.EventHandler(this.buttonClearOptions_Click);
            // 
            // TunerPanelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.close;
            this.ClientSize = new System.Drawing.Size(601, 522);
            this.Controls.Add(this.buttonClearOptions);
            this.Controls.Add(this.close);
            this.Controls.Add(this.buttonRemovePanel);
            this.Controls.Add(this.buttonInsertNewPanel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewPanels);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TunerPanelsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка панелей отображения параметров";
            this.Load += new System.EventHandler(this.TunerPanelsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewPanels;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonP5;
        private System.Windows.Forms.Button buttonP4;
        private System.Windows.Forms.Button buttonP3;
        private System.Windows.Forms.Button buttonP2;
        private System.Windows.Forms.Button buttonP1;
        private System.Windows.Forms.TextBox textBoxP5;
        private System.Windows.Forms.TextBox textBoxP4;
        private System.Windows.Forms.TextBox textBoxP3;
        private System.Windows.Forms.TextBox textBoxP2;
        private System.Windows.Forms.TextBox textBoxP1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonInsertNewPanel;
        private System.Windows.Forms.Button buttonRemovePanel;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button buttonEditP5;
        private System.Windows.Forms.Button buttonEditP4;
        private System.Windows.Forms.Button buttonEditP3;
        private System.Windows.Forms.Button buttonEditP2;
        private System.Windows.Forms.Button buttonEditP1;
        private System.Windows.Forms.Button buttonClearOptions;
    }
}