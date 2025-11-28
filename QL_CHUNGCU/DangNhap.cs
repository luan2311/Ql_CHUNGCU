using DoAnHCSDL;
using QL_CHUNGCU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form_DangNhap_DangKy
{

    public partial class DangNhap : Form
    {
        QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();
        public DangNhap()
        {
            InitializeComponent();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            textBox_NhapMatKhau_DangNhap.PasswordChar = '●';
        }

        private void button_DangNhap_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ controls
            string tenDN = textBox_TenDangNhap_DangNhap.Text.Trim();
            string matKhau = textBox_NhapMatKhau_DangNhap.Text;
            if (string.IsNullOrEmpty(tenDN) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Truy vấn Entity Framework để tìm tài khoản
            var taiKhoan = db.TAIKHOANs
                             .FirstOrDefault(tk => tk.TenDangNhap == tenDN && tk.MatKhau == matKhau);

            if (taiKhoan != null)
            {
                MessageBox.Show($"Đăng nhập thành công! Chào mừng {taiKhoan.TenDangNhap}.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở dashboard theo quyền
                if (taiKhoan.Quyen == "Admin")
                {
                    FrmDashboardAdmin frmAdmin = new FrmDashboardAdmin(taiKhoan.Quyen);
                    frmAdmin.Show();
                }
                else // User
                {
                    FrmDashboardCuDan frmUser = new FrmDashboardCuDan(taiKhoan.Quyen);
                    frmUser.Show();
                }

                // Ẩn form đăng nhập
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void linkLabel_DangKyTK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    DangKy dangKy = new DangKy();
        //    dangKy.ShowDialog();
        //}

        //private void linkLabel_QuenMK_DangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    QuenMatKhau quenMatKhau = new QuenMatKhau();
        //    quenMatKhau.ShowDialog();
        //}

        //private void linkLabel_DoiMK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    DoiMatKhau doiMatKhau = new DoiMatKhau();
        //    doiMatKhau.ShowDialog();
        //}
    }
}
