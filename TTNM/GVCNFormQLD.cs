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
    public partial class GVCNFormQLD : Form
    {
        private string username;
        public GVCNFormQLD(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void GVCNFormQLD_Load(object sender, EventArgs e)
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
            AddDiemGVCN addDiemGVCN = new AddDiemGVCN(username);
            addDiemGVCN.ShowDialog();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GVCNForm home = new GVCNForm(username);
            home.Show();
        }

        private void button10_Click(object sender, EventArgs e)
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

            SuaDiemGVCN editDiem = new SuaDiemGVCN(username, hocsinhID, hovaten, lop, monhoc, diemso);
            editDiem.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dgvDiem.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một học sinh trước khi xóa điểm.", "Thông báo");
                return;
            }
            else
            {
                dgvDiem.Visible = false;
                btnAddDiem.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                pictureBox1.Visible = false;
                DialogResult result = MessageBox.Show($"Bạn có thực sự muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    dgvDiem.Visible = true;
                    btnAddDiem.Visible = true;
                    button9.Visible = true;
                    button10.Visible = true;
                    pictureBox1.Visible = true;
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
                    button9.Visible = true;
                    button10.Visible = true;
                    pictureBox1.Visible = true;
                    LoadData();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
