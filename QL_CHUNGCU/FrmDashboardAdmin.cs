//using QL_CHUNGCU.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;

namespace QL_CHUNGCU
{
    public partial class FrmDashboardAdmin : Form
    {
        private string quyenDangNhap; // "Admin" hoặc "User"

        public FrmDashboardAdmin(string quyen) // sửa constructor để nhận quyền
        {
            InitializeComponent();
            quyenDangNhap = quyen;
        }

        private void HideTabs()
        {
            tabMain.Appearance = TabAppearance.FlatButtons;
            tabMain.ItemSize = new Size(0, 1);
            tabMain.SizeMode = TabSizeMode.Fixed;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            HideTabs();  // Ẩn tab khi chạy chương trình

            //UC_TongQuan tongQuann = new UC_TongQuan();
            //tongQuann.Dock = DockStyle.Fill;
            //tabTongQuan.Controls.Add(tongQuann);

            //UC_TaiKhoan taiKhoan = new UC_TaiKhoan();
            //taiKhoan.Dock = DockStyle.Fill;
            //tabTaiKhoan.Controls.Add(taiKhoan);

            //UC_NhanVien nhanVien = new UC_NhanVien();
            //nhanVien.Dock = DockStyle.Fill;
            //tabNhanVien.Controls.Add(nhanVien);

            //UC_ThongKeDanCu thongKeDanCu = new UC_ThongKeDanCu();
            //thongKeDanCu.Dock = DockStyle.Fill;
            //tabThongKeDanCu.Controls.Add(thongKeDanCu);

            //UC_SuCo suCo = new UC_SuCo();
            //suCo.Dock = DockStyle.Fill;
            //tabSuCo.Controls.Add(suCo);

            //UC_BaoSuCo baoSuCo = new UC_BaoSuCo();
            //baoSuCo.Dock = DockStyle.Fill;
            //tabBaoCao.Controls.Add(baoSuCo);

            //UC_ThongKeSuCo thongKeSuCo = new UC_ThongKeSuCo();
            //thongKeSuCo.Dock = DockStyle.Fill;
            //tabThongKeSuCo.Controls.Add(thongKeSuCo);

            //UC_LichSuSuCo lichSuSuCo = new UC_LichSuSuCo();
            //lichSuSuCo.Dock = DockStyle.Fill;
            //tabLichSu.Controls.Add(lichSuSuCo);

        }
        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 0;
        }
        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 1;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 2;
        }
        private void btnThongKeDanCu_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 3;
        }

        private void btnSuCo_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 4;
        }

        private void btnBaoCaoSuCo_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 5;
        }
        private void btnThongKeSuCo_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 6;
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 7;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát không", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
