using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityProjeUygulama
{
    public partial class FrmUrun : Form
    {
        public FrmUrun()
        {
            InitializeComponent();
        }

        DbEntityUrunEntities db = new DbEntityUrunEntities();

        private void BtnListele_Click(object sender, EventArgs e)
        {
            //listeleme butonunun kodları
            dataGridView1.DataSource = (from x in db.TBLURUN
                                        select new
                                        {
                                            x.URUNID,
                                            x.URUNAD,
                                            x.MARKA,
                                            x.STOK,
                                            x.FIYAT,
                                            x.TBLKATEGORI.AD,
                                            x.DURUM
                                        }).ToList();
        }

        private void FrmUrun_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (from x in db.TBLURUN
                                        select new
                                        {
                                            x.URUNID,
                                            x.URUNAD,
                                            x.MARKA,
                                            x.STOK,
                                            x.FIYAT,
                                            x.TBLKATEGORI.AD,
                                            x.DURUM
                                        }).ToList();
            BtnEkle.Enabled = true;
            BtnGuncelle.Enabled = false;
            BtnSil.Enabled = false;
            BtnTemizle.Enabled = false;
            BtnListele.Enabled = true;

            //kategorilerdeki comboboxa numara değil de ismne kategorileri getiren kısım
            var kategoriler = (from x in db.TBLKATEGORI
                               select new
                               {
                                   x.ID,
                                   x.AD
                               }
                               ).ToList();
            CmbUrunKategori.ValueMember = "ID";
            CmbUrunKategori.DisplayMember = "AD";
            CmbUrunKategori.DataSource = kategoriler;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            TBLURUN t = new TBLURUN();
            t.URUNAD = TxtUrunAd.Text;
            t.MARKA = TxtUrunMarka.Text;
            t.STOK = short.Parse(TxtUrunStok.Text);
            t.KATEGORI = int.Parse(CmbUrunKategori.SelectedValue.ToString());
            t.FIYAT = decimal.Parse(TxtUrunFiyat.Text);
            t.DURUM = true;
            db.TBLURUN.Add(t);
            db.SaveChanges();
            dataGridView1.DataSource = (from x in db.TBLURUN
                                        select new
                                        {
                                            x.URUNID,
                                            x.URUNAD,
                                            x.MARKA,
                                            x.STOK,
                                            x.FIYAT,
                                            x.TBLKATEGORI.AD,
                                            x.DURUM
                                        }).ToList();
            MessageBox.Show("Yeni ürün eklendi.", "Başarılı", MessageBoxButtons.OK);
            TxtUrunAd.Text = "";
            TxtUrunDurum.Text = "";
            TxtUrunFiyat.Text = "";
            TxtUrunID.Text = "";
            TxtUrunMarka.Text = "";
            TxtUrunStok.Text = "";
            CmbUrunKategori.Text = "";
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int y = Convert.ToInt32(TxtUrunID.Text);
            var urun = db.TBLURUN.Find(y);
            db.TBLURUN.Remove(urun);
            db.SaveChanges();
            dataGridView1.DataSource = (from x in db.TBLURUN
                                        select new
                                        {
                                            x.URUNID,
                                            x.URUNAD,
                                            x.MARKA,
                                            x.STOK,
                                            x.FIYAT,
                                            x.TBLKATEGORI.AD,
                                            x.DURUM
                                        }).ToList();
            MessageBox.Show("Ürün silindi.", "Başarılı", MessageBoxButtons.OK);
            TxtUrunAd.Text = "";
            TxtUrunDurum.Text = "";
            TxtUrunFiyat.Text = "";
            TxtUrunID.Text = "";
            TxtUrunMarka.Text = "";
            TxtUrunStok.Text = "";
            CmbUrunKategori.Text = "";
            BtnEkle.Enabled = true;
            BtnGuncelle.Enabled = false;
            BtnSil.Enabled = false;
            BtnTemizle.Enabled = false;
            BtnListele.Enabled = true;

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            TxtUrunAd.Text = "";
            TxtUrunDurum.Text = "";
            TxtUrunFiyat.Text = "";
            TxtUrunID.Text = "";
            TxtUrunMarka.Text = "";
            TxtUrunStok.Text = "";
            CmbUrunKategori.Text = "";
            BtnEkle.Enabled = true;
            BtnGuncelle.Enabled = false;
            BtnSil.Enabled = false;
            BtnTemizle.Enabled = false;
            BtnListele.Enabled = true;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtUrunID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtUrunAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TxtUrunMarka.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            TxtUrunStok.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TxtUrunFiyat.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            TxtUrunDurum.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            CmbUrunKategori.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();


            BtnEkle.Enabled = false;
            BtnGuncelle.Enabled = true;
            BtnSil.Enabled = true;
            BtnTemizle.Enabled = true;
            BtnListele.Enabled = true;
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int y = Convert.ToInt32(TxtUrunID.Text);
            var urun = db.TBLURUN.Find(y);
            urun.URUNAD = TxtUrunAd.Text;
            urun.MARKA = TxtUrunMarka.Text;
            urun.STOK = short.Parse(TxtUrunStok.Text);
            urun.FIYAT = decimal.Parse(TxtUrunFiyat.Text);
            urun.KATEGORI = int.Parse(CmbUrunKategori.SelectedValue.ToString());
            urun.DURUM = true;
            db.SaveChanges();

            dataGridView1.DataSource = (from x in db.TBLURUN
                                        select new
                                        {
                                            x.URUNID,
                                            x.URUNAD,
                                            x.MARKA,
                                            x.STOK,
                                            x.FIYAT,
                                            x.TBLKATEGORI.AD,
                                            x.DURUM
                                        }).ToList();
            MessageBox.Show("Ürün güncellendi.", "Başarılı", MessageBoxButtons.OK);
            TxtUrunAd.Text = "";
            TxtUrunDurum.Text = "";
            TxtUrunFiyat.Text = "";
            TxtUrunID.Text = "";
            TxtUrunMarka.Text = "";
            TxtUrunStok.Text = "";
            CmbUrunKategori.Text = "";
            BtnEkle.Enabled = true;
            BtnGuncelle.Enabled = false;
            BtnSil.Enabled = false;
            BtnTemizle.Enabled = false;
            BtnListele.Enabled = true;
        }
    }
}
