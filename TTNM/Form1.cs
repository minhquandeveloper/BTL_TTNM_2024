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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKhoi12_Click(object sender, EventArgs e)
        {
            Form12 form2 = new Form12();
            form2.Show();
            //this.Opacity = 1; 
        }

        private void btnKhoi11_Click(object sender, EventArgs e)
        {
            Form11 form3 = new Form11();
            form3.Show();
        }

        private void btnKhoi10_Click(object sender, EventArgs e)
        {
            Form10 form4 = new Form10();    
            form4.Show();
        }
    }
}
