using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_bank
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            users users = new users();
            users.TopLevel = false;
            panel3.Controls.Add(users);
            users.BringToFront();
            users.Show();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SUPRADDEP\OneDrive\Documents\Blood.mdf;Integrated Security=True;");


        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from stock", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from otherbloodbanks", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                // Assuming the actual column names are "BloodGroup" and "Quantity"
                string bloodGroup = dataGridView1.Rows[i].Cells["blood_group"].Value.ToString();
                int quantitys = Convert.ToInt32(dataGridView1.Rows[i].Cells["quantity"].Value);

                // Build the update query
                query = $"UPDATE Stock SET quantity = {quantitys} WHERE blood_group = '{bloodGroup}'";

                // Execute the update query
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();

        }
    }
}
