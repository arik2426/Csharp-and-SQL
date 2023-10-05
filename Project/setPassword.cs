using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Project
{
    public partial class setPassword : Form
    {
        string email;
        SqlConnection con;
        public setPassword(string email)
        {
            InitializeComponent();
            this.email = email;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLUser"].ConnectionString);
        }

        private void Restore_button_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(newPassword.Text))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UsersTableChangeProc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //parameters
                SqlParameter email = new SqlParameter("@email", SqlDbType.VarChar, 256);
                SqlParameter psw = new SqlParameter("@psw", SqlDbType.NVarChar, 256);
                SqlParameter actType = new SqlParameter("@actionType", SqlDbType.NVarChar, 20);
                cmd.Parameters.Add(email);
                cmd.Parameters.Add(psw);
                cmd.Parameters.Add(actType);
                email.Value = this.email;
                psw.Value = newPassword.Text;
                actType.Value = "NewPass";
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                con.Close();
                this.DialogResult = DialogResult.OK;
                this.Close();
                MessageBox.Show("Password Changed Successfully!");
            }
            else
            {
                MessageBox.Show("invalid password");
            }
        }

    }
}

