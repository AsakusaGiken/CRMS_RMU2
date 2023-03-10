namespace CRMS_RMU2
{
    partial class MTIDSetting
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
            this.button_Add = new System.Windows.Forms.Button();
            this.richTextBox_mtidList = new System.Windows.Forms.RichTextBox();
            this.textBox_Add = new System.Windows.Forms.TextBox();
            this.textBox_Delete = new System.Windows.Forms.TextBox();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(262, 56);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Add.TabIndex = 0;
            this.button_Add.Text = "追加";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // richTextBox_mtidList
            // 
            this.richTextBox_mtidList.Location = new System.Drawing.Point(29, 12);
            this.richTextBox_mtidList.Name = "richTextBox_mtidList";
            this.richTextBox_mtidList.Size = new System.Drawing.Size(111, 395);
            this.richTextBox_mtidList.TabIndex = 1;
            this.richTextBox_mtidList.Text = "";
            // 
            // textBox_Add
            // 
            this.textBox_Add.Location = new System.Drawing.Point(160, 58);
            this.textBox_Add.Name = "textBox_Add";
            this.textBox_Add.Size = new System.Drawing.Size(81, 19);
            this.textBox_Add.TabIndex = 2;
            // 
            // textBox_Delete
            // 
            this.textBox_Delete.Location = new System.Drawing.Point(160, 104);
            this.textBox_Delete.Name = "textBox_Delete";
            this.textBox_Delete.Size = new System.Drawing.Size(81, 19);
            this.textBox_Delete.TabIndex = 4;
            // 
            // button_Delete
            // 
            this.button_Delete.Location = new System.Drawing.Point(262, 102);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(75, 23);
            this.button_Delete.TabIndex = 3;
            this.button_Delete.Text = "削除";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(213, 191);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 5;
            this.button_Save.Text = "保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // MTIDSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 419);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.textBox_Delete);
            this.Controls.Add(this.button_Delete);
            this.Controls.Add(this.textBox_Add);
            this.Controls.Add(this.richTextBox_mtidList);
            this.Controls.Add(this.button_Add);
            this.Name = "MTIDSetting";
            this.Text = "MTID設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.RichTextBox richTextBox_mtidList;
        private System.Windows.Forms.TextBox textBox_Add;
        private System.Windows.Forms.TextBox textBox_Delete;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_Save;
    }
}