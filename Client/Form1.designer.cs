namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btn_generatePKey = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.connection_stus = new System.Windows.Forms.Label();
            this.tBox_Username = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_connect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.verify = new System.Windows.Forms.Button();
            this.btn_refreash = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.list_Clients = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.company_text = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 290);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the File";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(165, 284);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 30);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP Address";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(92, 33);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(173, 22);
            this.txtIP.TabIndex = 3;
            this.txtIP.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port No.";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(92, 85);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(173, 22);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "8888";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(18, 342);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 25);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(353, 36);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            this.lblStatus.TabIndex = 7;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(143, 342);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(401, 25);
            this.progressBar1.TabIndex = 8;
            // 
            // btn_generatePKey
            // 
            this.btn_generatePKey.Location = new System.Drawing.Point(360, 15);
            this.btn_generatePKey.Margin = new System.Windows.Forms.Padding(4);
            this.btn_generatePKey.Name = "btn_generatePKey";
            this.btn_generatePKey.Size = new System.Drawing.Size(200, 28);
            this.btn_generatePKey.TabIndex = 9;
            this.btn_generatePKey.Text = "Generate Public key";
            this.btn_generatePKey.UseVisualStyleBackColor = true;
            this.btn_generatePKey.Click += new System.EventHandler(this.btn_generatePKey_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "key Size ";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(123, 18);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(160, 22);
            this.numericUpDown1.TabIndex = 12;
            this.numericUpDown1.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.company_text);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.connection_stus);
            this.groupBox1.Controls.Add(this.tBox_Username);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btn_connect);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(16, 69);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(579, 155);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "connect to server";
            // 
            // connection_stus
            // 
            this.connection_stus.AutoSize = true;
            this.connection_stus.Location = new System.Drawing.Point(243, 124);
            this.connection_stus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.connection_stus.Name = "connection_stus";
            this.connection_stus.Size = new System.Drawing.Size(0, 17);
            this.connection_stus.TabIndex = 9;
            // 
            // tBox_Username
            // 
            this.tBox_Username.Location = new System.Drawing.Point(357, 36);
            this.tBox_Username.Margin = new System.Windows.Forms.Padding(4);
            this.tBox_Username.Name = "tBox_Username";
            this.tBox_Username.Size = new System.Drawing.Size(173, 22);
            this.tBox_Username.TabIndex = 8;
            this.tBox_Username.Text = "hamida";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(280, 39);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Username";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(471, 119);
            this.btn_connect.Margin = new System.Windows.Forms.Padding(4);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(100, 28);
            this.btn_connect.TabIndex = 6;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.verify);
            this.groupBox2.Controls.Add(this.btn_refreash);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.list_Clients);
            this.groupBox2.Controls.Add(this.btnBrowse);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(16, 231);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(579, 375);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send files";
            // 
            // verify
            // 
            this.verify.Location = new System.Drawing.Point(332, 126);
            this.verify.Name = "verify";
            this.verify.Size = new System.Drawing.Size(149, 28);
            this.verify.TabIndex = 10;
            this.verify.Text = "verify messages";
            this.verify.UseVisualStyleBackColor = true;
            this.verify.Click += new System.EventHandler(this.verify_Click);
            // 
            // btn_refreash
            // 
            this.btn_refreash.Location = new System.Drawing.Point(28, 216);
            this.btn_refreash.Margin = new System.Windows.Forms.Padding(4);
            this.btn_refreash.Name = "btn_refreash";
            this.btn_refreash.Size = new System.Drawing.Size(100, 28);
            this.btn_refreash.TabIndex = 9;
            this.btn_refreash.Text = "Refreash";
            this.btn_refreash.UseVisualStyleBackColor = true;
            this.btn_refreash.Click += new System.EventHandler(this.btn_refreash_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(211, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "select client who resive your file ";
            // 
            // list_Clients
            // 
            this.list_Clients.FormattingEnabled = true;
            this.list_Clients.ItemHeight = 16;
            this.list_Clients.Location = new System.Drawing.Point(19, 50);
            this.list_Clients.Margin = new System.Windows.Forms.Padding(4);
            this.list_Clients.Name = "list_Clients";
            this.list_Clients.Size = new System.Drawing.Size(247, 148);
            this.list_Clients.TabIndex = 0;
            this.list_Clients.SelectedIndexChanged += new System.EventHandler(this.list_Clients_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(280, 88);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "comany";
            // 
            // company_text
            // 
            this.company_text.Location = new System.Drawing.Point(356, 85);
            this.company_text.Margin = new System.Windows.Forms.Padding(4);
            this.company_text.Name = "company_text";
            this.company_text.Size = new System.Drawing.Size(173, 22);
            this.company_text.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 619);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_generatePKey);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Send Files";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btn_generatePKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.TextBox tBox_Username;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label connection_stus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_refreash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox list_Clients;
        private System.Windows.Forms.Button verify;
        private System.Windows.Forms.TextBox company_text;
        private System.Windows.Forms.Label label7;

    }
}

