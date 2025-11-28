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
    public partial class FrmThongKeSuCo : Form
    {
        public FrmThongKeSuCo()
        {
            InitializeComponent();
            LoadFilter();
        }

        QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();
        
        private void LoadFilter()
        {
            // Load tháng
            for (int i = 1; i <= 12; i++)
                cbThang.Items.Add(i);

            // Load năm
            for (int y = 2020; y <= 2035; y++)
                cbNam.Items.Add(y);

            // Load nhân viên từ bảng NHANVIEN
            cbNhanVien.DataSource = db.NHANVIENs
                .Select(n => new { n.MaNV, n.HoTen })
                .ToList();
            cbNhanVien.DisplayMember = "HoTen";
            cbNhanVien.ValueMember = "MaNV";

            cbThang.SelectedIndex = 0;
            cbNam.SelectedIndex = 0;
        }
        private void LoadChart(dynamic data)
        {
            chartThongKeSuCo.Series.Clear();
            chartThongKeSuCo.ChartAreas[0].AxisX.Interval = 1;

            Series series = new Series("Số sự cố");
            series.ChartType = SeriesChartType.Column;

            foreach (var item in data)
            {
                series.Points.AddXY(item.HoTen, item.SoLuongSuCo);
            }

            chartThongKeSuCo.Series.Add(series);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            int thang = Convert.ToInt32(cbThang.Text);
            int nam = Convert.ToInt32(cbNam.Text);
            string maNV = cbNhanVien.SelectedValue.ToString();

            // Sử dụng SqlQuery thay vì function import
            var data = db.Database.SqlQuery<ThongKeSuCoResult>(
                "SELECT * FROM dbo.fn_ThongKeSuCoTheoNhanVien(@Thang, @Nam, @MaNV)",
                new System.Data.SqlClient.SqlParameter("@Thang", thang),
                new System.Data.SqlClient.SqlParameter("@Nam", nam),
                new System.Data.SqlClient.SqlParameter("@MaNV", maNV)
            ).ToList();

            dgvthongKeSuCo.DataSource = data;
            LoadChart(data);
        }

        // Thêm class DTO
        public class ThongKeSuCoResult
        {
            public string HoTen { get; set; }
            public int SoLuongSuCo { get; set; }
        }
    }
}
