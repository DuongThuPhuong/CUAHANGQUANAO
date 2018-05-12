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
    public partial class DANGNHAP : Form
    {
        QuanaoDataContext db;
        public DANGNHAP()
        {
           InitializeComponent();
           db = new QuanaoDataContext();
        }

        private void DANGNHAP_Load(object sender, EventArgs e)
        {
          
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            if (kiemtra(textBox1.Text, textBox2.Text))
            {
                GIAODIEN f1 = new GIAODIEN();
                MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                f1.ShowDialog();

            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác");
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                textBox1.Focus();
            }
        }
        private bool kiemtra(string p1, string p2)
        {
            QuanaoDataContext context = new QuanaoDataContext();
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập vào tài khoản");
                return false;
            }
            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập vào Mật khẩu");
                return false;
            }

            var q = from p in context.TAIKHOANs
                    where p.USERNAME == textBox1.Text
                    && p.PASSWORD == textBox2.Text
                    select p;

            if (q.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn có muốn thoát hay không?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (tb == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        }
    }

