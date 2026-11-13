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
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }
        private void chkWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = !chkWindowsAuth.Checked;
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
            label3.Enabled = enabled;
            label4.Enabled = enabled;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            string connectionString = BuildConnectionString();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Подключение успешно!", "Успех",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppConnection.ConnectionString = BuildConnectionString();
            MessageBox.Show("Строка подключения сохранена!", "Успех",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private string BuildConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = txtServer.Text,
                InitialCatalog = txtDatabase.Text,
                IntegratedSecurity = chkWindowsAuth.Checked
            };

            if (!chkWindowsAuth.Checked)
            {
                builder.UserID = txtUsername.Text;
                builder.Password = txtPassword.Text;
            }

            if (chkTrustServerCert.Checked)
            {
                builder.TrustServerCertificate = true;
            }

            return builder.ToString();
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
