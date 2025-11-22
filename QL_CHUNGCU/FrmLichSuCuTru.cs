using System;
using System.Linq;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmLichSuCuTru : Form
    {
        private QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();

        public FrmLichSuCuTru()
        {
            InitializeComponent();
        }

        private void FrmLichSuCuTru_Load(object sender, EventArgs e)
        {
            LoadCuDanList();
            LoadAllHistory();
        }

        // Load danh sách cư dân vào ListBox
        private void LoadCuDanList()
        {
            try
            {
                var listCuDan = db.CU_DAN
                    .OrderBy(c => c.HoTen)
                    .Select(c => new
                    {
                        c.MaCD,
                        c.HoTen,
                        DisplayText = c.MaCD + " - " + c.HoTen
                    })
                    .ToList();

                lstCuDan.DataSource = listCuDan;
                lstCuDan.DisplayMember = "DisplayText";
                lstCuDan.ValueMember = "MaCD";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách cư dân: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load tất cả lịch sử
        private void LoadAllHistory()
        {
            try
            {
                var listLichSu = db.LICH_SU_CANHO
                    .OrderByDescending(l => l.NgayChuyen)
                    .Select(l => new
                    {
                        l.MaLichSu,
                        l.MaCD,
                        HoTenCuDan = l.CU_DAN.HoTen,
                        l.MaCanHoCu,
                        l.MaCanHoMoi,
                        l.NgayChuyen,
                        l.GhiChu
                    })
                    .ToList();

                dgvLichSu.DataSource = listLichSu;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load lịch sử theo mã cư dân
        private void LoadHistoryByMaCD(string maCD)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maCD))
                {
                    LoadAllHistory();
                    return;
                }

                var listLichSu = db.LICH_SU_CANHO
                    .Where(l => l.MaCD == maCD)
                    .OrderByDescending(l => l.NgayChuyen)
                    .Select(l => new
                    {
                        l.MaLichSu,
                        l.MaCD,
                        HoTenCuDan = l.CU_DAN.HoTen,
                        l.MaCanHoCu,
                        l.MaCanHoMoi,
                        l.NgayChuyen,
                        l.GhiChu
                    })
                    .ToList();

                dgvLichSu.DataSource = listLichSu;
                FormatDataGridView();
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
                dgvLichSu.Columns["MaLichSu"].Width = 100;

                dgvLichSu.Columns["MaCD"].HeaderText = "Mã cư dân";
                dgvLichSu.Columns["MaCD"].Width = 100;

                dgvLichSu.Columns["HoTenCuDan"].HeaderText = "Họ tên";
                dgvLichSu.Columns["HoTenCuDan"].Width = 150;

                dgvLichSu.Columns["MaCanHoCu"].HeaderText = "Căn hộ cũ";
                dgvLichSu.Columns["MaCanHoCu"].Width = 100;

                dgvLichSu.Columns["MaCanHoMoi"].HeaderText = "Căn hộ mới";
                dgvLichSu.Columns["MaCanHoMoi"].Width = 100;

                dgvLichSu.Columns["NgayChuyen"].HeaderText = "Ngày chuyển";
                dgvLichSu.Columns["NgayChuyen"].Width = 120;
                dgvLichSu.Columns["NgayChuyen"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvLichSu.Columns["GhiChu"].HeaderText = "Ghi chú";
                dgvLichSu.Columns["GhiChu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        // Sự kiện chọn cư dân từ ListBox
        private void lstCuDan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCuDan.SelectedValue != null)
            {
                string maCD = lstCuDan.SelectedValue.ToString();
                LoadHistoryByMaCD(maCD);
            }
        }

        // Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadCuDanList();
            LoadAllHistory();
            lstCuDan.SelectedIndex = -1;
        }

        // Thoát
        private void btnThoat_Click(object sender, EventArgs e)
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