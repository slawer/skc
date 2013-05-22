namespace SKC
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проектыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.текущаяРаботаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.закрытьПрограммуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сервисToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рапортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрБДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.регистраторToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрПараметровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.калибровкаПараметраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.сброситьОбъемToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поправичныйКоэффициентToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаПанелейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настрокаТехнологииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureParameters = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаРасходомеровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаСоединенияСБДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаГрафическогоТаблоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаСоединенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.разблокировкаПараметровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxProccessVolume = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TtextBoxPressure = new System.Windows.Forms.TextBox();
            this.TtextBoxСonsumption = new System.Windows.Forms.TextBox();
            this.TtextBoxTemperature = new System.Windows.Forms.TextBox();
            this.TtextBoxVolume = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TtextBoxDensity = new System.Windows.Forms.TextBox();
            this.checkBoxRgrTurner = new System.Windows.Forms.CheckBox();
            this.buttonStartWork = new System.Windows.Forms.Button();
            this.buttonFinishWork = new System.Windows.Forms.Button();
            this.buttonNewStage = new System.Windows.Forms.Button();
            this.buttonRaport = new System.Windows.Forms.Button();
            this.buttonCorrectKoef = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelDevManStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDBStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelWorkState = new System.Windows.Forms.ToolStripStatusLabel();
            this.listViewStages = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.редактироватьЭтапыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerToDBSaver = new System.Windows.Forms.Timer(this.components);
            this.timerCheckerForParameters = new System.Windows.Forms.Timer(this.components);
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxKoefs = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelCurrentDateTimeDay = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.graphicsSheet1 = new GraphicComponent.GraphicsSheet();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.сервисToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1024, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.проектыToolStripMenuItem,
            this.текущаяРаботаToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.закрытьПрограммуToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // проектыToolStripMenuItem
            // 
            this.проектыToolStripMenuItem.Name = "проектыToolStripMenuItem";
            this.проектыToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.проектыToolStripMenuItem.Text = "Проекты";
            this.проектыToolStripMenuItem.Click += new System.EventHandler(this.проектыToolStripMenuItem_Click);
            // 
            // текущаяРаботаToolStripMenuItem1
            // 
            this.текущаяРаботаToolStripMenuItem1.Name = "текущаяРаботаToolStripMenuItem1";
            this.текущаяРаботаToolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.текущаяРаботаToolStripMenuItem1.Text = "Текущая работа";
            this.текущаяРаботаToolStripMenuItem1.Click += new System.EventHandler(this.текущаяРаботаToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(183, 6);
            // 
            // закрытьПрограммуToolStripMenuItem
            // 
            this.закрытьПрограммуToolStripMenuItem.Name = "закрытьПрограммуToolStripMenuItem";
            this.закрытьПрограммуToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.закрытьПрограммуToolStripMenuItem.Text = "Закрыть программу";
            this.закрытьПрограммуToolStripMenuItem.Click += new System.EventHandler(this.закрытьПрограммуToolStripMenuItem_Click);
            // 
            // сервисToolStripMenuItem
            // 
            this.сервисToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.рапортToolStripMenuItem,
            this.просмотрБДToolStripMenuItem,
            this.toolStripMenuItem3,
            this.регистраторToolStripMenuItem,
            this.просмотрПараметровToolStripMenuItem,
            this.калибровкаПараметраToolStripMenuItem,
            this.toolStripMenuItem4,
            this.сброситьОбъемToolStripMenuItem,
            this.поправичныйКоэффициентToolStripMenuItem});
            this.сервисToolStripMenuItem.Name = "сервисToolStripMenuItem";
            this.сервисToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.сервисToolStripMenuItem.Text = "Сервис";
            // 
            // рапортToolStripMenuItem
            // 
            this.рапортToolStripMenuItem.Name = "рапортToolStripMenuItem";
            this.рапортToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.рапортToolStripMenuItem.Text = "Рапорт";
            this.рапортToolStripMenuItem.Click += new System.EventHandler(this.buttonRaport_Click);
            // 
            // просмотрБДToolStripMenuItem
            // 
            this.просмотрБДToolStripMenuItem.Name = "просмотрБДToolStripMenuItem";
            this.просмотрБДToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.просмотрБДToolStripMenuItem.Text = "Просмотр БД";
            this.просмотрБДToolStripMenuItem.Click += new System.EventHandler(this.просмотрБДToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(229, 6);
            // 
            // регистраторToolStripMenuItem
            // 
            this.регистраторToolStripMenuItem.Name = "регистраторToolStripMenuItem";
            this.регистраторToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.регистраторToolStripMenuItem.Text = "Регистратор";
            this.регистраторToolStripMenuItem.Click += new System.EventHandler(this.регистраторToolStripMenuItem_Click);
            // 
            // просмотрПараметровToolStripMenuItem
            // 
            this.просмотрПараметровToolStripMenuItem.Name = "просмотрПараметровToolStripMenuItem";
            this.просмотрПараметровToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.просмотрПараметровToolStripMenuItem.Text = "Просмотр параметров";
            this.просмотрПараметровToolStripMenuItem.Click += new System.EventHandler(this.просмотрПараметровToolStripMenuItem_Click);
            // 
            // калибровкаПараметраToolStripMenuItem
            // 
            this.калибровкаПараметраToolStripMenuItem.Name = "калибровкаПараметраToolStripMenuItem";
            this.калибровкаПараметраToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.калибровкаПараметраToolStripMenuItem.Text = "Калибровка параметра";
            this.калибровкаПараметраToolStripMenuItem.Click += new System.EventHandler(this.калибровкаПараметраToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(229, 6);
            // 
            // сброситьОбъемToolStripMenuItem
            // 
            this.сброситьОбъемToolStripMenuItem.Name = "сброситьОбъемToolStripMenuItem";
            this.сброситьОбъемToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.сброситьОбъемToolStripMenuItem.Text = "Сбросить объем";
            this.сброситьОбъемToolStripMenuItem.Click += new System.EventHandler(this.сброситьОбъемToolStripMenuItem_Click);
            // 
            // поправичныйКоэффициентToolStripMenuItem
            // 
            this.поправичныйКоэффициентToolStripMenuItem.Name = "поправичныйКоэффициентToolStripMenuItem";
            this.поправичныйКоэффициентToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.поправичныйКоэффициентToolStripMenuItem.Text = "Поправочный коэффициент";
            this.поправичныйКоэффициентToolStripMenuItem.Click += new System.EventHandler(this.buttonCorrectKoef_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкаПанелейToolStripMenuItem,
            this.настрокаТехнологииToolStripMenuItem,
            this.configureParameters,
            this.настройкаРасходомеровToolStripMenuItem,
            this.настройкаСоединенияСБДToolStripMenuItem,
            this.настройкаГрафическогоТаблоToolStripMenuItem,
            this.настройкаСоединенияToolStripMenuItem,
            this.toolStripMenuItem1,
            this.разблокировкаПараметровToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // настройкаПанелейToolStripMenuItem
            // 
            this.настройкаПанелейToolStripMenuItem.Name = "настройкаПанелейToolStripMenuItem";
            this.настройкаПанелейToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.настройкаПанелейToolStripMenuItem.Text = "Настройка панелей";
            this.настройкаПанелейToolStripMenuItem.Click += new System.EventHandler(this.настройкаПанелейToolStripMenuItem_Click);
            // 
            // настрокаТехнологииToolStripMenuItem
            // 
            this.настрокаТехнологииToolStripMenuItem.Name = "настрокаТехнологииToolStripMenuItem";
            this.настрокаТехнологииToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.настрокаТехнологииToolStripMenuItem.Text = "Настрока технологии";
            this.настрокаТехнологииToolStripMenuItem.Click += new System.EventHandler(this.настрокаТехнологииToolStripMenuItem_Click);
            // 
            // configureParameters
            // 
            this.configureParameters.Enabled = false;
            this.configureParameters.Name = "configureParameters";
            this.configureParameters.Size = new System.Drawing.Size(256, 22);
            this.configureParameters.Text = "Настройка параметров";
            this.configureParameters.Click += new System.EventHandler(this.configureParameters_Click);
            // 
            // настройкаРасходомеровToolStripMenuItem
            // 
            this.настройкаРасходомеровToolStripMenuItem.Name = "настройкаРасходомеровToolStripMenuItem";
            this.настройкаРасходомеровToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.настройкаРасходомеровToolStripMenuItem.Text = "Настройка расходомеров";
            this.настройкаРасходомеровToolStripMenuItem.Click += new System.EventHandler(this.настройкаРасходомеровToolStripMenuItem_Click);
            // 
            // настройкаСоединенияСБДToolStripMenuItem
            // 
            this.настройкаСоединенияСБДToolStripMenuItem.Name = "настройкаСоединенияСБДToolStripMenuItem";
            this.настройкаСоединенияСБДToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.настройкаСоединенияСБДToolStripMenuItem.Text = "Настройка соединения с БД";
            this.настройкаСоединенияСБДToolStripMenuItem.Click += new System.EventHandler(this.настройкаСоединенияСБДToolStripMenuItem_Click);
            // 
            // настройкаГрафическогоТаблоToolStripMenuItem
            // 
            this.настройкаГрафическогоТаблоToolStripMenuItem.Name = "настройкаГрафическогоТаблоToolStripMenuItem";
            this.настройкаГрафическогоТаблоToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.настройкаГрафическогоТаблоToolStripMenuItem.Text = "Настройка графического табло";
            this.настройкаГрафическогоТаблоToolStripMenuItem.Click += new System.EventHandler(this.настройкаГрафическогоТаблоToolStripMenuItem_Click);
            // 
            // настройкаСоединенияToolStripMenuItem
            // 
            this.настройкаСоединенияToolStripMenuItem.Name = "настройкаСоединенияToolStripMenuItem";
            this.настройкаСоединенияToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.настройкаСоединенияToolStripMenuItem.Text = "Настройка соединения с devMan";
            this.настройкаСоединенияToolStripMenuItem.Click += new System.EventHandler(this.настройкаСоединенияToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(253, 6);
            // 
            // разблокировкаПараметровToolStripMenuItem
            // 
            this.разблокировкаПараметровToolStripMenuItem.Name = "разблокировкаПараметровToolStripMenuItem";
            this.разблокировкаПараметровToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.разблокировкаПараметровToolStripMenuItem.Text = "Разблокировка параметров";
            this.разблокировкаПараметровToolStripMenuItem.Click += new System.EventHandler(this.разблокировкаПараметровToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem1,
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem1
            // 
            this.справкаToolStripMenuItem1.Name = "справкаToolStripMenuItem1";
            this.справкаToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.справкаToolStripMenuItem1.Text = "Справка";
            this.справкаToolStripMenuItem1.Click += new System.EventHandler(this.справкаToolStripMenuItem1_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе ...";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBoxProccessVolume);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.TtextBoxPressure);
            this.panel1.Controls.Add(this.TtextBoxСonsumption);
            this.panel1.Controls.Add(this.TtextBoxTemperature);
            this.panel1.Controls.Add(this.TtextBoxVolume);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.TtextBoxDensity);
            this.panel1.Location = new System.Drawing.Point(12, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 83);
            this.panel1.TabIndex = 2;
            // 
            // textBoxProccessVolume
            // 
            this.textBoxProccessVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxProccessVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxProccessVolume.Location = new System.Drawing.Point(505, 42);
            this.textBoxProccessVolume.Name = "textBoxProccessVolume";
            this.textBoxProccessVolume.ReadOnly = true;
            this.textBoxProccessVolume.Size = new System.Drawing.Size(69, 26);
            this.textBoxProccessVolume.TabIndex = 22;
            this.textBoxProccessVolume.TabStop = false;
            this.textBoxProccessVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(10, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 15);
            this.label9.TabIndex = 12;
            this.label9.Text = "Объем этапа [м3]";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(388, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 15);
            this.label13.TabIndex = 18;
            this.label13.Text = "Объем общий [м3]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(199, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 15);
            this.label11.TabIndex = 14;
            this.label11.Text = "Давление [атм]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(10, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "Расход [л/сек]";
            // 
            // TtextBoxPressure
            // 
            this.TtextBoxPressure.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TtextBoxPressure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TtextBoxPressure.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TtextBoxPressure.Location = new System.Drawing.Point(313, 43);
            this.TtextBoxPressure.Name = "TtextBoxPressure";
            this.TtextBoxPressure.ReadOnly = true;
            this.TtextBoxPressure.Size = new System.Drawing.Size(69, 26);
            this.TtextBoxPressure.TabIndex = 19;
            this.TtextBoxPressure.TabStop = false;
            this.TtextBoxPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TtextBoxСonsumption
            // 
            this.TtextBoxСonsumption.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TtextBoxСonsumption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TtextBoxСonsumption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TtextBoxСonsumption.Location = new System.Drawing.Point(124, 10);
            this.TtextBoxСonsumption.Name = "TtextBoxСonsumption";
            this.TtextBoxСonsumption.ReadOnly = true;
            this.TtextBoxСonsumption.Size = new System.Drawing.Size(69, 26);
            this.TtextBoxСonsumption.TabIndex = 16;
            this.TtextBoxСonsumption.TabStop = false;
            this.TtextBoxСonsumption.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TtextBoxTemperature
            // 
            this.TtextBoxTemperature.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TtextBoxTemperature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TtextBoxTemperature.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TtextBoxTemperature.Location = new System.Drawing.Point(505, 11);
            this.TtextBoxTemperature.Name = "TtextBoxTemperature";
            this.TtextBoxTemperature.ReadOnly = true;
            this.TtextBoxTemperature.Size = new System.Drawing.Size(69, 26);
            this.TtextBoxTemperature.TabIndex = 20;
            this.TtextBoxTemperature.TabStop = false;
            this.TtextBoxTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TtextBoxVolume
            // 
            this.TtextBoxVolume.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TtextBoxVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TtextBoxVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TtextBoxVolume.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.TtextBoxVolume.Location = new System.Drawing.Point(124, 42);
            this.TtextBoxVolume.Name = "TtextBoxVolume";
            this.TtextBoxVolume.ReadOnly = true;
            this.TtextBoxVolume.Size = new System.Drawing.Size(69, 26);
            this.TtextBoxVolume.TabIndex = 17;
            this.TtextBoxVolume.TabStop = false;
            this.TtextBoxVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(199, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 15);
            this.label10.TabIndex = 13;
            this.label10.Text = "Плотность [г/см3]";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(388, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 15);
            this.label12.TabIndex = 15;
            this.label12.Text = "Температура [С]";
            // 
            // TtextBoxDensity
            // 
            this.TtextBoxDensity.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TtextBoxDensity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TtextBoxDensity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TtextBoxDensity.Location = new System.Drawing.Point(313, 11);
            this.TtextBoxDensity.Name = "TtextBoxDensity";
            this.TtextBoxDensity.ReadOnly = true;
            this.TtextBoxDensity.Size = new System.Drawing.Size(69, 26);
            this.TtextBoxDensity.TabIndex = 18;
            this.TtextBoxDensity.TabStop = false;
            this.TtextBoxDensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxRgrTurner
            // 
            this.checkBoxRgrTurner.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxRgrTurner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxRgrTurner.Enabled = false;
            this.checkBoxRgrTurner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxRgrTurner.Location = new System.Drawing.Point(309, 141);
            this.checkBoxRgrTurner.Name = "checkBoxRgrTurner";
            this.checkBoxRgrTurner.Size = new System.Drawing.Size(93, 52);
            this.checkBoxRgrTurner.TabIndex = 19;
            this.checkBoxRgrTurner.Text = "Включить запись";
            this.checkBoxRgrTurner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRgrTurner.UseVisualStyleBackColor = true;
            this.checkBoxRgrTurner.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checkBoxRgrTurner.Click += new System.EventHandler(this.checkBoxRgrTurner_Click);
            // 
            // buttonStartWork
            // 
            this.buttonStartWork.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStartWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStartWork.Location = new System.Drawing.Point(12, 141);
            this.buttonStartWork.Name = "buttonStartWork";
            this.buttonStartWork.Size = new System.Drawing.Size(93, 52);
            this.buttonStartWork.TabIndex = 14;
            this.buttonStartWork.Text = "Начать работу";
            this.buttonStartWork.UseVisualStyleBackColor = true;
            this.buttonStartWork.Click += new System.EventHandler(this.buttonStartWork_Click);
            // 
            // buttonFinishWork
            // 
            this.buttonFinishWork.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFinishWork.Enabled = false;
            this.buttonFinishWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFinishWork.Location = new System.Drawing.Point(210, 141);
            this.buttonFinishWork.Name = "buttonFinishWork";
            this.buttonFinishWork.Size = new System.Drawing.Size(93, 52);
            this.buttonFinishWork.TabIndex = 12;
            this.buttonFinishWork.Text = "Конец работы";
            this.buttonFinishWork.UseVisualStyleBackColor = true;
            this.buttonFinishWork.Click += new System.EventHandler(this.buttonFinishWork_Click);
            // 
            // buttonNewStage
            // 
            this.buttonNewStage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNewStage.Enabled = false;
            this.buttonNewStage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNewStage.Location = new System.Drawing.Point(111, 141);
            this.buttonNewStage.Name = "buttonNewStage";
            this.buttonNewStage.Size = new System.Drawing.Size(93, 52);
            this.buttonNewStage.TabIndex = 13;
            this.buttonNewStage.Text = "Новый этап";
            this.buttonNewStage.UseVisualStyleBackColor = true;
            this.buttonNewStage.Click += new System.EventHandler(this.buttonNewStage_Click);
            // 
            // buttonRaport
            // 
            this.buttonRaport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRaport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRaport.Location = new System.Drawing.Point(507, 141);
            this.buttonRaport.Name = "buttonRaport";
            this.buttonRaport.Size = new System.Drawing.Size(93, 52);
            this.buttonRaport.TabIndex = 35;
            this.buttonRaport.Text = "Рапорт";
            this.buttonRaport.UseVisualStyleBackColor = true;
            this.buttonRaport.Click += new System.EventHandler(this.buttonRaport_Click);
            // 
            // buttonCorrectKoef
            // 
            this.buttonCorrectKoef.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCorrectKoef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCorrectKoef.Location = new System.Drawing.Point(408, 141);
            this.buttonCorrectKoef.Name = "buttonCorrectKoef";
            this.buttonCorrectKoef.Size = new System.Drawing.Size(93, 52);
            this.buttonCorrectKoef.TabIndex = 33;
            this.buttonCorrectKoef.Text = "Поправочный коэффициент";
            this.buttonCorrectKoef.UseVisualStyleBackColor = true;
            this.buttonCorrectKoef.Click += new System.EventHandler(this.buttonCorrectKoef_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelDevManStatus,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelDBStatus,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabelWorkState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 602);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1024, 22);
            this.statusStrip1.TabIndex = 36;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelDevManStatus
            // 
            this.toolStripStatusLabelDevManStatus.Name = "toolStripStatusLabelDevManStatus";
            this.toolStripStatusLabelDevManStatus.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabelDevManStatus.Text = "Соединение не инициализировано";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // toolStripStatusLabelDBStatus
            // 
            this.toolStripStatusLabelDBStatus.Name = "toolStripStatusLabelDBStatus";
            this.toolStripStatusLabelDBStatus.Size = new System.Drawing.Size(216, 17);
            this.toolStripStatusLabelDBStatus.Text = "Связь с SQL сервером не установлена";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // toolStripStatusLabelWorkState
            // 
            this.toolStripStatusLabelWorkState.Name = "toolStripStatusLabelWorkState";
            this.toolStripStatusLabelWorkState.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelWorkState.Text = "Работа остановлена";
            // 
            // listViewStages
            // 
            this.listViewStages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewStages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewStages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader6,
            this.columnHeader10});
            this.listViewStages.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewStages.FullRowSelect = true;
            this.listViewStages.GridLines = true;
            this.listViewStages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewStages.Location = new System.Drawing.Point(606, 52);
            this.listViewStages.MultiSelect = false;
            this.listViewStages.Name = "listViewStages";
            this.listViewStages.Size = new System.Drawing.Size(406, 141);
            this.listViewStages.TabIndex = 38;
            this.listViewStages.UseCompatibleStateImageBehavior = false;
            this.listViewStages.View = System.Windows.Forms.View.Details;
            this.listViewStages.DoubleClick += new System.EventHandler(this.listViewStages_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 38;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Этап";
            this.columnHeader2.Width = 111;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 3;
            this.columnHeader3.Text = "Объем";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 59;
            // 
            // columnHeader4
            // 
            this.columnHeader4.DisplayIndex = 5;
            this.columnHeader4.Text = "П.Расход";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 70;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "П.Объем";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader5.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.DisplayIndex = 7;
            this.columnHeader7.Text = "П.Давление";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader7.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "П.Плотность";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader6.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.DisplayIndex = 2;
            this.columnHeader10.Text = "Начало этапа";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 93;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактироватьЭтапыToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(191, 26);
            // 
            // редактироватьЭтапыToolStripMenuItem
            // 
            this.редактироватьЭтапыToolStripMenuItem.Name = "редактироватьЭтапыToolStripMenuItem";
            this.редактироватьЭтапыToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.редактироватьЭтапыToolStripMenuItem.Text = "Редактировать этапы";
            this.редактироватьЭтапыToolStripMenuItem.Click += new System.EventHandler(this.редактироватьЭтапыToolStripMenuItem_Click);
            // 
            // timerToDBSaver
            // 
            this.timerToDBSaver.Tick += new System.EventHandler(this.timerToDBSaver_Tick);
            // 
            // timerCheckerForParameters
            // 
            this.timerCheckerForParameters.Interval = 1000;
            this.timerCheckerForParameters.Tick += new System.EventHandler(this.timerCheckerForParameters_Tick);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(182, 22);
            this.toolStripLabel1.Text = "  Поправочный коэффициент";
            // 
            // toolStripTextBoxKoefs
            // 
            this.toolStripTextBoxKoefs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripTextBoxKoefs.Name = "toolStripTextBoxKoefs";
            this.toolStripTextBoxKoefs.Size = new System.Drawing.Size(15, 22);
            this.toolStripTextBoxKoefs.Text = "1";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel5.Text = "             ";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(113, 22);
            this.toolStripLabel2.Text = "Время процесса: ";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel3.Text = "00:00:00";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel4.Text = "             ";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(133, 22);
            this.toolStripButton1.Text = "Состояние датчиков";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripLabelCurrentDateTimeDay
            // 
            this.toolStripLabelCurrentDateTimeDay.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelCurrentDateTimeDay.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabelCurrentDateTimeDay.Name = "toolStripLabelCurrentDateTimeDay";
            this.toolStripLabelCurrentDateTimeDay.Size = new System.Drawing.Size(23, 22);
            this.toolStripLabelCurrentDateTimeDay.Text = "---";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBoxKoefs,
            this.toolStripLabel5,
            this.toolStripLabel2,
            this.toolStripLabel3,
            this.toolStripLabel4,
            this.toolStripButton1,
            this.toolStripLabelCurrentDateTimeDay});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1024, 25);
            this.toolStrip1.TabIndex = 39;
            this.toolStrip1.Text = "Этан";
            // 
            // graphicsSheet1
            // 
            this.graphicsSheet1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphicsSheet1.BackColor = System.Drawing.SystemColors.Window;
            this.graphicsSheet1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphicsSheet1.Location = new System.Drawing.Point(0, 199);
            this.graphicsSheet1.Margin = new System.Windows.Forms.Padding(4);
            this.graphicsSheet1.Name = "graphicsSheet1";
            this.graphicsSheet1.ScrollHorizontal = null;
            this.graphicsSheet1.ScrollVertical = null;
            this.graphicsSheet1.Size = new System.Drawing.Size(1024, 400);
            this.graphicsSheet1.TabIndex = 37;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 624);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listViewStages);
            this.Controls.Add(this.graphicsSheet1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.checkBoxRgrTurner);
            this.Controls.Add(this.buttonStartWork);
            this.Controls.Add(this.buttonRaport);
            this.Controls.Add(this.buttonFinishWork);
            this.Controls.Add(this.buttonCorrectKoef);
            this.Controls.Add(this.buttonNewStage);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "СКЦ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxProccessVolume;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TtextBoxTemperature;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox TtextBoxVolume;
        private System.Windows.Forms.TextBox TtextBoxPressure;
        private System.Windows.Forms.TextBox TtextBoxСonsumption;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TtextBoxDensity;
        private System.Windows.Forms.CheckBox checkBoxRgrTurner;
        private System.Windows.Forms.Button buttonStartWork;
        private System.Windows.Forms.Button buttonFinishWork;
        private System.Windows.Forms.Button buttonNewStage;
        private System.Windows.Forms.Button buttonRaport;
        private System.Windows.Forms.Button buttonCorrectKoef;
        private System.Windows.Forms.ToolStripMenuItem сервисToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рапортToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поправичныйКоэффициентToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDevManStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWorkState;
        private System.Windows.Forms.ToolStripMenuItem проектыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureParameters;
        private System.Windows.Forms.ToolStripMenuItem настройкаГрафическогоТаблоToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDBStatus;
        private System.Windows.Forms.ListView listViewStages;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private GraphicComponent.GraphicsSheet graphicsSheet1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Timer timerToDBSaver;
        private System.Windows.Forms.ToolStripMenuItem настройкаСоединенияСБДToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem просмотрПараметровToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаПанелейToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закрытьПрограммуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаСоединенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem разблокировкаПараметровToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem настрокаТехнологииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem регистраторToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem редактироватьЭтапыToolStripMenuItem;
        private System.Windows.Forms.Timer timerCheckerForParameters;
        private System.Windows.Forms.ToolStripMenuItem текущаяРаботаToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem сброситьОбъемToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripTextBoxKoefs;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelCurrentDateTimeDay;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem просмотрБДToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem калибровкаПараметраToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаРасходомеровToolStripMenuItem;
    }
}

