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
    public partial class users : Form
    {
        public users()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SUPRADDEP\OneDrive\Documents\Blood.mdf;Integrated Security=True;");

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Check if the entered value is a valid 10-digit decimal
                decimal phoneNumber = 0;
                if (textBox1.Text.Length != 10 || !decimal.TryParse(textBox1.Text, out phoneNumber))
                {

                    return; // Exit the method early
                }

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [user] WHERE pho_no = @pho_no", con))
                {
                    cmd.Parameters.AddWithValue("@pho_no", phoneNumber);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            textBox2.Text = reader["name"].ToString();
                            comboBox1.Text = reader["blood_group"].ToString();
                            richTextBox1.Text = reader["address"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 10 && textBox2.Text != null && richTextBox1.Text != null && comboBox1.Text != null)
            {
                try
                {
                   
                        
                        // Assuming textBox2 contains the phone number as decimal
                        if (textBox1.Text.Length == 10 && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(richTextBox1.Text) && !string.IsNullOrEmpty(comboBox1.Text))
                        {
                            try
                            {
                                string query = "UPDATE [user] SET name = @name, blood_group = @blood_group, address = @address WHERE pho_no = @pho_no";

                                using (SqlCommand cmd = new SqlCommand(query, con))
                                {
                                // Assuming textBox2 contains the phone number as an integer
                                decimal phoneNumber=decimal.Parse(textBox1.Text);
                                        cmd.Parameters.AddWithValue("@name", textBox1.Text);
                                        cmd.Parameters.AddWithValue("@blood_group", comboBox1.Text);
                                        cmd.Parameters.AddWithValue("@address", richTextBox1.Text);
                                        cmd.Parameters.AddWithValue("@pho_no", phoneNumber);

                                        con.Open();
                                        int rowsAffected = cmd.ExecuteNonQuery();
                                        con.Close();

                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("Update successful");
                                        }
                                        else
                                        {
                                            MessageBox.Show("No records were updated. Make sure the phone number exists.");
                                        }
                                   
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error updating record: " + ex.Message);
                            }
                        }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating record: " + ex.Message);
                }
            }

        }
    }
}
