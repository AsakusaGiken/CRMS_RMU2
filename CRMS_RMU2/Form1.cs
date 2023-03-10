using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMS_RMU2
{
    public partial class Form1 : Form
    {
        int eCode;  //error code
        MyRichTextBox turbinComTextBox = new MyRichTextBox();
        string turbinComs="";
        string turbinCom;
        //string comBuf;
        int packetCounter;
        int MQ;
        Socket mySocket;
        bool isComOpen;
        bool isConnected;

        public Form1()
        {
            InitializeComponent();

            //生データ表示フィールド
            turbinComTextBox.Location = new Point(307, 48);
            turbinComTextBox.Size = new Size(118, 274);
            Controls.Add(turbinComTextBox);

            serialPort1.BaudRate = 19200;

            //turbinComs = "";
            //turbinCom = "";
            //comBuf = "";
            MQ = 0;
            packetCounter = 0;

            isComOpen = false;
            isConnected = false;
        }

        ~Form1()
        {
            mySocket.Close();
        }

        //data set delegate
        delegate void TextSet(string text);


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

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
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
            catch (System.InvalidOperationException e1)
            {
                eCode = 1;
            }
            catch (System.ArgumentException e2)
            {
                eCode = 2;
            }
        }

        private void button_comOpen_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = textBox_Com.Text;
            serialPort1.Open();
            isComOpen = true;
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            turbinComs = "";
            turbinComTextBox.Text = "";
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress address = Dns.GetHostAddresses(textBox_IP.Text)[0];
                IPEndPoint serverEP = new IPEndPoint(address, Int32.Parse(textBox_PORT.Text));
                mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mySocket.Connect(serverEP);
                if (mySocket.Connected)
                {
                    connectLamp.BackColor = Color.GreenYellow;
                    isConnected = true;

                    // 受信処理インスタンス生成

                    ReadWorkProc readproc = new ReadWorkProc(Socket, this);

                    // 受信スレッド生成
                    Thread work = new Thread(new ThreadStart(readproc.Run));
                    // 受信はバックグラウンドで処理
                    work.IsBackground = true;

                    work.Start();


                }
            }
            catch(Exception ex)
            {

            }
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

}
