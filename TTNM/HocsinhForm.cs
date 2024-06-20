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
    public partial class HocsinhForm : Form
    {
        private string username;
        public HocsinhForm(string username)
        {
            InitializeComponent();
            this.username = username;

        }

        private void HocsinhForm_Load(object sender, EventArgs e)
        {
            label1.Text = username;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            HocsinhFormQLD hocsinhFormQLD = new HocsinhFormQLD(username);
            hocsinhFormQLD.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = username;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
