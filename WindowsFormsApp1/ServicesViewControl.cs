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
    public partial class ServicesViewControl : UserControl
    {

        public ServicesViewControl()
        {
            InitializeComponent();
            cmbSort.Items.AddRange(new object[]
           {
                "Название A → Я",
                "Название Я → A",
                "Цена ↑",
                "Цена ↓"
           });
            cmbSort.SelectedIndex = 0;

            /* ---------- события ---------- */
            cmbSort.SelectedIndexChanged += (s, e) => ReloadServices();
            txtSearch.TextChanged += (s, e) => ReloadServices();

            cmbConsumers.SelectedIndexChanged += (s, e) => { /* ничего; нужна только для Order */ };

            LoadUsersToComboBox();
            LoadServices();
            // Подписываемся: при любом изменении прав перескроем контролы
            Access.RightsChanged += () => ApplyRights(this);

            // И сразу при старте применяем права к этой форме
            ApplyRights(this);
        }

        private static void ApplyRights(Control root)
        {
            foreach (Control c in root.Controls)
            {
                if (c.Tag is string code)
                    c.Visible = Access.Has(code);   // или c.Enabled = Access.Has(code);

                if (c.HasChildren)
                    ApplyRights(c);
            }
        }
        private void flowServices_Paint(object sender, PaintEventArgs e)
        {

        }


        private void OrderService(int serviceId)
        {
            if (cmbConsumers.SelectedValue == null)
            {
                MessageBox.Show("Выберите потребителя перед оформлением услуги.");
                return;
            }

            int consumerId = (int)cmbConsumers.SelectedValue;

            using (SqlConnection cn = new SqlConnection(AppConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO service_orders (consumer_id, service_id)
                  VALUES (@c, @s)", cn))
            {
                cmd.Parameters.AddWithValue("@c", consumerId);
                cmd.Parameters.AddWithValue("@s", serviceId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Услуга успешно заказана!");
        }



        private void LoadServices()
        {
            flowServices.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = "SELECT id, name, description, price FROM services WHERE is_active = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string desc = reader.GetString(2);
                        decimal price = reader.GetDecimal(3);

                        var card = new ServiceCardControl(id, name, desc, price);
                        card.OnOrderClicked += OrderService;
                        card.OnDeleteClicked += DeleteService;

                        flowServices.Controls.Add(card);
                    }
                }
            }
        }
        private void DeleteService(int serviceId)
        {
            if (MessageBox.Show(
                    "Удалить услугу?\nБудут удалены все заявки на неё.",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        /* 1. удаляем зависимые заявки */
                        var cmd = new SqlCommand(
                            "DELETE FROM service_orders WHERE service_id = @sid",
                            cn, tx);
                        cmd.Parameters.AddWithValue("@sid", serviceId);
                        cmd.ExecuteNonQuery();

                        /* 2. гасим саму услугу */
                        cmd.CommandText =
                            "DELETE FROM services WHERE id = @sid";
                        cmd.ExecuteNonQuery();

                        tx.Commit();
                        MessageBox.Show("Услуга удалена.");
                        LoadServices();                 // перечитать витрину
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Ошибка при удалении:\n" + ex.Message,
                                        "SQL",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadUsersToComboBox()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection cn = new SqlConnection(AppConnection.ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(
                   "SELECT id, full_name FROM consumers ORDER BY full_name", cn))
            {
                da.Fill(tbl);
            }

            cmbConsumers.DataSource = tbl;
            cmbConsumers.DisplayMember = "full_name";
            cmbConsumers.ValueMember = "id";
            cmbConsumers.SelectedIndex = -1;
        }

        private void ReloadServices()
        {
            flowServices.Controls.Clear();

            /* 1. собираем WHERE и ORDER BY */
            string where = "WHERE is_active = 1";
            string order = "name ASC";
            string search = txtSearch.Text.Trim();

            if (search.Length > 0)
                where += " AND name LIKE @p";

            switch (cmbSort.SelectedIndex)
            {
                case 1: order = "name DESC"; break;
                case 2: order = "price ASC"; break;
                case 3: order = "price DESC"; break;
            }

            string sql = @"
SELECT id, name, description, price
FROM   services
" + where + @"
ORDER  BY " + order;

            using (SqlConnection cn = new SqlConnection(AppConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (search.Length > 0)
                    cmd.Parameters.AddWithValue("@p", "%" + search + "%");

                cn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        int id = r.GetInt32(0);
                        string name = r.GetString(1);
                        string desc = r.GetString(2);
                        decimal price = r.GetDecimal(3);

                        ServiceCardControl card = new ServiceCardControl(id, name, desc, price);
                        card.OnOrderClicked += OrderService;
                        card.OnDeleteClicked += DeleteService;

                        flowServices.Controls.Add(card);
                    }
                }
            }
        }
        private void ServicesViewControl_Load(object sender, EventArgs e)
        {
            LoadUsersToComboBox();
        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            if (cmbConsumers.SelectedValue == null)
            {
                MessageBox.Show("Сначала выберите потребителя.");
                return;
            }

            int id = (int)cmbConsumers.SelectedValue;
            string name = cmbConsumers.Text;

            using (UserOrdersForm f = new UserOrdersForm(id, name))
                f.ShowDialog(this);
        
        
        }
    

        private void btnAddService_Click(object sender, EventArgs e)
        {
            using (AddServiceForm f = new AddServiceForm())
                if (f.ShowDialog(this) == DialogResult.OK)
                    ReloadServices();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
