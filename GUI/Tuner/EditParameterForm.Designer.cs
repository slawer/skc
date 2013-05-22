namespace SKC
{
    partial class EditParameterForm
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
            this.textBoxParameterName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxParameterDesc = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectChannel = new System.Windows.Forms.Button();
            this.textBoxParameterChannelName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxControlBlocking = new System.Windows.Forms.CheckBox();
            this.checkBoxControlAlarm = new System.Windows.Forms.CheckBox();
            this.textBoxAlarmValue = new System.Windows.Forms.TextBox();
            this.textBoxBlockingValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxParameterUnits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMininumValue = new System.Windows.Forms.TextBox();
            this.textBoxMaximumValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxIsSaveToDB = new System.Windows.Forms.CheckBox();
            this.textBoxPorogToDB = new System.Windows.Forms.TextBox();
            this.numericUpDownIntervalToSaveToDB = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntervalToSaveToDB)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя параметра";
            // 
            // textBoxParameterName
            // 
            this.textBoxParameterName.Location = new System.Drawing.Point(105, 26);
            this.textBoxParameterName.Name = "textBoxParameterName";
            this.textBoxParameterName.Size = new System.Drawing.Size(255, 20);
            this.textBoxParameterName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(366, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Описатель";
            // 
            // textBoxParameterDesc
            // 
            this.textBoxParameterDesc.Location = new System.Drawing.Point(434, 26);
            this.textBoxParameterDesc.Name = "textBoxParameterDesc";
            this.textBoxParameterDesc.Size = new System.Drawing.Size(100, 20);
            this.textBoxParameterDesc.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectChannel);
            this.groupBox1.Controls.Add(this.textBoxParameterChannelName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.checkBoxControlBlocking);
            this.groupBox1.Controls.Add(this.checkBoxControlAlarm);
            this.groupBox1.Controls.Add(this.textBoxAlarmValue);
            this.groupBox1.Controls.Add(this.textBoxBlockingValue);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBoxParameterUnits);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxMininumValue);
            this.groupBox1.Controls.Add(this.textBoxMaximumValue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(15, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 191);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Общие настройки параметра";
            // 
            // selectChannel
            // 
            this.selectChannel.Location = new System.Drawing.Point(488, 27);
            this.selectChannel.Name = "selectChannel";
            this.selectChannel.Size = new System.Drawing.Size(25, 20);
            this.selectChannel.TabIndex = 2;
            this.selectChannel.Text = "...";
            this.selectChannel.UseVisualStyleBackColor = true;
            this.selectChannel.Click += new System.EventHandler(this.selectChannel_Click);
            // 
            // textBoxParameterChannelName
            // 
            this.textBoxParameterChannelName.Location = new System.Drawing.Point(107, 28);
            this.textBoxParameterChannelName.Name = "textBoxParameterChannelName";
            this.textBoxParameterChannelName.ReadOnly = true;
            this.textBoxParameterChannelName.Size = new System.Drawing.Size(375, 20);
            this.textBoxParameterChannelName.TabIndex = 13;
            this.textBoxParameterChannelName.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Сигнал с датчика";
            // 
            // checkBoxControlBlocking
            // 
            this.checkBoxControlBlocking.AutoSize = true;
            this.checkBoxControlBlocking.Location = new System.Drawing.Point(186, 159);
            this.checkBoxControlBlocking.Name = "checkBoxControlBlocking";
            this.checkBoxControlBlocking.Size = new System.Drawing.Size(109, 17);
            this.checkBoxControlBlocking.TabIndex = 9;
            this.checkBoxControlBlocking.Text = "Контролировать";
            this.checkBoxControlBlocking.UseVisualStyleBackColor = true;
            // 
            // checkBoxControlAlarm
            // 
            this.checkBoxControlAlarm.AutoSize = true;
            this.checkBoxControlAlarm.Location = new System.Drawing.Point(186, 133);
            this.checkBoxControlAlarm.Name = "checkBoxControlAlarm";
            this.checkBoxControlAlarm.Size = new System.Drawing.Size(109, 17);
            this.checkBoxControlAlarm.TabIndex = 7;
            this.checkBoxControlAlarm.Text = "Контролировать";
            this.checkBoxControlAlarm.UseVisualStyleBackColor = true;
            // 
            // textBoxAlarmValue
            // 
            this.textBoxAlarmValue.Location = new System.Drawing.Point(100, 131);
            this.textBoxAlarmValue.Name = "textBoxAlarmValue";
            this.textBoxAlarmValue.Size = new System.Drawing.Size(80, 20);
            this.textBoxAlarmValue.TabIndex = 6;
            this.textBoxAlarmValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxBlockingValue
            // 
            this.textBoxBlockingValue.Location = new System.Drawing.Point(100, 157);
            this.textBoxBlockingValue.Name = "textBoxBlockingValue";
            this.textBoxBlockingValue.Size = new System.Drawing.Size(80, 20);
            this.textBoxBlockingValue.TabIndex = 8;
            this.textBoxBlockingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Блокировочное";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Аварийное";
            // 
            // comboBoxParameterUnits
            // 
            this.comboBoxParameterUnits.FormattingEnabled = true;
            this.comboBoxParameterUnits.Items.AddRange(new object[] {
            "т",
            "кг",
            "г",
            "кг/см2",
            "атм",
            "г/см3",
            "л/сек",
            "м3",
            "град",
            "°С",
            "см",
            "м",
            "сек",
            "%",
            "кГм",
            "тМ ",
            "кНм",
            "об/мин",
            "м3/мин",
            "л",
            "мм",
            "мин",
            "час",
            "м/сек",
            "м/час",
            "мин/м",
            "Тс",
            "кГ",
            "Н",
            "кН",
            "Единицы измерения не определены"});
            this.comboBoxParameterUnits.Location = new System.Drawing.Point(296, 88);
            this.comboBoxParameterUnits.Name = "comboBoxParameterUnits";
            this.comboBoxParameterUnits.Size = new System.Drawing.Size(217, 21);
            this.comboBoxParameterUnits.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(293, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Единицы измерения параметра";
            // 
            // textBoxMininumValue
            // 
            this.textBoxMininumValue.Location = new System.Drawing.Point(207, 63);
            this.textBoxMininumValue.Name = "textBoxMininumValue";
            this.textBoxMininumValue.Size = new System.Drawing.Size(80, 20);
            this.textBoxMininumValue.TabIndex = 3;
            this.textBoxMininumValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxMaximumValue
            // 
            this.textBoxMaximumValue.Location = new System.Drawing.Point(207, 89);
            this.textBoxMaximumValue.Name = "textBoxMaximumValue";
            this.textBoxMaximumValue.Size = new System.Drawing.Size(80, 20);
            this.textBoxMaximumValue.TabIndex = 4;
            this.textBoxMaximumValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Максимальное значение параметра";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Минимальное значение параметра";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxIsSaveToDB);
            this.groupBox2.Controls.Add(this.textBoxPorogToDB);
            this.groupBox2.Controls.Add(this.numericUpDownIntervalToSaveToDB);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(15, 249);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(519, 90);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "База данных";
            // 
            // checkBoxIsSaveToDB
            // 
            this.checkBoxIsSaveToDB.AutoSize = true;
            this.checkBoxIsSaveToDB.Location = new System.Drawing.Point(236, 29);
            this.checkBoxIsSaveToDB.Name = "checkBoxIsSaveToDB";
            this.checkBoxIsSaveToDB.Size = new System.Drawing.Size(206, 17);
            this.checkBoxIsSaveToDB.TabIndex = 12;
            this.checkBoxIsSaveToDB.Text = "Сохранять параметр в базу данных";
            this.checkBoxIsSaveToDB.UseVisualStyleBackColor = true;
            // 
            // textBoxPorogToDB
            // 
            this.textBoxPorogToDB.Location = new System.Drawing.Point(133, 54);
            this.textBoxPorogToDB.Name = "textBoxPorogToDB";
            this.textBoxPorogToDB.Size = new System.Drawing.Size(80, 20);
            this.textBoxPorogToDB.TabIndex = 11;
            // 
            // numericUpDownIntervalToSaveToDB
            // 
            this.numericUpDownIntervalToSaveToDB.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownIntervalToSaveToDB.Location = new System.Drawing.Point(133, 28);
            this.numericUpDownIntervalToSaveToDB.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownIntervalToSaveToDB.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownIntervalToSaveToDB.Name = "numericUpDownIntervalToSaveToDB";
            this.numericUpDownIntervalToSaveToDB.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownIntervalToSaveToDB.TabIndex = 10;
            this.numericUpDownIntervalToSaveToDB.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Порог для записи";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Интервал записи (мс)";
            // 
            // accept
            // 
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(378, 345);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 13;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(459, 345);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 14;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // EditParameterForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(545, 384);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxParameterDesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxParameterName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditParameterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование параметра";
            this.Load += new System.EventHandler(this.EditParameterForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntervalToSaveToDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxParameterName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxParameterDesc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxParameterUnits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMininumValue;
        private System.Windows.Forms.TextBox textBoxMaximumValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxControlBlocking;
        private System.Windows.Forms.CheckBox checkBoxControlAlarm;
        private System.Windows.Forms.TextBox textBoxAlarmValue;
        private System.Windows.Forms.TextBox textBoxBlockingValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button selectChannel;
        private System.Windows.Forms.TextBox textBoxParameterChannelName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxIsSaveToDB;
        private System.Windows.Forms.TextBox textBoxPorogToDB;
        private System.Windows.Forms.NumericUpDown numericUpDownIntervalToSaveToDB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
    }
}