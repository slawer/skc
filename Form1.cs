using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using Microsoft.VisualBasic;

using DataBase;
using Buffering;
using GraphicComponent;

using WCF;
using DeviceManager;
using WCF.WCF_Client;

namespace SKC
{
    public partial class Form1 : Form
    {
        private loadResults load_res;                       // текущее состояние приложения
        private GraphicManager manager;                     // управляет отрисовкой параметров

        private Application _app = null;                    // основное приложение
        private DataBaseSaverAgent agent;                   // отправляет данные на сохранение в БД        

        private bool isFinished = false;                    // была завершена работа или нет

        private TimeSpan waitDevManInterval = new TimeSpan(0, 0, 0, 5);
        private DateTime finishedTime = DateTime.MinValue;
        private TimeSpan waitFinishedTime = new TimeSpan(0, 0, 0, 30);

        private TimeSpan intrvalNewStages = new TimeSpan(0, 0, 0, 5); // Время, в течении которого не разрешается переключать этап
        private DateTime NewStagesTime = DateTime.MinValue; // Время начала этапа

        public Form1()
        {
            updtr = new UpdaterList(upd);

            lastSatage = DateTime.Now;
            lastSpan = new TimeSpan(0, 0, 0, 10);

            _app = Application.CreateInstance();
            _app.Commutator.onParameterUpdated += new EventHandler(Commutator_onParameterUpdated);

            InitializeComponent();

            manager = graphicsSheet1.InstanceManager();
            manager.StartTime = DateTime.Now;

            manager.OnData += new OnDataEventHander(manager_OnData);
            manager.OnDataNeed += new EventHandler(manager_OnDataNeed);
            
            manager.Orientation = GraphicComponent.Orientation.Horizontal;

            _app.Graphic_consumption = manager.InstanceGraphic();
            _app.Graphic_volume = manager.InstanceGraphic();

            _app.Graphic_density = manager.InstanceGraphic();

            _app.Graphic_pressure = manager.InstanceGraphic();
            _app.Graphic_temperature = manager.InstanceGraphic();

            for (int i = 0; i < 5; i++)
            {
                _app.Commutator.Parameters[i].Channel = null;
            }

            setv = new SetterValue(SeterValue);
            agent = _app.Manager.CreateAgent();

            dStatuser = new devMnStatuser(DevStatuse);

            DevManClient.onConnected += new EventHandler(DevManClient_onConnected);
            DevManClient.onDisconnected += new EventHandler(DevManClient_onDisconnected);

            _app.Load();
            toolStripLabelCurrentDateTimeDay.Text = DateTime.Now.ToString("dddd    dd MMMM yyyy    HH:mm:ss    ", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// передать данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manager_OnData(object sender, GraphicEventArgs e)
        {
            try
            {
                if (_app != null)
                {
                    Slice[] slices = _app.getData(e.StartTime, e.FinishTime);
                    if (slices != null)
                    {
                        _app.Graphic_consumption.Clear();
                        _app.Graphic_volume.Clear();

                        _app.Graphic_density.Clear();
                        _app.Graphic_pressure.Clear();

                        _app.Graphic_temperature.Clear();

                        foreach (Slice slice in slices)
                        {
                            if (slice.slice != null)
                            {
                                int sliceLen = slice.slice.Length;

                                if (_app.Commutator.Technology.Consumption.IndexToSave > -1 &&
                                    _app.Commutator.Technology.Consumption.IndexToSave < sliceLen)
                                {   
                                    _app.Graphic_consumption.Insert(slice._date, slice.slice[_app.Commutator.Technology.Consumption.IndexToSave]);
                                }

                                if (_app.Commutator.Technology.Volume.IndexToSave > -1 &&
                                    _app.Commutator.Technology.Volume.IndexToSave < sliceLen)
                                {
                                    _app.Graphic_volume.Insert(slice._date, slice.slice[_app.Commutator.Technology.Volume.IndexToSave]);
                                }

                                if (_app.Commutator.Technology.Density.Index > -1 &&
                                    _app.Commutator.Technology.Density.Index < _app.Commutator.Parameters.Length)
                                {
                                    int num = _app.Commutator.Parameters[_app.Commutator.Technology.Density.Index].Channel.Number;
                                    if (num > -1 && num < sliceLen)
                                    {
                                        _app.Graphic_density.Insert(slice._date, slice.slice[num]);
                                    }
                                }

                                if (_app.Commutator.Technology.Pressure.Index > -1 &&
                                    _app.Commutator.Technology.Pressure.Index < _app.Commutator.Parameters.Length)
                                {
                                    int num = _app.Commutator.Parameters[_app.Commutator.Technology.Pressure.Index].Channel.Number;
                                    if (num > -1 && num < sliceLen)
                                    {
                                        _app.Graphic_pressure.Insert(slice._date, slice.slice[num]);
                                    }
                                }

                                if (_app.Commutator.Technology.Temperature.Index > -1 &&
                                    _app.Commutator.Technology.Temperature.Index < _app.Commutator.Parameters.Length)
                                {
                                    int num = _app.Commutator.Parameters[_app.Commutator.Technology.Temperature.Index].Channel.Number;
                                    if (num > -1 && num < sliceLen)
                                    {
                                        _app.Graphic_temperature.Insert(slice._date, slice.slice[num]);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    _app = Application.CreateInstance();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private delegate void devMnStatuser(string status, Color clr);
        private devMnStatuser dStatuser;

        private void DevStatuse(string status, Color clr)
        {
            toolStripStatusLabelDevManStatus.BackColor = clr;
            toolStripStatusLabelDevManStatus.Text = status;
        }

        /// <summary>
        /// нту соединения с devMan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevManClient_onDisconnected(object sender, EventArgs e)
        {
                try
                {
                    Invoke(dStatuser, "Не подключен с серверу данных");
                }
                catch { }
        }

        /// <summary>
        /// есть соединение с devMan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevManClient_onConnected(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    Invoke(dStatuser, "Подключен с серверу данных", SystemColors.Control);
                }
                catch { }

                DevManClient.Context.Update();
                if (_app.Commutator.Technology.Stages.IsWork == false)
                {
                    // Привести тех. параметры в исходное состояние
                    //DevManClient.UpdateParameter(_app.Commutator.Technology.Volume.IndexToSave, float.NaN);
                    //DevManClient.UpdateParameter(_app.Commutator.Technology.Consumption.IndexToSave, float.NaN);
                    DevManClient.UpdateParameter(_app.Commutator.Technology.ProccessVolume.IndexToSave, float.NaN);

                    foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                    {
                        DevManClient.UpdateParameter(rgr.Consumption.IndexToSave, float.NaN);
                        DevManClient.UpdateParameter(rgr.Volume.IndexToSave, float.NaN);
                    }
                } 
            }
            catch { }
        }

        /// <summary>
        /// Получены данные от devMan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Commutator_onParameterUpdated(object sender, EventArgs e)
        {
            TechStage stage = _app.Commutator.Technology.Stages.Current;
            if (stage != null)
            {
                float con = stage.Consumption;
                try
                {
                    if (stage.Number > -1 && stage.Number <= _app.CurrentProject.Stages.Count)
                    {
                        ProjectStage p_stage = _app.CurrentProject.Stages[stage.Number - 1];
                        if (p_stage != null)
                        {
                            float vol = stage.Volume;

                            if (vol > p_stage.Max_volume) p_stage.Max_volume = vol;
                            if (vol < p_stage.Min_volume) p_stage.Min_volume = vol;
                        }
                    }
                }
                catch { }

                if (!Information.IsNumeric(_app.Commutator.Technology.Consumption.FormattedValue))
                {
                    BeginInvoke(setv, TtextBoxСonsumption, "-----");
                    TtextBoxСonsumption.BackColor = Color.Salmon;
                }
                else
                {
                    BeginInvoke(setv, TtextBoxСonsumption, string.Format("{0:F1}", con));
                    TtextBoxСonsumption.BackColor = SystemColors.ButtonFace;
                }

                BeginInvoke(setv, TtextBoxDensity, stage.FormattedDensity);// string.Format("{0:F3}", stage.Density));

                BeginInvoke(setv, TtextBoxVolume, string.Format("{0:F3}", stage.Volume));
                BeginInvoke(setv, TtextBoxTemperature, stage.FormattedTemperature);// string.Format("{0:F1}", stage.Temperature));

                BeginInvoke(setv, TtextBoxPressure, stage.FormattedPressure);// string.Format("{0:F1}", stage.Pressure));
                BeginInvoke(setv, textBoxProccessVolume, string.Format("{0:F2}", _app.Commutator.Technology.Stages.ProccessVolume));
            }
            else
            {
                // ---- не начата работа ----

                if (_app.Commutator.Technology.Stages.IsWork == false)
                {
                    if (isFinished)
                    {
                        if (_app.Commutator.Technology.Consumption.Index > -1 &&
                            _app.Commutator.Technology.Consumption.Index < _app.Commutator.Parameters.Length)
                        {
                            //int index = _app.Commutator.Technology.Consumption.Index;
                            //BeginInvoke(setv, TtextBoxСonsumption, string.Format("{0:F1}", _app.Commutator.Parameters[index].CurrentValue));

                            BeginInvoke(setv, TtextBoxСonsumption, string.Format("{0:F1}", 0));// _app.Commutator.Technology.Consumption.Value));
                        }

                        if (_app.Commutator.Technology.Volume.Index > -1 &&
                            _app.Commutator.Technology.Volume.Index < _app.Commutator.Parameters.Length)
                        {
                            //int index = _app.Commutator.Technology.Volume.Index;
                            //BeginInvoke(setv, TtextBoxVolume, string.Format("{0:F2}", _app.Commutator.Parameters[index].CurrentValue));

                            //_app.Commutator.Technology.Stages.
                            //BeginInvoke(setv, TtextBoxVolume, string.Format("{0:F3}", _app.Commutator.Technology.Volume.Value));
                        }

                        BeginInvoke(setv, TtextBoxDensity, _app.Commutator.Technology.Density.FormattedValue);// string.Format("{0:F3}", _app.Commutator.Technology.Density.Value));
                        BeginInvoke(setv, TtextBoxPressure, _app.Commutator.Technology.Pressure.FormattedValue);// string.Format("{0:F1}", _app.Commutator.Technology.Pressure.Value));

                        BeginInvoke(setv, TtextBoxTemperature, _app.Commutator.Technology.Temperature.FormattedValue);// string.Format("{0:F1}", _app.Commutator.Technology.Temperature.Value));
                    }
                    else
                    {
                        foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                        {
                            if (rgr.IsMain)
                            {
                                BeginInvoke(setv, TtextBoxСonsumption, string.Format("{0:F1}", rgr.Consumption.Value));
                                BeginInvoke(setv, textBoxProccessVolume, string.Format("{0:F2}", rgr.Volume.Value));

                                break;
                            }
                        }

                        //BeginInvoke(setv, TtextBoxСonsumption, _app.Commutator.Technology.Consumption.FormattedValue);
                        BeginInvoke(setv, TtextBoxDensity, _app.Commutator.Technology.Density.FormattedValue);// string.Format("{0:F3}", _app.Commutator.Technology.Density.Value));

                        BeginInvoke(setv, TtextBoxVolume, string.Format("{0:F2}", 0.0f)); 
                        BeginInvoke(setv, TtextBoxTemperature, _app.Commutator.Technology.Temperature.FormattedValue);// string.Format("{0:F1}", _app.Commutator.Technology.Temperature.Value));

                        BeginInvoke(setv, TtextBoxPressure, _app.Commutator.Technology.Pressure.FormattedValue);// string.Format("{0:F1}", _app.Commutator.Technology.Pressure.Value));
                        //BeginInvoke(setv, textBoxProccessVolume, _app.Commutator.Technology.Volume.FormattedValue);//string.Format("{0:F2}", 0.0f));
                    }
                }
            }
        }

        /// <summary>
        /// Необходимо передать данные для отрисовки графическому компоненту
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void manager_OnDataNeed(object sender, EventArgs e)
        {
            try
            {
                if (_app != null)
                {
                    Slice[] slices = _app.getData(manager.StartTime, manager.FinishTime);
                    if (slices != null)
                    {
                        _app.Graphic_consumption.Clear();
                        _app.Graphic_volume.Clear();

                        _app.Graphic_density.Clear();
                        _app.Graphic_pressure.Clear();

                        _app.Graphic_temperature.Clear();

                        DateTime _minT = _app.Commutator.MinTimeParameter();
                        foreach (Slice slice in slices)
                        {
                            if (slice.slice != null)
                            {
                                int sliceLen = slice.slice.Length;

                                if (_app.Commutator.Technology.Consumption.IndexToSave > -1 &&
                                    _app.Commutator.Technology.Consumption.IndexToSave < sliceLen)
                                {
                                    _app.Graphic_consumption.Insert(slice._date, slice.slice[_app.Commutator.Technology.Consumption.IndexToSave]);
                                    _app.Graphic_consumption.Tmin = _minT;
                                }

                                if (_app.Commutator.Technology.Volume.IndexToSave > -1 &&
                                    _app.Commutator.Technology.Volume.IndexToSave < sliceLen)
                                {
                                    _app.Graphic_volume.Insert(slice._date, slice.slice[_app.Commutator.Technology.Volume.IndexToSave]);
                                    _app.Graphic_volume.Tmin = _minT;
                                }

                                if (_app.Commutator.Technology.Density.Index > -1 &&
                                    _app.Commutator.Technology.Density.Index < _app.Commutator.Parameters.Length)
                                {
                                    int num = _app.Commutator.Parameters[_app.Commutator.Technology.Density.Index].Channel.Number;
                                    if (num > -1 && num < sliceLen)
                                    {
                                        _app.Graphic_density.Insert(slice._date, slice.slice[num]);
                                        _app.Graphic_density.Tmin = _minT;
                                    }
                                }

                                if (_app.Commutator.Technology.Pressure.Index > -1 &&
                                    _app.Commutator.Technology.Pressure.Index < _app.Commutator.Parameters.Length)
                                {
                                    int num = _app.Commutator.Parameters[_app.Commutator.Technology.Pressure.Index].Channel.Number;
                                    if (num > -1 && num < sliceLen)
                                    {
                                        _app.Graphic_pressure.Insert(slice._date, slice.slice[num]);
                                        _app.Graphic_pressure.Tmin = _minT;
                                    }
                                }

                                if (_app.Commutator.Technology.Temperature.Index > -1 &&
                                    _app.Commutator.Technology.Temperature.Index < _app.Commutator.Parameters.Length)
                                {
                                    int num = _app.Commutator.Parameters[_app.Commutator.Technology.Temperature.Index].Channel.Number;
                                    if (num > -1 && num < sliceLen)
                                    {
                                        _app.Graphic_temperature.Insert(slice._date, slice.slice[num]);
                                        _app.Graphic_temperature.Tmin = _minT;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    _app = Application.CreateInstance();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        SetterValue setv;
        private void SeterValue(TextBox box, String Value)
        {
            if (!Information.IsNumeric(Value))
            {
                box.BackColor = Color.Salmon;
            }
            else
                box.BackColor = SystemColors.ButtonFace;

            box.Text = Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            manager.Update();

            manager.UpdatePeriod = 500;
            manager.Mode = GraphicComponent.DrawMode.Activ;

            LoadApp();
            _app.UpdateTechGraphics();

            _app.LoadStages();
            _app.Commutator.Technology.Stages.InitializeStages();

            CorrectPanels();

            if (_app.Commutator.Technology.Stages.IsWork)
            {
                buttonStartWork.Enabled = false;
                buttonNewStage.Enabled = true;

                buttonFinishWork.Enabled = true;
                
                buttonRaport.Enabled = false;
                рапортToolStripMenuItem.Enabled = false;

                toolStripStatusLabelWorkState.Text = "Идет работа";
                ForLoadSetCurrentStage();

                try
                {
                    if (_app.Commutator.Technology.Stages.Current != null)
                    {
                        checkBoxRgrTurner.Enabled = true;
                        if (_app.Commutator.Technology.Stages.Current.StateRGR == StateRGR.Pressed)
                        {
                            dfgff = true;
                            checkBoxRgrTurner.Checked = true;
                        }
                        else
                            checkBoxRgrTurner.Checked = false;
                    }
                }
                catch { }

                if (_app.Commutator.Technology.Stages.Current != null)
                {
                    Project project = _app.CurrentProject;
                    if (project != null)
                    {
                        int number = _app.Commutator.Technology.Stages.Current.Number;
                        if (number > -1 && number <= project.Stages.Count)
                        {
                            project.Stages[number - 1].Koef = _app.Commutator.Technology.Stages.CorrectionFactor;

                            //toolStripLabelStageNumber.Text = string.Format("Этап {0}", number);

                            //toolStripLabelStageName.Text = project.Stages[number - 1].StageName;
                            _app.Commutator.Technology.Stages.Current.NameStage = project.Stages[number - 1].StageName;

                            toolStripTextBoxKoefs.Text = project.Stages[number - 1].Koef.ToString();
                        }
                    }
                }

                InsertToListStagesWithClear(_app.CurrentProject);
                SetSelectStageInListStages();
            }
            else
            {
                _app.Commutator.Technology.Stages.resetRGROn();
            }

            _app.Graphic_pressure.Units = "[кг/см2]";
            _app.Graphic_consumption.Units = "[л/сек]";
            _app.Graphic_volume.Units = "[м3]";
            _app.Graphic_density.Units = "[г/см3]";
//            _app.Graphic_temperature.Units = "[°C]";

            if (load_res == loadResults.ProjectLoadedAndDBLoaded)
            {
                if (_app.Manager.State != DataBaseState.Saving)
                {
                    try
                    {
                        _app.Manager.TurnOnToSavingMode();
                    }
                    catch { }
                }
                timerToDBSaver.Start();
            }

            label8.ForeColor = _app.Graphic_consumption.Color;
            TtextBoxСonsumption.ForeColor = _app.Graphic_consumption.Color;            
            
            label9.ForeColor = _app.Graphic_volume.Color;
            TtextBoxVolume.ForeColor = _app.Graphic_volume.Color;

            label10.ForeColor = _app.Graphic_density.Color;
            TtextBoxDensity.ForeColor = _app.Graphic_density.Color;
            
            label11.ForeColor = _app.Graphic_pressure.Color;
            TtextBoxPressure.ForeColor = _app.Graphic_pressure.Color;

            label12.ForeColor = _app.Graphic_temperature.Color;
            label12.Text = _app.Graphic_temperature.Description + _app.Graphic_temperature.Units;
            TtextBoxTemperature.ForeColor = _app.Graphic_temperature.Color;

            timerCheckerForParameters.Start();
        }

        /// <summary>
        /// Скорректировать панели
        /// </summary>
        protected void CorrectPanels()
        {
            try
            {
                if (_app.Panels != null)
                {
                    foreach (ParametersViewPanel panel in _app.Panels)
                    {
                        if (panel != null)
                        {
                            if (panel.Parameter1.Parameter != null) panel.Parameter1.Parameter = GetParameterForPanel(panel.Parameter1.Parameter.Identifier);
                            if (panel.Parameter2.Parameter != null) panel.Parameter2.Parameter = GetParameterForPanel(panel.Parameter2.Parameter.Identifier);

                            if (panel.Parameter3.Parameter != null) panel.Parameter3.Parameter = GetParameterForPanel(panel.Parameter3.Parameter.Identifier);
                            if (panel.Parameter4.Parameter != null) panel.Parameter4.Parameter = GetParameterForPanel(panel.Parameter4.Parameter.Identifier);

                            if (panel.Parameter5.Parameter != null) panel.Parameter5.Parameter = GetParameterForPanel(panel.Parameter5.Parameter.Identifier);
                        }
                    }
                }
            }
            catch { }
        }

        protected Parameter GetParameterForPanel(Guid identifier)
        {
            foreach (Parameter parameter in _app.Commutator.Parameters)
            {
                if (parameter.Identifier == identifier)
                {
                    return parameter;
                }
            }

            return null;
        }

        /// <summary>
        /// Послать команду сброса объема
        /// </summary>
        protected void ResetRgR()
        {
            try
            {
                foreach (BlockViewCommand cmd in _app.Commands)
                {
                    if (cmd.Actived)
                    {
                        if (cmd.UseForReset)
                        {
                            _app.devTcpManager.Client.Send(System.Text.Encoding.Default.GetBytes(cmd.CommandDsn));
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// тестово начали работу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartWork_Click(object sender, EventArgs e)
        {
            try
            {
                switch (load_res)
                {
                    case loadResults.ProjectLoadedAndDBLoaded:

                        // ---- требуемы условия выполнены можно начинать работу ----

                        NewStagesTime = DateTime.Now;
                        ResetRgR();                             // сбросили объем в бксд
                        System.Threading.Thread.Sleep(500);     // ждем пока бксд обработает сброс объема

                        _app.Commutator.Technology.Stages.resetRGROn();
                        foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                        {
                            rgr.Reset();
                            //rgr.CurrentVolume = 0.0f;
                        }

                        Project current = _app.CurrentProject;
                        if (current != null)
                        {
                            current.Worked = DateTime.Now;
                            foreach (ProjectStage stage in current.Stages)
                            {
                                stage.StageVolume = float.NaN;

                                stage.Min_volume = float.MaxValue;
                                stage.Max_volume = float.MinValue;

                                stage.StartTime = DateTime.MinValue;
                                stage.FinishTime = DateTime.MaxValue;
                            }

                            //InsertToListStagesWithClear(current);

                            if (current.Stages.Count == 0)
                            {
                                ProjectStage stage = new ProjectStage();

                                stage.Koef = 1.0f;

                                stage.StageVolume = float.NaN;
                                stage.StageName = string.Format("Этап работы 1");

                                current.Stages.Add(stage);                                
                            }
                            
                            //toolStripLabelStageNumber.Text = string.Format("Этап {0}", 1);

                            //toolStripLabelStageName.Text = current.Stages[0].StageName;
                            toolStripTextBoxKoefs.Text = current.Stages[0].Koef.ToString();

                            toolStripStatusLabelWorkState.Text = "Идет работа";

                            buttonStartWork.Enabled = false;
                            buttonNewStage.Enabled = true;

                            checkBoxRgrTurner.Enabled = true;
                            buttonFinishWork.Enabled = true;

                            buttonRaport.Enabled = false;
                            рапортToolStripMenuItem.Enabled = false;                            

                            _app.Commutator.Technology.Stages.NextStage();
                            _app.Commutator.Technology.Stages.Current.NameStage = current.Stages[0].StageName;

                            try
                            {
                                current.Stages[_app.Commutator.Technology.Stages.Current.Number - 1].StartTime = _app.Commutator.Technology.Stages.Current.StartTime;
                                current.Stages[_app.Commutator.Technology.Stages.Current.Number - 1].FinishTime = DateTime.MaxValue;
                            }
                            catch { }

                            _app.SaveStages();                            

                            _app.Commutator.Technology.Stages.TotalVolume = 0.0f;
                            _app.Commutator.Technology.Stages.ProccessVolume = 0.0f;

                            _app.Commutator.Technology.Stages.UpdateKoef(current.Stages[0].Koef);

                            InsertToListStagesWithClear(current);
                            SetSelectStageInListStages();

                            _app.Save();
                        }

                        if (_app.AutoStartConsumption)
                        {
                            checkBoxRgrTurner.Checked = true;
                        }
                        
                        break;

                    default:

                        ShowResultStatus(true);
                        break;
                }

                isFinished = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
        }

        bool dfgff = false;
        /// <summary>
        /// Запись РГР
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            switch (load_res)
            {
                case loadResults.ProjectLoadedAndDBLoaded:

                    if (checkBoxRgrTurner.Checked)
                    {
                        if (dfgff == false)
                        {
                            _app.Commutator.Technology.Stages.RgrOn();
                            foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                            {

                            }
                            checkBoxRgrTurner.Enabled = false;
                        }
                        else
                        {
                            checkBoxRgrTurner.Enabled = false;
                            dfgff = false;
                        }
                    }
                    break;

                default:

                    ShowResultStatus(true);
                    break;
            }
        }

        /// <summary>
        /// установить текущий этап в интерфесе. после загрузки проета и работы
        /// </summary>
        protected void ForLoadSetCurrentStage()
        {
            try
            {
                TechStage stage = _app.Commutator.Technology.Stages.Current;
                if (stage != null)
                {
                    int currentStageNumber = stage.Number; //stage.Number;
                    //toolStripLabelStageNumber.Text = string.Format("Этап {0}", currentStageNumber);

                    Project prjSt = _app.CurrentProject;
                    if (prjSt != null)
                    {
                        if (currentStageNumber > -1 && currentStageNumber < prjSt.Stages.Count)
                        {
                            //toolStripLabelStageName.Text = prjSt.Stages[currentStageNumber - 1].StageName;
                            toolStripTextBoxKoefs.Text = prjSt.Stages[currentStageNumber - 1].Koef.ToString();
                        }

                        InsertToListStagesWithClear(prjSt);
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// новый этап работы
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void buttonNewStage_Click(object sender, EventArgs e)
        {
            try
            {
                switch (load_res)
                {
                    case loadResults.ProjectLoadedAndDBLoaded:

                        DateTime now = DateTime.Now;
                        TimeSpan interval = now - NewStagesTime;
                        if (interval.Ticks < intrvalNewStages.Ticks)
                        {
                            break;
                        }

                        Project current = _app.CurrentProject;
                        if (current != null)
                        {
                            NewStagesTime = DateTime.Now;

                            TechStage last = _app.Commutator.Technology.Stages.Current;
                            if (last != null)
                            {
                                ResetRgR();                             // сбросили объем в бксд
                                System.Threading.Thread.Sleep(500);     // ждем пока бксд обработает сброс объема


                                _app.Commutator.Technology.Stages.NextStage();
                                foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                                {
                                    rgr.Reset();
                                }

                                //_app.Commutator.Technology.Rgr.Reset();

                                _app.SaveStages();

                                int stage_number = last.Number;
                                float stage_volume = last.Volume;

                                if (stage_number > -1 && stage_number <= current.Stages.Count)
                                {
                                    current.Stages[stage_number - 1].StageVolume = stage_volume;
                                }

                                int current_stage_number = _app.Commutator.Technology.Stages.Current.Number;
                                if (current_stage_number > -1 && current_stage_number > current.Stages.Count)
                                {
                                    // нету этапа в проектах
                                    ProjectStage stage = new ProjectStage();

                                    stage.Koef = 1.0f;

                                    stage.StageVolume = float.NaN;
                                    stage.StageName = string.Format("Этап работы {0}", current_stage_number);

                                    current.Stages.Add(stage);
                                }

                                _app.Commutator.Technology.Stages.UpdateKoef(current.Stages[current_stage_number - 1].Koef);

                                try
                                {
                                    current.Stages[_app.Commutator.Technology.Stages.Current.Number - 1].StartTime = _app.Commutator.Technology.Stages.Current.StartTime;
                                    current.Stages[_app.Commutator.Technology.Stages.Current.Number - 2].FinishTime = _app.Commutator.Technology.Stages.Current.StartTime;
                                }
                                catch { }

                                //toolStripLabelStageNumber.Text = string.Format("Этап {0}", current_stage_number);

                                //toolStripLabelStageName.Text = current.Stages[current_stage_number - 1].StageName;
                                _app.Commutator.Technology.Stages.Current.NameStage = current.Stages[current_stage_number - 1].StageName;

                                toolStripTextBoxKoefs.Text = current.Stages[current_stage_number - 1].Koef.ToString();

                                InsertToListStagesWithClear(current);
                                SetSelectStageInListStages();
                                
                                _app.Save();
                            }
                        }

                        break;

                    default:

                        ShowResultStatus(true);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
        }

        /// <summary>
        /// определяем поправочный коэффициент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCorrectKoef_Click(object sender, EventArgs e)
        {
            try
            {
                KoefForm frm = new KoefForm();
                frm.textBoxKoef.Text = _app.Commutator.Technology.Stages.CorrectionFactor.ToString();

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    _app.Commutator.Technology.Stages.UpdateKoef(frm.koef);
                    foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                    {
                        if (rgr.IsMain)
                        {
                            rgr.UpdateKoef(frm.koef);
                        }
                    }

                    _app.SaveStages();
                    toolStripTextBoxKoefs.Text = _app.Commutator.Technology.Stages.CorrectionFactor.ToString();
                }
            }
            catch { }
        }

        /// <summary>
        /// настраиваем параметры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configureParameters_Click(object sender, EventArgs e)
        {
            TunerForm frm = new TunerForm();
            frm.ShowDialog(this);

            _app.Save();
        }

        /// <summary>
        /// завершить работу
        /// </summary>
        /// <param name="sender">источник события</param>
        /// <param name="e">аргументы события</param>
        private void buttonFinishWork_Click(object sender, EventArgs e)
        {
            try
            {
                switch (load_res)
                {
                    case loadResults.ProjectLoadedAndDBLoaded:

                        TechStage current = _app.Commutator.Technology.Stages.Current;
                        if (current != null)
                        {
                            isFinished = true;
                            finishedTime = DateTime.Now;
                            do
                            {
                                _app.Commutator.Technology.Stages.Current.FinishStage();
                                _app.Commutator.Technology.Stages.Finish();

                                foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                                {
                                    rgr.FinishStage();
                                }

                            } while (_app.Commutator.Technology.Stages.IsWork);

                            _app.SaveStages();

                            _app.Commutator.Technology.Consumption.Value = 0.0f;                           

                            buttonNewStage.Enabled = false;

                            checkBoxRgrTurner.Checked = false;
                            checkBoxRgrTurner.Enabled = false;

                            buttonStartWork.Enabled = true;
                            buttonFinishWork.Enabled = false;

                            buttonRaport.Enabled = true;
                            рапортToolStripMenuItem.Enabled = true;

                            buttonRaport.Enabled = true;
                            toolStripStatusLabelWorkState.Text = "Работа остановлена";

                            Project prj = _app.CurrentProject;
                            if (prj != null)
                            {
                                prj.Finish = DateTime.Now;
                                if (prj.Stages.Count > 0)
                                {
                                    prj.Stages[current.Number - 1].StageVolume = current.Volume;
                                    prj.Stages[current.Number - 1].FinishTime = prj.Finish;
                                }
                            }
                        }

                        foreach (ListViewItem item in listViewStages.Items)
                        {
                            item.BackColor = Color.White;
                        }

                        InsertToListStagesWithClear(_app.CurrentProject);
                        _app.Save();
                                                
                        DevManClient.UpdateParameter(_app.Commutator.Technology.Consumption.IndexToSave, 0.0f);
                        DevManClient.UpdateParameter(_app.Commutator.Technology.Volume.IndexToSave, current.Volume);

                        foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                        {
                            DevManClient.UpdateParameter(rgr.Consumption.IndexToSave, 0.0f);
                            DevManClient.UpdateParameter(rgr.Volume.IndexToSave, rgr.CurrentVolume);
                        }

                        break;

                    default:

                        ShowResultStatus(true);
                        break;
                }

                //isFinished = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
        }

        private void настройкаГрафическогоТаблоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphicsTurnerForm frm = new GraphicsTurnerForm(manager);
            frm.ShowDialog(this);

            label8.ForeColor = _app.Graphic_consumption.Color;
            TtextBoxСonsumption.ForeColor = _app.Graphic_consumption.Color;

            label9.ForeColor = _app.Graphic_volume.Color;
            TtextBoxVolume.ForeColor = _app.Graphic_volume.Color;

            label10.ForeColor = _app.Graphic_density.Color;
            TtextBoxDensity.ForeColor = _app.Graphic_density.Color;

            label11.ForeColor = _app.Graphic_pressure.Color;
            TtextBoxPressure.ForeColor = _app.Graphic_pressure.Color;

            label12.ForeColor = _app.Graphic_temperature.Color;
            label12.Text = _app.Graphic_temperature.Description + _app.Graphic_temperature.Units;
            TtextBoxTemperature.ForeColor = _app.Graphic_temperature.Color;
            InsertToListStagesWithClear(_app.CurrentProject);
            SetSelectStageInListStages();

            _app.Save();
        }

        /// <summary>
        /// проверить состояние программы
        /// </summary>
        /// <param name="viewMessage">Отображать сообщение о состоянии или нет</param>
        protected void ShowResultStatus(bool viewMessage)
        {
            try
            {
                switch (load_res)
                {
                    case loadResults.NotBDPing:

                        if (viewMessage)
                        {
                            MessageBox.Show(this, "Не установлена связь с сервером баз данных.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;

                    case loadResults.NotLoadBD:

                        if (viewMessage)
                        {
                            MessageBox.Show(this, "Для текущей работы не загружена база данных.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;

                    case loadResults.ProjectLoaded:

                        if (viewMessage)
                        {
                            MessageBox.Show(this, "Не загружена работа.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;

                    case loadResults.ProjectLoadedAndDBLoaded:

                        if (viewMessage)
                        {
                            MessageBox.Show(this, "Проект и база данных для проекта загружены.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;

                    case loadResults.ProjectLoadedDBFind:

                        if (viewMessage)
                        {
                            MessageBox.Show(this, "пока не понятно что делать", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;

                    case loadResults.ProjectLoadedDBNotLoad:

                        if (viewMessage)
                        {
                            MessageBox.Show(this, "Не загружена база данных для текущей работы", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;

                    case loadResults.ProjectLoadedNotFindDB:

                        if (viewMessage)
                        {
                            MessageBox.Show(this, "Не найдена база данных для текущей работы.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;

                    case loadResults.ProjectLoadNotBDPing:

                        if (viewMessage)
                        {
                            MessageBox.Show(this, "Не установлена связь с сервером баз данных.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;

                    case loadResults.ProjectNotLoaded:

                        if (viewMessage)
                        {
                            MessageBox.Show(this, "Не загружена работа.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;

                    default:

                        if (viewMessage)
                        {
                            MessageBox.Show(this, "Во время работы программы возникла ошибка.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
        }

        /// <summary>
        /// вызвать окно проектов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void проектыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_app.Commutator.Technology.Stages.IsWork == false)
            {
                ProjectsForm2 frm = new ProjectsForm2();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    // ------ выбрали новую работу! ------
                    // ------ необходимо проверить наличие БД для данной работы ------
                    // ------ в случае необходимости создать, или же загрузить ------

                    if (_app.Manager.State == DataBaseState.Saving)
                    {
                        _app.Manager.TurnOffFromSavingMode();
                    }

                    if (_app.Manager.State == DataBaseState.Loaded)
                    {
                        _app.Manager.CloseDB();
                    }

                    InsertKoefs(_app.CurrentProject);
                    InsertToListStagesWithClear(_app.CurrentProject);

                    //InsertToListStages(_app.CurrentProject);

                    CheckAndLoadBD(_app.CurrentProject);
                    ShowResultStatus(true);

                    if (_app.Manager.State != DataBaseState.Saving)
                    {
                        try
                        {
                            _app.Manager.TurnOnToSavingMode();
                        }
                        catch { }
                    }
                    timerToDBSaver.Start();

                    isFinished = false;
                    _app.Commutator.Technology.is_finished = false;
                    _app.Commutator.ClearData();
                }

                InsertToListStagesWithClear(_app.CurrentProject);
                _app.Save();
            }
            else
            {
                // ---- ведуться работы ----
                MessageBox.Show(this, "Ведется работа. Вызов дерева проектов запрещен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Добавить этапы в список этапов
        /// </summary>
        /// <param name="project">Активный проект</param>
        protected void InsertToListStages(Project project)
        {
            try
            {
                if (project != null)
                {
                    //listViewStages.Items.Clear();
                    foreach (ProjectStage stage in project.Stages)
                    {
                        ListViewItem item = new ListViewItem((listViewStages.Items.Count + 1).ToString());

                        ListViewItem.ListViewSubItem name = new ListViewItem.ListViewSubItem(item, stage.StageName);
                        ListViewItem.ListViewSubItem koef = new ListViewItem.ListViewSubItem(item, "-------");

                        ListViewItem.ListViewSubItem p_rashod = new ListViewItem.ListViewSubItem(item, string.Format("{0:F1}", stage.Plan_consumption));
                        ListViewItem.ListViewSubItem p_obem = new ListViewItem.ListViewSubItem(item, string.Format("{0:F2}", stage.Plan_volume));

                        ListViewItem.ListViewSubItem p_davlenie = new ListViewItem.ListViewSubItem(item, string.Format("{0:F1}", stage.Plan_pressure));
                        ListViewItem.ListViewSubItem p_plotnost = new ListViewItem.ListViewSubItem(item, string.Format("{0:F3}", stage.Plan_density));

                        p_rashod.ForeColor = _app.Graphic_consumption.Color;
                        p_obem.ForeColor = _app.Graphic_volume.Color;

                        p_davlenie.ForeColor = _app.Graphic_pressure.Color;
                        p_plotnost.ForeColor = _app.Graphic_density.Color;

                        item.UseItemStyleForSubItems = false;

                        item.Tag = stage;

                        item.SubItems.Add(name);
                        item.SubItems.Add(koef);

                        item.SubItems.Add(p_rashod);

                        item.SubItems.Add(p_obem);
                        item.SubItems.Add(p_davlenie);
                        item.SubItems.Add(p_plotnost);

                        listViewStages.Items.Add(item);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Добавить этапы в список этапов
        /// </summary>
        /// <param name="project">Активный проект</param>
        protected void InsertToListStagesWithClear(Project project)
        {
            try
            {
                if (project != null)
                {
                    listViewStages.Items.Clear();

                    Color color = Color.White;
                    foreach (ProjectStage stage in project.Stages)
                    {
                        ListViewItem item = new ListViewItem((listViewStages.Items.Count + 1).ToString());

                        ListViewItem.ListViewSubItem name = new ListViewItem.ListViewSubItem(item, stage.StageName);

                        string volSt = "------";
                        if (float.IsNaN(stage.StageVolume) != true) volSt = string.Format("{0:F2}", stage.StageVolume);
                        
                        ListViewItem.ListViewSubItem koef = new ListViewItem.ListViewSubItem(item, volSt);
                        
                        ListViewItem.ListViewSubItem p_rashod = new ListViewItem.ListViewSubItem(item, string.Format("{0:F1}", stage.Plan_consumption));
                        ListViewItem.ListViewSubItem p_obem = new ListViewItem.ListViewSubItem(item, string.Format("{0:F2}", stage.Plan_volume));

                        ListViewItem.ListViewSubItem p_davlenie = new ListViewItem.ListViewSubItem(item, string.Format("{0:F1}", stage.Plan_pressure));
                        ListViewItem.ListViewSubItem p_plotnost = new ListViewItem.ListViewSubItem(item, string.Format("{0:F3}", stage.Plan_density));

                        /*string min_string = "-----";
                        if (stage.Min_volume != float.MaxValue) min_string = string.Format("{0:F2}", stage.Min_volume);
                        ListViewItem.ListViewSubItem p_min = new ListViewItem.ListViewSubItem(item, min_string);

                        string max_string = "-----";
                        if (stage.Max_volume != float.MinValue) max_string = string.Format("{0:F2}", stage.Max_volume);
                        ListViewItem.ListViewSubItem p_max = new ListViewItem.ListViewSubItem(item, max_string);*/

                        string d_string = "-----";
                        if (stage.StartTime != DateTime.MinValue) d_string = stage.StartTime.ToLongTimeString();
                        ListViewItem.ListViewSubItem p_start = new ListViewItem.ListViewSubItem(item, d_string);

                        p_rashod.ForeColor = _app.Graphic_consumption.Color;
                        p_obem.ForeColor = _app.Graphic_volume.Color;

                        p_davlenie.ForeColor = _app.Graphic_pressure.Color;
                        p_plotnost.ForeColor = _app.Graphic_density.Color;

                        item.Tag = stage;

                        item.UseItemStyleForSubItems = false;

                        item.SubItems.Add(name);
                        item.SubItems.Add(koef);

                        item.SubItems.Add(p_rashod);
                        item.SubItems.Add(p_obem);
                        item.SubItems.Add(p_davlenie);
                        item.SubItems.Add(p_plotnost);

                        //item.SubItems.Add(p_min);
                        //item.SubItems.Add(p_max);

                        item.SubItems.Add(p_start);
                        /*item.UseItemStyleForSubItems = true;

                        if (color == Color.White)
                        {
                            item.BackColor = color;
                            color = Color.Red;
                        }
                        else
                        {
                            item.BackColor = color;
                            color = Color.White;
                        }*/

                        listViewStages.Items.Add(item);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// добавить поправочные коэффициенты
        /// </summary>
        /// <param name="project">Проек в котором поправочные коэффициенты</param>
        protected void InsertKoefs(Project project)
        {
            try
            {
            }
            catch { }
        }

        /// <summary>
        /// обрабатываем БД для нового проекта
        /// </summary>
        /// <param name="project">Обрабатываемый проект</param>
        protected void CheckAndLoadBD(Project project)
        {
            try
            {
                if (project != null)
                {
                    string db_name = project.DB_Name;
                    if (db_name != string.Empty)
                    {
                        if (_app.Manager.IsConnectValid)
                        {
                            string[] bases = _app.Manager.DataBases;
                            if (bases != null)
                            {
                                bool find = false;
                                foreach (string db in bases)
                                {
                                    if (db_name == db)
                                    {
                                        find = true;
                                        break;
                                    }
                                }

                                if (find == false)
                                {
                                    _app.Manager.CreateBD(db_name);
                                    _app.Manager.LoadDB(db_name);

                                    if (_app.Manager.State == DataBaseState.Loaded)
                                    {
                                        foreach (Parameter parameter in _app.Commutator.Parameters)
                                        {
                                            _app.Manager.InsertParameter(parameter.Identifier);
                                        }
                                    }

                                    load_res = loadResults.ProjectLoadedAndDBLoaded;
                                    toolStripStatusLabelDBStatus.Text = "Связь с SQL сервером установлена";
                                }
                                else
                                {
                                    // ---- нашли БД нужно ее загрузить ----

                                    _app.Manager.LoadDB(db_name);
                                    load_res = loadResults.ProjectLoadedAndDBLoaded;

                                    toolStripStatusLabelDBStatus.Text = "Связь с SQL сервером установлена";
                                }
                            }
                            else
                            {
                                // ---- нету баз данных ----
                                load_res = loadResults.ProjectLoadedNotFindDB;
                                toolStripStatusLabelDBStatus.Text = "Связь с БД отсутствует";
                            }
                        }
                        else
                        {
                            // ---- нету связи с сервером БД ----
                            load_res = loadResults.ProjectLoadNotBDPing;
                            toolStripStatusLabelDBStatus.Text = "Связь с SQL сервером не установлена";
                        }
                    }
                    else
                    {
                        // ---- не определено имя БД для проекта ----
                        load_res = loadResults.NotLoadBD;
                        toolStripStatusLabelDBStatus.Text = "Связь с БД отсутсвует";
                    }
                }
                else
                {
                    // ---- проект не загружен ----

                    load_res = loadResults.ProjectNotLoaded;
                    toolStripStatusLabelDBStatus.Text = "Задание не удалось загрузить";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                load_res = loadResults.Default;

                toolStripStatusLabelDBStatus.Text = "Задание не загружено";
            }
        }

        // ---------------------- поддержка -------------------

        /// <summary>
        /// Выделить текущий этап работы
        /// </summary>
        private void SetSelectStageInListStages()
        {
            if (_app.Commutator.Technology.Stages.IsWork)
            {
                TechStage stage = _app.Commutator.Technology.Stages.Current;
                if (stage != null)
                {
                    int stage_number = stage.Number;
                    if (stage_number > -1 && stage_number <= listViewStages.Items.Count)
                    {
                        listViewStages.Items[stage_number - 1].BackColor = Color.LightSkyBlue;
                        listViewStages.Items[stage_number - 1].UseItemStyleForSubItems = true;

                        listViewStages.EnsureVisible(stage_number - 1);
                    }
                }
            }
            else
                foreach (ListViewItem item in listViewStages.Items)
                {
                    item.BackColor = Color.White;
                }
        }

        /// <summary>
        /// Загружаемся, проверяем текущее состояние
        /// </summary>
        protected void LoadApp()
        {
            try
            {
                if (_app != null)
                {
                    if (_app.CurrentProject != null)
                    {
                        Project current = _app.CurrentProject;
                        load_res = loadResults.ProjectLoaded;

                        if (_app.Manager.IsConnectValid)
                        {
                            toolStripStatusLabelDBStatus.Text = "Соединение с SQL сервером установлено";

                            // ---- есть связь с сервером БД
                            if (CheckBD(current.DB_Name))
                            {
                                // ---- найдена БД для проекта ----

                                load_res = loadResults.ProjectLoadedDBFind;
                                if (LoadDB(current.DB_Name))
                                {
                                    load_res = loadResults.ProjectLoadedAndDBLoaded;

                                    InsertKoefs(_app.CurrentProject);
                                    InsertToListStages(_app.CurrentProject);
                                }
                                else
                                {
                                    load_res = loadResults.ProjectLoadedDBNotLoad;

                                    InsertKoefs(_app.CurrentProject);
                                    InsertToListStages(_app.CurrentProject);

                                    toolStripStatusLabelDBStatus.Text = "Связь с БД отсутсвует";
                                }
                            }
                            else
                            {
                                // ---- не найдена БД на сервере ----

                                load_res = loadResults.ProjectLoadedNotFindDB;

                                InsertKoefs(_app.CurrentProject);
                                InsertToListStages(_app.CurrentProject);

                                toolStripStatusLabelDBStatus.Text = "Связь с БД отсутсвует";
                            }
                        }
                        else
                        {
                            // ----- нету связи с сервером быз данных -----

                            load_res = loadResults.ProjectLoadNotBDPing;

                            InsertKoefs(_app.CurrentProject);
                            InsertToListStages(_app.CurrentProject);

                            toolStripStatusLabelDBStatus.Text = "Связь с БД отсутсвует";

                        }
                    }
                    else
                    {
                        // ---- нету активного проекта ----

                        load_res = loadResults.ProjectNotLoaded;
                        toolStripStatusLabelDBStatus.Text = "Задание не выбрано";
                    }
                }
            }
            catch { }
        }


        /// <summary>
        /// Проверить состояние БД
        /// </summary>
        protected bool CheckBD(string db_name)
        {
            try
            {
                if (db_name != string.Empty)
                {
                    string[] bases = _app.Manager.DataBases;
                    if (bases != null)
                    {
                        foreach (string db in bases)
                        {
                            if (db_name == db)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Загружаем БД проекта
        /// </summary>
        /// <param name="db_name">Имя БД которую загрузить</param>
        /// <returns></returns>
        protected bool LoadDB(string db_name)
        {
            try
            {
                _app.Manager.LoadDB(db_name);
                return true;
            }
            catch { }
            return false;
        }

        protected Mutex tMutex = new Mutex();   // синхронизирует таймер
        /// <summary>
        /// Передаем данные для сохранения и проверяем наличие БД и сервера БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerToDBSaver_Tick(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (tMutex.WaitOne(50))
                {
                    blocked = true;
                    DateTime now = DateTime.Now;

                    if (_app.Manager.IsConnectValid)
                    {
                        toolStripStatusLabelDBStatus.Text = "Соединение с SQL сервер установлено";

                        Parameter[] parameters = _app.Commutator.Parameters;
                        if (parameters != null)
                        {
                            long ticks = now.Ticks;
                            foreach (Parameter parameter in parameters)
                            {
                                if (parameter.SaveToDB)
                                {
                                    if (now > parameter.DB_Time)
                                    {
                                        TimeSpan interval = now - parameter.DB_Time;
                                        TimeSpan pInterval = new TimeSpan(0, 0, 0, 0, parameter.IntervalToSaveToDB);

                                        if (interval.Ticks > pInterval.Ticks)
                                        {
                                            parameter.DB_Time = now;
                                            agent.Save(parameter.Identifier, parameter.CurrentValue, ticks);
                                        }
                                    }
                                    else
                                        parameter.DB_Time = now;

                                }
                            }
                        }
                    }
                    /*else
                    {
                        toolStripStatusLabelDBStatus.Text = "Соединение с SQL не установлено";
                    }*/
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
            finally
            {
                if (blocked) tMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// запускаем Рапорт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRaport_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey readKey = Registry.CurrentUser.OpenSubKey("Software\\SKB OREOL\\SKC\\{87333C0C-C0D2-45C5-A56C-68050851173A}");
                if (readKey != null)
                {
                    string raportPath = (string)readKey.GetValue("ProgramName");
                    readKey.Close();

                    if (raportPath != null && raportPath != string.Empty)
                    {
                        if (File.Exists(raportPath))
                        {
                            Process.Start(raportPath);
                        }
                        else
                        {
                            MessageBox.Show(this, "Не найдена программа \"Рапорт\"", "Сообщение",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Не найдена программа \"Рапорт\"", "Сообщение",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Не установлена программа \"Рапорт\"", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch { }
        }

        /// <summary>
        /// Настраиваем соединение с БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void настройкаСоединенияСБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBOptionsForm frm = new DBOptionsForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                if (_app.Commutator.Technology.Stages.IsWork == false)
                {
                    switch (_app.Manager.State)
                    {
                        case DataBaseState.Loaded:

                            MessageBox.Show(this, "Нельзя переключить сервер БД, если загружена база данных для проекта", "Сообщение",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            break;

                        case DataBaseState.Saving:

                            MessageBox.Show(this, "Нельзя переключить сервер БД во время записи параметров", "Сообщение",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            break;

                        default:

                            try
                            {
                                _app.Manager.UserID = frm.UserID;
                                _app.Manager.Password = frm.Password;

                                _app.Manager.DataSource = frm.DataSource;
                                if (_app.Manager.IsConnectValid)
                                {
                                    toolStripStatusLabelDBStatus.Text = "Соединение с SQL сервер установлено";
                                }
                                else
                                {
                                    toolStripStatusLabelDBStatus.Text = "Соединение с SQL не установлено";
                                }

                                _app.Save();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                    }
                }
                else
                    MessageBox.Show(this, "Нельзя переключить сервер БД, во время выполнения работы.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// просматириваем параметры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void просмотрПараметровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParametersViewForm frm = new ParametersViewForm();
            frm.Show(this);
        }

        /// <summary>
        /// настраиваем панели отображения параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void настройкаПанелейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TunerPanelsForm frm = new TunerPanelsForm();
            frm.ShowDialog(this);
            _app.Save();
        }

        /// <summary>
        /// пытаемся закрыть программу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_app.Commutator.Technology.Stages.IsWork)
            {
                MessageBox.Show(this, "Нельзя завершить работу программы во время работы!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                e.Cancel = true;
            }

            // Привести тех. параметры в исходное состояние
            DevManClient.UpdateParameter(_app.Commutator.Technology.ProccessVolume.IndexToSave, float.NaN);
            foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
            {
                DevManClient.UpdateParameter(rgr.Consumption.IndexToSave, float.NaN);
                DevManClient.UpdateParameter(rgr.Volume.IndexToSave, float.NaN);
            }
        }

        /// <summary>
        /// pfrhsdftvcz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void закрытьПрограммуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox frm = new AboutBox();
            frm.ShowDialog(this);
        }

        private void настройкаСоединенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            devManConnectorForm frm = new devManConnectorForm();
            //frm.accept_AutoLocalhost();
            frm.ShowDialog(this);

            _app.Save();
        }

        private void checkBoxRgrTurner_Click(object sender, EventArgs e)
        {
            _app.SaveStages();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void разблокировкаПараметровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnblockForm frm = new UnblockForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                if (frm.Password == _app.Manager.Password)
                {
                    configureParameters.Enabled = true;
                }
                else
                    configureParameters.Enabled = false;
            }
        }

        /// <summary>
        /// Щелкнули на этап
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewStages_DoubleClick(object sender, EventArgs e)
        {
            if (listViewStages.SelectedItems != null && listViewStages.SelectedItems.Count > 0)
            {
                ListViewItem selected = listViewStages.SelectedItems[0];
                if (selected != null)
                {
                    StageViewForm frm = new StageViewForm(selected.Index);
                    frm.Show(this);
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //WindowState = FormWindowState.Maximized;
            _app.devTcpManager.OnPacket += new PacketEventHandler(devTcpManager_OnPacket);

            if (_app.Commutator.Technology.Stages.IsWork == false)
            {
                LoadAppForm frm = new LoadAppForm();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    if (frm.radioButtonNewWork.Checked)
                    {
                        ProjectsForm2 frm_p = new ProjectsForm2();
                        if (frm_p.CreateNewAndSelectProject(this))
                        {
                            // ------ выбрали новую работу! ------
                            // ------ необходимо проверить наличие БД для данной работы ------
                            // ------ в случае необходимости создать, или же загрузить ------

                            if (_app.Manager.State == DataBaseState.Saving)
                            {
                                _app.Manager.TurnOffFromSavingMode();
                            }

                            if (_app.Manager.State == DataBaseState.Loaded)
                            {
                                _app.Manager.CloseDB();
                            }

                            InsertKoefs(_app.CurrentProject);
                            InsertToListStagesWithClear(_app.CurrentProject);

                            //InsertToListStages(_app.CurrentProject);

                            CheckAndLoadBD(_app.CurrentProject);
                            ShowResultStatus(true);

                            if (_app.Manager.State != DataBaseState.Saving)
                            {
                                try
                                {
                                    _app.Manager.TurnOnToSavingMode();
                                }
                                catch { }
                            }
                            timerToDBSaver.Start();

                            isFinished = false;
                            _app.Commutator.Technology.is_finished = false;

                            _app.Commutator.ClearData();
                        }

                        InsertToListStagesWithClear(_app.CurrentProject);
                        _app.Save();
                    }
                    else
                        if (frm.radioButtonNewWithStages.Checked)
                        {
                            проектыToolStripMenuItem_Click(this, EventArgs.Empty);
                        }
                        else if (frm.radioButtonSelectWork.Checked)
                        {
                            проектыToolStripMenuItem_Click(this, EventArgs.Empty);
                        }
                }
                else
                {
                    закрытьПрограммуToolStripMenuItem_Click(this, EventArgs.Empty);
                }
            }
            Parameter[] parameters = _app.Commutator.Parameters;
            if (parameters != null)
            {
                PDescription[] pars;
                pars = DevManClient.Parameters;
                foreach (Parameter parameter in parameters)
                {
                    try
                    {
                        foreach (PDescription param in pars)
                        {
                            if (param.Number == parameter.Channel.Number)
                            {
                                parameter.Channel.Type = param.Type;
                                break;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private DateTime lastSatage;
        private TimeSpan lastSpan;

        private UpdaterList updtr;

        private void upd()
        {            
            buttonNewStage_Click(null, EventArgs.Empty);
        }
        /// <summary>
        /// получили пакет от devMan по старому каналу обмена информацией
        /// </summary>
        /// <param name="packet"></param>
        protected void devTcpManager_OnPacket(string packet)
        {
            DateTime now = DateTime.Now;
            if ((now - lastSatage) > lastSpan)
            {
                if (_app.Commutator.Technology.Stages.IsWork)
                {
                    if (packet.Length > 5)
                    {
                        BlockViewCommand[] cmds = _app.Commands.ToArray();
                        if (cmds != null)
                        {
                            string sPak = packet.Substring(2, packet.Length - 5);
                            foreach (BlockViewCommand cmd in cmds)
                            {
                                if (cmd.Actived)
                                {
                                    if (cmd.UseForNextStage)
                                    {
                                        if (cmd.CommandDsn.Length > 5)
                                        {
                                            string sCmd = cmd.CommandDsn.Substring(2, cmd.CommandDsn.Length - 5);
                                            if (sCmd == sPak)
                                            {
                                                Invoke(updtr);
                                                lastSatage = DateTime.Now;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void настрокаТехнологииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_app.Commutator.Technology.Stages.IsWork == false)
            {
                EditTechParameterForm1 frm = new EditTechParameterForm1();
                frm.ShowDialog(this);

                _app.Save();

                label12.Text = _app.Graphic_temperature.Description + _app.Graphic_temperature.Units;
            }
            else
                MessageBox.Show(this, "Изменение технологических параметров в процессе работы запрещено.",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation | MessageBoxIcon.Error);
        }

        private void регистраторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KRSForm frm = new KRSForm(manager.StartTime);
            frm.Show(this);
        }

        /// <summary>
        /// редактировать этапы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void редактированиеЭтапыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_app.Commutator.Technology.Stages.IsWork)
                {
                    Project prj = _app.CurrentProject;
                    if (prj != null)
                    {
                        ProjectStagesForm frm = new ProjectStagesForm(prj);
                        frm.ShowDialog(this);

                        InsertKoefs(_app.CurrentProject);
                        InsertToListStagesWithClear(_app.CurrentProject);
                    }
                }
                else
                    MessageBox.Show(this, "Не разрешается редактировать этапы во время работы.", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void редактироватьЭтапыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            редактированиеЭтапыToolStripMenuItem_Click(null, e);
        }

        private Mutex cMutex = new Mutex();

        /// <summary>
        /// проверяем параметры на валидность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCheckerForParameters_Tick(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (cMutex.WaitOne(50))
                {
                    blocked = true;
                    //if (_app.Manager.IsConnectValid)
                    {
                        Parameter[] parameters = _app.Commutator.Parameters;
                        if (parameters != null)
                        {
                            toolStripButton1.BackColor = SystemColors.Control;
                            foreach (Parameter parameter in parameters)
                            {
                                if (parameter.IsValidValue == false)
                                {
                                    PDescription param = parameter.Channel;
                                    if (param.Type != DeviceManager.FormulaType.Capture)
                                    {
                                        toolStripButton1.BackColor = Color.Salmon;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    try
                    {
                        if (DevManClient.isConnected())
                        {
                            DateTime now = DateTime.Now;
                            if (now > _app.Commutator.LastTime)
                            {
                                TimeSpan interval = now - _app.Commutator.LastTime;
                                if (interval.Ticks > waitDevManInterval.Ticks)
                                {
                                    DevManClient.Disconnect();
                                    Invoke(dStatuser, "Не подключен с серверу данных", Color.Salmon);
                                    _app.Commutator.LastTime = now;
                                }
                                else
                                {
                                    Invoke(dStatuser, "Подключен с серверу данных", SystemColors.Control);
                                }
                            }
                            else
                            {
                                Invoke(dStatuser, "Подключен с серверу данных", SystemColors.Control);
                            }
                        }
                        else
                        {
                            DateTime now = DateTime.Now;
                            if (now > _app.Commutator.LastTime)
                            {
                                TimeSpan interval = now - _app.Commutator.LastTime;
                                if (interval.Ticks > waitDevManInterval.Ticks)
                                {
                                    _app.attemptConnect();
                                    Invoke(dStatuser, "Не подключен с серверу данных", Color.Salmon);
                                    _app.Commutator.LastTime = now;
                                }
                                else
                                {
                                    Invoke(dStatuser, "Не подключен с серверу данных", Color.Salmon);
                                }
                            }
                            else
                            {
                                Invoke(dStatuser, "Не подключен с серверу данных", Color.Salmon);
                            }
                        }
                    }
                    catch { }

                    if( isFinished )
                    {
                        DateTime now = DateTime.Now;
                        TimeSpan interval = now - finishedTime;
                        if (interval.Ticks > waitFinishedTime.Ticks)
                        {
                            // Привести тех. параметры в исходное состояние
                            isFinished = false;
                            //DevManClient.UpdateParameter(_app.Commutator.Technology.Consumption.IndexToSave, float.NaN);
                            //DevManClient.UpdateParameter(_app.Commutator.Technology.Volume.IndexToSave, float.NaN);
                            //DevManClient.UpdateParameter(_app.Commutator.Technology.ProccessVolume.IndexToSave, 0);
                            DevManClient.UpdateParameter(_app.Commutator.Technology.ProccessVolume.IndexToSave, float.NaN);

                            foreach (Rgr rgr in _app.Commutator.Technology.Rgrs)
                            {
                                DevManClient.UpdateParameter(rgr.Consumption.IndexToSave, float.NaN);
                                DevManClient.UpdateParameter(rgr.Volume.IndexToSave, float.NaN);
                            }
                        }
                    }
                }


                if (_app.Commutator.Technology.Stages.IsWork)
                {
                    Project prj = _app.CurrentProject;
                    if (prj != null)
                    {
                        TimeSpan delta = DateTime.Now - prj.Worked;
                        toolStripLabel3.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", delta.Hours, delta.Minutes, delta.Seconds);
                    }
                }
                toolStripLabelCurrentDateTimeDay.Text = DateTime.Now.ToString("dddd    dd MMMM yyyy    HH:mm:ss    ", CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
            finally
            {
                if (blocked) cMutex.ReleaseMutex();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ParameterCheckerForm frm = new ParameterCheckerForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void текущаяРаботаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CurrentProjectForm frm = new CurrentProjectForm();
            frm.ShowDialog(this);
        }

        /// <summary>
        /// сбросить объем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сброситьОбъемToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_app.Commutator.Technology.Stages.IsWork == false)
                {
                    if (_app.devTcpManager.Client.Connected)
                    {
                        foreach (BlockViewCommand cmd in _app.Commands)
                        {
                            if (cmd.Actived)
                            {
                                if (cmd.UseForReset)
                                {
                                    _app.devTcpManager.Client.Send(System.Text.Encoding.Default.GetBytes(cmd.CommandDsn));
                                }
                            }
                        }
                    }
                    else
                        MessageBox.Show(this, "Не установлена связь с сервером данных.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show(this, "Сбрасывать объем во время работы запрещено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch { }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                if (FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
                {
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;                    
                }
                else
                {
                    SetVisibleCore(false);
                    
                    FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    WindowState = FormWindowState.Maximized;

                    SetVisibleCore(true);
                }
            }
        }

        private void просмотрБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey readKey = Registry.CurrentUser.OpenSubKey("Software\\SKB OREOL\\SKC\\{33C18F9B-A548-44C6-B7F1-EFFD1238307D}");

                if (readKey != null)
                {
                    string raportPath = (string)readKey.GetValue("ProgramName");
                    readKey.Close();

                    if (raportPath != null && raportPath != string.Empty)
                    {
                        if (File.Exists(raportPath))
                        {
                            Process.Start(raportPath);
                        }
                        else
                        {
                            MessageBox.Show(this, "Не найдена программа \"Просмотра базы данных\"", "Сообщение",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Не найдена программа \"Просмотра базы данных\"", "Сообщение",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Не установлена программа \"Просмотра базы данных\"", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void калибровкаПараметраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeviceManager.AddTransformationForm frm = new DeviceManager.AddTransformationForm(_app);
            frm.ShowDialog(this);

            _app.Save();
        }

        /// <summary>
        /// запускаем справку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("skc_help.chm");
            }
            catch { }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// настройка расходомеров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void настройкаРасходомеровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RgrsForm frm = new RgrsForm();
            frm.ShowDialog(this);
        }
    }

    /// <summary>
    /// результат загрузки приложения
    /// </summary>
    enum loadResults
    {
        /// <summary>
        /// Проект не загружен
        /// </summary>
        ProjectNotLoaded,

        /// <summary>
        /// Проект загружен
        /// </summary>
        ProjectLoaded,

        /// <summary>
        /// Проект загружен, но нету связи с сервером БД
        /// </summary>
        ProjectLoadNotBDPing,

        /// <summary>
        /// проект загружен, но не найдена БД для проекта
        /// </summary>
        ProjectLoadedNotFindDB,

        /// <summary>
        /// Проект загружен а БД не загружена
        /// </summary>
        ProjectLoadedDBNotLoad,

        /// <summary>
        /// проект загружен и БД тоже загружена
        /// </summary>
        ProjectLoadedAndDBLoaded,

        /// <summary>
        /// Проект загружен и база данных найдена
        /// </summary>
        ProjectLoadedDBFind,

        /// <summary>
        /// нету связи с БД
        /// </summary>
        NotBDPing,

        /// <summary>
        /// Не загружена БД
        /// </summary>
        NotLoadBD,

        /// <summary>
        /// Ничего не делалось.
        /// </summary>
        Default
    }

    delegate void SetterValue(TextBox box, String Value);
    delegate void UpdaterList();
}