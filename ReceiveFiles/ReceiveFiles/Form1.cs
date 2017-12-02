using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Soap;

namespace Server
{

    public partial class Form1 : Form
    {
        private const int BufferSize = 10000;
        public string Status = string.Empty;
        TcpClient client = null;
        public Thread T = null;
        List<SomeData> data=new List<SomeData>();
        List<TcpClient> clients = new List<TcpClient>();
        TcpListener Listener = null;
        String CA = String.Empty;

        public Form1()
        {
            InitializeComponent();
            ShowData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Server is Running...";
            ThreadStart Ts = new ThreadStart(StartReceiving);
            T = new Thread(Ts);
            T.Start();
            data.Clear();   
        }

        //start main thread
        public void StartReceiving()
        {
           
            ReceiveTCP(8888);
        }

        public void ShowData()
        {
            listBox1.DataSource = null;
            listBox1.ValueMember = "Value";
            listBox1.DisplayMember = "Text";
            listBox1.DataSource = data;
        }

        public void ReceiveTCP(int portN)
        {
            
            try
            {
                Listener = Server.start(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            for (; ; )
            {
                Status = String.Empty;

                if (Listener.Pending())
                {
                    client = Listener.AcceptTcpClient();

                    Thread thread = new Thread(process_client);
                    thread.Start();
                }
            }
        }


        

        
        public static byte[] getBytes(String str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static String getString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            T.Abort();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowData();
        }
    



 
       




        void process_client()
        {
            TcpClient mClient = client;
            NetworkStream stream = mClient.GetStream();
            Status = "Connected to a client\n";
            BinaryReader reader = new BinaryReader(stream);
            int option = reader.ReadInt32();

            if (option == 99)
            {
                CA = reader.ReadString();
                return;
            }

            clients.Add(mClient);

           

            SomeData itm = null;
            if (option == 1)
            {
                itm = new SomeData();
                itm.Value = reader.ReadString();
                int len = reader.ReadInt32();
                itm.certificate = reader.ReadBytes(len);
                data.Add(itm);

            }


            ShowData();
            //listen for messages   
            while (mClient.Connected)
            {
                try
                {
                    option = reader.ReadInt32();

                    if (option == 2)
                    {
                        byte[] b = getBytes("lst");
                        mClient.Client.Send(b);
                        Server.SendToClient(mClient, data);
                    }

                    else if (option == 3)
                    {


                        int recever = reader.ReadInt32();

                        int dataLen = reader.ReadInt32();

                        byte[] msg = reader.ReadBytes(dataLen);

                        TcpClient reciever = clients[recever];

                        NetworkStream rec = reciever.GetStream();

                        reciever.Client.Send(getBytes("msg"));


                        BinaryWriter recW = new BinaryWriter(rec);

                        recW.Write(itm.Text);
                        recW.Write(msg.Length);
                        recW.Write(msg);
                        recW.Flush();

                    }
                    else if (option == 4)
                    {
                        int ix = clients.IndexOf(mClient);
                        clients.RemoveAt(ix);
                        data.RemoveAt(ix);
                        mClient.Close();
                    }
                }
                catch
                {
                    continue;
                }
            }





        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}