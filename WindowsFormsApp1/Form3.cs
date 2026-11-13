using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3(string expectedCode)
        {
            InitializeComponent();
            _expectedCode = expectedCode;
        }

        private readonly string _expectedCode;

        public bool IsConfirmed { get; private set; } = false;

      
           
        

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredCode = txtCode.Text.Trim();

            if (enteredCode == _expectedCode)
            {
                IsConfirmed = true;
                MessageBox.Show("Код подтверждён. Продолжаем регистрацию.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный код. Попробуйте снова.");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
