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
 //       int eCode;  //error code
        MyRichTextBox turbinComTextBox = new MyRichTextBox();
        string turbinComs="";
 //       string turbinCom;
        //string comBuf;
        int packetCounter;
        int MQ;
        Socket mySocket;
        bool isComOpen;
        bool isConnected;

        IPAddress ipaddress;
        IPEndPoint endpoint;


        // マニュアルリセットイベントのインスタンスを生成
        private static ManualResetEvent connectDone = new ManualResetEvent(false);  //接続シグナル用
        private static ManualResetEvent sendDone = new ManualResetEvent(false);     //送信シグナル用
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);  //受信シグナル用
        // 受信データのレスポンス
        private static string response = string.Empty;

        Socket client;
        int timerCounter = 0;

 //       int dummy=0;

        System.Timers.Timer myTimer;

        public static SerialPort mySerial;

        static byte[] txBuf;
        //int TXQ;
        static bool isPacketEnd;
        static int txPos;
        int txCnt;

        private TcpClient tcpClient;
        private NetworkStream networkStream;


        public Form1()
        {
            InitializeComponent();

            //生データ表示フィールド
            turbinComTextBox.Location = new Point(307, 48);
            turbinComTextBox.Size = new Size(118, 274);
            Controls.Add(turbinComTextBox);

            //serialPort1.BaudRate = 19200;
            serialPort1.BaudRate = 57600;
           

            //timer1.Interval = 50;
            timer1.Interval = 100;
            timer1.Enabled = false;

            myTimer = new System.Timers.Timer(300);
            myTimer.Elapsed += OnTimerEvent;


            //turbinComs = "";
            //turbinCom = "";
            //comBuf = "";
            MQ = 0;
            packetCounter = 0;

            //MTID リスト
            mtidListUpdate();

            //設定読み込み
            readBasicSettings();

            isComOpen = false;
            isConnected = false;

            mySerial = serialPort1 as SerialPort;
            

            txBuf = new byte[Constants.TXBUF_SIZE];

            isPacketEnd = false;
            txCnt = 0;
            txPos = 0;
        }

        ~Form1()
        {
            Debug.WriteLine("終了");
            tcpClient.Close();
            tcpClient = null;
            serialPort1.Close();
        }

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


        //data set delegate
        delegate void TextSet(string text);

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

        private void testDisplay()
        {
            byte[] data = new byte[1000];
            //byte[] myCom = new byte[100];
            byte[] myCom = new byte[100];

            //int dataPos = 0;
            int dataLen = serialPort1.BytesToRead;
            int packetLen = 0;
            int packetPos = 0;
            //int MQ = 0;
            //turbinComs += serialPort1.ReadLine();
            serialPort1.Read(data, 0, dataLen);
            for (int i = 0; i < dataLen; i++)
            {
                switch (MQ)
                {
                    case 0:
                        if (data[i] == 0x01)
                        {
                            MQ = 1;
                            packetLen = 0;
                        }
                        break;
                    case 1:
                        packetLen = data[i];
                        MQ = 2;
                        break;
                    case 2:
                        myCom[packetPos] = data[i];
                        packetPos++;
                        if (packetPos >= packetLen)
                        {
                            //Getstringを使って生成したstringが+=できない現象←StringBuilderを使った
                            //turbinComs += packetCounter.ToString() + System.Text.Encoding.UTF8.GetString(myCom);
                            StringBuilder sb = new StringBuilder();
                            sb.Append(packetCounter.ToString());
                            for (int j = 0; j < packetLen; j++)
                            {
                                sb.Append(Convert.ToChar(myCom[j]));
                            }
                            sb.Append("\r\n");
                            //turbinCom = str + "\r\n";
                            packetCounter++;
                            turbinComs += sb;
                            //turbinComs = String.Concat(turbinComs, str);

                            MQ = 0;
                        }
                        break;

                }
            }
            Invoke(new TextSet(turbinComTextBox.SetText), new object[] { turbinComs });
            //Invoke(new TextSet(turbinComTextBox.SetText), new object[] { turbinCom });
        }

        private void test2()
        {
            //testDisplay();
            if (isConnected)
            {
                byte[] data = new byte[1000];
                int dataLen = serialPort1.BytesToRead;
                serialPort1.Read(data, 0, dataLen);
                mySocket.Send(data, dataLen, SocketFlags.None);
            }
            else
            {
                serialPort1.DiscardInBuffer();
            }
        }

        private void ipClose()
        {
            mySocket.Shutdown(SocketShutdown.Both);
            mySocket.Close();
        }

        

        private void button_comOpen_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = textBox_Com.Text;
            serialPort1.Open();
            isComOpen = true;
            timer1.Enabled = true;
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            turbinComs = "";
            turbinComTextBox.Text = "";
        }

        
        private void connectTest()
        {
            IPAddress address = Dns.GetHostAddresses(textBox_IP.Text)[0];
            IPEndPoint serverEP = new IPEndPoint(address, Int32.Parse(textBox_PORT.Text));
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mySocket.Connect(serverEP);
            if (mySocket.Connected)
            {
                connectLamp.BackColor = Color.GreenYellow;
                isConnected = true;


            }
        }

        private void ipConnect()
        {
            // IPアドレスとポート番号を取得
            ipaddress = Dns.GetHostAddresses(textBox_IP.Text)[0];
            endpoint = new IPEndPoint(ipaddress, Int32.Parse(textBox_PORT.Text));
            // TCP/IPのソケットを作成
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mySocket.Connect(endpoint);
        }

        private void ipConnect2()
        {
            
            response = string.Empty;

            //シグナルをリセット
            connectDone.Reset();
            sendDone.Reset();
            receiveDone.Reset();
            try
            {
                // IPアドレスとポート番号を取得
                IPAddress ipaddress = Dns.GetHostAddresses(textBox_IP.Text)[0];
                IPEndPoint endpoint = new IPEndPoint(ipaddress, Int32.Parse(textBox_PORT.Text));

                // TCP/IPのソケットを作成
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // エンドポイント（IPアドレスとポート）へ接続
                client.BeginConnect(endpoint, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();  //接続シグナルになるまで待機

                // ASCIIエンコーディングで送信データをバイトの配列に変換
                //byte[] byteData = Encoding.ASCII.GetBytes(data + "<EOF>");

                // サーバーへデータを送信
                //client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
                //sendDone.WaitOne();  //送信シグナルになるまで待機

                // ソケット情報を保持する為のオブジェクトを生成
                StateObject state = new StateObject();
                state.workSocket = client;

                // サーバーからデータ受信
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                receiveDone.WaitOne();  //受信シグナルになるまで待機

                // ソケット接続終了
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                Debug.WriteLine("接続終了");
            }
            catch(Exception ex)
            {

            }
        }



        //接続
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

        //受信
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
                        mySerial.Write(buffer, 0, bytesRead);
                        //mySerial.Write(buffer, 1, bytesRead-2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                ipConnect2();
                Debug.WriteLine("接続");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            /*
            response = string.Empty;

            //シグナルをリセット
            connectDone.Reset();
            sendDone.Reset();
            receiveDone.Reset();
            try
            {
                // IPアドレスとポート番号を取得
                IPAddress ipaddress = Dns.GetHostAddresses(textBox_IP.Text)[0];
                IPEndPoint endpoint = new IPEndPoint(ipaddress, Int32.Parse(textBox_PORT.Text));

                // TCP/IPのソケットを作成
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // エンドポイント（IPアドレスとポート）へ接続
                client.BeginConnect(endpoint, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();  //接続シグナルになるまで待機

                // ASCIIエンコーディングで送信データをバイトの配列に変換
                //byte[] byteData = Encoding.ASCII.GetBytes(data + "<EOF>");

                // サーバーへデータを送信
                //client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
                //sendDone.WaitOne();  //送信シグナルになるまで待機

                // ソケット情報を保持する為のオブジェクトを生成
                StateObject state = new StateObject();
                state.workSocket = client;

                // サーバーからデータ受信
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                receiveDone.WaitOne();  //受信シグナルになるまで待機

                // ソケット接続終了
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                Debug.WriteLine("接続終了");
            }
            catch(Exception ex)
            {

            }
            */

        }
       
        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // ソケットを取得
                Socket client = (Socket)ar.AsyncState;

                // 非同期接続を終了
                client.EndConnect(ar);
                Debug.WriteLine("接続完了");


                // シグナル状態にし、メインスレッドの処理を続行する
                connectDone.Set();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // ソケットを取得
                Socket client = (Socket)ar.AsyncState;

                // 非同期送信を終了
                int bytesSent = client.EndSend(ar);
                Debug.WriteLine("送信完了");
