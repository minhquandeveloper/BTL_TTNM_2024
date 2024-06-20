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
    public partial class MangerStu : Form
    {
        public MangerStu()
        {
            InitializeComponent();
            
        }
        
        private void MangerStu_Load(object sender, EventArgs e)
        {
            Functions.Connect();
            button3.Focus();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string sql = "SELECT ROW_NUMBER() OVER(ORDER BY hs.id ASC) AS STT,hs.Mahocsinh AS 'Mã học sinh', hs.Hovaten AS 'Họ và tên học sinh', hs.Gioitinh AS 'Giới tính', hs.Ngaysinh AS 'Ngày sinh', l.Tenlop AS 'Tên lớp', hs.SDT_Phuhuynh AS 'Số điện thoại phụ huynh', hs.Hocky AS 'Học kỳ', hs.Namhoc AS 'Năm học' FROM Hocsinh hs JOIN Lop l ON hs.MaLop = l.id WHERE l.Tenlop = N'" + txtsearch.Text.Trim() + "'";
;
            dgvDanhSachHS.DataSource = Functions.GetDataToTable(sql);
            Functions.Disconnect();
        }
    }
}
