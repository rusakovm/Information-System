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
    public partial class AddServiceForm : Form
    {
        public AddServiceForm()
        {
            InitializeComponent();


            numPrice.Value = 0;
            Placeholder.Attach(txtName, "Название услуги");
            Placeholder.Attach(txtDesc, "Описание (необязательно)");

            chkActive.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string desc = txtDesc.Text.Trim();
            decimal price = numPrice.Value;
            bool isActive = chkActive.Checked;

            if (Placeholder.GetUserText(txtName) == "")
            {
                MessageBox.Show("Введите название услуги.");
                txtName.Focus(); return;
            }

            if (numPrice.Value <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля.");
                numPrice.Focus(); return;
            }

            if (MessageBox.Show($"Добавить услугу «{name}» за {price:C} ?",
                                "Подтверждение",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (InsertService(name, desc, price, isActive))
            {
                DialogResult = DialogResult.OK;  // сигнал вызывающему коду
                Close();
            }
        }
        private bool InsertService(string name, string desc,
                                   decimal price, bool active)
        {
            const string sql = @"INSERT INTO services
                                 (name, description, price, is_active)
                                 VALUES (@n,@d,@p,@a)";

            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@d", (object)desc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@p", price);
                cmd.Parameters.AddWithValue("@a", active);

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message,
                                    "SQL",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void AddServiceForm_Load(object sender, EventArgs e)
        {
            numPrice.Validating += (s, ev) =>
numPrice.Value = Math.Round(numPrice.Value, 2);
        }
    }
}
