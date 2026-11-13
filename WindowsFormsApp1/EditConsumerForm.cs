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
    public partial class EditConsumerForm : Form
    {
        private readonly int _consumerId;


        public EditConsumerForm(int consumerId)
        {
            InitializeComponent();
            _consumerId = consumerId;
            LoadRegions();
            LoadConsumerData();

            Placeholder.Attach(txtFullName, "Название / ФИО");
            Placeholder.Attach(txtContactPerson, "Контактное лицо");
            Placeholder.Attach(txtPhone, "+7 (xxx) xxx-xx-xx");
            Placeholder.Attach(txtEmail, "E-mail");
            Placeholder.Attach(txtAddress, "Юридический адрес");
            Placeholder.Attach(cmbRegion, "Выберите регион");

        }
        private void LoadRegions()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, name FROM regions", conn);
                DataTable table = new DataTable();
                adapter.Fill(table);

                cmbRegion.DataSource = table;
                cmbRegion.DisplayMember = "name";
                cmbRegion.ValueMember = "id";
            }
        }
        private void LoadConsumerData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM consumers WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", _consumerId);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtFullName.Text = reader["full_name"].ToString();
                        txtContactPerson.Text = reader["contact_person"].ToString();
                        txtPhone.Text = reader["phone"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        txtAddress.Text = reader["address"].ToString();
                        cmbRegion.SelectedValue = Convert.ToInt32(reader["region_id"]);
                    }
                }
            }
        }
      
        private void EditConsumer(int consumerId)
        {
           
        }

        private bool ValidateFields()
        {
            /* 1. обязательные поля -------------------------------------------- */
            if (Placeholder.GetUserText(txtFullName).Length == 0)
            { MessageBox.Show("Введите название / ФИО."); txtFullName.Focus(); return false; }

            if (cmbRegion.SelectedIndex < 0)
            { MessageBox.Show("Выберите регион."); cmbRegion.DroppedDown = true; return false; }

            /* 2. телефон (опционально) ---------------------------------------- */
            string phone = Placeholder.GetUserText(txtPhone);
            if (phone.Length > 0 &&
                !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\+7\d{10}$"))
            { MessageBox.Show("Телефон должен быть в формате +7XXXXXXXXXX."); txtPhone.Focus(); return false; }

            /* 3. e-mail (опционально) ----------------------------------------- */
            string mail = Placeholder.GetUserText(txtEmail);
            if (mail.Length > 0 &&
                !System.Text.RegularExpressions.Regex.IsMatch(mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            { MessageBox.Show("Введите корректный e-mail."); txtEmail.Focus(); return false; }

            return true;        // всё OK
        }

        private void EditConsumerForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"UPDATE consumers SET 
            full_name = @fullName,
            contact_person = @contactPerson,
            phone = @phone,
            email = @email,
            address = @address,
            region_id = @regionId
            WHERE id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _consumerId);
                cmd.Parameters.AddWithValue("@fullName", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@contactPerson", txtContactPerson.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@regionId", (int)cmbRegion.SelectedValue);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Данные обновлены.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
