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
using System.Data.Common;
using System.Data.Odbc;

namespace TTNM
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void ReloadData()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Câu lệnh SQL để truy vấn dữ liệu
                string query = @"
                        SELECT Lop.id, Lop.Tenlop, Lop.Siso, GVCN.Hovaten
                        FROM Lop
                        JOIN GVCN ON Lop.id = GVCN.Lopchunhiem
                        WHERE Lop.Tenlop LIKE '%12%'
                        ";

                // Tạo SqlCommand
                using (SqlCommand command = new SqlCommand(query, DbConnection.Instance.Connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            dgv12.AutoGenerateColumns = false; // Tắt tự động tạo cột

                            dgv12.Columns.Clear();
                            AddColumn("id", "ID");
                            AddColumn("Tenlop", "Tên lớp");
                            AddColumn("Siso", "Sĩ số");
                            AddColumn("Hovaten", "Họ và tên GVCN");

                            dgv12.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu để hiển thị.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddColumn(string dataPropertyName, string headerText)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            dgv12.Columns.Add(column);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DbConnection.Instance.CloseConnection();
            base.OnFormClosing(e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem12_Click(object sender, EventArgs e)
        {
            FormThem form5 = new FormThem(this.ReloadData);
            form5.Show();
        }
    }
}

