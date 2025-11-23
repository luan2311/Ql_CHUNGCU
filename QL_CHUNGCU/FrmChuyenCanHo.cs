using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmChuyenCanHo : Form
    {
        private QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();

        public FrmChuyenCanHo()
        {
            InitializeComponent();
        }

        private void FrmChuyenCanHo_Load(object sender, EventArgs e)
        {
            LoadCuDanComboBox();
            LoadCanHoMoiComboBox();
            dtpNgayChuyen.Value = DateTime.Now;
            ClearForm();
        }

        // Load danh sách cư dân có hợp đồng hiệu lực
        private void LoadCuDanComboBox()
        {
            try
            {
                var listCuDan = db.CU_DAN
                    .Where(c => c.HOPDONGs.Any(h => h.TrangThai == "Hiệu lực"))
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

        // Load danh sách căn hộ trống
        private void LoadCanHoMoiComboBox()
        {
            try
            {
                var listCanHo = db.CANHOes
                    .Where(c => c.TrangThai == "Trống")
                    .OrderBy(c => c.MaCanHo)
                    .Select(c => new
                    {
                        c.MaCanHo,
                        DisplayText = c.MaCanHo + " - " + c.LoaiCanHo + " (Tầng " + c.Tang + ", " + c.DienTich + "m²)"
                    })
                    .ToList();

                cboCanHoMoi.DataSource = listCanHo;
                cboCanHoMoi.DisplayMember = "DisplayText";
                cboCanHoMoi.ValueMember = "MaCanHo";
                cboCanHoMoi.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách căn hộ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Khi chọn cư dân, tự động load thông tin căn hộ hiện tại
        private void cboCuDan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboCuDan.SelectedIndex == -1) return;

                string maCD = cboCuDan.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(maCD)) return;

                // Lấy hợp đồng hiệu lực của cư dân
                var hopDong = db.HOPDONGs
                    .FirstOrDefault(h => h.MaCD == maCD && h.TrangThai == "Hiệu lực");

                if (hopDong != null)
                {
                    txtMaHopDongCu.Text = hopDong.MaHDong;
                    
                    var canHoCu = db.CANHOes.Find(hopDong.MaCanHo);
                    if (canHoCu != null)
                    {
                        txtCanHoHienTai.Text = canHoCu.MaCanHo + " - " + canHoCu.LoaiCanHo + 
                            " (Tầng " + canHoCu.Tang + ", " + canHoCu.DienTich + "m²)";
                        txtMaCanHoCu.Text = canHoCu.MaCanHo;
                    }
                }
                else
                {
                    MessageBox.Show("Cư dân này không có hợp đồng hiệu lực!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin căn hộ hiện tại: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Validate dữ liệu
        private bool ValidateData()
        {
            // Kiểm tra cư dân
            if (cboCuDan.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn cư dân!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCuDan.Focus();
                return false;
            }

            // Kiểm tra căn hộ hiện tại
            if (string.IsNullOrWhiteSpace(txtMaCanHoCu.Text))
            {
                MessageBox.Show("Không tìm thấy căn hộ hiện tại của cư dân!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra căn hộ mới
            if (cboCanHoMoi.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn căn hộ mới!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCanHoMoi.Focus();
                return false;
            }

            string maCanHoMoi = cboCanHoMoi.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(maCanHoMoi))
            {
                MessageBox.Show("Mã căn hộ mới không hợp lệ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra căn hộ mới phải khác căn hộ cũ
            if (txtMaCanHoCu.Text.Trim() == maCanHoMoi)
            {
                MessageBox.Show("Căn hộ mới phải khác căn hộ hiện tại!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCanHoMoi.Focus();
                return false;
            }

            // Kiểm tra căn hộ mới phải có trạng thái "Trống"
            var canHoMoi = db.CANHOes.Find(maCanHoMoi);
            if (canHoMoi == null)
            {
                MessageBox.Show("Căn hộ mới không tồn tại!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (canHoMoi.TrangThai?.Trim().ToLower() != "trống")
            {
                MessageBox.Show("Căn hộ mới phải có trạng thái 'Trống'!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCanHoMoi.Focus();
                return false;
            }

            // Kiểm tra ngày chuyển
            if (dtpNgayChuyen.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày chuyển không được lớn hơn ngày hiện tại!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayChuyen.Focus();
                return false;
            }

            return true;
        }

        // Thực hiện chuyển căn hộ
        private void btnChuyenCanHo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateData())
                {
                    return;
                }

                string maCD = cboCuDan.SelectedValue.ToString();
                string maCanHoCu = txtMaCanHoCu.Text.Trim();
                string maCanHoMoi = cboCanHoMoi.SelectedValue.ToString();
                string maHopDongCu = txtMaHopDongCu.Text.Trim();
                DateTime ngayChuyen = dtpNgayChuyen.Value;

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn chuyển cư dân từ căn hộ {maCanHoCu} sang căn hộ {maCanHoMoi}?\n\n" +
                    "Hệ thống sẽ:\n" +
                    "- Tạo hợp đồng mới cho căn hộ mới\n" +
                    "- Cập nhật trạng thái hợp đồng cũ\n" +
                    "- Cập nhật trạng thái các căn hộ\n" +
                    "- Lưu lịch sử chuyển căn hộ",
                    "Xác nhận chuyển căn hộ",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                // Gọi stored procedure sp_ChuyenCanHo
                // Nếu bạn đã tạo stored procedure trong database, sử dụng như sau:
                // db.sp_ChuyenCanHo(maCD, maCanHoCu, maCanHoMoi, ngayChuyen);
                // db.SaveChanges();

                // Nếu chưa có stored procedure, thực hiện manual:
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // 1. Lấy hợp đồng cũ
                        var hopDongCu = db.HOPDONGs.Find(maHopDongCu);
                        if (hopDongCu == null)
                        {
                            throw new Exception("Không tìm thấy hợp đồng cũ!");
                        }

                        // 2. Cập nhật trạng thái hợp đồng cũ
                        hopDongCu.TrangThai = "Đã hủy";
                        db.Entry(hopDongCu).State = EntityState.Modified;

                        // 3. Tạo mã hợp đồng mới
                        string maHopDongMoi = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");

                        // 4. Tạo hợp đồng mới
                        HOPDONG hopDongMoi = new HOPDONG
                        {
                            MaHDong = maHopDongMoi,
                            MaCanHo = maCanHoMoi,
                            MaCD = maCD,
                            LoaiHopDong = hopDongCu.LoaiHopDong,
                            NgayKy = ngayChuyen,
                            NgayHetHan = hopDongCu.NgayHetHan, // Giữ nguyên ngày hết hạn
                            GiaTriHopDong = hopDongCu.GiaTriHopDong,
                            TrangThai = "Hiệu lực"
                        };
                        db.HOPDONGs.Add(hopDongMoi);

                        // 5. Cập nhật trạng thái căn hộ cũ
                        var canHoCu = db.CANHOes.Find(maCanHoCu);
                        if (canHoCu != null)
                        {
                            canHoCu.TrangThai = "Trống";
                            db.Entry(canHoCu).State = EntityState.Modified;
                        }

                        // 6. Cập nhật trạng thái căn hộ mới
                        var canHoMoi = db.CANHOes.Find(maCanHoMoi);
                        if (canHoMoi != null)
                        {
                            canHoMoi.TrangThai = "Đã thuê";
                            db.Entry(canHoMoi).State = EntityState.Modified;
                        }

                        // 7. Lưu lịch sử cư trú
                        LICH_SU_CANHO lichSu = new LICH_SU_CANHO
                        {
                            MaCD = maCD,
                            MaCanHoCu = maCanHoCu,
                            MaCanHoMoi = maCanHoMoi,
                            MaHDongCu = maHopDongCu,
                            MaHDongMoi = maHopDongMoi,
                            NgayChuyen = ngayChuyen
                        };
                        db.LICH_SU_CANHO.Add(lichSu);

                        // 8. Lưu tất cả thay đổi
                        db.SaveChanges();
                        transaction.Commit();

                        MessageBox.Show(
                            $"Chuyển căn hộ thành công!\n\n" +
                            $"Hợp đồng mới: {maHopDongMoi}\n" +
                            $"Căn hộ mới: {maCanHoMoi}\n" +
                            $"Ngày chuyển: {ngayChuyen:dd/MM/yyyy}",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        ClearForm();
                        LoadCuDanComboBox();
                        LoadCanHoMoiComboBox();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Lỗi trong quá trình chuyển căn hộ: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuyển căn hộ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa form
        private void ClearForm()
        {
            cboCuDan.SelectedIndex = -1;
            txtCanHoHienTai.Clear();
            txtMaCanHoCu.Clear();
            txtMaHopDongCu.Clear();
            cboCanHoMoi.SelectedIndex = -1;
            dtpNgayChuyen.Value = DateTime.Now;
            cboCuDan.Focus();
        }

        // Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadCuDanComboBox();
            LoadCanHoMoiComboBox();
        }

        // Đóng form
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
