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
    public partial class Gudang : Form
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
            cmd.CommandText = "SELECT * FROM [tbl_barang]";
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();
        }

        public void clear()
        {
            kode.Text = null;
            nama.Text = null;
            jumlah.Text = null;
            satuan.Text = null;
            harga.Text = null;
            guna2DateTimePicker1.Value= DateTime.Now;

            guna2Button5.Enabled = false;
            guna2Button6.Enabled = false;
            guna2Button4.Enabled = true;
        }

        public Gudang()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT INTO [tbl_barang] VALUES (@kode, @nama, @exp, @jml, @satuan, @harga)";
            cmd.Parameters.AddWithValue("@kode", kode.Text);
            cmd.Parameters.AddWithValue("@nama", nama.Text);
            cmd.Parameters.AddWithValue("@exp", guna2DateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@jml", jumlah.Text);
            cmd.Parameters.AddWithValue("@satuan", satuan.Text);
            cmd.Parameters.AddWithValue("@harga", harga.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Data berhasil ditambahkan!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabel();
            clear();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE [tbl_barang] SET kode_barang = @kode, nama_barang = @nama, expired_date = @exp, jumlah_barang = @jml, satuan = @satuan, harga_satuan = @harga WHERE id_barang = @id";
            cmd.Parameters.AddWithValue("id", id.Text);
            cmd.Parameters.AddWithValue("@kode", kode.Text);
            cmd.Parameters.AddWithValue("@nama", nama.Text);
            cmd.Parameters.AddWithValue("@exp", guna2DateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@jml", jumlah.Text);
            cmd.Parameters.AddWithValue("@satuan", satuan.Text);
            cmd.Parameters.AddWithValue("@harga", harga.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Data berhasil diubah!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabel();
            clear();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE FROM [tbl_barang] WHERE id_barang = @id";
            cmd.Parameters.AddWithValue("id", id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Data berhasil dihapus!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabel();
            clear();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow data = this.guna2DataGridView1.Rows[e.RowIndex];

                kode.Text = data.Cells["kode_barang"].Value.ToString();
                nama.Text = data.Cells["nama_barang"].Value.ToString();
                jumlah.Text = data.Cells["jumlah_barang"].Value.ToString();
                satuan.Text = data.Cells["satuan"].Value.ToString();
                harga.Text = data.Cells["harga_satuan"].Value.ToString();
                id.Text = data.Cells["id_barang"].Value.ToString();
            }

            guna2Button4.Enabled = false;
            guna2Button5.Enabled = true;
            guna2Button6.Enabled = true;
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM [tbl_gudang] WHERE nama_barang LIKE '%"+ this.search.Text +"%'";
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
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
