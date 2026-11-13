namespace WindowsFormsApp1
{
    partial class MetersViewControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbConsumers = new System.Windows.Forms.ComboBox();
            this.flowMeters = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddMeter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbConsumers
            // 
            this.cmbConsumers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbConsumers.Font = new System.Drawing.Font("Verdana", 12F);
            this.cmbConsumers.FormattingEnabled = true;
            this.cmbConsumers.Location = new System.Drawing.Point(259, 6);
            this.cmbConsumers.Name = "cmbConsumers";
            this.cmbConsumers.Size = new System.Drawing.Size(368, 33);
            this.cmbConsumers.TabIndex = 0;
            this.cmbConsumers.SelectedIndexChanged += new System.EventHandler(this.cmbConsumers_SelectedIndexChanged);
            // 
            // flowMeters
            // 
            this.flowMeters.AutoScroll = true;
            this.flowMeters.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMeters.Location = new System.Drawing.Point(3, 138);
            this.flowMeters.Name = "flowMeters";
            this.flowMeters.Size = new System.Drawing.Size(1058, 618);
            this.flowMeters.TabIndex = 1;
            this.flowMeters.WrapContents = false;
            // 
            // btnAddMeter
            // 
            this.btnAddMeter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAddMeter.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnAddMeter.Location = new System.Drawing.Point(633, 3);
            this.btnAddMeter.Name = "btnAddMeter";
            this.btnAddMeter.Size = new System.Drawing.Size(260, 40);
            this.btnAddMeter.TabIndex = 2;
            this.btnAddMeter.Tag = "METERS_ADD";
            this.btnAddMeter.Text = "Добавить счетчик";
            this.btnAddMeter.UseVisualStyleBackColor = true;
            this.btnAddMeter.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F);
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите потребителя";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cmbConsumers);
            this.flowLayoutPanel1.Controls.Add(this.btnAddMeter);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(53, 35);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(975, 40);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbSort);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 132);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cmbSort
            // 
            this.cmbSort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbSort.Font = new System.Drawing.Font("Verdana", 12F);
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(312, 81);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(261, 33);
            this.cmbSort.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F);
            this.label2.Location = new System.Drawing.Point(56, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Сортировка";
            // 
            // MetersViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowMeters);
            this.Name = "MetersViewControl";
            this.Size = new System.Drawing.Size(1064, 830);
            this.Load += new System.EventHandler(this.MetersViewControl_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbConsumers;
        private System.Windows.Forms.FlowLayoutPanel flowMeters;
        private System.Windows.Forms.Button btnAddMeter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Label label2;
    }
}
