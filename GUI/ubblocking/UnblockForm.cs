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
    public partial class UnblockForm : Form
    {
        public UnblockForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Определяет пароль на систему
        /// </summary>
        public string Password
        {
            get
            {
                return textBoxPass.Text;
            }
        }
    }
}
