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
    public partial class BlockCommandForm : Form
    {
        public BlockCommandForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Определяет команду DSN
        /// </summary>
        public string CommandDSN
        {
            get
            {
                return textBoxCommand.Text;
            }

            set
            {
                textBoxCommand.Text = value;
            }
        }

        /// <summary>
        /// Определяет использовать или нет команду для сброса объема
        /// </summary>
        public bool UseReset
        {
            get
            {
                return checkBoxuseForReset.Checked;
            }

            set
            {
                checkBoxuseForReset.Checked = value;
            }
        }

        /// <summary>
        /// Определяет использовать или нет команду для перехода на новый этап
        /// </summary>
        public bool UseNext
        {
            get
            {
                return checkBoxuseForNext.Checked;
            }

            set
            {
                checkBoxuseForNext.Checked = value;
            }
        }
    }
}
