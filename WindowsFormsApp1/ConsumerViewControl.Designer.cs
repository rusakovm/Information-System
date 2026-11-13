namespace WindowsFormsApp1
{
    partial class ConsumerViewControl
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
            this.flowConsumers = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddConsumer = new System.Windows.Forms.Button();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowConsumers
            // 
            this.flowConsumers.AutoScroll = true;
            this.flowConsumers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowConsumers.Location = new System.Drawing.Point(0, 133);
            this.flowConsumers.Name = "flowConsumers";
            this.flowConsumers.Size = new System.Drawing.Size(1064, 615);
            this.flowConsumers.TabIndex = 0;
            this.flowConsumers.WrapContents = false;
            this.flowConsumers.Paint += new System.Windows.Forms.PaintEventHandler(this.flowConsumers_Paint);
            // 
            // btnAddConsumer
            // 
            this.btnAddConsumer.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnAddConsumer.Location = new System.Drawing.Point(35, 28);
            this.btnAddConsumer.Name = "btnAddConsumer";
            this.btnAddConsumer.Size = new System.Drawing.Size(280, 40);
            this.btnAddConsumer.TabIndex = 0;
            this.btnAddConsumer.Text = "Добавить потребителя";
            this.btnAddConsumer.UseVisualStyleBackColor = true;
            this.btnAddConsumer.Click += new System.EventHandler(this.btnAddConsumer_Click);
            // 
            // cmbSort
            // 
            this.cmbSort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbSort.Font = new System.Drawing.Font("Verdana", 12F);
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(554, 28);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(479, 33);
            this.cmbSort.TabIndex = 5;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnAddConsumer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbSort);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 127);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Verdana", 12F);
            this.txtSearch.Location = new System.Drawing.Point(108, 83);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(406, 32);
            this.txtSearch.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F);
            this.label3.Location = new System.Drawing.Point(30, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Поиск";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F);
            this.label2.Location = new System.Drawing.Point(415, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Сортировка";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ConsumerViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowConsumers);
            this.Name = "ConsumerViewControl";
            this.Size = new System.Drawing.Size(1064, 830);
            this.Load += new System.EventHandler(this.ConsumerViewControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowConsumers;
        private System.Windows.Forms.Button btnAddConsumer;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label3;
    }
}
