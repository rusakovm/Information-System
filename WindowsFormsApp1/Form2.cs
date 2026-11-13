using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
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

        public Form2()
        {
            InitializeComponent();
         
            asidePanel1.BorderStyle = BorderStyle.None;
            asidePanel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, asidePanel1.Width + 100, asidePanel1.Height, 20, 20));
            asidePanel2.BorderStyle = BorderStyle.None;
            asidePanel2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, asidePanel2.Width + 100, asidePanel2.Height, 20, 20));
            asidePanel3.BorderStyle = BorderStyle.None;
            asidePanel3.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, asidePanel3.Width + 100, asidePanel3.Height, 20, 20));
            asidePanel4.BorderStyle = BorderStyle.None;
            asidePanel4.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, asidePanel4.Width + 100, asidePanel4.Height, 20, 20));
            asidePanel5.BorderStyle = BorderStyle.None;
            asidePanel5.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, asidePanel5.Width + 100, asidePanel5.Height, 20, 20));
            asidePanel6.BorderStyle = BorderStyle.None;
            asidePanel6.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, asidePanel6.Width + 100, asidePanel6.Height, 20, 20));
            asidePanel7.BorderStyle = BorderStyle.None;
            asidePanel7.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, asidePanel6.Width + 100, asidePanel6.Height, 20, 20));



           
            panel2.MouseDown += TitleBar_MouseDown;
            Access.InfoChanged += OnAccessInfoChanged;


        }
        private void OnAccessInfoChanged()
        {
            lblUserName.Text = Access.FullName;
            lblPosition.Text = Access.PositionName;
            picAvatar.Image = Access.PhotoImage;
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

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



        private void flowLayoutPanel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void flowEmployees_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void asidePanel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void asidePanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void asidePanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        public void asidePanelClear()
        {
            asidePanel1.BackColor = Color.Transparent;
            asidePanel2.BackColor = Color.Transparent;
            asidePanel3.BackColor = Color.Transparent;
            asidePanel4.BackColor = Color.Transparent;
            asidePanel5.BackColor = Color.Transparent;
            asidePanel6.BackColor = Color.Transparent;
            asidePanel7.BackColor = Color.Transparent;
            label1.ForeColor = Color.FromArgb(240, 235, 243);
            label2.ForeColor = Color.FromArgb(240, 235, 243);
            label3.ForeColor = Color.FromArgb(240, 235, 243);
            label5.ForeColor = Color.FromArgb(240, 235, 243);
            label6.ForeColor = Color.FromArgb(240, 235, 243);
            label7.ForeColor = Color.FromArgb(240, 235, 243);
            label8.ForeColor = Color.FromArgb(240, 235, 243);
            consumerViewControl1.Visible = false;
            viewReceiptControl1.Visible = false;
            metersViewControl1.Visible = false;
            employeeViewControl1.Visible = false;
            servicesViewControl1.Visible = false;
            
        }

        private void asidePanel2_MouseClick(object sender, MouseEventArgs e)
        {
            
            asidePanelClear();
            viewReceiptControl1.BringToFront();
            viewReceiptControl1.Visible = true;
            label2.ForeColor = Color.FromArgb(0, 121, 194);
            asidePanel2.BackColor = Color.FromArgb(240, 235, 243);
          
        }

        private void asidePanel1_MouseClick(object sender, MouseEventArgs e)
        {
            asidePanelClear();
            consumerViewControl1.BringToFront();
            consumerViewControl1.Visible = true;
            label1.ForeColor = Color.FromArgb(0, 121, 194);
            asidePanel1.BackColor = Color.FromArgb(238, 228, 247);
        }

        private void asidePanel3_MouseClick(object sender, MouseEventArgs e)
        {
            asidePanelClear();
            metersViewControl1.BringToFront();
            metersViewControl1.Visible = true;
            label3.ForeColor = Color.FromArgb(0, 121, 194);
            asidePanel3.BackColor = Color.FromArgb(238, 228, 247);
        }

        private void asidePanel4_MouseClick(object sender, MouseEventArgs e)
        {
            asidePanelClear();
            label5.ForeColor = Color.FromArgb(0, 121, 194);
            asidePanel4.BackColor = Color.FromArgb(238, 228, 247);
            employeeViewControl1.BringToFront();
            employeeViewControl1.Visible = true;
        }

        private void asidePanel5_MouseClick(object sender, MouseEventArgs e)
        {
            asidePanelClear();
            label6.ForeColor = Color.FromArgb(0, 121, 194);
            asidePanel5.BackColor = Color.FromArgb(238, 228, 247);
            servicesViewControl1.BringToFront();
            servicesViewControl1.Visible = true;
        }

        private void asidePanel6_MouseClick(object sender, MouseEventArgs e)
        {
            asidePanelClear();
            label7.ForeColor = Color.FromArgb(0, 121, 194);
            asidePanel6.BackColor = Color.FromArgb(238, 228, 247);
        }

        private void asidePanel7_MouseClick(object sender, MouseEventArgs e)
        {
            asidePanelClear();
            label8.ForeColor = Color.FromArgb(0, 121, 194);
            asidePanel7.BackColor = Color.FromArgb(238, 228, 247);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            ApplyRights(this);
            lblUserName.Text = Access.FullName;      // «User User»
            lblPosition.Text = Access.PositionName;
            picAvatar.Image = Access.PhotoImage;
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.Size = new Size(80, 80);          // или 96x96
            picAvatar.BorderStyle = BorderStyle.FixedSingle;    // опционально
            Access.RightsChanged += () => ApplyRights(this);   // перестраиваем видимость
            asidePanelClear();
            ApplyRights(this);

        }
        
        private static void ApplyRights(Control root)
        {
            foreach (Control c in root.Controls)
            {
                if (c.Tag is string code)
                    c.Visible = Access.Has(code);   // нужно скрыть? ставим Enabled = false

                if (c.HasChildren)
                    ApplyRights(c);
            }
        }
        

        private void servicesViewControl1_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }


    }
