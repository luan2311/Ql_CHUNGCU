using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmCuDan : Form
    {
        private QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();

        public FrmCuDan()
        {
            InitializeComponent();
        }

        private void FrmCuDan_Load(object sender, EventArgs e)
        {
            LoadData();
            cboTimKiem.SelectedIndex = 0;
            cboGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
        }

        // Load dữ liệu lên DataGridView
        private void LoadData()
        {
            try
            {
                var listCuDan = db.CU_DAN
                    .Select(c => new
                    {
                        c.MaCD,
                        c.HoTen,
                        c.NgaySinh,
                        c.GioiTinh,
                        c.SDT,
                        c.CCCD,
                        c.Email
                    })
                    .ToList();

                dgvCuDan.DataSource = listCuDan;
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa các ô nhập liệu
        private void ClearInputs()
        {
            txtMaCD.Clear();
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = -1;
            txtSDT.Clear();
            txtCCCD.Clear();
            txtEmail.Clear();
            txtMaCD.Focus();
            txtMaCD.Enabled = true;
        }

        // Lấy dữ liệu từ form
        private CU_DAN GetDataFromForm()
        {
            CU_DAN cuDan = new CU_DAN();

            cuDan.MaCD = txtMaCD.Text.Trim();
            cuDan.HoTen = txtHoTen.Text.Trim();
            cuDan.NgaySinh = dtpNgaySinh.Value;
            cuDan.GioiTinh = cboGioiTinh.Text;
            cuDan.SDT = txtSDT.Text.Trim();
            cuDan.CCCD = txtCCCD.Text.Trim();
            cuDan.Email = txtEmail.Text.Trim();

            return cuDan;
        }

        // Hiển thị dữ liệu lên form
        private void DisplayDataToForm(CU_DAN cuDan)
        {
            if (cuDan != null)
            {
                txtMaCD.Text = cuDan.MaCD;
                txtHoTen.Text = cuDan.HoTen;
                dtpNgaySinh.Value = cuDan.NgaySinh ?? DateTime.Now;
                cboGioiTinh.Text = cuDan.GioiTinh;
                txtSDT.Text = cuDan.SDT;
                txtCCCD.Text = cuDan.CCCD;
                txtEmail.Text = cuDan.Email;
                txtMaCD.Enabled = false;
            }
        }

        // Validate dữ liệu
        private bool ValidateCuDan(CU_DAN cuDan, bool isInsert)
        {
            // Kiểm tra mã cư dân
            if (string.IsNullOrWhiteSpace(cuDan.MaCD))
            {
                MessageBox.Show("Mã cư dân không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCD.Focus();
                return false;
            }

            if (cuDan.MaCD.Length > 10)
            {
                MessageBox.Show("Mã cư dân không được vượt quá 10 ký tự!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCD.Focus();
                return false;
            }

            // Kiểm tra mã đã tồn tại (chỉ khi thêm mới)
            if (isInsert && db.CU_DAN.Any(c => c.MaCD == cuDan.MaCD))
            {
                MessageBox.Show("Mã cư dân đã tồn tại!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCD.Focus();
                return false;
            }

            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(cuDan.HoTen))
            {
                MessageBox.Show("Họ tên không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            // Kiểm tra ngày sinh
            if (!cuDan.NgaySinh.HasValue || cuDan.NgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            // Kiểm tra giới tính
            if (string.IsNullOrWhiteSpace(cuDan.GioiTinh))
            {
                MessageBox.Show("Giới tính không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGioiTinh.Focus();
                return false;
            }

            // Kiểm tra SDT (phải đúng 10 số)
            if (string.IsNullOrWhiteSpace(cuDan.SDT))
            {
                MessageBox.Show("Số điện thoại không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            if (!Regex.IsMatch(cuDan.SDT, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại phải đúng 10 chữ số!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            // Kiểm tra CCCD (phải đúng 12 số)
            if (string.IsNullOrWhiteSpace(cuDan.CCCD))
            {
                MessageBox.Show("CCCD không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCCCD.Focus();
                return false;
            }

            if (!Regex.IsMatch(cuDan.CCCD, @"^\d{12}$"))
            {
                MessageBox.Show("CCCD phải đúng 12 chữ số!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCCCD.Focus();
                return false;
            }

            // Kiểm tra CCCD đã tồn tại (trừ trường hợp sửa cùng mã cư dân)
            bool cccdExists = isInsert 
                ? db.CU_DAN.Any(c => c.CCCD == cuDan.CCCD)
                : db.CU_DAN.Any(c => c.CCCD == cuDan.CCCD && c.MaCD != cuDan.MaCD);

            if (cccdExists)
            {
                MessageBox.Show("CCCD đã tồn tại trong hệ thống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCCCD.Focus();
                return false;
            }

            // Kiểm tra Email hợp lệ
            if (string.IsNullOrWhiteSpace(cuDan.Email))
            {
                MessageBox.Show("Email không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!Regex.IsMatch(cuDan.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        // Thêm cư dân
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                CU_DAN cuDan = GetDataFromForm();

                if (!ValidateCuDan(cuDan, true))
                {
                    return;
                }

                db.CU_DAN.Add(cuDan);
                db.SaveChanges();

                MessageBox.Show("Thêm cư dân thành công!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm cư dân: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa cư dân
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaCD.Text))
                {
                    MessageBox.Show("Vui lòng chọn cư dân từ danh sách để sửa!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maCD = txtMaCD.Text.Trim();
                CU_DAN cuDan = db.CU_DAN.Find(maCD);

                if (cuDan == null)
                {
                    MessageBox.Show("Không tìm thấy cư dân!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cập nhật dữ liệu
                CU_DAN updatedCuDan = GetDataFromForm();

                if (!ValidateCuDan(updatedCuDan, false))
                {
                    return;
                }

                cuDan.HoTen = updatedCuDan.HoTen;
                cuDan.NgaySinh = updatedCuDan.NgaySinh;
                cuDan.GioiTinh = updatedCuDan.GioiTinh;
                cuDan.SDT = updatedCuDan.SDT;
                cuDan.CCCD = updatedCuDan.CCCD;
                cuDan.Email = updatedCuDan.Email;

                db.Entry(cuDan).State = EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("Cập nhật cư dân thành công!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật cư dân: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa cư dân
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaCD.Text))
                {
                    MessageBox.Show("Vui lòng chọn cư dân cần xóa!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maCD = txtMaCD.Text.Trim();
                CU_DAN cuDan = db.CU_DAN.Find(maCD);

                if (cuDan == null)
                {
                    MessageBox.Show("Không tìm thấy cư dân!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra xem cư dân có liên kết với hợp đồng nào không
                bool hasContract = db.HOPDONGs.Any(h => h.MaCD == maCD);

                if (hasContract)
                {
                    MessageBox.Show("Không thể xóa cư dân đã có hợp đồng liên kết!", "Cảnh báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem cư dân có liên kết với lịch sử căn hộ không
                bool hasHistory = db.LICH_SU_CANHO.Any(l => l.MaCD == maCD);

                if (hasHistory)
                {
                    MessageBox.Show("Không thể xóa cư dân đã có lịch sử căn hộ!", "Cảnh báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa cư dân này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    db.CU_DAN.Remove(cuDan);
                    db.SaveChanges();

                    MessageBox.Show("Xóa cư dân thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa cư dân: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        // Tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string timKiem = txtTimKiem.Text.Trim();

                if (string.IsNullOrWhiteSpace(timKiem))
                {
                    MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                IQueryable<CU_DAN> query = db.CU_DAN;

                switch (cboTimKiem.SelectedIndex)
                {
                    case 0: // Tìm theo Họ tên
                        query = query.Where(c => c.HoTen.Contains(timKiem));
                        break;

                    case 1: // Tìm theo CCCD
                        query = query.Where(c => c.CCCD == timKiem);
                        break;

                    case 2: // Tìm theo SĐT
                        query = query.Where(c => c.SDT == timKiem);
                        break;

                    default:
                        MessageBox.Show("Vui lòng chọn loại tìm kiếm!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                var result = query
                    .Select(c => new
                    {
                        c.MaCD,
                        c.HoTen,
                        c.NgaySinh,
                        c.GioiTinh,
                        c.SDT,
                        c.CCCD,
                        c.Email
                    })
                    .ToList();

                dgvCuDan.DataSource = result;

                if (result.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Click vào DataGridView
        private void dgvCuDan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCuDan.Rows[e.RowIndex];
                
                string maCD = row.Cells["MaCD"].Value?.ToString();
                
                if (!string.IsNullOrEmpty(maCD))
                {
                    CU_DAN cuDan = db.CU_DAN.Find(maCD);

                    if (cuDan != null)
                    {
                        db.Entry(cuDan).Reload();
                        DisplayDataToForm(cuDan);
                    }
                }
            }
        }

        // Dispose DbContext khi đóng form
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            db?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
