using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TTNM
{
    public partial class ManagerPoint : Form
    {
        private string username;

        public ManagerPoint(string username)
        {
            InitializeComponent();
            this.username = username;

        }

        private void ManagerPoint_Load(object sender, EventArgs e)
        {
            label1.Text = username;
            LoadData();
        }

        private void dgvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void LoadData()
        {
            //Lấy dữ liệu từ bảng Hocsinh, Monhoc và Hocsinh_monhoc để lấy ra những thông tin như id, Hovaten, Lop, Monhoc, Diemso
            string sql = @"
            SELECT 
                hs.id AS HocsinhID, 
                hs.Hovaten, 
                hs.Lop, 
                mh.Tenmon AS Monhoc, 
                hsmh.Diemso
            FROM 
                hocsinh hs
            JOIN 
                Hocsinh_Monhoc hsmh ON hs.id = hsmh.hocsinh_id
            JOIN 
                Monhoc mh ON mh.id = hsmh.monhoc_id;";

            DataTable DataHS = new DataTable();
            DataHS = Class.Functions.GetDataToTable(sql);
            dgvDiem.DataSource = DataHS;

            dgvDiem.Columns[0].HeaderText = "STT";
            dgvDiem.Columns[1].HeaderText = "Họ và tên";
            dgvDiem.Columns[2].HeaderText = "Lớp";
            dgvDiem.Columns[3].HeaderText = "Môn học";
            dgvDiem.Columns[4].HeaderText = "Điểm số";
            dgvDiem.AllowUserToAddRows = false;
            dgvDiem.EditMode = DataGridViewEditMode.EditProgrammatically;

            DataGridViewImageColumn editColumn = new DataGridViewImageColumn();
            editColumn.HeaderText = "Sửa";
            editColumn.Image = Properties.Resources.kisspng_computer_icons_button_encapsulated_postscript_plus_sign_5ae17e114f8e32_0038317615247273133259;
            editColumn.Name = "Edit";
            editColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvDiem.Columns.Add(editColumn);

            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.HeaderText = "Xóa";
            deleteColumn.Image = Properties.Resources.kisspng_computer_icons_button_encapsulated_postscript_plus_sign_5ae17e114f8e32_0038317615247273133259; 
            deleteColumn.Name = "Delete";
            deleteColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvDiem.Columns.Add(deleteColumn);

            dgvDiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDiem.RowTemplate.Height = 5; 

            dgvDiem.CellClick += dgvDiem_CellContentClick;

        }

        private void btnAddDiem_Click(object sender, EventArgs e)
        {

            // Mở form AddDiem và truyền tham số username
            AddDiem addDiem = new AddDiem(username);
            addDiem.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home(username);
            home.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
