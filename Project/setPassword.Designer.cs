namespace Project
{
    partial class setPassword
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
            this.newPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.welcome_login_label = new System.Windows.Forms.Label();
            this.Restore_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newPassword
            // 
            this.newPassword.Location = new System.Drawing.Point(155, 201);
            this.newPassword.Name = "newPassword";
            this.newPassword.Size = new System.Drawing.Size(328, 20);
            this.newPassword.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "New Password";
            // 
            // welcome_login_label
            // 
            this.welcome_login_label.AutoSize = true;
            this.welcome_login_label.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcome_login_label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.welcome_login_label.Location = new System.Drawing.Point(151, 60);
            this.welcome_login_label.Name = "welcome_login_label";
            this.welcome_login_label.Size = new System.Drawing.Size(244, 24);
            this.welcome_login_label.TabIndex = 46;
            this.welcome_login_label.Text = "Set your new password";
            // 
            // Restore_button
            // 
            this.Restore_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Restore_button.Location = new System.Drawing.Point(208, 296);
            this.Restore_button.Name = "Restore_button";
            this.Restore_button.Size = new System.Drawing.Size(134, 65);
            this.Restore_button.TabIndex = 47;
            this.Restore_button.Text = "Restore";
            this.Restore_button.UseVisualStyleBackColor = true;
            this.Restore_button.Click += new System.EventHandler(this.Restore_button_Click);
            // 
            // setPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 419);
            this.Controls.Add(this.Restore_button);
            this.Controls.Add(this.welcome_login_label);
            this.Controls.Add(this.newPassword);
            this.Controls.Add(this.label2);
            this.Name = "setPassword";
            this.Text = "Set Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox newPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label welcome_login_label;
        private System.Windows.Forms.Button Restore_button;
    }
}