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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp1
{
    public partial class EmployeeCardControl : UserControl
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

        public int EmployeeId { get; }

        public event Action<int> OnEditClicked;
        public event Action<int> OnDeleteClicked;
        public event Action<int> OnResetClicked;

        public EmployeeCardControl(int id, string lastName, string firstName, string middleName, string phone, string email)
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.BorderStyle = BorderStyle.None;
            EmployeeId = id;

            lblFIO.Text = $"{lastName} {firstName} {middleName}";
            lblPhone.Text = $"📞 {phone}";
            lblEmail.Text = $"📧 {email}";

            btnReset.Text = "🔑";
            btnReset.Click += (s, e) => OnResetClicked?.Invoke(EmployeeId);

            btnEdit.Text = "✏️";
            btnDelete.Text = "🗑";

            btnEdit.Click += (s, e) => OnEditClicked?.Invoke(EmployeeId);
            btnDelete.Click += (s, e) => OnDeleteClicked?.Invoke(EmployeeId);
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
        private void EmployeeCardControl_Load(object sender, EventArgs e)
        {

        }

        private void OnResetClicked_Click(object sender, EventArgs e)
        {

        }
    }
}
