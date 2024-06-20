using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTNM
{
    public partial class Home : Form
    {
        private string username;

        public Home(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = username;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagerPoint quanLyDiem = new ManagerPoint(username);
            quanLyDiem.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MangerStu form = new MangerStu();
            form.ShowDialog();
        }
    }
}
