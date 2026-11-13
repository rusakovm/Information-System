using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    /*
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }*/
    static class Program
    {
        public static string ConnectionString { get; private set; }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConnectionString = ConfigurationManager.AppSettings["DefaultConnection"];



            // Показываем форму подключения перед главной формой
            while (!AppConnection.IsConnectionStringSet)
            {
                using (var connectionForm = new ConnectionForm())
                {
                    if (connectionForm.ShowDialog() != DialogResult.OK)
                    {
                        if (MessageBox.Show("Не удалось подключиться к БД. Выйти из приложения?",
                                            "Ошибка подключения",
                                            MessageBoxButtons.YesNo) == DialogResult.Yes)
                            return;          // закрываем приложение
                    }
                }
            }

            if (!AppConnection.IsConnectionStringSet)
            {
                // Если строка подключения не установлена, показываем форму настройки
                var connectionForm = new ConnectionForm();
                if (connectionForm.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Не удалось установить подключение к базе данных.");
                    return;
                }
            }

            /* ---------- 1. Показываем форму авторизации модально ---------- */
            using (var login = new Form1())          // ← Form1 = LoginForm
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;                          // нажал «Отмена» или закрыл — выходим
            }

            /* ---------- 2. Запускаем главную форму как основное окно ---------- */
            Application.Run(new Form2());            // ← Form2 = главная витрина
        }
    }
}
