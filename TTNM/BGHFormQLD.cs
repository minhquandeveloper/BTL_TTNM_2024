using System;
using System.Data;
using System.Windows.Forms;

namespace TTNM
{
    public partial class BGHFormQLD : Form
    {
        private string username;

        public BGHFormQLD(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void ManagerPoint_Load(object sender, EventArgs e)
        {
            label1.Text = username;
            LoadData();
        }

        private void LoadData()
        {
            string sql = "SELECT hs.id, hs.Hovaten, l.Tenlop, mh.Tenmon, hsmh.Diemso " +
                         "FROM Hocsinh hs " +
                         "INNER JOIN Lop l ON hs.MaLop = l.id " +
                         "INNER JOIN Hocsinh_Monhoc hsmh ON hs.id = hsmh.hocsinh_id " +
                         "INNER JOIN Monhoc mh ON hsmh.monhoc_id = mh.id";

            DataTable DataHS = Class.Functions.GetDataToTable(sql); 
            dgvDiem.DataSource = DataHS;

            dgvDiem.Columns["id"].HeaderText = "STT"; 
            dgvDiem.Columns["Hovaten"].HeaderText = "Họ và tên";
            dgvDiem.Columns["Tenlop"].HeaderText = "Lớp";
            dgvDiem.Columns["Tenmon"].HeaderText = "Môn học";
            dgvDiem.Columns["Diemso"].HeaderText = "Điểm số";

            dgvDiem.AllowUserToAddRows = false;
            dgvDiem.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvDiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnAddDiem_Click(object sender, EventArgs e)
        {
            AddDiemBGH addDiem = new AddDiemBGH(username);
            addDiem.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BGHForm home = new BGHForm(username);
            home.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dgvDiem.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một học sinh trước khi sửa điểm.");
                return;
            }

            int i = dgvDiem.CurrentRow.Index;
            string hocsinhID = dgvDiem.Rows[i].Cells["id"].Value.ToString(); 
            string hovaten = dgvDiem.Rows[i].Cells["Hovaten"].Value.ToString();
            string lop = dgvDiem.Rows[i].Cells["Tenlop"].Value.ToString();
            string monhoc = dgvDiem.Rows[i].Cells["Tenmon"].Value.ToString();
            string diemso = dgvDiem.Rows[i].Cells["Diemso"].Value.ToString();

            SuaDiemBGH editDiem = new SuaDiemBGH(username, hocsinhID, hovaten, lop, monhoc, diemso);
            editDiem.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dgvDiem.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một học sinh trước khi xóa điểm.","Thông báo");
                return;
            } else
            {
                dgvDiem.Visible = false;
                btnAddDiem.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                pictureBox1.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                DialogResult result = MessageBox.Show($"Bạn có thực sự muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    dgvDiem.Visible = true;
                    btnAddDiem.Visible = true;
                    button7.Visible = true;
                    button8.Visible = true;
                    pictureBox1.Visible = true;
                    pictureBox6.Visible = true;
                    pictureBox7.Visible = true;
                    return;

                }
                else
                {
                    int i = dgvDiem.CurrentRow.Index;
                    string hocsinhID = dgvDiem.Rows[i].Cells["id"].Value.ToString();
                    string monhoc = dgvDiem.Rows[i].Cells["Tenmon"].Value.ToString();

                    string sql = "DELETE FROM Hocsinh_Monhoc WHERE hocsinh_id = " + hocsinhID + " AND monhoc_id = (SELECT id FROM Monhoc WHERE Tenmon = N'" + monhoc + "')";
                    Class.Functions.RunSql(sql);
                    dgvDiem.Visible = true;
                    btnAddDiem.Visible = true;
                    button7.Visible = true;
                    button8.Visible = true;
                    pictureBox1.Visible = true;
                    pictureBox6.Visible = true;
                    pictureBox7.Visible = true;
                    LoadData();
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}
