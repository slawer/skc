using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SKC;

namespace DeviceManager
{
    public partial class AddTransformationForm : Form
    {
        private SKC.Application app = null;                 // констекст
        private Transformation transformation = null;       // калибровка

        private Argument first, second;                     // аргументы функции
        private InsertToText t_inserter = null;             // вывод значений во время калибровки

        private Parameter _parameter;                       // калибруемый параметр

        //private Media media = null;                       // усреднение
        //private Float[] med = null;                       // аргумены усреднения

        private bool needCorrect = false;                   // необходимо корректировать таблицу калибровки
        private bool correctP = true;

        private double oldValue;
        private double newValue;

        private int tempIndex = -1;

        public AddTransformationForm(SKC.Application _app)
        {
            app = _app;
            app.Commutator.onParameterUpdated += new EventHandler(Converter_OnComplete);
            
            InitializeComponent();
            
            transformation = new Transformation();

            transformation.OnInsert += new Transformation.TConditionEventHandle(transformation_OnInsert);
            transformation.OnEdit += new Transformation.TConditionEventHandle(transformation_OnEdit);
            transformation.OnRemove += new Transformation.TConditionEventHandle(transformation_OnRemove);

            transformation.OnClear += new EventHandler(transformation_OnClear);
            transformation.OnError += new Transformation.ErrorEventHandle(transformation_OnError);

            transformation.OnExist += new Transformation.TConditionEventHandle(transformation_OnExist);

            Transformation.TCondition t1 = new Transformation.TCondition();
            Transformation.TCondition t2 = new Transformation.TCondition();

            t1.Result = 0;
            t1.Signal = 0;

            t2.Result = 65535;
            t2.Signal = 65535;

            transformation.Insert(t1);
            transformation.Insert(t2);

            first = new Argument();
            second = new Argument();

            calibrationGraphic.CalculateScale();
            t_inserter = new InsertToText(InserterText);

            /*
            media = new Media();

            media.Args[0].Index = 0;
            media.Args[1].Index = 1;

            med = new Float[2];
            for (int i = 0; i < med.Length; i++)
            {
                med[i] = new Float();
            }
             */ 
        }

        /// <summary>
        /// вывести значения в текстовые поля во время калибровки
        /// </summary>
        /// <param name="physical">Значение с датчика</param>
        /// <param name="calibrated">Откалиброванное значение</param>
        private void InserterText(float physical, float calibrated)
        {
            textBoxFromDevicePhysic.Text = string.Format("{0:F3}", physical);
            textBoxFromDeviceCalibrated.Text = string.Format("{0:F3}", calibrated);
        }

        /// <summary>
        /// Конвертор выполнил обработку данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Converter_OnComplete(object sender, EventArgs e)
        {
            if (checkBoxCalculate.Checked)
            {                
                {
                    transformation.Arg = _parameter.ClearValue;
                    float calibrated = transformation.Calculate();

                    /*
                    if (checkBoxCalcMedia.Checked)
                    {
                        med[0].Value = calibrated;
                        med[1].Value = (float)numericUpDownCountPtMedia.Value;

                        calibrated = media.Calculate(med, med);                        
                    }

                     */

                    calibrationGraphic.InsertPoint(new PointF(transformation.Arg, calibrated));

                    Invoke(t_inserter, transformation.Arg, calibrated);
                    calibrationGraphic.Draw = Draw.PointAndPoints;

                    calibrationGraphic.Present();
                }
            }
        }

