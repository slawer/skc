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

using DisplayComponent;

namespace SKC
{
    public partial class KRSForm : Form
    {
        private Application _app = null;                    // основное приложение

        private GraphicManager manager;                     // управляет отрисовкой параметров
        private Graphic[] graphics = null;                  // отображаемые графики

        private ParametersViewPanel panelView;              // отображаемая панель

        public KRSForm(DateTime _now)
        {
            InitializeComponent();

            _app = Application.CreateInstance();

            manager = graphicsSheet1.InstanceManager();
            manager.StartTime = _app.Commutator.MinTimeParameter();
/*
            if (manager.StartTime == DateTime.MinValue)
            {
                manager.StartTime = DateTime.Now;
            }
*/
            manager.OnDataNeed += new EventHandler(manager_OnDataNeed);
            manager.Orientation = GraphicComponent.Orientation.Horizontal;

            graphics = new Graphic[5];
            for (int index = 0; index < graphics.Length; index++)
            {
                graphics[index] = manager.InstanceGraphic();

                graphics[index].Range.Min = 0;
                graphics[index].Range.Max = 65535;
            }

            manager.UpdatePeriod = 500;
            numericUpDown1.Value = 500;
        }

        private void KRSForm_Load(object sender, EventArgs e)
        {
            foreach (ParametersViewPanel panel in _app.Panels)
            {
                comboBoxPanels.Items.Add(panel.Description);
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

                                        if (digitDisplay1.Digits.Count > 0)
                                        {
                                            digitDisplay1.Digits[0].Current = slice.slice[index];
                                        }
                                    }
                                    else
                                    {
                                        if (digitDisplay1.Digits.Count > 0)
                                        {
                                            digitDisplay1.Digits[0].Index = -1;
                                        }
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

                                        if (digitDisplay1.Digits.Count > 1)
                                        {
                                            digitDisplay1.Digits[1].Current = slice.slice[index];
                                        }
                                    }
                                    else
                                    {
                                        if (digitDisplay1.Digits.Count > 1)
                                        {
                                            digitDisplay1.Digits[1].Index = -1;
                                        }
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

                                        if (digitDisplay1.Digits.Count > 2)
                                        {
                                            digitDisplay1.Digits[2].Current = slice.slice[index];
                                        }
                                    }
                                    else
                                    {
                                        if (digitDisplay1.Digits.Count > 2)
                                        {
                                            digitDisplay1.Digits[2].Index = -1;
                                        }
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

                                        if (digitDisplay1.Digits.Count > 3)
                                        {
                                            digitDisplay1.Digits[3].Current = slice.slice[index];
                                        }
                                    }
                                    else
                                    {
                                        if (digitDisplay1.Digits.Count > 3)
                                        {
                                            digitDisplay1.Digits[3].Index = -1;
                                        }
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

                                        if (digitDisplay1.Digits.Count > 4)
                                        {
                                            digitDisplay1.Digits[4].Current = slice.slice[index];
                                        }
                                    }
                                    else
                                    {
                                        if (digitDisplay1.Digits.Count > 4)
                                        {
                                            digitDisplay1.Digits[4].Index = -1;
                                        }
                                    }
                                }
                            }
                        }

                        digitDisplay1.Render();
                    }
                }
                else
                {
                    _app = Application.CreateInstance();
                }
            }
            catch { }
        }

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
                        digitDisplay1.Digits.Clear();

                        panelView.Parameter1.Graphic = graphics[0];
                        
                        Digit dig_1 = new Digit();

                        dig_1.Color = panelView.Parameter1.Graphic.Color;
                        dig_1.Description = panelView.Parameter1.Graphic.Description;

                        dig_1.Index = 0;                        
                        digitDisplay1.Digits.Add(dig_1);
                        

                        panelView.Parameter2.Graphic = graphics[1];
                        
                        Digit dig_2 = new Digit();

                        dig_2.Color = panelView.Parameter2.Graphic.Color;
                        dig_2.Description = panelView.Parameter2.Graphic.Description;

                        dig_2.Index = 0;
                        digitDisplay1.Digits.Add(dig_2);

                        panelView.Parameter3.Graphic = graphics[2];

                        Digit dig_3 = new Digit();

                        dig_3.Color = panelView.Parameter3.Graphic.Color;
                        dig_3.Description = panelView.Parameter3.Graphic.Description;

                        dig_3.Index = 0;
                        digitDisplay1.Digits.Add(dig_3);

                        panelView.Parameter4.Graphic = graphics[3];

                        Digit dig_4 = new Digit();

                        dig_4.Color = panelView.Parameter4.Graphic.Color;
                        dig_4.Description = panelView.Parameter4.Graphic.Description;

                        dig_4.Index = 0;
                        digitDisplay1.Digits.Add(dig_4);

                        panelView.Parameter5.Graphic = graphics[4];

                        Digit dig_5 = new Digit();

                        dig_5.Color = panelView.Parameter5.Graphic.Color;
                        dig_5.Description = panelView.Parameter5.Graphic.Description;

                        dig_5.Index = 0;
                        digitDisplay1.Digits.Add(dig_5);
                    }
                }

                manager.Mode = GraphicComponent.DrawMode.Activ;
                manager.updTime();
                
                manager.Update();
            }
            catch { }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            manager.UpdatePeriod = (int)numericUpDown1.Value;
        }

        private void KRSForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            manager.Mode = GraphicComponent.DrawMode.Passive;
            manager.OnDataNeed -= manager_OnDataNeed;
        }
    }
}