namespace SKC
{
    partial class InsertStageForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxStageName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.textBoxKoef = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPlanRashod = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxPlanPlotnost = new System.Windows.Forms.TextBox();
            this.textBoxPlanDavlenie = new System.Windows.Forms.TextBox();
            this.textBoxPlanObem = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название этапа работы";
            // 
            // textBoxStageName
            // 
            this.textBoxStageName.Location = new System.Drawing.Point(145, 23);
            this.textBoxStageName.Name = "textBoxStageName";
            this.textBoxStageName.Size = new System.Drawing.Size(257, 20);
            this.textBoxStageName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Поправочный коэффициент";
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(246, 215);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 3;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(327, 215);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // textBoxKoef
            // 
            this.textBoxKoef.Location = new System.Drawing.Point(174, 159);
            this.textBoxKoef.Name = "textBoxKoef";
            this.textBoxKoef.Size = new System.Drawing.Size(58, 20);
            this.textBoxKoef.TabIndex = 2;
            this.textBoxKoef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Объем на этапе";
            // 
            // textBoxPlanRashod
            // 
            this.textBoxPlanRashod.Location = new System.Drawing.Point(123, 28);
            this.textBoxPlanRashod.Name = "textBoxPlanRashod";
            this.textBoxPlanRashod.Size = new System.Drawing.Size(65, 20);
            this.textBoxPlanRashod.TabIndex = 1;
            this.textBoxPlanRashod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Плотность на этапе";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Расход на этапе";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Давление на этапе";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxPlanPlotnost);
            this.groupBox1.Controls.Add(this.textBoxPlanDavlenie);
            this.groupBox1.Controls.Add(this.textBoxPlanObem);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxPlanRashod);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 95);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Плановые значения технологических параметров на этапе";
            // 
            // textBoxPlanPlotnost
            // 
            this.textBoxPlanPlotnost.Location = new System.Drawing.Point(308, 54);
            this.textBoxPlanPlotnost.Name = "textBoxPlanPlotnost";
            this.textBoxPlanPlotnost.Size = new System.Drawing.Size(65, 20);
            this.textBoxPlanPlotnost.TabIndex = 4;
            this.textBoxPlanPlotnost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxPlanDavlenie
            // 
            this.textBoxPlanDavlenie.Location = new System.Drawing.Point(123, 54);
            this.textBoxPlanDavlenie.Name = "textBoxPlanDavlenie";
            this.textBoxPlanDavlenie.Size = new System.Drawing.Size(65, 20);
            this.textBoxPlanDavlenie.TabIndex = 3;
            this.textBoxPlanDavlenie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxPlanObem
            // 
            this.textBoxPlanObem.Location = new System.Drawing.Point(309, 28);
            this.textBoxPlanObem.Name = "textBoxPlanObem";
            this.textBoxPlanObem.Size = new System.Drawing.Size(64, 20);
            this.textBoxPlanObem.TabIndex = 2;
            this.textBoxPlanObem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // InsertStageForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(414, 250);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxKoef);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxStageName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertStageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить этап в проект";
            this.Load += new System.EventHandler(this.InsertStageForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxStageName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.TextBox textBoxKoef;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPlanRashod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxPlanPlotnost;
        private System.Windows.Forms.TextBox textBoxPlanDavlenie;
        private System.Windows.Forms.TextBox textBoxPlanObem;
    }
}