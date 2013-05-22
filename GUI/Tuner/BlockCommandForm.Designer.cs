namespace SKC
{
    partial class BlockCommandForm
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
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.checkBoxuseForReset = new System.Windows.Forms.CheckBox();
            this.checkBoxuseForNext = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(304, 79);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 0;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(385, 79);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Команда DSN";
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Location = new System.Drawing.Point(96, 23);
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(364, 20);
            this.textBoxCommand.TabIndex = 3;
            // 
            // checkBoxuseForReset
            // 
            this.checkBoxuseForReset.AutoSize = true;
            this.checkBoxuseForReset.Location = new System.Drawing.Point(27, 49);
            this.checkBoxuseForReset.Name = "checkBoxuseForReset";
            this.checkBoxuseForReset.Size = new System.Drawing.Size(201, 17);
            this.checkBoxuseForReset.TabIndex = 4;
            this.checkBoxuseForReset.Text = "Использовать для сброса объема";
            this.checkBoxuseForReset.UseVisualStyleBackColor = true;
            // 
            // checkBoxuseForNext
            // 
            this.checkBoxuseForNext.AutoSize = true;
            this.checkBoxuseForNext.Location = new System.Drawing.Point(27, 72);
            this.checkBoxuseForNext.Name = "checkBoxuseForNext";
            this.checkBoxuseForNext.Size = new System.Drawing.Size(246, 17);
            this.checkBoxuseForNext.TabIndex = 5;
            this.checkBoxuseForNext.Text = "Использовать для перехода на новый этап";
            this.checkBoxuseForNext.UseVisualStyleBackColor = true;
            // 
            // BlockCommandForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(472, 114);
            this.Controls.Add(this.checkBoxuseForNext);
            this.Controls.Add(this.checkBoxuseForReset);
            this.Controls.Add(this.textBoxCommand);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BlockCommandForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCommand;
        private System.Windows.Forms.CheckBox checkBoxuseForReset;
        private System.Windows.Forms.CheckBox checkBoxuseForNext;
    }
}