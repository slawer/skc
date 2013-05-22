namespace SKC
{
    partial class KRSForm
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxPanels = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.digitDisplay1 = new DisplayComponent.DigitDisplay();
            this.graphicsSheet1 = new GraphicComponent.GraphicsSheet();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(494, 639);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown1.TabIndex = 19;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(385, 642);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Время обновления";
            // 
            // comboBoxPanels
            // 
            this.comboBoxPanels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxPanels.FormattingEnabled = true;
            this.comboBoxPanels.Location = new System.Drawing.Point(162, 639);
            this.comboBoxPanels.Name = "comboBoxPanels";
            this.comboBoxPanels.Size = new System.Drawing.Size(217, 21);
            this.comboBoxPanels.TabIndex = 11;
            this.comboBoxPanels.SelectedIndexChanged += new System.EventHandler(this.comboBoxPanels_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 642);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Отображаемая панель";
            // 
            // digitDisplay1
            // 
            this.digitDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.digitDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.digitDisplay1.Location = new System.Drawing.Point(12, 12);
            this.digitDisplay1.Name = "digitDisplay1";
            this.digitDisplay1.Size = new System.Drawing.Size(225, 621);
            this.digitDisplay1.TabIndex = 1;
            // 
            // graphicsSheet1
            // 
            this.graphicsSheet1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.graphicsSheet1.BackColor = System.Drawing.SystemColors.Window;
            this.graphicsSheet1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphicsSheet1.Location = new System.Drawing.Point(243, 12);
            this.graphicsSheet1.Name = "graphicsSheet1";
            this.graphicsSheet1.Size = new System.Drawing.Size(668, 621);
            this.graphicsSheet1.TabIndex = 0;
            // 
            // KRSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 672);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxPanels);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.digitDisplay1);
            this.Controls.Add(this.graphicsSheet1);
            this.MinimumSize = new System.Drawing.Size(300, 400);
            this.Name = "KRSForm";
            this.Text = "Регистратор";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KRSForm_FormClosing);
            this.Load += new System.EventHandler(this.KRSForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphicComponent.GraphicsSheet graphicsSheet1;
        private DisplayComponent.DigitDisplay digitDisplay1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxPanels;
        private System.Windows.Forms.Label label1;
    }
}