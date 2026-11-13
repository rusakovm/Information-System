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
    public partial class ReadingCardControl : UserControl
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
        public event Action<int> OnEditClicked;
        public event Action<int> OnDeleteClicked;
        public int ReadingId { get; }
        public ReadingCardControl(int readingId, decimal value, DateTime date)
        {
            InitializeComponent();
            ReadingId = readingId;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.BorderStyle = BorderStyle.None;

            lblValue.Text = $"📈 {value:F2} м³";
            lblDate.Text = $"📅 {date:dd.MM.yyyy}";

            btnEdit.Click += (s, e) => OnEditClicked?.Invoke(ReadingId);
            btnDelete.Click += (s, e) => OnDeleteClicked?.Invoke(ReadingId);
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
        private void ReadingCardControl_Load(object sender, EventArgs e)
        {

        }
    }
}
