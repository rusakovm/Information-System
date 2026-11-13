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
    public partial class EmployeeViewControl : UserControl
    {
        public EmployeeViewControl()
        {
            InitializeComponent();
            InitSortCombo();
            txtSearch.TextChanged += (s, e) => LoadEmployees();
            LoadEmployees();
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

        private void flowEmployees_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadEmployees()
        {
            flowEmployees.Controls.Clear();

            /*** 1. ORDER BY из cmbSort ***/
            string orderBy;
            switch (cmbSort.SelectedIndex)
            {
                case 1: orderBy = "ORDER BY last_name DESC, first_name DESC"; break;
                case 2: orderBy = "ORDER BY hire_date DESC"; break;
                case 3: orderBy = "ORDER BY hire_date ASC"; break;
                default: orderBy = "ORDER BY last_name, first_name"; break;   // индекс 0
            }

            /*** 2. WHERE – фильтр поиска по фамилии/имени ***/
            string where = "";
            string search = txtSearch.Text.Trim();
            if (search.Length > 0)
                where = "WHERE last_name LIKE @p OR first_name LIKE @p OR middle_name LIKE @p";

            string sql = @"
SELECT id,
       last_name, first_name, middle_name,
       phone_number, email
FROM   employees
" + where + @"
" + orderBy;

            using (SqlConnection cn = new SqlConnection(AppConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (where.Length > 0)
                    cmd.Parameters.AddWithValue("@p", "%" + search + "%");

                cn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        int id = r.GetInt32(0);
                        string lastName = r.GetString(1);
                        string firstName = r.GetString(2);
                        string middleName = r.IsDBNull(3) ? "" : r.GetString(3);
                        string phone = r.IsDBNull(4) ? "" : r.GetString(4);
                        string email = r.IsDBNull(5) ? "" : r.GetString(5);

                        EmployeeCardControl card = new EmployeeCardControl(
                                                     id, lastName, firstName,
                                                     middleName, phone, email);
                        card.OnEditClicked += EditEmployee;
                        card.OnDeleteClicked += DeleteEmployee;
                        card.OnResetClicked += ResetPassword;

                        flowEmployees.Controls.Add(card);
                    }
                }
            }
        }

        private void EditEmployee(int employeeId)
        {
            var form = new EditEmployeeForm(employeeId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void InitSortCombo()
        {
            cmbSort.Items.Add("Фамилия A → Я");
            cmbSort.Items.Add("Фамилия Я → A");
            cmbSort.Items.Add("Дата найма: новые");
            cmbSort.Items.Add("Дата найма: старые");

            cmbSort.SelectedIndex = 0;                 // сортировка по умолчанию
            cmbSort.SelectedIndexChanged += (s, e) => LoadEmployees();
        }
        private void ResetPassword(int employeeId)
        {
            // найдём user_id + login
            int userId = 0; string login = "";
            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            {
                var cmd = new SqlCommand(
                    @"SELECT ua.id, ua.username
              FROM employees e JOIN user_auth ua ON ua.id = e.user_id
              WHERE e.id = @eid", cn);
                cmd.Parameters.AddWithValue("@eid", employeeId);
                cn.Open();
                using (var r = cmd.ExecuteReader())
                    if (r.Read()) { userId = r.GetInt32(0); login = r.GetString(1); }
            }
            if (userId == 0) { MessageBox.Show("Учётка не найдена."); return; }

            using (var dlg = new ResetPasswordForm(userId, login))
                dlg.ShowDialog(this);
        }


        private void DeleteEmployee(int employeeId)
        {
            var result = MessageBox.Show("Удалить сотрудника?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM employees WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", employeeId);
                    cmd.ExecuteNonQuery();
                }

                LoadEmployees();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var f = new SchedulerForm())   // ← та форма-хост
                f.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var f = new SalaryChartForm())
                f.ShowDialog(this);
        }

        private void EmployeeViewControl_Load(object sender, EventArgs e)
        {

        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            using (var dlg = new RolePermissionsForm())
                dlg.ShowDialog(this);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
