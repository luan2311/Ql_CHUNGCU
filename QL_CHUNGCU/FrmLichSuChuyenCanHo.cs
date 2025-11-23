using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmLichSuChuyenCanHo : Form
    {
        private QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();

        public FrmLichSuChuyenCanHo()
        {
            InitializeComponent();
        }

        private void FrmLichSuChuyenCanHo_Load(object sender, EventArgs e)
        {
            LoadFilterComboBoxes();
            LoadAllTransferHistory();
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

        // Load tất cả lịch sử chuyển căn hộ
        private void LoadAllTransferHistory()
        {
            try
            {
                var listLichSu = db.LICH_SU_CANHO
                    .OrderByDescending(l => l.NgayChuyen)
                    .Select(l => new
                    {
                        l.MaLichSu,
                        l.MaCD,
                        TenCuDan = l.CU_DAN.HoTen,
                        l.MaCanHoCu,
                        LoaiCanHoCu = db.CANHOes.Where(c => c.MaCanHo == l.MaCanHoCu).Select(c => c.LoaiCanHo).FirstOrDefault(),
                        l.MaCanHoMoi,
                        LoaiCanHoMoi = db.CANHOes.Where(c => c.MaCanHo == l.MaCanHoMoi).Select(c => c.LoaiCanHo).FirstOrDefault(),
                        l.MaHDongCu,
                        l.MaHDongMoi,
                        l.NgayChuyen,
                        l.GhiChu
                    })
                    .ToList();

                dgvLichSu.DataSource = listLichSu;
                FormatDataGridView();
                lblTongSo.Text = "Tổng số lần chuyển căn hộ: " + listLichSu.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Định dạng DataGridView
        private void FormatDataGridView()
        {
            if (dgvLichSu.Columns.Count > 0)
            {
                dgvLichSu.Columns["MaLichSu"].HeaderText = "Mã lịch sử";
                dgvLichSu.Columns["MaLichSu"].Width = 80;

                dgvLichSu.Columns["MaCD"].HeaderText = "Mã cư dân";
                dgvLichSu.Columns["MaCD"].Width = 90;

                dgvLichSu.Columns["TenCuDan"].HeaderText = "Tên cư dân";
                dgvLichSu.Columns["TenCuDan"].Width = 150;

                dgvLichSu.Columns["MaCanHoCu"].HeaderText = "Căn hộ cũ";
                dgvLichSu.Columns["MaCanHoCu"].Width = 90;

                dgvLichSu.Columns["LoaiCanHoCu"].HeaderText = "Loại CH cũ";
                dgvLichSu.Columns["LoaiCanHoCu"].Width = 90;

                dgvLichSu.Columns["MaCanHoMoi"].HeaderText = "Căn hộ mới";
                dgvLichSu.Columns["MaCanHoMoi"].Width = 90;

                dgvLichSu.Columns["LoaiCanHoMoi"].HeaderText = "Loại CH mới";
                dgvLichSu.Columns["LoaiCanHoMoi"].Width = 90;

                dgvLichSu.Columns["MaHDongCu"].HeaderText = "HĐ cũ";
                dgvLichSu.Columns["MaHDongCu"].Width = 90;

                dgvLichSu.Columns["MaHDongMoi"].HeaderText = "HĐ mới";
                dgvLichSu.Columns["MaHDongMoi"].Width = 90;

                dgvLichSu.Columns["NgayChuyen"].HeaderText = "Ngày chuyển";
                dgvLichSu.Columns["NgayChuyen"].Width = 100;
                dgvLichSu.Columns["NgayChuyen"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvLichSu.Columns["GhiChu"].HeaderText = "Ghi chú";
                dgvLichSu.Columns["GhiChu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        // Lọc theo cư dân
        private void cboCuDan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboCuDan.SelectedIndex == -1)
                {
                    LoadAllTransferHistory();
                    return;
                }

                string maCD = cboCuDan.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(maCD)) return;

                var listLichSu = db.LICH_SU_CANHO
                    .Where(l => l.MaCD == maCD)
                    .OrderByDescending(l => l.NgayChuyen)
                    .Select(l => new
                    {
                        l.MaLichSu,
                        l.MaCD,
                        TenCuDan = l.CU_DAN.HoTen,
                        l.MaCanHoCu,
                        LoaiCanHoCu = db.CANHOes.Where(c => c.MaCanHo == l.MaCanHoCu).Select(c => c.LoaiCanHo).FirstOrDefault(),
                        l.MaCanHoMoi,
                        LoaiCanHoMoi = db.CANHOes.Where(c => c.MaCanHo == l.MaCanHoMoi).Select(c => c.LoaiCanHo).FirstOrDefault(),
                        l.MaHDongCu,
                        l.MaHDongMoi,
                        l.NgayChuyen,
                        l.GhiChu
                    })
                    .ToList();

                dgvLichSu.DataSource = listLichSu;
                FormatDataGridView();
                lblTongSo.Text = "Tổng số lần chuyển căn hộ: " + listLichSu.Count;

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

        // Lọc theo căn hộ (bao gồm cả căn hộ cũ và mới)
        private void cboCanHo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboCanHo.SelectedIndex == -1)
                {
                    LoadAllTransferHistory();
                    return;
                }

                string maCanHo = cboCanHo.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(maCanHo)) return;

                var listLichSu = db.LICH_SU_CANHO
                    .Where(l => l.MaCanHoCu == maCanHo || l.MaCanHoMoi == maCanHo)
                    .OrderByDescending(l => l.NgayChuyen)
                    .Select(l => new
                    {
                        l.MaLichSu,
                        l.MaCD,
                        TenCuDan = l.CU_DAN.HoTen,
                        l.MaCanHoCu,
                        LoaiCanHoCu = db.CANHOes.Where(c => c.MaCanHo == l.MaCanHoCu).Select(c => c.LoaiCanHo).FirstOrDefault(),
                        l.MaCanHoMoi,
                        LoaiCanHoMoi = db.CANHOes.Where(c => c.MaCanHo == l.MaCanHoMoi).Select(c => c.LoaiCanHo).FirstOrDefault(),
                        l.MaHDongCu,
                        l.MaHDongMoi,
                        l.NgayChuyen,
                        l.GhiChu
                    })
                    .ToList();

                dgvLichSu.DataSource = listLichSu;
                FormatDataGridView();
                lblTongSo.Text = "Tổng số lần chuyển căn hộ: " + listLichSu.Count;

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

                IQueryable<LICH_SU_CANHO> query = db.LICH_SU_CANHO;

                switch (cboTimKiem.SelectedIndex)
                {
                    case 0: // Tìm theo Mã cư dân
                        query = query.Where(l => l.MaCD.Contains(timKiem));
                        break;
                    case 1: // Tìm theo Mã căn hộ cũ
                        query = query.Where(l => l.MaCanHoCu == timKiem);
                        break;
                    case 2: // Tìm theo Mã căn hộ mới
                        query = query.Where(l => l.MaCanHoMoi == timKiem);
                        break;
                    case 3: // Tìm theo Tên cư dân
                        query = query.Where(l => l.CU_DAN.HoTen.Contains(timKiem));
                        break;
                }

                var listLichSu = query
                    .OrderByDescending(l => l.NgayChuyen)
                    .Select(l => new
                    {
                        l.MaLichSu,
                        l.MaCD,
                        TenCuDan = l.CU_DAN.HoTen,
                        l.MaCanHoCu,
                        LoaiCanHoCu = db.CANHOes.Where(c => c.MaCanHo == l.MaCanHoCu).Select(c => c.LoaiCanHo).FirstOrDefault(),
                        l.MaCanHoMoi,
                        LoaiCanHoMoi = db.CANHOes.Where(c => c.MaCanHo == l.MaCanHoMoi).Select(c => c.LoaiCanHo).FirstOrDefault(),
                        l.MaHDongCu,
                        l.MaHDongMoi,
                        l.NgayChuyen,
                        l.GhiChu
                    })
                    .ToList();

                dgvLichSu.DataSource = listLichSu;
                FormatDataGridView();
                lblTongSo.Text = "Tổng số lần chuyển căn hộ: " + listLichSu.Count;

                if (listLichSu.Count == 0)
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

        // Xuất Excel
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLichSu.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv",
                    FileName = "LichSuChuyenCanHo_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                    Title = "Xuất dữ liệu"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FilterIndex == 1) // Excel
                    {
                        ExportToExcel(saveFileDialog.FileName);
                    }
                    else // CSV
                    {
                        ExportToCSV(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Xuất dữ liệu thành công!\nĐường dẫn: " + saveFileDialog.FileName,
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở file sau khi xuất
                    DialogResult openFile = MessageBox.Show("Bạn có muốn mở file vừa xuất?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (openFile == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xuất sang CSV (đơn giản, không cần thư viện)
        private void ExportToCSV(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            // Thêm tiêu đề
            var headers = dgvLichSu.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            // Thêm dữ liệu
            foreach (DataGridViewRow row in dgvLichSu.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + (cell.Value?.ToString() ?? "") + "\"").ToArray()));
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        // Xuất sang Excel (sử dụng HTML format - tương thích tốt)
        private void ExportToExcel(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            // Tạo file HTML có thể mở bằng Excel
            sb.AppendLine("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv=\"content-type\" content=\"text/plain; charset=UTF-8\"/>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h2>LỊCH SỬ CHUYỂN CĂN HỘ</h2>");
            sb.AppendLine("<p>Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</p>");
            sb.AppendLine("<table border='1'>");

            // Thêm tiêu đề
            sb.AppendLine("<tr>");
            foreach (DataGridViewColumn column in dgvLichSu.Columns)
            {
                sb.AppendLine("<th>" + column.HeaderText + "</th>");
            }
            sb.AppendLine("</tr>");

            // Thêm dữ liệu
            foreach (DataGridViewRow row in dgvLichSu.Rows)
            {
                sb.AppendLine("<tr>");
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = cell.Value?.ToString() ?? "";
                    
                    // Format ngày tháng
                    if (cell.OwningColumn.Name == "NgayChuyen" && cell.Value != null && cell.Value is DateTime)
                    {
                        value = ((DateTime)cell.Value).ToString("dd/MM/yyyy");
                    }
                    
                    sb.AppendLine("<td>" + value + "</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        // Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadFilterComboBoxes();
            LoadAllTransferHistory();
            txtTimKiem.Clear();
        }

        // Xem chi tiết
        private void dgvLichSu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                int maLichSu = Convert.ToInt32(dgvLichSu.Rows[e.RowIndex].Cells["MaLichSu"].Value);
                LICH_SU_CANHO lichSu = db.LICH_SU_CANHO.Find(maLichSu);

                if (lichSu == null)
                {
                    MessageBox.Show("Không tìm thấy lịch sử!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy thông tin chi tiết
                var canHoCu = db.CANHOes.Find(lichSu.MaCanHoCu);
                var canHoMoi = db.CANHOes.Find(lichSu.MaCanHoMoi);
                var cuDan = db.CU_DAN.Find(lichSu.MaCD);

                string thongTin = $"THÔNG TIN CHI TIẾT CHUYỂN CĂN HỘ\n" +
                    $"=========================================\n\n" +
                    $"Mã lịch sử: {lichSu.MaLichSu}\n" +
                    $"Mã cư dân: {lichSu.MaCD}\n" +
                    $"Tên cư dân: {cuDan?.HoTen}\n" +
                    $"SĐT: {cuDan?.SDT}\n\n" +
                    $"--- THÔNG TIN CĂN HỘ CŨ ---\n" +
                    $"Mã căn hộ: {lichSu.MaCanHoCu}\n" +
                    $"Loại: {canHoCu?.LoaiCanHo}\n" +
                    $"Diện tích: {canHoCu?.DienTich} m²\n" +
                    $"Tầng: {canHoCu?.Tang}\n" +
                    $"Mã hợp đồng cũ: {lichSu.MaHDongCu}\n\n" +
                    $"--- THÔNG TIN CĂN HỘ MỚI ---\n" +
                    $"Mã căn hộ: {lichSu.MaCanHoMoi}\n" +
                    $"Loại: {canHoMoi?.LoaiCanHo}\n" +
                    $"Diện tích: {canHoMoi?.DienTich} m²\n" +
                    $"Tầng: {canHoMoi?.Tang}\n" +
                    $"Mã hợp đồng mới: {lichSu.MaHDongMoi}\n\n" +
                    $"Ngày chuyển: {(lichSu.NgayChuyen.HasValue ? lichSu.NgayChuyen.Value.ToString("dd/MM/yyyy") : "N/A")}\n" +
                    $"Ghi chú: {lichSu.GhiChu}";

                MessageBox.Show(thongTin, "Chi tiết chuyển căn hộ",
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
