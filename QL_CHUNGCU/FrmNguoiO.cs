using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmNguoiO : Form
    {
        private QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();

        public FrmNguoiO()
        {
            InitializeComponent();
        }

        private void FrmNguoiO_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCanHoComboBox();
            cboTimKiem.SelectedIndex = 0;
        }

        // Load danh sách căn hộ vào ComboBox
        private void LoadCanHoComboBox()
        {
            try
            {
                var listCanHo = db.CANHOes
                    .OrderBy(c => c.MaCanHo)
                    .Select(c => new
                    {
                        c.MaCanHo,
                        DisplayText = c.MaCanHo + " -   " + c.LoaiCanHo + " (Tầng " + c.Tang + ")"
                    })
                    .ToList();

                cboMaCanHo.DataSource = listCanHo;
                cboMaCanHo.DisplayMember = "DisplayText";
                cboMaCanHo.ValueMember = "MaCanHo";
                cboMaCanHo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách căn hộ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load dữ liệu lên DataGridView
        private void LoadData()
        {
            try
            {
                var listNguoiO = db.NGUOI_O
                    .Select(n => new
                    {
                        n.MaNguoiO,
                        n.MaCanHo,
                        n.HoTen,
                        n.NgaySinh,
                        n.QuanHe,
                        n.CCCD
                    })
                    .ToList();

                dgvNguoiO.DataSource = listNguoiO;
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
            if (dgvNguoiO.Columns.Count > 0)
            {
                dgvNguoiO.Columns["MaNguoiO"].HeaderText = "Mã người ở";
                dgvNguoiO.Columns["MaNguoiO"].Width = 100;

                dgvNguoiO.Columns["MaCanHo"].HeaderText = "Mã căn hộ";
                dgvNguoiO.Columns["MaCanHo"].Width = 100;

                dgvNguoiO.Columns["HoTen"].HeaderText = "Họ tên";
                dgvNguoiO.Columns["HoTen"].Width = 150;

                dgvNguoiO.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvNguoiO.Columns["NgaySinh"].Width = 100;
                dgvNguoiO.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvNguoiO.Columns["QuanHe"].HeaderText = "Quan hệ";
                dgvNguoiO.Columns["QuanHe"].Width = 100;

                dgvNguoiO.Columns["CCCD"].HeaderText = "CCCD";
                dgvNguoiO.Columns["CCCD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        // Xóa các ô nhập liệu
        private void ClearInputs()
        {
            txtMaNguoiO.Clear();
            cboMaCanHo.SelectedIndex = -1;
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            txtQuanHe.Clear();
            txtCCCD.Clear();
            txtMaNguoiO.Focus();
            txtMaNguoiO.Enabled = true;
        }

        // Lấy dữ liệu từ form
        private NGUOI_O GetDataFromForm()
        {
            NGUOI_O nguoiO = new NGUOI_O();

            nguoiO.MaNguoiO = txtMaNguoiO.Text.Trim();
            nguoiO.MaCanHo = cboMaCanHo.SelectedValue?.ToString();
            nguoiO.HoTen = txtHoTen.Text.Trim();
            nguoiO.NgaySinh = dtpNgaySinh.Value;
            nguoiO.QuanHe = txtQuanHe.Text.Trim();
            nguoiO.CCCD = txtCCCD.Text.Trim();

            return nguoiO;
        }

        // Hiển thị dữ liệu lên form
        private void DisplayDataToForm(NGUOI_O nguoiO)
        {
            if (nguoiO != null)
            {
                txtMaNguoiO.Text = nguoiO.MaNguoiO;
                cboMaCanHo.SelectedValue = nguoiO.MaCanHo;
                txtHoTen.Text = nguoiO.HoTen;
                dtpNgaySinh.Value = nguoiO.NgaySinh ?? DateTime.Now;
                txtQuanHe.Text = nguoiO.QuanHe;
                txtCCCD.Text = nguoiO.CCCD;
                txtMaNguoiO.Enabled = false;
            }
        }

        // Validate dữ liệu
        private bool ValidateNguoiO(NGUOI_O nguoiO, bool isInsert)
        {
            // Kiểm tra mã người ở
            if (string.IsNullOrWhiteSpace(nguoiO.MaNguoiO))
            {
                MessageBox.Show("Mã người ở không được để trống!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNguoiO.Focus();
                return false;
            }

            if (nguoiO.MaNguoiO.Length > 10)
            {
                MessageBox.Show("Mã người ở không được vượt quá 10 ký tự!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNguoiO.Focus();
                return false;
            }

            // Kiểm tra mã đã tồn tại (chỉ khi thêm mới)
            if (isInsert && db.NGUOI_O.Any(n => n.MaNguoiO == nguoiO.MaNguoiO))
            {
                MessageBox.Show("Mã người ở đã tồn tại!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNguoiO.Focus();
                return false;
            }

            // Kiểm tra mã căn hộ
            if (string.IsNullOrWhiteSpace(nguoiO.MaCanHo))
            {
                MessageBox.Show("Vui lòng chọn căn hộ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaCanHo.Focus();
                return false;
            }

            // Kiểm tra căn hộ có tồn tại không
            if (!db.CANHOes.Any(c => c.MaCanHo == nguoiO.MaCanHo))
            {
                MessageBox.Show("Căn hộ không tồn tại!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaCanHo.Focus();
                return false;
            }

            // Kiểm tra họ tên (bắt buộc)
            if (string.IsNullOrWhiteSpace(nguoiO.HoTen))
            {
                MessageBox.Show("Họ tên không được để trống!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (nguoiO.HoTen.Length > 100)
            {
                MessageBox.Show("Họ tên không được vượt quá 100 ký tự!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            // Kiểm tra ngày sinh
            if (!nguoiO.NgaySinh.HasValue || nguoiO.NgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            // Kiểm tra quan hệ (bắt buộc)
            if (string.IsNullOrWhiteSpace(nguoiO.QuanHe))
            {
                MessageBox.Show("Quan hệ không được để trống!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuanHe.Focus();
                return false;
            }

            // Validate quan hệ (chỉ cho phép: con, vợ, chồng, bạn bè)
            string[] validRelations = { "con", "vợ", "chồng", "bạn bè" };
            string quanHe = nguoiO.QuanHe.Trim().ToLower();

            if (!validRelations.Contains(quanHe))
            {
                MessageBox.Show("Quan hệ chỉ được phép là: con, vợ, chồng, bạn bè!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuanHe.Focus();
                return false;
            }

            // Kiểm tra CCCD (phải đúng 12 số)
            if (!string.IsNullOrWhiteSpace(nguoiO.CCCD))
            {
                if (!Regex.IsMatch(nguoiO.CCCD, @"^\d{12}$"))
                {
                    MessageBox.Show("CCCD phải đúng 12 chữ số!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCCCD.Focus();
                    return false;
                }

                // Kiểm tra CCCD đã tồn tại (trừ trường hợp sửa cùng mã người ở)
                bool cccdExists = isInsert
                    ? db.NGUOI_O.Any(n => n.CCCD == nguoiO.CCCD)
                    : db.NGUOI_O.Any(n => n.CCCD == nguoiO.CCCD && n.MaNguoiO != nguoiO.MaNguoiO);

                if (cccdExists)
                {
                    MessageBox.Show("CCCD đã tồn tại trong hệ thống!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCCCD.Focus();
                    return false;
                }
            }

            return true;
        }

        // Thêm người ở
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                NGUOI_O nguoiO = GetDataFromForm();

                if (!ValidateNguoiO(nguoiO, true))
                {
                    return;
                }

                db.NGUOI_O.Add(nguoiO);
                db.SaveChanges();

                MessageBox.Show("Thêm người ở thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm người ở: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa người ở
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaNguoiO.Text))
                {
                    MessageBox.Show("Vui lòng chọn người ở từ danh sách để sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maNguoiO = txtMaNguoiO.Text.Trim();
                NGUOI_O nguoiO = db.NGUOI_O.Find(maNguoiO);

                if (nguoiO == null)
                {
                    MessageBox.Show("Không tìm thấy người ở!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cập nhật dữ liệu
                NGUOI_O updatedNguoiO = GetDataFromForm();

                if (!ValidateNguoiO(updatedNguoiO, false))
                {
                    return;
                }

                nguoiO.MaCanHo = updatedNguoiO.MaCanHo;
                nguoiO.HoTen = updatedNguoiO.HoTen;
                nguoiO.NgaySinh = updatedNguoiO.NgaySinh;
                nguoiO.QuanHe = updatedNguoiO.QuanHe;
                nguoiO.CCCD = updatedNguoiO.CCCD;

                db.Entry(nguoiO).State = EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("Cập nhật người ở thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật người ở: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa người ở
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaNguoiO.Text))
                {
                    MessageBox.Show("Vui lòng chọn người ở cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maNguoiO = txtMaNguoiO.Text.Trim();
                NGUOI_O nguoiO = db.NGUOI_O.Find(maNguoiO);

                if (nguoiO == null)
                {
                    MessageBox.Show("Không tìm thấy người ở!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa người ở này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    db.NGUOI_O.Remove(nguoiO);
                    db.SaveChanges();

                    MessageBox.Show("Xóa người ở thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa người ở: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            LoadCanHoComboBox();
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

                IQueryable<NGUOI_O> query = db.NGUOI_O;

                switch (cboTimKiem.SelectedIndex)
                {
                    case 0: // Tìm theo Mã căn hộ
                        query = query.Where(n => n.MaCanHo == timKiem);
                        break;

                    case 1: // Tìm theo Họ tên
                        query = query.Where(n => n.HoTen.Contains(timKiem));
                        break;

                    default:
                        MessageBox.Show("Vui lòng chọn loại tìm kiếm!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                var result = query
                    .Select(n => new
                    {
                        n.MaNguoiO,
                        n.MaCanHo,
                        n.HoTen,
                        n.NgaySinh,
                        n.QuanHe,
                        n.CCCD
                    })
                    .ToList();

                dgvNguoiO.DataSource = result;
                FormatDataGridView();

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

        // Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Click vào DataGridView
        private void dgvNguoiO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNguoiO.Rows[e.RowIndex];

                string maNguoiO = row.Cells["MaNguoiO"].Value?.ToString();

                if (!string.IsNullOrEmpty(maNguoiO))
                {
                    NGUOI_O nguoiO = db.NGUOI_O.Find(maNguoiO);

                    if (nguoiO != null)
                    {
                        db.Entry(nguoiO).Reload();
                        DisplayDataToForm(nguoiO);
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
