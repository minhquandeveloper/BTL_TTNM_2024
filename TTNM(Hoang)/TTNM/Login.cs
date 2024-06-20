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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            string retrievedUsername = Class.Functions.GetLogin(textBox1.Text, textBox2.Text);
            if (string.IsNullOrEmpty(retrievedUsername))
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
                return;
            } 
            // nếu như tên đăng nhập bắt đầu bằng chữ BGH thì mở form BGH, GVCN thì mở form GVCN, GVBM thì mở form GVBM
            if (retrievedUsername.StartsWith("GVCN"))
            {
                this.Hide();
                Home hm = new Home(textBox1.Text);
                hm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
