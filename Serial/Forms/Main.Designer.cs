namespace GPSParseTool
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RateCombo = new System.Windows.Forms.ComboBox();
            this.PortCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.DataList = new System.Windows.Forms.ListBox();
            this.DataTree = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ListView = new System.Windows.Forms.ListView();
            this.Attribute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RawText = new System.Windows.Forms.TextBox();
            this.LoadGPXBtn = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.ListClrBtn = new System.Windows.Forms.Button();
            this.ExportGPXBtn = new System.Windows.Forms.Button();
            this.OpenLogBtn = new System.Windows.Forms.Button();
            this.SendSerial = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // RateCombo
            // 
            this.RateCombo.FormattingEnabled = true;
            this.RateCombo.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "74880"});
            this.RateCombo.Location = new System.Drawing.Point(73, 46);
            this.RateCombo.Name = "RateCombo";
            this.RateCombo.Size = new System.Drawing.Size(121, 20);
            this.RateCombo.TabIndex = 0;
            // 
            // PortCombo
            // 
            this.PortCombo.FormattingEnabled = true;
            this.PortCombo.Location = new System.Drawing.Point(73, 20);
            this.PortCombo.Name = "PortCombo";
            this.PortCombo.Size = new System.Drawing.Size(121, 20);
            this.PortCombo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "BoardRate";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ClearBtn);
            this.groupBox1.Controls.Add(this.ConnectBtn);
            this.groupBox1.Controls.Add(this.PortCombo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.RateCombo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 107);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(104, 75);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(90, 23);
            this.ClearBtn.TabIndex = 5;
            this.ClearBtn.Text = "Stop";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(8, 75);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(90, 23);
            this.ConnectBtn.TabIndex = 4;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // DataList
            // 
            this.DataList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DataList.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DataList.FormattingEnabled = true;
            this.DataList.Location = new System.Drawing.Point(12, 125);
            this.DataList.Name = "DataList";
            this.DataList.Size = new System.Drawing.Size(200, 498);
            this.DataList.TabIndex = 5;
            this.DataList.SelectedIndexChanged += new System.EventHandler(this.DataList_SelectedIndexChanged);
            // 
            // DataTree
            // 
            this.DataTree.Location = new System.Drawing.Point(6, 20);
            this.DataTree.Name = "DataTree";
            this.DataTree.Size = new System.Drawing.Size(216, 236);
            this.DataTree.TabIndex = 6;
            this.DataTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DataTree_AfterSelect);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ListView);
            this.groupBox2.Controls.Add(this.RawText);
            this.groupBox2.Controls.Add(this.DataTree);
            this.groupBox2.Location = new System.Drawing.Point(218, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1042, 663);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
            // 
            // ListView
            // 
            this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Attribute,
            this.Value,
            this.Description});
            this.ListView.GridLines = true;
            this.ListView.HideSelection = false;
            this.ListView.Location = new System.Drawing.Point(6, 262);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.Size = new System.Drawing.Size(1030, 395);
            this.ListView.TabIndex = 8;
            this.ListView.UseCompatibleStateImageBehavior = false;
            this.ListView.View = System.Windows.Forms.View.Details;
            // 
            // Attribute
            // 
            this.Attribute.Text = "Attribute";
            this.Attribute.Width = 150;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 360;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 580;
            // 
            // RawText
            // 
            this.RawText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RawText.Location = new System.Drawing.Point(228, 20);
            this.RawText.Multiline = true;
            this.RawText.Name = "RawText";
            this.RawText.ReadOnly = true;
            this.RawText.Size = new System.Drawing.Size(808, 236);
            this.RawText.TabIndex = 7;
            // 
            // LoadGPXBtn
            // 
            this.LoadGPXBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LoadGPXBtn.BackColor = System.Drawing.SystemColors.Control;
            this.LoadGPXBtn.Location = new System.Drawing.Point(12, 652);
            this.LoadGPXBtn.Name = "LoadGPXBtn";
            this.LoadGPXBtn.Size = new System.Drawing.Size(97, 23);
            this.LoadGPXBtn.TabIndex = 7;
            this.LoadGPXBtn.Text = "View GPX";
            this.LoadGPXBtn.UseVisualStyleBackColor = false;
            this.LoadGPXBtn.Click += new System.EventHandler(this.LoadGPXBtn_Click);
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // ListClrBtn
            // 
            this.ListClrBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ListClrBtn.Location = new System.Drawing.Point(115, 652);
            this.ListClrBtn.Name = "ListClrBtn";
            this.ListClrBtn.Size = new System.Drawing.Size(97, 23);
            this.ListClrBtn.TabIndex = 8;
            this.ListClrBtn.Text = "Clear";
            this.ListClrBtn.UseVisualStyleBackColor = true;
            this.ListClrBtn.Click += new System.EventHandler(this.ListClrBtn_Click);
            // 
            // ExportGPXBtn
            // 
            this.ExportGPXBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExportGPXBtn.Location = new System.Drawing.Point(12, 626);
            this.ExportGPXBtn.Name = "ExportGPXBtn";
            this.ExportGPXBtn.Size = new System.Drawing.Size(97, 23);
            this.ExportGPXBtn.TabIndex = 9;
            this.ExportGPXBtn.Text = "Export GPX";
            this.ExportGPXBtn.UseVisualStyleBackColor = true;
            this.ExportGPXBtn.Click += new System.EventHandler(this.ExportGPXBtn_Click);
            // 
            // OpenLogBtn
            // 
            this.OpenLogBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenLogBtn.Location = new System.Drawing.Point(115, 626);
            this.OpenLogBtn.Name = "OpenLogBtn";
            this.OpenLogBtn.Size = new System.Drawing.Size(97, 23);
            this.OpenLogBtn.TabIndex = 10;
            this.OpenLogBtn.Text = "Open Log";
            this.OpenLogBtn.UseVisualStyleBackColor = true;
            this.OpenLogBtn.Click += new System.EventHandler(this.OpenLogBtn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.OpenLogBtn);
            this.Controls.Add(this.ExportGPXBtn);
            this.Controls.Add(this.ListClrBtn);
            this.Controls.Add(this.LoadGPXBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.DataList);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "GPS Parser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox RateCombo;
        private System.Windows.Forms.ComboBox PortCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.ListBox DataList;
        private System.Windows.Forms.TreeView DataTree;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button LoadGPXBtn;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.ListView ListView;
        private System.Windows.Forms.TextBox RawText;
        private System.Windows.Forms.Button ListClrBtn;
        private System.Windows.Forms.ColumnHeader Attribute;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.Button ExportGPXBtn;
        private System.Windows.Forms.Button OpenLogBtn;
        private System.IO.Ports.SerialPort SendSerial;
    }
}

