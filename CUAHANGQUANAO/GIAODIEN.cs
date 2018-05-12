using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CUAHANGQUANAO.DataContext;

namespace CUAHANGQUANAO
{
    public partial class GIAODIEN : Form
    {
        QuanaoDataContext db;
        public GIAODIEN()
        {
            InitializeComponent();
            db= new QuanaoDataContext();
        }

        private void GIAODIEN_Load(object sender, EventArgs e)
        {
            LoadSanPham();
        }
        private void LoadSanPham()
        {
            this.dataGiaodien.DataSource = db.SANPHAMs.ToList();
        }
        private void đĂNGNHẬPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DANGNHAP f1 = new DANGNHAP();
            f1.ShowDialog();
            this.Hide();
        }

        private void tHOÁTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            SanPham f3 = new SanPham();
            f3.ShowDialog();
            this.Hide();
        }
      private void btnKhachhang_Click(object sender, EventArgs e)
       {
           KhachHang f4 = new KhachHang();
           f4.ShowDialog();
           this.Hide();
       }

       private void btnHoaDon_Click(object sender, EventArgs e)
       {
           HoaDon f5 = new HoaDon();
           f5.ShowDialog();
           this.Hide();
       }

       private void btnNhanVien_Click(object sender, EventArgs e)
       {
           NhanVien f6 = new NhanVien();
           f6.ShowDialog();
           this.Hide();
       }

       private void btnNCC_Click(object sender, EventArgs e)
       {
           NCC f7 = new NCC();
           f7.ShowDialog();
           this.Hide();
       }

       private void btnPNhap_Click(object sender, EventArgs e)
       {
           PhieuNhap f8 = new PhieuNhap();
           f8.ShowDialog();
           this.Hide();
       }

       private void btnNhomHang_Click(object sender, EventArgs e)
       {
           NhomHang f9 = new NhomHang();
           f9.ShowDialog();
           this.Hide();
       }
       
        private void dataGiaodien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuanaoDataContext db = new QuanaoDataContext();
            var query = from product in db.SANPHAMs
                        where product.MaSP.Contains(txtTimKiem.Text) || product.TenSP.Contains(txtTimKiem.Text) || product.ChungLoai.Contains(txtTimKiem.Text)
                        select new
                        {
                            MaSP = product.MaSP,
                            TenSP = product.TenSP,
                            ChungLoai = product.ChungLoai,
                            DONGIA = product.DONGIA,
                            SOLUONG = product.SOLUONG
                        };
                       dataGiaodien.DataSource= query.ToList();
        }
    }
}
