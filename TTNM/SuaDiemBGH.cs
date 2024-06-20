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
    public partial class SuaDiemBGH : Form
    {
        private string username;
        private string hocsinhID;
        private string hovaten;
        private string lop;
        private string monhoc;
        private string diemso;
        public SuaDiemBGH(string username, string hocsinhID, string hovaten, string lop, string monhoc, string diemso)
        {
            InitializeComponent();
            this.username = username;
            this.hocsinhID = hocsinhID;
            this.hovaten = hovaten;
            this.lop = lop;
            this.monhoc = monhoc;
            this.diemso = diemso;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SuaDiemBGH_Load(object sender, EventArgs e)
        {
            label1.Text = username;
            txtHoTen.Text = hovaten;
            txtTenLop.Text = lop;
            txtTenMon.Text = monhoc;
            txtDiem.Text = diemso;

            txtHoTen.Enabled = false;
            txtTenLop.Enabled = false;
            txtTenMon.Enabled = false;
        }

        private void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            string newDiemso = txtDiem.Text;

            string sqlUpdateDiem = $"UPDATE Hocsinh_Monhoc SET Diemso = '{newDiemso}' WHERE hocsinh_id = {hocsinhID} AND monhoc_id = (SELECT id FROM Monhoc WHERE Tenmon = '{monhoc}')";

            try
            {
                Functions.RunSql(sqlUpdateDiem);
                MessageBox.Show("Cập nhật điểm thành công", "Thông báo", MessageBoxButtons.OK);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyThongTin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            
        }
    }
}


