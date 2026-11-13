using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class OrderCardControl : UserControl
    {

        private readonly int _orderId;

        public OrderCardControl(int orderId, string serviceName, decimal price, DateTime date, string status, string comment)
        {
            InitializeComponent();
            _orderId = orderId;

            lblServiceName.Text = serviceName;
            lblPrice.Text = $"{price:F2} ₽";
            lblDate.Text = date.ToShortDateString();
            cmbStatus.Items.AddRange(new[] { "В ожидании", "Выполнено", "Отменено" });
            cmbStatus.SelectedItem = status;
            txtComment.Text = comment;

            btnSave.Click += btnSave_Click;
        }

        private void OrderCardControl_Load(object sender, EventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"UPDATE service_orders 
                             SET status = @status, comment = @comment 
                             WHERE id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _orderId);
                cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@comment", txtComment.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Обновлено!");
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
