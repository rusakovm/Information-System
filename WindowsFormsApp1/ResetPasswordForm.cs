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
    public partial class ResetPasswordForm : Form
    {
        private readonly int _userId;
        public ResetPasswordForm(int userId, string login)
        {
            InitializeComponent();
            _userId = userId;
            txtLogin.Text = login;
            Placeholder.Attach(txtPwd1, "Введите новый пароль");
            Placeholder.Attach(txtPwd2, "Повторите пароль");
            btnSave.Click += btnSave_Click;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string p1 = Placeholder.GetUserText(txtPwd1);
            string p2 = Placeholder.GetUserText(txtPwd2);

            // валидация длины
            if (p1.Length < 6)
            {
                MessageBox.Show("Пароль должен быть не менее 6 символов.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPwd1.Focus();
                return;
            }

            // можно добавить проверку сложности, например:
            // if (!Regex.IsMatch(p1, @"(?=.*\d)(?=.*[A-Z])")) ...

            // совпадение
            if (p1 != p2)
            {
                MessageBox.Show("Пароли не совпадают.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPwd2.Focus();
                return;
            }
            string hash = BCrypt.Net.BCrypt.HashPassword(p1);

            const string sql = @"UPDATE user_auth SET password_hash = @h
                                 WHERE id = @uid";

            using (var cn = new SqlConnection(AppConnection.ConnectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@h", hash);
                cmd.Parameters.AddWithValue("@uid", _userId);

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Пароль обновлён.");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message,
                                    "SQL",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
    }
}
