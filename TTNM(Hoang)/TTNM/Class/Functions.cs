using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TTNM.Class
{
    internal class Functions
    {
        // Cải tiến: Không cần thiết khai báo biến riêng cho chuỗi kết nối
        // Hàm Connect() đã đảm nhận việc tạo kết nối, nên biến Conn có thể là private để kiểm soát truy cập tốt hơn
        private static SqlConnection Conn;

        // Hàm kết nối CSDL - chỉ nên tạo một kết nối duy nhất và giữ cho nó mở trong suốt quá trình hoạt động của ứng dụng
        public static void Connect()
        {
            if (Conn == null || Conn.State != System.Data.ConnectionState.Open)
            {
                string connString = "Data Source=DESKTOP-QK3BES8\\SQLEXPRESS;Initial Catalog=TTNM;Integrated Security=True";
                Conn = new SqlConnection(connString); // Khởi tạo kết nối với chuỗi kết nối
                Conn.Open(); // Mở kết nối
            }
        }

        public static void Disconnect()
        {
            if (Conn != null && Conn.State == System.Data.ConnectionState.Open)
            {
                Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
        }

        public static string GetLogin(string username, string password)
        {
            string result = "";
            try 
            {
                Connect(); 

                using (SqlCommand cmd = new SqlCommand("SELECT Tentaikhoan FROM Taikhoan WHERE Tentaikhoan = @tentaikhoan AND Matkhau = @matkhau", Conn))
                {
                    cmd.Parameters.AddWithValue("@tentaikhoan", username);
                    cmd.Parameters.AddWithValue("@matkhau", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = reader["Tentaikhoan"].ToString(); 
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi SQL: " + ex.Message);
            }
            return result;
        }

        //GetDataToTable
        public static System.Data.DataTable GetDataToTable(string sql)
        {
            Connect();
            System.Data.DataTable table = new System.Data.DataTable();
            using (SqlCommand cmd = new SqlCommand(sql, Conn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            return table;
        }

        public static void RunSql(string sql)
        {
            SqlCommand cmd;		                // Khai báo đối tượng SqlCommand
            cmd = new SqlCommand();	         // Khởi tạo đối tượng
            cmd.Connection = Functions.Conn;	  // Gán kết nối
            cmd.CommandText = sql;			  // Gán câu lệnh SQL
            try
            {
                cmd.ExecuteNonQuery();		  // Thực hiện câu lệnh SQL
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }

        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Functions.Conn);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            cbo.DataSource = table;

            cbo.ValueMember = ma;    // Truong gia tri
            cbo.DisplayMember = ten;    // Truong hien thi
        }


    }
}
