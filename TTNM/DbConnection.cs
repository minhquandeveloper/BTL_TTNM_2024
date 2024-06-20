using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TTNM
{
    public class DbConnection
    {
        private static DbConnection instance;
        private SqlConnection connection;

        public DbConnection()
        {
            string connectionString = @"Data Source=DESKTOP-10O5HLK\BUIPHU;database=TTNM;integrated security=true;";
            connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Kết nối đến cơ sở dữ liệu đã thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message);
            }
        }

        public static DbConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbConnection();
                }
                return instance;
            }
        }

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}