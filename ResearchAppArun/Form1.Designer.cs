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
            this.SuspendLayout();
            // 
            // fileLinkTB
            // 
            this.fileLinkTB.Location = new System.Drawing.Point(120, 22);
            this.fileLinkTB.Name = "fileLinkTB";
            this.fileLinkTB.ReadOnly = true;
            this.fileLinkTB.Size = new System.Drawing.Size(356, 20);
            this.fileLinkTB.TabIndex = 1;
            this.fileLinkTB.TextChanged += new System.EventHandler(this.fileLinkTB_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OpenBtn
            // 
            this.OpenBtn.Location = new System.Drawing.Point(13, 18);
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(81, 23);
            this.OpenBtn.TabIndex = 2;
            this.OpenBtn.Text = "OpenCSV";
            this.OpenBtn.UseVisualStyleBackColor = true;
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // msgLbl
            // 
            this.msgLbl.AutoSize = true;
            this.msgLbl.ForeColor = System.Drawing.Color.Red;
            this.msgLbl.Location = new System.Drawing.Point(32, 325);
            this.msgLbl.Name = "msgLbl";
            this.msgLbl.Size = new System.Drawing.Size(51, 13);
            this.msgLbl.TabIndex = 3;
            this.msgLbl.Text = "Console: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 547);
            this.Controls.Add(this.msgLbl);
            this.Controls.Add(this.OpenBtn);
            this.Controls.Add(this.fileLinkTB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox fileLinkTB;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button OpenBtn;
        private System.Windows.Forms.Label msgLbl;
    }
}

