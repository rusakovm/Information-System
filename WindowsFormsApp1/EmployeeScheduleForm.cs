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
using Telegram.Bot.Types;

namespace WindowsFormsApp1
{
    public partial class EmployeeScheduleForm : Form
    {
        private int selectedScheduleId = -1;


        public EmployeeScheduleForm()
        {
            InitializeComponent();

        }

        private void EmployeeScheduleForm_Load(object sender, EventArgs e)
        {
            SetupScheduleGrid();
            LoadEmployees();
            LoadSchedule();
        }
        private void LoadEmployees()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(
    "SELECT id, last_name + ' ' + first_name AS FullName FROM employees", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbEmployees.DataSource = dt;
                cmbEmployees.DisplayMember = "FullName";
                cmbEmployees.ValueMember = "id";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int employeeId = (int)cmbEmployees.SelectedValue;
            DateTime date = dtpDate.Value.Date;
            TimeSpan start = dtpStart.Value.TimeOfDay;
            TimeSpan end = dtpEnd.Value.TimeOfDay;
            string description = txtDescription.Text;

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"INSERT INTO employee_schedule (employee_id, work_date, shift_start, shift_end, description)
                         VALUES (@empId, @date, @start, @end, @desc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@empId", employeeId);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@desc", description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadSchedule(); // Обновление таблицы
        }
        private void LoadSchedule()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"SELECT s.id, e.last_name + ' ' + e.first_name AS employee, s.work_date, s.shift_start, s.shift_end, s.description
                         FROM employee_schedule s
                         JOIN employees e ON s.employee_id = e.id
                         ORDER BY s.work_date DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSchedule.DataSource = dt;
            }
        }

        private void dgvSchedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSchedule.Rows[e.RowIndex];

                selectedScheduleId = Convert.ToInt32(row.Cells["id"].Value);
                cmbEmployees.Text = row.Cells["employee"].Value.ToString();
                dtpDate.Value = Convert.ToDateTime(row.Cells["work_date"].Value);
                dtpStart.Value = DateTime.Today + (TimeSpan)row.Cells["shift_start"].Value;
                dtpEnd.Value = DateTime.Today + (TimeSpan)row.Cells["shift_end"].Value;
                txtDescription.Text = row.Cells["description"].Value?.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedScheduleId == -1)
            {
                MessageBox.Show("Выберите запись для редактирования.");
                return;
            }

            int employeeId = (int)cmbEmployees.SelectedValue;
            DateTime date = dtpDate.Value.Date;
            TimeSpan start = dtpStart.Value.TimeOfDay;
            TimeSpan end = dtpEnd.Value.TimeOfDay;
            string description = txtDescription.Text;

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"UPDATE employee_schedule
                         SET employee_id = @empId, work_date = @date, shift_start = @start, shift_end = @end, description = @desc
                         WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@empId", employeeId);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@desc", description);
                cmd.Parameters.AddWithValue("@id", selectedScheduleId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            selectedScheduleId = -1;
            LoadSchedule();
            MessageBox.Show("Запись обновлена.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedScheduleId == -1)
            {
                MessageBox.Show("Сначала выберите запись.");
                return;
            }

            var confirm = MessageBox.Show("Удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = "DELETE FROM employee_schedule WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", selectedScheduleId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            selectedScheduleId = -1;
            LoadSchedule();
            MessageBox.Show("Удалено.");
        }
        private void SetupScheduleGrid()
        {
            dgvSchedule.Columns.Clear();
            dgvSchedule.AutoGenerateColumns = false;
            dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSchedule.MultiSelect = false;
            dgvSchedule.AllowUserToAddRows = false;
            dgvSchedule.ReadOnly = true;

            // ID (скрыто)
            var colId = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "id",
                HeaderText = "ID",
                Name = "id",
                Visible = false
            };
            dgvSchedule.Columns.Add(colId);

            // Сотрудник
            dgvSchedule.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "employee", 
                DataPropertyName = "employee",
                HeaderText = "Сотрудник",
                Width = 150
            });

            // Дата
            dgvSchedule.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "work_date",
                DataPropertyName = "work_date",
                HeaderText = "Дата",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" },
                Width = 100
            });

            // Начало смены
            dgvSchedule.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "shift_start",
                DataPropertyName = "shift_start",
                HeaderText = "Начало",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "hh\\:mm" },
                Width = 80
            });

            // Конец смены
            dgvSchedule.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "shift_end",
                DataPropertyName = "shift_end",
                HeaderText = "Конец",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "hh\\:mm" },
                Width = 80
            });

            // Описание
            dgvSchedule.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "description",
                DataPropertyName = "description",
                HeaderText = "Описание",
                Width = 200
            });
        }
    }
}
