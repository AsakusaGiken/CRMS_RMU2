using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMS_RMU2
{
    public partial class MTIDSetting : Form
    {
        String mtidList;
        
        public MTIDSetting()
        {
            InitializeComponent();

            string filePath = System.IO.Path.Combine("./MTID.txt");
            if (!System.IO.File.Exists(filePath))
            {
                FileStream fs = File.Create("./MTID.txt");
                fs.Close();
                
            }
            System.IO.StreamReader myFile = new System.IO.StreamReader("./MTID.txt");
            while (myFile.Peek() >= 0)
            {
                mtidList += myFile.ReadLine() + "\r\n";
                richTextBox_mtidList.Text = mtidList;
            }
            myFile.Close();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            mtidList += textBox_Add.Text +"\r\n";
            richTextBox_mtidList.Text = mtidList;
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            mtidList = mtidList.Replace(textBox_Delete.Text+"\r\n", "");
            richTextBox_mtidList.Text = mtidList;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.StreamWriter myWriteFile = new System.IO.StreamWriter("./MTID.txt");
                myWriteFile.Write(mtidList);
                myWriteFile.Close();
            }catch(Exception ex)
            {
                MessageBox.Show("ファイルの保存に失敗しました。\n\n" + ex.ToString(), "ファイル保存エラー");
            }
            this.Close();
        }
    }
}
