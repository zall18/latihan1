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
    public partial class Admin_ku : Form
    {
        SqlConnection conn = Connection.Connect();
        SqlCommand cmd;
        SqlDataAdapter dr;
        SqlDataReader rd;
        DataTable dt;

        public void tabel()
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT id_user, tipe_user, nama, alamat, telpon FROM [tbl_user]";
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();
        }
        public void clear()
        {
            tipe.Text = null;
            nama.Text = null;
            Alamat.Text = null;
            telpon.Text = null;
            username.Text = null;
            password.Text = null;
            guna2Button5.Enabled = false;
            guna2Button6.Enabled = false;
            guna2Button4.Enabled = true;
        }

        public Admin_ku()
        {
            InitializeComponent();
            tabel();
            guna2Button6.Enabled = false;
            guna2Button5.Enabled = false;
            guna2Button4.Enabled = true;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT INTO [tbl_user] VALUES (@tipe, @nama, @alamat, @telpon, @username, @password)";
            cmd.Parameters.AddWithValue("@tipe", tipe.Text);
            cmd.Parameters.AddWithValue("@nama", nama.Text);
            cmd.Parameters.AddWithValue("@alamat", Alamat.Text);
            cmd.Parameters.AddWithValue("@telpon", telpon.Text);
            cmd.Parameters.AddWithValue("@username", username.Text);
            cmd.Parameters.AddWithValue("@password", Hash.HashChar(password.Text));
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Data berhasil ditambahkan!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabel();
            clear();

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE [tbl_user] SET tipe_user = @tipe, nama = @nama, alamat = @alamat, telpon = @telpon WHERE id_user = @id";
            cmd.Parameters.AddWithValue("@id", id.Text);
            cmd.Parameters.AddWithValue("@tipe", tipe.Text);
            cmd.Parameters.AddWithValue("@nama", nama.Text);
            cmd.Parameters.AddWithValue("@alamat", Alamat.Text);
            cmd.Parameters.AddWithValue("@telpon", telpon.Text);
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Data berhasil diubah!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabel();
            clear();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE FROM [tbl_user] WHERE id_user = @id";
            cmd.Parameters.AddWithValue("@id", id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Data berhasil dihapus!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabel();
            clear();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT id_user, tipe_user, nama, alamat, telpon FROM [tbl_user] WHERE nama LIKE '%"+ this.search.Text + "%' OR tipe_user LIKE '%"+ this.search.Text +"%'";
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow data = this.guna2DataGridView1.Rows[e.RowIndex];

                tipe.Text = data.Cells["tipe_user"].Value.ToString();
                nama.Text = data.Cells["nama"].Value.ToString();
                telpon.Text = data.Cells["telpon"].Value.ToString();
                Alamat.Text = data.Cells["alamat"].Value.ToString();

                string name = data.Cells["nama"].Value.ToString();
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM [tbl_user] WHERE nama = @nama";
                cmd.Parameters.AddWithValue("@nama", name);
                rd = cmd.ExecuteReader();
                rd.Read();
                id.Text = rd["id_user"].ToString();
                conn.Close();

            }

            guna2Button5.Enabled = true;
            guna2Button6.Enabled = true;
            guna2Button4.Enabled = false;
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

