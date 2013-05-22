using System;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;

namespace SKC
{
    static class Program
    {
        /// <summary>
        /// Идентификационный номер системного мьютекса, по которому определяется запещоно или нет приложение
        /// </summary>
        private const string identifier = "932202f8-5aad-4747-b18c-89d374fb212f";

        private static Mutex mutex = null;              // определяет запуск приложения
        private static bool isNotRunning = false;       // содержит значение на основе которого определяет возможность запуска программы        

        private static Application app = null;          // основное приложение

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                mutex = new Mutex(true, identifier, out isNotRunning);
                if (isNotRunning)
                {
                    app = Application.CreateInstance();
                    if (app != null)
                    {             
                        System.Windows.Forms.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

                        System.Windows.Forms.Application.EnableVisualStyles();
                        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                        
                        System.Windows.Forms.Application.Run(new Form1());
                    }
                }
                else
                    MessageBox.Show("Приложение уже запущено", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        /// <summary>
        /// приложение завершает свою работу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            app.Save();
        }
    }
}