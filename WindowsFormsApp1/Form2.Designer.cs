namespace WindowsFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.asidePanel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.asidePanel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.asidePanel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.asidePanel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.asidePanel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.asidePanel7 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.asidePanel6 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.servicesViewControl1 = new WindowsFormsApp1.ServicesViewControl();
            this.employeeViewControl1 = new WindowsFormsApp1.EmployeeViewControl();
            this.metersViewControl1 = new WindowsFormsApp1.MetersViewControl();
            this.viewReceiptControl1 = new WindowsFormsApp1.viewReceiptControl();
            this.consumerViewControl1 = new WindowsFormsApp1.ConsumerViewControl();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.asidePanel1.SuspendLayout();
            this.asidePanel2.SuspendLayout();
            this.asidePanel3.SuspendLayout();
            this.asidePanel4.SuspendLayout();
            this.asidePanel5.SuspendLayout();
            this.asidePanel7.SuspendLayout();
            this.asidePanel6.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(680, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1222, 64);
            this.panel2.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.servicesViewControl1);
            this.panel4.Controls.Add(this.employeeViewControl1);
            this.panel4.Controls.Add(this.metersViewControl1);
            this.panel4.Controls.Add(this.viewReceiptControl1);
            this.panel4.Controls.Add(this.consumerViewControl1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(680, 64);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1222, 969);
            this.panel4.TabIndex = 5;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Controls.Add(this.asidePanel7);
            this.panel1.Controls.Add(this.asidePanel6);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 1033);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel2.Controls.Add(this.asidePanel1);
            this.flowLayoutPanel2.Controls.Add(this.asidePanel2);
            this.flowLayoutPanel2.Controls.Add(this.asidePanel3);
            this.flowLayoutPanel2.Controls.Add(this.asidePanel4);
            this.flowLayoutPanel2.Controls.Add(this.asidePanel5);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(338, 444);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(342, 366);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // asidePanel1
            // 
            this.asidePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(235)))), ((int)(((byte)(243)))));
            this.asidePanel1.Controls.Add(this.label1);
            this.asidePanel1.Location = new System.Drawing.Point(3, 3);
            this.asidePanel1.Name = "asidePanel1";
            this.asidePanel1.Size = new System.Drawing.Size(339, 56);
            this.asidePanel1.TabIndex = 2;
            this.asidePanel1.Tag = "CONSUMERS_VIEW";
            this.asidePanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.asidePanel_Paint);
            this.asidePanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel1_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(194)))));
            this.label1.Location = new System.Drawing.Point(19, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Потребители";
            this.label1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel1_MouseClick);
            // 
            // asidePanel2
            // 
            this.asidePanel2.BackColor = System.Drawing.Color.Transparent;
            this.asidePanel2.Controls.Add(this.label2);
            this.asidePanel2.Location = new System.Drawing.Point(3, 65);
            this.asidePanel2.Name = "asidePanel2";
            this.asidePanel2.Size = new System.Drawing.Size(339, 56);
            this.asidePanel2.TabIndex = 12;
            this.asidePanel2.Tag = "PAYMENTS_VIEW";
            this.asidePanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.asidePanel2_Paint);
            this.asidePanel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel2_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(228)))), ((int)(((byte)(247)))));
            this.label2.Location = new System.Drawing.Point(19, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Квитанции";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            this.label2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel2_MouseClick);
            // 
            // asidePanel3
            // 
            this.asidePanel3.BackColor = System.Drawing.Color.Transparent;
            this.asidePanel3.Controls.Add(this.label3);
            this.asidePanel3.Location = new System.Drawing.Point(3, 127);
            this.asidePanel3.Name = "asidePanel3";
            this.asidePanel3.Size = new System.Drawing.Size(339, 56);
            this.asidePanel3.TabIndex = 13;
            this.asidePanel3.Tag = "METERS_VIEW";
            this.asidePanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.asidePanel3_Paint);
            this.asidePanel3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel3_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(228)))), ((int)(((byte)(247)))));
            this.label3.Location = new System.Drawing.Point(19, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Данные счетчиков";
            this.label3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel3_MouseClick);
            // 
            // asidePanel4
            // 
            this.asidePanel4.BackColor = System.Drawing.Color.Transparent;
            this.asidePanel4.Controls.Add(this.label5);
            this.asidePanel4.Location = new System.Drawing.Point(3, 189);
            this.asidePanel4.Name = "asidePanel4";
            this.asidePanel4.Size = new System.Drawing.Size(339, 56);
            this.asidePanel4.TabIndex = 14;
            this.asidePanel4.Tag = "EMPLOYEES_VIEW";
            this.asidePanel4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel4_MouseClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(228)))), ((int)(((byte)(247)))));
            this.label5.Location = new System.Drawing.Point(19, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "Сотрудники";
            this.label5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel4_MouseClick);
            // 
            // asidePanel5
            // 
            this.asidePanel5.BackColor = System.Drawing.Color.Transparent;
            this.asidePanel5.Controls.Add(this.label6);
            this.asidePanel5.Location = new System.Drawing.Point(3, 251);
            this.asidePanel5.Name = "asidePanel5";
            this.asidePanel5.Size = new System.Drawing.Size(339, 56);
            this.asidePanel5.TabIndex = 15;
            this.asidePanel5.Tag = "SERVICES_VIEW";
            this.asidePanel5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel5_MouseClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(228)))), ((int)(((byte)(247)))));
            this.label6.Location = new System.Drawing.Point(19, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Услуи";
            this.label6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel5_MouseClick);
            // 
            // asidePanel7
            // 
            this.asidePanel7.BackColor = System.Drawing.Color.Transparent;
            this.asidePanel7.Controls.Add(this.label8);
            this.asidePanel7.Location = new System.Drawing.Point(341, 878);
            this.asidePanel7.Name = "asidePanel7";
            this.asidePanel7.Size = new System.Drawing.Size(339, 56);
            this.asidePanel7.TabIndex = 17;
            this.asidePanel7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel7_MouseClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(228)))), ((int)(((byte)(247)))));
            this.label8.Location = new System.Drawing.Point(19, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(211, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "Служба поддержки";
            this.label8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel7_MouseClick);
            // 
            // asidePanel6
            // 
            this.asidePanel6.BackColor = System.Drawing.Color.Transparent;
            this.asidePanel6.Controls.Add(this.label7);
            this.asidePanel6.Location = new System.Drawing.Point(341, 816);
            this.asidePanel6.Name = "asidePanel6";
            this.asidePanel6.Size = new System.Drawing.Size(339, 56);
            this.asidePanel6.TabIndex = 16;
            this.asidePanel6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel6_MouseClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(228)))), ((int)(((byte)(247)))));
            this.label7.Location = new System.Drawing.Point(19, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "Настройки";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            this.label7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asidePanel6_MouseClick);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.picAvatar);
            this.panel9.Controls.Add(this.lblPosition);
            this.panel9.Controls.Add(this.lblUserName);
            this.panel9.Location = new System.Drawing.Point(0, 185);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(680, 221);
            this.panel9.TabIndex = 16;
            this.panel9.Paint += new System.Windows.Forms.PaintEventHandler(this.panel9_Paint);
            // 
            // picAvatar
            // 
            this.picAvatar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picAvatar.Location = new System.Drawing.Point(292, 26);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(100, 100);
            this.picAvatar.TabIndex = 18;
            this.picAvatar.TabStop = false;
            // 
            // lblPosition
            // 
            this.lblPosition.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPosition.BackColor = System.Drawing.Color.Transparent;
            this.lblPosition.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPosition.ForeColor = System.Drawing.Color.Black;
            this.lblPosition.Location = new System.Drawing.Point(0, 174);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(680, 29);
            this.lblPosition.TabIndex = 17;
            this.lblPosition.Text = "User User";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Font = new System.Drawing.Font("Verdana", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUserName.ForeColor = System.Drawing.Color.Black;
            this.lblUserName.Location = new System.Drawing.Point(0, 129);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(680, 45);
            this.lblUserName.TabIndex = 16;
            this.lblUserName.Text = "User User";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(174, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(303, 160);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(228)))), ((int)(((byte)(247)))));
            this.label4.Location = new System.Drawing.Point(32, 1041);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Copywright";
            // 
            // servicesViewControl1
            // 
            this.servicesViewControl1.Location = new System.Drawing.Point(16, 16);
            this.servicesViewControl1.Name = "servicesViewControl1";
            this.servicesViewControl1.Size = new System.Drawing.Size(1082, 830);
            this.servicesViewControl1.TabIndex = 4;
            this.servicesViewControl1.Load += new System.EventHandler(this.servicesViewControl1_Load);
            // 
            // employeeViewControl1
            // 
            this.employeeViewControl1.Location = new System.Drawing.Point(16, 16);
            this.employeeViewControl1.Name = "employeeViewControl1";
            this.employeeViewControl1.Size = new System.Drawing.Size(1064, 830);
            this.employeeViewControl1.TabIndex = 3;
            // 
            // metersViewControl1
            // 
            this.metersViewControl1.Location = new System.Drawing.Point(16, 16);
            this.metersViewControl1.Name = "metersViewControl1";
            this.metersViewControl1.Size = new System.Drawing.Size(1064, 830);
            this.metersViewControl1.TabIndex = 2;
            // 
            // viewReceiptControl1
            // 
            this.viewReceiptControl1.Location = new System.Drawing.Point(16, 16);
            this.viewReceiptControl1.Name = "viewReceiptControl1";
            this.viewReceiptControl1.Size = new System.Drawing.Size(1064, 830);
            this.viewReceiptControl1.TabIndex = 1;
            // 
            // consumerViewControl1
            // 
            this.consumerViewControl1.Location = new System.Drawing.Point(16, 16);
            this.consumerViewControl1.Name = "consumerViewControl1";
            this.consumerViewControl1.Size = new System.Drawing.Size(1082, 830);
            this.consumerViewControl1.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.asidePanel1.ResumeLayout(false);
            this.asidePanel1.PerformLayout();
            this.asidePanel2.ResumeLayout(false);
            this.asidePanel2.PerformLayout();
            this.asidePanel3.ResumeLayout(false);
            this.asidePanel3.PerformLayout();
            this.asidePanel4.ResumeLayout(false);
            this.asidePanel4.PerformLayout();
            this.asidePanel5.ResumeLayout(false);
            this.asidePanel5.PerformLayout();
            this.asidePanel7.ResumeLayout(false);
            this.asidePanel7.PerformLayout();
            this.asidePanel6.ResumeLayout(false);
            this.asidePanel6.PerformLayout();
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel asidePanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel asidePanel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel asidePanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel asidePanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel asidePanel6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel asidePanel5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel asidePanel7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.PictureBox picAvatar;
        private ConsumerViewControl consumerViewControl1;
        private viewReceiptControl viewReceiptControl1;
        private MetersViewControl metersViewControl1;
        private EmployeeViewControl employeeViewControl1;
        private ServicesViewControl servicesViewControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}