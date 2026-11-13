namespace WindowsFormsApp1
{
    partial class RolePermissionsForm
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
            this.cmbRoles = new System.Windows.Forms.ComboBox();
            this.btnAddRole = new System.Windows.Forms.Button();
            this.flowPerms = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDeleteRole = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbRoles
            // 
            this.cmbRoles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbRoles.Font = new System.Drawing.Font("Verdana", 12F);
            this.cmbRoles.FormattingEnabled = true;
            this.cmbRoles.Location = new System.Drawing.Point(41, 46);
            this.cmbRoles.Name = "cmbRoles";
            this.cmbRoles.Size = new System.Drawing.Size(299, 33);
            this.cmbRoles.TabIndex = 0;
            this.cmbRoles.SelectedIndexChanged += new System.EventHandler(this.cmbRoles_SelectedIndexChanged);
            // 
            // btnAddRole
            // 
            this.btnAddRole.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddRole.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnAddRole.Location = new System.Drawing.Point(457, 46);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(160, 40);
            this.btnAddRole.TabIndex = 1;
            this.btnAddRole.Text = "Добавить должность";
            this.btnAddRole.UseVisualStyleBackColor = true;
            this.btnAddRole.Click += new System.EventHandler(this.btnAddRole_Click);
            // 
            // flowPerms
            // 
            this.flowPerms.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowPerms.AutoScroll = true;
            this.flowPerms.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPerms.Location = new System.Drawing.Point(41, 103);
            this.flowPerms.Name = "flowPerms";
            this.flowPerms.Size = new System.Drawing.Size(576, 295);
            this.flowPerms.TabIndex = 2;
            // 
            // btnDeleteRole
            // 
            this.btnDeleteRole.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDeleteRole.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnDeleteRole.Location = new System.Drawing.Point(41, 416);
            this.btnDeleteRole.Name = "btnDeleteRole";
            this.btnDeleteRole.Size = new System.Drawing.Size(160, 40);
            this.btnDeleteRole.TabIndex = 3;
            this.btnDeleteRole.Text = "Удалить";
            this.btnDeleteRole.UseVisualStyleBackColor = true;
            this.btnDeleteRole.Click += new System.EventHandler(this.btnDeleteRole_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnApply.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnApply.Location = new System.Drawing.Point(457, 416);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(160, 40);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // RolePermissionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 487);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnDeleteRole);
            this.Controls.Add(this.flowPerms);
            this.Controls.Add(this.btnAddRole);
            this.Controls.Add(this.cmbRoles);
            this.Name = "RolePermissionsForm";
            this.Text = "RolePermissionsForm";
            this.Load += new System.EventHandler(this.RolePermissionsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRoles;
        private System.Windows.Forms.Button btnAddRole;
        private System.Windows.Forms.FlowLayoutPanel flowPerms;
        private System.Windows.Forms.Button btnDeleteRole;
        private System.Windows.Forms.Button btnApply;
    }
}