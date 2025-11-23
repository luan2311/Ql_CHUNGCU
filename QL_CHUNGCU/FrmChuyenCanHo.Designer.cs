namespace QL_CHUNGCU
{
    partial class FrmChuyenCanHo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblCuDan;
        private System.Windows.Forms.ComboBox cboCuDan;
        private System.Windows.Forms.Label lblCanHoHienTai;
        private System.Windows.Forms.TextBox txtCanHoHienTai;
        private System.Windows.Forms.Label lblCanHoMoi;
        private System.Windows.Forms.ComboBox cboCanHoMoi;
        private System.Windows.Forms.Label lblNgayChuyen;
        private System.Windows.Forms.DateTimePicker dtpNgayChuyen;
        private System.Windows.Forms.TextBox txtMaCanHoCu;
        private System.Windows.Forms.TextBox txtMaHopDongCu;
        private System.Windows.Forms.Button btnChuyenCanHo;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblNgayChuyen = new System.Windows.Forms.Label();
            this.dtpNgayChuyen = new System.Windows.Forms.DateTimePicker();
            this.lblCanHoMoi = new System.Windows.Forms.Label();
            this.cboCanHoMoi = new System.Windows.Forms.ComboBox();
            this.lblCanHoHienTai = new System.Windows.Forms.Label();
            this.txtCanHoHienTai = new System.Windows.Forms.TextBox();
            this.lblCuDan = new System.Windows.Forms.Label();
            this.cboCuDan = new System.Windows.Forms.ComboBox();
            this.txtMaCanHoCu = new System.Windows.Forms.TextBox();
            this.txtMaHopDongCu = new System.Windows.Forms.TextBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnChuyenCanHo = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.grpThongTin);
            this.pnlMain.Controls.Add(this.txtMaCanHoCu);
            this.pnlMain.Controls.Add(this.txtMaHopDongCu);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(660, 250);
            this.pnlMain.TabIndex = 0;
            // 
            // grpThongTin
            // 
            this.grpThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTin.Controls.Add(this.lblNgayChuyen);
            this.grpThongTin.Controls.Add(this.dtpNgayChuyen);
            this.grpThongTin.Controls.Add(this.lblCanHoMoi);
            this.grpThongTin.Controls.Add(this.cboCanHoMoi);
            this.grpThongTin.Controls.Add(this.lblCanHoHienTai);
            this.grpThongTin.Controls.Add(this.txtCanHoHienTai);
            this.grpThongTin.Controls.Add(this.lblCuDan);
            this.grpThongTin.Controls.Add(this.cboCuDan);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpThongTin.Location = new System.Drawing.Point(0, 0);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(660, 250);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin chuyển căn hộ";
            // 
            // lblCuDan
            // 
            this.lblCuDan.AutoSize = true;
            this.lblCuDan.Location = new System.Drawing.Point(20, 40);
            this.lblCuDan.Name = "lblCuDan";
            this.lblCuDan.Size = new System.Drawing.Size(98, 19);
            this.lblCuDan.TabIndex = 0;
            this.lblCuDan.Text = "Chọn cư dân:";
            // 
            // cboCuDan
            // 
            this.cboCuDan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCuDan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCuDan.FormattingEnabled = true;
            this.cboCuDan.Location = new System.Drawing.Point(160, 37);
            this.cboCuDan.Name = "cboCuDan";
            this.cboCuDan.Size = new System.Drawing.Size(480, 25);
            this.cboCuDan.TabIndex = 1;
            this.cboCuDan.SelectedIndexChanged += new System.EventHandler(this.cboCuDan_SelectedIndexChanged);
            // 
            // lblCanHoHienTai
            // 
            this.lblCanHoHienTai.AutoSize = true;
            this.lblCanHoHienTai.Location = new System.Drawing.Point(20, 90);
            this.lblCanHoHienTai.Name = "lblCanHoHienTai";
            this.lblCanHoHienTai.Size = new System.Drawing.Size(113, 19);
            this.lblCanHoHienTai.TabIndex = 2;
            this.lblCanHoHienTai.Text = "Căn hộ hiện tại:";
            // 
            // txtCanHoHienTai
            // 
            this.txtCanHoHienTai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCanHoHienTai.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCanHoHienTai.Location = new System.Drawing.Point(160, 87);
            this.txtCanHoHienTai.Name = "txtCanHoHienTai";
            this.txtCanHoHienTai.ReadOnly = true;
            this.txtCanHoHienTai.Size = new System.Drawing.Size(480, 25);
            this.txtCanHoHienTai.TabIndex = 3;
            // 
            // lblCanHoMoi
            // 
            this.lblCanHoMoi.AutoSize = true;
            this.lblCanHoMoi.Location = new System.Drawing.Point(20, 140);
            this.lblCanHoMoi.Name = "lblCanHoMoi";
            this.lblCanHoMoi.Size = new System.Drawing.Size(131, 19);
            this.lblCanHoMoi.TabIndex = 4;
            this.lblCanHoMoi.Text = "Chọn căn hộ mới:";
            // 
            // cboCanHoMoi
            // 
            this.cboCanHoMoi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCanHoMoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCanHoMoi.FormattingEnabled = true;
            this.cboCanHoMoi.Location = new System.Drawing.Point(160, 137);
            this.cboCanHoMoi.Name = "cboCanHoMoi";
            this.cboCanHoMoi.Size = new System.Drawing.Size(480, 25);
            this.cboCanHoMoi.TabIndex = 5;
            // 
            // lblNgayChuyen
            // 
            this.lblNgayChuyen.AutoSize = true;
            this.lblNgayChuyen.Location = new System.Drawing.Point(20, 190);
            this.lblNgayChuyen.Name = "lblNgayChuyen";
            this.lblNgayChuyen.Size = new System.Drawing.Size(99, 19);
            this.lblNgayChuyen.TabIndex = 6;
            this.lblNgayChuyen.Text = "Ngày chuyển:";
            // 
            // dtpNgayChuyen
            // 
            this.dtpNgayChuyen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayChuyen.Location = new System.Drawing.Point(160, 187);
            this.dtpNgayChuyen.Name = "dtpNgayChuyen";
            this.dtpNgayChuyen.Size = new System.Drawing.Size(200, 25);
            this.dtpNgayChuyen.TabIndex = 7;
            // 
            // txtMaCanHoCu
            // 
            this.txtMaCanHoCu.Location = new System.Drawing.Point(0, 0);
            this.txtMaCanHoCu.Name = "txtMaCanHoCu";
            this.txtMaCanHoCu.Size = new System.Drawing.Size(0, 20);
            this.txtMaCanHoCu.TabIndex = 0;
            this.txtMaCanHoCu.Visible = false;
            // 
            // txtMaHopDongCu
            // 
            this.txtMaHopDongCu.Location = new System.Drawing.Point(0, 0);
            this.txtMaHopDongCu.Name = "txtMaHopDongCu";
            this.txtMaHopDongCu.Size = new System.Drawing.Size(0, 20);
            this.txtMaHopDongCu.TabIndex = 0;
            this.txtMaHopDongCu.Visible = false;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlButtons.Controls.Add(this.btnChuyenCanHo);
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnDong);
            this.pnlButtons.Location = new System.Drawing.Point(12, 268);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(660, 60);
            this.pnlButtons.TabIndex = 1;
            // 
            // btnChuyenCanHo
            // 
            this.btnChuyenCanHo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnChuyenCanHo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnChuyenCanHo.Location = new System.Drawing.Point(170, 10);
            this.btnChuyenCanHo.Name = "btnChuyenCanHo";
            this.btnChuyenCanHo.Size = new System.Drawing.Size(140, 40);
            this.btnChuyenCanHo.TabIndex = 0;
            this.btnChuyenCanHo.Text = "Chuyển căn hộ";
            this.btnChuyenCanHo.UseVisualStyleBackColor = true;
            this.btnChuyenCanHo.Click += new System.EventHandler(this.btnChuyenCanHo_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLamMoi.Location = new System.Drawing.Point(326, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 40);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDong.Location = new System.Drawing.Point(462, 10);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 40);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmChuyenCanHo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 341);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlMain);
            this.MinimumSize = new System.Drawing.Size(700, 380);
            this.Name = "FrmChuyenCanHo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyển căn hộ";
            this.Load += new System.EventHandler(this.FrmChuyenCanHo_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}