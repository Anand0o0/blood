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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Blood_bank
{
    public partial class SignUp : Form
    {
        private login logins;
        public SignUp()
        {
            InitializeComponent();
            this.logins = logins;
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SUPRADDEP\OneDrive\Documents\Blood.mdf;Integrated Security=True;");
     

        private void SignIn_Load(object sender, EventArgs e)
        {

        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = $"INSERT INTO employee VALUES ({int.Parse(textBox1.Text)}, '{textBox2.Text}', {double.Parse(textBox3.Text)}, '{comboBox1.SelectedItem}', '{textBox5.Text}', '{textBox6.Text}', '{maskedTextBox1.Text}')";
            if(maskedTextBox1.Text == maskedTextBox2.Text)
            {
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Error in data");
            }
            con.Close();
            login logins = new login();
            logins.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
