namespace QL_CHUNGCU
{
    partial class FrmBaoCaoSuCo
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
            this.btnGui = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaCanHo = new System.Windows.Forms.TextBox();
            this.txtMoTaSuCo = new System.Windows.Forms.TextBox();
            this.txtNhanVienTiepNhan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã căn hộ";
            // 
            // btnGui
            // 
            this.btnGui.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGui.Location = new System.Drawing.Point(290, 198);
            this.btnGui.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(82, 39);
            this.btnGui.TabIndex = 1;
            this.btnGui.Text = "Gửi";
            this.btnGui.UseVisualStyleBackColor = true;
            this.btnGui.Click += new System.EventHandler(this.btnGui_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mô tả sự cố";
            // 
            // txtMaCanHo
            // 
            this.txtMaCanHo.Location = new System.Drawing.Point(232, 38);
            this.txtMaCanHo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMaCanHo.Name = "txtMaCanHo";
            this.txtMaCanHo.Size = new System.Drawing.Size(391, 20);
            this.txtMaCanHo.TabIndex = 3;
            // 
            // txtMoTaSuCo
            // 
            this.txtMoTaSuCo.Location = new System.Drawing.Point(232, 90);
            this.txtMoTaSuCo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMoTaSuCo.Name = "txtMoTaSuCo";
            this.txtMoTaSuCo.Size = new System.Drawing.Size(391, 20);
            this.txtMoTaSuCo.TabIndex = 4;
            // 
            // txtNhanVienTiepNhan
            // 
            this.txtNhanVienTiepNhan.Location = new System.Drawing.Point(232, 143);
            this.txtNhanVienTiepNhan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNhanVienTiepNhan.Name = "txtNhanVienTiepNhan";
            this.txtNhanVienTiepNhan.Size = new System.Drawing.Size(391, 20);
            this.txtNhanVienTiepNhan.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 140);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nhân viên tiếp nhận";
            // 
            // FrmBaoCaoSuCo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 580);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNhanVienTiepNhan);
            this.Controls.Add(this.txtMoTaSuCo);
            this.Controls.Add(this.txtMaCanHo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmBaoCaoSuCo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaCanHo;
        private System.Windows.Forms.TextBox txtMoTaSuCo;
        private System.Windows.Forms.TextBox txtNhanVienTiepNhan;
        private System.Windows.Forms.Label label3;
    }
}