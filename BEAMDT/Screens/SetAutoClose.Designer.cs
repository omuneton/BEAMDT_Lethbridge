namespace BEAMDT.Screens
{
    partial class SetAutoClose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetAutoClose));
            this.picdown = new System.Windows.Forms.PictureBox();
            this.picUp = new System.Windows.Forms.PictureBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.lblPregunta = new System.Windows.Forms.Label();
            this.lbl6Izq = new System.Windows.Forms.LinkLabel();
            this.pic6Izq = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbl3Der = new System.Windows.Forms.LinkLabel();
            this.pic3Der = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // picdown
            // 
            this.picdown.Image = ((System.Drawing.Image)(resources.GetObject("picdown.Image")));
            this.picdown.Location = new System.Drawing.Point(343, 217);
            this.picdown.Name = "picdown";
            this.picdown.Size = new System.Drawing.Size(111, 63);
            this.picdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picdown.Click += new System.EventHandler(this.picdown_Click);
            // 
            // picUp
            // 
            this.picUp.Image = ((System.Drawing.Image)(resources.GetObject("picUp.Image")));
            this.picUp.Location = new System.Drawing.Point(343, 148);
            this.picUp.Name = "picUp";
            this.picUp.Size = new System.Drawing.Size(111, 63);
            this.picUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUp.Click += new System.EventHandler(this.picUp_Click);
            // 
            // lblValor
            // 
            this.lblValor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblValor.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold);
            this.lblValor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblValor.Location = new System.Drawing.Point(0, 51);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(798, 63);
            this.lblValor.Text = "0";
            this.lblValor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPregunta
            // 
            this.lblPregunta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblPregunta.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold);
            this.lblPregunta.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPregunta.Location = new System.Drawing.Point(0, 0);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(798, 56);
            this.lblPregunta.Text = "Auto Close Time:";
            this.lblPregunta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl6Izq
            // 
            this.lbl6Izq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(255)))), ((int)(((byte)(19)))));
            this.lbl6Izq.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lbl6Izq.ForeColor = System.Drawing.Color.Black;
            this.lbl6Izq.Location = new System.Drawing.Point(10, 429);
            this.lbl6Izq.Name = "lbl6Izq";
            this.lbl6Izq.Size = new System.Drawing.Size(98, 40);
            this.lbl6Izq.TabIndex = 45;
            this.lbl6Izq.TabStop = false;
            this.lbl6Izq.Text = "Back";
            this.lbl6Izq.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl6Izq.Click += new System.EventHandler(this.picback_Click_1);
            // 
            // pic6Izq
            // 
            this.pic6Izq.Image = ((System.Drawing.Image)(resources.GetObject("pic6Izq.Image")));
            this.pic6Izq.Location = new System.Drawing.Point(2, 423);
            this.pic6Izq.Name = "pic6Izq";
            this.pic6Izq.Size = new System.Drawing.Size(108, 56);
            this.pic6Izq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic6Izq.Click += new System.EventHandler(this.picback_Click_1);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(800, 480);
            // 
            // lbl3Der
            // 
            this.lbl3Der.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(255)))), ((int)(((byte)(19)))));
            this.lbl3Der.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lbl3Der.ForeColor = System.Drawing.Color.Black;
            this.lbl3Der.Location = new System.Drawing.Point(693, 429);
            this.lbl3Der.Name = "lbl3Der";
            this.lbl3Der.Size = new System.Drawing.Size(98, 40);
            this.lbl3Der.TabIndex = 50;
            this.lbl3Der.TabStop = false;
            this.lbl3Der.Text = "Ok";
            this.lbl3Der.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl3Der.Click += new System.EventHandler(this.picOk_Click);
            // 
            // pic3Der
            // 
            this.pic3Der.Image = ((System.Drawing.Image)(resources.GetObject("pic3Der.Image")));
            this.pic3Der.Location = new System.Drawing.Point(691, 422);
            this.pic3Der.Name = "pic3Der";
            this.pic3Der.Size = new System.Drawing.Size(108, 56);
            this.pic3Der.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic3Der.Click += new System.EventHandler(this.picOk_Click);
            // 
            // SetAutoClose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(110)))), ((int)(((byte)(165)))));
            this.Controls.Add(this.lbl3Der);
            this.Controls.Add(this.pic3Der);
            this.Controls.Add(this.lbl6Izq);
            this.Controls.Add(this.pic6Izq);
            this.Controls.Add(this.picdown);
            this.Controls.Add(this.picUp);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.pictureBox3);
            this.Name = "SetAutoClose";
            this.Size = new System.Drawing.Size(800, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picdown;
        private System.Windows.Forms.PictureBox picUp;
        private System.Windows.Forms.Label lblPregunta;
        public System.Windows.Forms.Label lblValor;
        public System.Windows.Forms.LinkLabel lbl6Izq;
        public System.Windows.Forms.PictureBox pic6Izq;
        private System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.LinkLabel lbl3Der;
        public System.Windows.Forms.PictureBox pic3Der;
    }
}
