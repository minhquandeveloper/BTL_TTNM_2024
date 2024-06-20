using System;
using System.Data;
using System.Windows.Forms;
using TTNM.Class;
using System.Data.SqlClient;

namespace TTNM
{
    public partial class HocsinhFormQLD : Form
    {
        private string username;
        public HocsinhFormQLD(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private void HocsinhFormQLD_Load(object sender, EventArgs e)
        {
            try
            {
                label1.Text = username;
                string query = @"
                    SELECT TOP 2
                        hsmh.monhoc_id AS [Mã môn học],
                        hs.Hovaten AS [Tên học sinh],
                        hsmh.Diemso AS [Điểm],
                        hsmh.Hocky AS [Học kỳ],
                        hsmh.Namhoc AS [Năm học]
                    FROM Hocsinh_monhoc hsmh
                    INNER JOIN Monhoc m ON hsmh.monhoc_id = m.id
                    INNER JOIN Hocsinh hs ON hsmh.hocsinh_id = hs.id
                    WHERE hs.Mahocsinh = @mahocsinh
                    ORDER BY hsmh.hocsinh_id, hsmh.monhoc_id";
                SqlParameter[] parameters = {
                    new SqlParameter("@mahocsinh", username) // Sử dụng mã học sinh đã đăng nhập
                };

                DataTable data = Functions.GetDataToTable(query, parameters);
                if (data == null)
                {
                    MessageBox.Show("Không thể tải dữ liệu từ cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dgvDiem.DataSource = data;
                dgvDiem.Columns["Mã môn học"].HeaderText = "Mã môn học";
                dgvDiem.Columns["Tên học sinh"].HeaderText = "Tên học sinh";
                dgvDiem.Columns["Điểm"].HeaderText = "Điểm";
                dgvDiem.Columns["Học kỳ"].HeaderText = "Học kỳ";
                dgvDiem.Columns["Năm học"].HeaderText = "Năm học";

                dgvDiem.AllowUserToAddRows = false;
                dgvDiem.EditMode = DataGridViewEditMode.EditProgrammatically;
                dgvDiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = username;
        }

        private void dgvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnAddDiem_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"
                    SELECT 
                        hsmh.monhoc_id AS [Mã môn học],
                        hs.Hovaten AS [Tên học sinh],
                        hsmh.Diemso AS [Điểm],
                        hsmh.Hocky AS [Học kỳ],
                        hsmh.Namhoc AS [Năm học]
                    FROM Hocsinh_monhoc hsmh
                    INNER JOIN Monhoc m ON hsmh.monhoc_id = m.id
                    INNER JOIN Hocsinh hs ON hsmh.hocsinh_id = hs.id
                    WHERE hs.Mahocsinh = @mahocsinh
                    ORDER BY hsmh.hocsinh_id, hsmh.monhoc_id";
                SqlParameter[] parameters = {
                    new SqlParameter("@mahocsinh", username) 
                };
                DataTable data = Functions.GetDataToTable(query, parameters);
                if (data == null)
                {
                    MessageBox.Show("Không thể tải dữ liệu từ cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dgvDiem.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
