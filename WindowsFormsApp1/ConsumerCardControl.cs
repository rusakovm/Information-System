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
    public partial class ConsumerCardControl : UserControl
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

        public int ConsumerId { get; private set; }

        public event Action<int> OnEditClicked;
        public event Action<int> OnDeleteClicked;
        public ConsumerCardControl(int consumerId, string fullName, string contactPerson, string phone, string email)

        {
            InitializeComponent();

            ConsumerId = consumerId;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.BorderStyle = BorderStyle.None;
            lblFullName.Text = fullName;
            lblContactPerson.Text = $"Контакт: {contactPerson}";
            lblPhone.Text = $"Телефон: {phone}";
            lblEmail.Text = $"Email: {email}";
            // Подписываемся: при любом изменении прав перескроем контролы
            Access.RightsChanged += () => ApplyRights(this);

            // И сразу при старте применяем права к этой форме
            ApplyRights(this);
            btnEdit.Text = "✏️";
            btnDelete.Text = "❌";

            btnEdit.Click += (s, e) => OnEditClicked?.Invoke(consumerId);
            btnDelete.Click += (s, e) => OnDeleteClicked?.Invoke(consumerId);
        }

        private void ConsumerCardControl_Load(object sender, EventArgs e)
        {

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

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
