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


namespace TTNM
{
    public partial class FormThem : Form
    {
        private Action reloadDataCallback;

        public FormThem(Action reloadDataCallback)
        {
            InitializeComponent();
            this.reloadDataCallback = reloadDataCallback;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenlop = txtTenLop.Text;
            int siso;
            if (!int.TryParse(txtSiSo.Text, out siso))
            {
                MessageBox.Show("Sĩ số phải là một số nguyên.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string hovatenGVCN = txtGVCN.Text;

            try
            {
                using (SqlConnection connection = DbConnection.Instance.Connection)
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string queryLop = @"
                                INSERT INTO Lop (Tenlop, Siso)
                                VALUES (@Tenlop, @Siso);
                                SELECT SCOPE_IDENTITY();";

                            SqlCommand cmdLop = new SqlCommand(queryLop, connection, transaction);
                            cmdLop.Parameters.AddWithValue("@Tenlop", tenlop);
                            cmdLop.Parameters.AddWithValue("@Siso", siso);

                            int newLopID = Convert.ToInt32(cmdLop.ExecuteScalar());

                            string queryGVCN = @"
                                INSERT INTO GVCN (Hovaten, Lopchunhiem)
                                VALUES (@Hovaten, @Lopchunhiem)";

                            SqlCommand cmdGVCN = new SqlCommand(queryGVCN, connection, transaction);
                            cmdGVCN.Parameters.AddWithValue("@Hovaten", hovatenGVCN);
                            cmdGVCN.Parameters.AddWithValue("@Lopchunhiem", newLopID);

                            cmdGVCN.ExecuteNonQuery();

                            transaction.Commit();

                            CustomFormSuccess.ShowSuccess("Thêm mới thành công");

                            reloadDataCallback?.Invoke();

                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

