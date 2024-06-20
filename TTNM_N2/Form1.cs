using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TTNM_N2
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data source=LAPTOP-RUNN5F4F\\SQLEXPRESS;Initial catalog=TTNM;Integrated security=true";
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Hocsinh";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Hocsinh (Mahocsinh, Hovaten, Gioitinh, Ngaysinh, MaLop, SDT_Phuhuynh, Hocky, Namhoc) VALUES (@Mahocsinh, @Hovaten, @Gioitinh, @Ngaysinh, @MaLop, @SDT_Phuhuynh, @Hocky, @Namhoc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Mahocsinh", tbMhs.Text);
                cmd.Parameters.AddWithValue("@Hovaten", tbHoten.Text);
                cmd.Parameters.AddWithValue("@Gioitinh", rdNam.Checked ? "Nam" : "Nữ");
                cmd.Parameters.AddWithValue("@Ngaysinh", dtpNgay.Value);
                cmd.Parameters.AddWithValue("@MaLop", tbMalop.Text);
                cmd.Parameters.AddWithValue("@SDT_Phuhuynh", tbSdt.Text);
                cmd.Parameters.AddWithValue("@Hocky", tbHocky.Text);
                cmd.Parameters.AddWithValue("@Namhoc", tbNamhoc.Text);
                cmd.ExecuteNonQuery();
            }
            LoadData();
            MessageBox.Show("Bạn đã thêm thông tin thành công!");
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (selectedId >= 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Hocsinh SET Mahocsinh = @Mahocsinh, Hovaten = @Hovaten, Gioitinh = @Gioitinh, Ngaysinh = @Ngaysinh, MaLop = @MaLop, SDT_Phuhuynh = @SDT_Phuhuynh, Hocky = @Hocky, Namhoc = @Namhoc WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", selectedId);
                    cmd.Parameters.AddWithValue("@Mahocsinh", tbMhs.Text);
                    cmd.Parameters.AddWithValue("@Hovaten", tbHoten.Text);
                    cmd.Parameters.AddWithValue("@Gioitinh", rdNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@Ngaysinh", dtpNgay.Value);
                    cmd.Parameters.AddWithValue("@MaLop", tbMalop.Text);
                    cmd.Parameters.AddWithValue("@SDT_Phuhuynh", tbSdt.Text);
                    cmd.Parameters.AddWithValue("@Hocky", tbHocky.Text);
                    cmd.Parameters.AddWithValue("@Namhoc", tbNamhoc.Text);
                    cmd.ExecuteNonQuery();
                }
                LoadData();
                MessageBox.Show("Bạn đã sửa thông tin thành công!");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để sửa.");
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (selectedId >= 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Hocsinh WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", selectedId);
                    cmd.ExecuteNonQuery();
                }
                LoadData();
                MessageBox.Show("Bạn đã xóa thông tin thành công!");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.");
            }
        }
        private int selectedId = -1;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    selectedId = Convert.ToInt32(row.Cells["id"].Value);
                    tbMhs.Text = row.Cells["Mahocsinh"].Value.ToString();
                    tbHoten.Text = row.Cells["Hovaten"].Value.ToString();
                    if (row.Cells["Gioitinh"].Value.ToString() == "Nam")
                    {
                        rdNam.Checked = true;
                    }
                    else
                    {
                        rdNu.Checked = true;
                    }
                    dtpNgay.Value = Convert.ToDateTime(row.Cells["Ngaysinh"].Value);
                    tbMalop.Text = row.Cells["MaLop"].Value.ToString();
                    tbSdt.Text = row.Cells["SDT_Phuhuynh"].Value.ToString();
                    tbHocky.Text = row.Cells["Hocky"].Value.ToString();
                    tbNamhoc.Text = row.Cells["Namhoc"].Value.ToString();
                }
        }
    }
}
