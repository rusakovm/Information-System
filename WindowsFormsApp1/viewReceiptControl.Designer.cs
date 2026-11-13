namespace WindowsFormsApp1
{
    partial class viewReceiptControl
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
            this.cmbConsumer = new System.Windows.Forms.ComboBox();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnSavePdf = new System.Windows.Forms.Button();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbConsumer
            // 
            this.cmbConsumer.Font = new System.Drawing.Font("Verdana", 12F);
            this.cmbConsumer.FormattingEnabled = true;
            this.cmbConsumer.Location = new System.Drawing.Point(62, 83);
            this.cmbConsumer.Name = "cmbConsumer";
            this.cmbConsumer.Size = new System.Drawing.Size(368, 33);
            this.cmbConsumer.TabIndex = 0;
            // 
            // dtFrom
            // 
            this.dtFrom.Font = new System.Drawing.Font("Verdana", 12F);
            this.dtFrom.Location = new System.Drawing.Point(191, 132);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(261, 32);
            this.dtFrom.TabIndex = 1;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnCalculate.Location = new System.Drawing.Point(62, 202);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(240, 40);
            this.btnCalculate.TabIndex = 2;
            this.btnCalculate.Text = "Получить данные";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Verdana", 12F);
            this.lblInfo.Location = new System.Drawing.Point(57, 256);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 25);
            this.lblInfo.TabIndex = 3;
            // 
            // btnSavePdf
            // 
            this.btnSavePdf.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnSavePdf.Location = new System.Drawing.Point(62, 443);
            this.btnSavePdf.Name = "btnSavePdf";
            this.btnSavePdf.Size = new System.Drawing.Size(340, 40);
            this.btnSavePdf.TabIndex = 4;
            this.btnSavePdf.Text = "Сформировать квитанцию";
            this.btnSavePdf.UseVisualStyleBackColor = true;
            this.btnSavePdf.Click += new System.EventHandler(this.btnSavePdf_Click);
            // 
            // dtTo
            // 
            this.dtTo.Font = new System.Drawing.Font("Verdana", 12F);
            this.dtTo.Location = new System.Drawing.Point(513, 132);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(261, 32);
            this.dtTo.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F);
            this.label1.Location = new System.Drawing.Point(57, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Выберите потребителя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F);
            this.label2.Location = new System.Drawing.Point(57, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Период с";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F);
            this.label3.Location = new System.Drawing.Point(470, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "по";
            // 
            // viewReceiptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.btnSavePdf);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.cmbConsumer);
            this.Name = "viewReceiptControl";
            this.Size = new System.Drawing.Size(1064, 510);
            this.Load += new System.EventHandler(this.viewReceiptControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbConsumer;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnSavePdf;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
