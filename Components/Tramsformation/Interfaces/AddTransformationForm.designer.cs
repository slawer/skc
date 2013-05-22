namespace DeviceManager
{
    partial class AddTransformationForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewCalibrationTable = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.removePoint = new System.Windows.Forms.Button();
            this.insertPoint = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxToTableCalibrated = new System.Windows.Forms.TextBox();
            this.textBoxTotablePhysic = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.accept = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectParameter = new System.Windows.Forms.Button();
            this.textBoxSelectedParameter = new System.Windows.Forms.TextBox();
            this.checkBoxDoScale = new System.Windows.Forms.CheckBox();
            this.fromTo = new System.Windows.Forms.Button();
            this.checkBoxCalculate = new System.Windows.Forms.CheckBox();
            this.textBoxFromDeviceCalibrated = new System.Windows.Forms.TextBox();
            this.textBoxFromDevicePhysic = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.calibrationGraphic = new DeviceManager.CalibrationGraphic();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalibrationTable)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewCalibrationTable
            // 
            this.dataGridViewCalibrationTable.AllowUserToAddRows = false;
            this.dataGridViewCalibrationTable.AllowUserToResizeColumns = false;
            this.dataGridViewCalibrationTable.AllowUserToResizeRows = false;
            this.dataGridViewCalibrationTable.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewCalibrationTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewCalibrationTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewCalibrationTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewCalibrationTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3});
            this.dataGridViewCalibrationTable.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridViewCalibrationTable.Location = new System.Drawing.Point(440, 12);
            this.dataGridViewCalibrationTable.Name = "dataGridViewCalibrationTable";
            this.dataGridViewCalibrationTable.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewCalibrationTable.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCalibrationTable.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridViewCalibrationTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewCalibrationTable.Size = new System.Drawing.Size(206, 267);
            this.dataGridViewCalibrationTable.TabIndex = 22;
            this.dataGridViewCalibrationTable.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewCalibrationTable_CellBeginEdit);
            this.dataGridViewCalibrationTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCalibrationTable_CellEndEdit);
            this.dataGridViewCalibrationTable.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dataGridViewCalibrationTable_CellParsing);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Сигнал";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Значение";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // removePoint
            // 
            this.removePoint.Location = new System.Drawing.Point(543, 371);
            this.removePoint.Name = "removePoint";
            this.removePoint.Size = new System.Drawing.Size(69, 23);
            this.removePoint.TabIndex = 37;
            this.removePoint.Text = "Удалить";
            this.removePoint.UseVisualStyleBackColor = true;
            this.removePoint.Click += new System.EventHandler(this.removePoint_Click);
            // 
            // insertPoint
            // 
            this.insertPoint.Location = new System.Drawing.Point(468, 371);
            this.insertPoint.Name = "insertPoint";
            this.insertPoint.Size = new System.Drawing.Size(69, 23);
            this.insertPoint.TabIndex = 36;
            this.insertPoint.Text = "Добавить";
            this.insertPoint.UseVisualStyleBackColor = true;
            this.insertPoint.Click += new System.EventHandler(this.insertPoint_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxToTableCalibrated);
            this.groupBox3.Controls.Add(this.textBoxTotablePhysic);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(468, 285);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(185, 80);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Данные для таблицы";
            // 
            // textBoxToTableCalibrated
            // 
            this.textBoxToTableCalibrated.Location = new System.Drawing.Point(75, 47);
            this.textBoxToTableCalibrated.Name = "textBoxToTableCalibrated";
            this.textBoxToTableCalibrated.Size = new System.Drawing.Size(100, 20);
            this.textBoxToTableCalibrated.TabIndex = 7;
            this.textBoxToTableCalibrated.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxTotablePhysic
            // 
            this.textBoxTotablePhysic.Location = new System.Drawing.Point(75, 21);
            this.textBoxTotablePhysic.Name = "textBoxTotablePhysic";
            this.textBoxTotablePhysic.Size = new System.Drawing.Size(100, 20);
            this.textBoxTotablePhysic.TabIndex = 6;
            this.textBoxTotablePhysic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Значение";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Сигнал";
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(571, 449);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 38;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Параметр";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectParameter);
            this.groupBox1.Controls.Add(this.textBoxSelectedParameter);
            this.groupBox1.Controls.Add(this.checkBoxDoScale);
            this.groupBox1.Controls.Add(this.fromTo);
            this.groupBox1.Controls.Add(this.checkBoxCalculate);
            this.groupBox1.Controls.Add(this.textBoxFromDeviceCalibrated);
            this.groupBox1.Controls.Add(this.textBoxFromDevicePhysic);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 285);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 138);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            // 
            // selectParameter
            // 
            this.selectParameter.Location = new System.Drawing.Point(417, 23);
            this.selectParameter.Name = "selectParameter";
            this.selectParameter.Size = new System.Drawing.Size(27, 20);
            this.selectParameter.TabIndex = 49;
            this.selectParameter.Text = "...";
            this.selectParameter.UseVisualStyleBackColor = true;
            this.selectParameter.Click += new System.EventHandler(this.selectParameter_Click);
            // 
            // textBoxSelectedParameter
            // 
            this.textBoxSelectedParameter.Location = new System.Drawing.Point(70, 24);
            this.textBoxSelectedParameter.Name = "textBoxSelectedParameter";
            this.textBoxSelectedParameter.ReadOnly = true;
            this.textBoxSelectedParameter.Size = new System.Drawing.Size(341, 20);
            this.textBoxSelectedParameter.TabIndex = 48;
            // 
            // checkBoxDoScale
            // 
            this.checkBoxDoScale.AutoSize = true;
            this.checkBoxDoScale.Location = new System.Drawing.Point(176, 79);
            this.checkBoxDoScale.Name = "checkBoxDoScale";
            this.checkBoxDoScale.Size = new System.Drawing.Size(222, 17);
            this.checkBoxDoScale.TabIndex = 47;
            this.checkBoxDoScale.Text = "Масштабировать без последней точки";
            this.checkBoxDoScale.UseVisualStyleBackColor = true;
            this.checkBoxDoScale.CheckedChanged += new System.EventHandler(this.checkBoxDoScale_CheckedChanged);
            // 
            // fromTo
            // 
            this.fromTo.Location = new System.Drawing.Point(12, 103);
            this.fromTo.Name = "fromTo";
            this.fromTo.Size = new System.Drawing.Size(100, 23);
            this.fromTo.TabIndex = 46;
            this.fromTo.Text = "Зафиксировать";
            this.fromTo.UseVisualStyleBackColor = true;
            this.fromTo.Click += new System.EventHandler(this.fromTo_Click);
            // 
            // checkBoxCalculate
            // 
            this.checkBoxCalculate.AutoSize = true;
            this.checkBoxCalculate.Location = new System.Drawing.Point(176, 53);
            this.checkBoxCalculate.Name = "checkBoxCalculate";
            this.checkBoxCalculate.Size = new System.Drawing.Size(137, 17);
            this.checkBoxCalculate.TabIndex = 45;
            this.checkBoxCalculate.Text = "Включить калибровку";
            this.checkBoxCalculate.UseVisualStyleBackColor = true;
            this.checkBoxCalculate.CheckedChanged += new System.EventHandler(this.checkBoxCalculate_CheckedChanged);
            // 
            // textBoxFromDeviceCalibrated
            // 
            this.textBoxFromDeviceCalibrated.Location = new System.Drawing.Point(70, 77);
            this.textBoxFromDeviceCalibrated.Name = "textBoxFromDeviceCalibrated";
            this.textBoxFromDeviceCalibrated.Size = new System.Drawing.Size(100, 20);
            this.textBoxFromDeviceCalibrated.TabIndex = 44;
            this.textBoxFromDeviceCalibrated.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxFromDevicePhysic
            // 
            this.textBoxFromDevicePhysic.Location = new System.Drawing.Point(70, 51);
            this.textBoxFromDevicePhysic.Name = "textBoxFromDevicePhysic";
            this.textBoxFromDevicePhysic.ReadOnly = true;
            this.textBoxFromDevicePhysic.Size = new System.Drawing.Size(100, 20);
            this.textBoxFromDevicePhysic.TabIndex = 43;
            this.textBoxFromDevicePhysic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Значение";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Сигнал";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(468, 400);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 23);
            this.button2.TabIndex = 38;
            this.button2.Text = "Сбросить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // calibrationGraphic
            // 
            this.calibrationGraphic.Draw = DeviceManager.Draw.PointsOnly;
            this.calibrationGraphic.Location = new System.Drawing.Point(12, 12);
            this.calibrationGraphic.LogicalPixelX = 65535F;
            this.calibrationGraphic.LogicalPixelY = 65535F;
            this.calibrationGraphic.Name = "calibrationGraphic";
            this.calibrationGraphic.Size = new System.Drawing.Size(422, 267);
            this.calibrationGraphic.TabIndex = 0;
            // 
            // AddTransformationForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 482);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.removePoint);
            this.Controls.Add(this.insertPoint);
            this.Controls.Add(this.dataGridViewCalibrationTable);
            this.Controls.Add(this.calibrationGraphic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddTransformationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Кусочно-линейная аппроксимация";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddTransformationForm_FormClosing);
            this.Load += new System.EventHandler(this.AddTransformationForm_Load);
            this.Shown += new System.EventHandler(this.AddTransformationForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalibrationTable)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CalibrationGraphic calibrationGraphic;
        private System.Windows.Forms.DataGridView dataGridViewCalibrationTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button removePoint;
        private System.Windows.Forms.Button insertPoint;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxToTableCalibrated;
        private System.Windows.Forms.TextBox textBoxTotablePhysic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxCalculate;
        private System.Windows.Forms.TextBox textBoxFromDeviceCalibrated;
        private System.Windows.Forms.TextBox textBoxFromDevicePhysic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button fromTo;
        private System.Windows.Forms.CheckBox checkBoxDoScale;
        private System.Windows.Forms.Button selectParameter;
        private System.Windows.Forms.TextBox textBoxSelectedParameter;
        private System.Windows.Forms.Button button2;
    }
}