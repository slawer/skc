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
            this.menuItem3Minits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem5Minits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem10minits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem15minits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem30minits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem1hours = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.отображатьГрафикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вертикальноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.горизонтальноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ччвчвToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.отрисовыватьЧислаНаШкалеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.режимПросмотраЗначенийПараметровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuItem3Minits,
            this.menuItem5Minits,
            this.menuItem10minits,
            this.menuItem15minits,
            this.menuItem30minits,
            this.menuItem1hours,
            this.toolStripMenuItem1,
            this.отображатьГрафикиToolStripMenuItem,
            this.ччвчвToolStripMenuItem,
            this.отрисовыватьЧислаНаШкалеToolStripMenuItem,
            this.режимПросмотраЗначенийПараметровToolStripMenuItem});
            this.contextMenuIntervalInCell.Name = "contextMenuIntervalInCell";
            this.contextMenuIntervalInCell.Size = new System.Drawing.Size(246, 340);
            this.contextMenuIntervalInCell.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuIntervalInCell_Opening);
            // 
            // menuItemSecond
            // 
            this.menuItemSecond.Name = "menuItemSecond";
            this.menuItemSecond.Size = new System.Drawing.Size(244, 22);
            this.menuItemSecond.Text = "1 cекунда в одной клетке";
            this.menuItemSecond.Visible = false;
            this.menuItemSecond.Click += new System.EventHandler(this.dedeToolStripMenuItem_Click);
            // 
            // menuItem10Second
            // 
            this.menuItem10Second.Name = "menuItem10Second";
            this.menuItem10Second.Size = new System.Drawing.Size(244, 22);
            this.menuItem10Second.Text = "10 секунд в одной клетке";
            this.menuItem10Second.Click += new System.EventHandler(this.edToolStripMenuItem_Click);
            // 
            // menuItem30Second
            // 
            this.menuItem30Second.Name = "menuItem30Second";
            this.menuItem30Second.Size = new System.Drawing.Size(244, 22);
            this.menuItem30Second.Text = "30 секунд в клетке";
            this.menuItem30Second.Click += new System.EventHandler(this.секундВКленткеToolStripMenuItem_Click);
            // 
            // menuItem1Minits
            // 
            this.menuItem1Minits.Name = "menuItem1Minits";
            this.menuItem1Minits.Size = new System.Drawing.Size(244, 22);
            this.menuItem1Minits.Text = "1 минута в клетке";
            this.menuItem1Minits.Click += new System.EventHandler(this.минутаВКлеткеToolStripMenuItem_Click);
            // 
            // menuItem3Minits
            // 
            this.menuItem3Minits.Name = "menuItem3Minits";
            this.menuItem3Minits.Size = new System.Drawing.Size(244, 22);
            this.menuItem3Minits.Text = "3 минуты в клетке";
            this.menuItem3Minits.Click += new System.EventHandler(this.минутыВКлеткеToolStripMenuItem_Click);
            // 
            // menuItem5Minits
            // 
            this.menuItem5Minits.Name = "menuItem5Minits";
            this.menuItem5Minits.Size = new System.Drawing.Size(244, 22);
            this.menuItem5Minits.Text = "5 минут в клетке";
            this.menuItem5Minits.Click += new System.EventHandler(this.минутВКлеткеToolStripMenuItem_Click_1);
            // 
            // menuItem10minits
            // 
            this.menuItem10minits.Name = "menuItem10minits";
            this.menuItem10minits.Size = new System.Drawing.Size(244, 22);
            this.menuItem10minits.Text = "10 минут в клетке";
            this.menuItem10minits.Click += new System.EventHandler(this.минутВКлеткеToolStripMenuItem_Click);
            // 
            // menuItem15minits
            // 
            this.menuItem15minits.Name = "menuItem15minits";
            this.menuItem15minits.Size = new System.Drawing.Size(244, 22);
            this.menuItem15minits.Text = "15 минут в клетке";
            this.menuItem15minits.Click += new System.EventHandler(this.минутВКлеткеToolStripMenuItem1_Click);
            // 
            // menuItem30minits
            // 
            this.menuItem30minits.Name = "menuItem30minits";
            this.menuItem30minits.Size = new System.Drawing.Size(244, 22);
            this.menuItem30minits.Text = "30 минут в клетке";
            this.menuItem30minits.Click += new System.EventHandler(this.минутВКлеткеToolStripMenuItem2_Click);
            // 
            // menuItem1hours
            // 
            this.menuItem1hours.Name = "menuItem1hours";
            this.menuItem1hours.Size = new System.Drawing.Size(244, 22);
            this.menuItem1hours.Text = "1 час в клетке";
            this.menuItem1hours.Click += new System.EventHandler(this.часВКлеткеToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(241, 6);
            // 
            // отображатьГрафикиToolStripMenuItem
            // 
            this.отображатьГрафикиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вертикальноToolStripMenuItem,
            this.горизонтальноToolStripMenuItem});
            this.отображатьГрафикиToolStripMenuItem.Name = "отображатьГрафикиToolStripMenuItem";
            this.отображатьГрафикиToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.отображатьГрафикиToolStripMenuItem.Text = "Отображать графики";
            this.отображатьГрафикиToolStripMenuItem.DropDownOpened += new System.EventHandler(this.отображатьГрафикиToolStripMenuItem_DropDownOpened);
            // 
            // вертикальноToolStripMenuItem
            // 
            this.вертикальноToolStripMenuItem.Name = "вертикальноToolStripMenuItem";
            this.вертикальноToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.вертикальноToolStripMenuItem.Text = "Вертикально";
            this.вертикальноToolStripMenuItem.Click += new System.EventHandler(this.вертикальноToolStripMenuItem_Click);
            // 
            // горизонтальноToolStripMenuItem
            // 
            this.горизонтальноToolStripMenuItem.Name = "горизонтальноToolStripMenuItem";
            this.горизонтальноToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.горизонтальноToolStripMenuItem.Text = "Горизонтально";
            this.горизонтальноToolStripMenuItem.Click += new System.EventHandler(this.горизонтальноToolStripMenuItem_Click);
            // 
            // ччвчвToolStripMenuItem
            // 
            this.ччвчвToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.ччвчвToolStripMenuItem.Name = "ччвчвToolStripMenuItem";
            this.ччвчвToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.ччвчвToolStripMenuItem.Text = "Количество делений сетки";
            this.ччвчвToolStripMenuItem.DropDownOpening += new System.EventHandler(this.ччвчвToolStripMenuItem_DropDownOpening);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // отрисовыватьЧислаНаШкалеToolStripMenuItem
            // 
            this.отрисовыватьЧислаНаШкалеToolStripMenuItem.Name = "отрисовыватьЧислаНаШкалеToolStripMenuItem";
            this.отрисовыватьЧислаНаШкалеToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.отрисовыватьЧислаНаШкалеToolStripMenuItem.Text = "Отрисовывать числа на шкале";
            this.отрисовыватьЧислаНаШкалеToolStripMenuItem.Click += new System.EventHandler(this.отрисовыватьЧислаНаШкалеToolStripMenuItem_Click);
            // 
            // режимПросмотраЗначенийПараметровToolStripMenuItem
            // 
            this.режимПросмотраЗначенийПараметровToolStripMenuItem.Name = "режимПросмотраЗначенийПараметровToolStripMenuItem";
            this.режимПросмотраЗначенийПараметровToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.режимПросмотраЗначенийПараметровToolStripMenuItem.Text = "Режим просмотра параметров";
            this.режимПросмотраЗначенийПараметровToolStripMenuItem.Click += new System.EventHandler(this.режимПросмотраЗначенийПараметровToolStripMenuItem_Click);
            // 
            // GraphicsSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuIntervalInCell;
            this.Name = "GraphicsSheet";
            this.Size = new System.Drawing.Size(341, 644);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphicsSheet_MouseMove);
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
        private System.Windows.Forms.ToolStripMenuItem menuItem3Minits;
        private System.Windows.Forms.ToolStripMenuItem menuItem5Minits;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem отображатьГрафикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вертикальноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem горизонтальноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ччвчвToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem отрисовыватьЧислаНаШкалеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem режимПросмотраЗначенийПараметровToolStripMenuItem;




    }
}
