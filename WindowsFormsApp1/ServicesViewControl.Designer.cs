namespace WindowsFormsApp1
{
    partial class ServicesViewControl
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
            this.flowServices = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbConsumers = new System.Windows.Forms.ComboBox();
            this.btnViewOrders = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddService = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowServices
            // 
            this.flowServices.AutoScroll = true;
            this.flowServices.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowServices.Location = new System.Drawing.Point(0, 139);
            this.flowServices.Name = "flowServices";
            this.flowServices.Size = new System.Drawing.Size(1064, 688);
            this.flowServices.TabIndex = 0;
            this.flowServices.WrapContents = false;
            this.flowServices.Paint += new System.Windows.Forms.PaintEventHandler(this.flowServices_Paint);
            // 
            // cmbConsumers
            // 
            this.cmbConsumers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbConsumers.Font = new System.Drawing.Font("Verdana", 12F);
            this.cmbConsumers.FormattingEnabled = true;
            this.cmbConsumers.Location = new System.Drawing.Point(259, 6);
            this.cmbConsumers.Name = "cmbConsumers";
            this.cmbConsumers.Size = new System.Drawing.Size(368, 33);
            this.cmbConsumers.TabIndex = 1;
            this.cmbConsumers.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnViewOrders
            // 
            this.btnViewOrders.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnViewOrders.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnViewOrders.Location = new System.Drawing.Point(633, 3);
            this.btnViewOrders.Name = "btnViewOrders";
            this.btnViewOrders.Size = new System.Drawing.Size(160, 40);
            this.btnViewOrders.TabIndex = 2;
            this.btnViewOrders.Text = "Посмотреть заказы";
            this.btnViewOrders.UseVisualStyleBackColor = true;
            this.btnViewOrders.Click += new System.EventHandler(this.btnViewOrders_Click);
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
            // btnAddService
            // 
            this.btnAddService.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAddService.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnAddService.Location = new System.Drawing.Point(799, 3);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(160, 40);
            this.btnAddService.TabIndex = 4;
            this.btnAddService.Tag = "SERVICES_ADD";
            this.btnAddService.Text = "Добавить";
            this.btnAddService.UseVisualStyleBackColor = true;
            this.btnAddService.Click += new System.EventHandler(this.btnAddService_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cmbConsumers);
            this.flowLayoutPanel1.Controls.Add(this.btnViewOrders);
            this.flowLayoutPanel1.Controls.Add(this.btnAddService);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(23, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(992, 40);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbSort);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 133);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Verdana", 12F);
            this.txtSearch.Location = new System.Drawing.Point(576, 72);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(406, 32);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F);
            this.label3.Location = new System.Drawing.Point(490, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Поиск";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F);
            this.label2.Location = new System.Drawing.Point(21, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Сортировка";
            // 
            // cmbSort
            // 
            this.cmbSort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbSort.Font = new System.Drawing.Font("Verdana", 12F);
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(160, 71);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(317, 33);
            this.cmbSort.TabIndex = 6;
            // 
            // ServicesViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowServices);
            this.Name = "ServicesViewControl";
            this.Size = new System.Drawing.Size(1064, 830);
            this.Load += new System.EventHandler(this.ServicesViewControl_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowServices;
        private System.Windows.Forms.ComboBox cmbConsumers;
        private System.Windows.Forms.Button btnViewOrders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.TextBox txtSearch;
    }
}
