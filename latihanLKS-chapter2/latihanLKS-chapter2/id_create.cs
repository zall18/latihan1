using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihanLKS_chapter2
{
    static class id_create
    {
        public static SqlConnection conn = Connection.Connect();

        public static string No_transaksi()
        {
            string id;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT no_transaksi FROM [tbl_transaksi] ORDER BY no_transaksi DESC";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                int urut;
                urut = Convert.ToInt32(dr["no_transaksi"].ToString().Substring(4)) + 1;
                id = "TRN" + urut.ToString("000");
               

            }
            else
            {
                id = "TRN001";
            }
            dr.Close();
            return id;
        }


    }
}
