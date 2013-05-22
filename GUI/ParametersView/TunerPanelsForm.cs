using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SKC
{
    public partial class TunerPanelsForm : Form
    {
        private Application _app = null;                    // основное приложение
        private ParametersViewPanel selectedPanel = null;   // выбранная панель

        public TunerPanelsForm()
        {
            InitializeComponent();
            _app = Application.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunerPanelsForm_Load(object sender, EventArgs e)
        {
            foreach (ParametersViewPanel panel in _app.Panels)
            {
                InsertPanelInlistView(panel);
            }
        }

        /// <summary>
        /// выбрали панель в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewPanels_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listViewPanels.SelectedItems != null)
                {
                    if (listViewPanels.SelectedItems.Count > 0)
                    {
                        ParametersViewPanel selected = listViewPanels.SelectedItems[0].Tag as ParametersViewPanel;
                        if (selected != null)
                        {
                            selectedPanel = selected;
                            ShowSelectedPanel();
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// определяем первый параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonP1_Click(object sender, EventArgs e)
        {
            ParametersListForm frm = new ParametersListForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter selected = frm.SelectedParameter;
                if (selected != null)
                {
                    if (selectedPanel != null)
                    {
                        selectedPanel.Parameter1.Parameter = selected;
                    }
                    else
                    {
                        GraphicPanelParameter par = new GraphicPanelParameter(selected);

                        textBoxP1.Text = selected.Name;
                        textBoxP1.Tag = par;
                    }

                    ShowSelectedPanel();
                }
            }
        }

        /// <summary>
        /// редактируем первый параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditP1_Click(object sender, EventArgs e)
        {
            if (selectedPanel != null && selectedPanel.Parameter1.Parameter != null)
            {
                EditParameterPanelForm frm = new EditParameterPanelForm();

                frm.ParameterDescription = selectedPanel.Parameter1.Parameter.Name;
                
                frm.ParameterMin = selectedPanel.Parameter1.Min;
                frm.ParameterMax = selectedPanel.Parameter1.Max;

                frm.ParameterColor = selectedPanel.Parameter1.Color;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    selectedPanel.Parameter1.Min = frm.ParameterMin;
                    selectedPanel.Parameter1.Max = frm.ParameterMax;

                    selectedPanel.Parameter1.Color = frm.ParameterColor;
                }
            }
            else
            {
                GraphicPanelParameter selPar = textBoxP1.Tag as GraphicPanelParameter;
                if (selPar != null && selPar.Parameter != null)
                {
                    EditParameterPanelForm frm = new EditParameterPanelForm();

                    frm.ParameterDescription = selPar.Parameter.Name;

                    frm.ParameterMin = selPar.Min;
                    frm.ParameterMax = selPar.Max;

                    frm.ParameterColor = selPar.Color;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        selPar.Min = frm.ParameterMin;
                        selPar.Max = frm.ParameterMax;

                        selPar.Color = frm.ParameterColor;
                    }
                }
            }
        }

        /// <summary>
        /// определяем второй параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonP2_Click(object sender, EventArgs e)
        {
            ParametersListForm frm = new ParametersListForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter selected = frm.SelectedParameter;
                if (selected != null)
                {
                    if (selectedPanel != null)
                    {
                        selectedPanel.Parameter2.Parameter = selected;
                    }
                    else
                    {
                        GraphicPanelParameter par = new GraphicPanelParameter(selected);

                        textBoxP2.Text = selected.Name;
                        textBoxP2.Tag = par;
                    }

                    ShowSelectedPanel();
                }
            }
        }

        /// <summary>
        /// редактируем второй параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditP2_Click(object sender, EventArgs e)
        {
            if (selectedPanel != null && selectedPanel.Parameter2.Parameter != null)
            {
                EditParameterPanelForm frm = new EditParameterPanelForm();

                frm.ParameterDescription = selectedPanel.Parameter2.Parameter.Name;

                frm.ParameterMin = selectedPanel.Parameter2.Min;
                frm.ParameterMax = selectedPanel.Parameter2.Max;

                frm.ParameterColor = selectedPanel.Parameter2.Color;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    selectedPanel.Parameter2.Min = frm.ParameterMin;
                    selectedPanel.Parameter2.Max = frm.ParameterMax;

                    selectedPanel.Parameter2.Color = frm.ParameterColor;
                }
            }
            else
            {
                GraphicPanelParameter selPar = textBoxP2.Tag as GraphicPanelParameter;
                if (selPar != null && selPar.Parameter != null)
                {
                    EditParameterPanelForm frm = new EditParameterPanelForm();

                    frm.ParameterDescription = selPar.Parameter.Name;

                    frm.ParameterMin = selPar.Min;
                    frm.ParameterMax = selPar.Max;

                    frm.ParameterColor = selPar.Color;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        selPar.Min = frm.ParameterMin;
                        selPar.Max = frm.ParameterMax;

                        selPar.Color = frm.ParameterColor;
                    }
                }
            }
        }

        /// <summary>
        /// определяем третий параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonP3_Click(object sender, EventArgs e)
        {
            ParametersListForm frm = new ParametersListForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter selected = frm.SelectedParameter;
                if (selected != null)
                {
                    if (selectedPanel != null)
                    {
                        selectedPanel.Parameter3.Parameter = selected;
                    }
                    else
                    {
                        GraphicPanelParameter par = new GraphicPanelParameter(selected);

                        textBoxP3.Text = selected.Name;
                        textBoxP3.Tag = par;
                    }

                    ShowSelectedPanel();
                }
            }
        }

        /// <summary>
        /// редактируем третий параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditP3_Click(object sender, EventArgs e)
        {
            if (selectedPanel != null && selectedPanel.Parameter3.Parameter != null)
            {
                EditParameterPanelForm frm = new EditParameterPanelForm();

                frm.ParameterDescription = selectedPanel.Parameter3.Parameter.Name;

                frm.ParameterMin = selectedPanel.Parameter3.Min;
                frm.ParameterMax = selectedPanel.Parameter3.Max;

                frm.ParameterColor = selectedPanel.Parameter3.Color;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    selectedPanel.Parameter3.Min = frm.ParameterMin;
                    selectedPanel.Parameter3.Max = frm.ParameterMax;

                    selectedPanel.Parameter3.Color = frm.ParameterColor;
                }
            }
            else
            {
                GraphicPanelParameter selPar = textBoxP3.Tag as GraphicPanelParameter;
                if (selPar != null && selPar.Parameter != null)
                {
                    EditParameterPanelForm frm = new EditParameterPanelForm();

                    frm.ParameterDescription = selPar.Parameter.Name;

                    frm.ParameterMin = selPar.Min;
                    frm.ParameterMax = selPar.Max;

                    frm.ParameterColor = selPar.Color;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        selPar.Min = frm.ParameterMin;
                        selPar.Max = frm.ParameterMax;

                        selPar.Color = frm.ParameterColor;
                    }
                }
            }
        }

        /// <summary>
        /// определяем четвертый параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonP4_Click(object sender, EventArgs e)
        {
            ParametersListForm frm = new ParametersListForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter selected = frm.SelectedParameter;
                if (selected != null)
                {
                    if (selectedPanel != null)
                    {
                        selectedPanel.Parameter4.Parameter = selected;
                    }
                    else
                    {
                        GraphicPanelParameter par = new GraphicPanelParameter(selected);

                        textBoxP4.Text = selected.Name;
                        textBoxP4.Tag = par;
                    }

                    ShowSelectedPanel();
                }
            }
        }

        /// <summary>
        /// редактируем четвертый параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditP4_Click(object sender, EventArgs e)
        {
            if (selectedPanel != null && selectedPanel.Parameter4.Parameter != null)
            {
                EditParameterPanelForm frm = new EditParameterPanelForm();
                if (selectedPanel.Parameter4.Parameter != null)
                {
                    frm.ParameterDescription = selectedPanel.Parameter4.Parameter.Name;

                    frm.ParameterMin = selectedPanel.Parameter4.Min;
                    frm.ParameterMax = selectedPanel.Parameter4.Max;

                    frm.ParameterColor = selectedPanel.Parameter4.Color;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        selectedPanel.Parameter4.Min = frm.ParameterMin;
                        selectedPanel.Parameter4.Max = frm.ParameterMax;

                        selectedPanel.Parameter4.Color = frm.ParameterColor;
                    }
                }
            }
            else
            {
                GraphicPanelParameter selPar = textBoxP4.Tag as GraphicPanelParameter;
                if (selPar != null && selPar.Parameter != null)
                {
                    EditParameterPanelForm frm = new EditParameterPanelForm();

                    frm.ParameterDescription = selPar.Parameter.Name;

                    frm.ParameterMin = selPar.Min;
                    frm.ParameterMax = selPar.Max;

                    frm.ParameterColor = selPar.Color;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        selPar.Min = frm.ParameterMin;
                        selPar.Max = frm.ParameterMax;

                        selPar.Color = frm.ParameterColor;
                    }
                }
            }
        }

        /// <summary>
        /// определяем пятый параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonP5_Click(object sender, EventArgs e)
        {
            ParametersListForm frm = new ParametersListForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter selected = frm.SelectedParameter;
                if (selected != null)
                {
                    if (selectedPanel != null)
                    {
                        selectedPanel.Parameter5.Parameter = selected;
                    }
                    else
                    {
                        GraphicPanelParameter par = new GraphicPanelParameter(selected);
                        textBoxP5.Text = selected.Name;
                        textBoxP5.Tag = par;
                    }

                    ShowSelectedPanel();
                }
            }
        }

        /// <summary>
        /// редактируем пятый параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditP5_Click(object sender, EventArgs e)
        {
            if (selectedPanel != null && selectedPanel.Parameter5.Parameter != null)
            {
                EditParameterPanelForm frm = new EditParameterPanelForm();

                frm.ParameterDescription = selectedPanel.Parameter5.Parameter.Name;

                frm.ParameterMin = selectedPanel.Parameter5.Min;
                frm.ParameterMax = selectedPanel.Parameter5.Max;

                frm.ParameterColor = selectedPanel.Parameter5.Color;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    selectedPanel.Parameter5.Min = frm.ParameterMin;
                    selectedPanel.Parameter5.Max = frm.ParameterMax;

                    selectedPanel.Parameter5.Color = frm.ParameterColor;
                }
            }
            else
            {
                GraphicPanelParameter selPar = textBoxP5.Tag as GraphicPanelParameter;
                if (selPar != null && selPar.Parameter != null)
                {
                    EditParameterPanelForm frm = new EditParameterPanelForm();

                    frm.ParameterDescription = selPar.Parameter.Name;

                    frm.ParameterMin = selPar.Min;
                    frm.ParameterMax = selPar.Max;

                    frm.ParameterColor = selPar.Color;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        selPar.Min = frm.ParameterMin;
                        selPar.Max = frm.ParameterMax;

                        selPar.Color = frm.ParameterColor;
                    }
                }
            }
        }

        /// <summary>
        /// добавить панель для отображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInsertNewPanel_Click(object sender, EventArgs e)
        {
            if (selectedPanel == null)
            {
                ParametersViewPanel newPanel = new ParametersViewPanel();

                newPanel.Description = textBoxDescription.Text;

                if (textBoxP1.Tag != null) newPanel.Parameter1 = textBoxP1.Tag as GraphicPanelParameter;
                if (textBoxP2.Tag != null) newPanel.Parameter2 = textBoxP2.Tag as GraphicPanelParameter;

                if (textBoxP3.Tag != null) newPanel.Parameter3 = textBoxP3.Tag as GraphicPanelParameter;
                if (textBoxP4.Tag != null) newPanel.Parameter4 = textBoxP4.Tag as GraphicPanelParameter;

                if (textBoxP5.Tag != null) newPanel.Parameter5 = textBoxP5.Tag as GraphicPanelParameter;

                _app.Panels.Add(newPanel);
                InsertPanelInlistView(newPanel);

                selectedPanel = newPanel;
            }
            else
            {
                MessageBox.Show(this, "", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// удаляем панель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemovePanel_Click(object sender, EventArgs e)
        {
            if (listViewPanels.SelectedItems != null)
            {
                if (listViewPanels.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in listViewPanels.SelectedItems)
                    {
                        ParametersViewPanel panel = item.Tag as ParametersViewPanel;
                        if (panel != null)
                        {
                            _app.Panels.Remove(panel);
                            listViewPanels.Items.Remove(item);

                            buttonClearOptions_Click(null, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// редакритуем описание панели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            if (selectedPanel != null)
            {
                selectedPanel.Description = textBoxDescription.Text;
                foreach (ListViewItem item in listViewPanels.Items)
                {
                    ParametersViewPanel itPanel = item.Tag as ParametersViewPanel;
                    if (itPanel != null)
                    {
                        if (itPanel == selectedPanel)
                        {
                            item.SubItems[1].Text = selectedPanel.Description;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// очистить настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearOptions_Click(object sender, EventArgs e)
        {
            if (selectedPanel != null)
            {
                selectedPanel = null;

                textBoxP1.Tag = null;
                textBoxP1.Text = string.Empty;

                textBoxP2.Tag = null;
                textBoxP2.Text = string.Empty;

                textBoxP3.Tag = null;
                textBoxP3.Text = string.Empty;

                textBoxP4.Tag = null;
                textBoxP4.Text = string.Empty;

                textBoxP5.Tag = null;
                textBoxP5.Text = string.Empty;

                textBoxDescription.Text = string.Empty;
            }
        }

        /// <summary>
        /// Добавить панель в список панелей
        /// </summary>
        /// <param name="panel">Добавляемая панель</param>
        protected void InsertPanelInlistView(ParametersViewPanel panel)
        {
            ListViewItem item = new ListViewItem((listViewPanels.Items.Count + 1).ToString());
            ListViewItem.ListViewSubItem desc = new ListViewItem.ListViewSubItem(item, panel.Description);

            item.SubItems.Add(desc);
            item.Tag = panel;

            listViewPanels.Items.Add(item);
        }

        /// <summary>
        /// Отобразить выбранную панель
        /// </summary>
        protected void ShowSelectedPanel()
        {
            if (selectedPanel != null)
            {
                textBoxP1.Text = string.Empty;
                textBoxP2.Text = string.Empty;
                textBoxP3.Text = string.Empty;
                textBoxP4.Text = string.Empty;
                textBoxP5.Text = string.Empty;

                textBoxP1.Tag = selectedPanel.Parameter1;
                if (selectedPanel.Parameter1.Parameter != null) textBoxP1.Text = selectedPanel.Parameter1.Parameter.Name;

                textBoxP2.Tag = selectedPanel.Parameter2;
                if (selectedPanel.Parameter2.Parameter != null) textBoxP2.Text = selectedPanel.Parameter2.Parameter.Name;

                textBoxP3.Tag = selectedPanel.Parameter3;
                if (selectedPanel.Parameter3.Parameter != null) textBoxP3.Text = selectedPanel.Parameter3.Parameter.Name;

                textBoxP4.Tag = selectedPanel.Parameter4;
                if (selectedPanel.Parameter4.Parameter != null) textBoxP4.Text = selectedPanel.Parameter4.Parameter.Name;

                textBoxP5.Tag = selectedPanel.Parameter5;
                if (selectedPanel.Parameter5.Parameter != null) textBoxP5.Text = selectedPanel.Parameter5.Parameter.Name;

                textBoxDescription.Text = selectedPanel.Description;
            }
        }
    }
}