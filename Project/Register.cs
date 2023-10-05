using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Project
{
    public partial class Register : Form
    {
        SqlConnection con;
        string connectionString = ConfigurationManager.ConnectionStrings["SQLGuest"].ConnectionString;

        public Register()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            DataTable rndTable = RollQuestions();
            Q1.Text = rndTable.Rows[0].Field<string>(1);
            Q2.Text = rndTable.Rows[1].Field<string>(1);
            Q3.Text = rndTable.Rows[2].Field<string>(1);
            Q4.Text = rndTable.Rows[3].Field<string>(1);
            Q5.Text = rndTable.Rows[4].Field<string>(1);
        }

        private DataTable RollQuestions()
        {
            // get a data table of the security questions
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("GetQuestionsProc", con);
            cmd.CommandType = CommandType.StoredProcedure; // command type
            cmd.Prepare();
            adapter.SelectCommand = cmd; // link command to adapter
            adapter.Fill(dt);

            // choose 5 random questions
            Random r = new Random();
            var rowsTaken = new HashSet<int>();
            DataTable rndTable = dt.Clone();
            for (int i = 0; i < 5; i++)
            {
                int rndRowIndex = r.Next(dt.Rows.Count);
                while (!rowsTaken.Add(rndRowIndex))
                {
                    rndRowIndex = r.Next(dt.Rows.Count);
                }
                DataRow randomRow = dt.Rows[rndRowIndex];
                rndTable.ImportRow(randomRow);

            }
            return rndTable; // Return the list of updated labels
        }
        private byte GetQuestionId(string questionText)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Q_ID FROM RestoreQuestions WHERE Question = @questionText", con);
                cmd.Parameters.AddWithValue("@questionText", questionText);
                return (byte)cmd.ExecuteScalar();
            }
        }
        private void register_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(email_textBox.Text) || string.IsNullOrWhiteSpace(passwordLabel.Text) || string.IsNullOrWhiteSpace(firstName.Text) || string.IsNullOrWhiteSpace(lastName.Text) || string.IsNullOrWhiteSpace(A1.Text) || string.IsNullOrWhiteSpace(A2.Text) || string.IsNullOrWhiteSpace(A3.Text) || string.IsNullOrWhiteSpace(A4.Text) || string.IsNullOrWhiteSpace(A5.Text))
            {
                MessageBox.Show("One of your textfields is empty");
            }

            else
            {
                con.Open();
                //check mail is in right format
                Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match email_match = emailRegex.Match(email_textBox.Text);
                if (!email_match.Success)
                {
                    MessageBox.Show("email is in wrong format - example@example.com");
                    con.Close();
                    return;
                }
                else
                {
                    SqlParameter email = new SqlParameter("@email", SqlDbType.VarChar, 256);
                    email.Value = email_textBox.Text;
                    SqlCommand chker = new SqlCommand("UserExistsProc", con);
                    chker.CommandType = CommandType.StoredProcedure;
                    chker.Parameters.Add(email);
                    chker.Prepare();
                    //initialize an adapter with above commands
                    SqlDataAdapter adapter = new SqlDataAdapter(chker);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0) //check if user exists in the data base
                    {
                        MessageBox.Show("email already exists in the system!");
                        con.Close();
                        return;
                    }
                    chker.Parameters.Remove(email);
                    SqlCommand cmd = new SqlCommand("UsersTableChangeProc", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //parameters
                    SqlParameter psw = new SqlParameter("@psw", SqlDbType.NVarChar, 256);
                    SqlParameter fName = new SqlParameter("@f_name", SqlDbType.VarChar, 20);
                    SqlParameter lname = new SqlParameter("@l_name", SqlDbType.VarChar, 20);
                    SqlParameter dob = new SqlParameter("@dob", SqlDbType.Date);
                    SqlParameter actType = new SqlParameter("@actionType", SqlDbType.NText, 20);
                    //seting values    
                    psw.Value = passwordLabel.Text;
                    fName.Value = firstName.Text;
                    lname.Value = lastName.Text;
                    dob.Value = dateTimePicker_label.Value;
                    actType.Value = "Insert";
                    // add parameters
                    cmd.Parameters.Add(email);
                    cmd.Parameters.Add(psw);
                    cmd.Parameters.Add(fName);
                    cmd.Parameters.Add(lname);
                    cmd.Parameters.Add(dob);
                    cmd.Parameters.Add(actType);
                    //prepare and execute
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    // link questions to user
                    SqlCommand cmd2 = new SqlCommand("PasswordRestoreTableInsertProc", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    //parameters
                    SqlParameter QID = new SqlParameter("@qid", SqlDbType.TinyInt);
                    SqlParameter answer = new SqlParameter("@ans", SqlDbType.Text, 256);
                    // remove parameter from previous command
                    cmd.Parameters.Remove(email);
                    // add parameters
                    cmd2.Parameters.Add(email);
                    cmd2.Parameters.Add(QID);
                    cmd2.Parameters.Add(answer);
                    // values + execution
                    //Q1 + A1
                    byte questionId1 = GetQuestionId(Q1.Text);
                    QID.Value = questionId1;
                    answer.Value = A1.Text;
                    cmd2.Prepare();
                    cmd2.ExecuteNonQuery();
                    //Q2 + A2
                    byte questionId2 = GetQuestionId(Q2.Text);
                    QID.Value = questionId2;
                    answer.Value = A2.Text;
                    cmd2.Prepare();
                    cmd2.ExecuteNonQuery();
                    //Q3 + A3
                    byte questionId3 = GetQuestionId(Q3.Text);
                    QID.Value = questionId3;
                    answer.Value = A3.Text;
                    cmd2.Prepare();
                    cmd2.ExecuteNonQuery();
                    //Q4 + A4
                    byte questionId4 = GetQuestionId(Q4.Text);
                    QID.Value = questionId4;
                    answer.Value = A4.Text;
                    cmd2.Prepare();
                    cmd2.ExecuteNonQuery();
                    //Q5 + A5
                    byte questionId5 = GetQuestionId(Q5.Text);
                    QID.Value = questionId5;
                    answer.Value = A5.Text;
                    cmd2.Prepare();
                    cmd2.ExecuteNonQuery();

                    con.Close();
                    this.Close();
                    MessageBox.Show("User Created");
                }
            }
        }
    }
}
    

