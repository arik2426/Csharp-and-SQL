using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Project
{
    public partial class PasswordRestore : Form
    {
        SqlConnection con;
        private string email = "";
        DataTable tbl;

        public PasswordRestore(string email)
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLGuest"].ConnectionString);
            this.email = email;
            tbl = new DataTable();

            // Load the user questions
            DataTable chosenQ = RollQuestions();
            tbl = chosenQ.Copy();
            if (chosenQ.Rows.Count >= 3)
            {
                Q1.Text = chosenQ.Rows[0]["Question"].ToString();
                Q2.Text = chosenQ.Rows[1]["Question"].ToString();
                Q3.Text = chosenQ.Rows[2]["Question"].ToString();

                // Store the DataTable for later use
               
            }
        }
        private DataTable RollQuestions()
        {
            // get a data table of the security questions
            //command + parameter
            SqlCommand cmd = new SqlCommand("QNAProc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter email = new SqlParameter("@email", SqlDbType.VarChar, 256);
            email.Value = this.email;
            cmd.Parameters.Add(email);
            // adapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            // choose 3 random questions
            Random r = new Random();
            var rowsTaken = new HashSet<int>();
            DataTable rndTable = dt.Clone();
            for (int i = 0; i < 3; i++)
            {
                int rndRowIndex = r.Next(dt.Rows.Count);
                while (!rowsTaken.Add(rndRowIndex))
                {
                    rndRowIndex = r.Next(dt.Rows.Count);
                }
                DataRow randomRow = dt.Rows[rndRowIndex];
                rndTable.ImportRow(randomRow);
            }
           
            return rndTable;
        }
   
        private void Restore_button_Click(object sender, EventArgs e)
        {

            con.Open();
            //group text boxes
            //group questions labels
            var Qbox = new List<TextBox> { A1, A2, A3 };
            // check if answers are correct
            bool errorFlag = false;
            int i = 0;

            foreach (DataRow dr in tbl.Rows)
            {
                if (this.email == "")
                {
                    errorFlag = true;
                    //break; // we chose to not use the break in order to protect from timing attacks
                }
                else if (!Qbox[i % Qbox.Count].Text.Equals(dr.ItemArray[2]))
                {
                    errorFlag = true; //go over all questions for security reasons
                }

                i++;
            }
            // if any of the answers were wrong
            if (errorFlag)
            {
                MessageBox.Show("Incorrect answers provided. Please try again.");
            }
            else
            {
                using (setPassword form = new setPassword(this.email))
                {
                    this.Close();
                    form.ShowDialog(); ;
                    this.Show();
                    
                }
                con.Close();
            }
        }
    }
}

