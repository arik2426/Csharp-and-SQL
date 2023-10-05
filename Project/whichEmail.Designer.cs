namespace Project
{
    partial class whichEmail
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
            this.welcome_login_label = new System.Windows.Forms.Label();
            this.email_textBox = new System.Windows.Forms.TextBox();
            this.Email_label = new System.Windows.Forms.Label();
            this.Next_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // welcome_login_label
            // 
            this.welcome_login_label.AutoSize = true;
            this.welcome_login_label.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcome_login_label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.welcome_login_label.Location = new System.Drawing.Point(160, 58);
            this.welcome_login_label.Name = "welcome_login_label";
            this.welcome_login_label.Size = new System.Drawing.Size(253, 24);
            this.welcome_login_label.TabIndex = 2;
            this.welcome_login_label.Text = "Choose Email to restore";
            // 
            // email_textBox
            // 
            this.email_textBox.Location = new System.Drawing.Point(164, 140);
            this.email_textBox.Name = "email_textBox";
            this.email_textBox.Size = new System.Drawing.Size(244, 20);
            this.email_textBox.TabIndex = 7;
            // 
            // Email_label
            // 
            this.Email_label.AutoSize = true;
            this.Email_label.Location = new System.Drawing.Point(69, 143);
            this.Email_label.Name = "Email_label";
            this.Email_label.Size = new System.Drawing.Size(32, 13);
            this.Email_label.TabIndex = 6;
            this.Email_label.Text = "Email";
            // 
            // Next_Button
            // 
            this.Next_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Next_Button.Location = new System.Drawing.Point(218, 260);
            this.Next_Button.Name = "Next_Button";
            this.Next_Button.Size = new System.Drawing.Size(134, 65);
            this.Next_Button.TabIndex = 45;
            this.Next_Button.Text = "Next";
            this.Next_Button.UseVisualStyleBackColor = true;
            this.Next_Button.Click += new System.EventHandler(this.Next_Button_Click);
            // 
            // whichEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 428);
            this.Controls.Add(this.Next_Button);
            this.Controls.Add(this.email_textBox);
            this.Controls.Add(this.Email_label);
            this.Controls.Add(this.welcome_login_label);
            this.Name = "whichEmail";
            this.Text = "Password Input Restore";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label welcome_login_label;
        private System.Windows.Forms.TextBox email_textBox;
        private System.Windows.Forms.Label Email_label;
        private System.Windows.Forms.Button Next_Button;
    }
}