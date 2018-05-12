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
using System.Data.SqlClient;

namespace CUAHANGQUANAO
{
    public partial class NCC : Form
    {
        QuanaoDataContext db;
        public NCC()
        {
            InitializeComponent();
            db = new QuanaoDataContext();
        }
        private void NCC_Load(object sender, EventArgs e)
        {
            LoadNCC();
            SetControl(false);
        }
        private void LoadNCC()
        {
            this.dataNhaCC.DataSource = db.NCCs.ToList();
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
            var query = from ncc in db.NCCs
                        where ncc.MaNCC.Contains(txtSearch.Text) || ncc.TenNCC.Contains(txtSearch.Text)
                        select new
                        {
                            MaNCC = ncc.MaNCC,
                            TenNCC = ncc.TenNCC,
                            DC = ncc.DC,
                            SDT = ncc.SDT
                        };
            dataNhaCC.DataSource = query.ToList();
        }
        private bool kiemtra()
        {
            if (string.IsNullOrEmpty(this.txtMaNCC.Text))
            {
                MessageBox.Show(" Vui lòng nhập vào mã nhà cung cấp");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtTenNCC.Text))
            {
                MessageBox.Show("vui lòng nhập vào tên  nhà cung cấp");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtDC.Text))
            {
                MessageBox.Show(" Vui lòng nhập vào địa chỉ");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập vào số điện thoại");
                return false;
            }
            return true;
        }
         private void dataNhaCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
         private void dataNhaCC_CellClick(object sender, DataGridViewCellEventArgs e)
         {
         }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if(kiemtra())
            {
               try
               {
                   //////////tạo mới đối tượng nhà cung cấp
                   NCC ncc = new NCC();
                 
               }
               catch (Exception ex)
               {
                   MessageBox.Show(" Có lỗi xảy ra trong quá trình thêm mới nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
            }
         }
            
            private void txtSDT_TextChanged(object sender, EventArgs e)
            {

            }
            private void txtTenNCC_TextChanged(object sender, EventArgs e)
            {

            }

            private void Input_Enter(object sender, EventArgs e)
            {

            }

        }
    }
               