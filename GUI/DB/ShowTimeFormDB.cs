using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;

using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace SKC
{
    public partial class ShowTimeFormDB : Form
    {
        private DataTable enums;

        private IAsyncResult iResult = null;
        private FinderDelegate finder = null;
        
        private bool closed = false;


        public ShowTimeFormDB()
        {
            InitializeComponent();

            finder = new FinderDelegate(FinderFunction);
            iResult = finder.BeginInvoke(new AsyncCallback(CallBack), null);

            if (iResult.AsyncWaitHandle.WaitOne(1500))
            {
                closed = true;
            }
        }

        /// <summary>
        /// Найденные компьютеры в сети
        /// </summary>
        public DataTable DataTables
        {
            get { return enums; }
        }

        protected delegate void FinderDelegate();
        protected void FinderFunction()
        {
            enums = SqlDataSourceEnumerator.Instance.GetDataSources();
        }

        protected void CallBack(IAsyncResult iResult)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ShowTimeForm_Load(object sender, EventArgs e)
        {
            if (closed)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                Opacity = 1.0f;
        }
    }
}