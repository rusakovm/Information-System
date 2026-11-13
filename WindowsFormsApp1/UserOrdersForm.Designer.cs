namespace WindowsFormsApp1
{
    partial class UserOrdersForm
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
            this.lblUserName = new System.Windows.Forms.Label();
            this.flowOrders = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.SystemColors.Control;
            this.lblUserName.Font = new System.Drawing.Font("Verdana", 12F);
            this.lblUserName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUserName.Location = new System.Drawing.Point(31, 26);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(73, 25);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "label1";
            // 
            // flowOrders
            // 
            this.flowOrders.AutoScroll = true;
            this.flowOrders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowOrders.Location = new System.Drawing.Point(2, 73);
            this.flowOrders.Name = "flowOrders";
            this.flowOrders.Size = new System.Drawing.Size(968, 507);
            this.flowOrders.TabIndex = 3;
            this.flowOrders.WrapContents = false;
            this.flowOrders.Paint += new System.Windows.Forms.PaintEventHandler(this.flowOrders_Paint);
            // 
            // UserOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.flowOrders);
            this.Controls.Add(this.lblUserName);
            this.Name = "UserOrdersForm";
            this.Text = "UserOrdersForm";
            this.Load += new System.EventHandler(this.UserOrdersForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.FlowLayoutPanel flowOrders;
    }
}