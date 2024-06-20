using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TTNM.Class
{
    internal class Functions
    {
        private static SqlConnection Conn;
        public static void Connect()
        {
            if (Conn == null || Conn.State != System.Data.ConnectionState.Open)
            {
                string connString = "Data Source=DESKTOP-HKODH8U\\SQLEXPRESS;Initial Catalog=TTNM;Integrated Security=True";
                Conn = new SqlConnection(connString); 
                Conn.Open(); 
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

        public static (string, string) GetLogin(string username, string password)
        {
            string retrievedUsername = "";
            string role = "";
            try
            {
                Connect();
                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }
                using (SqlCommand cmd = new SqlCommand("SELECT Tentaikhoan, Chucvu_id FROM Taikhoan WHERE Tentaikhoan = @tentaikhoan AND Matkhau = @matkhau", Conn))
                {
                    cmd.Parameters.AddWithValue("@tentaikhoan", username);
                    cmd.Parameters.AddWithValue("@matkhau", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retrievedUsername = reader["Tentaikhoan"].ToString();
                            role = reader["Chucvu_id"].ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi SQL: " + ex.Message);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
            return (retrievedUsername, role);
        }


        //GetDataToTable
        public static DataTable GetDataToTable(string sql)
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
        public static DataTable GetDataToTable(string sql, SqlParameter[] parameters = null)
        {
            Connect();
            DataTable table = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sql, Conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

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
