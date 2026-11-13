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
    public partial class EditReadingForm : Form
    {
        private readonly int _readingId;

        public EditReadingForm(int readingId)
        {
            InitializeComponent();
            _readingId = readingId;
            LoadReading();
        }
        private void LoadReading()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                var cmd = new SqlCommand("SELECT reading_value, reading_date FROM meter_readings WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", _readingId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        decimal value = reader.GetDecimal(0);

                        // Устанавливаем границы ДО установки Value
                        nudValue.Minimum = 0;
                        nudValue.Maximum = Math.Max(value + 1000, 100000); // добавим запас
                        nudValue.DecimalPlaces = 2;

                        nudValue.Value = value;
                        nudValue.Value = reader.GetDecimal(0);
                        dtpDate.Value = reader.GetDateTime(1);


                    }
                }
            }
        }

        private void EditReadingForm_Load(object sender, EventArgs e)
        {
            nudValue.Validating += (s, ev) =>
nudValue.Value = Math.Round(nudValue.Value, 2);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                var cmd = new SqlCommand(@"UPDATE meter_readings 
                                       SET reading_value = @value, reading_date = @date 
                                       WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", _readingId);
                cmd.Parameters.AddWithValue("@value", nudValue.Value);
                cmd.Parameters.AddWithValue("@date", dtpDate.Value.Date);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Показание обновлено.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
