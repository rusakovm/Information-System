using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class UserControlIn : UserControl
    {
        public UserControlIn()
        {
            InitializeComponent();
            Placeholder.Attach(textBox2, "Логин");
            Placeholder.Attach(textBox1, "Пароль");
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = Placeholder.GetUserText(textBox2).Trim();
            string pwd = Placeholder.GetUserText(textBox1);
            /* 1. авторизация */
            var auth = new AuthService();
            bool success = auth.AuthenticateUser(login, pwd);

            if (!success)
            {
                MessageBox.Show("Неверный логин или пароль.");
                return;                     // → ничего дальше не делаем
            }

            /* 2. успешный вход – инициализируем данные пользователя */
            Access.Init(login.Trim());

            /* 3. сообщаем форме-логину, что всё ок */
            var parent = FindForm();        // UserControl → Form1 (форма-логин)
            if (parent != null)
            {
                parent.DialogResult = DialogResult.OK;
                // parent.Close();          // не нужно: установка DialogResult сама закроет форму-диалог
            }
            // 2. Проверка подключения к базе данных
            /*
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
            */

            // 3. Проверка работоспособности подключения
            /*try
            {
                using (var connection = new SqlConnection(AppConnection.ConnectionString))
                {
                    connection.Open();

                    // Проверяем доступность нужной таблицы (опционально)
                    var cmd = new SqlCommand("SELECT 1", connection);
                    cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}\nПожалуйста, проверьте настройки подключения.");

                // Показываем форму подключения для повторной настройки
                var connectionForm = new ConnectionForm();
                if (connectionForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                // Повторная попытка после настройки
                button1_Click(sender, e);
                return;
            }*/
            // 1. Проверка аутентификации пользователя
            /*
            var authService = new AuthService();
            bool success = authService.AuthenticateUser(textBox2.Text, textBox1.Text);

            if (!success)
            {*/
                /*
                MessageBox.Show("Неверный логин или пароль.");
                return;
                *//*
                Access.Init(textBox2.Text.Trim());

                var parent = FindForm();            // форма-контейнер
                if (parent != null)
                {
                    parent.DialogResult = DialogResult.OK;
                    parent.Close();
                }
            }*/

           //Access.Init(textBox2.Text.Trim());

            // 4. Если всё успешно - открываем Form2

            //MessageBox.Show("Успешный вход!");
            /*
            this.Hide();
            Form2 newForm = new Form2();
            newForm.Show();
            */
        }
        public class AuthService
        {
            public bool RegisterUser(string username, string password)
            {
                // 1. Проверяем наличие строки подключения
                if (!CheckDatabaseConnection())
                {
                    return false;
                }

                // 2. Хешируем пароль
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                // 3. Регистрируем пользователя
                using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
                {
                    string query = "INSERT INTO user_auth (username, password_hash) VALUES (@username, @password_hash)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hashedPassword);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        HandleDatabaseError(ex);
                        return false;
                    }
                }
            }

            public bool AuthenticateUser(string username, string password)
            {
                // 1. Проверяем наличие строки подключения
                if (!CheckDatabaseConnection())
                {
                    return false;
                }

                // 2. Проверяем учетные данные
                using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
                {
                    string query = "SELECT password_hash FROM user_auth WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    try
                    {
                        conn.Open();
                        var storedHash = cmd.ExecuteScalar()?.ToString();

                        if (storedHash != null)
                        {
                            return BCrypt.Net.BCrypt.Verify(password, storedHash);
                        }
                        return false;
                    }
                    catch (SqlException ex)
                    {
                        HandleDatabaseError(ex);
                        return false;
                    }
                }
            }

            private bool CheckDatabaseConnection()
            {
                // Если строка подключения не установлена, показываем форму настройки
                if (!AppConnection.IsConnectionStringSet)
                {
                    var connectionForm = new ConnectionForm();
                    if (connectionForm.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("Не удалось установить подключение к базе данных.");
                        return false;
                    }
                }

                // Проверяем работоспособность подключения
                try
                {
                    using (var connection = new SqlConnection(AppConnection.ConnectionString))
                    {
                        connection.Open();
                        var cmd = new SqlCommand("SELECT 1", connection);
                        cmd.ExecuteScalar();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}\nПожалуйста, проверьте настройки подключения.");

                    // Показываем форму подключения для повторной настройки
                    var connectionForm = new ConnectionForm();
                    if (connectionForm.ShowDialog() != DialogResult.OK)
                    {
                        return false;
                    }

                    // Рекурсивно проверяем снова после настройки
                    return CheckDatabaseConnection();
                }
            }

            private void HandleDatabaseError(SqlException ex)
            {
                if (ex.Number == 2627) // Ошибка дублирования ключа (пользователь уже существует)
                {
                    MessageBox.Show("Пользователь с таким именем уже существует.");
                }
                else
                {
                    MessageBox.Show($"Ошибка базы данных: {ex.Message}");

                    // Предлагаем проверить подключение при серьезных ошибках
                    if (ex.Number == -2 || ex.Number == 53 || ex.Number == 4060) // Ошибки подключения
                    {
                        var connectionForm = new ConnectionForm();
                        connectionForm.ShowDialog();
                    }
                }
            }
        }
        /*
        public bool AuthenticateUser(string username, string password)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT password_hash FROM user_auth WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string storedHash = reader.GetString(0);
                        return BCrypt.Net.BCrypt.Verify(password, storedHash);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        */

            public void AddDefaultAdmin()
            {
                string username = "admin";
                string password = "admin123";
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            
                {
                    string query = "IF NOT EXISTS (SELECT 1 FROM user_auth WHERE username = @username) " +
                                   "INSERT INTO user_auth (username, password_hash) VALUES (@username, @password_hash)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hashedPassword);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Пользователь 'admin' добавлен или уже существует.");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Ошибка при добавлении admin: " + ex.Message);
                    }
                }
            }
        }
    

}
