using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DataBase;
using Buffering;
using GraphicComponent;

using WCF;
using WCF.WCF_Client;

namespace SKC
{
    public partial class ParametersViewForm : Form
    {
        private Application _app = null;                    // основное приложение

        private GraphicManager manager;                     // управляет отрисовкой параметров
        private Graphic[] graphics = null;                  // отображаемые графики

        private ParametersViewPanel panelView;              // отображаемая панель

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        public ParametersViewForm()
        {
            InitializeComponent();

            _app = Application.CreateInstance();
            manager = graphicsSheet1.InstanceManager();

            manager.Mode = GraphicComponent.DrawMode.PassivScroll;
            manager.Orientation = GraphicComponent.Orientation.Horizontal;

            manager.UpdatePeriod = 500;

            graphics = new Graphic[5];
            for (int index = 0; index < graphics.Length; index++)
            {
                graphics[index] = manager.InstanceGraphic();
                
                graphics[index].Range.Min = 0;
                graphics[index].Range.Max = 65535;
            }
        }

        /// <summary>
        /// Загрузить данные для отображения
        /// </summary>
        private void LoadData()
        {
            try
            {
                if (_app != null)
                {
                    Slice[] slices = _app.getData(DateTime.MinValue, DateTime.MaxValue);
                    if (slices != null)
                    {
                        foreach (Graphic graphic in graphics)
                        {
                            graphic.Clear();
                        }

                        if (panelView != null)
                        {
                            DateTime _minT = _app.Commutator.MinTimeParameter();
                            foreach (Slice slice in slices)
                            {
                                if (slice.slice != null)
                                {
                                    int sliceLen = slice.slice.Length;
                                    if (panelView.Parameter1.Parameter != null)
                                    {
                                        int index = panelView.Parameter1.Parameter.Channel.Number;
                                        if (index > -1 && index < slice.slice.Length)
                                        {
                                            graphics[0].Insert(slice._date, slice.slice[index]);
                                            graphics[0].Tmin = _minT;
                                        }

                                        graphics[0].Units = string.Format("[{0}]", panelView.Parameter1.Parameter.Units);
                                        graphics[0].Description = panelView.Parameter1.Parameter.Description;
                                    }

                                    if (panelView.Parameter2.Parameter != null)
                                    {
                                        int index = panelView.Parameter2.Parameter.Channel.Number;
                                        if (index > -1 && index < slice.slice.Length)
                                        {
                                            graphics[1].Insert(slice._date, slice.slice[index]);
                                            graphics[1].Tmin = _minT;
                                        }

                                        graphics[1].Units = string.Format("[{0}]", panelView.Parameter2.Parameter.Units);
                                        graphics[1].Description = panelView.Parameter2.Parameter.Description;
                                    }

                                    if (panelView.Parameter3.Parameter != null)
                                    {
                                        int index = panelView.Parameter3.Parameter.Channel.Number;
                                        if (index > -1 && index < slice.slice.Length)
                                        {
                                            graphics[2].Insert(slice._date, slice.slice[index]);
                                            graphics[2].Tmin = _minT;
                                        }

                                        graphics[2].Units = string.Format("[{0}]", panelView.Parameter3.Parameter.Units);
                                        graphics[2].Description = panelView.Parameter3.Parameter.Description;
                                    }

                                    if (panelView.Parameter4.Parameter != null)
                                    {
                                        int index = panelView.Parameter4.Parameter.Channel.Number;
                                        if (index > -1 && index < slice.slice.Length)
                                        {
                                            graphics[3].Insert(slice._date, slice.slice[index]);
                                            graphics[3].Tmin = _minT;
                                        }

                                        graphics[3].Units = string.Format("[{0}]", panelView.Parameter4.Parameter.Units);
                                        graphics[3].Description = panelView.Parameter4.Parameter.Description;
                                    }

                                    if (panelView.Parameter5.Parameter != null)
                                    {
                                        int index = panelView.Parameter5.Parameter.Channel.Number;
                                        if (index > -1 && index < slice.slice.Length)
                                        {
                                            graphics[4].Insert(slice._date, slice.slice[index]);
                                            graphics[4].Tmin = _minT;
                                        }

                                        graphics[4].Units = string.Format("[{0}]", panelView.Parameter5.Parameter.Units);
                                        graphics[4].Description = panelView.Parameter5.Parameter.Description;
                                    }
                                }
                            }

                            manager.StartTime = _minT;
                        }

                        graphics[0].Current = float.NaN;
                        graphics[1].Current = float.NaN;
                        graphics[2].Current = float.NaN;
                        graphics[3].Current = float.NaN;
                        graphics[4].Current = float.NaN;
                    }
                }
                else
                {
                    _app = Application.CreateInstance();
                }
            }
            catch { }            
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParametersViewForm_Load(object sender, EventArgs e)
        {
            foreach (ParametersViewPanel panel in _app.Panels)
            {
                comboBoxPanels.Items.Add(panel.Description);
            }
        }

        /// <summary>
        /// Выбрали панель для отображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxPanels_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < graphics.Length; i++)
                {
                    graphics[i].Description = string.Empty;
                    graphics[i].Units = string.Empty;
                }

                int selectedpanel_index = comboBoxPanels.SelectedIndex;
                if (selectedpanel_index > -1 && selectedpanel_index < _app.Panels.Count)
                {
                    panelView = _app.Panels[selectedpanel_index];
                    if (panelView != null)
                    {
                        panelView.Parameter1.Graphic = graphics[0];
                        panelView.Parameter2.Graphic = graphics[1];

                        panelView.Parameter3.Graphic = graphics[2];
                        panelView.Parameter4.Graphic = graphics[3];

                        panelView.Parameter5.Graphic = graphics[4];
                    }
                }

                LoadData();
                manager.Update();
            }
            catch { }
        }

        /// <summary>
        /// отобразить данные начиная со времени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                manager.Update();
            }
            catch { }
        }

        private FormWindowState lastState;
        private void ParametersViewForm_SizeChanged(object sender, EventArgs e)
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