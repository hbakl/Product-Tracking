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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DbEntityUrunEntities db = new DbEntityUrunEntities();

        private void BtnListele_Click(object sender, EventArgs e)
        {
            var komut = db.TBLKATEGORI.ToList();
            dataGridView1.DataSource = komut;
            BtnGuncelle.Enabled = false;
            BtnSil.Enabled = false;
            BtnEkle.Enabled = true;
            TxtAd.Text = "";
            TxtId.Text = "";
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            //veritabanına txt ad ile yeni kategori oluşturan kod
            TBLKATEGORI t = new TBLKATEGORI();
            t.AD = TxtAd.Text;
            db.TBLKATEGORI.Add(t);
            db.SaveChanges();

            //ekleme sonrası dgv'i güncelleyen kod
            var komut = db.TBLKATEGORI.ToList();
            dataGridView1.DataSource = komut;
            MessageBox.Show("Kategori eklendi.", "Başarılı", MessageBoxButtons.OK);
            TxtAd.Text = "";
            TxtId.Text = "";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var komut = db.TBLKATEGORI.ToList();
            dataGridView1.DataSource = komut;
            BtnGuncelle.Enabled = false;
            BtnSil.Enabled = false;
            BtnEkle.Enabled = true;
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {

            //veritabanından txt id'ye göre kayıt silen kod
            int x = Convert.ToInt32(TxtId.Text);
            var ktgr = db.TBLKATEGORI.Find(x);
            db.TBLKATEGORI.Remove(ktgr);
            db.SaveChanges();

            //silme sonrası dgv'i güncelleyen kod
            var komut = db.TBLKATEGORI.ToList();
            dataGridView1.DataSource = komut;
            MessageBox.Show("Kategori silindi.", "Başarılı", MessageBoxButtons.OK);
            BtnGuncelle.Enabled = false;
            BtnSil.Enabled = false;
            BtnEkle.Enabled = true;
            TxtAd.Text = "";
            TxtId.Text = "";

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            BtnGuncelle.Enabled = true;
            BtnSil.Enabled = true;
            BtnEkle.Enabled = false;
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(TxtId.Text);
            var ktgr = db.TBLKATEGORI.Find(x);
            ktgr.AD = TxtAd.Text;
            db.SaveChanges();

            //silme sonrası dgv'i güncelleyen kod
            var komut = db.TBLKATEGORI.ToList();
            dataGridView1.DataSource = komut;
            MessageBox.Show("Kategori güncellendi.", "Başarılı", MessageBoxButtons.OK);
            BtnGuncelle.Enabled = false;
            BtnSil.Enabled = false;
            BtnEkle.Enabled = true;
            TxtAd.Text = "";
            TxtId.Text = "";
        }
    }
}
