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
    public partial class MeterCardControl : UserControl
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

            public int MeterId { get; }

        public event Action<int> OnEditClicked;
        public event Action<int> OnDeleteClicked;
        public event Action<int> OnReadingsClicked;
        public MeterCardControl(int meterId, string serialNumber, DateTime installationDate, bool isActive)
        {
            InitializeComponent();

            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.BorderStyle = BorderStyle.None;

            MeterId = meterId;

            lblSerial.Text = $"Серийный номер: {serialNumber}";
            lblDate.Text = $"Установлен: {installationDate:dd.MM.yyyy}";
            lblActive.Text = isActive ? "✅ Активен" : "⛔ Неактивен";

            btnEdit.Text = "✏️";
            btnDelete.Text = "🗑";
            btnReadings.Text = "📈";

            btnEdit.Click += (s, e) => OnEditClicked?.Invoke(MeterId);
            btnDelete.Click += (s, e) => OnDeleteClicked?.Invoke(MeterId);
            btnReadings.Click += (s, e) => OnReadingsClicked?.Invoke(MeterId);
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

        private void MeterCardControl_Load(object sender, EventArgs e)
        {

        }

        private void btnReadings_Click(object sender, EventArgs e)
        {
            var form = new MeterReadingsForm(MeterId);
            form.ShowDialog();
        }

        private void lblSerial_Click(object sender, EventArgs e)
        {

        }
    }
}
