namespace ResearchAppArun
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.fileLinkTB = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenBtn = new System.Windows.Forms.Button();
            this.msgLbl = new System.Windows.Forms.Label();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.ShowFreqTable = new System.Windows.Forms.Button();
            this.showpmewtable = new System.Windows.Forms.Button();
            this.showslope = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileLinkTB
            // 
            this.fileLinkTB.Location = new System.Drawing.Point(160, 27);
            this.fileLinkTB.Margin = new System.Windows.Forms.Padding(4);
            this.fileLinkTB.Name = "fileLinkTB";
            this.fileLinkTB.ReadOnly = true;
            this.fileLinkTB.Size = new System.Drawing.Size(473, 22);
            this.fileLinkTB.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OpenBtn
            // 
            this.OpenBtn.Location = new System.Drawing.Point(17, 22);
            this.OpenBtn.Margin = new System.Windows.Forms.Padding(4);
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(108, 28);
            this.OpenBtn.TabIndex = 2;
            this.OpenBtn.Text = "OpenCSV";
            this.OpenBtn.UseVisualStyleBackColor = true;
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // msgLbl
            // 
            this.msgLbl.AutoSize = true;
            this.msgLbl.ForeColor = System.Drawing.Color.Red;
            this.msgLbl.Location = new System.Drawing.Point(43, 596);
            this.msgLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.msgLbl.Name = "msgLbl";
            this.msgLbl.Size = new System.Drawing.Size(67, 17);
            this.msgLbl.TabIndex = 3;
            this.msgLbl.Text = "Console: ";
            // 
            // dg1
            // 
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Location = new System.Drawing.Point(46, 117);
            this.dg1.Name = "dg1";
            this.dg1.ReadOnly = true;
            this.dg1.RowHeadersWidth = 51;
            this.dg1.RowTemplate.Height = 24;
            this.dg1.Size = new System.Drawing.Size(1121, 450);
            this.dg1.TabIndex = 4;
            // 
            // ShowFreqTable
            // 
            this.ShowFreqTable.Location = new System.Drawing.Point(656, 26);
            this.ShowFreqTable.Name = "ShowFreqTable";
            this.ShowFreqTable.Size = new System.Drawing.Size(177, 24);
            this.ShowFreqTable.TabIndex = 5;
            this.ShowFreqTable.Text = "Show Frequency Table";
            this.ShowFreqTable.UseVisualStyleBackColor = true;
            this.ShowFreqTable.Click += new System.EventHandler(this.ShowFreqTable_Click);
            // 
            // showpmewtable
            // 
            this.showpmewtable.Location = new System.Drawing.Point(875, 26);
            this.showpmewtable.Name = "showpmewtable";
            this.showpmewtable.Size = new System.Drawing.Size(161, 23);
            this.showpmewtable.TabIndex = 6;
            this.showpmewtable.Text = "Show pmew table";
            this.showpmewtable.UseVisualStyleBackColor = true;
            this.showpmewtable.Click += new System.EventHandler(this.showpmewtable_Click);
            // 
            // showslope
            // 
            this.showslope.Location = new System.Drawing.Point(656, 74);
            this.showslope.Name = "showslope";
            this.showslope.Size = new System.Drawing.Size(177, 23);
            this.showslope.TabIndex = 7;
            this.showslope.Text = "Show Slope table";
            this.showslope.UseVisualStyleBackColor = true;
            this.showslope.Click += new System.EventHandler(this.showslope_Click);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(191, 636);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(833, 351);
            this.chart1.TabIndex = 8;
            this.chart1.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1239, 673);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.showslope);
            this.Controls.Add(this.showpmewtable);
            this.Controls.Add(this.ShowFreqTable);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.msgLbl);
            this.Controls.Add(this.OpenBtn);
            this.Controls.Add(this.fileLinkTB);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox fileLinkTB;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button OpenBtn;
        private System.Windows.Forms.Label msgLbl;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.Button ShowFreqTable;
        private System.Windows.Forms.Button showpmewtable;
        private System.Windows.Forms.Button showslope;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}

