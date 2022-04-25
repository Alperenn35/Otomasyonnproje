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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void firmalistesi()
        {
            SqlDataAdapter da= new SqlDataAdapter("Select * From TBL_FIRMALAR", bgl.baglanti());
            DataTable dt= new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();

            sehirlistesi();

            carikodaciklamalar();
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

        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("Select OZELKOD1 from TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader(); // Bir Veri kaynağından gelen verileri sırayla okumak için kullanılan geniş bir nesne kategorisidir.
            while (dr.Read())
            {
                rchAdres.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }


   

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv != null)
            {
                var row = dgv.CurrentRow;
                var id = row?.Cells["ID"].Value?.ToString();
                var ad = row?.Cells["AD"].Value?.ToString();
                var statu = row?.Cells["YETKILISTATU"].Value?.ToString();
                var adsoyad = row?.Cells["YETKILIADSOYAD"].Value?.ToString();
                var tc = row?.Cells["YETKILITC"].Value?.ToString();
                var sektor = row?.Cells["SEKTOR"].Value?.ToString();
                var telefon1 = row?.Cells["TELEFON1"].Value?.ToString();
                var telefon2 = row?.Cells["TELEFON2"].Value?.ToString();
                var telefon3 = row?.Cells["TELEFON3"].Value?.ToString();
                var maıl = row?.Cells["MAIL"].Value?.ToString();
                var fax = row?.Cells["FAX"].Value?.ToString();
                var ıl = row?.Cells["IL"].Value?.ToString();
                var ılce = row?.Cells["ILCE"].Value?.ToString();
                var vergıdaıre = row?.Cells["VERGIDAIRE"].Value?.ToString();
                var adres = row?.Cells["ADRES"].Value?.ToString();
                var ozelkod1 = row?.Cells["OZELKOD1"].Value?.ToString();
                var ozelkod2 = row?.Cells["OZELKOD2"].Value?.ToString();
                var ozelkod3 = row?.Cells["OZELKOD3"].Value?.ToString();

                txtId.Text = id;
                txtAd.Text = ad;
                txtYetkılıGorev.Text = statu;
                txtYetkılı.Text = adsoyad;
                mskTC.Text = tc;
                txtSektor.Text = sektor;
                mskTelefon1.Text = telefon1;
                mskTelefon2.Text = telefon2;
                mskTelefon3.Text = telefon3;
                txtMail.Text = maıl;
                mskFax.Text = fax;
                Cmbil.Text = ıl;
                Cmbilce.Text = ılce;
                txtVergi.Text = vergıdaıre;
                rchAdres.Text = adres;
                txtKod1.Text = ozelkod1;
                txtKod2.Text = ozelkod2;
                txtKod3.Text = ozelkod3;


            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)", bgl.baglanti());



            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtYetkılıGorev.Text);
            komut.Parameters.AddWithValue("@P3", txtYetkılı.Text);
            komut.Parameters.AddWithValue("@P4", mskTC.Text);
            komut.Parameters.AddWithValue("@P5", txtSektor.Text);
            komut.Parameters.AddWithValue("@P6", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", mskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", mskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", txtMail.Text);
            komut.Parameters.AddWithValue("@P10", mskFax.Text);
            komut.Parameters.AddWithValue("@P11", Cmbil.Text);
            komut.Parameters.AddWithValue("@P12", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P13", txtVergi.Text);
            komut.Parameters.AddWithValue("@P14", rchAdres.Text);
            komut.Parameters.AddWithValue("@P15", txtKod1.Text);
            komut.Parameters.AddWithValue("@P16", txtKod2.Text);
            komut.Parameters.AddWithValue("@P17", txtKod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();



        }

        private void groupControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            txtAd.Text = String.Empty;
            txtYetkılıGorev.Text = String.Empty;
            txtYetkılı.Text = String.Empty;
            mskTC.Text = String.Empty;
            txtSektor.Text = String.Empty;
            mskTelefon1.Text = String.Empty;
            mskTelefon2.Text = String.Empty;
            mskTelefon3.Text = String.Empty;
            txtMail.Text = String.Empty;
            mskFax.Text = String.Empty;
            Cmbil.Text = String.Empty;
            Cmbilce.Text = String.Empty;
            txtVergi.Text = String.Empty;
            rchAdres.Text = String.Empty;
            txtKod1.Text = String.Empty;
            txtKod2.Text = String.Empty;
            txtKod3.Text = String.Empty;


        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                Cmbilce.Properties.Items.Clear(); // İlçelerde daha önce seçilmiş olan ilçeleri temizle.
                SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    Cmbilce.Properties.Items.Add(dr[0]);
                }
                bgl.baglanti().Close();
            }
        }

        private void groupControl7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_FIRMALAR where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            firmalistesi();
            MessageBox.Show("Firma listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);



        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_FIRMALAR set AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,YETKILITC=@P4,SEKTOR=@P5,TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8,MAIL=@P9,IL=@P11,ILCE=@P12,FAX=@P10,VERGIDAIRE=@P13,ADRES=@P14," +
                "OZELKOD1=@P15,OZELKOD2=@P16,OZELKOD3=@P17 WHERE ID=@P18", bgl.baglanti());


            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtYetkılıGorev.Text);
            komut.Parameters.AddWithValue("@P3", txtYetkılı.Text);
            komut.Parameters.AddWithValue("@P4", mskTC.Text);
            komut.Parameters.AddWithValue("@P5", txtSektor.Text);
            komut.Parameters.AddWithValue("@P6", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", mskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", mskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", txtMail.Text);
            komut.Parameters.AddWithValue("@P10", mskFax.Text);
            komut.Parameters.AddWithValue("@P11", Cmbil.Text);
            komut.Parameters.AddWithValue("@P12", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P13", txtVergi.Text);
            komut.Parameters.AddWithValue("@P14", rchAdres.Text);
            komut.Parameters.AddWithValue("@P15", txtKod1.Text);
            komut.Parameters.AddWithValue("@P16", txtKod2.Text);
            komut.Parameters.AddWithValue("@P17", txtKod3.Text);
            komut.Parameters.AddWithValue("@P18", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistesi();
        }

        
    }
}
