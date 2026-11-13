using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using static WindowsFormsApp1.Form1;


namespace WindowsFormsApp1
{
    public partial class UserControlReg : UserControl
    {
        public UserControlReg()
        {
            InitializeComponent();
            _employeeService = new EmployeeService(AppConnection.ConnectionString);
            LoadPositions();
            // в конструкторе формы / контроле или в дизайнере
            picPhoto.SizeMode = PictureBoxSizeMode.Zoom;
            picPhoto.Size = new Size(120, 120);          // или 96x96
            picPhoto.BorderStyle = BorderStyle.FixedSingle;    // опционально
            Placeholder.Attach(txtLastName, "Фамилия");
            Placeholder.Attach(txtFirstName, "Имя");
            Placeholder.Attach(txtMiddleName, "Отчество");
            Placeholder.Attach(txtPassportSeries, "Серия паспорта (4 цифры)");
            Placeholder.Attach(txtPassportNumber, "Номер паспорта (6 цифр)");
            Placeholder.Attach(txtPhone, "Телефон +7XXXXXXXXXX");
            Placeholder.Attach(txtEmail, "E-mail");
            Placeholder.Attach(cmbPosition, "Выберите должность");
            // при загрузке данных
            //picPhoto.Image = Access.PhotoImage ?? Properties.Resources.noavatar;

        }

        private void LoadPositions()
        {
            DataTable positions = _employeeService.GetPositions();

            cmbPosition.DataSource = positions;
            cmbPosition.DisplayMember = "name";  // то, что видит пользователь
            cmbPosition.ValueMember = "id";      // то, что будет передано в БД
            cmbPosition.SelectedIndex = -1;      // чтобы ничего не было выбрано по умолчанию
        }
        private string GenerateConfirmationCode()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999).ToString(); // 6-значный код
        }





        private EmployeeService _employeeService;

        private async void button1_Click(object sender, EventArgs e)
        {
            bool isValid = _employeeService.ValidateEmployee(
            
             txtLastName,
             
             txtFirstName,
             txtPassportSeries,
             txtPassportNumber,
             txtPhone,
             txtEmail,
             cmbPosition
 );

            if (!isValid)
                return;

            string code = GenerateConfirmationCode();

            string botToken = "";
            string adminChatId = "6833954850";

            var notifier = new TelegramNotifier(botToken, adminChatId);
            //await notifier.SendConfirmationCodeAsync(txtMiddleName + " " + txtFirstName, code);

            var confirmForm = new Form3(code);
            confirmForm.ShowDialog();

            if (!confirmForm.IsConfirmed)
            {
                MessageBox.Show("Регистрация отменена. Код не подтверждён.");
                return;
            }

            var credentials = _employeeService.GenerateCredentials(txtFirstName.Text, txtLastName.Text);
            var authService = new AuthService();
            // Сохраняем в user_auth и получаем user_id

            int userId = authService.RegisterUser(credentials.Username, credentials.Password);

            if (userId == -1)
            {
                MessageBox.Show("Не удалось зарегистрировать пользователя. Возможно, логин уже существует.");
                return;
            }

            _employeeService.SaveEmployee(
                txtLastName,
                txtFirstName,
                txtMiddleName,
                txtPassportSeries,
                txtPassportNumber,
                txtPhone,
                txtEmail,
                DateTime.Now.Date,
                cmbPosition,
                userId,
                _photoBytes
            );

            MessageBox.Show("Сотрудник успешно зарегистрирован.");
            MessageBox.Show($"Регистрация завершена!\nВаш логин: {credentials.Username}\nПароль: {credentials.Password}", "Учетные данные", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPassportSeries_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        private byte[] _photoBytes;   // поле-член класса UserControlReg


        private void btnLoadPhoto_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Файлы изображений|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _photoBytes = System.IO.File.ReadAllBytes(ofd.FileName);
                    picPhoto.Image = Image.FromFile(ofd.FileName);   // превью
                }
            }
        }
    }



    public class AuthService
    {

        public int RegisterUser(string username, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"INSERT INTO user_auth (username, password_hash, is_active)
                         OUTPUT INSERTED.id
                         VALUES (@username, @password_hash, 1)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password_hash", hashedPassword);

                try
                {
                    conn.Open();
                    int userId = (int)cmd.ExecuteScalar(); // получаем ID созданной записи
                    return userId;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Ошибка регистрации пользователя: " + ex.Message);
                    return -1;
                }
            }
        }
    }





    public class EmployeeService
    {
        private readonly string _connectionString;

        public EmployeeService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public (string Username, string Password) GenerateCredentials(string firstName, string lastName)
        {
            string username = $"{firstName.ToLower()}.{lastName.ToLower()}.{new Random().Next(100, 999)}";
            string password = Guid.NewGuid().ToString("N").Substring(0, 8); // 8 символов

            return (username, password);
        }







        public bool ValidateEmployee(TextBox txtLastName, TextBox txtFirstName,
            TextBox txtPassportSeries, TextBox txtPassportNumber, TextBox txtPhone,
            TextBox txtEmail, ComboBox cmbPosition)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Введите фамилию."); txtLastName.Focus(); return false;
            }
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Введите имя."); txtFirstName.Focus(); return false;
            }
            if (!Regex.IsMatch(txtPassportSeries.Text, @"^\d{4}$"))
            {
                MessageBox.Show("Серия паспорта должна содержать 4 цифры."); txtPassportSeries.Focus(); return false;
            }
            if (!Regex.IsMatch(txtPassportNumber.Text, @"^\d{6}$"))
            {
                MessageBox.Show("Номер паспорта должен содержать 6 цифр."); txtPassportNumber.Focus(); return false;
            }
            if (!Regex.IsMatch(txtPhone.Text, @"^\+7\d{10}$"))
            {
                MessageBox.Show("Телефон должен быть в формате +7XXXXXXXXXX."); txtPhone.Focus(); return false;
            }
            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Введите корректный email."); txtEmail.Focus(); return false;
            }
            if (cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите должность."); cmbPosition.Focus(); return false;
            }


            return true;
        }



        public DataTable GetPositions()
        {
            DataTable table = new DataTable();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT id, name FROM positions ORDER BY name";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adapter.Fill(table);
                }
            }

            return table;
        }

        public void SaveEmployee(TextBox txtLastName, TextBox txtFirstName, TextBox txtMiddleName,
            TextBox txtPassportSeries, TextBox txtPassportNumber, TextBox txtPhone, TextBox txtEmail,
            DateTime hireDate, ComboBox cmbPosition, int userId, byte[] photo)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
