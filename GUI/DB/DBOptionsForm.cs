using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;


namespace SKC
{
    public partial class DBOptionsForm : Form
    {
        protected Application _app = null;

        public DBOptionsForm()
        {
            InitializeComponent();
            _app = Application.CreateInstance();
            
        }

        /// <summary>
        /// запрашивем список серверов БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            ShowTimeFormDB frm = new ShowTimeFormDB();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                System.Data.DataTable table = frm.DataTables;

                dataGridView1.DataSource = table;

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;

                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridView1.ReadOnly = true;
            }
        }

        /// <summary>
        /// показать пароль или нет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox check = sender as CheckBox;
            if (check != null)
            {
                switch (check.Checked)
                {
                    case true:

                        textBoxPassword.UseSystemPasswordChar = false;
                        break;

                    case false:

                        textBoxPassword.UseSystemPasswordChar = true;
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// проверить соединение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheckDBState_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxDataSource.Text == string.Empty)
                {
                    MessageBox.Show(this, "Введине сервер БД", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (textBoxUserID.Text == string.Empty)
                {
                    MessageBox.Show(this, "Введине логин пользователя БД", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string connectionString = string.Format("Data Source={0};User ID={1};Password={2}", 
                    textBoxDataSource.Text,textBoxUserID.Text, textBoxPassword.Text);

                if (IsConnectValid(connectionString))
                {
                    MessageBox.Show(this, "Соединение установлено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(this, "Соединение не установлено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Осуществляет проверку подключения к серверу БД
        /// </summary>
        public bool IsConnectValid(string ConnectionStringToServer)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConnectionStringToServer);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        SqlConnection.ClearPool(connection);
                    }

                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// выделили строку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string firstSource = dataGridView1[0, e.RowIndex].Value.ToString();
                string secondSource = dataGridView1[1, e.RowIndex].Value.ToString();

                if (secondSource != string.Empty)
                {
                    textBoxDataSource.Text = string.Format("{0}\\{1}", firstSource, secondSource);
                }
                else
                    textBoxDataSource.Text = firstSource;
            }
            catch { }
        }

        /// <summary>
        /// проверить введенные данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accept_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxDataSource.Text == string.Empty)
                {
                    MessageBox.Show(this, "Не указан сервер БД", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DialogResult = System.Windows.Forms.DialogResult.None;
                }

                if (textBoxUserID.Text == string.Empty)
                {
                    MessageBox.Show(this, "Не указан логин пользователя БД", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DialogResult = System.Windows.Forms.DialogResult.None;
                }
            }
            catch { }
        }

        /// <summary>
        /// Возвращяет источник данных
        /// </summary>
        public string DataSource
        {
            get
            {
                return textBoxDataSource.Text;
            }
        }

        /// <summary>
        /// Возвращяет логин пользователя БД
        /// </summary>
        public string UserID
        {
            get
            {
                return textBoxUserID.Text;
            }
        }

        /// <summary>
        /// Возвращяет пароль пользователя БД
        /// </summary>
        public string Password
        {
            get
            {
                return textBoxPassword.Text;
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBOptionsForm_Load(object sender, EventArgs e)
        {
            try
            {
                textBoxDataSource.Text = _app.Manager.DataSource;

                textBoxUserID.Text = _app.Manager.UserID;
                textBoxPassword.Text = _app.Manager.Password;
            }
            catch { }
        }
    }
}