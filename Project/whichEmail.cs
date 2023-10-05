using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Project
{
    public partial class whichEmail : Form
    {
        SqlConnection con; // declare connection
        private string email = "";
        public whichEmail()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLGuest"].ConnectionString);
        }

        private void Next_Button_Click(object sender, EventArgs e)
        {

            //check mail is in right format
            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match email_match = emailRegex.Match(email_textBox.Text);
            if (!email_match.Success)
            {
                MessageBox.Show("email is in wrong format - example@example.com");
                return;
            }
            con.Open();
            // Check if the user exists
            SqlParameter email = new SqlParameter("@email", SqlDbType.VarChar, 256);
            email.Value = email_textBox.Text;
            SqlCommand cmd3 = new SqlCommand("UserExistsProc", con);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add(email);
            cmd3.Prepare();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd3);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0) // Check if user exists
            {
                this.email = email_textBox.Text;
                using (PasswordRestore form = new PasswordRestore(email_textBox.Text))
                {
                    this.Close();
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("email is not in the system!");
                email.Value = "";
            }

            con.Close();
        }
    }
}
