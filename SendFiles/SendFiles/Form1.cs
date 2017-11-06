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

        string publicKey=String.Empty;

        string publicAndPrivateKey = String.Empty;

        List<SomeData> clientList = new List<SomeData>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new Client();
            progressBar1.Visible=true;
            progressBar1.Minimum=1;
            progressBar1.Value=1;
            progressBar1.Step=1;
            
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
                MessageBox.Show("Select a file","Warning");
        }

        public void SendTCP(string M, string IPA, Int32 PortN)
        {
            byte[] SendingBuffer = null;
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
                String encrypted =  AsymmetricEncryption.EncryptText(data, keySize, key);

               // MessageBox.Show(encrypted);

                BinaryWriter writer = new BinaryWriter(netstream);
                writer.Write(3);
                writer.Write(reciever);
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

                         new Thread(receiveListening).Start();
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

        void receiveListening()
        {
            byte[] buffer = new byte[BufferSize] ;

            while (true)
            {
                //TODO recieve reques 

                // TODO show dialog

                // recieve decrypt and save file
                byte[] req =new byte[4];
                client.socket.Client.Receive(req);

                if (Client.getString(req).Equals("send"))
                {
                    string message = "Accept the Incoming File ";
                    string caption = "Incoming Connection";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string SaveFileName = string.Empty;
                        StreamWriter stream = new StreamWriter(client.socket.GetStream());
                        stream.Write(1);

                        byte[] msg;
                        //client.socket.Client.Receive(msg);
                        //SaveFileDialog DialogSave = new SaveFileDialog();
                        //DialogSave.Filter = "All files (*.*)|*.*";
                        //DialogSave.RestoreDirectory = true;
                        //DialogSave.Title = "Where do you want to save the file?";
                        //DialogSave.InitialDirectory = @"C:/";
                        //var test = DialogSave.ShowDialog();
                        //if (test == DialogResult.OK)
                        //    SaveFileName = DialogSave.FileName;
                        int RecBytes = 0;
                        byte[] RecData;//= new byte[BufferSize];
                        NetworkStream netstream = client.socket.GetStream();
                        BinaryReader streamR = new BinaryReader(netstream);
                        int len = streamR.ReadInt32();
                        RecData =  streamR.ReadBytes(len);

                        SaveFileName = "C:/test.txt";


                        byte[] key = new byte[8];
                        byte[] ms = new byte[RecData.Length - 8];
                        Array.Copy( RecData, key, 8);

                       byte[] dec = AsymmetricEncryption.Decrypt(key, 1024, publicAndPrivateKey);

                       Array.Copy( RecData, key, 8);

                         Array.Copy(RecData, 8, ms, 0, ms.Length);

                        SymmetricEncryption symmetric = new SymmetricEncryption();
                        symmetric.key = key;

                        String res = symmetric.decrypt_data(ms);

                        FileStream Fs = new FileStream(SaveFileName, FileMode.OpenOrCreate, FileAccess.Write);

                        Fs.Write(Client.getBytes(res), 0, Client.getBytes(res).Length);
                        Fs.Close();
                       // netstream.Close();
                       // client.Close();

                    }
                }
            
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
            clientList= client.ReceiveClientList();
            ShowData();
        }

        private void list_Clients_SelectedIndexChanged(object sender, EventArgs e)
        {
              reciever =  list_Clients.SelectedIndex;

        }
    }
}