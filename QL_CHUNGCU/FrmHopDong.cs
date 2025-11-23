using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmHopDong : Form
    {
        private QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();

        public FrmHopDong()
        {
            InitializeComponent();
        }

        private void FrmHopDong_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCanHoComboBox();
            LoadCuDanComboBox();
            cboTimKiem.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
        }

        // Load danh sách căn hộ trống vào ComboBox
        private void LoadCanHoComboBox()
        {
            try
            {
                var listCanHo = db.CANHOes
                    .Where(c => c.TrangThai == "Trống")
                    .OrderBy(c => c.MaCanHo)
                    .Select(c => new
                    {
                        c.MaCanHo,
                        DisplayText = c.MaCanHo + " - " + c.LoaiCanHo + " (Tầng " + c.Tang + ")"
                    })
                    .ToList();

                cboCanHo.DataSource = listCanHo;
                cboCanHo.DisplayMember = "DisplayText";
                cboCanHo.ValueMember = "MaCanHo";
                cboCanHo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách căn hộ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load danh sách cư dân vào ComboBox
        private void LoadCuDanComboBox()
        {
            try
            {
                var listCuDan = db.CU_DAN
                    .OrderBy(c => c.HoTen)
                    .Select(c => new
                    {
                        c.MaCD,
                        DisplayText = c.MaCD + " - " + c.HoTen + " (" + c.SDT + ")"
                    })
                    .ToList();

                cboCuDan.DataSource = listCuDan;
                cboCuDan.DisplayMember = "DisplayText";
                cboCuDan.ValueMember = "MaCD";
                cboCuDan.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách cư dân: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load dữ liệu lên DataGridView
        private void LoadData()
        {
            try
            {
                var listHopDong = db.HOPDONGs
                    .Select(h => new
                    {
                        h.MaHDong,
                        h.MaCanHo,
                        h.MaCD,
                        TenCuDan = h.CU_DAN.HoTen,
                        h.LoaiHopDong,
                        h.NgayKy,
                        h.NgayHetHan,
                        h.GiaTriHopDong,
                        h.TrangThai
                    })
                    .OrderByDescending(h => h.NgayKy)
                    .ToList();

                dgvHopDong.DataSource = listHopDong;
                FormatDataGridView();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Định dạng DataGridView
        private void FormatDataGridView()
        {
            if (dgvHopDong.Columns.Count > 0)
            {
                dgvHopDong.Columns["MaHDong"].HeaderText = "Mã hợp đồng";
                dgvHopDong.Columns["MaHDong"].Width = 80;

                dgvHopDong.Columns["MaCanHo"].HeaderText = "Mã căn hộ";
                dgvHopDong.Columns["MaCanHo"].Width = 80;

                dgvHopDong.Columns["MaCD"].HeaderText = "Mã cư dân";
                dgvHopDong.Columns["MaCD"].Width = 80;

                dgvHopDong.Columns["TenCuDan"].HeaderText = "Tên cư dân";
                dgvHopDong.Columns["TenCuDan"].Width = 150;

                dgvHopDong.Columns["LoaiHopDong"].HeaderText = "Loại";
                dgvHopDong.Columns["LoaiHopDong"].Width = 80;

                dgvHopDong.Columns["NgayKy"].HeaderText = "Ngày ký";
                dgvHopDong.Columns["NgayKy"].Width = 80;
                dgvHopDong.Columns["NgayKy"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvHopDong.Columns["NgayHetHan"].HeaderText = "Ngày hết hạn";
                dgvHopDong.Columns["NgayHetHan"].Width = 80;
                dgvHopDong.Columns["NgayHetHan"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvHopDong.Columns["GiaTriHopDong"].HeaderText = "Giá trị (VNĐ)";
                dgvHopDong.Columns["GiaTriHopDong"].Width = 100;
                dgvHopDong.Columns["GiaTriHopDong"].DefaultCellStyle.Format = "N0";
                dgvHopDong.Columns["GiaTriHopDong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // Cột cuối cùng tự động Fill - GIỐNG FrmNguoiO
                dgvHopDong.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvHopDong.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        // Xóa các ô nhập liệu
        private void ClearInputs()
        {
            txtMaHopDong.Clear();
            cboCanHo.SelectedIndex = -1;
            cboCuDan.SelectedIndex = -1;
            cboLoaiHopDong.SelectedIndex = -1;
            dtpNgayKy.Value = DateTime.Now;
            dtpNgayHetHan.Value = DateTime.Now;
            numGiaTri.Value = 0;
            cboTrangThai.SelectedIndex = 0;
            txtMaHopDong.Focus();
            txtMaHopDong.Enabled = true;
            cboTrangThai.Enabled = false;
        }

        // Lấy dữ liệu từ form
        private HOPDONG GetDataFromForm()
        {
            HOPDONG hopDong = new HOPDONG();

            hopDong.MaHDong = txtMaHopDong.Text.Trim();
            hopDong.MaCanHo = cboCanHo.SelectedValue?.ToString();
            hopDong.MaCD = cboCuDan.SelectedValue?.ToString();
            hopDong.LoaiHopDong = cboLoaiHopDong.Text;
            hopDong.NgayKy = dtpNgayKy.Value;
            hopDong.NgayHetHan = dtpNgayHetHan.Value;
            hopDong.GiaTriHopDong = numGiaTri.Value;
            hopDong.TrangThai = cboTrangThai.Text;

            return hopDong;
        }

        // Hiển thị dữ liệu lên form
        private void DisplayDataToForm(HOPDONG hopDong)
        {
            if (hopDong != null)
            {
                txtMaHopDong.Text = hopDong.MaHDong;
                
                // Load lại danh sách căn hộ bao gồm căn hộ hiện tại
                var listCanHo = db.CANHOes
                    .Where(c => c.TrangThai == "Trống" || c.MaCanHo == hopDong.MaCanHo)
                    .OrderBy(c => c.MaCanHo)
                    .Select(c => new
                    {
                        c.MaCanHo,
                        DisplayText = c.MaCanHo + " - " + c.LoaiCanHo + " (Tầng " + c.Tang + ")"
                    })
                    .ToList();

                cboCanHo.DataSource = listCanHo;
                cboCanHo.DisplayMember = "DisplayText";
                cboCanHo.ValueMember = "MaCanHo";
                cboCanHo.SelectedValue = hopDong.MaCanHo;

                cboCuDan.SelectedValue = hopDong.MaCD;
                cboLoaiHopDong.Text = hopDong.LoaiHopDong;
                dtpNgayKy.Value = hopDong.NgayKy ?? DateTime.Now;
                dtpNgayHetHan.Value = hopDong.NgayHetHan ?? DateTime.Now;
                numGiaTri.Value = hopDong.GiaTriHopDong ?? 0;
                cboTrangThai.Text = hopDong.TrangThai;
                
                txtMaHopDong.Enabled = false;
                cboTrangThai.Enabled = true;
            }
        }

        // Validate dữ liệu
        private bool ValidateHopDong(HOPDONG hopDong, bool isInsert)
        {
            // Kiểm tra mã hợp đồng
            if (string.IsNullOrWhiteSpace(hopDong.MaHDong))
            {
                MessageBox.Show("Mã hợp đồng không được để trống!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHopDong.Focus();
                return false;
            }

            if (hopDong.MaHDong.Length > 10)
            {
                MessageBox.Show("Mã hợp đồng không được vượt quá 10 ký tự!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHopDong.Focus();
                return false;
            }

            // Kiểm tra mã đã tồn tại (chỉ khi thêm mới)
            if (isInsert && db.HOPDONGs.Any(h => h.MaHDong == hopDong.MaHDong))
            {
                MessageBox.Show("Mã hợp đồng đã tồn tại!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHopDong.Focus();
                return false;
            }

            // Kiểm tra mã căn hộ
            if (string.IsNullOrWhiteSpace(hopDong.MaCanHo))
            {
                MessageBox.Show("Vui lòng chọn căn hộ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCanHo.Focus();
                return false;
            }

            // Kiểm tra căn hộ phải trống (chỉ khi ký mới)
            if (isInsert)
            {
                var canHo = db.CANHOes.Find(hopDong.MaCanHo);
                if (canHo == null)
                {
                    MessageBox.Show("Căn hộ không tồn tại!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboCanHo.Focus();
                    return false;
                }

                if (canHo.TrangThai?.Trim().ToLower() != "trống")
                {
                    MessageBox.Show("Chỉ được ký hợp đồng cho căn hộ có trạng thái 'Trống'!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboCanHo.Focus();
                    return false;
                }
            }

            // Kiểm tra mã cư dân
            if (string.IsNullOrWhiteSpace(hopDong.MaCD))
            {
                MessageBox.Show("Vui lòng chọn cư dân!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCuDan.Focus();
                return false;
            }

            // Kiểm tra cư dân có tồn tại không
            if (!db.CU_DAN.Any(c => c.MaCD == hopDong.MaCD))
            {
                MessageBox.Show("Cư dân không tồn tại!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCuDan.Focus();
                return false;
            }

            // Kiểm tra loại hợp đồng
            if (string.IsNullOrWhiteSpace(hopDong.LoaiHopDong))
            {
                MessageBox.Show("Vui lòng chọn loại hợp đồng!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiHopDong.Focus();
                return false;
            }

            // Kiểm tra ngày ký
            if (!hopDong.NgayKy.HasValue)
            {
                MessageBox.Show("Ngày ký không hợp lệ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayKy.Focus();
                return false;
            }

            // Kiểm tra ngày hết hạn
            if (!hopDong.NgayHetHan.HasValue)
            {
                MessageBox.Show("Ngày hết hạn không hợp lệ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayHetHan.Focus();
                return false;
            }

            // Kiểm tra ngày hết hạn phải sau ngày ký
            if (hopDong.NgayHetHan <= hopDong.NgayKy)
            {
                MessageBox.Show("Ngày hết hạn phải sau ngày ký!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayHetHan.Focus();
                return false;
            }

            // Kiểm tra giá trị hợp đồng
            if (!hopDong.GiaTriHopDong.HasValue || hopDong.GiaTriHopDong.Value <= 0)
            {
                MessageBox.Show("Giá trị hợp đồng phải lớn hơn 0!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numGiaTri.Focus();
                return false;
            }

            return true;
        }

        // Tự động tính ngày hết hạn dựa trên loại hợp đồng
        private void cboLoaiHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiHopDong.SelectedIndex == -1) return;

            DateTime ngayKy = dtpNgayKy.Value;
            DateTime ngayHetHan = ngayKy;

            switch (cboLoaiHopDong.Text)
            {
                case "6 tháng":
                    ngayHetHan = ngayKy.AddMonths(6);
                    break;
                case "1 năm":
                    ngayHetHan = ngayKy.AddYears(1);
                    break;
                case "2 năm":
                    ngayHetHan = ngayKy.AddYears(2);
                    break;
                case "Dài hạn":
                    ngayHetHan = ngayKy.AddYears(5);
                    break;
            }

            dtpNgayHetHan.Value = ngayHetHan;
        }

        // Cập nhật ngày hết hạn khi thay đổi ngày ký
        private void dtpNgayKy_ValueChanged(object sender, EventArgs e)
        {
            if (cboLoaiHopDong.SelectedIndex != -1)
            {
                cboLoaiHopDong_SelectedIndexChanged(sender, e);
            }
        }

        // Thêm hợp đồng
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                HOPDONG hopDong = GetDataFromForm();

                if (!ValidateHopDong(hopDong, true))
                {
                    return;
                }

                // Thêm hợp đồng
                db.HOPDONGs.Add(hopDong);

                // Cập nhật trạng thái căn hộ
                var canHo = db.CANHOes.Find(hopDong.MaCanHo);
                if (canHo != null)
                {
                    canHo.TrangThai = "Đã thuê";
                }

                db.SaveChanges();

                MessageBox.Show("Thêm hợp đồng thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LoadCanHoComboBox(); // Load lại danh sách căn hộ trống
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa hợp đồng
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaHopDong.Text))
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng từ danh sách để sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maHopDong = txtMaHopDong.Text.Trim();
                HOPDONG hopDong = db.HOPDONGs.Find(maHopDong);

                if (hopDong == null)
                {
                    MessageBox.Show("Không tìm thấy hợp đồng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cập nhật dữ liệu (chỉ cho phép sửa giá trị và ngày hết hạn)
                HOPDONG updatedHopDong = GetDataFromForm();

                if (!ValidateHopDong(updatedHopDong, false))
                {
                    return;
                }

                hopDong.NgayHetHan = updatedHopDong.NgayHetHan;
                hopDong.GiaTriHopDong = updatedHopDong.GiaTriHopDong;
                hopDong.TrangThai = updatedHopDong.TrangThai;

                db.Entry(hopDong).State = EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("Cập nhật hợp đồng thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa hợp đồng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaHopDong.Text))
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maHopDong = txtMaHopDong.Text.Trim();
                HOPDONG hopDong = db.HOPDONGs.Find(maHopDong);

                if (hopDong == null)
                {
                    MessageBox.Show("Không tìm thấy hợp đồng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa hợp đồng này?\nCăn hộ sẽ được chuyển về trạng thái 'Trống'.",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Cập nhật trạng thái căn hộ về trống
                    var canHo = db.CANHOes.Find(hopDong.MaCanHo);
                    if (canHo != null)
                    {
                        canHo.TrangThai = "Trống";
                    }

                    db.HOPDONGs.Remove(hopDong);
                    db.SaveChanges();

                    MessageBox.Show("Xóa hợp đồng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    LoadCanHoComboBox(); // Load lại danh sách căn hộ trống
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chấm dứt hợp đồng
        private void btnChamDut_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaHopDong.Text))
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần chấm dứt!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maHopDong = txtMaHopDong.Text.Trim();
                HOPDONG hopDong = db.HOPDONGs.Find(maHopDong);

                if (hopDong == null)
                {
                    MessageBox.Show("Không tìm thấy hợp đồng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (hopDong.TrangThai != "Hiệu lực")
                {
                    MessageBox.Show("Chỉ có thể chấm dứt hợp đồng đang 'Hiệu lực'!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn chấm dứt hợp đồng này?\nCăn hộ sẽ được chuyển về trạng thái 'Trống'.",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Cập nhật trạng thái hợp đồng
                    hopDong.TrangThai = "Đã hủy";

                    // Cập nhật trạng thái căn hộ về trống
                    var canHo = db.CANHOes.Find(hopDong.MaCanHo);
                    if (canHo != null)
                    {
                        canHo.TrangThai = "Trống";
                    }

                    db.Entry(hopDong).State = EntityState.Modified;
                    db.SaveChanges();

                    MessageBox.Show("Chấm dứt hợp đồng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    LoadCanHoComboBox(); // Load lại danh sách căn hộ trống
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chấm dứt hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            LoadCanHoComboBox();
            LoadCuDanComboBox();
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

                IQueryable<HOPDONG> query = db.HOPDONGs;

                switch (cboTimKiem.SelectedIndex)
                {
                    case 0: // Tìm theo Mã căn hộ
                        query = query.Where(h => h.MaCanHo == timKiem);
                        break;
                    case 1: // Tìm theo Mã cư dân
                        query = query.Where(h => h.MaCD == timKiem);
                        break;
                    case 2: // Tìm theo Trạng thái
                        query = query.Where(h => h.TrangThai == timKiem);
                        break;
                }

                var listHopDong = query
                    .Select(h => new
                    {
                        h.MaHDong,
                        h.MaCanHo,
                        h.MaCD,
                        TenCuDan = h.CU_DAN.HoTen,
                        h.LoaiHopDong,
                        h.NgayKy,
                        h.NgayHetHan,
                        h.GiaTriHopDong,
                        h.TrangThai
                    })
                    .OrderByDescending(h => h.NgayKy)
                    .ToList();

                dgvHopDong.DataSource = listHopDong;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CellClick trên DataGridView
        private void dgvHopDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                string maHopDong = dgvHopDong.Rows[e.RowIndex].Cells["MaHDong"].Value.ToString();
                HOPDONG hopDong = db.HOPDONGs.Find(maHopDong);

                DisplayDataToForm(hopDong);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}