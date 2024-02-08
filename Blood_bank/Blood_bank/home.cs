using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_bank
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
            homes homes = new homes();
            homes.TopLevel = false;
            panel3.Controls.Add(homes);
            homes.BringToFront();
            homes.Show();

        }

        private void home_Load(object sender, EventArgs e)
        {

        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            Donate donate = new Donate();
            donate.TopLevel = false;
            panel3.Controls.Add(donate);
            donate.BringToFront();
            donate.Show();
        }

        private void home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            homes homes = new homes();
            homes.TopLevel = false;
            panel3.Controls.Add(homes);
            homes.BringToFront();
            homes.Show();
        }

        private void button_WOC3_Click(object sender, EventArgs e)
        {
            Request request = new Request();
            request.TopLevel = false;
            panel3.Controls.Add(request);
            request.BringToFront(); 
            request.Show();
        }

        private void button_WOC4_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.TopLevel = false;
            panel3.Controls.Add(settings);
            settings.BringToFront();
            settings.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            this.Hide();
        }
    }
}
