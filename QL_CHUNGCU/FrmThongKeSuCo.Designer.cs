namespace QL_CHUNGCU
{
    partial class FrmThongKeSuCo
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cbThang = new System.Windows.Forms.ComboBox();
            this.cbNam = new System.Windows.Forms.ComboBox();
            this.cbNhanVien = new System.Windows.Forms.ComboBox();
            this.dgvthongKeSuCo = new System.Windows.Forms.DataGridView();
            this.chartThongKeSuCo = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnThongKe = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvthongKeSuCo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKeSuCo)).BeginInit();
            this.SuspendLayout();
            // 
            // cbThang
            // 
            this.cbThang.FormattingEnabled = true;
            this.cbThang.Location = new System.Drawing.Point(145, 47);
            this.cbThang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbThang.Name = "cbThang";
            this.cbThang.Size = new System.Drawing.Size(158, 21);
            this.cbThang.TabIndex = 0;
            // 
            // cbNam
            // 
            this.cbNam.FormattingEnabled = true;
            this.cbNam.Location = new System.Drawing.Point(145, 110);
            this.cbNam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbNam.Name = "cbNam";
            this.cbNam.Size = new System.Drawing.Size(158, 21);
            this.cbNam.TabIndex = 1;
            // 
            // cbNhanVien
            // 
            this.cbNhanVien.FormattingEnabled = true;
            this.cbNhanVien.Location = new System.Drawing.Point(145, 173);
            this.cbNhanVien.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbNhanVien.Name = "cbNhanVien";
            this.cbNhanVien.Size = new System.Drawing.Size(158, 21);
            this.cbNhanVien.TabIndex = 2;
            // 
            // dgvthongKeSuCo
            // 
            this.dgvthongKeSuCo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvthongKeSuCo.Location = new System.Drawing.Point(44, 317);
            this.dgvthongKeSuCo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvthongKeSuCo.Name = "dgvthongKeSuCo";
            this.dgvthongKeSuCo.RowHeadersWidth = 51;
            this.dgvthongKeSuCo.RowTemplate.Height = 24;
            this.dgvthongKeSuCo.Size = new System.Drawing.Size(506, 241);
            this.dgvthongKeSuCo.TabIndex = 3;
            // 
            // chartThongKeSuCo
            // 
            chartArea1.Name = "ChartArea1";
            this.chartThongKeSuCo.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartThongKeSuCo.Legends.Add(legend1);
            this.chartThongKeSuCo.Location = new System.Drawing.Point(393, 44);
            this.chartThongKeSuCo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chartThongKeSuCo.Name = "chartThongKeSuCo";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartThongKeSuCo.Series.Add(series1);
            this.chartThongKeSuCo.Size = new System.Drawing.Size(362, 249);
            this.chartThongKeSuCo.TabIndex = 4;
            this.chartThongKeSuCo.Text = "chart_ThongKeSuCo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tháng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Năm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 173);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nhân viên";
            // 
            // btnThongKe
            // 
            this.btnThongKe.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.Location = new System.Drawing.Point(146, 218);
            this.btnThongKe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(143, 51);
            this.btnThongKe.TabIndex = 8;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // FrmThongKeSuCo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 580);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chartThongKeSuCo);
            this.Controls.Add(this.dgvthongKeSuCo);
            this.Controls.Add(this.cbNhanVien);
            this.Controls.Add(this.cbNam);
            this.Controls.Add(this.cbThang);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmThongKeSuCo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvthongKeSuCo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKeSuCo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbThang;
        private System.Windows.Forms.ComboBox cbNam;
        private System.Windows.Forms.ComboBox cbNhanVien;
        private System.Windows.Forms.DataGridView dgvthongKeSuCo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartThongKeSuCo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnThongKe;
    }
}