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
    public partial class ClientForm : Form
    {
        #region data members

        public string SendingFilePath = string.Empty;
        public string VerfyFilePath = string.Empty;
        public string CertificateFilePath = string.Empty;

        int recieverId = -1;

        RSACryptoServiceProvider rsaProvider = null;

        CA_info ca;

        Client client;

        List<SomeData> clientList = new List<SomeData>();

        public SomeData info;

        Thread recieveThread = null;

        public string CA;
        #endregion

        #region construcror

        public ClientForm()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            client = new Client();

            list_Clients.ValueMember = "certificate";
            list_Clients.DisplayMember = "username";
            list_Clients.DataSource = clientList;
        }
        #endregion

        #region bottons actions

        private void btn_generatePKey_Click(object sender, EventArgs e)
        {
            // generate public and private keys
            int keySize = Convert.ToInt32(numericUpDown1.Value);
            rsaProvider = AsymmetricEncryption.GenerateKeys(keySize);
            btn_generatePKey.Enabled = false;
            groupBox1.Enabled = true;

        }

        private void btn_CreateCertificate_Click(object sender, EventArgs e)
        {
            // action => start CAInfo Form for send cerificate request
            ca = new CA_info(rsaProvider.ToXmlString(false), this);
            ca.Show();
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
                    info.username = tBox_Username.Text;

                    recieveThread = new Thread(clientReceiver);
                    recieveThread.Start();
                    //start connection with button                   
                    Directory.CreateDirectory("D://" + info.username);
                    client.connect(txtIP.Text, Convert.ToInt32(txtPort.Text));
                    client.sendUserData(info);// to server
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
            client.request_client_list();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SendingFilePath = Helper.ShowDialogue("Desktop");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (SendingFilePath != string.Empty)
            {
                if (recieverId == -1)
                {
                    MessageBox.Show("select reciever");
                    return;
                }

                SendFile(SendingFilePath, txtIP.Text, Int32.Parse(txtPort.Text));
            }
            else
                MessageBox.Show("Select a file", "Warning");
        }

        private void verify_Click(object sender, EventArgs e)
        {
            string path = "D://" + info.username;
            VerfyFilePath = Helper.ShowDialogue(path);
        }
        #endregion

        #region recieving thread process
        void clientReceiver()
        {
            TcpClient socket = this.client.socket;

            int reqLen = Helper.getBytes("msg").Length;
            
            byte[] req = new byte[reqLen];

            NetworkStream netStream = socket.GetStream();
            
            while (socket.Connected)
            {
                socket.Client.Receive(req);

                BinaryReader streamR = new BinaryReader(netStream);
               
                if (Helper.getString(req).Equals("msg"))
                {
                    string sender = streamR.ReadString();
                    DialogResult result = 
                        MessageBox.Show(
                        "Accept the Incoming File from " + sender,
                        "Incoming Connection", 
                        MessageBoxButtons.YesNo
                     );

                    
                    if (result == DialogResult.Yes)
                    {
                        int dataLen = streamR.ReadInt32();

                        byte[] RecievedData = streamR.ReadBytes(dataLen);

                        //SaveFileName = streamR.ReadString();
                        Message m = (Message)Helper.deSerilize(RecievedData);                      

                        string sender_public_key = null;
                        foreach (SomeData item in clientList)
                        {
                            if (item.username.Equals(sender))
                            {
                                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                                rsa.FromXmlString(CA);
                                bool autinticated = rsa.VerifyHash(
                                    new SHA1Managed().ComputeHash(Helper.Serilize(item.info)),
                                    CryptoConfig.MapNameToOID("SHA1"),
                                    item.certificate
                                );
                                if(autinticated)   
                                   sender_public_key = item.info.publicKey;
                                else
                                {
                                    string msg =  "not secure connection with "+sender+"\n do you want to complete process?";
                                    if (MessageBox.Show(msg, "warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        sender_public_key = item.info.publicKey;
                                    }
                                    else
                                        return;
                                }
                                break;
                            }
                        }



                        if (AsymmetricEncryption.verifyMsg(sender_public_key, m))
                        {
                            String res = AsymmetricEncryption.PGPDecrypt(m.msg, rsaProvider.ToXmlString(true));

                            client.saveFile(res, "D://" + info.username + "/" + m.file_name);

                            FileStream stream = new FileStream("D://" + info.username + "/" +
                                Path.GetFileNameWithoutExtension(m.file_name) + ".sign", FileMode.OpenOrCreate);

                            stream.WriteByte(0);
                            stream.Write(Helper.getBytes(sender_public_key), 0, sender_public_key.Length);
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
                else if (Helper.getString(req).Equals("lst"))
                {
                    clientList.Clear();
                    int len = streamR.ReadInt32();
                    for (int i = 0; i < len; i++)
                    {
                        SomeData itm = new SomeData();

                        int l = streamR.ReadInt32();
                        itm = (SomeData)Helper.deSerilize(streamR.ReadBytes(l));

                        clientList.Add(itm);
                    }

                    this.Invoke((MethodInvoker)(() => ShowData()));
                }
            }


        }
        #endregion

        #region sending files process
        public void SendFile(string M, string IPA, Int32 PortN)
        {
            TcpClient mclient = this.client.socket;
           
            lblStatus.Text = "";
            
            NetworkStream netstream = mclient.GetStream();
            
            try
            {
                String key = clientList[recieverId].info.publicKey;
                
                lblStatus.Text = "Connected to the Server...\n";

                String data = File.ReadAllText(SendingFilePath, Encoding.GetEncoding(20127));

                int keySize = Int32.Parse(numericUpDown1.Value.ToString());

                byte[] encrypted;
                byte[] signedMsg = AsymmetricEncryption.signData(rsaProvider, out encrypted ,data, keySize, key);

                Message myMsg = new Message(encrypted, signedMsg, Path.GetFileName(SendingFilePath));

                BinaryWriter writer = new BinaryWriter(netstream);
                byte[] msg = Helper.Serilize(myMsg);

                writer.Write(3);

                writer.Write(recieverId);

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

        public void ShowData()
        {
            list_Clients.DataSource = null;
            list_Clients.ValueMember = "certificate";
            list_Clients.DisplayMember = "username";
            list_Clients.DataSource = clientList;

        }
        private void list_Clients_SelectedIndexChanged(object sender, EventArgs e)
        {
            recieverId = list_Clients.SelectedIndex;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            new BinaryWriter(client.socket.GetStream()).Write(4);
            recieveThread.Abort();
            client.disconnect();
        }
    }
}