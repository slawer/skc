namespace SKC
{
    partial class LoadAppForm
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
            this.radioButtonCurrentWork = new System.Windows.Forms.RadioButton();
            this.radioButtonNewWork = new System.Windows.Forms.RadioButton();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.radioButtonNewWithStages = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectWork = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButtonCurrentWork
            // 
            this.radioButtonCurrentWork.AutoSize = true;
            this.radioButtonCurrentWork.Location = new System.Drawing.Point(14, 97);
            this.radioButtonCurrentWork.Name = "radioButtonCurrentWork";
            this.radioButtonCurrentWork.Size = new System.Drawing.Size(172, 17);
            this.radioButtonCurrentWork.TabIndex = 0;
            this.radioButtonCurrentWork.Text = "Продолжить текущую работу";
            this.radioButtonCurrentWork.UseVisualStyleBackColor = true;
            // 
            // radioButtonNewWork
            // 
            this.radioButtonNewWork.AutoSize = true;
            this.radioButtonNewWork.Checked = true;
            this.radioButtonNewWork.Location = new System.Drawing.Point(14, 28);
            this.radioButtonNewWork.Name = "radioButtonNewWork";
            this.radioButtonNewWork.Size = new System.Drawing.Size(132, 17);
            this.radioButtonNewWork.TabIndex = 1;
            this.radioButtonNewWork.TabStop = true;
            this.radioButtonNewWork.Text = "Начать новую работу";
            this.radioButtonNewWork.UseVisualStyleBackColor = true;
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(77, 142);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 2;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(158, 142);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonNewWithStages
            // 
            this.radioButtonNewWithStages.AutoSize = true;
            this.radioButtonNewWithStages.Location = new System.Drawing.Point(14, 51);
            this.radioButtonNewWithStages.Name = "radioButtonNewWithStages";
            this.radioButtonNewWithStages.Size = new System.Drawing.Size(222, 17);
            this.radioButtonNewWithStages.TabIndex = 4;
            this.radioButtonNewWithStages.TabStop = true;
            this.radioButtonNewWithStages.Text = "Создать работу на основе имеющейся";
            this.radioButtonNewWithStages.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelectWork
            // 
            this.radioButtonSelectWork.AutoSize = true;
            this.radioButtonSelectWork.Location = new System.Drawing.Point(14, 74);
            this.radioButtonSelectWork.Name = "radioButtonSelectWork";
            this.radioButtonSelectWork.Size = new System.Drawing.Size(186, 17);
            this.radioButtonSelectWork.TabIndex = 5;
            this.radioButtonSelectWork.TabStop = true;
            this.radioButtonSelectWork.Text = "Выбрать заготовленную работу";
            this.radioButtonSelectWork.UseVisualStyleBackColor = true;
            // 
            // LoadAppForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(245, 177);
            this.Controls.Add(this.radioButtonSelectWork);
            this.Controls.Add(this.radioButtonNewWithStages);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.radioButtonNewWork);
            this.Controls.Add(this.radioButtonCurrentWork);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadAppForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Начало работы";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        public System.Windows.Forms.RadioButton radioButtonNewWork;
        public System.Windows.Forms.RadioButton radioButtonCurrentWork;
        public System.Windows.Forms.RadioButton radioButtonNewWithStages;
        public System.Windows.Forms.RadioButton radioButtonSelectWork;
    }
}