using Blood_bank.custom;
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
    public partial class Donate : Form
    {
        public Donate()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SUPRADDEP\OneDrive\Documents\Blood.mdf;Integrated Security=True;");

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Donate_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the entered value is a valid 10-digit decimal
                decimal phoneNumber;
                if (textBox2.Text.Length != 10 || !decimal.TryParse(textBox2.Text, out phoneNumber))
                {
                    MessageBox.Show("Please enter a valid 10-digit phone number.");
                    return; // Exit the method early
                }

                con.Open();

                string selectQuery = "SELECT * FROM [user] WHERE pho_no = @pho_no";

                using (SqlCommand selectCmd = new SqlCommand(selectQuery, con))
                {
                    selectCmd.Parameters.AddWithValue("@pho_no", phoneNumber);

                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            string query = "Insert into donate values(@a,@b,@c)";
                            SqlCommand inCmd = new SqlCommand(query, con);
                            inCmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                            inCmd.Parameters.AddWithValue("@b", phoneNumber);
                            inCmd.Parameters.AddWithValue("@c",textBox3.Text);
                            inCmd.ExecuteNonQuery();

                        }
                        else
                        {
                            reader.Close();

                            string insertQuery = "INSERT INTO [user] (pho_no,name,blood_group,address,last_date_of_donation) VALUES (@param1, @param2, @param3, @param4, @param5)";

                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                            {
                                // Assuming the columns in your 'user' table are named column1, column2, column3, column4, and date_column
                                insertCmd.Parameters.AddWithValue("@param1", phoneNumber);
                                insertCmd.Parameters.AddWithValue("@param2", textBox1.Text);
                                insertCmd.Parameters.AddWithValue("@param3", comboBox1.Text);
                                insertCmd.Parameters.AddWithValue("@param4", richTextBox1.Text);
                                insertCmd.Parameters.AddWithValue("@param5", dateTimePicker1.Value);

                                insertCmd.ExecuteNonQuery();
                                MessageBox.Show("Blood Donation Registration Completed Successfully.");
                            }
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

        private bool IsMoreThanSixMonths(DateTime startDate, DateTime endDate)
        {
            TimeSpan difference = endDate - startDate;
            return difference.TotalDays > 6 * 30;
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                // Check if the entered value is a valid 10-digit decimal
                decimal phoneNumber=0;
                if (textBox2.Text.Length != 10||!decimal.TryParse(textBox2.Text, out phoneNumber) )
                {
                   
                    return; // Exit the method early
                }

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [user],[donate] WHERE pho_no = @pho_no", con))
                {
                     cmd.Parameters.AddWithValue("@pho_no",phoneNumber);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            textBox1.Text = reader["name"].ToString();
                            comboBox1.Text = reader["blood_group"].ToString();
                            richTextBox1.Text = reader["address"].ToString();

                            if (reader["last_date_of_donation"] != DBNull.Value)
                            {
                                DateTime lastDonationDate = (DateTime)reader["last_date_of_donation"];
                                DateTime selectedDate = dateTimePicker1.Value;

                                if (IsMoreThanSixMonths(lastDonationDate, selectedDate))
                                {
                                    // Additional code if the condition is met
                                }
                                else
                                {
                                    MessageBox.Show("Minimum gap between two consecutive donations must be at least 6 months.");
                                }
                            }
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
    }

}


