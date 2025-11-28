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
    public partial class FrmNhanVien : Form
    {
        QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();
        public FrmNhanVien()
        {
            InitializeComponent();
            LoadData();
            LoadFilterBoxes();

            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
        }
       

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;  // tránh click tiêu đề cột

            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

            // Đổ dữ liệu lên các textbox - kiểm tra null trước khi gán
            txtMaNV.Text = row.Cells["MaNV"].Value?.ToString() ?? "";
            txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
            txtChucVu.Text = row.Cells["ChucVu"].Value?.ToString() ?? "";
            txtPhongBan.Text = row.Cells["PhongBan"].Value?.ToString() ?? "";
            txtSDT.Text = row.Cells["SDT"].Value?.ToString() ?? "";
            txtLuong.Text = row.Cells["Luong"].Value?.ToString() ?? "";

            // Xử lý ngày - nếu null thì dùng ngày hôm nay
            if (row.Cells["NgayVaoLam"].Value != null)
                dtpNgayVaoLam.Value = Convert.ToDateTime(row.Cells["NgayVaoLam"].Value);
            else
                dtpNgayVaoLam.Value = DateTime.Today;

            // KHÔNG CHO SỬA MÃ NV (PK)
            txtMaNV.Enabled = false;
        }


        private void LoadData()
        {
            dgvNhanVien.DataSource = db.NHANVIENs
                .Select(n => new { n.MaNV, n.HoTen, n.ChucVu, n.PhongBan, n.SDT, n.Luong, n.NgayVaoLam })
                .ToList();
        }

        private void LoadFilterBoxes()
        {
            cbPhongBan.Items.Clear();
            cbChucVu.Items.Clear();
            cbPhongBan.Items.AddRange(db.NHANVIENs.Select(x => x.PhongBan).Distinct().ToArray());
            cbChucVu.Items.AddRange(db.NHANVIENs.Select(x => x.ChucVu).Distinct().ToArray());
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtChucVu.Clear();
            txtPhongBan.Clear();
            txtSDT.Clear();
            txtLuong.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtLuong.Text, out decimal luong) || luong <= 3000000)
            {
                MessageBox.Show("Lương phải là số và > 3,000,000.");
                return;
            }

            var nv = new NHANVIEN
            {
                MaNV = txtMaNV.Text,
                HoTen = txtHoTen.Text,
                ChucVu = txtChucVu.Text,
                PhongBan = txtPhongBan.Text,
                SDT = txtSDT.Text,
                Luong = luong,
                NgayVaoLam = dtpNgayVaoLam.Value.Date
            };

            db.NHANVIENs.Add(nv);
            db.SaveChanges();
            LoadData();
            LoadFilterBoxes();
            MessageBox.Show("Đã thêm nhân viên.");
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            var nv = db.NHANVIENs.Find(txtMaNV.Text);
            if (nv == null) { MessageBox.Show("Không tìm thấy nhân viên."); return; }

            if (!decimal.TryParse(txtLuong.Text, out decimal luong) || luong <= 3000000)
            {
                MessageBox.Show("Lương phải > 3,000,000.");
                return;
            }

            nv.HoTen = txtHoTen.Text;
            nv.ChucVu = txtChucVu.Text;
            nv.PhongBan = txtPhongBan.Text;
            nv.SDT = txtSDT.Text;
            nv.Luong = luong;
            nv.NgayVaoLam = dtpNgayVaoLam.Value.Date;

            db.SaveChanges();
            LoadData();
            MessageBox.Show("Cập nhật thành công.");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var nv = db.NHANVIENs.Find(txtMaNV.Text);
            if (nv == null) { MessageBox.Show("Không tìm thấy."); return; }

            // Kiểm tra ràng buộc FK: tránh xóa NV nếu có bảng tham chiếu (SUCO, THANH_TOAN)
            var hasRef = db.SUCOes.Any(s => s.MaNV == nv.MaNV) || db.THANH_TOAN.Any(t => t.NguoiThu == nv.MaNV);
            if (hasRef)
            {
                MessageBox.Show("Không thể xóa nhân viên vì có dữ liệu liên quan.");
                return;
            }

            db.NHANVIENs.Remove(nv);
            db.SaveChanges();
            LoadData();
            MessageBox.Show("Xóa thành công.");
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string phong = cbPhongBan.Text;
            string chucvu = cbChucVu.Text;

            var q = db.NHANVIENs.AsQueryable();
            if (!string.IsNullOrWhiteSpace(phong)) q = q.Where(x => x.PhongBan.Contains(phong));
            if (!string.IsNullOrWhiteSpace(chucvu)) q = q.Where(x => x.ChucVu.Contains(chucvu));
            dgvNhanVien.DataSource = q.Select(n => new { n.MaNV, n.HoTen, n.ChucVu, n.PhongBan, n.SDT, n.Luong, n.NgayVaoLam }).ToList();
        }

        private void btnHienTatCa_Click(object sender, EventArgs e)
        {
            cbPhongBan.SelectedIndex = -1;
            cbChucVu.SelectedIndex = -1;

            cbPhongBan.Text = "";
            cbChucVu.Text = "";

            LoadData();
        }

    }
}
