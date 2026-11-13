namespace WindowsFormsApp1
{
    partial class SchedulerForm
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
            this.cmbEmployees = new System.Windows.Forms.ComboBox();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.dtpWeek = new System.Windows.Forms.DateTimePicker();
            this.btnApply = new System.Windows.Forms.Button();
            this.schedule = new WindowsFormsApp1.WeekScheduleControl();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbEmployees
            // 
            this.cmbEmployees.Font = new System.Drawing.Font("Verdana", 12F);
            this.cmbEmployees.FormattingEnabled = true;
            this.cmbEmployees.Location = new System.Drawing.Point(12, 38);
            this.cmbEmployees.Name = "cmbEmployees";
            this.cmbEmployees.Size = new System.Drawing.Size(357, 33);
            this.cmbEmployees.TabIndex = 0;
            this.cmbEmployees.SelectedIndexChanged += new System.EventHandler(this.cmbEmployees_SelectedIndexChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Verdana", 16F);
            this.btnPrev.Location = new System.Drawing.Point(20, 613);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(240, 40);
            this.btnPrev.TabIndex = 1;
            this.btnPrev.Text = "←";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Verdana", 16F);
            this.btnNext.Location = new System.Drawing.Point(834, 615);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(240, 40);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "→";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // dtpWeek
            // 
            this.dtpWeek.CustomFormat = "MMMM yyyy";
            this.dtpWeek.Font = new System.Drawing.Font("Verdana", 12F);
            this.dtpWeek.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWeek.Location = new System.Drawing.Point(386, 39);
            this.dtpWeek.Name = "dtpWeek";
            this.dtpWeek.Size = new System.Drawing.Size(200, 32);
            this.dtpWeek.TabIndex = 3;
            this.dtpWeek.ValueChanged += new System.EventHandler(this.dtpWeek_ValueChanged);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnApply.Location = new System.Drawing.Point(321, 615);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(265, 40);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Применить изменения";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // schedule
            // 
            this.schedule.AutoScroll = true;
            this.schedule.AutoScrollMinSize = new System.Drawing.Size(1030, 960);
            this.schedule.EmployeeId = 0;
            this.schedule.Location = new System.Drawing.Point(12, 84);
            this.schedule.Name = "schedule";
            this.schedule.Size = new System.Drawing.Size(1062, 520);
            this.schedule.TabIndex = 4;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnDelete.Location = new System.Drawing.Point(635, 35);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(160, 40);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // SchedulerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 754);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.schedule);
            this.Controls.Add(this.dtpWeek);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.cmbEmployees);
            this.Name = "SchedulerForm";
            this.Text = "SchedulerForm";
            this.Load += new System.EventHandler(this.SchedulerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEmployees;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.DateTimePicker dtpWeek;
        private WeekScheduleControl schedule;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnDelete;
    }
}