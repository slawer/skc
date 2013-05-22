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
    public partial class ResetterOfVolumeForm : Form
    {
        private Application _app = null;

        public ResetterOfVolumeForm()
        {
            InitializeComponent();
            _app = Application.CreateInstance();
        }

        /// <summary>
        /// Загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetterOfVolumeForm_Load(object sender, EventArgs e)
        {
            if (_app != null)
            {
                foreach (BlockViewCommand command in _app.Commands)
                {
                    InsertToiList(command);
                }
            }

            listView1.ItemChecked +=new ItemCheckedEventHandler(listView1_ItemChecked);
        }

        /// <summary>
        /// добавить команду
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            BlockCommandForm frm = new BlockCommandForm();

            frm.Text = "Добавление команды БО";
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                BlockViewCommand command = new BlockViewCommand();

                command.CommandDsn = frm.CommandDSN;
                
                command.UseForReset = frm.UseReset;
                command.UseForNextStage = frm.UseNext;

                if (_app != null)
                {
                    _app.Commands.Add(command);
                    InsertToiList(command);
                }
            }
        }

        /// <summary>
        /// редактировать команду
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
            {
                BlockViewCommand command = listView1.SelectedItems[0].Tag as BlockViewCommand;
                if (command != null)
                {
                    BlockCommandForm frm = new BlockCommandForm();

                    frm.Text = "Редактирование команды БО";

                    frm.CommandDSN = command.CommandDsn;

                    frm.UseReset = command.UseForReset;
                    frm.UseNext = command.UseForNextStage;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        command.CommandDsn = frm.CommandDSN;

                        command.UseForReset = frm.UseReset;
                        command.UseForNextStage = frm.UseNext;

                        listView1.SelectedItems[0].SubItems[1].Text = command.CommandDsn;
                        listView1.SelectedItems[0].SubItems[2].Text = command.TextType;
                    }
                }
            }
        }

        /// <summary>
        /// удалить команду
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
                {
                    BlockViewCommand command = listView1.SelectedItems[0].Tag as BlockViewCommand;
                    if (command != null)
                    {
                        _app.Commands.Remove(command);
                        listView1.Items.Remove(listView1.SelectedItems[0]);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Добавить команду в список
        /// </summary>
        /// <param name="command">Добавляемая в список команда</param>
        protected void InsertToiList(BlockViewCommand command)
        {
            ListViewItem item = new ListViewItem();

            ListViewItem.ListViewSubItem textItem = new ListViewItem.ListViewSubItem(item, command.CommandDsn);
            ListViewItem.ListViewSubItem typeItem = new ListViewItem.ListViewSubItem(item, command.TextType);

            if (command.Actived)
            {
                item.Checked = true;
            }
            else
                item.Checked = false;

            item.SubItems.Add(textItem);
            item.SubItems.Add(typeItem);

            item.Tag = command;
            listView1.Items.Add(item);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                BlockViewCommand cmd = e.Item.Tag as BlockViewCommand;
                if (cmd != null)
                {
                    cmd.Actived = e.Item.Checked;
                }
            }
            catch { }
        }
    }
}