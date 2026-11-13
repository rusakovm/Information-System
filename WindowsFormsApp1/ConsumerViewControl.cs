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
    public partial class ConsumerViewControl : UserControl
    {
        public ConsumerViewControl()
        {
            InitializeComponent();
            InitSortCombo();
            txtSearch.TextChanged += (s, e) => LoadConsumers();
            LoadConsumers();
        }
        private void EditConsumer(int consumerId)
        {
            // Здесь открытие формы редактирования
            var form = new EditConsumerForm(consumerId);
            if (form.ShowDialog() == DialogResult.OK)
                LoadConsumers();
        }
        private void DeleteConsumer(int consumerId)
        {
            if (MessageBox.Show("Удалить выбранного потребителя со всеми связанными данными?",
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
                        /* 1. показания → meters */
                        var cmd = new SqlCommand(
                            @"DELETE FROM meter_readings
                      WHERE meter_id IN (SELECT id FROM meters WHERE consumer_id = @cid)", cn, tx);
                        cmd.Parameters.AddWithValue("@cid", consumerId);
                        cmd.ExecuteNonQuery();

                        /* 2. счётчики */
                        cmd.CommandText = "DELETE FROM meters WHERE consumer_id = @cid";
                        cmd.ExecuteNonQuery();

                        /* 3. заказы услуг */
                        cmd.CommandText = "DELETE FROM service_orders WHERE consumer_id = @cid";
                        cmd.ExecuteNonQuery();

                        /* 4. платежи (если связаны напрямую) */
                        cmd.CommandText = "DELETE FROM payments WHERE consumer_id = @cid";
                        cmd.ExecuteNonQuery();

                        /* 5. сам потребитель */
                        cmd.CommandText = "DELETE FROM consumers WHERE id = @cid";
                        cmd.ExecuteNonQuery();

                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Ошибка при удалении:\n" + ex.Message,
                                        "SQL",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            MessageBox.Show("Потребитель и все связанные записи удалены.");
            LoadConsumers();                // перечитать список карточек
        }

        private void InitSortCombo()
        {
            cmbSort.Items.Add("Название A → Я");
            cmbSort.Items.Add("Название Я → A");
            cmbSort.Items.Add("Дата добавления: новые");
            cmbSort.Items.Add("Дата добавления: старые");

            cmbSort.SelectedIndex = 0;                   // сортировка по умолчанию
            cmbSort.SelectedIndexChanged += (s, e) => LoadConsumers();
        }

        private void LoadConsumers()
        {
            flowConsumers.Controls.Clear();

            /* ---------- WHERE + ORDER BY ---------- */
            string where = "";                      // фильтр поиска
            string order;

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                where = "WHERE full_name LIKE @p";

            switch (cmbSort.SelectedIndex)
            {
                case 1: order = "ORDER BY full_name DESC"; break;
                case 2: order = "ORDER BY registration_date DESC"; break;
                case 3: order = "ORDER BY registration_date ASC"; break;
                default: order = "ORDER BY full_name"; break;
            }

            string sql = @"
SELECT id, full_name, contact_person, phone, email
FROM   consumers
" + where + @"
" + order;

            using (SqlConnection cn = new SqlConnection(AppConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (where.Length > 0)
                    cmd.Parameters.AddWithValue("@p", "%" + txtSearch.Text.Trim() + "%");

                cn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        int id = r.GetInt32(0);
                        string name = r.GetString(1);
                        string contact = r.IsDBNull(2) ? "" : r.GetString(2);
                        string phone = r.IsDBNull(3) ? "" : r.GetString(3);
                        string email = r.IsDBNull(4) ? "" : r.GetString(4);

                        ConsumerCardControl card = new ConsumerCardControl(id, name, contact, phone, email);
                        card.OnEditClicked += EditConsumer;
                        card.OnDeleteClicked += DeleteConsumer;

                        flowConsumers.Controls.Add(card);
                    }
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowConsumers_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddConsumer_Click(object sender, EventArgs e)
        {
            using (var f = new AddConsumer())    // имя вашей формы
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    // пользователь действительно добавил запись —
                    // перезагружаем витрину (если она у вас есть)
                    LoadConsumers();                 // обновить список карточек
                }
            }
        }

        private void ConsumerViewControl_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
