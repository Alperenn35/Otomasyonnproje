using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OtomasyonProje
{
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void personelliste()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_PERSONELLER", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader(); // Datadaki verileri oku ve çalıştır.
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]); // 0. Index'ten itibaren illeri ekle.
            }
            bgl.baglanti().Close();
        }



        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            personelliste();

            sehirlistesi();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                Cmbilce.Properties.Items.Clear(); // İlçelerde daha önce seçilmiş olan ilçeleri temizle.
                SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", Cmbil.SelectedIndex + 1);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    Cmbilce.Properties.Items.Add(dr[0]);
                }
                bgl.baglanti().Close();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_PERSONELLER (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", bgl.baglanti());

            komut.Parameters.AddWithValue("@P1", txtAd.Text); // Parametreyi Değer Olarak Ekle.
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@P4", mskTC.Text);
            komut.Parameters.AddWithValue("@P5", txtMail.Text);
            komut.Parameters.AddWithValue("@P6", Cmbil.Text);
            komut.Parameters.AddWithValue("@P7", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P8", rchAdres.Text);
            komut.Parameters.AddWithValue("@P9", txtGorev.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Bilgileri Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelliste();


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = sender as DataGridView;  //DataGridView'deki Verilere Tıklandığında Textbox'larda otomatik görünmesini sağlar.
            if (dgv != null)
            {
                var row = dgv.CurrentRow;
                var id = row?.Cells["ID"].Value?.ToString();
                var ad = row?.Cells["AD"].Value?.ToString();
                var soyad = row?.Cells["SOYAD"].Value?.ToString();
                var telefon = row?.Cells["TELEFON"].Value?.ToString();
                var tc = row?.Cells["TC"].Value?.ToString();
                var maıl = row?.Cells["MAIL"].Value?.ToString();
                var ıl = row?.Cells["IL"].Value?.ToString();
                var ılce = row?.Cells["ILCE"].Value?.ToString();
                var adres = row?.Cells["ADRES"].Value?.ToString();
                var gorev = row?.Cells["GOREV"].Value?.ToString();

                txtId.Text = id;
                txtAd.Text = ad;
                txtSoyad.Text = soyad;
                mskTelefon1.Text = telefon;
                mskTC.Text = tc;
                txtMail.Text = maıl;
                Cmbil.Text = ıl;
                Cmbilce.Text = ılce;
                rchAdres.Text = adres;
                txtGorev.Text = gorev;
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            {
                txtId.Text = String.Empty;
                txtAd.Text = String.Empty;
                txtSoyad.Text = String.Empty;
                mskTelefon1.Text = String.Empty;
                mskTC.Text = String.Empty;
                txtMail.Text = String.Empty;
                Cmbil.Text = String.Empty;
                Cmbilce.Text = String.Empty;
                rchAdres.Text = String.Empty;
                txtGorev.Text = String.Empty;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_PERSONELLER where ID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtId.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Müşteri Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.None);
                personelliste();
            }

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_PERSONELLER set AD=@P1,SOYAD=@P2,TELEFON=@P3,TC=@P4,MAIL=@P5,IL=@P6,ILCE=@P7,ADRES=@P8,GOREV=@P9 WHERE ID=@P10", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@P4", mskTC.Text);
            komut.Parameters.AddWithValue("@P5", txtMail.Text);
            komut.Parameters.AddWithValue("@P6", Cmbil.Text);
            komut.Parameters.AddWithValue("@P7", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P8", rchAdres.Text);
            komut.Parameters.AddWithValue("@P9", txtGorev.Text);
            komut.Parameters.AddWithValue("@P10", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            personelliste();
        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }
    }
}
