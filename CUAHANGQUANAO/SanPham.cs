using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CUAHANGQUANAO.DataContext;

namespace CUAHANGQUANAO
{
    public partial class SanPham : Form
    {
        QuanaoDataContext db;
        public SanPham()
        {
            InitializeComponent();
            db = new QuanaoDataContext();
        }

        private void SanPham_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            SetControl(false);
        }
        
        private void LoadSanPham()
        {
            this.dataSanPham.DataSource = db.SANPHAMs.ToList();
        }

        private void SetControl( bool an)
        {

        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            GIAODIEN f2=new GIAODIEN();
            this.Hide();
            f2.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
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
            dataSanPham.DataSource = query.ToList();
        }
        private bool kiemtra()
            {
            if(string.IsNullOrEmpty(this.txtMaSP.Text))
            {
                MessageBox.Show(" VUi lòng nhập vào sản phẩm");
                return false;
            }
            if(string.IsNullOrEmpty(this.txtTenSP.Text))
            {
                MessageBox.Show("vui lòng nhập vào tên sản phẩm");
                return false;
            }
            if(string.IsNullOrEmpty(this.txtChungLoai.Text))
            {
                MessageBox.Show(" Vui lòng nhập vào chủng loại");
                return false;
            }
            if(string.IsNullOrEmpty(this.txtDonGia.Text))
            {
                MessageBox.Show("Vui lòng nhập vào đơn giá");
                return false;
            }
            if( string.IsNullOrEmpty(this.txtSoLuong.Text))
            {
                MessageBox.Show(" Vui lòng nhập vào số lượng");
                return false;
            }
            return true;
                 
            }
        private void btnThemq_Click(object sender, EventArgs e)
        {
            if( kiemtra())
            {
                try 
                {
                ///////Tạo mới đối tượng sản phẩm
                    SANPHAM product= new SANPHAM();
                    product.MaSP= this.txtMaSP.Text;
                    product.TenSP=this.txtTenSP.Text;
                    product.ChungLoai=this.txtChungLoai.Text;
                    product.DONGIA=float.Parse(txtDonGia.Text);
                    product.SOLUONG=int.Parse(txtSoLuong.Text);

                    ////luu lai vap db
                    db.SANPHAMs.InsertOnSubmit(product);
                    db.SubmitChanges();
                    MessageBox.Show("Thêm vào danh sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    /// xoa thông tin người khi nhập xong
                    XoaNhap();
                    LoadSanPham();
                }
                catch( Exception ex)
                {
                    MessageBox.Show(" Có lỗi xảy ra trong quá trình thêm mới sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
           private void XoaNhap()
           {
               this.txtMaSP.Text="";
               this.txtTenSP.Text = "";
               this.txtChungLoai.Text = "";
               this.txtDonGia.Text="";
               this.txtSoLuong.Text="";
           }
           private void dataSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
           {
               //xu ly su kien khi danh sach sản phẩm duoc chon
               int chon = dataSanPham.SelectedCells[0].RowIndex;
               DataGridViewRow chonRow = dataSanPham.Rows[chon];

               this.txtMaSP.Text = chonRow.Cells[0].Value.ToString();
               this.txtTenSP.Text = chonRow.Cells[1].Value.ToString();
               this.txtChungLoai.Text = chonRow.Cells[2].Value.ToString();
               this.txtDonGia.Text = chonRow.Cells[3].Value.ToString();
               this.txtSoLuong.Text = chonRow.Cells[4].Value.ToString();

               SetControl(true);
               this.txtMaSP.Enabled = false;

           }
           private void btnSua_Click(object sender, EventArgs e)
           {
              ////Sủa thông tin sản phẩm
              ///Lấy thống tin sản phẩm
               SANPHAM product = db.SANPHAMs.FirstOrDefault(pr => pr.MaSP == this.txtMaSP.Text);
               product.TenSP = this.txtTenSP.Text;
               product.ChungLoai = this.txtChungLoai.Text;
               product.DONGIA = float.Parse(txtDonGia.Text);
               product.SOLUONG = int.Parse(txtSoLuong.Text);



               db.SubmitChanges();
               MessageBox.Show(" Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
               XoaNhap();
               LoadSanPham();
               SetControl(true);
           }

           private void btnXoa_Click(object sender, EventArgs e)
           {
               SANPHAM product = db.SANPHAMs.FirstOrDefault(pr => pr.MaSP == this.txtMaSP.Text);
              
               db.SubmitChanges();
               XoaNhap();
               LoadSanPham();
               SetControl(true);
               this.btnSearch.Enabled = false;
           }

           private void txtMaSP_TextChanged(object sender, EventArgs e)
           {

           }
           private void dataSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
           {

           }

   }
}
       