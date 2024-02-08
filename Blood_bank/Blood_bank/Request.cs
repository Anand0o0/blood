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
    public partial class Request : Form
    {
        public Request()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SUPRADDEP\OneDrive\Documents\Blood.mdf;Integrated Security=True;");

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            if (textBox2.Text.Length == 10)
            {
                decimal phoneNumber;
                if (decimal.TryParse(textBox2.Text, out phoneNumber))
                {
                    string query = "SELECT * FROM [user] WHERE pho_no = @pho_no";

                    
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@pho_no", phoneNumber);

                            try
                            {
                                con.Open();

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        // Move to the first row (assuming id_no is unique)
                                        reader.Read();

                                        // Example: Auto-fill other textboxes based on the database values
                                        if (reader["name"] != DBNull.Value)
                                        {
                                            textBox1.Text = reader["name"].ToString();
                                        }

                                        if (reader["address"] != DBNull.Value)
                                        {
                                            richTextBox1.Text = reader["address"].ToString();
                                        }
                                        
                                    }
                                }
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        }
                    
                }
               
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlDataAdapter adapter = new SqlDataAdapter("select * from stock",con);
            con.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(comboBox1.Text))
                {
                    string query = "SELECT quantity FROM Stock WHERE blood_group = @blood_group";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@blood_group", comboBox1.Text);

                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int a = int.Parse(textBox4.Text);
                                int b = (int)reader["quantity"];

                                if (a > b)
                                {
                                    MessageBox.Show($"Currently, we do not have {a} units of stock. The present available stock is {b}.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("No data found for the selected blood group.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a blood group before checking the stock.");
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


        private void button_WOC1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox4.Text, out int result))
            {
                if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(comboBox1.Text))
                {
                    int quantityToSubtract = int.Parse(textBox4.Text);

                    string query = "UPDATE Stock SET quantity = (quantity - @quantity) WHERE blood_group = @bloodGroup";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@quantity", quantityToSubtract);
                        cmd.Parameters.AddWithValue("@bloodGroup", comboBox1.SelectedItem.ToString());

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    MessageBox.Show("Your order placed successfully");

                    // Clear other fields
                    textBox1.Text = string.Empty;
                    textBox3.Text = string.Empty;
                    comboBox1.Text = string.Empty;
                    richTextBox1.Text = string.Empty;
                    richTextBox2.Text = string.Empty;
                    dataGridView1.DataSource = null;
                }
                else
                {
                    MessageBox.Show("Please fill all entries before submission");
                }
            }
            else
            {
                // Display a message if parsing fails
                MessageBox.Show("Please enter a valid integer in quantity required.");
            }

        }

        private void Request_Load(object sender, EventArgs e)
        {

        }
    }
}
