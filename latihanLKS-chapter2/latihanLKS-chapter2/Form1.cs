using Microsoft.SqlServer.Server;
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
    public partial class Form1 : Form
    {
        SqlConnection conn = Connection.Connect();
        SqlCommand cmd;
        SqlDataReader rd;
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM [tbl_user] WHERE username = @username AND password = @password";
            cmd.Parameters.AddWithValue("@username", username.Text);
            cmd.Parameters.AddWithValue("@password", Hash.HashChar(password.Text));
            using(rd =  cmd.ExecuteReader())
            {
                if(rd.HasRows)
                {
                    rd.Read();
                    string tipe = rd["tipe_user"].ToString();
                    int id_user = (int)rd["id_user"];
                    string nama = rd["nama"].ToString();
                    Session.SessionStart(id_user, nama);
                    conn.Close();

                    conn.Open();
                    SqlCommand log = conn.CreateCommand();
                    log.CommandType = System.Data.CommandType.Text;
                    log.CommandText = "INSERT INTO [tbl_log] (id_user, aktifitas) VALUES(@id, 'LOGIN')";
                    log.Parameters.AddWithValue("@id", id_user);
                    
                    
                    log.ExecuteNonQuery();
                    conn.Close() ;

                    MessageBox.Show("Login berhasil!", " SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if(tipe == "Admin")
                    {
                        Admin_log al = new Admin_log();
                        al.Show();
                        this.Hide();
                    }else if(tipe == "Kasir")
                    {
                        Kasir kasir= new Kasir();
                        kasir.Show();
                        this.Hide();
                    }else if(tipe == "Gudang")
                    {
                        Gudang gudang = new Gudang();
                        gudang.Show();
                        this.Hide();
                    }
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("Login gagal!", " FAILED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }
    }
}
