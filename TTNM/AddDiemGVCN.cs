using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTNM.Class;

namespace TTNM
{
    public partial class AddDiemGVCN : Form
    {
        private string username;

        public AddDiemGVCN(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void AddDiemGVCN_Load(object sender, EventArgs e)
        {
            label1.Text = username;
            Functions.FillCombo("SELECT id, Tenmon FROM Monhoc", cobMonHoc, "id", "Tenmon");
            cobMonHoc.SelectedIndex = -1;
            Functions.FillCombo("SELECT id, Hovaten FROM Hocsinh", cboHoten, "id", "Hovaten");
            cboHoten.SelectedIndex = -1;
            Functions.FillCombo("SELECT id, Tenlop FROM Lop", cboLop, "id", "Tenlop");
            cboLop.SelectedIndex = -1;
        }


        private bool IsValidDiem(string diemso)
        {
            double diem;
            if (double.TryParse(diemso, out diem))
            {
                if (diem >= 0 && diem <= 10)
                {
                    return true;
                }
            }
            return false;
        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (cboHoten.SelectedIndex == -1 || cobMonHoc.SelectedIndex == -1 || string.IsNullOrEmpty(txtDiem.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int hocsinhId = (int)cboHoten.SelectedValue;
            int monhocId = (int)cobMonHoc.SelectedValue;
            string diemso = txtDiem.Text.Trim();

            if (!IsValidDiem(diemso))
            {
                MessageBox.Show("Điểm số không hợp lệ, vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hocky = "1";
            string namhoc = "2024-2025";
            string sqlHocSinhMonHoc = $"INSERT INTO Hocsinh_Monhoc (hocsinh_id, monhoc_id, Diemso, Hocky, Namhoc) " + $"VALUES ({hocsinhId}, {monhocId}, '{diemso}', '{hocky}', '{namhoc}')";
            try
            {
                Functions.RunSql(sqlHocSinhMonHoc);
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                txtDiem.Visible = false;
                cboHoten.Visible = false;
                cobMonHoc.Visible = false;
                cboLop.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                MessageBox.Show("Bạn đã thêm mới thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
