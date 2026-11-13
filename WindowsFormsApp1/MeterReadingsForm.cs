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
    public partial class MeterReadingsForm : Form
    {
        private readonly int _meterId;

        public MeterReadingsForm(int meterId)
        {
            InitializeComponent();
            _meterId = meterId;
            LoadReadings();
        }

        private void MeterReadingsForm_Load(object sender, EventArgs e)
        {

        }
        private void LoadReadings()
        {
            flowReadings.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"SELECT id, reading_value, reading_date 
                         FROM meter_readings 
                         WHERE meter_id = @meterId 
                         ORDER BY reading_date DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@meterId", _meterId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        decimal value = reader.GetDecimal(1);
                        DateTime date = reader.GetDateTime(2);

                        var card = new ReadingCardControl(id, value, date);
                        card.OnEditClicked += EditReading;
                        card.OnDeleteClicked += DeleteReading;
                        flowReadings.Controls.Add(card);
                    }
                }
            }
        }
        private void EditReading(int readingId)
        {
            var form = new EditReadingForm(readingId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadReadings(); // обновляем список карточек
            }
        }

        private void DeleteReading(int readingId)
        {
            var confirm = MessageBox.Show("Удалить показание?", "Подтверждение", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM meter_readings WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", readingId);
                cmd.ExecuteNonQuery();
            }

            LoadReadings();
        }

        private void flowReadings_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddReading_Click(object sender, EventArgs e)
        {
            using (var dlg = new AddReadingForm(_meterId))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    LoadReadings();         // перечитать список после успешного добавления
            }
        }
    }
}
