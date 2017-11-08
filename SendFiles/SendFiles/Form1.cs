using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using ReceiveFiles;
using System.Threading;

namespace SendFiles
{
    public partial class Form1 : Form
    {
        public string SendingFilePath = string.Empty;

        private const int BufferSize = 1024;

        Client client;

        int reciever = -1;

        bool connected = false;

        string username = String.Empty;

        string publicKey = String.Empty;

        string publicAndPrivateKey = String.Empty;

        List<SomeData> clientList = new List<SomeData>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new Client();
            progressBar1.Visible = true;
            progressBar1.Minimum = 1;
            progressBar1.Value = 1;
            progressBar1.Step = 1;
            list_Clients.ValueMember = "Value";
            list_Clients.DisplayMember = "Text";
            list_Clients.DataSource = clientList;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "All Files (*.*)|*.*";
            Dlg.CheckFileExists = true;
            Dlg.Title = "Choose a File";
            Dlg.InitialDirectory = @"C:\";
            if (Dlg.ShowDialog() == DialogResult.OK)
            {
                SendingFilePath = Dlg.FileName;

            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (SendingFilePath != string.Empty)
            {
                if (reciever == -1)
                {
                    MessageBox.Show("select reciever");
                    return;
                }

                SendTCP(SendingFilePath, txtIP.Text, Int32.Parse(txtPort.Text));
            }
            else
                MessageBox.Show("Select a file", "Warning");
        }

        public void SendTCP(string M, string IPA, Int32 PortN)
        {

            TcpClient mclient = this.client.socket;
            lblStatus.Text = "";
            NetworkStream netstream = mclient.GetStream();
            try
            {
                String key = clientList[reciever].Value;
                lblStatus.Text = "Connected to the Server...\n";
                netstream = mclient.GetStream();

                String data = File.ReadAllText(SendingFilePath, Encoding.GetEncoding(20127));

                int keySize = Int32.Parse(numericUpDown1.Value.ToString());
                byte[] encrypted = AsymmetricEncryption.PGPEncrypt(data, keySize, key);

                // MessageBox.Show(encrypted);

                BinaryWriter writer = new BinaryWriter(netstream);
                writer.Write(3);
                writer.Write(reciever);
                writer.Write(encrypted.Length);
                writer.Write(encrypted);
                writer.Flush();

                // TODO commented for test 
                //FileStream Fs = new FileStream(M, FileMode.Open, FileAccess.Read);
                //int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Fs.Length) / Convert.ToDouble(BufferSize)));
                //progressBar1.Maximum = NoOfPackets;
                //int TotalLength = (int)Fs.Length, CurrentPacketLength, counter = 0;
                //for (int i = 0; i < NoOfPackets; i++)
                //{
                //    if (TotalLength > BufferSize)
                //    {
                //        CurrentPacketLength = BufferSize;
                //        TotalLength = TotalLength - CurrentPacketLength;
                //    }
                //    else
                //        CurrentPacketLength = TotalLength;
                //    SendingBuffer = new byte[CurrentPacketLength];
                //    Fs.Read(SendingBuffer, 0, CurrentPacketLength);
                //    netstream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                //    if (progressBar1.Value >= progressBar1.Maximum)
                //        progressBar1.Value = progressBar1.Minimum;
                //    progressBar1.PerformStep();
                //}

                //lblStatus.Text = lblStatus.Text + "Sent " + Fs.Length.ToString() + " bytes to the server";
                //Fs.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btn_generatePKey_Click(object sender, EventArgs e)
        {
            int keySize = Convert.ToInt32(numericUpDown1.Value);
            AsymmetricEncryption.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);
            btn_generatePKey.Enabled = false;
            groupBox1.Enabled = true;

        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (tBox_Username.Text == String.Empty)
            {
                MessageBox.Show("enter Username please !");
            }
            else
                if (txtIP.Text == String.Empty || txtPort.Text == String.Empty)
            {
                MessageBox.Show("enter Ip or port please !");
            }
            else
            {
                try
                {
                    username = tBox_Username.Text;
                    //start connection with button

                    client.connect(txtIP.Text, Convert.ToInt32(txtPort.Text));
                    client.sendUserData(username, publicKey);
                    new Thread(clientReceiver).Start();
                    connection_stus.Text = "Connected";
                    btn_connect.Enabled = false;
                    connected = true;
                    groupBox2.Enabled = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("error " + ex);
                }
            }
        }

        void clientReceiver()
        {

            String regStr;
            TcpClient socket = this.client.socket;


            //  
            int le = getBytes("msg").Length;
            byte[] req = new byte[le];
            while (true)
            {

                NetworkStream netStream = socket.GetStream();
                //while (netStream.Read(req, 0, req.Length) > 0)
                //{
                socket.Client.Receive(req);
                regStr = getString(req);
                BinaryReader streamR = new BinaryReader(netStream);
                if (regStr.Equals("msg"))
                {
                    string message = "Accept the Incoming File ";
                    string caption = "Incoming Connection";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string SaveFileName = string.Empty;

                        byte[] RecData;
                        int dataLen = streamR.ReadInt32();


                        
                        RecData = streamR.ReadBytes(dataLen);

                        SaveFileName = "D://test.txt";

                        String res = AsymmetricEncryption.PGPDecrypt(RecData, publicAndPrivateKey);

                        FileStream Fs = new FileStream(SaveFileName, FileMode.OpenOrCreate, FileAccess.Write);

                        byte[] resBytes = getBytes(res);
                        Fs.Write(resBytes, 0, resBytes.Length);
                        Fs.Close();

                    }

                }
                else
                {

                    //netstream = socket.GetStream();

                    clientList.Clear();
                    int len = streamR.ReadInt32();
                    for (int i = 0; i < len; i++)
                    {
                        SomeData itm = new SomeData();
                        itm.Text = streamR.ReadString();
                        itm.Value = streamR.ReadString();
                        clientList.Add(itm);
                    }
                    //streamR.Close();
                    //ShowData();
                    this.Invoke((MethodInvoker)(() => ShowData()));
                }

                // }


            }

        }

        public void ShowData()
        {
            list_Clients.DataSource = null;
            list_Clients.ValueMember = "Value";
            list_Clients.DisplayMember = "Text";
            list_Clients.DataSource = clientList;

        }
        private void btn_refreash_Click(object sender, EventArgs e)
        {
            client.sendReq();

        }

        private void list_Clients_SelectedIndexChanged(object sender, EventArgs e)
        {
            reciever = list_Clients.SelectedIndex;

        }

        public static byte[] getBytes(String str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static String getString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        
    }
}