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

namespace latihanLKS_chapter2
{
    public partial class Admin_kl : Form
    {

        SqlConnection conn = Connection.Connect();
        SqlCommand cmd;
        SqlDataAdapter dr;
        DataTable dt;
        SqlDataReader rd;
        public Admin_kl()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM [tbl_transaksi] WHERE tgl_transaksi BETWEEN '" + awal.Text + "' AND '" + akhir.Text + "'";
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT tgl_transaksi AS date, SUM(tbl_transaksi.total_bayar) AS harga FROM [tbl_transaksi] WHERE tgl_transaksi BETWEEN '" + awal.Text + "' AND '" + akhir.Text + "' GROUP BY tgl_transaksi";
            rd = cmd.ExecuteReader();
            int i = 0;
         
            while (rd.Read())
            {
                string date = rd["date"].ToString();
                double harga = Convert.ToDouble(rd["harga"]);
          
                chart1.Series[i].Points.AddXY(date, harga);
            }
            conn.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand log = conn.CreateCommand();
            log.CommandType = System.Data.CommandType.Text;
            log.CommandText = "INSERT INTO [tbl_log] (id_user, aktifitas) VALUES(@id, 'LOGOUT')";
            log.Parameters.AddWithValue("@id", Session.id_user);
            log.ExecuteNonQuery();
            conn.Close();

        }
    }
}
