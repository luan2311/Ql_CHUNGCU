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
    public partial class FrmThongKeDanCu : Form
    {
        QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();
        public FrmThongKeDanCu()
        {
            InitializeComponent();
        }
        private void LoadChart(dynamic data)
        {
            chartThongKeDanCu.Series.Clear();
            chartThongKeDanCu.ChartAreas[0].AxisX.Interval = 1;

            Series series = new Series("Số cư dân");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;  // Hiển thị số trên cột

            foreach (var item in data)
            {
                series.Points.AddXY(item.Ten, item.SoLuong);
            }

            chartThongKeDanCu.Series.Add(series);
        }

        private void btnTheoTang_Click(object sender, EventArgs e)
        {
            var data = db.Database.SqlQuery<ThongKeDanCuTheoTangDTO>(
    @"SELECT c.Tang AS Ten, COUNT(*) AS SoLuong
      FROM CU_DAN cd
      JOIN HOPDONG h ON cd.MaCD = h.MaCD
      JOIN CANHO c ON h.MaCanHo = c.MaCanHo
      GROUP BY c.Tang
      ORDER BY c.Tang"
).ToList();



            dgvThongKeDanCu.DataSource = data;
            LoadChart(data);
        }

        private void btnTheoBlock_Click(object sender, EventArgs e)
        {
            var data = db.Database.SqlQuery<ThongKeDanCuTheoBlockDTO>(
      @"SELECT c.Block AS Ten, COUNT(*) AS SoLuong
      FROM CU_DAN cd
      JOIN HOPDONG h ON cd.MaCD = h.MaCD
      JOIN CANHO c ON h.MaCanHo = c.MaCanHo
      GROUP BY c.Block
      ORDER BY c.Block"
  ).ToList();



            dgvThongKeDanCu.DataSource = data;
            LoadChart(data);
        }
    }
}
