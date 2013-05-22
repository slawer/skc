using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Buffering;
using GraphicComponent;

namespace SKC
{
    public partial class StageViewForm : Form
    {
        private GraphicManager manager;                     // управляет отрисовкой параметров
        private Application _app = null;                    // основное приложение

        private int stage_index;

        private Graphic[] graphics;                         // графики которые отрисовывать
        private ProjectStage sel_stage;

        float maxRashod = float.NaN;
        float minRashod = float.NaN;
        float sredRashod = float.NaN;

        float maxObem = float.NaN;
        float minObem = float.NaN;
        float sredObem = float.NaN;

        float maxDavlenie = float.NaN;
        float minDavlenie = float.NaN;
        float sredDavlenie = float.NaN;

        float maxPlotnost = float.NaN;
        float minPlotnost = float.NaN;
        float sredPlotnost = float.NaN;

        float maxTemp = float.NaN;
        float minTemp = float.NaN;
        float sredTemp = float.NaN;

        public StageViewForm(int s_index)
        {
            stage_index = s_index;
            InitializeComponent();

            _app = Application.CreateInstance();
            if (_app != null)
            {
                Project prj = _app.CurrentProject;
                if (prj != null)
                {
                    if (stage_index > -1 && stage_index < prj.Stages.Count)
                    {
                        sel_stage = prj.Stages[stage_index];
                        if (sel_stage != null)
                        {
                            manager = graphicsSheet1.InstanceManager();
                            manager.Mode = GraphicComponent.DrawMode.PassivScroll;

                            manager.HardTime = true;
                            manager.HardStartTime = sel_stage.StartTime;

                            manager.StartTime = sel_stage.StartTime;
                            manager.Orientation = GraphicComponent.Orientation.Horizontal;

                            graphics = new Graphic[5];
                            for (int index = 0; index < 5; index++)
                            {
                                graphics[index] = manager.InstanceGraphic();
                                switch (index)
                                {
                                    case 0:

                                        graphics[index].Color = _app.Graphic_consumption.Color;
                                        graphics[index].Description = _app.Graphic_consumption.Description;

                                        graphics[index].Range.Min = _app.Graphic_consumption.Range.Min;
                                        graphics[index].Range.Max = _app.Graphic_consumption.Range.Max;

                                        graphics[index].Units = _app.Graphic_consumption.Units;
                                        break;

                                    case 1:

                                        graphics[index].Color = _app.Graphic_volume.Color;
                                        graphics[index].Description = _app.Graphic_volume.Description;

                                        graphics[index].Range.Min = _app.Graphic_volume.Range.Min;
                                        graphics[index].Range.Max = _app.Graphic_volume.Range.Max;

                                        graphics[index].Units = _app.Graphic_volume.Units;
                                        break;

                                    case 2:

                                        graphics[index].Color = _app.Graphic_density.Color;
                                        graphics[index].Description = _app.Graphic_density.Description;

                                        graphics[index].Range.Min = _app.Graphic_density.Range.Min;
                                        graphics[index].Range.Max = _app.Graphic_density.Range.Max;

                                        graphics[index].Units = _app.Graphic_density.Units;
                                        break;

                                    case 3:

                                        graphics[index].Color = _app.Graphic_pressure.Color;
                                        graphics[index].Description = _app.Graphic_pressure.Description;

                                        graphics[index].Range.Min = _app.Graphic_pressure.Range.Min;
                                        graphics[index].Range.Max = _app.Graphic_pressure.Range.Max;

                                        graphics[index].Units = _app.Graphic_pressure.Units;
                                        break;

                                    case 4:

                                        graphics[index].Color = _app.Graphic_temperature.Color;
                                        graphics[index].Description = _app.Graphic_temperature.Description;

                                        graphics[index].Range.Min = _app.Graphic_temperature.Range.Min;
                                        graphics[index].Range.Max = _app.Graphic_temperature.Range.Max;

                                        graphics[index].Units = _app.Graphic_temperature.Units;
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }
        }

        /// <summary>
        /// Загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageViewForm_Load(object sender, EventArgs e)
        {
            if (sel_stage != null)
            {
                textBoxStartTime.Text = sel_stage.StartTime.ToLongTimeString();
                textBoxFinishTime.Text = sel_stage.FinishTime.ToLongTimeString();

                Text = string.Format("Просмотр этапа: {0}", sel_stage.StageName);

                CaclulateStatistics();
                InsertParametersInList();
            }
        }

        /// <summary>
        /// рисуем графики
        /// </summary>
        private void ShowGraphics()
        {
            //manager.HardTime = true;
            //manager.StartTime = sel_stage.StartTime;            

            Slice[] stage_data = _app.getData(sel_stage.StartTime, sel_stage.FinishTime);
            if (stage_data != null)
            {
                int rashod_index = _app.Commutator.Technology.Consumption.IndexToSave;
                int obem_index = _app.Commutator.Technology.Volume.IndexToSave;

                int davlenie_index = _app.Commutator.Parameters[_app.Commutator.Technology.Pressure.Index].Channel.Number;
                int plotnost_index = _app.Commutator.Parameters[_app.Commutator.Technology.Density.Index].Channel.Number;

                int temp_index = _app.Commutator.Parameters[_app.Commutator.Technology.Temperature.Index].Channel.Number;

                graphics[0].Clear();
                graphics[1].Clear();
                graphics[2].Clear();
                graphics[3].Clear();
                graphics[4].Clear();

                foreach (Slice slice in stage_data)
                {
                    if (slice.slice != null)
                    {
                        graphics[0].Insert(slice._date, slice.slice[rashod_index]);
                        graphics[1].Insert(slice._date, slice.slice[obem_index]);
                        graphics[2].Insert(slice._date, slice.slice[plotnost_index]);
                        graphics[3].Insert(slice._date, slice.slice[davlenie_index]);
                        graphics[4].Insert(slice._date, slice.slice[temp_index]);
                    }
                }

                graphics[0].Current = float.NaN;
                graphics[1].Current = float.NaN;
                graphics[2].Current = float.NaN;
                graphics[3].Current = float.NaN;
                graphics[4].Current = float.NaN;
            }

            manager.Update();            
        }

        /// <summary>
        /// Вычислить статистические данные параметров
        /// </summary>
        private void CaclulateStatistics()
        {
            Slice[] stage_data = _app.getData(sel_stage.StartTime, sel_stage.FinishTime);
            if (stage_data != null)
            {
                int rashod_index = _app.Commutator.Technology.Consumption.IndexToSave;
                int obem_index = _app.Commutator.Technology.Volume.IndexToSave;

                int davlenie_index = _app.Commutator.Parameters[_app.Commutator.Technology.Pressure.Index].Channel.Number;
                int plotnost_index = _app.Commutator.Parameters[_app.Commutator.Technology.Density.Index].Channel.Number;

                int temp_index = _app.Commutator.Parameters[_app.Commutator.Technology.Temperature.Index].Channel.Number;

                maxRashod = FindMax(stage_data, rashod_index); 
                minRashod = FindMin(stage_data, rashod_index);
                sredRashod = GetMiddle(stage_data, rashod_index);
                
                maxObem = FindMax(stage_data, obem_index);
                minObem = FindMin(stage_data, obem_index);
                sredObem = GetMiddle(stage_data, obem_index);

                maxDavlenie = FindMax(stage_data, davlenie_index);
                minDavlenie = FindMin(stage_data, davlenie_index);
                sredDavlenie = GetMiddle(stage_data, davlenie_index);

                maxPlotnost = FindMax(stage_data, plotnost_index);
                minPlotnost = FindMin(stage_data, plotnost_index);
                sredPlotnost = GetMiddle(stage_data, plotnost_index);

                maxTemp = FindMax(stage_data, temp_index);
                minTemp = FindMin(stage_data, temp_index);
                sredTemp = GetMiddle(stage_data, temp_index);
            }
        }

        /// <summary>
        /// Минимальное значение
        /// </summary>
        /// <param name="slices"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private float FindMax(Slice[] slices, int index)
        {
            try
            {
                float max = float.MinValue;
                foreach (Slice slice in slices)
                {
                    if (slice.slice != null)
                    {
                        if (index > -1 && index <= slice.slice.Length)
                        {
                            if (slice.slice[index] > max)
                            {
                                max = slice.slice[index];
                            }
                        }
                    }
                }

                return max;
            }
            catch { }
            return float.NaN; 
        }
        
        /// <summary>
        /// максимальное значение
        /// </summary>
        /// <param name="slices"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private float FindMin(Slice[] slices, int index)
        {
            try
            {
                float min = float.MaxValue;
                foreach (Slice slice in slices)
                {
                    if (slice.slice != null)
                    {
                        if (index > -1 && index <= slice.slice.Length)
                        {
                            if (slice.slice[index] < min)
                            {
                                min = slice.slice[index];
                            }
                        }
                    }
                }

                return min;
            }
            catch { }
            return float.NaN;
        }

        /// <summary>
        /// получить среднее значение параметра на этапе
        /// </summary>
        /// <param name="slice"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private float GetMiddle(Slice[] slice, int index)
        {
            try
            {
                if (slice != null && index > -1)
                {
                    double mean = 0.0f;
                    for (int i = 1; i < slice.Length; i++)
                    {
                        if (slice[i].slice != null && slice[i - 1].slice != null)
                        {
                            if (index <= slice[i].slice.Length && index <= slice[i - 1].slice.Length)
                            {
                                double p = (slice[i].slice[index] + slice[i - 1].slice[index]) / 2.0f;
                                double t = (double)(slice[i]._date.Ticks - slice[i - 1]._date.Ticks);

                                if (double.IsNaN(p) == false && double.IsNaN(t) == false)
                                {
                                    mean += p * t;
                                }
                            }
                        }
                    }

                    double startTime = slice[0]._date.Ticks;
                    double finishTime = slice[slice.Length - 1]._date.Ticks;

                    double result = mean / (finishTime - startTime);
                    return (float)result;
                }
            }
            catch { }
            return float.NaN;
        }

        /// <summary>
        /// Отобразить статистику и параметры в таблице
        /// </summary>
        private void InsertParametersInList()
        {
            string[] names = { "Расход", "Объем", "Давление", "Плотность", "Температура" };

            string min_string = "-----";
            string max_string = "-----";

            string sred_string = "-----";
            string plan_string = "-----";

            for (int i = 0; i < 5; i++)
            {
                ListViewItem item = new ListViewItem(names[i]);
                switch (i)
                {
                    case 0:

                        item.ForeColor = _app.Graphic_consumption.Color;

                        min_string = "-----";
                        if (float.IsNaN(minRashod) == false)
                        {
                            min_string = string.Format("{0:F2}", minRashod);
                        }

                        max_string = "-----";
                        if (float.IsNaN(maxRashod) == false)
                        {
                            max_string = string.Format("{0:F2}", maxRashod);
                        }

                        sred_string = "-----";
                        if (float.IsNaN(sredRashod) == false)
                        {
                            sred_string = string.Format("{0:F2}", sredRashod);
                        }

                        plan_string = "-----";
                        if (float.IsNaN(sel_stage.Plan_consumption) == false)
                        {
                            plan_string = string.Format("{0:F2}", sel_stage.Plan_consumption);
                        }

                        ListViewItem.ListViewSubItem max_ras = new ListViewItem.ListViewSubItem(item, max_string);
                        ListViewItem.ListViewSubItem min_ras = new ListViewItem.ListViewSubItem(item, min_string);

                        ListViewItem.ListViewSubItem sred_ras = new ListViewItem.ListViewSubItem(item, sred_string);
                        ListViewItem.ListViewSubItem plan_ras = new ListViewItem.ListViewSubItem(item, plan_string);

                        item.SubItems.Add(min_ras);
                        item.SubItems.Add(max_ras);

                        item.SubItems.Add(sred_ras);
                        item.SubItems.Add(plan_ras);

                        break;

                    case 1:

                        item.ForeColor = _app.Graphic_volume.Color;

                        min_string = "-----";
                        if (float.IsNaN(minRashod) == false)
                        {
                            min_string = string.Format("{0:F2}", minObem);
                        }

                        max_string = "-----";
                        if (float.IsNaN(maxRashod) == false)
                        {
                            max_string = string.Format("{0:F2}", maxObem);
                        }

                        sred_string = "-----";
                        if (float.IsNaN(sredRashod) == false)
                        {
                            sred_string = string.Format("{0:F2}", sredObem);
                        }

                        plan_string = "-----";
                        if (float.IsNaN(sel_stage.Plan_volume) == false)
                        {
                            plan_string = string.Format("{0:F2}", sel_stage.Plan_volume);
                        }

                        ListViewItem.ListViewSubItem max_rasVol = new ListViewItem.ListViewSubItem(item, max_string);
                        ListViewItem.ListViewSubItem min_rasVol = new ListViewItem.ListViewSubItem(item, min_string);

                        ListViewItem.ListViewSubItem sred_rasVol = new ListViewItem.ListViewSubItem(item, sred_string);
                        ListViewItem.ListViewSubItem plan_rasVol = new ListViewItem.ListViewSubItem(item, plan_string);

                        item.SubItems.Add(min_rasVol);
                        item.SubItems.Add(max_rasVol);

                        item.SubItems.Add(sred_rasVol);
                        item.SubItems.Add(plan_rasVol);

                        break;

                    case 2:

                        item.ForeColor = _app.Graphic_pressure.Color;

                        min_string = "-----";
                        if (float.IsNaN(minRashod) == false)
                        {
                            min_string = string.Format("{0:F2}", minDavlenie);
                        }

                        max_string = "-----";
                        if (float.IsNaN(maxRashod) == false)
                        {
                            max_string = string.Format("{0:F2}", maxDavlenie);
                        }

                        sred_string = "-----";
                        if (float.IsNaN(sredRashod) == false)
                        {
                            sred_string = string.Format("{0:F2}", sredDavlenie);
                        }

                        plan_string = "-----";
                        if (float.IsNaN(sel_stage.Plan_pressure) == false)
                        {
                            plan_string = string.Format("{0:F2}", sel_stage.Plan_pressure);
                        }

                        ListViewItem.ListViewSubItem max_rasDav = new ListViewItem.ListViewSubItem(item, max_string);
                        ListViewItem.ListViewSubItem min_rasDav = new ListViewItem.ListViewSubItem(item, min_string);

                        ListViewItem.ListViewSubItem sred_rasDav = new ListViewItem.ListViewSubItem(item, sred_string);
                        ListViewItem.ListViewSubItem plan_rasDav = new ListViewItem.ListViewSubItem(item, plan_string);

                        item.SubItems.Add(min_rasDav);
                        item.SubItems.Add(max_rasDav);

                        item.SubItems.Add(sred_rasDav);
                        item.SubItems.Add(plan_rasDav);

                        break;

                    case 3:

                        item.ForeColor = _app.Graphic_density.Color;

                        min_string = "-----";
                        if (float.IsNaN(minRashod) == false)
                        {
                            min_string = string.Format("{0:F2}", minPlotnost);
                        }

                        max_string = "-----";
                        if (float.IsNaN(maxRashod) == false)
                        {
                            max_string = string.Format("{0:F2}", maxPlotnost);
                        }

                        sred_string = "-----";
                        if (float.IsNaN(sredRashod) == false)
                        {
                            sred_string = string.Format("{0:F2}", sredPlotnost);
                        }

                        plan_string = "-----";
                        if (float.IsNaN(sel_stage.Plan_density) == false)
                        {
                            plan_string = string.Format("{0:F2}", sel_stage.Plan_density);
                        }

                        ListViewItem.ListViewSubItem max_rasPlo = new ListViewItem.ListViewSubItem(item, max_string);
                        ListViewItem.ListViewSubItem min_rasPlo = new ListViewItem.ListViewSubItem(item, min_string);

                        ListViewItem.ListViewSubItem sred_rasPlo = new ListViewItem.ListViewSubItem(item, sred_string);
                        ListViewItem.ListViewSubItem plan_rasPlo = new ListViewItem.ListViewSubItem(item, plan_string);

                        item.SubItems.Add(min_rasPlo);
                        item.SubItems.Add(max_rasPlo);

                        item.SubItems.Add(sred_rasPlo);
                        item.SubItems.Add(plan_rasPlo);

                        break;

                    case 4:

                        item.ForeColor = _app.Graphic_temperature.Color;

                        min_string = "-----";
                        if (float.IsNaN(minRashod) == false)
                        {
                            min_string = string.Format("{0:F2}", minTemp);
                        }

                        max_string = "-----";
                        if (float.IsNaN(maxRashod) == false)
                        {
                            max_string = string.Format("{0:F2}", maxTemp);
                        }

                        sred_string = "-----";
                        if (float.IsNaN(sredRashod) == false)
                        {
                            sred_string = string.Format("{0:F2}", sredTemp);
                        }

                        plan_string = "-----";

                        ListViewItem.ListViewSubItem max_rasTem = new ListViewItem.ListViewSubItem(item, max_string);
                        ListViewItem.ListViewSubItem min_rasTem = new ListViewItem.ListViewSubItem(item, min_string);

                        ListViewItem.ListViewSubItem sred_rasTem = new ListViewItem.ListViewSubItem(item, sred_string);
                        ListViewItem.ListViewSubItem plan_rasTem = new ListViewItem.ListViewSubItem(item, plan_string);

                        item.SubItems.Add(min_rasTem);
                        item.SubItems.Add(max_rasTem);

                        item.SubItems.Add(sred_rasTem);
                        item.SubItems.Add(plan_rasTem);

                        break;

                    default:
                        break;
                }

                listViewParameters.Items.Add(item);
            }
        }

        private void StageViewForm_Shown(object sender, EventArgs e)
        {
            ShowGraphics();
        }

        private FormWindowState lastState;
        private void StageViewForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                manager.Update();
                lastState = WindowState;
            }
            else
                if (lastState == FormWindowState.Maximized)
                {
                    manager.Update();
                    lastState = WindowState;
                }

        }
    }
}