        /// <summary>
        /// точка уже имеется в таблице
        /// </summary>
        /// <param name="Condition"></param>
        private void transformation_OnExist(Transformation.TCondition Condition)
        {
            MessageBox.Show(this, "Добавляемая точка имеется в таблице калибровки", "Информация", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// ошибка
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="Error"></param>
        private void transformation_OnError(Transformation.TCondition Condition, Transformation.TypeError Error)
        {
            switch (Error)
            {
                case DeviceManager.Transformation.TypeError.TransformationError:

                    if (transformation.Table.Count > 2)
                    {
                        transformation.Remove(Condition);
                        MessageBox.Show(this, "Во время вычисления коэффициентов афинного преобразования возникла ошибка. Строка с ошибкой удалена", 
                                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        button2_Click(null, null);
                        MessageBox.Show(this, "Во время вычисления коэффициентов афинного преобразования возникла ошибка. Таблица очищена",
                                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                default:

                    break;
            }
            ShowCalibrationTableInDataGrid(transformation);
            ShowCalibrationTableInGraphics(transformation);
        }

        /// <summary>
        /// очистили таблицу калибровки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void transformation_OnClear(object sender, EventArgs e)
        {
            ShowCalibrationTableInDataGrid(transformation);
            ShowCalibrationTableInGraphics(transformation);
        }

        /// <summary>
        /// удалили точку
        /// </summary>
        /// <param name="Condition"></param>
        private void transformation_OnRemove(Transformation.TCondition Condition)
        {
            ShowCalibrationTableInDataGrid(transformation);
            ShowCalibrationTableInGraphics(transformation);
        }

        /// <summary>
        /// Добавили точку
        /// </summary>
        /// <param name="Condition"></param>
        private void transformation_OnInsert(Transformation.TCondition Condition)
        {
            ShowCalibrationTableInDataGrid(transformation);
            ShowCalibrationTableInGraphics(transformation);
        }

        /// <summary>
        /// Изменили точку
        /// </summary>
        /// <param name="Condition"></param>
        private void transformation_OnEdit(Transformation.TCondition Condition)
        {
            ShowCalibrationTableInDataGrid(transformation);
            ShowCalibrationTableInGraphics(transformation);
        }

        /// <summary>
        /// Первый аргумент
        /// </summary>
        public Argument FirstArg
        {
            get { return first; }
            set { first = value; }
        }

        /// <summary>
        /// Второй аргумент
        /// </summary>
        public Argument SecondtArg
        {
            get { return second; }
            set { second = value; }
        }

        /// <summary>
        /// Определяет кусочно-линейную апроксимацию
        /// </summary>
        public Transformation Transformation
        {
            get { return transformation; }
            set 
            { 
                transformation = value;

                transformation.OnInsert += new Transformation.TConditionEventHandle(transformation_OnInsert);
                transformation.OnEdit += new Transformation.TConditionEventHandle(transformation_OnEdit);
                transformation.OnRemove += new Transformation.TConditionEventHandle(transformation_OnRemove);

                transformation.OnClear += new EventHandler(transformation_OnClear);
                transformation.OnError += new Transformation.ErrorEventHandle(transformation_OnError);

                transformation.OnExist += new Transformation.TConditionEventHandle(transformation_OnExist);
            }
        }

        /// <summary>
        /// Добавляем точку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insertPoint_Click(object sender, EventArgs e)
        {
            try
            {
                Transformation.TCondition t_condition = new Transformation.TCondition();

                t_condition.Signal = double.Parse(textBoxTotablePhysic.Text);
                t_condition.Result = double.Parse(textBoxToTableCalibrated.Text);

                transformation.Insert(t_condition);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Удаляем точку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removePoint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCalibrationTable.Rows.Count > 2)
                {
                    int selectedRow = dataGridViewCalibrationTable.SelectedCells[0].RowIndex;
                    Transformation.TCondition t_condition = transformation.Table[selectedRow];

                    transformation.Remove(t_condition);
                }
                else
                {
                    MessageBox.Show(this, "В таблице не может менее двух точек", "Предупреждение", 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch { }
        }

        /// <summary>
        /// Вывод таблицы калибровки
        /// </summary>
        /// <param name="calibrationTable">Таблица которую необходимо вывести</param>
        private void ShowCalibrationTableInDataGrid(Transformation calibrationTable)
        {
            dataGridViewCalibrationTable.Rows.Clear();
            for (int i = 0; i < calibrationTable.Table.Count; i++)
            {
                DataGridViewRow r = new DataGridViewRow();

                if ((i % 2) == 0) r.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                dataGridViewCalibrationTable.Rows.Add(r);
            }
                        
            for (int i = 0; i < calibrationTable.Table.Count; i++)
            {
                dataGridViewCalibrationTable[0, i].Value = calibrationTable.Table[i].Signal;
                dataGridViewCalibrationTable[1, i].Value = calibrationTable.Table[i].Result;
            }
        }

        /// <summary>
        /// Вывод графика на форму
        /// </summary>
        /// <param name="calibrationTable">Таблица калибровки которую необходимо показать</param>
        private void ShowCalibrationTableInGraphics(Transformation calibrationTable)
        {
            int k = 0;
            if (checkBoxDoScale.Checked)
            {
                k = 1;
            }

            PointF[] points = new PointF[calibrationTable.Table.Count - k];
            float maxX = 0, maxY = 0;

            for (int index = 0; index < calibrationTable.Table.Count - k; index++)
            {

                if (calibrationTable.Table[index].Signal > maxX) maxX = (float)calibrationTable.Table[index].Signal;
                if (calibrationTable.Table[index].Result > maxY) maxY = (float)calibrationTable.Table[index].Result;                

                points[index] = new PointF((float)calibrationTable.Table[index].Signal, (float)calibrationTable.Table[index].Result);
            }
            calibrationGraphic.InsertPoints(points);

            if (maxX != 0 || maxY != 0)
            {
                calibrationGraphic.LogicalPixelX = maxX;
                calibrationGraphic.LogicalPixelY = maxY;

                calibrationGraphic.CalculateScale();
            }

            calibrationGraphic.Draw = Draw.PointsOnly;
            calibrationGraphic.Present();
        }

        /// <summary>
        /// отобразить калибровку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTransformationForm_Shown(object sender, EventArgs e)
        {
            ShowCalibrationTableInDataGrid(transformation);
            ShowCalibrationTableInGraphics(transformation);

            /*Formula formula = GetArgument(first);
            if (formula != null)
            {
                textBoxSelectedParameter.Tag = formula;
                textBoxSelectedParameter.Text = formula.Macros.Description;
            }*/
        }

        /// <summary>
        /// Выбрать калибруемый параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectParameter_Click(object sender, EventArgs e)
        {
            ResultsForm r_frm = new ResultsForm(app);
            if (_parameter != null)
            {
                r_frm.Position = _parameter.Identifier;
            }

            if (r_frm.ShowDialog(this) == DialogResult.OK)
            {
                textBoxSelectedParameter.Tag = r_frm.SelectedParameter;
                textBoxSelectedParameter.Text = r_frm.SelectedParameter.Name;

                _parameter = r_frm.SelectedParameter;
                checkBoxDoScale.Checked = false;

                transformation.Clear();
                foreach (Transformation.TCondition val in _parameter.Transformation.Table)
                {
                    Transformation.TCondition _v = new DeviceManager.Transformation.TCondition();
                    
                    _v.Multy = val.Multy;
                    _v.Result = val.Result;

                    _v.Shift = val.Shift;
                    _v.Signal = val.Signal;

                    transformation.Insert(_v);
                }
                //transformation.Arg[0].Index = first.Index;
            }
        }
        /*
        /// <summary>
        /// Получеть формулу с указанным аргументом
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        private Formula GetArgument(Argument argument)
        {
            foreach (Formula formula in app.Converter.Formuls)
            {
                if (formula.Position == argument.Index)
                {
                    return formula;
                }
            }

            return null;
        }*/

        /// <summary>
        /// приняли настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            if (_parameter != null)
            {
                if (transformation.Table.Count >= 2)
                {
                    _parameter.Transformation.Clear();
                    foreach (var val in transformation.Table)
                    {
                        _parameter.Transformation.Insert(val);
                    }

                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(this, "Таблица калибровки не корректна", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                    DialogResult = System.Windows.Forms.DialogResult.None;
                }
            }
        }

        /// <summary>
        /// зафиксить калибровку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fromTo_Click(object sender, EventArgs e)
        {
            textBoxTotablePhysic.Text = textBoxFromDevicePhysic.Text;
            textBoxToTableCalibrated.Text = textBoxFromDeviceCalibrated.Text;
        }

        private delegate void InsertToText(float physical, float calibrated);

        /// <summary>
        /// закрываемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTransformationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            app.Commutator.onParameterUpdated -= Converter_OnComplete;

            transformation.OnInsert -= transformation_OnInsert;
            transformation.OnEdit -= transformation_OnEdit;
            transformation.OnRemove -= transformation_OnRemove;

            transformation.OnClear -= transformation_OnClear;
            transformation.OnError -= transformation_OnError;

            transformation.OnExist -= transformation_OnExist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxCalculate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCalculate.Checked)
            {                
            }
            else
            {
                calibrationGraphic.Draw = Draw.PointsOnly;
                calibrationGraphic.Present();
            }            
        }

        private void AddTransformationForm_Load(object sender, EventArgs e)
        {
        }

        private void checkBoxDoScale_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDoScale.Checked)
            {
                if (transformation.Table.Count < 3)
                {
                    MessageBox.Show(this, "При масштабировании без последней точки в таблице должно быть минимум три точки", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    checkBoxDoScale.Checked = false;
                }
            }
            ShowCalibrationTableInGraphics(transformation);
        }

        /// <summary>
        /// начинаем редактирование точки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCalibrationTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            needCorrect = false;
            correctP = false;

            if (e.ColumnIndex == 0) correctP = true;
            oldValue = SKC.Application.ParseDouble(dataGridViewCalibrationTable[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        /// <summary>
        /// завершаем редактирование точки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCalibrationTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedRow = dataGridViewCalibrationTable.SelectedCells[0].RowIndex;

                if (correctP)
                {
                    if( !transformation.EditSignal(newValue, selectedRow) )
                    {
                        MessageBox.Show(this, "Введённый сигнал уже есть в таблице калибровки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ShowCalibrationTableInDataGrid(transformation);
                    }
                }
                else
                {
                    transformation.EditResult(newValue, selectedRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// парсим
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCalibrationTable_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (Type.GetTypeCode(e.Value.GetType()) == TypeCode.String)
            {
                try
                {
                    newValue = SKC.Application.ParseDouble(e.Value.ToString());
                    if (!double.IsNaN(newValue))
                    {
                        e.ParsingApplied = true;
                    }
                    else
                        throw new Exception();
                }
                catch (Exception)
                {
                    MessageBox.Show("Введенное значение не является допустимым числом");
                    newValue = oldValue;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                checkBoxDoScale.Checked = false;

                transformation.Clear();

                Transformation.TCondition t1 = new Transformation.TCondition();
                Transformation.TCondition t2 = new Transformation.TCondition();

                t1.Result = 0;
                t1.Signal = 0;

                t2.Result = 65535;
                t2.Signal = 65535;

                transformation.Insert(t1);
                transformation.Insert(t2);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}