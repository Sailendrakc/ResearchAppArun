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
            this.parmInput = new System.Windows.Forms.TextBox();
            this.LBLparmInput = new System.Windows.Forms.Label();
            this.Btnslide = new System.Windows.Forms.Button();
            this.BtnParmSubmit = new System.Windows.Forms.Button();
            this.BtnEntryRule = new System.Windows.Forms.Button();
            this.BtnRank = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
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
            this.msgLbl.Location = new System.Drawing.Point(43, 647);
            this.msgLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.msgLbl.Name = "msgLbl";
            this.msgLbl.Size = new System.Drawing.Size(67, 17);
            this.msgLbl.TabIndex = 3;
            this.msgLbl.Text = "Console: ";
            // 
            // dg1
            // 
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Location = new System.Drawing.Point(46, 183);
            this.dg1.Name = "dg1";
            this.dg1.ReadOnly = true;
            this.dg1.RowHeadersWidth = 51;
            this.dg1.RowTemplate.Height = 24;
            this.dg1.Size = new System.Drawing.Size(1121, 450);
            this.dg1.TabIndex = 4;
            // 
            // ShowFreqTable
            // 
            this.ShowFreqTable.Location = new System.Drawing.Point(863, 26);
            this.ShowFreqTable.Name = "ShowFreqTable";
            this.ShowFreqTable.Size = new System.Drawing.Size(177, 24);
            this.ShowFreqTable.TabIndex = 5;
            this.ShowFreqTable.Text = "Show Frequency Table";
            this.ShowFreqTable.UseVisualStyleBackColor = true;
            this.ShowFreqTable.Click += new System.EventHandler(this.ShowFreqTable_Click);
            // 
            // showpmewtable
            // 
            this.showpmewtable.Location = new System.Drawing.Point(777, 67);
            this.showpmewtable.Name = "showpmewtable";
            this.showpmewtable.Size = new System.Drawing.Size(161, 23);
            this.showpmewtable.TabIndex = 6;
            this.showpmewtable.Text = "Show pmew table";
            this.showpmewtable.UseVisualStyleBackColor = true;
            this.showpmewtable.Click += new System.EventHandler(this.showpmewtable_Click);
            // 
            // showslope
            // 
            this.showslope.Location = new System.Drawing.Point(963, 67);
            this.showslope.Name = "showslope";
            this.showslope.Size = new System.Drawing.Size(177, 23);
            this.showslope.TabIndex = 7;
            this.showslope.Text = "Show Slope table";
            this.showslope.UseVisualStyleBackColor = true;
            this.showslope.Click += new System.EventHandler(this.showslope_Click);
            // 
            // TlineLBL
            // 
            this.TlineLBL.AutoSize = true;
            this.TlineLBL.Location = new System.Drawing.Point(14, 73);
            this.TlineLBL.Name = "TlineLBL";
            this.TlineLBL.Size = new System.Drawing.Size(86, 17);
            this.TlineLBL.TabIndex = 9;
            this.TlineLBL.Text = "Total Lines: ";
            // 
            // BtnShowAll
            // 
            this.BtnShowAll.Location = new System.Drawing.Point(656, 27);
            this.BtnShowAll.Name = "BtnShowAll";
            this.BtnShowAll.Size = new System.Drawing.Size(161, 23);
            this.BtnShowAll.TabIndex = 10;
            this.BtnShowAll.Text = "Show All data";
            this.BtnShowAll.UseVisualStyleBackColor = true;
            this.BtnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);
            // 
            // parmInput
            // 
            this.parmInput.Location = new System.Drawing.Point(411, 74);
            this.parmInput.Name = "parmInput";
            this.parmInput.Size = new System.Drawing.Size(222, 22);
            this.parmInput.TabIndex = 11;
            // 
            // LBLparmInput
            // 
            this.LBLparmInput.AutoSize = true;
            this.LBLparmInput.Location = new System.Drawing.Point(160, 73);
            this.LBLparmInput.Name = "LBLparmInput";
            this.LBLparmInput.Size = new System.Drawing.Size(250, 17);
            this.LBLparmInput.TabIndex = 12;
            this.LBLparmInput.Text = "Enter: Start, Jump, Len, End, ndivision";
            // 
            // Btnslide
            // 
            this.Btnslide.Location = new System.Drawing.Point(1065, 27);
            this.Btnslide.Name = "Btnslide";
            this.Btnslide.Size = new System.Drawing.Size(75, 23);
            this.Btnslide.TabIndex = 13;
            this.Btnslide.Text = "slide";
            this.Btnslide.UseVisualStyleBackColor = true;
            this.Btnslide.Click += new System.EventHandler(this.Btnslide_Click);
            // 
            // BtnParmSubmit
            // 
            this.BtnParmSubmit.Location = new System.Drawing.Point(640, 72);
            this.BtnParmSubmit.Name = "BtnParmSubmit";
            this.BtnParmSubmit.Size = new System.Drawing.Size(75, 23);
            this.BtnParmSubmit.TabIndex = 14;
            this.BtnParmSubmit.Text = "submit";
            this.BtnParmSubmit.UseVisualStyleBackColor = true;
            this.BtnParmSubmit.Click += new System.EventHandler(this.BtnParmSubmit_Click);
            // 
            // BtnEntryRule
            // 
            this.BtnEntryRule.Location = new System.Drawing.Point(46, 118);
            this.BtnEntryRule.Name = "BtnEntryRule";
            this.BtnEntryRule.Size = new System.Drawing.Size(124, 31);
            this.BtnEntryRule.TabIndex = 15;
            this.BtnEntryRule.Text = "Apply Entry rule";
            this.BtnEntryRule.UseVisualStyleBackColor = true;
            this.BtnEntryRule.Click += new System.EventHandler(this.BtnEntryRule_Click);
            // 
            // BtnRank
            // 
            this.BtnRank.Location = new System.Drawing.Point(204, 118);
            this.BtnRank.Name = "BtnRank";
            this.BtnRank.Size = new System.Drawing.Size(120, 30);
            this.BtnRank.TabIndex = 16;
            this.BtnRank.Text = "Show Rank table";
            this.BtnRank.UseVisualStyleBackColor = true;
            this.BtnRank.Click += new System.EventHandler(this.BtnRank_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1239, 673);
            this.Controls.Add(this.BtnRank);
            this.Controls.Add(this.BtnEntryRule);
            this.Controls.Add(this.BtnParmSubmit);
            this.Controls.Add(this.Btnslide);
            this.Controls.Add(this.LBLparmInput);
            this.Controls.Add(this.parmInput);
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
        private System.Windows.Forms.TextBox parmInput;
        private System.Windows.Forms.Label LBLparmInput;
        private System.Windows.Forms.Button Btnslide;
        private System.Windows.Forms.Button BtnParmSubmit;
        private System.Windows.Forms.Button BtnEntryRule;
        private System.Windows.Forms.Button BtnRank;
    }
}

