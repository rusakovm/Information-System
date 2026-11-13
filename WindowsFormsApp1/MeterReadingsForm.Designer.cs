namespace WindowsFormsApp1
{
    partial class MeterReadingsForm
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
            this.flowReadings = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddReading = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowReadings
            // 
            this.flowReadings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowReadings.AutoScroll = true;
            this.flowReadings.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowReadings.Location = new System.Drawing.Point(64, 66);
            this.flowReadings.Name = "flowReadings";
            this.flowReadings.Size = new System.Drawing.Size(679, 305);
            this.flowReadings.TabIndex = 0;
            this.flowReadings.WrapContents = false;
            this.flowReadings.Paint += new System.Windows.Forms.PaintEventHandler(this.flowReadings_Paint);
            // 
            // btnAddReading
            // 
            this.btnAddReading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddReading.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnAddReading.Location = new System.Drawing.Point(64, 398);
            this.btnAddReading.Name = "btnAddReading";
            this.btnAddReading.Size = new System.Drawing.Size(180, 40);
            this.btnAddReading.TabIndex = 1;
            this.btnAddReading.Text = "Добавить";
            this.btnAddReading.UseVisualStyleBackColor = true;
            this.btnAddReading.Click += new System.EventHandler(this.btnAddReading_Click);
            // 
            // MeterReadingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAddReading);
            this.Controls.Add(this.flowReadings);
            this.Name = "MeterReadingsForm";
            this.Text = "MeterReadingsForm";
            this.Load += new System.EventHandler(this.MeterReadingsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowReadings;
        private System.Windows.Forms.Button btnAddReading;
    }
}