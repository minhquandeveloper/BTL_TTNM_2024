using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTNM.Class;

namespace TTNM
{
    public partial class AddDiem : Form
    {
        private static int currentId = 5;
        private string username;

        public AddDiem(string username)
        {
            InitializeComponent();
            this.username = username;
            Functions.FillCombo("SELECT id, Tenmon FROM Monhoc", cobMonHoc, "id", "Tenmon");
            cobMonHoc.SelectedIndex = -1;
            Functions.FillCombo("SELECT id, Hovaten FROM Hocsinh", cboHoten, "id", "Hovaten");
            cboHoten.SelectedIndex = -1;
            Functions.FillCombo("SELECT id, Lop FROM Hocsinh", cboLop, "id", "Lop");
            cboLop.SelectedIndex = -1;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

            int hocsinhId = (int)cboHoten.SelectedValue;
            int monhocId = (int)cobMonHoc.SelectedValue;
            string diemso = txtDiem.Text;

            string hocky = "1";
            string namhoc = "2024-2025";
            string sqlHocSinhMonHoc = $"INSERT INTO Hocsinh_Monhoc (hocsinh_id, monhoc_id, Diemso, Hocky, Namhoc) " +
                                       $"VALUES ({hocsinhId}, {monhocId}, '{diemso}', '{hocky}', '{namhoc}')";
            Functions.RunSql(sqlHocSinhMonHoc);

            MessageBox.Show("Thêm điểm thành công", "Thông báo", MessageBoxButtons.OK);
            this.Close();
        }

        private void AddDiem_Load(object sender, EventArgs e)
        {
            label1.Text = username;
        }

        private void cobMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
