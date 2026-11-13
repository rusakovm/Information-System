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
    public partial class AddMeterForm : Form
    {
        private readonly int _consumerId;

        public AddMeterForm(int consumerId)
        {
            InitializeComponent();
            _consumerId = consumerId;

            dtpInstall.Value = DateTime.Today;
            chkActive.Checked = true;

            Placeholder.Attach(txtSerial, "Серийный номер");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string serial = txtSerial.Text.Trim();
            DateTime install = dtpInstall.Value.Date;
            bool active = chkActive.Checked;

            if (string.IsNullOrWhiteSpace(serial))
            {
                MessageBox.Show("Введите серийный номер.");
                txtSerial.Focus();
                return;
            }


            if (install > DateTime.Today)
            {
                MessageBox.Show("Дата установки не может быть в будущем.");
                dtpInstall.Focus();
                return;
            }

            /* при желании можно проверять, что такого номера ещё нет
               SELECT COUNT(*) FROM meters WHERE serial_number = @sn            */

            /* --- подтверждение -------------------------------- */
            if (MessageBox.Show($"Добавить счётчик № {serial}?",
                                "Подтверждение",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            /* --- добавляем ------------------------------------ */
            bool ok = InsertMeter(serial, install, chkActive.Checked);
            if (ok)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        private bool InsertMeter(string serial, DateTime install, bool active)
        {
            const string sql = @"INSERT INTO meters
                (consumer_id, serial_number, installation_date, is_active)
                VALUES (@cid, @sn, @dt, @act)";

            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@cid", _consumerId);
                cmd.Parameters.AddWithValue("@sn", serial);
                cmd.Parameters.AddWithValue("@dt", install);
                cmd.Parameters.AddWithValue("@act", active);

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message,
                                    "SQL",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void AddMeterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
