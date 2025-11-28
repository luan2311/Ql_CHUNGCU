namespace QL_CHUNGCU
{
    partial class FrmSuCo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnHienTatCa = new System.Windows.Forms.Button();
            this.cbTrangThai = new System.Windows.Forms.ComboBox();
            this.dgvSuCo = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTrangThaiCapNhat = new System.Windows.Forms.ComboBox();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaSC = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuCo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(174, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trạng thái:";
            // 
            // btnLoc
            // 
            this.btnLoc.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoc.Location = new System.Drawing.Point(118, 78);
            this.btnLoc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(157, 46);
            this.btnLoc.TabIndex = 1;
            this.btnLoc.Text = "Lọc theo trạng thái";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // btnHienTatCa
            // 
            this.btnHienTatCa.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHienTatCa.Location = new System.Drawing.Point(424, 78);
            this.btnHienTatCa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHienTatCa.Name = "btnHienTatCa";
            this.btnHienTatCa.Size = new System.Drawing.Size(157, 46);
            this.btnHienTatCa.TabIndex = 2;
            this.btnHienTatCa.Text = "Hiện tất cả";
            this.btnHienTatCa.UseVisualStyleBackColor = true;
            this.btnHienTatCa.Click += new System.EventHandler(this.btnHienTatCa_Click);
            // 
            // cbTrangThai
            // 
            this.cbTrangThai.FormattingEnabled = true;
            this.cbTrangThai.Location = new System.Drawing.Point(339, 24);
            this.cbTrangThai.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbTrangThai.Name = "cbTrangThai";
            this.cbTrangThai.Size = new System.Drawing.Size(198, 21);
            this.cbTrangThai.TabIndex = 3;
            // 
            // dgvSuCo
            // 
            this.dgvSuCo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuCo.Location = new System.Drawing.Point(39, 271);
            this.dgvSuCo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvSuCo.Name = "dgvSuCo";
            this.dgvSuCo.RowHeadersWidth = 51;
            this.dgvSuCo.RowTemplate.Height = 24;
            this.dgvSuCo.Size = new System.Drawing.Size(718, 198);
            this.dgvSuCo.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 210);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Trạng thái mới";
            // 
            // cbTrangThaiCapNhat
            // 
            this.cbTrangThaiCapNhat.FormattingEnabled = true;
            this.cbTrangThaiCapNhat.Location = new System.Drawing.Point(235, 214);
            this.cbTrangThaiCapNhat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbTrangThaiCapNhat.Name = "cbTrangThaiCapNhat";
            this.cbTrangThaiCapNhat.Size = new System.Drawing.Size(229, 21);
            this.cbTrangThaiCapNhat.TabIndex = 6;
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat.Location = new System.Drawing.Point(528, 169);
            this.btnCapNhat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(106, 40);
            this.btnCapNhat.TabIndex = 7;
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = true;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(57, 161);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Mã sự cố";
            // 
            // txtMaSC
            // 
            this.txtMaSC.Location = new System.Drawing.Point(235, 161);
            this.txtMaSC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMaSC.Name = "txtMaSC";
            this.txtMaSC.Size = new System.Drawing.Size(229, 20);
            this.txtMaSC.TabIndex = 14;
            // 
            // FrmSuCo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 580);
            this.Controls.Add(this.txtMaSC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.cbTrangThaiCapNhat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvSuCo);
            this.Controls.Add(this.cbTrangThai);
            this.Controls.Add(this.btnHienTatCa);
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmSuCo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuCo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnHienTatCa;
        private System.Windows.Forms.ComboBox cbTrangThai;
        private System.Windows.Forms.DataGridView dgvSuCo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTrangThaiCapNhat;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaSC;
    }
}