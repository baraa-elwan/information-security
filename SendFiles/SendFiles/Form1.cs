using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using Server;
using System.Threading;

namespace Client
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

        public void SendFile(string M, string IPA, Int32 PortN)
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
                byte[] encrypted = AsymmetricEncryption.PGPEncrypt(data, 1024, key);

                MessageBox.Show(AsymmetricEncryption.PGPDecrypt(encrypted, publicAndPrivateKey));
               
                BinaryWriter writer = new BinaryWriter(netstream);
                writer.Write(3);
                writer.Write(reciever);
                writer.Write(encrypted.Length);
                writer.Write(encrypted);
                writer.Flush();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

        private void btn_refreash_Click(object sender, EventArgs e)
        {
            client.sendReq();

        }

        private void btn_generatePKey_Click(object sender, EventArgs e)
        {
            int keySize = Convert.ToInt32(numericUpDown1.Value);
            AsymmetricEncryption.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);
            btn_generatePKey.Enabled = false;
            groupBox1.Enabled = true;

        }

        private void list_Clients_SelectedIndexChanged(object sender, EventArgs e)
        {
            reciever = list_Clients.SelectedIndex;

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

                SendFile(SendingFilePath, txtIP.Text, Int32.Parse(txtPort.Text));
            }
            else
                MessageBox.Show("Select a file", "Warning");
        }

       
        #region recievin thread process
        void clientReceiver()
        {
            TcpClient socket = this.client.socket;

            int reqLen = getBytes("msg").Length;
            byte[] req = new byte[reqLen];
            while (socket.Connected)
            {

                NetworkStream netStream = socket.GetStream();
                socket.Client.Receive(req);
                
                BinaryReader streamR = new BinaryReader(netStream);
                if (getString(req).Equals("msg"))
                {
                    string message = "Accept the Incoming File ";

                    string caption = "Incoming Connection";

                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons);

                    if (result == DialogResult.Yes)
                    {
                        string SaveFileName = string.Empty;

                        byte[] RecData;

                        int dataLen = streamR.ReadInt32();

                        RecData = streamR.ReadBytes(dataLen);

                        SaveFileName = "D://test.txt";

                        String res = AsymmetricEncryption.PGPDecrypt(RecData, publicAndPrivateKey);

                        client.saveFile(res, SaveFileName);

                    }

                }
                else
                {
                    clientList.Clear();
                    int len = streamR.ReadInt32();
                    for (int i = 0; i < len; i++)
                    {
                        SomeData itm = new SomeData();
                        itm.Text = streamR.ReadString();
                        itm.Value = streamR.ReadString();
                        clientList.Add(itm);
                    }
                    
                    this.Invoke((MethodInvoker)(() => ShowData()));
                }
            }

        }
        #endregion

        #region helpers

        public void ShowData()
        {
            list_Clients.DataSource = null;
            list_Clients.ValueMember = "Value";
            list_Clients.DisplayMember = "Text";
            list_Clients.DataSource = clientList;

        }

        public static byte[] getBytes(String str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static String getString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
        #endregion

    }
}