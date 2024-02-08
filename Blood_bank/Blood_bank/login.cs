using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_bank
{
    public partial class login : Form
    {

        public login()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SUPRADDEP\OneDrive\Documents\Blood.mdf;Integrated Security=True;");
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            // Validate the input for id_no
            if (!int.TryParse(textBox1.Text, out int id))
            {
                MessageBox.Show("Invalid id_no. Please enter a valid integer.");
                return;
            }

            string query = "SELECT * FROM employee WHERE id_no = @id_no AND password = @password";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Add parameters
                cmd.Parameters.AddWithValue("@id_no", id); // Use the parsed id
                cmd.Parameters.AddWithValue("@password", textBox2.Text);

                con.Open();

                // Execute the query
                SqlDataReader reader = cmd.ExecuteReader();

                // Check if any rows were returned
                if (reader.HasRows)
                {
                    home home = new home();
                    home.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid access!!!");
                }

                reader.Close();
                cmd.Dispose();
                con.Close();
            }
        }


        private void button_WOC1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp ob1 = new SignUp();
            ob1.ShowDialog();//
            ob1 = null;
            this.Show();
        }
       
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
