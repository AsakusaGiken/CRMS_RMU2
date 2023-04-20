using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRMS_RMU2
{
    public partial class Form1 : Form
    {
        bool isComOpen;
        int timerCounter = 0;
        System.Timers.Timer myTimer;
        public static SerialPort mySerial;
        static byte[] txBuf;
        static bool isPacketEnd;
        static int txPos;
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        public Form1()
        {
            InitializeComponent();

            //Serial Port
            //serialPort1.BaudRate = 19200;
            serialPort1.BaudRate = 57600;
            isComOpen = false;
            mySerial = serialPort1 as SerialPort;

            //timer
            myTimer = new System.Timers.Timer(300);
            myTimer.Elapsed += OnTimerEvent;

            //MTID リスト
            mtidListUpdate();

            //設定読み込み
            readBasicSettings();

            //初期値
            txBuf = new byte[Constants.TXBUF_SIZE];
            isPacketEnd = false;
            txPos = 0;
        }

        ~Form1()
        {
            Debug.WriteLine("終了");
            serialPort1.Close();
            isComOpen=false;
            myTimer.Enabled = false;
            myTimer.Stop();
            myTimer = null;
            tcpClient.Close();
            tcpClient = null;
            
        }

        //MTID リスト
        private void mtidListUpdate()
        {
            string filePath = System.IO.Path.Combine("./MTID.txt");
            if (!System.IO.File.Exists(filePath))
            {
                FileStream fs = File.Create("./MTID.txt");
                fs.Close();
                System.IO.StreamWriter sw = new System.IO.StreamWriter("./MTID.txt");
                string[] s = new string[]
                {
                    "14","32","33","1001","1003","1007","2001","2002","2003","2004",
                    "2005","2006","2007","2008","2009","2010","2011","2012","2013","2014",
                    "2015","2016","2017","2018","2019","2020","2021","2022","2023","2024",
                    "2025","1008","1009"
                };
                for (int i = 0; i < s.Length; i++)
                {
                    sw.WriteLine(s[i]);
                }
                sw.Close();
            }
            System.IO.StreamReader myFile = new System.IO.StreamReader("./MTID.txt");
            while (myFile.Peek() >= 0)
            {
                comboBox_idSelect.Items.Add(myFile.ReadLine());
            }
            myFile.Close();
        }

        //セーブデータをロード
        private void readBasicSettings()
        {
            string filePath = System.IO.Path.Combine("./SETTING.txt");
            if (!System.IO.File.Exists(filePath))
            {
                FileStream fs = File.Create("./SETTING.txt");
                fs.Close();
                System.IO.StreamWriter f = new StreamWriter("./SETTING.txt");
                f.WriteLine("54.95.4.11");
                f.WriteLine("1008");
                f.WriteLine("COM1");
                f.Close();
            }
            System.IO.StreamReader myFile = new System.IO.StreamReader("./SETTING.txt");
            string myIP = myFile.ReadLine();
            string myID = myFile.ReadLine();
            string myCOM = myFile.ReadLine();   
            textBox_IP.Text = myIP;
            comboBox_idSelect.Text = myID;
            textBox_PORT.Text = "2" + Convert.ToInt32(myID).ToString("0000");
            textBox_Com.Text = myCOM;
            myFile.Close();
        }


        void clearTxBufAll()
        {
            for (int i = 0; i < Constants.TXBUF_SIZE; i++)
            {
                txBuf[i] = 0x00;
            }

        }

        void clearTxBuf()
        {
            for(int i=0; i<Constants.TXBUF_SIZE; i++)
            {
                if (txBuf[i] == 0x00)
                {
                    break;
                }
                else
                {
                    txBuf[i] = 0x00;
                }
            }
        }
       

        //TCP接続
        private async void ipConnect3()
        {
            try
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(textBox_IP.Text, Int32.Parse(textBox_PORT.Text));
                networkStream = tcpClient.GetStream();
                // connectButton.Enabled = false;
                ReadFromTcpAndWriteToSerial();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //TCP受信->Serial
        private async void ReadFromTcpAndWriteToSerial()
        {
            try
            {
                byte[] buffer = new byte[1024];

                while (tcpClient.Connected)
                {
                    int bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        mySerial.Write(buffer, 1, bytesRead-2);
                        Debug.WriteLine("受信:"+bytesRead.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "通信中に終了", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Serial->TCPはタイマーで定期処理
        private void OnTimerEvent(Object sender, ElapsedEventArgs e)
        {
            try
            {
                timerCounter++; //使ってない
                if (tcpClient.Connected == false)
                {
                    Debug.WriteLine("再接続");
                    //ipConnect2();
                    ipConnect3();
                }
                if (isComOpen)
                {
                    //Serial受信
                    byte[] data = new byte[1000];
                    int dataLen = serialPort1.BytesToRead;
                    if (dataLen > 0)
                    {
                        serialPort1.Read(data, 0, dataLen);
                        //copy
                        for (int i = 0; i < dataLen; i++)
                        {
                            txBuf[txPos] = data[i];
                            txPos++;
                        }
                        //end of the packet?
                        //パケット区切り（0x04）で分けないとCRMS側のヘッダエラーが出やすいので区切る
                        //そのためにtxBufに一時保存してる
                        if (txBuf[txPos - 1] == 0x04)  //end of the packet is 0x04
                        {
                            isPacketEnd = true;
                        }
                        //send
                        if (isPacketEnd)
                        {
                            //TCPへ送信
                            if (networkStream != null && networkStream.CanWrite)
                            {
                                networkStream.Write(txBuf, 0, txPos);
                                networkStream.Flush();
                            }
                            Debug.WriteLine("送信完了" + dataLen.ToString());

                            txPos = 0;
                            clearTxBuf();
                            isPacketEnd = false;

                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "通信中に終了", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //以下ツールバー関連
        private void mTID設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MTIDSetting myMtidForm = new MTIDSetting();
            myMtidForm.ShowDialog();
            mtidListUpdate();
        }

        private void 基本設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicSetting myBasicSetting = new BasicSetting(comboBox_idSelect.Text);
            myBasicSetting.ShowDialog();
            readBasicSettings();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help myHelp = new Help();
            myHelp.ShowDialog();
        }

        private void comboBox_idSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_idSelect_TextChanged(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(comboBox_idSelect.Text);
            string s = n.ToString("0000");
           textBox_PORT.Text = "2" + s;
        }

        private void comboBox_idSelect_DropDownClosed(object sender, EventArgs e)
        {

        }

        //接続
        private void button_Conn_Click(object sender, EventArgs e)
        {

            try
            {
                //COM open
                serialPort1.PortName = textBox_Com.Text;
                serialPort1.Open();
                serialPort1.DiscardInBuffer();
                isComOpen = true;
                clearTxBufAll();
                myTimer.Start();
                connectLamp.BackColor = Color.GreenYellow;
            }
            catch(Exception e1)
            {
                MessageBox.Show("COMオープンに失敗しました。\n\n" + e1.ToString(), "COMオープンエラー");
            }

            try
            {
                //IO connect
                Debug.WriteLine("接続");
                //ipConnect2();
                ipConnect3();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("IP接続に失敗しました。\n\n" + ex.ToString(), "IP接続エラー");
            }

        }

        //終了
        private void button_Close_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("終了");
            serialPort1.Close();
            isComOpen = false;
            myTimer.Enabled = false;
            myTimer.Stop();
            myTimer = null;
            tcpClient.Close();
            tcpClient = null;
            this.Close();
        }
    }

    //定数クラス
    static class Constants
    {
        public const int TXBUF_SIZE = 1000;
    }

}
