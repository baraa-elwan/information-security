namespace Client
{
    partial class CA_info
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
            this.tBox_SiteName = new System.Windows.Forms.TextBox();
            this.tBox_Country = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tBox_City = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_SendCA = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "site name";
            // 
            // tBox_SiteName
            // 
            this.tBox_SiteName.Location = new System.Drawing.Point(106, 39);
            this.tBox_SiteName.Name = "tBox_SiteName";
            this.tBox_SiteName.Size = new System.Drawing.Size(100, 20);
            this.tBox_SiteName.TabIndex = 1;
            this.tBox_SiteName.Text = "google.com";
            // 
            // tBox_Country
            // 
            this.tBox_Country.Location = new System.Drawing.Point(106, 92);
            this.tBox_Country.Name = "tBox_Country";
            this.tBox_Country.Size = new System.Drawing.Size(100, 20);
            this.tBox_Country.TabIndex = 3;
            this.tBox_Country.Text = "Syria";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "country";
            // 
            // tBox_City
            // 
            this.tBox_City.Location = new System.Drawing.Point(106, 141);
            this.tBox_City.Name = "tBox_City";
            this.tBox_City.Size = new System.Drawing.Size(100, 20);
            this.tBox_City.TabIndex = 5;
            this.tBox_City.Text = "Damascus";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "city";
            // 
            // btn_SendCA
            // 
            this.btn_SendCA.Location = new System.Drawing.Point(76, 213);
            this.btn_SendCA.Name = "btn_SendCA";
            this.btn_SendCA.Size = new System.Drawing.Size(129, 28);
            this.btn_SendCA.TabIndex = 6;
            this.btn_SendCA.Text = "Send";
            this.btn_SendCA.UseVisualStyleBackColor = true;
            this.btn_SendCA.Click += new System.EventHandler(this.btn_SendCA_Click);
            // 
            // CA_info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btn_SendCA);
            this.Controls.Add(this.tBox_City);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tBox_Country);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tBox_SiteName);
            this.Controls.Add(this.label1);
            this.Name = "CA_info";
            this.Text = "CA_info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBox_SiteName;
        private System.Windows.Forms.TextBox tBox_Country;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBox_City;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_SendCA;
    }
}