namespace SKC
{
    partial class TunerForm
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
            this.listViewParameters = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.editParameter = new System.Windows.Forms.Button();
            this.techParameters = new System.Windows.Forms.Button();
            this.buttonResetterOfVolume = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewParameters
            // 
            this.listViewParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewParameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewParameters.FullRowSelect = true;
            this.listViewParameters.GridLines = true;
            this.listViewParameters.HideSelection = false;
            this.listViewParameters.Location = new System.Drawing.Point(12, 12);
            this.listViewParameters.Name = "listViewParameters";
            this.listViewParameters.Size = new System.Drawing.Size(639, 412);
            this.listViewParameters.TabIndex = 0;
            this.listViewParameters.UseCompatibleStateImageBehavior = false;
            this.listViewParameters.View = System.Windows.Forms.View.Details;
            this.listViewParameters.DoubleClick += new System.EventHandler(this.listViewParameters_DoubleClick);
            this.listViewParameters.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewParameters_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Параметр";
            this.columnHeader2.Width = 371;
            // 
            // editParameter
            // 
            this.editParameter.Location = new System.Drawing.Point(12, 430);
            this.editParameter.Name = "editParameter";
            this.editParameter.Size = new System.Drawing.Size(158, 23);
            this.editParameter.TabIndex = 1;
            this.editParameter.Text = "Редактировать параметр";
            this.editParameter.UseVisualStyleBackColor = true;
            this.editParameter.Click += new System.EventHandler(this.editParameter_Click);
            // 
            // techParameters
            // 
            this.techParameters.Location = new System.Drawing.Point(176, 429);
            this.techParameters.Name = "techParameters";
            this.techParameters.Size = new System.Drawing.Size(184, 23);
            this.techParameters.TabIndex = 2;
            this.techParameters.Text = "Технологические параметры";
            this.techParameters.UseVisualStyleBackColor = true;
            this.techParameters.Click += new System.EventHandler(this.techParameters_Click);
            // 
            // buttonResetterOfVolume
            // 
            this.buttonResetterOfVolume.Location = new System.Drawing.Point(366, 429);
            this.buttonResetterOfVolume.Name = "buttonResetterOfVolume";
            this.buttonResetterOfVolume.Size = new System.Drawing.Size(171, 23);
            this.buttonResetterOfVolume.TabIndex = 3;
            this.buttonResetterOfVolume.Text = "Настройка сброса объема";
            this.buttonResetterOfVolume.UseVisualStyleBackColor = true;
            this.buttonResetterOfVolume.Click += new System.EventHandler(this.buttonResetterOfVolume_Click);
            // 
            // TunerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 464);
            this.Controls.Add(this.buttonResetterOfVolume);
            this.Controls.Add(this.techParameters);
            this.Controls.Add(this.editParameter);
            this.Controls.Add(this.listViewParameters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TunerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка параметров";
            this.Load += new System.EventHandler(this.TunerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewParameters;
        private System.Windows.Forms.Button editParameter;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button techParameters;
        private System.Windows.Forms.Button buttonResetterOfVolume;
    }
}