using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void login_button_Click(object sender, EventArgs e)
        {
            //if ConfigurationManager is red. go to 'Project' --> Add Reference --> System.Configuration
            string connectionString = ConfigurationManager.ConnectionStrings["SQLGuest"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //check text boxes - does not count as an attempt to log in
            if (string.IsNullOrWhiteSpace(email_textBox.Text) ||
                string.IsNullOrWhiteSpace(password_textBox.Text))
            {
                MessageBox.Show("Enter email and password");
            }
            else
            {
                
                con.Open();
                SqlCommand sc = new SqlCommand("UserLoginProc", con);
                sc.CommandType = CommandType.StoredProcedure; // command type
                // procedure parameters
                SqlParameter email = new SqlParameter("@email", SqlDbType.Text, 256);
                SqlParameter psw = new SqlParameter("@psw", SqlDbType.NText, 256);
                // get values
                email.Value = email_textBox.Text;
                psw.Value = password_textBox.Text;
                // add the parameters to the procedure
                sc.Parameters.Add(email);
                sc.Parameters.Add(psw);
                // prepare and execute
                sc.Prepare();
                sc.ExecuteNonQuery();
                // check if procedure went well
                sc.CommandText = "LogInStatusProc"; // changed the proceudure to execute
                sc.Parameters.Remove(psw); // remove unnecessary parameter 
                // add a returned value from procedure
                SqlParameter result = sc.Parameters.Add("@res", SqlDbType.Int);
                result.Direction = ParameterDirection.ReturnValue;
                // prepare and execute procedure
                sc.Prepare();
                sc.ExecuteNonQuery();
                //if procedure succeded then user is succefully logged in - else user is gets an error message
                if ((int)result.Value > 0)
                {
                    using (SuccessfulLogin form = new SuccessfulLogin(email_textBox.Text))
                    {
                        this.Hide();
                        email_textBox.Clear();
                        password_textBox.Clear();
                        form.ShowDialog(); ;
                        this.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid email and/or password");
                    email_textBox.Clear();
                    password_textBox.Clear();
                }
                con.Close();
            }
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            new Register().Show();  //Opens register Form
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                password_textBox.PasswordChar = '\0';
            }
            else
            {
                password_textBox.PasswordChar ='•';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new whichEmail().Show();            
        }
    }
}
