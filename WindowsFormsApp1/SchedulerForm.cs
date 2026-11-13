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
    public partial class SchedulerForm : Form
    {
        public SchedulerForm()
        {
            InitializeComponent();

            // события
            Load += SchedulerForm_Load;
            cmbEmployees.SelectedIndexChanged += cmbEmployees_SelectedIndexChanged;
            btnPrev.Click += (s, e) => ShiftWeek(-7);
            btnNext.Click += (s, e) => ShiftWeek(+7);
            dtpWeek.ValueChanged += dtpWeek_ValueChanged;


            // datePicker всегда показывает понедельник
            dtpWeek.Value = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);

        }

        private void SchedulerForm_Load(object sender, EventArgs e)
        {
            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var da = new SqlDataAdapter(
                 "SELECT id, last_name + ' ' + first_name AS fio FROM employees ORDER BY fio", cn))
            {
                var tbl = new DataTable();
                da.Fill(tbl);
                cmbEmployees.DataSource = tbl;
                cmbEmployees.DisplayMember = "fio";
                cmbEmployees.ValueMember = "id";
                cmbEmployees.SelectedIndex = -1;
            }
        }

        private void cmbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmployees.SelectedIndex < 0) return;      // ничего не выбрано

            int empId;

            object val = cmbEmployees.SelectedValue;

            if (val is int)                                  // нормальный случай
                empId = (int)val;
            else if (val is DataRowView drv && drv["id"] != DBNull.Value)
                empId = Convert.ToInt32(drv["id"]);          // первое срабатывание
            else
                return;                                      // непредвиденный тип

            // всегда берём понедельник выбранной недели
            DateTime monday = dtpWeek.Value.Date.AddDays(-(int)dtpWeek.Value.DayOfWeek + 1);

            schedule.LoadWeek(empId, monday);
        }
        private void ShiftWeek(int days)
        {
            dtpWeek.Value = dtpWeek.Value.AddDays(days);
        }

        private void dtpWeek_ValueChanged(object sender, EventArgs e)
        {
            if (cmbEmployees.SelectedValue == null) return;
            int empId = (int)cmbEmployees.SelectedValue;
            schedule.LoadWeek(empId, dtpWeek.Value);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (cmbEmployees.SelectedValue == null) { MessageBox.Show("Выберите сотрудника."); return; }

            try
            {
                WeekSaver.Save(schedule);   // см. ниже
                MessageBox.Show("Расписание сохранено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (schedule.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите блок.");
                return;
            }

            if (MessageBox.Show("Удалить выбранный блок?", "Подтверждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            schedule.RemoveSelected();

        }
    }
}
