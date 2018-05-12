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
using CUAHANGQUANAO.DataContext;

namespace CUAHANGQUANAO
{
    public partial class KhachHang : Form
    {
        QuanaoDataContext db;
        public KhachHang()
        {
            InitializeComponent();
            db = new QuanaoDataContext();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            LoadKhachHang();
            SetControl(false);
        }
        private void LoadKhachHang()
        {
            this.dataKhachhang.DataSource = db.KHACHHANGs.ToList();
        }
        private bool SetControl(bool an)
        {
            return true;
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
            var query = from customer in db.KHACHHANGs
                        where customer.MaKH.Contains(txtSearch.Text) || customer.TenKH.Contains(txtSearch.Text) || customer.GT.Contains(txtSearch.Text)
                        select new
                        {
                            MaKH = customer.MaKH,
                            TenKH = customer.TenKH,
                            NS = customer.NS,
                            GT = customer.GT,
                            Sdt = customer.Sdt,
                            Email = customer.Email

                        };
            dataKhachhang.DataSource = query.ToList();
        }
        private bool kiemtra()
        {
            if (string.IsNullOrEmpty(this.txtMaKH.Text))
            {
                MessageBox.Show(" VUi lòng nhập vào mã khách hàng");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtTenKH.Text))
            {
                MessageBox.Show("vui lòng nhập vào tên khách hàng");
                return false;
            }
            if (string.IsNullOrEmpty(this.dateTimePicker1.Text))
            {
                MessageBox.Show(" Vui lòng nhập vào ngày sinh");
                return false;
            }
            if (string.IsNullOrEmpty(this.cbxGT.Text))
            {
                MessageBox.Show("Vui lòng nhập vào giới tính");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập vào số điện thoại");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtMail.Text))
            {
                MessageBox.Show("Vui lòng nhập vào số Email");
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
                     /////tạo mới dối tượng khách hàng
            KHACHHANG customer = new KHACHHANG();
            customer.MaKH = this.txtMaKH.Text;
            customer.TenKH = this.txtTenKH.Text;
            customer.NS = this.dateTimePicker1.Value.Date;
            customer.GT = this.cbxGT.Text;
            customer.Sdt = this.txtSDT.Text;
            customer.Email = this.txtMail.Text;

            //// lưu lại vào db
            db.KHACHHANGs.InsertOnSubmit(customer);
            db.SubmitChanges();
            MessageBox.Show(" Thêm vào danh sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            /// xóa thông tin nguời khi nhập xong
            XoaNhap();
            LoadKhachHang();

                }
                catch
                {
                    MessageBox.Show(" Có lỗi xảy ra trong quá trình thêm mới khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void XoaNhap()
        {
            this.txtMaKH.Text = "";
            this.txtTenKH.Text = "";
            this.dateTimePicker1.Text = "";
            this.cbxGT.Text = "";
            this.txtSDT.Text = "";
            this.txtMail.Text = "";
        }

        private void dataKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //// Xử lý sự kiện khi danh sách khách hàng được chọn
            int chon = dataKhachhang.SelectedCells[0].RowIndex;
            DataGridViewRow chonRow = dataKhachhang.Rows[chon];

            this.txtMaKH.Text = chonRow.Cells[0].Value.ToString();
            this.txtTenKH.Text = chonRow.Cells[1].Value.ToString();
            this.dateTimePicker1.Text = chonRow.Cells[2].Value.ToString();
            this.cbxGT.Text = chonRow.Cells[3].Value.ToString();
            this.txtSDT.Text = chonRow.Cells[4].Value.ToString();
            this.txtMail.Text = chonRow.Cells[5].Value.ToString();

            SetControl(true);
            this.txtMaKH.Enabled = false;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            ////Lấy thông tin khách hàng
            KHACHHANG customer = db.KHACHHANGs.FirstOrDefault(ctr => ctr.MaKH == this.txtMaKH.Text);     
            customer.TenKH = this.txtTenKH.Text;
            customer.NS = this.dateTimePicker1.Value.Date;
            customer.GT = this.cbxGT.Text;
            customer.Sdt = this.txtSDT.Text;
            customer.Email = this.txtMail.Text;

            db.SubmitChanges();
            MessageBox.Show(" Sửa thành công","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            XoaNhap();
            LoadKhachHang();
            SetControl(true);

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            KHACHHANG customer = db.KHACHHANGs.FirstOrDefault(ctr => ctr.MaKH == this.txtMaKH.Text);
            db.KHACHHANGs.DeleteOnSubmit(customer);

            db.SubmitChanges();
            XoaNhap();
            MessageBox.Show("Xóa thành công");
            LoadKhachHang();
            SetControl(true);

            this.btnSearch.Enabled = false;
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        }
       /* private void dataKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }*/
 }