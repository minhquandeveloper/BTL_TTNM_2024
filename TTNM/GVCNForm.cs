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
    public partial class GVCNForm : Form
    {
        private string username;
        public GVCNForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void GVCNForm_Load(object sender, EventArgs e)
        {
            label1.Text = username;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            GVCNFormQLD gVCNFormQLD = new GVCNFormQLD(username);
            gVCNFormQLD.ShowDialog();
            this.Close();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
