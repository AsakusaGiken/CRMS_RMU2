namespace CRMS_RMU2
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.textBox_Com = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.基本設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mTID設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_PORT = new System.Windows.Forms.TextBox();
            this.connectLamp = new System.Windows.Forms.TextBox();
            this.comboBox_idSelect = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Conn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button_Close = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.Handshake = System.IO.Ports.Handshake.RequestToSend;
            // 
            // textBox_Com
            // 
            this.textBox_Com.Location = new System.Drawing.Point(64, 140);
            this.textBox_Com.Name = "textBox_Com";
            this.textBox_Com.ReadOnly = true;
            this.textBox_Com.Size = new System.Drawing.Size(77, 19);
            this.textBox_Com.TabIndex = 2;
            this.textBox_Com.Text = "COM3";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基本設定ToolStripMenuItem,
            this.mTID設定ToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(445, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 基本設定ToolStripMenuItem
            // 
            this.基本設定ToolStripMenuItem.Name = "基本設定ToolStripMenuItem";
            this.基本設定ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.基本設定ToolStripMenuItem.Text = "基本設定";
            this.基本設定ToolStripMenuItem.Click += new System.EventHandler(this.基本設定ToolStripMenuItem_Click);
            // 
            // mTID設定ToolStripMenuItem
            // 
            this.mTID設定ToolStripMenuItem.Name = "mTID設定ToolStripMenuItem";
            this.mTID設定ToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.mTID設定ToolStripMenuItem.Text = "MTID編集";
            this.mTID設定ToolStripMenuItem.Click += new System.EventHandler(this.mTID設定ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(42, 78);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.ReadOnly = true;
            this.textBox_IP.Size = new System.Drawing.Size(100, 19);
            this.textBox_IP.TabIndex = 5;
            this.textBox_IP.Text = "54.95.4.11";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "PORT";
            // 
            // textBox_PORT
            // 
            this.textBox_PORT.Location = new System.Drawing.Point(64, 107);
            this.textBox_PORT.Name = "textBox_PORT";
            this.textBox_PORT.ReadOnly = true;
            this.textBox_PORT.Size = new System.Drawing.Size(78, 19);
            this.textBox_PORT.TabIndex = 8;
            this.textBox_PORT.Text = "21008";
            // 
            // connectLamp
            // 
            this.connectLamp.Location = new System.Drawing.Point(182, 46);
            this.connectLamp.Name = "connectLamp";
            this.connectLamp.ReadOnly = true;
            this.connectLamp.Size = new System.Drawing.Size(16, 19);
            this.connectLamp.TabIndex = 10;
            // 
            // comboBox_idSelect
            // 
            this.comboBox_idSelect.FormattingEnabled = true;
            this.comboBox_idSelect.Location = new System.Drawing.Point(20, 46);
            this.comboBox_idSelect.Name = "comboBox_idSelect";
            this.comboBox_idSelect.Size = new System.Drawing.Size(121, 20);
            this.comboBox_idSelect.TabIndex = 12;
            this.comboBox_idSelect.TextChanged += new System.EventHandler(this.comboBox_idSelect_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "MTID選択";
            // 
            // button_Conn
            // 
            this.button_Conn.Location = new System.Drawing.Point(212, 46);
            this.button_Conn.Name = "button_Conn";
            this.button_Conn.Size = new System.Drawing.Size(75, 23);
            this.button_Conn.TabIndex = 14;
            this.button_Conn.Text = "接続";
            this.button_Conn.UseVisualStyleBackColor = true;
            this.button_Conn.Click += new System.EventHandler(this.button_Conn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "COM";
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(197, 109);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 16;
            this.button_Close.Text = "終了";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 228);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_Conn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_idSelect);
            this.Controls.Add(this.connectLamp);
            this.Controls.Add(this.textBox_PORT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.textBox_Com);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Capstone Remote Monitoring System to RMU2 Connect";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox textBox_Com;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mTID設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_PORT;
        private System.Windows.Forms.TextBox connectLamp;
        private System.Windows.Forms.ToolStripMenuItem 基本設定ToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox_idSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Conn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_Close;
    }
}

