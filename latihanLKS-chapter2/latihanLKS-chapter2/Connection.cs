using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihanLKS_chapter2
{
    static class Connection
    {

        public static SqlConnection conn = null;
        public static string connection = "Data Source=DESKTOP-FIK5SPH\\SQLEXPRESS;Initial Catalog=latihan5;Integrated Security=True";

        public static SqlConnection Connect()
        {
            if(conn == null)
            {
                conn = new SqlConnection(connection);
                return conn;
            }
            return conn;
        }

    }
}
