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
    public partial class FrmLichSuSuCo : Form
    {
        QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();
        public FrmLichSuSuCo()
        {
            InitializeComponent();
            LoadData();
        }
        
        

        private void LoadData()
        {
            //dgvLichSu.DataSource = db.LICH_SU_SUCO
            //    .Select(l => new { l.ID, l.MaSC, l.MaNV, l.NgayHoanThanh, l.GhiChu })
            //    .ToList();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            var ma = txtMaSc.Text.Trim();
            if (string.IsNullOrWhiteSpace(ma))
            {
                LoadData();
                return;
            }
            dgvLichSu.DataSource = db.LICH_SU_SUCO
                .Where(l => l.MaSC == ma)
                .Select(l => new { l.ID, l.MaSC, l.MaNV, l.NgayHoanThanh, l.GhiChu })
                .ToList();
        }

        private void btnHienTatCa_Click(object sender, EventArgs e)
        {
            txtMaSc.Clear();
            LoadData();
        }
    }
}
