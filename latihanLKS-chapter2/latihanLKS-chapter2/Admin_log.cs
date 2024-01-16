using Guna.UI2.WinForms;
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
    public partial class Admin_log : Form
    {
        SqlConnection conn = Connection.Connect();
        SqlCommand cmd;
        SqlDataAdapter dr;
        DataTable dt;

        public void tabel()
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT l.id_log, u.username, CONVERT(datetime, l.waktu) as date, l.aktifitas FROM [tbl_log] l INNER JOIN [tbl_user] u ON l.id_user = u.id_user ORDER BY l.waktu DESC";
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();

        }
        public Admin_log()
        {
            InitializeComponent();
            tabel();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Admin_ku ku = new Admin_ku();
            ku.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT l.id_log, u.username, l.waktu, l.aktifitas FROM [tbl_log] l INNER JOIN [tbl_user] u ON l.id_user = u.id_user WHERE l.waktu = @waktu ORDER BY l.waktu DESC";
            cmd.Parameters.AddWithValue("@waktu", guna2DateTimePicker1.Value);
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Admin_kl kl = new Admin_kl();
            kl.Show();
            this.Hide();
        }
    }
}
