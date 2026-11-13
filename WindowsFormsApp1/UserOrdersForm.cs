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
    public partial class UserOrdersForm : Form
    {
        private readonly int _consumerId;


        public UserOrdersForm(int consumerId, string consumerName)
        {
            InitializeComponent();
         
            _consumerId = consumerId;
            lblUserName.Text = "Заказы: " + consumerName;
            LoadOrders();
        }

        private void LoadOrders()
        {
            flowOrders.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"SELECT o.id, s.name, s.price, o.order_date, o.status, o.comment
                         FROM service_orders o
                         JOIN services s ON o.service_id = s.id
                         WHERE o.consumer_id = @id
                         ORDER BY o.order_date DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _consumerId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int orderId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);
                        DateTime date = reader.GetDateTime(3);
                        string status = reader.GetString(4);
                        string comment = reader.IsDBNull(5) ? "" : reader.GetString(5);

                        var card = new OrderCardControl(orderId, name, price, date, status, comment);
                        flowOrders.Controls.Add(card);
                    }
                }
            }
        }

        private void UserOrdersForm_Load(object sender, EventArgs e)
        {
           
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void flowOrders_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
