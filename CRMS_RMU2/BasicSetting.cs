using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRMS_RMU2
{
    public partial class BasicSetting : Form
    {
        public BasicSetting()
        {
            InitializeComponent();
        }

        public BasicSetting(string id)
        {
            InitializeComponent();

            //設定ファイル読み込み
            /*
            string filePath = System.IO.Path.Combine("./SETTING.txt");
            if (!System.IO.File.Exists(filePath))
            {
                FileStream fs = File.Create("./SETTING.txt");
                fs.Close();
                System.IO.StreamWriter sw = new System.IO.StreamWriter("./SETTING.txt");
                sw.WriteLine("54.95.4.11");
                sw.WriteLine("1008");
                sw.WriteLine("COM1");
                sw.Close();
            }
            */
            System.IO.StreamReader myFile = new System.IO.StreamReader("./SETTING.txt");
            textBox_IP.Text = myFile.ReadLine();
            string myID = myFile.ReadLine();  //dummy read
            textBox_ID.Text = id;
            comboBox_com.Text = myFile.ReadLine();
            myFile.Close();

            //COMポートチェック
            string[] ports = SerialPort.GetPortNames();     //COMポート一覧取得
            foreach (string port in ports)
            {
                comboBox_com.Items.Add(port);                  //COMポート一覧をコンボボックスに追加
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                string s = textBox_IP.Text + "\r\n";
                s += textBox_ID.Text + "\r\n";
                s += comboBox_com.Text + "\r\n";
                System.IO.StreamWriter myWriteFile = new System.IO.StreamWriter("./SETTING.txt");
                myWriteFile.Write(s);
                myWriteFile.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("ファイルの保存に失敗しました。\n\n" + ex.ToString(), "ファイル保存エラー");
            }
            this.Close();
        }
       
    }
}
