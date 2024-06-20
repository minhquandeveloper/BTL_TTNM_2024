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
        public static string LoggedInUser { get; private set; } // Thêm thuộc tính để lưu mã học sinh đăng nhập

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

            var loginInfo = Class.Functions.GetLogin(textBox1.Text, textBox2.Text);
            string retrievedUsername = loginInfo.Item1;
            string role = loginInfo.Item2;

            if (string.IsNullOrEmpty(retrievedUsername))
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
                return;
            }

            LoggedInUser = textBox1.Text; // Gán mã học sinh vào thuộc tính LoggedInUser

            this.Hide();
            if (role == "1") // BGH
            {
                BGHForm bghForm = new BGHForm(textBox1.Text);
                bghForm.ShowDialog();
            }
            else if (role == "2") // GVCN
            {
                GVCNForm gvcnForm = new GVCNForm(textBox1.Text);
                gvcnForm.ShowDialog();
            }
            else if (role == "3") // GVBM
            {
                GVBMForm gvbmForm = new GVBMForm(textBox1.Text);
                gvbmForm.ShowDialog();
            }
            else if (role == "4") // Học sinh
            {
                HocsinhForm hocsinhForm = new HocsinhForm(LoggedInUser); // Truyền mã học sinh vào form
                hocsinhForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }


    }
}
