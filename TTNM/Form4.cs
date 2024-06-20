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

namespace TTNM
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void ReLoadData()
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
                        LEFT JOIN GVCN ON Lop.id = GVCN.Lopchunhiem
                        WHERE Lop.Tenlop LIKE '%10%'
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
                            dgv10.AutoGenerateColumns = false; // Tắt tự động tạo cột

                            dgv10.Columns.Clear();
                            AddColumn("id", "ID");
                            AddColumn("Tenlop", "Tên lớp");
                            AddColumn("Siso", "Sĩ số");
                            AddColumn("Hovaten", "GVCN");

                            dgv10.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu để hiển thị.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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
            dgv10.Columns.Add(column);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DbConnection.Instance.CloseConnection();
            base.OnFormClosing(e);
        }
        private void btnThem10_Click(object sender, EventArgs e)
        {
            FormThem form5 = new FormThem(this.ReLoadData);
            form5.Show();
        }
    }
}
