using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace Project
{
    partial class Login
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
            this.Welcome_label = new System.Windows.Forms.Label();
            this.welcome_login_label = new System.Windows.Forms.Label();
            this.Email_label = new System.Windows.Forms.Label();
            this.Password_label = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.email_textBox = new System.Windows.Forms.TextBox();
            this.password_textBox = new System.Windows.Forms.TextBox();
            this.login_button = new System.Windows.Forms.Button();
            this.register_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.frgtPassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Welcome_label
            // 
            this.Welcome_label.AutoSize = true;
            this.Welcome_label.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Welcome_label.Location = new System.Drawing.Point(178, 32);
            this.Welcome_label.Name = "Welcome_label";
            this.Welcome_label.Size = new System.Drawing.Size(158, 37);
            this.Welcome_label.TabIndex = 0;
            this.Welcome_label.Text = "Welcome";
            // 
            // welcome_login_label
            // 
            this.welcome_login_label.AutoSize = true;
            this.welcome_login_label.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcome_login_label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.welcome_login_label.Location = new System.Drawing.Point(164, 93);
            this.welcome_login_label.Name = "welcome_login_label";
            this.welcome_login_label.Size = new System.Drawing.Size(172, 24);
            this.welcome_login_label.TabIndex = 1;
            this.welcome_login_label.Text = "Login to system";
            // 
            // Email_label
            // 
            this.Email_label.AutoSize = true;
            this.Email_label.Location = new System.Drawing.Point(44, 150);
            this.Email_label.Name = "Email_label";
            this.Email_label.Size = new System.Drawing.Size(32, 13);
            this.Email_label.TabIndex = 2;
            this.Email_label.Text = "Email";
            // 
            // Password_label
            // 
            this.Password_label.AutoSize = true;
            this.Password_label.Location = new System.Drawing.Point(47, 201);
            this.Password_label.Name = "Password_label";
            this.Password_label.Size = new System.Drawing.Size(53, 13);
            this.Password_label.TabIndex = 3;
            this.Password_label.Text = "Password";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(304, 224);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(101, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Show password";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // email_textBox
            // 
            this.email_textBox.Location = new System.Drawing.Point(140, 150);
            this.email_textBox.Name = "email_textBox";
            this.email_textBox.Size = new System.Drawing.Size(244, 20);
            this.email_textBox.TabIndex = 5;
            // 
            // password_textBox
            // 
            this.password_textBox.Location = new System.Drawing.Point(140, 198);
            this.password_textBox.Name = "password_textBox";
            this.password_textBox.PasswordChar = '•';
            this.password_textBox.Size = new System.Drawing.Size(244, 20);
            this.password_textBox.TabIndex = 6;
            // 
            // login_button
            // 
            this.login_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.login_button.Location = new System.Drawing.Point(140, 299);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(75, 46);
            this.login_button.TabIndex = 7;
            this.login_button.Text = "Login";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // register_button
            // 
            this.register_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.register_button.Location = new System.Drawing.Point(304, 299);
            this.register_button.Name = "register_button";
            this.register_button.Size = new System.Drawing.Size(75, 46);
            this.register_button.TabIndex = 8;
            this.register_button.Text = "Register";
            this.register_button.UseVisualStyleBackColor = true;
            this.register_button.Click += new System.EventHandler(this.register_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(301, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Don\'t have a user?";
            // 
            // frgtPassword
            // 
            this.frgtPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.frgtPassword.Location = new System.Drawing.Point(157, 377);
            this.frgtPassword.Name = "frgtPassword";
            this.frgtPassword.Size = new System.Drawing.Size(206, 46);
            this.frgtPassword.TabIndex = 10;
            this.frgtPassword.Text = "Forgot Password";
            this.frgtPassword.UseVisualStyleBackColor = true;
            this.frgtPassword.Click += new System.EventHandler(this.button1_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 450);
            this.Controls.Add(this.frgtPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.register_button);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.password_textBox);
            this.Controls.Add(this.email_textBox);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Password_label);
            this.Controls.Add(this.Email_label);
            this.Controls.Add(this.welcome_login_label);
            this.Controls.Add(this.Welcome_label);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Welcome_label;
        private System.Windows.Forms.Label welcome_login_label;
        private System.Windows.Forms.Label Email_label;
        private System.Windows.Forms.Label Password_label;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox email_textBox;
        private System.Windows.Forms.TextBox password_textBox;
        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.Button register_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button frgtPassword;
    }
}

