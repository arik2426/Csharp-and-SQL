using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Project
{
    public partial class SuccessfulLogin : Form
    {
        SqlConnection conn;
        string email;
        string sql = ConfigurationManager.ConnectionStrings["SQLUser"].ConnectionString;

        public SuccessfulLogin(string email)
        {
            InitializeComponent();
            conn = new SqlConnection(sql);
            this.email = email;
            if (isAdmin())
            {
                this.label1.Visible = true;
                this.button2.Visible = true;
                this.button3.Visible = true;
            }
            else
            {
                this.label1.Visible = false;
                this.button2.Visible = false;
                this.button3.Visible = false;
            }
        }

        private bool isAdmin()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            //prepare the command
            SqlCommand cmd = new SqlCommand("UserRoleProc", conn);
            cmd.CommandType = CommandType.StoredProcedure; // command type
            SqlParameter email = new SqlParameter("@email", SqlDbType.Text, 256);
            email.Value = this.email;
            cmd.Parameters.Add(email);
            cmd.Prepare();

            adapter.SelectCommand = cmd; // link command to adapter
            adapter.Fill(dt); // fill data table
            if (dt.Rows.Count == 1) // check if the user exists
            {
                DataRow row = dt.Rows[0];
                if (row.ItemArray[1].ToString() == "2")
                {
                    conn.Close();
                    sql = ConfigurationManager.ConnectionStrings["SQLAdmin"].ConnectionString;
                    conn = new SqlConnection(sql);
                    return true;
                }
            }
            conn.Close();
            return false;
        }

        private void sizeDGV(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.Vertical;
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            dgv.Width = totalWidth;
        }

        private void ViewAttemptsButton_Click(object sender, EventArgs e)
        {
            conn.Open();
            //declare necessary components
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            //prepare the command
            SqlCommand cmd = new SqlCommand("GetLoginHistoryProc", conn);
            cmd.CommandType = CommandType.StoredProcedure; // command type
            SqlParameter email = new SqlParameter("@email", SqlDbType.Text, 256);
            email.Value = this.email;
            cmd.Parameters.Add(email);
            cmd.Prepare();

            adapter.SelectCommand = cmd; // link command to adapter
            adapter.Fill(dt); // fill data table
            dataGridView1.DataSource = dt; // fill table for user
            sizeDGV(dataGridView1);
            dataGridView1.Visible = true;

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            //declare necessary components
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            //prepare the command
            SqlCommand cmd = new SqlCommand("GetLoginHistoryProc", conn);
            cmd.CommandType = CommandType.StoredProcedure; // command type
            SqlParameter email = new SqlParameter("@email", SqlDbType.Text, 256);
            email.Value = this.email;
            cmd.Parameters.Add(email);
            cmd.Prepare();

            adapter.SelectCommand = cmd; // link command to adapter
            adapter.Fill(dt); // fill data table
            dataGridView1.DataSource = dt; // fill table for user
            sizeDGV(dataGridView1);
            dataGridView1.Visible = true;

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            //declare necessary components
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            //prepare the command
            SqlCommand cmd = new SqlCommand("SELECT * FROM BlockedUsers", conn);
            cmd.CommandType = CommandType.Text; // command type

            cmd.Prepare();

            adapter.SelectCommand = cmd; // link command to adapter
            adapter.Fill(dt); // fill data table
            dataGridView1.DataSource = dt; // fill table for user
            sizeDGV(dataGridView1);
            dataGridView1.Visible = true;

            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            //declare necessary components
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            //prepare the command
            SqlCommand cmd = new SqlCommand("SELECT * FROM LastSuccessfulUserStatus", conn);
            cmd.CommandType = CommandType.Text; // command type

            cmd.Prepare();

            adapter.SelectCommand = cmd; // link command to adapter
            adapter.Fill(dt); // fill data table
            dataGridView1.DataSource = dt; // fill table for user
            sizeDGV(dataGridView1);
            dataGridView1.Visible = true;

            conn.Close();
        }
    }
}
