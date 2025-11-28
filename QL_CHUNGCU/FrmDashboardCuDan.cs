//using QL_CHUNGCU.UserControls;
//using QL_CHUNGCU.UserControls_DanCu;
using QL_CHUNGCU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmDashboardCuDan : Form
    {
        private string quyenDangNhap; // lưu quyền User/Admin
        public FrmDashboardCuDan(string quyen)
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
        private void FrmDashboardCuDan_Load(object sender, EventArgs e)
        {
            HideTabs();

            //UC_ThongTin thongTin = new UC_ThongTin();
            //thongTin.Dock = DockStyle.Fill;
            //tabThongTin.Controls.Add(thongTin);

            //UC_HopDong hopDong = new UC_HopDong();
            //hopDong.Dock = DockStyle.Fill;
            //tabHopDong.Controls.Add(hopDong);

            //UC_HoaDon hoaDon = new UC_HoaDon();
            //hoaDon.Dock = DockStyle.Fill;
            //tabHoaDon.Controls.Add(hoaDon);

            //UC_SuCo_DanCu suCo_DanCu = new UC_SuCo_DanCu();
            //suCo_DanCu.Dock = DockStyle.Fill;
            //tabSuCo_DanCu.Controls.Add(suCo_DanCu);

            //UC_ThongBao thongBao = new UC_ThongBao();
            //thongBao.Dock = DockStyle.Fill;
            //tabThongBao.Controls.Add(thongBao);
        }

        private void btnThongTinNguoiO_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 0;
        }

        private void btnHopDong_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 1;
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 2;
        }

        private void btnSuCo_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 3;
        }

        private void btnThongBao_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 4;
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
