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
    public partial class MetersViewControl : UserControl
    {
        public MetersViewControl()
        {
            InitializeComponent();
            cmbSort.Items.AddRange(new object[]
          {
                "Серийный №  A → Z",
                "Серийный №  Z → A",
                "Дата установки ↑",
                "Дата установки ↓",
                "Только активные",
                "Только неактивные"
          });
            cmbSort.SelectedIndex = 0;          // сортировка по умолчанию

            /* обработчики */
            cmbSort.SelectedIndexChanged += (s, e) => ReloadMeters();
            cmbConsumers.SelectedIndexChanged += (s, e) => ReloadMeters();
            LoadConsumers();
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbConsumers.SelectedValue == null ||
           !int.TryParse(cmbConsumers.SelectedValue.ToString(), out int consumerId))
            {
                MessageBox.Show("Сначала выберите потребителя.");
                return;
            }

            using (var dlg = new AddMeterForm(consumerId))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    LoadMeters(consumerId);       // перечитать список
                    ReloadMeters();
            }
        }

        private void MetersViewControl_Load(object sender, EventArgs e)
        {

        }
        private void LoadConsumers()
        {
            DataTable table = new DataTable();

            using (SqlConnection cn = new SqlConnection(AppConnection.ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(
                   "SELECT id, full_name FROM consumers ORDER BY full_name", cn))
            {
                da.Fill(table);
            }

            cmbConsumers.DataSource = table;
            cmbConsumers.DisplayMember = "full_name";
            cmbConsumers.ValueMember = "id";
            cmbConsumers.SelectedIndex = -1;
        }
        private void LoadMeters(int consumerId)
        {
            flowMeters.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT id, serial_number, installation_date, is_active FROM meters WHERE consumer_id = @consumerId", conn);
                cmd.Parameters.AddWithValue("@consumerId", consumerId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string serial = reader.GetString(1);
                        DateTime install = reader.GetDateTime(2);
                        bool active = reader.GetBoolean(3);

                        var meterCard = new MeterCardControl(id, serial, install, active);
                        meterCard.OnEditClicked += EditMeter;
                        meterCard.OnDeleteClicked += DeleteMeter;
                        flowMeters.Controls.Add(meterCard);
                    }
                }
            }
        }

        private void ReloadMeters()
        {
            flowMeters.Controls.Clear();

            if (cmbConsumers.SelectedValue == null) return;

            int consumerId;
            if (!Int32.TryParse(cmbConsumers.SelectedValue.ToString(), out consumerId))
                return;

            /* 1. формируем ORDER BY / WHERE */
            string orderBy = "serial_number ASC";
            string where = "";

            switch (cmbSort.SelectedIndex)
            {
                case 1: orderBy = "serial_number DESC"; break;
                case 2: orderBy = "installation_date DESC"; break;
                case 3: orderBy = "installation_date ASC"; break;
                case 4: where = "AND is_active = 1"; break;
                case 5: where = "AND is_active = 0"; break;
            }

            string sql = @"
SELECT id, serial_number, installation_date, is_active
FROM   meters
WHERE  consumer_id = @cid " + where + @"
ORDER  BY " + orderBy;

            using (SqlConnection cn = new SqlConnection(AppConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@cid", consumerId);
                cn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        int id = r.GetInt32(0);
                        string serial = r.GetString(1);
                        DateTime install = r.GetDateTime(2);
                        bool active = r.GetBoolean(3);

                        MeterCardControl card = new MeterCardControl(id, serial, install, active);
                        card.OnEditClicked += EditMeter;
                        card.OnDeleteClicked += DeleteMeter;
                        flowMeters.Controls.Add(card);
                    }
                }
            }
        }

        /* ---------- обработчики комбобоксов ---------- */
        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadMeters();
        }

        private void cmbConsumers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConsumers.SelectedValue is int consumerId)
            {
                LoadMeters(consumerId);
            }
        }



        private void EditMeter(int meterId)
        {
            var form = new EditMeterForm(meterId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Обновляем список после редактирования
                if (cmbConsumers.SelectedValue != null && int.TryParse(cmbConsumers.SelectedValue.ToString(), out int consumerId))
                {
                    LoadMeters(consumerId);
                }
            }
        }
        private void DeleteMeter(int meterId)
        {
            var confirm = MessageBox.Show("Удалить счётчик?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                conn.Open();

                // Удаляем сначала показания (если есть), затем сам счётчик
                SqlCommand deleteReadings = new SqlCommand("DELETE FROM meter_readings WHERE meter_id = @meterId", conn);
                deleteReadings.Parameters.AddWithValue("@meterId", meterId);
                deleteReadings.ExecuteNonQuery();

                SqlCommand deleteMeter = new SqlCommand("DELETE FROM meters WHERE id = @meterId", conn);
                deleteMeter.Parameters.AddWithValue("@meterId", meterId);
                deleteMeter.ExecuteNonQuery();
            }

            MessageBox.Show("Счётчик удалён.");

            // Повторная загрузка списка
            if (cmbConsumers.SelectedValue != null && int.TryParse(cmbConsumers.SelectedValue.ToString(), out int consumerId))
            {
                LoadMeters(consumerId);
            }
        }


     

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
