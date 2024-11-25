using MySql.Data.MySqlClient;
using System;
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
    public partial class Cekout : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;
        private List<string> mahasiswaIds;
        private List<CheckBox> checkBoxes = new List<CheckBox>();
        private Dictionary<CheckBox, int> hargaDictionary = new Dictionary<CheckBox, int>();
        public int idTransaksi;

        public Cekout(int trans)
        {
            alamat = "server=localhost; database=db_bubble; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
            idTransaksi= trans;
        }

        private void Cekout_Load(object sender, EventArgs e)
        {
            try
            {
                // Membuka koneksi ke database
                koneksi.Open();
                // Query untuk mengambil data mahasiswa

                MySqlCommand cmd = new MySqlCommand("SELECT nama, id, harga, username FROM tbl_cucibersih", koneksi);
                MySqlDataReader reader = cmd.ExecuteReader();

                int posY = 214; // Posisi Y awal untuk setiap baris data
                int margin = 20; // Jarak antar baris data

                while (reader.Read())
                {
                    //mahasiswaIds.Add(reader["id"].ToString());
                    // Membuat label untuk Nama
                    Label labelNama = new Label();
                    labelNama.Text = reader["id"].ToString();
                    labelNama.Location = new Point(106, posY);
                    labelNama.AutoSize = true;
                    this.Controls.Add(labelNama);

                    // Membuat label untuk Nama
                    Label labelUmur = new Label();
                    labelUmur.Text = reader["nama"].ToString();
                    labelUmur.Location = new Point(151, posY);
                    labelUmur.AutoSize = true;
                    this.Controls.Add(labelUmur);

                    // Membuat label untuk harga
                    Label jkerja = new Label();
                    jkerja.Text = reader["harga"].ToString();
                    jkerja.Location = new Point(233, posY);
                    jkerja.AutoSize = true;
                    this.Controls.Add(jkerja);

                    // Membuat CheckBox untuk memilih item
                    CheckBox checkBox = new CheckBox();
                    checkBox.Location = new Point(289, posY);
                    checkBox.AutoSize = true;
                    this.Controls.Add(checkBox);

                    checkBoxes.Add(checkBox); // Menyimpan checkbox ke list
                    hargaDictionary[checkBox] = int.Parse(reader["harga"].ToString()); // Menyimpan harga sesuai dengan checkbox

                    // Memperbarui posisi Y untuk baris data berikutnya
                    posY += margin;
                }

                reader.Close();
                koneksi.Close();

                // Membuat tombol "Submit" setelah data selesai ditampilkan
                Button btnSubmit = new Button();
                btnSubmit.Text = "Submit";
                btnSubmit.Size = new Size(100, 30);
                btnSubmit.Location = new Point(20, posY + 20); // Letakkan di bawah data yang terakhir ditampilkan
                btnSubmit.Click += new EventHandler(BtnSubmit_Click);
                this.Controls.Add(btnSubmit);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }

        }

        private void back_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void btnstruc_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open(); // Membuka koneksi ke database
                int totalHarga = 0;
                string idTransaksi = ""; // Menyimpan ID transaksi

                foreach (var checkBox in checkBoxes)
                {
                    if (checkBox.Checked)
                    {
                        // Mendapatkan ID dari label yang terkait dengan checkbox
                        Control labelIdControl = this.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Location.Y == checkBox.Location.Y && lbl.Location.X == 106);
                        idTransaksi = labelIdControl?.Text;

                        if (!string.IsNullOrEmpty(idTransaksi))
                        {
                            // Tambahkan operasi untuk memproses transaksi
                            totalHarga += hargaDictionary[checkBox];
                        }
                    }
                }

                koneksi.Close(); // Menutup koneksi ke database

                if (!string.IsNullOrEmpty(idTransaksi))
                {
                    // Pindahkan ID transaksi ke form Nota

                    MessageBox.Show("Test");
                }
                else
                {
                    MessageBox.Show("Tidak ada transaksi yang dipilih!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open(); // Membuka koneksi ke database
                int totalHarga = 0;

                // Daftar kontrol yang akan dihapus dari UI
                List<Control> controlsToRemove = new List<Control>();

                foreach (var checkBox in checkBoxes)
                {
                    if (checkBox.Checked)
                    {
                        // Mendapatkan ID dari label yang terkait dengan checkbox
                        int index = checkBoxes.IndexOf(checkBox); // Mendapatkan indeks checkbox
                        Control labelIdControl = this.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Location.Y == checkBox.Location.Y && lbl.Location.X == 106);
                        string id = labelIdControl?.Text;

                        if (!string.IsNullOrEmpty(id))
                        {
                            // Menghapus data dari database berdasarkan ID
                            string deleteQuery = "DELETE FROM tbl_cucibersih WHERE id = @id";
                            MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, koneksi);
                            deleteCmd.Parameters.AddWithValue("@id", id);
                            deleteCmd.ExecuteNonQuery();

                            // Tambahkan harga ke total
                            totalHarga += hargaDictionary[checkBox];

                            lblHarga.Text = Convert.ToString(totalHarga);

                            // Tandai kontrol untuk dihapus dari UI
                            controlsToRemove.Add(checkBox);
                            controlsToRemove.Add(labelIdControl);
                        }
                    }
                }

                // Hapus kontrol dari UI
                foreach (var control in controlsToRemove)
                {
                    this.Controls.Remove(control);
                }

                // Perbarui daftar checkbox
                checkBoxes.RemoveAll(cb => cb.Checked);

                // Tampilkan total harga
                MessageBox.Show("Total harga: " + totalHarga);

                koneksi.Close(); // Menutup koneksi ke database
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }
    }
}
