namespace WindowsFormsApp1
{
    partial class MeterCardControl
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
            this.lblSerial = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblActive = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReadings = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Verdana", 12F);
            this.lblSerial.Location = new System.Drawing.Point(28, 44);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(73, 25);
            this.lblSerial.TabIndex = 0;
            this.lblSerial.Text = "label1";
            this.lblSerial.Click += new System.EventHandler(this.lblSerial_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Verdana", 12F);
            this.lblDate.Location = new System.Drawing.Point(28, 84);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(73, 25);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "label2";
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Font = new System.Drawing.Font("Verdana", 12F);
            this.lblActive.Location = new System.Drawing.Point(28, 124);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(73, 25);
            this.lblActive.TabIndex = 2;
            this.lblActive.Text = "label3";
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnEdit.Location = new System.Drawing.Point(3, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(64, 32);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Tag = "METERS_EDIT";
            this.btnEdit.Text = "✏️";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnDelete.Location = new System.Drawing.Point(3, 41);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 32);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Tag = "METERS_DELETE";
            this.btnDelete.Text = "\t🗑";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnReadings
            // 
            this.btnReadings.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnReadings.Location = new System.Drawing.Point(3, 79);
            this.btnReadings.Name = "btnReadings";
            this.btnReadings.Size = new System.Drawing.Size(64, 32);
            this.btnReadings.TabIndex = 5;
            this.btnReadings.Tag = "READINGS_ADD";
            this.btnReadings.Text = "📈";
            this.btnReadings.UseVisualStyleBackColor = true;
            this.btnReadings.Click += new System.EventHandler(this.btnReadings_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnReadings);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(785, 44);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(87, 116);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // MeterCardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(194)))));
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblActive);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblSerial);
            this.Name = "MeterCardControl";
            this.Size = new System.Drawing.Size(900, 208);
            this.Load += new System.EventHandler(this.MeterCardControl_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReadings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
