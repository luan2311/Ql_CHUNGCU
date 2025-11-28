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
    public partial class FrmSuCo : Form
    {
        QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();
        public FrmSuCo()
        {
            InitializeComponent();
            InitComboboxes();
            LoadData();

            dgvSuCo.CellClick += dgvSuCo_CellClick;
        }
        
       
        
        private void dgvSuCo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtMaSC.Text = dgvSuCo.Rows[e.RowIndex].Cells["MaSC"].Value.ToString();
            cbTrangThaiCapNhat.Text = dgvSuCo.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();
        }
        private void InitComboboxes()
        {
            cbTrangThai.Items.Clear();
            cbTrangThaiCapNhat.Items.Clear();

            string[] tt = { "Chưa xử lý", "Đang xử lý", "Hoàn thành" };

            cbTrangThai.Items.AddRange(tt);
            cbTrangThaiCapNhat.Items.AddRange(tt);
        }
        private void LoadData()
        {
            db = new QL_CHUNGCUEntities(); // refresh context

            dgvSuCo.DataSource = db.SUCOes
                .Select(s => new
                {
                    s.MaSC,
                    s.MaCanHo,
                    s.MaNV,
                    s.MoTa,
                    s.NgayBao,
                    s.TrangThai
                })
                .ToList();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string tt = cbTrangThai.Text.Trim();

            if (string.IsNullOrEmpty(tt))
            {
                LoadData();
                return;
            }

            dgvSuCo.DataSource = db.SUCOes
                .Where(s => s.TrangThai == tt)
                .Select(s => new
                {
                    s.MaSC,
                    s.MaCanHo,
                    s.MaNV,
                    s.MoTa,
                    s.NgayBao,
                    s.TrangThai
                })
                .ToList();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string id = txtMaSC.Text.Trim();

            if (id == "")
            {
                MessageBox.Show("Vui lòng chọn 1 sự cố trong bảng.");
                return;
            }

            var sc = db.SUCOes.Find(id);
            if (sc == null)
            {
                MessageBox.Show("Không tìm thấy sự cố.");
                return;
            }

            sc.TrangThai = cbTrangThaiCapNhat.Text;
            db.SaveChanges();

            LoadData();
            MessageBox.Show("Cập nhật trạng thái thành công!");
        }

        private void btnHienTatCa_Click(object sender, EventArgs e)
        {
            cbTrangThai.SelectedIndex = -1;
            cbTrangThai.Text = "";
            LoadData();
        }

    }
}
