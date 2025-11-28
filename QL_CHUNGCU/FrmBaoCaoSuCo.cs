using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_CHUNGCU
{
    public partial class FrmBaoCaoSuCo : Form
    {

        QL_CHUNGCUEntities db = new QL_CHUNGCUEntities();
        public FrmBaoCaoSuCo()
        {
            InitializeComponent();
        }

        

        private void btnGui_Click(object sender, EventArgs e)
        {
            string maCanHo = txtMaCanHo.Text.Trim();
            string moTa = txtMoTaSuCo.Text.Trim();

            if (string.IsNullOrEmpty(maCanHo) || string.IsNullOrEmpty(moTa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin Mã căn hộ và Mô tả sự cố.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi stored procedure sp_GhiNhanSuCo
                // Giả sử sp_GhiNhanSuCo trả về MaNV của nhân viên tiếp nhận
                var maNVParam = new SqlParameter("@MaNVTiepNhan", System.Data.SqlDbType.VarChar, 50);
                maNVParam.Direction = System.Data.ParameterDirection.Output;

                db.Database.ExecuteSqlCommand(
                    "EXEC sp_GhiNhanSuCo @MaCanHo, @MoTa, @MaNVTiepNhan OUTPUT",
                    new SqlParameter("@MaCanHo", maCanHo),
                    new SqlParameter("@MoTa", moTa),
                    maNVParam
                );

                string maNV = maNVParam.Value.ToString();

                // Hiển thị nhân viên tiếp nhận
                txtNhanVienTiepNhan.Text = maNV;

                MessageBox.Show("Ghi nhận sự cố thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa dữ liệu nhập
                txtMaCanHo.Clear();
                txtMoTaSuCo.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghi nhận sự cố: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
