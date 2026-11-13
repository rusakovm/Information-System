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
    public partial class AddReadingForm : Form
    {
        private readonly int _meterId;


        public AddReadingForm(int meterId)
        {
            InitializeComponent();
            _meterId = meterId;

            // значения по умолчанию
            numValue.DecimalPlaces = 2;
            numValue.Minimum = 0;
            numValue.Maximum = 9999999;
            dtpDate.Value = DateTime.Today;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal value = numValue.Value;
            DateTime date = dtpDate.Value.Date;

            // простая валидация
            if (value <= 0)
            {
                MessageBox.Show("Введите положительное значение.");
                return;
            }

            // спрашиваем подтверждение
            if (MessageBox.Show($"Добавить показание {value} от {date:d} ?",
                                "Подтверждение",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (InsertReading(value, date))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private bool InsertReading(decimal value, DateTime date)
        {
            const string sql = @"INSERT INTO meter_readings
                                 (meter_id, reading_value, reading_date)
                                 VALUES (@mId, @val, @dt)";

            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@mId", _meterId);
                cmd.Parameters.AddWithValue("@val", value);
                cmd.Parameters.AddWithValue("@dt", date);

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message,
                                    "SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void AddReadingForm_Load(object sender, EventArgs e)
        {
            numValue.Validating += (s, ev) =>
numValue.Value = Math.Round(numValue.Value, 2);
        }
    }
}
