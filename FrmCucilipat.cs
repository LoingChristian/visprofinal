﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;

namespace visprofinalproject
{
    public partial class FrmCucilipat : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;
        public FrmCucilipat()
        {
            alamat = "server=localhost; database=db_bubble; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmCustom1 frmCustom1 = new FrmCustom1();
            frmCustom1.Show();
            this.Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Inputan.Text != "" && txtUsername.Text != "")
                {
                    int harga = Int32.Parse(Inputan.Text) * 7000;
                    Random rnd = new Random();
                    int id_transaksi = rnd.Next(1000, 10000);
                    query = string.Format("insert into tbl_cucibersih (id, nama, total, username, harga, status)  values ('{0}','{1}', '{2}', '{3}', '{4}', '{5}');", id_transaksi, "Cuci lipat", Inputan.Text, txtUsername.Text, harga, "In Queue");


                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        Nota frmNota = new Nota(id_transaksi);
                        frmNota.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("Gagal inser Data . . . ");
                    }
                }
                else
                {
                    MessageBox.Show("Data Tidak lengkap !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_Click(object sender, EventArgs e)
        {

        }

        private void Inputan_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmCucilipat_Load(object sender, EventArgs e)
        {
            try
            {
                if (Inputan.Text != "" && txtUsername.Text != "")
                {
                    int harga = Int32.Parse(Inputan.Text) * 7000;
                    Random rnd = new Random();
                    int id_transaksi = rnd.Next(1000, 10000);
                    query = string.Format("insert into tbl_cucibersih (id, nama, total, username, harga, status)  values ('{0}','{1}', '{2}', '{3}', '{4}', '{5}');", id_transaksi, "Cuci Bersih", Inputan.Text, txtUsername.Text, harga, "In Queue");


                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        Nota frmNota = new Nota(id_transaksi);
                        frmNota.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("Gagal inser Data . . . ");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
