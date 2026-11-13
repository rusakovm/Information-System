using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TitleColorDialog : Form
    {

        public string TitleText => txtTitle.Text.Trim();
        public Color SelectedColor { get; private set; } = Color.SteelBlue;

        public TitleColorDialog()
        {
            InitializeComponent();
           
            btnOK.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Введите описание блока.");
                    DialogResult = DialogResult.None; // не закрывать диалог
                }
            };
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using (var cd = new ColorDialog())
            {
                cd.Color = SelectedColor;
                if (cd.ShowDialog(this) == DialogResult.OK)
                {
                    SelectedColor = cd.Color;
                    pnlPreview.BackColor = cd.Color;
                }
            }
        }

        private void TitleColorDialog_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
