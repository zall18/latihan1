using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace latihanLKS_chapter2
{
    public partial class Kasir : Form
    {
        SqlConnection conn = Connection.Connect();
        SqlCommand cmd;
        SqlDataAdapter dr;
        SqlDataReader rd;
        DataTable dt;

        public void menu()
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM [tbl_barang]";
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                menu_barang.Items.Add(rd["kode_barang"].ToString() + " - " + rd["nama_barang"].ToString());
                kode.Text = rd["kode_barang"].ToString();
            }
            conn.Close();
        }

        public void tabel_load()
        {
            dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.Int32"));
            dt.Columns.Add("Kode_brang", Type.GetType("System.String"));
            dt.Columns.Add("Nama_barang", Type.GetType("System.String"));
            dt.Columns.Add("Harga_satuan", Type.GetType("System.Int32"));
            dt.Columns.Add("Quantitas", Type.GetType("System.Int32"));
            dt.Columns.Add("Subtotal", Type.GetType("System.String"));
            guna2DataGridView1.DataSource = dt;
        }

        public Kasir()
        {
            InitializeComponent();
            nama_kasir.Text = Session.nama_user;
            menu();
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void menu_barang_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            string kode = menu_barang.Text.Split('-')[0];
            cmd.CommandText = "SELECT * FROM [tbl_barang] WHERE kode_barang = @kode";
            cmd.Parameters.AddWithValue("@kode", kode);
            rd = cmd.ExecuteReader();
            rd.Read();
            harga.Text = rd["harga_satuan"].ToString();
            id.Text = rd["id_barang"].ToString();
            conn.Close();
        }

        private void quan_TextChanged(object sender, EventArgs e)
        {
            int hb = int.Parse(harga.Text);
            int q;
            if(int.TryParse(quan.Text, out q))
            {
                int total = hb * q;
                th.Text = total.ToString();
            }

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT INTO [tbl_transaksi] VALUES (@no, @tgl, @nama, @tb, @iu, @ib)";
            cmd.Parameters.AddWithValue("@no", id_create.No_transaksi());
            cmd.Parameters.AddWithValue("@tgl", DateTime.Now);
            cmd.Parameters.AddWithValue("@nama", nama_kasir.Text);
            cmd.Parameters.AddWithValue("@tb", th.Text);
            cmd.Parameters.AddWithValue("@iu", Session.id_user);
            cmd.Parameters.AddWithValue("@ib", id.Text);
            cmd.ExecuteNonQuery();
            conn.Close();

            dt.Columns["ID"].AutoIncrement = true;
            dt.Columns["ID"].AutoIncrementSeed = 1;
            dt.Columns["ID"].AutoIncrementStep = 1;
            dt.Rows.Add(null, kode.Text, menu_barang.Text, harga.Text, quan.Text, th.Text);


            decimal total = 0;
            foreach(DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    decimal subtotal = Convert.ToDecimal(row.Cells["subtotal"].Value);
                    total += subtotal;
                }
            }

            th.Text = null;
            quan.Text = null;
            tb.Text = total.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int total = int.Parse(tb.Text);
            int bayar;

            if(int.TryParse(ub.Text, out bayar))
            {
                if(bayar < total)
                {
                    MessageBox.Show("Pembayaran Kurang!", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ub.Text = null;
                }
                else
                {
                    MessageBox.Show("Pembayaran Berhasil!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int kembali = bayar - total;
                    uk.Text = kembali.ToString();

                    tb.Text = "-";
                    ub.Text = null;
                }
            }


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Struk Pembelian", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(20, 10));
            e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(20, 35));
            e.Graphics.DrawString("Nama Kasir: Kinkin Jaenardi", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(120, 35));
            e.Graphics.DrawString("---------------------------------------------------------", new Font("Arial", 10), Brushes.Black, new Point(20, 50));
            e.Graphics.DrawString("Barang ", new Font("Arial", 10), Brushes.Black, new Point(30, 70));
            e.Graphics.DrawString("Harga ", new Font("Arial", 10), Brushes.Black, new Point(180, 70));
            int x = 20;
            int y = 90;
            for (int row = 0; row < guna2DataGridView1.Rows.Count; row++)
            {
                if (guna2DataGridView1.Rows[row].Cells["Nama_barang"].Value != null && guna2DataGridView1.Rows[row].Cells["Subtotal"].Value != null)
                {
                    string barang = guna2DataGridView1.Rows[row].Cells["Nama_barang"].Value.ToString();
                    string harga = guna2DataGridView1.Rows[row].Cells["Subtotal"].Value.ToString();
                    e.Graphics.DrawString(barang, new Font("Arial", 10), Brushes.Black, new Point(x, y));
                    e.Graphics.DrawString("Rp. " + harga, new Font("Arial", 10), Brushes.Black, new Point(x + 150, y));

                    y += 20;
                }
            }
            e.Graphics.DrawString("__________________", new Font("Arial", 10), Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("Total Harga", new Font("Arial", 10), Brushes.Black, new Point(x, y + 20)); ;
            e.Graphics.DrawString("Rp. " + tb.Text, new Font("Arial", 10), Brushes.Black, new Point(x + 160, y + 20));

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void Kasir_Load(object sender, EventArgs e)
        {
            tabel_load();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            print p = new print();
            p.Show();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Columns.Clear();
            tabel_load();
            uk.Text = "-";
        }
    }
}
