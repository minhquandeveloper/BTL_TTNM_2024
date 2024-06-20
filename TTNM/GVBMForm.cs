using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TTNM
{
    public partial class GVBMForm : Form
    {
        private string username;
        public GVBMForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void GVBMForm_Load(object sender, EventArgs e)
        {
            label1.Text = username;
        }



        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            GVBMFormQLD gVBMFormQLD = new GVBMFormQLD(username);
            gVBMFormQLD.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