/*
                txPos = 0;
                isPacketEnd = false;
//               clearTxBuf();
                for (int i = 0; i < Constants.TXBUF_SIZE; i++)
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
*/


                // シグナル状態にし、メインスレッドの処理を続行する
                sendDone.Set();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // ソケット情報を保持する為のオブジェクトから情報取得
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // 非同期受信を終了
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // 受信したデータを蓄積
                    //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    
                    mySerial.Write(state.buffer, 1, bytesRead-2);

                    Debug.WriteLine("受信"+bytesRead.ToString());


                    // 受信処理再開（まだ受信しているデータがあるため）
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    

                }
                /*
                else
                {
                    // 受信完了
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                        Debug.WriteLine("サーバーから「{0}」を受信", response);
                    }
                    // シグナル状態にし、メインスレッドの処理を続行する
                    receiveDone.Set();
                }
                */
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private void OnTimerEvent(Object sender, ElapsedEventArgs e)
        {
            timerCounter++;

            //if (client.Connected == false)
            if (tcpClient.Connected == false)
            {
                Debug.WriteLine("再接続");
                //ipConnect2();
                ipConnect3();
            }
            if (isComOpen)
            {
                //とりあえず受信
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
                    if (txBuf[txPos - 1] == 0x04)  //end of the packet is 0x04
                    {
                        isPacketEnd = true;
                    }
                    //send
                    if (isPacketEnd)
                    {
                        //client.Send(txBuf, 0, txPos, SocketFlags.None);

                        if (networkStream != null && networkStream.CanWrite)
                        {
                            networkStream.Write(txBuf, 0, txPos);
                            networkStream.Flush();
                        }
                        Debug.WriteLine("送信完了"+dataLen.ToString());

                        txPos = 0;
                        clearTxBuf();
                        isPacketEnd=false;
                        
                    }
                }
            }

        }

        /*
         * Timer1は使わない myTimerを使う
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timerCounter++;
                
                if(client.Connected == false)
                {
                    ipConnect2();
                }

                /*
                if (isComOpen)
                {
                    //CRMSからの受信
                    int dataLen = serialPort1.BytesToRead;
                    if (dataLen > 0)
                    {
                        byte[] data = new byte[1000];
                        serialPort1.Read(data, 0, dataLen);
                        //client.BeginSend(data, 0, dataLen, 0, new AsyncCallback(SendCallback), client);

                        if (mySocket.Connected == false)
                        {
                            mySocket.Close();
                            ipConnect2();
                            Debug.WriteLine("再接続");
                        }


                        //mySocket.Send(data, dataLen, SocketFlags.None);

                        client.BeginSend(data, 0, dataLen, 0, new AsyncCallback(SendCallback), client);
                        sendDone.WaitOne();  //送信シグナルになるまで待機



                        Debug.WriteLine("送信:"+dataLen.ToString());
                        Task.Delay(30);

                        //mySocket.Close();
                    }
                }
                */
                
            }
            catch (Exception e2)
            {
                timer1.Enabled = false;
                Debug.WriteLine(e2.ToString());
                MessageBox.Show("エラーが発生しました。\n\n" + e2.ToString(), "定期タイマーエラー");
            }
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            if(mySocket != null) { mySocket.Close(); }
        }

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
                //timer1.Enabled = true;
                myTimer.Start();
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

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            serialPort1.Close();
            mySocket.Close();
            Debug.WriteLine("切断");
        }
    }


    public class MyRichTextBox : RichTextBox
    {
        public MyRichTextBox() { }

        public void SetText(string text)
        {
            this.Text = text;
        }
        
    }

    // 非同期処理でソケット情報を保持する為のオブジェクト
    public class StateObject
    {
        // 受信バッファサイズ
        public const int BufferSize = 1024;

        // 受信バッファ
        public byte[] buffer = new byte[BufferSize];

        // 受信データ
        public StringBuilder sb = new StringBuilder();

        // ソケット
        public Socket workSocket = null;
    }

    //定数クラス
    static class Constants
    {
        public const int TXBUF_SIZE = 1000;
    }

}
