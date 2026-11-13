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
    public partial class SalaryChartForm : Form
    {
         private DateTime _currentMonth;
        public SalaryChartForm()
        {
            InitializeComponent();
            _currentMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            btnPrev.Click += (s, e) => ShiftMonth(-1);
            btnNext.Click += (s, e) => ShiftMonth(+1);
            cmbMonth.SelectedIndexChanged += (s, e) =>
            {
                if (cmbMonth.SelectedIndex >= 0)
                    _currentMonth = (DateTime)cmbMonth.SelectedItem;
                LoadChart();
            };
         

            InitMonthCombo();
            LoadChart();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new AddSalaryForm())
                if (f.ShowDialog(this) == DialogResult.OK)
                    LoadChart();          // обновляем после добавления
        }
        private void InitMonthCombo()
        {
            cmbMonth.Items.Clear();
            // например, последние 24 месяца
            DateTime m = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            for (int i = 0; i < 24; i++, m = m.AddMonths(-1))
                cmbMonth.Items.Add(m);

            cmbMonth.DisplayMember = "ToString";   // DateTime => 01.2025 …
            cmbMonth.Format += (s, e) =>
                e.Value = ((DateTime)e.Value).ToString("MMMM yyyy");

            cmbMonth.SelectedItem = _currentMonth;
        }
        private void ShiftMonth(int delta)
        {
            _currentMonth = _currentMonth.AddMonths(delta);
            cmbMonth.SelectedItem = _currentMonth;      // синхронизация
            if (cmbMonth.SelectedItem == null)          // вышли за границы списка
            {
                cmbMonth.Items.Insert(0, _currentMonth);
                cmbMonth.SelectedIndex = 0;
            }
            LoadChart();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void SalaryChartForm_Load(object sender, EventArgs e)
        {

        }
        private void LoadChart()
        {
            lblMonth.Text = _currentMonth.ToString("MMMM yyyy");

            /* ---------- 1. читаем суммы из БД ---------- */
            DataTable tbl = new DataTable();

            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var da = new SqlDataAdapter(
                @"SELECT
              e.last_name  + ' ' +
              LEFT(e.first_name ,1)+'.' +
              LEFT(e.middle_name,1)+'.'      AS fio,
              SUM(s.salary_amount)           AS total
          FROM employee_salaries s
          JOIN employees e ON e.id = s.employee_id
          WHERE YEAR(s.pay_date)  = @y
            AND MONTH(s.pay_date) = @m
          GROUP BY e.last_name, e.first_name, e.middle_name
          ORDER BY total DESC", cn))
            {
                da.SelectCommand.Parameters.AddWithValue("@y", _currentMonth.Year);
                da.SelectCommand.Parameters.AddWithValue("@m", _currentMonth.Month);
                da.Fill(tbl);
            }

            /* ---------- 2. получаем/создаём серию ---------- */
            Series ser;
            if (chart.Series.IndexOf("SalarySeries") >= 0)
                ser = chart.Series["SalarySeries"];
            else
            {
                ser = chart.Series.Add("SalarySeries");
                ser.ChartType = SeriesChartType.Column;
                ser.Color = Color.SteelBlue;
                ser.IsValueShownAsLabel = false;
            }
            ser.Points.Clear();

            /* ---------- 3. заполняем точки ---------- */
            foreach (DataRow r in tbl.Rows)
            {
                string fio = r.Field<string>("fio");
                decimal sum = r.Field<decimal>("total");

                int idx = ser.Points.AddY(sum);
                var pt = ser.Points[idx];

                pt.AxisLabel = fio;
                pt.ToolTip = $"{fio}\n{sum:n0} руб.";
            }

            /* ---------- 4. подписи осей и углы ---------- */
            var ca = chart.ChartAreas[0];
            ca.AxisY.Title = "руб";
            ca.AxisX.Title = "Сотрудник";
            ca.AxisX.LabelStyle.Angle = -30;      // наклон фамилий
            ca.RecalculateAxesScale();
        }
    }
}
