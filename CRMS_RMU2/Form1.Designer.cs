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
            this.button_comOpen = new System.Windows.Forms.Button();
            this.textBox_Com = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mTID設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_clear = new System.Windows.Forms.Button();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_PORT = new System.Windows.Forms.TextBox();
            this.button_Connect = new System.Windows.Forms.Button();
            this.connectLamp = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // button_comOpen
            // 
            this.button_comOpen.Location = new System.Drawing.Point(92, 35);
            this.button_comOpen.Name = "button_comOpen";
            this.button_comOpen.Size = new System.Drawing.Size(75, 23);
            this.button_comOpen.TabIndex = 1;
            this.button_comOpen.Text = "Open";
            this.button_comOpen.UseVisualStyleBackColor = true;
            this.button_comOpen.Click += new System.EventHandler(this.button_comOpen_Click);
            // 
            // textBox_Com
            // 
            this.textBox_Com.Location = new System.Drawing.Point(12, 35);
            this.textBox_Com.Name = "textBox_Com";
            this.textBox_Com.Size = new System.Drawing.Size(60, 19);
            this.textBox_Com.TabIndex = 2;
            this.textBox_Com.Text = "COM3";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTID設定ToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(452, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mTID設定ToolStripMenuItem
            // 
            this.mTID設定ToolStripMenuItem.Name = "mTID設定ToolStripMenuItem";
            this.mTID設定ToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.mTID設定ToolStripMenuItem.Text = "MTID設定";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(333, 328);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(75, 23);
            this.button_clear.TabIndex = 4;
            this.button_clear.Text = "Crear";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(36, 96);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.ReadOnly = true;
            this.textBox_IP.Size = new System.Drawing.Size(100, 19);
            this.textBox_IP.TabIndex = 5;
            this.textBox_IP.Text = "54.95.4.11";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "PORT";
            // 
            // textBox_PORT
            // 
            this.textBox_PORT.Location = new System.Drawing.Point(58, 125);
            this.textBox_PORT.Name = "textBox_PORT";
            this.textBox_PORT.ReadOnly = true;
            this.textBox_PORT.Size = new System.Drawing.Size(100, 19);
            this.textBox_PORT.TabIndex = 8;
            this.textBox_PORT.Text = "21008";
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(74, 150);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(75, 23);
            this.button_Connect.TabIndex = 9;
            this.button_Connect.Text = "Connect";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // connectLamp
            // 
            this.connectLamp.Location = new System.Drawing.Point(52, 152);
            this.connectLamp.Name = "connectLamp";
            this.connectLamp.ReadOnly = true;
            this.connectLamp.Size = new System.Drawing.Size(16, 19);
            this.connectLamp.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 364);
            this.Controls.Add(this.connectLamp);
            this.Controls.Add(this.button_Connect);
            this.Controls.Add(this.textBox_PORT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.textBox_Com);
            this.Controls.Add(this.button_comOpen);
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
        private System.Windows.Forms.Button button_comOpen;
        private System.Windows.Forms.TextBox textBox_Com;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mTID設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_PORT;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.TextBox connectLamp;
    }
}

