using DoAnHCSDL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QL_CHUNGCU
{
    public partial class FrmTongQuan : Form
    {
        QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();
        public FrmTongQuan()
        {
            InitializeComponent();
            LoadThangNam();
        }
        
        private void LoadThangNam()
        {
            // Load tháng 1-12
            for (int i = 1; i <= 12; i++)
                cbChonThang.Items.Add(i);

            // Load năm từ 2020 -> năm hiện tại
            int namHienTai = DateTime.Now.Year;
            for (int i = 2020; i <= namHienTai; i++)
                cbChonNam.Items.Add(i);

            cbChonThang.SelectedIndex = 0;
            cbChonNam.SelectedIndex = cbChonNam.Items.Count - 1;
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            int thang = int.Parse(cbChonThang.SelectedItem.ToString());
            int nam = int.Parse(cbChonNam.SelectedItem.ToString());

            // Tạo DTO và gán dữ liệu
            TongQuanDTO tongQuan = new TongQuanDTO
            {
                TongSoDanCu = GetTongSoDanCu(),
                SoCanHoTrong = GetSoCanHoTrong(),
                SoCanHoDangSuDung = GetSoCanHoDangSuDung(),
                SuCoChuaXuLy = GetSuCoChuaXuLy(thang, nam),
                TongDoanhThu = GetDoanhThuTheoThang(thang, nam)
            };

            // Hiển thị lên TextBox
            txtTongSoDanCu.Text = tongQuan.TongSoDanCu.ToString();
            txtSoCanHoTrong.Text = tongQuan.SoCanHoTrong.ToString();
            txtSoCanHoDangSuDung.Text = tongQuan.SoCanHoDangSuDung.ToString();
            txtSuCoChuaXuLy.Text = tongQuan.SuCoChuaXuLy.ToString();
            txtTongDoanhThu.Text = tongQuan.TongDoanhThu.ToString("N0");

            // Bind DataGridView và Chart
            var chiTiet = GetChiTietDoanhThu(thang, nam);
            dgvTongQuan.DataSource = chiTiet;

            chartTongQuan.DataSource = chiTiet;
            chartTongQuan.Series.Clear();
            Series series = new Series("Doanh Thu");
            series.XValueMember = "TenDichVu"; // Thay bằng tên cột phù hợp
            series.YValueMembers = "DoanhThu"; // Thay bằng tên cột phù hợp
            series.ChartType = SeriesChartType.Column;
            chartTongQuan.Series.Add(series);
            chartTongQuan.DataBind();
        }
        #region Các phương thức thực tế
        private int GetTongSoDanCu()
        {
            // Lấy tổng số cư dân
            return db.CU_DAN.Count();
        }

        private int GetSoCanHoTrong()
        {
            // Lấy số căn hộ đang trống
            return db.CANHOes.Count(ch => ch.TrangThai == "Trống");
        }

        private int GetSoCanHoDangSuDung()
        {
            // Lấy số căn hộ đang sử dụng (đang ở)
            return db.CANHOes.Count(ch => ch.TrangThai == "Đang sử dụng");
        }

        private int GetSuCoChuaXuLy(int thang, int nam)
        {
            // Lấy số sự cố chưa xử lý trong tháng/năm
            return db.SUCOes.Count(sc => sc.TrangThai == "Chưa xử lý"
                                         && sc.NgayBao.HasValue
                                         && sc.NgayBao.Value.Month == thang
                                         && sc.NgayBao.Value.Year == nam);
        }

        private decimal GetDoanhThuTheoThang(int thang, int nam)
        {
            // Lấy tổng doanh thu các hóa đơn đã thanh toán trong tháng/năm
            return db.HOADONs
                .Where(hd => hd.TrangThai == "Đã thanh toán"
                          && hd.NgayLap.HasValue
                          && hd.NgayLap.Value.Month == thang
                          && hd.NgayLap.Value.Year == nam)
                .Sum(hd => (decimal?)hd.TongTien) ?? 0;
        }

        private DataTable GetChiTietDoanhThu(int thang, int nam)
        {
            // Chi tiết doanh thu từng dịch vụ
            var query = from hd in db.HOADONs
                        join cthd in db.CT_HOADON on hd.MaHD equals cthd.MaHD
                        join dv in db.DICHVUs on cthd.MaDV equals dv.MaDV
                        where hd.TrangThai == "Đã thanh toán"
                              && hd.NgayLap.HasValue
                              && hd.NgayLap.Value.Month == thang
                              && hd.NgayLap.Value.Year == nam
                        group cthd by dv.TenDV into g
                        select new
                        {
                            TenDichVu = g.Key,
                            DoanhThu = g.Sum(x => x.ThanhTien)
                        };

            DataTable dt = new DataTable();
            dt.Columns.Add("TenDichVu");
            dt.Columns.Add("DoanhThu", typeof(decimal));

            foreach (var item in query)
            {
                dt.Rows.Add(item.TenDichVu, item.DoanhThu);
            }

            return dt;
        }
        #endregion

    }
}
