using System;
using System.Linq;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmLichSuHopDong : Form
    {
        private QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();

        public FrmLichSuHopDong()
        {
            InitializeComponent();
        }

        private void FrmLichSuHopDong_Load(object sender, EventArgs e)
        {
            LoadFilterComboBoxes();
            LoadAllTerminatedContracts();
            cboTimKiem.SelectedIndex = 0;
        }

        // Load danh sách cư dân và căn hộ cho bộ lọc
        private void LoadFilterComboBoxes()
        {
            try
            {
                // Load danh sách cư dân
                var listCuDan = db.CU_DAN
                    .OrderBy(c => c.HoTen)
                    .Select(c => new
                    {
                        c.MaCD,
                        DisplayText = c.MaCD + " - " + c.HoTen
                    })
                    .ToList();

                cboCuDan.DataSource = listCuDan;
                cboCuDan.DisplayMember = "DisplayText";
                cboCuDan.ValueMember = "MaCD";
                cboCuDan.SelectedIndex = -1;

                // Load danh sách căn hộ
                var listCanHo = db.CANHOes
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
                MessageBox.Show("Lỗi khi tải danh sách lọc: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load tất cả hợp đồng đã chấm dứt
        private void LoadAllTerminatedContracts()
        {
            try
            {
                var listHopDong = db.HOPDONGs
                    .Where(h => h.TrangThai == "Đã hủy")
                    .OrderByDescending(h => h.NgayKy)
                    .Select(h => new
                    {
                        h.MaHDong,
                        h.MaCanHo,
                        h.MaCD,
                        TenCuDan = h.CU_DAN.HoTen,
                        h.LoaiHopDong,
                        h.NgayKy,
                        h.NgayHetHan,
                        ThoiGianSuDung = h.NgayHetHan.HasValue && h.NgayKy.HasValue 
                            ? ((h.NgayHetHan.Value - h.NgayKy.Value).Days) 
                            : 0,
                        h.GiaTriHopDong,
                        h.TrangThai
                    })
                    .ToList();

                dgvHopDong.DataSource = listHopDong;
                FormatDataGridView();
                lblTongSo.Text = "Tổng số hợp đồng đã chấm dứt: " + listHopDong.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Định dạng DataGridView
        private void FormatDataGridView()
        {
            if (dgvHopDong.Columns.Count > 0)
            {
                dgvHopDong.Columns["MaHDong"].HeaderText = "Mã hợp đồng";
                dgvHopDong.Columns["MaHDong"].Width = 100;

                dgvHopDong.Columns["MaCanHo"].HeaderText = "Mã căn hộ";
                dgvHopDong.Columns["MaCanHo"].Width = 100;

                dgvHopDong.Columns["MaCD"].HeaderText = "Mã cư dân";
                dgvHopDong.Columns["MaCD"].Width = 100;

                dgvHopDong.Columns["TenCuDan"].HeaderText = "Tên cư dân";
                dgvHopDong.Columns["TenCuDan"].Width = 150;

                dgvHopDong.Columns["LoaiHopDong"].HeaderText = "Loại hợp đồng";
                dgvHopDong.Columns["LoaiHopDong"].Width = 100;

                dgvHopDong.Columns["NgayKy"].HeaderText = "Ngày ký";
                dgvHopDong.Columns["NgayKy"].Width = 100;
                dgvHopDong.Columns["NgayKy"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvHopDong.Columns["NgayHetHan"].HeaderText = "Ngày hết hạn";
                dgvHopDong.Columns["NgayHetHan"].Width = 100;
                dgvHopDong.Columns["NgayHetHan"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvHopDong.Columns["ThoiGianSuDung"].HeaderText = "Thời gian (ngày)";
                dgvHopDong.Columns["ThoiGianSuDung"].Width = 120;
                dgvHopDong.Columns["ThoiGianSuDung"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvHopDong.Columns["GiaTriHopDong"].HeaderText = "Giá trị (VNĐ)";
                dgvHopDong.Columns["GiaTriHopDong"].Width = 120;
                dgvHopDong.Columns["GiaTriHopDong"].DefaultCellStyle.Format = "N0";
                dgvHopDong.Columns["GiaTriHopDong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvHopDong.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvHopDong.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        // Lọc theo cư dân
        private void cboCuDan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboCuDan.SelectedIndex == -1)
                {
                    LoadAllTerminatedContracts();
                    return;
                }

                string maCD = cboCuDan.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(maCD)) return;

                var listHopDong = db.HOPDONGs
                    .Where(h => h.TrangThai == "Đã hủy" && h.MaCD == maCD)
                    .OrderByDescending(h => h.NgayKy)
                    .Select(h => new
                    {
                        h.MaHDong,
                        h.MaCanHo,
                        h.MaCD,
                        TenCuDan = h.CU_DAN.HoTen,
                        h.LoaiHopDong,
                        h.NgayKy,
                        h.NgayHetHan,
                        ThoiGianSuDung = h.NgayHetHan.HasValue && h.NgayKy.HasValue 
                            ? ((h.NgayHetHan.Value - h.NgayKy.Value).Days) 
                            : 0,
                        h.GiaTriHopDong,
                        h.TrangThai
                    })
                    .ToList();

                dgvHopDong.DataSource = listHopDong;
                FormatDataGridView();
                lblTongSo.Text = "Tổng số hợp đồng đã chấm dứt: " + listHopDong.Count;

                // Reset bộ lọc căn hộ
                cboCanHo.SelectedIndexChanged -= cboCanHo_SelectedIndexChanged;
                cboCanHo.SelectedIndex = -1;
                cboCanHo.SelectedIndexChanged += cboCanHo_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc theo cư dân: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lọc theo căn hộ
        private void cboCanHo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboCanHo.SelectedIndex == -1)
                {
                    LoadAllTerminatedContracts();
                    return;
                }

                string maCanHo = cboCanHo.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(maCanHo)) return;

                var listHopDong = db.HOPDONGs
                    .Where(h => h.TrangThai == "Đã hủy" && h.MaCanHo == maCanHo)
                    .OrderByDescending(h => h.NgayKy)
                    .Select(h => new
                    {
                        h.MaHDong,
                        h.MaCanHo,
                        h.MaCD,
                        TenCuDan = h.CU_DAN.HoTen,
                        h.LoaiHopDong,
                        h.NgayKy,
                        h.NgayHetHan,
                        ThoiGianSuDung = h.NgayHetHan.HasValue && h.NgayKy.HasValue 
                            ? ((h.NgayHetHan.Value - h.NgayKy.Value).Days) 
                            : 0,
                        h.GiaTriHopDong,
                        h.TrangThai
                    })
                    .ToList();

                dgvHopDong.DataSource = listHopDong;
                FormatDataGridView();
                lblTongSo.Text = "Tổng số hợp đồng đã chấm dứt: " + listHopDong.Count;

                // Reset bộ lọc cư dân
                cboCuDan.SelectedIndexChanged -= cboCuDan_SelectedIndexChanged;
                cboCuDan.SelectedIndex = -1;
                cboCuDan.SelectedIndexChanged += cboCuDan_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc theo căn hộ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                IQueryable<HOPDONG> query = db.HOPDONGs.Where(h => h.TrangThai == "Đã hủy");

                switch (cboTimKiem.SelectedIndex)
                {
                    case 0: // Tìm theo Mã hợp đồng
                        query = query.Where(h => h.MaHDong.Contains(timKiem));
                        break;
                    case 1: // Tìm theo Mã căn hộ
                        query = query.Where(h => h.MaCanHo == timKiem);
                        break;
                    case 2: // Tìm theo Mã cư dân
                        query = query.Where(h => h.MaCD == timKiem);
                        break;
                    case 3: // Tìm theo Tên cư dân
                        query = query.Where(h => h.CU_DAN.HoTen.Contains(timKiem));
                        break;
                }

                var listHopDong = query
                    .OrderByDescending(h => h.NgayKy)
                    .Select(h => new
                    {
                        h.MaHDong,
                        h.MaCanHo,
                        h.MaCD,
                        TenCuDan = h.CU_DAN.HoTen,
                        h.LoaiHopDong,
                        h.NgayKy,
                        h.NgayHetHan,
                        ThoiGianSuDung = h.NgayHetHan.HasValue && h.NgayKy.HasValue 
                            ? ((h.NgayHetHan.Value - h.NgayKy.Value).Days) 
                            : 0,
                        h.GiaTriHopDong,
                        h.TrangThai
                    })
                    .ToList();

                dgvHopDong.DataSource = listHopDong;
                FormatDataGridView();
                lblTongSo.Text = "Tổng số hợp đồng đã chấm dứt: " + listHopDong.Count;

                if (listHopDong.Count == 0)
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

        // Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadFilterComboBoxes();
            LoadAllTerminatedContracts();
            txtTimKiem.Clear();
        }

        // Xem chi tiết hợp đồng
        private void dgvHopDong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                string maHopDong = dgvHopDong.Rows[e.RowIndex].Cells["MaHDong"].Value?.ToString();
                if (string.IsNullOrEmpty(maHopDong)) return;

                HOPDONG hopDong = db.HOPDONGs.Find(maHopDong);
                if (hopDong == null)
                {
                    MessageBox.Show("Không tìm thấy hợp đồng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hiển thị thông tin chi tiết
                string thongTin = $"THÔNG TIN CHI TIẾT HỢP ĐỒNG\n" +
                    $"=====================================\n\n" +
                    $"Mã hợp đồng: {hopDong.MaHDong}\n" +
                    $"Mã căn hộ: {hopDong.MaCanHo}\n" +
                    $"Mã cư dân: {hopDong.MaCD}\n" +
                    $"Tên cư dân: {hopDong.CU_DAN?.HoTen}\n" +
                    $"Loại hợp đồng: {hopDong.LoaiHopDong}\n" +
                    $"Ngày ký: {(hopDong.NgayKy.HasValue ? hopDong.NgayKy.Value.ToString("dd/MM/yyyy") : "N/A")}\n" +
                    $"Ngày hết hạn: {(hopDong.NgayHetHan.HasValue ? hopDong.NgayHetHan.Value.ToString("dd/MM/yyyy") : "N/A")}\n" +
                    $"Thời gian sử dụng: {(hopDong.NgayHetHan.HasValue && hopDong.NgayKy.HasValue ? (hopDong.NgayHetHan.Value - hopDong.NgayKy.Value).Days + " ngày" : "N/A")}\n" +
                    $"Giá trị hợp đồng: {(hopDong.GiaTriHopDong.HasValue ? hopDong.GiaTriHopDong.Value.ToString("N0") + " VNĐ" : "N/A")}\n" +
                    $"Trạng thái: {hopDong.TrangThai}";

                MessageBox.Show(thongTin, "Chi tiết hợp đồng",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem chi tiết: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Đóng form
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Dispose DbContext khi đóng form
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            db?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
