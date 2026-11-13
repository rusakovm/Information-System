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
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class EditEmployeeForm : Form
    {

        private readonly int _employeeId;

        private byte[] _photoBytes;          // храним картинку в памяти
        private string _selectedImagePath;
        public EditEmployeeForm(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;

            LoadPositions();      // сначала должности
            LoadEmployeeData();
            Placeholder.Attach(txtLastName, "Фамилия");
            Placeholder.Attach(txtMiddleName, "Отчество (необязательно)");
            Placeholder.Attach(txtFirstName, "Имя");
            Placeholder.Attach(txtPassportSeries, "Серия (4 цифры)");
            Placeholder.Attach(txtPassportNumber, "Номер (6 цифр)");
            Placeholder.Attach(txtPhone, "+7XXXXXXXXXX");
            Placeholder.Attach(txtEmail, "example@mail.com");

        }

        private bool ValidateFields()
        {
            // ФИО
            if (Placeholder.GetUserText(txtLastName).Length == 0)
            { MessageBox.Show("Введите фамилию."); txtLastName.Focus(); return false; }

            if (Placeholder.GetUserText(txtFirstName).Length == 0)
            { MessageBox.Show("Введите имя."); txtFirstName.Focus(); return false; }

            // Паспорт: серия 4 цифры
            var series = Placeholder.GetUserText(txtPassportSeries);
            if (!Regex.IsMatch(series, @"^\d{4}$"))
            { MessageBox.Show("Серия паспорта: 4 цифры."); txtPassportSeries.Focus(); return false; }

            // Паспорт: номер 6 цифр
            var number = Placeholder.GetUserText(txtPassportNumber);
            if (!Regex.IsMatch(number, @"^\d{6}$"))
            { MessageBox.Show("Номер паспорта: 6 цифр."); txtPassportNumber.Focus(); return false; }

            // Телефон (опционально)
            var phone = Placeholder.GetUserText(txtPhone);
            if (phone.Length > 0 && !Regex.IsMatch(phone, @"^\+7\d{10}$"))
            { MessageBox.Show("Телефон должен быть в формате +7XXXXXXXXXX."); txtPhone.Focus(); return false; }

            // Email (опционально)
            var mail = Placeholder.GetUserText(txtEmail);
            if (mail.Length > 0 && !Regex.IsMatch(mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            { MessageBox.Show("Введите корректный email."); txtEmail.Focus(); return false; }

            // Должность
            if (cmbPosition.SelectedIndex < 0)
            { MessageBox.Show("Выберите должность."); cmbPosition.Focus(); return false; }

            return true;
        }
        private void LoadPositions()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = "SELECT id, name FROM positions ORDER BY name";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbPosition.DataSource = dt;
                cmbPosition.DisplayMember = "name";  // отображаемое имя
                cmbPosition.ValueMember = "id";      // значение, отправляемое в БД
                cmbPosition.SelectedIndex = -1;      // ничего не выбрано по умолчанию
            }
        }


        private void EditEmployeeForm_Load(object sender, EventArgs e)
        {
            picPhoto.SizeMode = PictureBoxSizeMode.Zoom;
            picPhoto.Size = new Size(80, 80);          // или 96x96
            picPhoto.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LoadEmployeeData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM employees WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", _employeeId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtLastName.Text = reader["last_name"].ToString();
                        txtFirstName.Text = reader["first_name"].ToString();
                        txtMiddleName.Text = reader["middle_name"].ToString();

                        txtPassportSeries.Text = reader["passport_series"].ToString();
                        txtPassportNumber.Text = reader["passport_number"].ToString();  // ← правильное поле
                        txtPhone.Text = reader["phone_number"].ToString();

                        txtEmail.Text = reader["email"].ToString();
                        cmbPosition.SelectedValue = Convert.ToInt32(reader["position_id"]);
                        if (!reader.IsDBNull(reader.GetOrdinal("photo")))
                        {
                            _photoBytes = (byte[])reader["photo"];
                            using (var ms = new MemoryStream(_photoBytes))
                                picPhoto.Image = Image.FromStream(ms);
                        }
                        else
                        {
                            _photoBytes = null;
                            //picPhoto.Image = Properties.Resources.noavatar; // запасная картинка
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            object posId = cmbPosition.SelectedValue;
            if (posId == null || (int)posId == -1)
            {
                MessageBox.Show("Не выбрана должность.");
                return;
            }
     
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"
                    UPDATE employees SET
                      last_name       = @lastName,
                      first_name      = @firstName,
                      middle_name     = @middleName,
                      passport_series = @passportSeries,
                      passport_number = @passportNumber,
                      phone_number    = @phone,
                      email           = @email,
                      position_id     = @positionId,
                      photo           = @photo        
                    WHERE id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _employeeId);
                cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@middleName", txtMiddleName.Text);
                cmd.Parameters.AddWithValue("@passportSeries", txtPassportSeries.Text);
                cmd.Parameters.AddWithValue("@passportNumber", txtPassportNumber.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@positionId", cmbPosition.SelectedValue);
                cmd.Parameters.Add("@photo", SqlDbType.VarBinary).Value =
    _photoBytes ?? (object)DBNull.Value;


                conn.Open();
                cmd.ExecuteNonQuery();

                Access.Init(Access.Login);   // ④

            }

            MessageBox.Show("Данные сотрудника обновлены.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadPhoto_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Файлы изображений|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                _selectedImagePath = ofd.FileName;
                _photoBytes = File.ReadAllBytes(_selectedImagePath);
                picPhoto.Image = Image.FromFile(_selectedImagePath);
            }
        }
    }
}
