namespace SKC
{
    partial class ParametersViewForm
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
            this.comboBoxPanels = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.graphicsSheet1 = new GraphicComponent.GraphicsSheet();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 633);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Отображаемая панель";
            // 
            // comboBoxPanels
            // 
            this.comboBoxPanels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxPanels.FormattingEnabled = true;
            this.comboBoxPanels.Location = new System.Drawing.Point(162, 630);
            this.comboBoxPanels.Name = "comboBoxPanels";
            this.comboBoxPanels.Size = new System.Drawing.Size(236, 21);
            this.comboBoxPanels.TabIndex = 1;
            this.comboBoxPanels.SelectedIndexChanged += new System.EventHandler(this.comboBoxPanels_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(404, 630);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 5;
            this.button1.Text = "Обновить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.graphicsSheet1.Size = new System.Drawing.Size(764, 612);
            this.graphicsSheet1.TabIndex = 0;
            // 
            // ParametersViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 663);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxPanels);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.graphicsSheet1);
            this.Name = "ParametersViewForm";
            this.Text = "Просмотр параметров";
            this.Load += new System.EventHandler(this.ParametersViewForm_Load);
            this.SizeChanged += new System.EventHandler(this.ParametersViewForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphicComponent.GraphicsSheet graphicsSheet1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPanels;
        private System.Windows.Forms.Button button1;
    }
}