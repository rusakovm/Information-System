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
    public partial class AddSalaryForm : Form
    {
        public AddSalaryForm()
        {
            InitializeComponent();
            Placeholder.Attach(cmbEmployee, "сотрудник");
            Placeholder.Attach(txtDesc, "Комментарий (необязательно)");
            LoadEmployees();
        }
        private void LoadEmployees()
        {
            var tbl = new DataTable();

            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var da = new SqlDataAdapter(
                @"SELECT  id,
                 last_name + ' ' +
                 first_name + ' ' +
                 ISNULL(middle_name,'')          AS fio
          FROM   employees
          ORDER  BY last_name, first_name", cn))
            {
                da.Fill(tbl);
            }

            cmbEmployee.DataSource = tbl;
            cmbEmployee.DisplayMember = "fio";   // ← показываем ФИО
            cmbEmployee.ValueMember = "id";    // ← в SelectedValue остаётся id
            cmbEmployee.SelectedIndex = -1;      // ничего не выбрано
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbEmployee.SelectedValue == null ||
                 Placeholder.GetUserText(cmbEmployee).Length == 0)
            {
                MessageBox.Show("Выберите сотрудника.");
                cmbEmployee.Focus();
                return;
            }

            if (numSum.Value <= 0)
            {
                MessageBox.Show("Сумма должна быть больше нуля.");
                numSum.Focus();
                return;
            }

            /* --- округляем до копеек, на всякий случай --- */
            decimal salary = Math.Round(numSum.Value, 2);

            DateTime payDate = dtpDate.Value.Date;          // 1-е число месяца
            int y = payDate.Year;
            int m = payDate.Month;

            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var cmd = new SqlCommand(
            @"IF EXISTS (SELECT 1 FROM employee_salaries
                 WHERE employee_id=@e AND YEAR(pay_date)=@y AND MONTH(pay_date)=@m)
      BEGIN
          UPDATE employee_salaries
             SET salary_amount = @s,
                 description   = @t,
                 pay_date      = @d
           WHERE employee_id   = @e
             AND YEAR(pay_date)= @y AND MONTH(pay_date)= @m;
      END
      ELSE
      BEGIN
          INSERT INTO employee_salaries
                 (employee_id, pay_date, salary_amount, description)
          VALUES (@e, @d, @s, @t);
      END", cn))
            {
                cmd.Parameters.AddWithValue("@e", cmbEmployee.SelectedValue);
                cmd.Parameters.AddWithValue("@d", payDate);
                cmd.Parameters.AddWithValue("@y", y);
                cmd.Parameters.AddWithValue("@m", m);
                cmd.Parameters.AddWithValue("@s", salary);
                cmd.Parameters.AddWithValue("@t", txtDesc.Text.Trim());

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void AddSalaryForm_Load(object sender, EventArgs e)
        {
            numSum.Validating += (s, ev) =>
       numSum.Value = Math.Round(numSum.Value, 2);
        }
    }
}
