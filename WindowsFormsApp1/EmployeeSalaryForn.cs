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
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class EmployeeSalaryForn : Form
    {
        public EmployeeSalaryForn()
        {
            InitializeComponent();
        }


        private void EmployeeSalaryForn_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            LoadSalaryChart();
        }
        private void LoadEmployees()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT id, last_name + ' ' + first_name AS FullName FROM employees", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbEmployee.DataSource = dt;
                cmbEmployee.DisplayMember = "FullName";
                cmbEmployee.ValueMember = "id";
                cmbEmployee.SelectedIndex = -1;
            }
        }
        private void LoadSalaryChart()
        {
            chartSalaries.Series.Clear();
            chartSalaries.ChartAreas.Clear();
            chartSalaries.Titles.Clear();

            ChartArea area = new ChartArea("MainArea");
            chartSalaries.ChartAreas.Add(area);

            Series series = new Series("Зарплата")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String
            };

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"
    SELECT e.last_name + ' ' + e.first_name AS FullName, SUM(s.salary_amount) AS TotalSalary
    FROM employee_salaries s
    JOIN employees e ON s.employee_id = e.id
    GROUP BY e.last_name + ' ' + e.first_name
    ORDER BY TotalSalary DESC";


                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    decimal salary = reader.GetDecimal(1);
                    series.Points.AddXY(name, salary);
                }
            }

            chartSalaries.Series.Add(series);
            chartSalaries.Titles.Add("Общая зарплата сотрудников");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Введите корректную сумму.");
                return;
            }

            int employeeId = (int)cmbEmployee.SelectedValue;
            DateTime payDate = dtpPayDate.Value.Date;
            string description = txtDescription.Text;

            using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
            {
                string query = @"INSERT INTO employee_salaries (employee_id, salary_amount, pay_date, description)
                             VALUES (@empId, @amount, @date, @desc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@empId", employeeId);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@date", payDate);
                cmd.Parameters.AddWithValue("@desc", description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadSalaryChart();
            MessageBox.Show("Зарплата добавлена.");
            
        }
        

        private void cmbEmployee_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }
    }
}
