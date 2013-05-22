using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SKC
{
    public partial class CurrentProjectForm : Form
    {
        private const string _frm_Plase1 = "Месторождение : {0}";
        private const string _frm_Bush1 =  "Куст          : {0}";
        private const string _frm_Well1 =  "Скважина      : {0}";
        private const string _frm_Job1 =   "Задание       : {0}";
        private const string _frm_Splt1 = " -----------------------------------";
        private const string _frm_Creat1 = "Дата создания Задания  : {0}";
        private const string _frm_Workd1 = "Дата начала работы     : {0}";
        private const string _frm_Finsh1 = "Дата завершения работы : {0}";
        private const string _frm_DBnam1 = "Имя Базы Данных        : {0}";

        Application _app = null;

        public CurrentProjectForm()
        {
            InitializeComponent();

            _app = Application.CreateInstance();
        }

        private void CurrentProjectForm_Shown(object sender, EventArgs e)
        {
            if (_app != null)
            {
                Project current = _app.CurrentProject;
                if (current != null)
                {
                    string s1 = current.Created.ToString();
                    string s2 = string.Empty; if (current.Worked != DateTime.MinValue) s2 = current.Worked.ToString();
                    string s3 = string.Empty; if (current.Finish != DateTime.MinValue) s3 = current.Finish.ToString();

                    textBoxDescription.Text = string.Concat(string.Format(_frm_Plase1, current.Place),
                                                        Constants.vbCrLf, string.Format(_frm_Bush1, current.Bush),
                                                        Constants.vbCrLf, string.Format(_frm_Well1, current.Well),
                                                        Constants.vbCrLf, string.Format(_frm_Job1, current.Job),
                                                        Constants.vbCrLf, _frm_Splt1,
                                                        Constants.vbCrLf, string.Format(_frm_Creat1, s1),
                                                        Constants.vbCrLf, string.Format(_frm_Workd1, s2),
                                                        Constants.vbCrLf, string.Format(_frm_Finsh1, s3),
                                                        Constants.vbCrLf, _frm_Splt1,
                                                        Constants.vbCrLf, string.Format(_frm_DBnam1, current.DB_Name)
                                                       );
                }
            }
        }
    }
}