namespace SKC
{
    partial class EditParameterPanelForm
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
            this.textBoxParameterDescription = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxMininumValue = new System.Windows.Forms.TextBox();
            this.textBoxMaximumValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonParameterColor = new System.Windows.Forms.Button();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Описание параметра";
            // 
            // textBoxParameterDescription
            // 
            this.textBoxParameterDescription.Location = new System.Drawing.Point(133, 27);
            this.textBoxParameterDescription.Name = "textBoxParameterDescription";
            this.textBoxParameterDescription.ReadOnly = true;
            this.textBoxParameterDescription.Size = new System.Drawing.Size(281, 20);
            this.textBoxParameterDescription.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonParameterColor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxMininumValue);
            this.groupBox1.Controls.Add(this.textBoxMaximumValue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(15, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 122);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // textBoxMininumValue
            // 
            this.textBoxMininumValue.Location = new System.Drawing.Point(216, 24);
            this.textBoxMininumValue.Name = "textBoxMininumValue";
            this.textBoxMininumValue.Size = new System.Drawing.Size(80, 20);
            this.textBoxMininumValue.TabIndex = 7;
            this.textBoxMininumValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxMaximumValue
            // 
            this.textBoxMaximumValue.Location = new System.Drawing.Point(216, 50);
            this.textBoxMaximumValue.Name = "textBoxMaximumValue";
            this.textBoxMaximumValue.Size = new System.Drawing.Size(80, 20);
            this.textBoxMaximumValue.TabIndex = 6;
            this.textBoxMaximumValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Максимальное значение параметра";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Минимальное значение параметра";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Цвет отрисовки параметра";
            // 
            // buttonParameterColor
            // 
            this.buttonParameterColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonParameterColor.Location = new System.Drawing.Point(216, 76);
            this.buttonParameterColor.Name = "buttonParameterColor";
            this.buttonParameterColor.Size = new System.Drawing.Size(20, 20);
            this.buttonParameterColor.TabIndex = 9;
            this.buttonParameterColor.UseVisualStyleBackColor = true;
            this.buttonParameterColor.Click += new System.EventHandler(this.buttonParameterColor_Click);
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(258, 192);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 3;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(339, 192);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // EditParameterPanelForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(430, 227);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxParameterDescription);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditParameterPanelForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование отображаемого на панели параметра";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxParameterDescription;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxMininumValue;
        private System.Windows.Forms.TextBox textBoxMaximumValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonParameterColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}