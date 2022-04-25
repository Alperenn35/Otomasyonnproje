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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_MUSTERILER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select Ilce from TBL_ILCELER where Sehir=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_MUSTERILER (AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", mskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", mskTC.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", Cmbil.Text);
            komut.Parameters.AddWithValue("@p8", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p9", rchAdres.Text);
            komut.Parameters.AddWithValue("@p10", txtVergi.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataRow dr = gridControl1.DataSource as DataRow;
            txtId.Text = dr["ID"].ToString();
            txtAd.Text = dr["AD"].ToString();
            txtSoyad.Text = dr["SOYAD"].ToString();
            mskTelefon1.Text = dr["TELEFON"].ToString();
            mskTelefon2.Text = dr["TELEFON2"].ToString();
            mskTC.Text = dr["TC"].ToString();
            txtMail.Text = dr["MAIL"].ToString();
            Cmbil.Text = dr["IL"].ToString();
            Cmbilce.Text = dr["ILCE"].ToString();
            txtVergi.Text = dr["VERGIDAIRE"].ToString();
            rchAdres.Text = dr["ADRES"].ToString();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from TBL_MUSTERILER where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_MUSTERILER set AD=@P1,SOYAD=@P2,TELEFON=@P3,TELEFON2=@P4,TC=@P5,MAIL=@P6,IL=@P7,ILCE=@P8,VERGIDAIRE=@P9,ADRES=@P10 where ID=@P11", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", mskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", mskTC.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", Cmbil.Text);
            komut.Parameters.AddWithValue("@p8", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p9", txtVergi.Text);
            komut.Parameters.AddWithValue("@p10", rchAdres.Text);
            komut.Parameters.AddWithValue("@p11", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }
    }
}
