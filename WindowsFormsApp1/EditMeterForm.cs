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
using System.Text.RegularExpressions;
namespace WindowsFormsApp1
{
    public partial class EditMeterForm : Form
    {
        private readonly int _meterId;
        public EditMeterForm(int meterId)
        {
            InitializeComponent();
            _meterId = meterId;

            Placeholder.Attach(txtSerialNumber, "Серийный номер");

            LoadMeter();
        }
        private void LoadMeter()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = "SELECT serial_number, installation_date, is_active FROM meters WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _meterId);

                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtSerialNumber.Text = reader["serial_number"].ToString();
                    dtpInstallDate.Value = Convert.ToDateTime(reader["installation_date"]);
                    chkIsActive.Checked = Convert.ToBoolean(reader["is_active"]);
                }
            }
        }

        private void EditMeterForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string serial = Placeholder.GetUserText(txtSerialNumber);
            if (string.IsNullOrWhiteSpace(serial))
            {
                MessageBox.Show("Введите серийный номер.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSerialNumber.Focus();
                return;
            }
            // например, только буквы+цифры, не менее 3 символов
            if (!Regex.IsMatch(serial, @"^[A-Za-z0-9\-]{3,}$"))
            {
                MessageBox.Show("Серийный номер должен быть не менее 3 символов, только буквы, цифры и «-».",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSerialNumber.Focus();
                return;
            }

            var ask = MessageBox.Show($"Добавить счётчик №{serial}?",
                                  "Подтверждение",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question);
            if (ask != DialogResult.Yes) return;
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"UPDATE meters SET
                             serial_number = @serial,
                             installation_date = @date,
                             is_active = @active
                             WHERE id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@serial", txtSerialNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@date", dtpInstallDate.Value.Date);
                cmd.Parameters.AddWithValue("@active", chkIsActive.Checked);
                cmd.Parameters.AddWithValue("@id", _meterId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Данные счётчика обновлены");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    
    }
}
