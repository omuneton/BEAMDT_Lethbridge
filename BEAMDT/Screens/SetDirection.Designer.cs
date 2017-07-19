namespace BEAMDT.Screens
{
    partial class SetDirection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetDirection));
            this.lblPregunta = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbl6Izq = new System.Windows.Forms.LinkLabel();
            this.pic6Izq = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // lblPregunta
            // 
            this.lblPregunta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblPregunta.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold);
            this.lblPregunta.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPregunta.Location = new System.Drawing.Point(0, 0);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(798, 56);
            this.lblPregunta.Text = "Set default direction:";
            this.lblPregunta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(240, 119);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(311, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(240, 263);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(311, 62);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click_1);
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
            this.lbl6Izq.Text = "Back";
            this.lbl6Izq.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl6Izq.Visible = false;
            this.lbl6Izq.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pic6Izq
            // 
            this.pic6Izq.Image = ((System.Drawing.Image)(resources.GetObject("pic6Izq.Image")));
            this.pic6Izq.Location = new System.Drawing.Point(2, 423);
            this.pic6Izq.Name = "pic6Izq";
            this.pic6Izq.Size = new System.Drawing.Size(108, 56);
            this.pic6Izq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic6Izq.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(0, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(800, 480);
            // 
            // SetDirection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(110)))), ((int)(((byte)(165)))));
            this.Controls.Add(this.lbl6Izq);
            this.Controls.Add(this.pic6Izq);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.pictureBox4);
            this.Name = "SetDirection";
            this.Size = new System.Drawing.Size(800, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.LinkLabel lbl6Izq;
        public System.Windows.Forms.PictureBox pic6Izq;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}
