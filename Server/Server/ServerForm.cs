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
using Client;
namespace Server
{

    public partial class ServerForm : Form
    {

        public string Status = string.Empty; //status of connection
        
        TcpClient client = null;// object for recieving sockets
        
        public Thread T = null; // listening thread
       
        List<SomeData> data=new List<SomeData>(); //list of informations of connected clients 

        List<TcpClient> clients = new List<TcpClient>();//list of connected clients 


        TcpListener Listener = null;
        
        String CA = String.Empty;

        public ServerForm()
        {
            InitializeComponent();
            ShowData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Server is Running...";

            // start listening thread
            ThreadStart Ts = new ThreadStart(StartReceiving);
            T = new Thread(Ts);
            T.Start();
            data.Clear();   
        }

       //show client list on form
        public void ShowData()
        {
            listBox1.DataSource = null;
            listBox1.ValueMember = "certificate";
            listBox1.DisplayMember = "username";
            listBox1.DataSource = data;
        }

        //listening action
        public void StartReceiving()
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
                    //Accept new socket and start thread for connection
                    client = Listener.AcceptTcpClient();

                    Thread thread = new Thread(process_client);
                    thread.Start();
                }
            }
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

            if (option == 99){
                CA = reader.ReadString();
                return;
            }

            clients.Add(mClient);

            SomeData itm = null;
            if (option == 1){
                itm = new SomeData();
                int len = reader.ReadInt32();
                itm =(SomeData) Helper.deSerilize(reader.ReadBytes(len));
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
                        byte[] b = Helper.getBytes("lst");
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

                        reciever.Client.Send(Helper.getBytes("msg"));


                        BinaryWriter recW = new BinaryWriter(rec);

                         recW.Write(itm.username);

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