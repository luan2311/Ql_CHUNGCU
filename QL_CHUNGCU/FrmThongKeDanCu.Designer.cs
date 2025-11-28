using System.Windows.Forms.DataVisualization.Charting;

namespace QL_CHUNGCU
{
    partial class FrmThongKeDanCu
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
            this.btnTheoTang = new System.Windows.Forms.Button();
            this.btnTheoBlock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvThongKeDanCu = new System.Windows.Forms.DataGridView();
            this.chartThongKeDanCu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeDanCu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKeDanCu)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTheoTang
            // 
            this.btnTheoTang.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTheoTang.Location = new System.Drawing.Point(204, 82);
            this.btnTheoTang.Margin = new System.Windows.Forms.Padding(5);
            this.btnTheoTang.Name = "btnTheoTang";
            this.btnTheoTang.Size = new System.Drawing.Size(245, 46);
            this.btnTheoTang.TabIndex = 0;
            this.btnTheoTang.Text = "Theo tầng";
            this.btnTheoTang.UseVisualStyleBackColor = true;
            this.btnTheoTang.Click += new System.EventHandler(this.btnTheoTang_Click);
            // 
            // btnTheoBlock
            // 
            this.btnTheoBlock.Location = new System.Drawing.Point(473, 82);
            this.btnTheoBlock.Margin = new System.Windows.Forms.Padding(5);
            this.btnTheoBlock.Name = "btnTheoBlock";
            this.btnTheoBlock.Size = new System.Drawing.Size(221, 46);
            this.btnTheoBlock.TabIndex = 1;
            this.btnTheoBlock.Text = "Theo tòa nhà";
            this.btnTheoBlock.UseVisualStyleBackColor = true;
            this.btnTheoBlock.Click += new System.EventHandler(this.btnTheoBlock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 102);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Thống kê theo:";
            // 
            // dgvThongKeDanCu
            // 
            this.dgvThongKeDanCu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKeDanCu.Location = new System.Drawing.Point(37, 433);
            this.dgvThongKeDanCu.Margin = new System.Windows.Forms.Padding(5);
            this.dgvThongKeDanCu.Name = "dgvThongKeDanCu";
            this.dgvThongKeDanCu.RowHeadersWidth = 51;
            this.dgvThongKeDanCu.RowTemplate.Height = 24;
            this.dgvThongKeDanCu.Size = new System.Drawing.Size(998, 315);
            this.dgvThongKeDanCu.TabIndex = 3;
            // 
            // chartThongKeDanCu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartThongKeDanCu.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartThongKeDanCu.Legends.Add(legend1);
            this.chartThongKeDanCu.Location = new System.Drawing.Point(62, 152);
            this.chartThongKeDanCu.Margin = new System.Windows.Forms.Padding(5);
            this.chartThongKeDanCu.Name = "chartThongKeDanCu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartThongKeDanCu.Series.Add(series1);
            this.chartThongKeDanCu.Size = new System.Drawing.Size(652, 242);
            this.chartThongKeDanCu.TabIndex = 4;
            this.chartThongKeDanCu.Text = "Thống Kê Dân Cư";
            // 
            // FrmThongKeDanCu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1685, 1061);
            this.Controls.Add(this.chartThongKeDanCu);
            this.Controls.Add(this.dgvThongKeDanCu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTheoBlock);
            this.Controls.Add(this.btnTheoTang);
            this.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmThongKeDanCu";
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeDanCu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKeDanCu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTheoTang;
        private System.Windows.Forms.Button btnTheoBlock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvThongKeDanCu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartThongKeDanCu;
    }
}