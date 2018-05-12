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
    public partial class NhomHang : Form
    {
        QuanaoDataContext db;
        public NhomHang()
        {
            InitializeComponent();
            db = new QuanaoDataContext();
        }

        private void NhomHang_Load(object sender, EventArgs e)
        {
            LoadNhomHang();
            SetControl(false);
        }
        private void LoadNhomHang()
        {
            this.dataNhomHang.DataSource = db.NHOMHANGs.ToList();
        }
        private void SetControl(bool an)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            GIAODIEN f2 = new GIAODIEN();
            this.Hide();
            f2.ShowDialog(); 
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            QuanaoDataContext db = new QuanaoDataContext();
            var query = from nhomhang in db.NHOMHANGs
                        where nhomhang.MaNhomHang.Contains(txtTimKiem.Text) || nhomhang.TenNhomHang.Contains(txtTimKiem.Text)
                        select new
                        {
                            MaNhomHang = nhomhang.MaNhomHang,
                            TenNhomHang = nhomhang.TenNhomHang
                        };
            dataNhomHang.DataSource = query.ToList();        
        }
        private bool kiemtra()
        {
            if(string.IsNullOrEmpty(this.txtMaNH.Text))
            {
                MessageBox.Show(" Vui lòng nhập vào mã nhóm hàng");
                return false;
            }
            if(string.IsNullOrEmpty(this.txtTenNH.Text))
            {
                MessageBox.Show(" Vui lòng nhập vào tên nhóm hàng");
                return false;
            }
            return true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (kiemtra())
            {
                try
                {
                    //////////tạp mới đối tượng nhóm hàng
                    NHOMHANG nhomhang = new NHOMHANG();
                    nhomhang.MaNhomHang = this.txtMaNH.Text;
                    nhomhang.TenNhomHang = this.txtTenNH.Text;

                    //// lưu lại vào db
                    db.NHOMHANGs.InsertOnSubmit(nhomhang);
                    db.SubmitChanges();
                    MessageBox.Show("Thêm vào danh sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    /// xóa thông tin  người khi nhập xong
                    XoaNhap();
                    LoadNhomHang();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(" Có lỗi xảy ra trong quá trình thêm mới nhóm hàng","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void XoaNhap()
        {
            this.txtMaNH.Text = "";
            this.txtTenNH.Text = "";
        }
        private void dataNhomHang_CellClick( object sender, DataGridViewCellEventArgs e)
        {
            /// Xử lý sự kiện khi danh sách nhóm hàng được chọn
            int chon = dataNhomHang.SelectedCells[0].RowIndex;
            DataGridViewRow chonRow = dataNhomHang.Rows[chon];

            this.txtMaNH.Text = chonRow.Cells[0].Value.ToString();
            this.txtTenNH.Text = chonRow.Cells[1].Value.ToString();

            SetControl(true);
            this.txtMaNH.Enabled = false;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            /// Sửa thông tin nhóm hàng
            /// Lấy thông tin nhóm hàng
            NHOMHANG nhomhang = db.NHOMHANGs.FirstOrDefault(nh => nh.MaNhomHang == this.txtMaNH.Text);
            nhomhang.TenNhomHang = this.txtTenNH.Text;

            db.SubmitChanges();
            MessageBox.Show(" Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            XoaNhap();
            LoadNhomHang();
            SetControl(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            NHOMHANG nhomhang = db.NHOMHANGs.FirstOrDefault(nh => nh.MaNhomHang == this.txtMaNH.Text);
              
               db.SubmitChanges();
               XoaNhap();
               LoadNhomHang();
               SetControl(true);
               this.btnSearch.Enabled = false;        
        }
      
    }
}

