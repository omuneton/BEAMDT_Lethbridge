namespace BEAMDT.Screens
{
    partial class FTP3
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTP3));
            this.lblPregunta = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.txtFTPPassword = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbl6Izq = new System.Windows.Forms.LinkLabel();
            this.pic6Izq = new System.Windows.Forms.PictureBox();
            this.lbl3Der = new System.Windows.Forms.LinkLabel();
            this.pic3Der = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // lblPregunta
            // 
            this.lblPregunta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblPregunta.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold);
            this.lblPregunta.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPregunta.Location = new System.Drawing.Point(0, 0);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(798, 61);
            this.lblPregunta.Text = "FTP Password";
            this.lblPregunta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblResultado
            // 
            this.lblResultado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblResultado.Font = new System.Drawing.Font("Tahoma", 26F, System.Drawing.FontStyle.Regular);
            this.lblResultado.ForeColor = System.Drawing.Color.White;
            this.lblResultado.Location = new System.Drawing.Point(0, 195);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(798, 47);
            this.lblResultado.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtFTPPassword
            // 
            this.txtFTPPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.txtFTPPassword.Font = new System.Drawing.Font("Tahoma", 26F, System.Drawing.FontStyle.Regular);
            this.txtFTPPassword.ForeColor = System.Drawing.Color.White;
            this.txtFTPPassword.Location = new System.Drawing.Point(232, 122);
            this.txtFTPPassword.Name = "txtFTPPassword";
            this.txtFTPPassword.Size = new System.Drawing.Size(331, 48);
            this.txtFTPPassword.TabIndex = 4;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(800, 480);
            // 
            // lbl6Izq
            // 
            this.lbl6Izq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(255)))), ((int)(((byte)(19)))));
            this.lbl6Izq.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lbl6Izq.ForeColor = System.Drawing.Color.Black;
            this.lbl6Izq.Location = new System.Drawing.Point(10, 429);
            this.lbl6Izq.Name = "lbl6Izq";
            this.lbl6Izq.Size = new System.Drawing.Size(98, 40);
            this.lbl6Izq.TabIndex = 44;
            this.lbl6Izq.TabStop = false;
            this.lbl6Izq.Text = "Back";
            this.lbl6Izq.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl6Izq.Click += new System.EventHandler(this.pictureBox2_Click_1);
            // 
            // pic6Izq
            // 
            this.pic6Izq.Image = ((System.Drawing.Image)(resources.GetObject("pic6Izq.Image")));
            this.pic6Izq.Location = new System.Drawing.Point(2, 423);
            this.pic6Izq.Name = "pic6Izq";
            this.pic6Izq.Size = new System.Drawing.Size(108, 56);
            this.pic6Izq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic6Izq.Click += new System.EventHandler(this.pictureBox2_Click_1);
            // 
            // lbl3Der
            // 
            this.lbl3Der.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(255)))), ((int)(((byte)(19)))));
            this.lbl3Der.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lbl3Der.ForeColor = System.Drawing.Color.Black;
            this.lbl3Der.Location = new System.Drawing.Point(693, 429);
            this.lbl3Der.Name = "lbl3Der";
            this.lbl3Der.Size = new System.Drawing.Size(98, 40);
            this.lbl3Der.TabIndex = 47;
            this.lbl3Der.TabStop = false;
            this.lbl3Der.Text = "Ok";
            this.lbl3Der.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl3Der.Click += new System.EventHandler(this.picOk_Click_1);
            // 
            // pic3Der
            // 
            this.pic3Der.Image = ((System.Drawing.Image)(resources.GetObject("pic3Der.Image")));
            this.pic3Der.Location = new System.Drawing.Point(691, 422);
            this.pic3Der.Name = "pic3Der";
            this.pic3Der.Size = new System.Drawing.Size(108, 56);
            this.pic3Der.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic3Der.Click += new System.EventHandler(this.picOk_Click_1);
            // 
            // FTP3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(110)))), ((int)(((byte)(165)))));
            this.Controls.Add(this.lbl3Der);
            this.Controls.Add(this.pic3Der);
            this.Controls.Add(this.lbl6Izq);
            this.Controls.Add(this.pic6Izq);
            this.Controls.Add(this.txtFTPPassword);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.pictureBox3);
            this.Name = "FTP3";
            this.Size = new System.Drawing.Size(800, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPregunta;
        public System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.LinkLabel lbl6Izq;
        public System.Windows.Forms.PictureBox pic6Izq;
        public System.Windows.Forms.LinkLabel lbl3Der;
        public System.Windows.Forms.PictureBox pic3Der;
        public System.Windows.Forms.TextBox txtFTPPassword;
        public Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
    }
}
