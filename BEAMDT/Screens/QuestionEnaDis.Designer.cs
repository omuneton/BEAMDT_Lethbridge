namespace BEAMDT.Screens
{
    partial class QuestionEnaDis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestionEnaDis));
            this.picEna = new System.Windows.Forms.PictureBox();
            this.picDis = new System.Windows.Forms.PictureBox();
            this.lblPregunta = new System.Windows.Forms.Label();
            this.lbl6Izq = new System.Windows.Forms.LinkLabel();
            this.pic6Izq = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // picEna
            // 
            this.picEna.Image = ((System.Drawing.Image)(resources.GetObject("picEna.Image")));
            this.picEna.Location = new System.Drawing.Point(233, 125);
            this.picEna.Name = "picEna";
            this.picEna.Size = new System.Drawing.Size(336, 72);
            this.picEna.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEna.Click += new System.EventHandler(this.picEna_Click);
            // 
            // picDis
            // 
            this.picDis.Image = ((System.Drawing.Image)(resources.GetObject("picDis.Image")));
            this.picDis.Location = new System.Drawing.Point(233, 266);
            this.picDis.Name = "picDis";
            this.picDis.Size = new System.Drawing.Size(336, 72);
            this.picDis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDis.Click += new System.EventHandler(this.picDis_Click);
            // 
            // lblPregunta
            // 
            this.lblPregunta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblPregunta.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold);
            this.lblPregunta.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPregunta.Location = new System.Drawing.Point(0, 0);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(798, 63);
            this.lblPregunta.Text = "Default Route:";
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
            this.lbl6Izq.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pic6Izq
            // 
            this.pic6Izq.Image = ((System.Drawing.Image)(resources.GetObject("pic6Izq.Image")));
            this.pic6Izq.Location = new System.Drawing.Point(2, 423);
            this.pic6Izq.Name = "pic6Izq";
            this.pic6Izq.Size = new System.Drawing.Size(108, 56);
            this.pic6Izq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic6Izq.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(800, 480);
            // 
            // QuestionEnaDis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(110)))), ((int)(((byte)(165)))));
            this.Controls.Add(this.lbl6Izq);
            this.Controls.Add(this.pic6Izq);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.picDis);
            this.Controls.Add(this.picEna);
            this.Controls.Add(this.pictureBox3);
            this.Name = "QuestionEnaDis";
            this.Size = new System.Drawing.Size(800, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picEna;
        public System.Windows.Forms.PictureBox picDis;
        public System.Windows.Forms.Label lblPregunta;
        public System.Windows.Forms.LinkLabel lbl6Izq;
        public System.Windows.Forms.PictureBox pic6Izq;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}
