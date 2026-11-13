using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ServiceCardControl : UserControl
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
        public int ServiceId { get; }

        public event Action<int> OnOrderClicked;
        public event Action<int> OnDeleteClicked;
        public ServiceCardControl(int id, string name, string description, decimal price)
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.BorderStyle = BorderStyle.None;
            ServiceId = id;

            lblName.Text = name;
            lblDescription.Text = description;
            lblPrice.Text = $"{price:F2} ₽";

            btnOrder.Text = "🛒 Заказать";
            btnDelete.Text = "❌";
            btnOrder.Click += (s, e) => OnOrderClicked?.Invoke(ServiceId);
            btnDelete.Click += (s, e) => OnDeleteClicked?.Invoke(ServiceId);
        }

        private void ServiceCardControl_Load(object sender, EventArgs e)
        {

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
