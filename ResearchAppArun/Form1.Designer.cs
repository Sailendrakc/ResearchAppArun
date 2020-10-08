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
            this.fileLinkTB = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenBtn = new System.Windows.Forms.Button();
            this.msgLbl = new System.Windows.Forms.Label();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.ShowFreqTable = new System.Windows.Forms.Button();
            this.showpmewtable = new System.Windows.Forms.Button();
            this.showslope = new System.Windows.Forms.Button();
            this.TlineLBL = new System.Windows.Forms.Label();
            this.BtnShowAll = new System.Windows.Forms.Button();
            this.LBLparmInput = new System.Windows.Forms.Label();
            this.Btnslide = new System.Windows.Forms.Button();
            this.BtnParmSubmit = new System.Windows.Forms.Button();
            this.BtnEntryRule = new System.Windows.Forms.Button();
            this.BtnRank = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TBsindex = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TBWinLen = new System.Windows.Forms.TextBox();
            this.TBend = new System.Windows.Forms.TextBox();
            this.TBjump = new System.Windows.Forms.TextBox();
            this.TBequalDiv = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnToCSV = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileLinkTB
            // 
            this.fileLinkTB.Location = new System.Drawing.Point(160, 44);
            this.fileLinkTB.Margin = new System.Windows.Forms.Padding(4);
            this.fileLinkTB.Name = "fileLinkTB";
            this.fileLinkTB.ReadOnly = true;
            this.fileLinkTB.Size = new System.Drawing.Size(338, 22);
            this.fileLinkTB.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OpenBtn
            // 
            this.OpenBtn.Location = new System.Drawing.Point(17, 41);
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
            this.msgLbl.Location = new System.Drawing.Point(24, 9);
            this.msgLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.msgLbl.Name = "msgLbl";
            this.msgLbl.Size = new System.Drawing.Size(67, 17);
            this.msgLbl.TabIndex = 3;
            this.msgLbl.Text = "Console: ";
            // 
            // dg1
            // 
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Location = new System.Drawing.Point(602, 67);
            this.dg1.Name = "dg1";
            this.dg1.ReadOnly = true;
            this.dg1.RowHeadersWidth = 51;
            this.dg1.RowTemplate.Height = 24;
            this.dg1.Size = new System.Drawing.Size(1153, 530);
            this.dg1.TabIndex = 4;
            // 
            // ShowFreqTable
            // 
            this.ShowFreqTable.Location = new System.Drawing.Point(49, 439);
            this.ShowFreqTable.Name = "ShowFreqTable";
            this.ShowFreqTable.Size = new System.Drawing.Size(149, 24);
            this.ShowFreqTable.TabIndex = 5;
            this.ShowFreqTable.Text = "Frequency";
            this.ShowFreqTable.UseVisualStyleBackColor = true;
            this.ShowFreqTable.Click += new System.EventHandler(this.ShowFreqTable_Click);
            // 
            // showpmewtable
            // 
            this.showpmewtable.Location = new System.Drawing.Point(46, 491);
            this.showpmewtable.Name = "showpmewtable";
            this.showpmewtable.Size = new System.Drawing.Size(151, 23);
            this.showpmewtable.TabIndex = 6;
            this.showpmewtable.Text = "PMEWS trust";
            this.showpmewtable.UseVisualStyleBackColor = true;
            this.showpmewtable.Click += new System.EventHandler(this.showpmewtable_Click);
            // 
            // showslope
            // 
            this.showslope.Location = new System.Drawing.Point(230, 439);
            this.showslope.Name = "showslope";
            this.showslope.Size = new System.Drawing.Size(135, 24);
            this.showslope.TabIndex = 7;
            this.showslope.Text = "Slope";
            this.showslope.UseVisualStyleBackColor = true;
            this.showslope.Click += new System.EventHandler(this.showslope_Click);
            // 
            // TlineLBL
            // 
            this.TlineLBL.AutoSize = true;
            this.TlineLBL.Location = new System.Drawing.Point(24, 89);
            this.TlineLBL.Name = "TlineLBL";
            this.TlineLBL.Size = new System.Drawing.Size(86, 17);
            this.TlineLBL.TabIndex = 9;
            this.TlineLBL.Text = "Total Lines: ";
            // 
            // BtnShowAll
            // 
            this.BtnShowAll.Location = new System.Drawing.Point(242, 83);
            this.BtnShowAll.Name = "BtnShowAll";
            this.BtnShowAll.Size = new System.Drawing.Size(161, 23);
            this.BtnShowAll.TabIndex = 10;
            this.BtnShowAll.Text = "Show All data";
            this.BtnShowAll.UseVisualStyleBackColor = true;
            this.BtnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);
            // 
            // LBLparmInput
            // 
            this.LBLparmInput.AutoSize = true;
            this.LBLparmInput.Location = new System.Drawing.Point(46, 309);
            this.LBLparmInput.Name = "LBLparmInput";
            this.LBLparmInput.Size = new System.Drawing.Size(112, 17);
            this.LBLparmInput.TabIndex = 12;
            this.LBLparmInput.Text = "N equal division:";
            // 
            // Btnslide
            // 
            this.Btnslide.Location = new System.Drawing.Point(423, 83);
            this.Btnslide.Name = "Btnslide";
            this.Btnslide.Size = new System.Drawing.Size(75, 23);
            this.Btnslide.TabIndex = 13;
            this.Btnslide.Text = "slide";
            this.Btnslide.UseVisualStyleBackColor = true;
            this.Btnslide.Click += new System.EventHandler(this.Btnslide_Click);
            // 
            // BtnParmSubmit
            // 
            this.BtnParmSubmit.Location = new System.Drawing.Point(252, 337);
            this.BtnParmSubmit.Name = "BtnParmSubmit";
            this.BtnParmSubmit.Size = new System.Drawing.Size(75, 23);
            this.BtnParmSubmit.TabIndex = 14;
            this.BtnParmSubmit.Text = "submit";
            this.BtnParmSubmit.UseVisualStyleBackColor = true;
            this.BtnParmSubmit.Click += new System.EventHandler(this.BtnParmSubmit_Click);
            // 
            // BtnEntryRule
            // 
            this.BtnEntryRule.Location = new System.Drawing.Point(49, 542);
            this.BtnEntryRule.Name = "BtnEntryRule";
            this.BtnEntryRule.Size = new System.Drawing.Size(170, 31);
            this.BtnEntryRule.TabIndex = 15;
            this.BtnEntryRule.Text = "Apply Entry rule";
            this.BtnEntryRule.UseVisualStyleBackColor = true;
            this.BtnEntryRule.Click += new System.EventHandler(this.BtnEntryRule_Click);
            // 
            // BtnRank
            // 
            this.BtnRank.Location = new System.Drawing.Point(46, 589);
            this.BtnRank.Name = "BtnRank";
            this.BtnRank.Size = new System.Drawing.Size(173, 30);
            this.BtnRank.TabIndex = 16;
            this.BtnRank.Text = "Display Rank";
            this.BtnRank.UseVisualStyleBackColor = true;
            this.BtnRank.Click += new System.EventHandler(this.BtnRank_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Start Index:";
            // 
            // TBsindex
            // 
            this.TBsindex.Location = new System.Drawing.Point(252, 171);
            this.TBsindex.Name = "TBsindex";
            this.TBsindex.Size = new System.Drawing.Size(100, 22);
            this.TBsindex.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Sliding Window Length:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Sliding Window Increment:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Process window length:";
            // 
            // TBWinLen
            // 
            this.TBWinLen.Location = new System.Drawing.Point(252, 204);
            this.TBWinLen.Name = "TBWinLen";
            this.TBWinLen.Size = new System.Drawing.Size(100, 22);
            this.TBWinLen.TabIndex = 22;
            // 
            // TBend
            // 
            this.TBend.Location = new System.Drawing.Point(252, 270);
            this.TBend.Name = "TBend";
            this.TBend.Size = new System.Drawing.Size(100, 22);
            this.TBend.TabIndex = 23;
            // 
            // TBjump
            // 
            this.TBjump.Location = new System.Drawing.Point(252, 242);
            this.TBjump.Name = "TBjump";
            this.TBjump.Size = new System.Drawing.Size(100, 22);
            this.TBjump.TabIndex = 24;
            // 
            // TBequalDiv
            // 
            this.TBequalDiv.Location = new System.Drawing.Point(252, 309);
            this.TBequalDiv.Name = "TBequalDiv";
            this.TBequalDiv.Size = new System.Drawing.Size(100, 22);
            this.TBequalDiv.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 403);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 26;
            this.label5.Text = "Features:";
            // 
            // btnToCSV
            // 
            this.btnToCSV.Location = new System.Drawing.Point(252, 595);
            this.btnToCSV.Name = "btnToCSV";
            this.btnToCSV.Size = new System.Drawing.Size(75, 23);
            this.btnToCSV.TabIndex = 27;
            this.btnToCSV.Text = "ToCSV";
            this.btnToCSV.UseVisualStyleBackColor = true;
            this.btnToCSV.Click += new System.EventHandler(this.btnToCSV_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1767, 673);
            this.Controls.Add(this.btnToCSV);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TBequalDiv);
            this.Controls.Add(this.TBjump);
            this.Controls.Add(this.TBend);
            this.Controls.Add(this.TBWinLen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TBsindex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnRank);
            this.Controls.Add(this.BtnEntryRule);
            this.Controls.Add(this.BtnParmSubmit);
            this.Controls.Add(this.Btnslide);
            this.Controls.Add(this.LBLparmInput);
            this.Controls.Add(this.BtnShowAll);
            this.Controls.Add(this.TlineLBL);
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
        private System.Windows.Forms.Label TlineLBL;
        private System.Windows.Forms.Button BtnShowAll;
        private System.Windows.Forms.Label LBLparmInput;
        private System.Windows.Forms.Button Btnslide;
        private System.Windows.Forms.Button BtnParmSubmit;
        private System.Windows.Forms.Button BtnEntryRule;
        private System.Windows.Forms.Button BtnRank;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TBsindex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TBWinLen;
        private System.Windows.Forms.TextBox TBend;
        private System.Windows.Forms.TextBox TBjump;
        private System.Windows.Forms.TextBox TBequalDiv;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnToCSV;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