INSERT INTO employees
 (last_name, first_name, middle_name, passport_series, passport_number,
  phone_number, email, hire_date, position_id, user_id, photo)
VALUES
 (@LastName, @FirstName, @MiddleName, @PassportSeries, @PassportNumber,
  @Phone, @Email, @HireDate, @PositionId, @UserId, @Photo)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text.Trim());
                    cmd.Parameters.AddWithValue("@PassportSeries", txtPassportSeries.Text.Trim());
                    cmd.Parameters.AddWithValue("@PassportNumber", txtPassportNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@HireDate", hireDate);
                    cmd.Parameters.AddWithValue("@Photo", (object)photo ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@PositionId", (int)cmbPosition.SelectedValue);

                    // Пока жёстко user_id = 1, позже можно добавить выбор
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Сотрудник успешно добавлен!");
        }
    }

   
    



    public class TelegramNotifier
    {
        private readonly string _botToken;
        private readonly string _adminChatId;

        public TelegramNotifier(string botToken, string adminChatId)
        {
            _botToken = botToken;
            _adminChatId = adminChatId;
        }

        public async Task SendConfirmationCodeAsync(string employeeName, string code)
        {
            var botClient = new TelegramBotClient(_botToken);

            string message = $"🔐 Новый запрос на регистрацию:\n" +
                             $"👤 Сотрудник: {employeeName}\n" +
                             $"🆔 Код подтверждения: {code}";

            await botClient.SendTextMessageAsync(
                chatId: _adminChatId,
                text: message
            );
        }
    }


}
   
