namespace GPSParseTool
{
    partial class GPXData
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GPXTree = new System.Windows.Forms.TreeView();
            this.ExportGPXBtn = new System.Windows.Forms.Button();
            this.LoadGPXBtn = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GPXTree);
            this.groupBox1.Controls.Add(this.ExportGPXBtn);
            this.groupBox1.Controls.Add(this.LoadGPXBtn);
            this.groupBox1.Controls.Add(this.webBrowser1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1240, 657);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GPX Data";
            // 
            // GPXTree
            // 
            this.GPXTree.Location = new System.Drawing.Point(3, 20);
            this.GPXTree.Name = "GPXTree";
            this.GPXTree.Size = new System.Drawing.Size(231, 602);
            this.GPXTree.TabIndex = 6;
            // 
            // ExportGPXBtn
            // 
            this.ExportGPXBtn.Location = new System.Drawing.Point(126, 628);
            this.ExportGPXBtn.Name = "ExportGPXBtn";
            this.ExportGPXBtn.Size = new System.Drawing.Size(108, 23);
            this.ExportGPXBtn.TabIndex = 5;
            this.ExportGPXBtn.Text = "View GPX";
            this.ExportGPXBtn.UseVisualStyleBackColor = true;
            this.ExportGPXBtn.Click += new System.EventHandler(this.ExportGPXBtn_Click);
            // 
            // LoadGPXBtn
            // 
            this.LoadGPXBtn.Location = new System.Drawing.Point(6, 628);
            this.LoadGPXBtn.Name = "LoadGPXBtn";
            this.LoadGPXBtn.Size = new System.Drawing.Size(114, 23);
            this.LoadGPXBtn.TabIndex = 3;
            this.LoadGPXBtn.Text = "Load GPX";
            this.LoadGPXBtn.UseVisualStyleBackColor = true;
            this.LoadGPXBtn.Click += new System.EventHandler(this.LoadGPXBtn_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(240, 20);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(994, 631);
            this.webBrowser1.TabIndex = 1;
            // 
            // GPXData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GPXData";
            this.Text = "GPXData";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button LoadGPXBtn;
        private System.Windows.Forms.Button ExportGPXBtn;
        private System.Windows.Forms.TreeView GPXTree;
    }
}