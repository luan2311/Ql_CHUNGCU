using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmCanHo : Form
    {
        private QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();

        public FrmCanHo()
        {
            InitializeComponent();
        }

        private void FrmCanHo_Load(object sender, EventArgs e)
        {
            LoadData();
            cboTimKiem.SelectedIndex = 0;
            // cboTrangThai.Visible = false;
            // label5.Visible = false;
        }

        // Load dữ liệu lên DataGridView
        private void LoadData()
        {
            try
            {
                var listCanHo = db.CANHOes
                    .Select(c => new
                    {
                        c.MaCanHo,
                        c.SoPhong,
                        c.DienTich,
                        c.Tang,
                        c.TrangThai,
                        c.LoaiCanHo,
                        c.Huong,
                        c.Block
                    })
                    .ToList();

                dgvCanHo.DataSource = listCanHo;
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
            txtMaCanHo.Clear();
            txtSoPhong.Clear();
            txtDienTich.Clear();
            txtTang.Clear();
            txtLoaiCanHo.Clear();
            cboTrangThai.SelectedIndex = -1;
            cboHuong.SelectedIndex = -1;
            cboBlock.SelectedIndex = -1;
            txtMaCanHo.Focus();
            txtMaCanHo.Enabled = true;

            cboTrangThai.Visible = false;
            label5.Visible = false;
        }

        // Lấy dữ liệu từ form
        private CANHO GetDataFromForm()
        {
            CANHO canHo = new CANHO();

            canHo.MaCanHo = txtMaCanHo.Text.Trim();

            if (int.TryParse(txtSoPhong.Text.Trim(), out int soPhong))
            {
                canHo.SoPhong = soPhong;
            }

            if (double.TryParse(txtDienTich.Text.Trim(), out double dienTich))
            {
                canHo.DienTich = dienTich;
            }

            if (int.TryParse(txtTang.Text.Trim(), out int tang))
            {
                canHo.Tang = tang;
            }

            canHo.TrangThai = cboTrangThai.Visible ? cboTrangThai.Text : "Trống";
            canHo.LoaiCanHo = txtLoaiCanHo.Text.Trim();
            canHo.Huong = cboHuong.Text;
            canHo.Block = cboBlock.Text;

            return canHo;
        }

        // Hiển thị dữ liệu lên form
        private void DisplayDataToForm(CANHO canHo)
        {
            if (canHo != null)
            {
                txtMaCanHo.Text = canHo.MaCanHo;
                txtSoPhong.Text = canHo.SoPhong?.ToString() ?? "";
                txtDienTich.Text = canHo.DienTich?.ToString() ?? "";
                txtTang.Text = canHo.Tang?.ToString() ?? "";
                cboTrangThai.Text = canHo.TrangThai;
                txtLoaiCanHo.Text = canHo.LoaiCanHo;
                cboHuong.Text = canHo.Huong;
                cboBlock.Text = canHo.Block;
                txtMaCanHo.Enabled = false;

                cboTrangThai.Visible = true;
                label5.Visible = true;
                cboTrangThai.Text = canHo.TrangThai;
            }
        }

        // Validate dữ liệu
        private bool ValidateCanHo(CANHO canHo, bool isInsert)
        {
            // Kiểm tra mã căn hộ
            if (string.IsNullOrWhiteSpace(canHo.MaCanHo))
            {
                MessageBox.Show("Mã căn hộ không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCanHo.Focus();
                return false;
            }

            if (canHo.MaCanHo.Length > 10)
            {
                MessageBox.Show("Mã căn hộ không được vượt quá 10 ký tự!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCanHo.Focus();
                return false;
            }

            // Kiểm tra mã đã tồn tại (chỉ khi thêm mới)
            if (isInsert && db.CANHOes.Any(c => c.MaCanHo == canHo.MaCanHo))
            {
                MessageBox.Show("Mã căn hộ đã tồn tại!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCanHo.Focus();
                return false;
            }

            // Kiểm tra số phòng
            if (!canHo.SoPhong.HasValue || canHo.SoPhong.Value < 0)
            {
                MessageBox.Show("Số phòng phải là số không âm!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPhong.Focus();
                return false;
            }

            // Kiểm tra diện tích
            if (!canHo.DienTich.HasValue || canHo.DienTich.Value < 20)
            {
                MessageBox.Show("Diện tích phải lớn hơn hoặc bằng 20 m²!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienTich.Focus();
                return false;
            }

            // Kiểm tra tầng
            if (!canHo.Tang.HasValue || canHo.Tang.Value < 0)
            {
                MessageBox.Show("Tầng phải là số không âm!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTang.Focus();
                return false;
            }

            // Kiểm tra trạng thái
            if (string.IsNullOrWhiteSpace(canHo.TrangThai))
            {
                MessageBox.Show("Trạng thái không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTrangThai.Focus();
                return false;
            }

            // Kiểm tra loại căn hộ
            if (string.IsNullOrWhiteSpace(canHo.LoaiCanHo))
            {
                MessageBox.Show("Loại căn hộ không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoaiCanHo.Focus();
                return false;
            }

            // Kiểm tra hướng
            if (string.IsNullOrWhiteSpace(canHo.Huong))
            {
                MessageBox.Show("Hướng không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHuong.Focus();
                return false;
            }

            // Kiểm tra Block
            if (string.IsNullOrWhiteSpace(canHo.Block))
            {
                MessageBox.Show("Block không được để trống!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboBlock.Focus();
                return false;
            }

            return true;
        }

        // Thêm căn hộ
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                CANHO canHo = GetDataFromForm();

                if (!ValidateCanHo(canHo, true))
                {
                    return;
                }

                db.CANHOes.Add(canHo);
                db.SaveChanges();

                MessageBox.Show("Thêm căn hộ thành công!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm căn hộ: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa căn hộ
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaCanHo.Text))
                {
                    if (!cboTrangThai.Visible)
                    {
                        MessageBox.Show("Vui lòng chọn căn hộ từ danh sách để sửa!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    MessageBox.Show("Vui lòng chọn căn hộ cần sửa!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maCanHo = txtMaCanHo.Text.Trim();
                CANHO canHo = db.CANHOes.Find(maCanHo);

                if (canHo == null)
                {
                    MessageBox.Show("Không tìm thấy căn hộ!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cập nhật dữ liệu
                CANHO updatedCanHo = GetDataFromForm();

                if (!ValidateCanHo(updatedCanHo, false))
                {
                    return;
                }

                canHo.SoPhong = updatedCanHo.SoPhong;
                canHo.DienTich = updatedCanHo.DienTich;
                canHo.Tang = updatedCanHo.Tang;
                canHo.TrangThai = updatedCanHo.TrangThai;
                canHo.LoaiCanHo = updatedCanHo.LoaiCanHo;
                canHo.Huong = updatedCanHo.Huong;
                canHo.Block = updatedCanHo.Block;

                db.Entry(canHo).State = EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("Cập nhật căn hộ thành công!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật căn hộ: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa căn hộ
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaCanHo.Text))
                {
                    MessageBox.Show("Vui lòng chọn căn hộ cần xóa!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maCanHo = txtMaCanHo.Text.Trim();
                CANHO canHo = db.CANHOes.Find(maCanHo);

                if (canHo == null)
                {
                    MessageBox.Show("Không tìm thấy căn hộ!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra trạng thái trước khi xóa
                if (canHo.TrangThai?.Trim().ToLower() != "trống")
                {
                    MessageBox.Show("Chỉ được xóa căn hộ có trạng thái 'Trống'!", "Cảnh báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa căn hộ này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    db.CANHOes.Remove(canHo);
                    db.SaveChanges();

                    MessageBox.Show("Xóa căn hộ thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa căn hộ: " + ex.Message, "Lỗi", 
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

                IQueryable<CANHO> query = db.CANHOes;

                switch (cboTimKiem.SelectedIndex)
                {
                    case 0: // Tìm theo Block
                        query = query.Where(c => c.Block == timKiem);
                        break;

                    case 1: // Tìm theo Tầng
                        if (int.TryParse(timKiem, out int tang))
                        {
                            query = query.Where(c => c.Tang == tang);
                        }
                        else
                        {
                            MessageBox.Show("Tầng phải là số nguyên!", "Cảnh báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;

                    case 2: // Tìm theo Trạng thái
                        query = query.Where(c => c.TrangThai == timKiem);
                        break;

                    default:
                        MessageBox.Show("Vui lòng chọn loại tìm kiếm!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                // Chỉ lấy các thuộc tính cần thiết
                var result = query
                    .Select(c => new
                    {
                        c.MaCanHo,
                        c.SoPhong,
                        c.DienTich,
                        c.Tang,
                        c.TrangThai,
                        c.LoaiCanHo,
                        c.Huong,
                        c.Block
                    })
                    .ToList();

                dgvCanHo.DataSource = result;

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
        private void dgvCanHo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCanHo.Rows[e.RowIndex];
                
                string maCanHo = row.Cells["MaCanHo"].Value?.ToString();
                
                if (!string.IsNullOrEmpty(maCanHo))
                {
                    CANHO canHo = db.CANHOes.Find(maCanHo);

                    if (canHo != null)
                    {
                        db.Entry(canHo).Reload();
                        DisplayDataToForm(canHo);
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
