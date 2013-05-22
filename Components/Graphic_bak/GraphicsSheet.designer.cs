namespace GraphicComponent
{
    partial class GraphicsSheet
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuIntervalInCell = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemSecond = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem10Second = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem30Second = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem1Minits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem10minits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem15minits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem30minits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem1hours = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuIntervalInCell.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuIntervalInCell
            // 
            this.contextMenuIntervalInCell.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSecond,
            this.menuItem10Second,
            this.menuItem30Second,
            this.menuItem1Minits,
            this.menuItem10minits,
            this.menuItem15minits,
            this.menuItem30minits,
            this.menuItem1hours});
            this.contextMenuIntervalInCell.Name = "contextMenuIntervalInCell";
            this.contextMenuIntervalInCell.Size = new System.Drawing.Size(212, 202);
            this.contextMenuIntervalInCell.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuIntervalInCell_Opening);
            // 
            // menuItemSecond
            // 
            this.menuItemSecond.Name = "menuItemSecond";
            this.menuItemSecond.Size = new System.Drawing.Size(211, 22);
            this.menuItemSecond.Text = "1 cекунда в одной клетке";
            this.menuItemSecond.Click += new System.EventHandler(this.dedeToolStripMenuItem_Click);
            // 
            // menuItem10Second
            // 
            this.menuItem10Second.Name = "menuItem10Second";
            this.menuItem10Second.Size = new System.Drawing.Size(211, 22);
            this.menuItem10Second.Text = "10 секунд в одной клетке";
            this.menuItem10Second.Click += new System.EventHandler(this.edToolStripMenuItem_Click);
            // 
            // menuItem30Second
            // 
            this.menuItem30Second.Name = "menuItem30Second";
            this.menuItem30Second.Size = new System.Drawing.Size(211, 22);
            this.menuItem30Second.Text = "30 секунд в клентке";
            this.menuItem30Second.Click += new System.EventHandler(this.секундВКленткеToolStripMenuItem_Click);
            // 
            // menuItem1Minits
            // 
            this.menuItem1Minits.Name = "menuItem1Minits";
            this.menuItem1Minits.Size = new System.Drawing.Size(211, 22);
            this.menuItem1Minits.Text = "1 минута в клетке";
            this.menuItem1Minits.Click += new System.EventHandler(this.минутаВКлеткеToolStripMenuItem_Click);
            // 
            // menuItem10minits
            // 
            this.menuItem10minits.Name = "menuItem10minits";
            this.menuItem10minits.Size = new System.Drawing.Size(211, 22);
            this.menuItem10minits.Text = "10 минут в клетке";
            this.menuItem10minits.Click += new System.EventHandler(this.минутВКлеткеToolStripMenuItem_Click);
            // 
            // menuItem15minits
            // 
            this.menuItem15minits.Name = "menuItem15minits";
            this.menuItem15minits.Size = new System.Drawing.Size(211, 22);
            this.menuItem15minits.Text = "15 минут в клетке";
            this.menuItem15minits.Click += new System.EventHandler(this.минутВКлеткеToolStripMenuItem1_Click);
            // 
            // menuItem30minits
            // 
            this.menuItem30minits.Name = "menuItem30minits";
            this.menuItem30minits.Size = new System.Drawing.Size(211, 22);
            this.menuItem30minits.Text = "30 минут в клетке";
            this.menuItem30minits.Click += new System.EventHandler(this.минутВКлеткеToolStripMenuItem2_Click);
            // 
            // menuItem1hours
            // 
            this.menuItem1hours.Name = "menuItem1hours";
            this.menuItem1hours.Size = new System.Drawing.Size(211, 22);
            this.menuItem1hours.Text = "1 час в клетке";
            this.menuItem1hours.Click += new System.EventHandler(this.часВКлеткеToolStripMenuItem_Click);
            // 
            // GraphicsSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuIntervalInCell;
            this.Name = "GraphicsSheet";
            this.Size = new System.Drawing.Size(398, 644);
            this.contextMenuIntervalInCell.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuIntervalInCell;
        private System.Windows.Forms.ToolStripMenuItem menuItemSecond;
        private System.Windows.Forms.ToolStripMenuItem menuItem10Second;
        private System.Windows.Forms.ToolStripMenuItem menuItem30Second;
        private System.Windows.Forms.ToolStripMenuItem menuItem1Minits;
        private System.Windows.Forms.ToolStripMenuItem menuItem10minits;
        private System.Windows.Forms.ToolStripMenuItem menuItem15minits;
        private System.Windows.Forms.ToolStripMenuItem menuItem30minits;
        private System.Windows.Forms.ToolStripMenuItem menuItem1hours;




    }
}
