using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data.SqlClient;
using BCrypt.Net;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect,
        // x-coordinate of upper-left corner
        int nTopRect,
        // y-coordinate of upper-left corner
        int nRightRect,// x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
        );

       

        public Form1()
        {

            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            //Resize += (s, e) => Invalidate();
            //this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            panel3.BorderStyle = BorderStyle.None;
            panel3.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel3.Width + 100, panel3.Height, 20, 20));
            asidePanel.BorderStyle = BorderStyle.None;
            asidePanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, asidePanel.Width + 100, asidePanel.Height, 20, 20));

            pictureBox5.BorderStyle = BorderStyle.None;
            pictureBox5.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pictureBox5.Width, pictureBox5.Height, 40, 40));
            userControlIn1.BringToFront();
            userControlIn1.Visible = true;
            userControlReg1.Visible = false;
            /*btnMax.Click += (s, e) => WindowState = FormWindowState.Minimized;

            btnMax.Click += (s, e) =>
            {
                if (WindowState == FormWindowState.Normal)
                {
                    WindowState = FormWindowState.Maximized;
                    btnMax.Text = "\u25A3";     // 🗗 — «восстановить»
                }
                else
                {
                    WindowState = FormWindowState.Normal;
                    btnMax.Text = "\u25A1";     // 🗖 — «развернуть»
                }
            };

            btnClose.Click += (s, e) => Close();

            // — перетаскивание формы мышью —
            tableLayoutPanel1.MouseDown += TitleBar_MouseDown;

            */

        }
        
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(
            IntPtr hWnd, int msg, int wParam, int lParam);
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void layoutUser_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void layoutPassword_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            asidePanel_Click(sender, e);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var authService = new AuthService();
           // bool success = authService.AuthenticateUser(textBox2.Text, textBox1.Text);
           /*
            if (success)
            {
                MessageBox.Show("Успешный вход!");
                // переход к главной форме
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }*/
            
        }
        
        public class AuthService
        {

            public bool RegisterUser(string username, string password)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
                {
                    string query = "INSERT INTO user_auth (username, password_hash) VALUES (@username, @password_hash)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hashedPassword);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        // Обработка ошибок (например, пользователь уже существует)
                        Console.WriteLine("Ошибка: " + ex.Message);
                        return false;
                    }
                }
            }
            public bool AuthenticateUser(string username, string password)
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
                {
                    string query = "SELECT password_hash FROM user_auth WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string storedHash = reader.GetString(0);
                        return BCrypt.Net.BCrypt.Verify(password, storedHash);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public void AddDefaultAdmin()
            {
                string username = "admin";
                string password = "admin123";
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                using (SqlConnection conn = new SqlConnection(AppConnection.ConnectionString))
                {
                    string query = "IF NOT EXISTS (SELECT 1 FROM user_auth WHERE username = @username) " +
                                   "INSERT INTO user_auth (username, password_hash) VALUES (@username, @password_hash)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hashedPassword);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Пользователь 'admin' добавлен или уже существует.");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Ошибка при добавлении admin: " + ex.Message);
                    }
                }
            }
        }
        

        private void asidePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void asidePanel_Click(object sender, EventArgs e)
        {
            userControlIn1.BringToFront();
            userControlIn1.Visible = true;
            userControlReg1.Visible = false;
            asidePanel.BackColor = Color.FromArgb(240, 235, 243);
            panel3.BackColor = Color.Transparent;
            label2.ForeColor = Color.FromArgb(238, 228, 247);
            label1.ForeColor = Color.FromArgb(0, 121, 194);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            userControlReg1.BringToFront();
            userControlReg1.Visible = true;
            userControlIn1.Visible = false;
            panel3.BackColor = Color.FromArgb(240, 235, 243);
            asidePanel.BackColor = Color.Transparent;
            label1.ForeColor = Color.FromArgb(238, 228, 247);
            label2.ForeColor = Color.FromArgb(0, 121, 194);

        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            panel3_Click(sender, e);
        }

        private void userControlReg1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMax_Click(object sender, EventArgs e)
        {

        }
         
        
    }
    
}
