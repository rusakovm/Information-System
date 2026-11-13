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
    public partial class RolePermissionsForm : Form
    {

        private readonly string _cs = AppConnection.ConnectionString;

        // кэшируем все разрешения (code, name)
        private DataTable _allPerms;
        public RolePermissionsForm()
        {
            InitializeComponent();
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox(
               "Введите название новой роли:", "Новая роль", "");

            if (string.IsNullOrWhiteSpace(name)) return;

            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(
                   "INSERT INTO positions(name) VALUES(@n); SELECT SCOPE_IDENTITY();", cn))
            {
                cmd.Parameters.AddWithValue("@n", name.Trim());
                cn.Open();
                int newId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            LoadRoles();
            cmbRoles.SelectedIndex = cmbRoles.FindStringExact(name.Trim());
        }
        private HashSet<string> GetRolePermissions(int roleId)
        {
            var set = new HashSet<string>();
            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(
                   "SELECT permission_code FROM position_permissions WHERE position_id=@id", cn))
            {
                cmd.Parameters.AddWithValue("@id", roleId);
                cn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read()) set.Add(r.GetString(0));
            }
            return set;
        }

        private void RolePermissionsForm_Load(object sender, EventArgs e)
        {
            LoadAllPermissions();
            LoadRoles();
        }
        private void LoadAllPermissions()
        {
            using (var cn = new SqlConnection(_cs))
            using (var da = new SqlDataAdapter(
                   "SELECT code, name FROM permissions ORDER BY name", cn))
            {
                _allPerms = new DataTable();
                da.Fill(_allPerms);
            }
        }

        /* ----------------------------------------------------------------- */
        /* 2. Загрузка ролей в ComboBox                                      */
        /* ----------------------------------------------------------------- */
        private void LoadRoles()
        {
            using (var cn = new SqlConnection(_cs))
            using (var da = new SqlDataAdapter(
                   "SELECT id, name FROM positions ORDER BY name", cn))
            {
                var tbl = new DataTable();
                da.Fill(tbl);

                cmbRoles.DataSource = tbl;
                cmbRoles.DisplayMember = "name";
                cmbRoles.ValueMember = "id";
                cmbRoles.SelectedIndex = -1;
            }
        }

        private void cmbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbRoles.SelectedIndex < 0) return;   // ещё ничего не выбрано

            /* ↓ СТАРАЯ строка, вызывавшая InvalidCastException
            int roleId = (int)cmbRoles.SelectedValue;
            */

            /* ↓ НОВЫЙ безопасный способ */
            int roleId;
            object val = cmbRoles.SelectedValue;

            if (val is int)                           // обычный случай
                roleId = (int)val;
            else if (val is DataRowView drv)          // первое срабатывание — DataRowView
                roleId = Convert.ToInt32(drv["id"]);  // "id" — поле из SELECT id,name
            else
                return;                               // непредвиденный тип → выходим

            // дальше ваш старый код
            HashSet<string> rolePerms = GetRolePermissions(roleId);

            flowPerms.Controls.Clear();
            foreach (DataRow row in _allPerms.Rows)
            {
                string code = row["code"].ToString();
                string name = row["name"].ToString();

                var cb = new CheckBox
                {
                    Text = name,
                    Tag = code,
                    AutoSize = true,
                    Checked = rolePerms.Contains(code)
                };
                flowPerms.Controls.Add(cb);
            }
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            if (cmbRoles.SelectedValue == null) return;

            if (MessageBox.Show("Удалить выбранную роль?\n(Будут удалены права, а у сотрудников – position_id обнулится)",
                                "Подтверждение",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning) != DialogResult.Yes) return;

            int roleId = (int)cmbRoles.SelectedValue;

            using (var cn = new SqlConnection(_cs))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        new SqlCommand("DELETE FROM position_permissions WHERE position_id=@id", cn, tx)
                        { Parameters = { new SqlParameter("@id", roleId) } }.ExecuteNonQuery();

                        new SqlCommand("UPDATE employees SET position_id = NULL WHERE position_id=@id", cn, tx)
                        { Parameters = { new SqlParameter("@id", roleId) } }.ExecuteNonQuery();

                        new SqlCommand("DELETE FROM positions WHERE id=@id", cn, tx)
                        { Parameters = { new SqlParameter("@id", roleId) } }.ExecuteNonQuery();

                        tx.Commit();
                        MessageBox.Show("Роль удалена.");

                        LoadRoles();
                        flowPerms.Controls.Clear();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (cmbRoles.SelectedValue == null)
            { MessageBox.Show("Сначала выберите роль."); return; }

            int roleId = (int)cmbRoles.SelectedValue;

            // собираем отмеченные коды
            var chosen = new List<string>();
            foreach (CheckBox cb in flowPerms.Controls)
                if (cb.Checked) chosen.Add(cb.Tag.ToString());

            using (var cn = new SqlConnection(_cs))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        // 1. чистим старые привязки
                        var cmd = new SqlCommand(
                            "DELETE FROM position_permissions WHERE position_id=@id", cn, tx);
                        cmd.Parameters.AddWithValue("@id", roleId);
                        cmd.ExecuteNonQuery();

                        // 2. вставляем выбранные
                        cmd = new SqlCommand(
                            "INSERT INTO position_permissions(position_id,permission_code) VALUES(@id,@code)", cn, tx);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = roleId;
                        cmd.Parameters.Add("@code", SqlDbType.NVarChar, 50);

                        foreach (string code in chosen)
                        {
                            cmd.Parameters["@code"].Value = code;
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        MessageBox.Show("Права успешно сохранены.");
                        Access.Reload();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }
    }
}
