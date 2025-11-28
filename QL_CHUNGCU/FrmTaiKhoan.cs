using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmTaiKhoan : Form
    {
        QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();
        public FrmTaiKhoan()
        {
            InitializeComponent();
            LoadData();
            cbQuyen.Items.AddRange(new object[] { "Admin", "CuDan" });

            // GẮN SỰ KIỆN CLICK ĐÚNG TÊN DATAGRIDVIEW
            dataGridView_TaiKhoan.CellClick += dataGridView_TaiKhoan_CellClick;
        }

        private void dataGridView_TaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtTenDangNhap.Text =
                dataGridView_TaiKhoan.Rows[e.RowIndex].Cells["TenDangNhap"].Value.ToString();

            txtMatKhau.Text =
                dataGridView_TaiKhoan.Rows[e.RowIndex].Cells["MatKhau"].Value.ToString();

            cbQuyen.Text =
                dataGridView_TaiKhoan.Rows[e.RowIndex].Cells["Quyen"].Value.ToString();

            txtTenDangNhap.Enabled = false;     // không cho phéo đổi tên đăng nhập
        }
        private void LoadData()
        {
            dataGridView_TaiKhoan.DataSource = db.TAIKHOANs
                .Select(t => new { t.TenDangNhap, t.MatKhau, t.Quyen })
                .ToList();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            cbQuyen.SelectedIndex = -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Nhập tên đăng nhập.");
                return;
            }
            if (txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải >= 6 ký tự.");
                return;
            }
            if (string.IsNullOrEmpty(cbQuyen.Text))
            {
                MessageBox.Show("Chọn quyền.");
                return;
            }

            // Kiểm tra tồn tại
            if (db.TAIKHOANs.Find(txtTenDangNhap.Text) != null)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại.");
                return;
            }

            var tk = new TAIKHOAN
            {
                TenDangNhap = txtTenDangNhap.Text,
                MatKhau = txtMatKhau.Text,
                Quyen = cbQuyen.Text
            };

            db.TAIKHOANs.Add(tk);
            db.SaveChanges();
            LoadData();
            MessageBox.Show("Thêm thành công.");
        }
        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            txtTenDangNhap.Text = dataGridView_TaiKhoan.Rows[e.RowIndex].Cells["TenDangNhap"].Value.ToString();
            txtMatKhau.Text = dataGridView_TaiKhoan.Rows[e.RowIndex].Cells["MatKhau"].Value.ToString();
            cbQuyen.Text = dataGridView_TaiKhoan.Rows[e.RowIndex].Cells["Quyen"].Value.ToString();
            txtTenDangNhap.Enabled = false; // tránh đổi PK
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var ten = txtTenDangNhap.Text;
            var tk = db.TAIKHOANs.Find(ten);
            if (tk == null) { MessageBox.Show("Không tìm thấy tài khoản."); return; }

            if (txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải >= 6 ký tự.");
                return;
            }

            tk.MatKhau = txtMatKhau.Text;
            tk.Quyen = cbQuyen.Text;
            db.SaveChanges();
            LoadData();
            MessageBox.Show("Cập nhật thành công.");
        }
    }
}
