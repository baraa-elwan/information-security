using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    public partial class Form1 : Form
    {
        #region members
        public string SendingFilePath = string.Empty;

        public string VerfyFilePath = string.Empty;

        private const int BufferSize = 1024;

        Client client;

        int reciever = -1;

        string username = String.Empty;
        string company = String.Empty;

        RSACryptoServiceProvider rsaProvider = null;

        List<SomeData> clientList = new List<SomeData>();

        int selected_file = -1;

        Thread recieveThread = null;

        #endregion

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


        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (tBox_Username.Text == String.Empty)
            {
                MessageBox.Show("enter Username please !");
            }
            else if (txtIP.Text == String.Empty || txtPort.Text == String.Empty)
            {
                MessageBox.Show("enter Ip or port please !");
            }
            else
            {
                try
                {
                    username = tBox_Username.Text;
                    //start connection with button
                    Directory.CreateDirectory("D://" + username);
                    client.connect(txtIP.Text, Convert.ToInt32(txtPort.Text));
                    //TODO 
                    client.sendUserData(username, rsaProvider.ToXmlString(false));
                    recieveThread = new Thread(clientReceiver);
                    recieveThread.Start();
                    connection_stus.Text = "Connected";
                    btn_connect.Enabled = false;
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
            rsaProvider = AsymmetricEncryption.GenerateKeys(keySize);
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
            Dlg.InitialDirectory = @"Desktop";
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
                    string message = "Accept the Incoming File from ";

                    string caption = "Incoming Connection";

                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                    DialogResult result;
                    string sender = streamR.ReadString();
                    result = MessageBox.Show(message + sender, caption, buttons);

                    if (result == DialogResult.Yes)
                    {


                        byte[] RecData;

                        int dataLen = streamR.ReadInt32();

                        RecData = streamR.ReadBytes(dataLen);

                        //SaveFileName = streamR.ReadString();
                        Message m = Message.deSerilizeMessage(RecData);

                        byte[] hash_data = new SHA1Managed().ComputeHash(m.msg);

                        RSACryptoServiceProvider sender_rsa = new RSACryptoServiceProvider();

                        byte[] sender_public_key = null;
                        foreach (SomeData item in clientList)
                        {
                            if (item.Text.Equals(sender))
                            {
                                sender_public_key = getBytes(item.Value);
                                sender_rsa.FromXmlString(item.Value);
                                break;
                            }
                        }



                        if (sender_rsa.VerifyHash(hash_data, CryptoConfig.MapNameToOID("SHA1"), m.signature))
                        {

                            String res = AsymmetricEncryption.PGPDecrypt(m.msg, rsaProvider.ToXmlString(true));

                            client.saveFile(res, "D://" + username + "/" + m.file_name);

                            FileStream stream = new FileStream("D://" + username + "/" +
                                Path.GetFileNameWithoutExtension(m.file_name) + ".sign", FileMode.OpenOrCreate);

                            stream.WriteByte(0);
                            stream.Write(sender_public_key, 0, sender_public_key.Length);
                            stream.WriteByte(1);
                            stream.Write(m.signature, 0, m.signature.Length);
                            stream.Close();




                        }
                        else
                        {
                            MessageBox.Show("not reliable message");
                        }

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

        public void SendFile(string M, string IPA, Int32 PortN)
        {



            TcpClient mclient = this.client.socket;
            lblStatus.Text = "";
            NetworkStream netstream = mclient.GetStream();
            try
            {
                String key = clientList[reciever].Value;
                lblStatus.Text = "Connected to the Server...\n";

                String data = File.ReadAllText(SendingFilePath, Encoding.GetEncoding(20127));

                int keySize = Int32.Parse(numericUpDown1.Value.ToString());

                SHA1Managed shaHashing = new SHA1Managed();

                byte[] encrypted = AsymmetricEncryption.PGPEncrypt(data, keySize, key);

                byte[] hashed_msg = shaHashing.ComputeHash(encrypted);

                byte[] signedMsg = rsaProvider.SignHash(hashed_msg, CryptoConfig.MapNameToOID("SHA1"));


                Message myMsg = new Message(encrypted, signedMsg, Path.GetFileName(SendingFilePath));

                BinaryWriter writer = new BinaryWriter(netstream);


                byte[] msg = Message.serilizeMessage(myMsg);

                writer.Write(3);

                writer.Write(reciever);

                writer.Write(msg.Length);

                writer.Write(msg);

                writer.Flush();




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            new BinaryWriter(client.socket.GetStream()).Write(4);
            recieveThread.Abort();
            client.disconnect();

        }

        private void verify_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "All Files (*.txt)|*.*";
            Dlg.CheckFileExists = true;
            Dlg.Title = "Choose a File";
            string path = "D://" + username + "/";
            Dlg.InitialDirectory = @path;
            if (Dlg.ShowDialog() == DialogResult.OK)
            {
                VerfyFilePath = Dlg.FileName;

            }



        }




    }
}