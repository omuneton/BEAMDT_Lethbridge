namespace BEAMDT.Screens
{
    partial class Settings1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings1));
            this.lblPregunta = new System.Windows.Forms.Label();
            this.lblBright = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.picChBackLi = new System.Windows.Forms.PictureBox();
            this.picTimeFor = new System.Windows.Forms.PictureBox();
            this.picResetUnit = new System.Windows.Forms.PictureBox();
            this.picResetSAC = new System.Windows.Forms.PictureBox();
            this.lblWifi = new System.Windows.Forms.Label();
            this.lblrmc = new System.Windows.Forms.Label();
            this.lblgga = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbl3Der = new System.Windows.Forms.LinkLabel();
            this.pic3Der = new System.Windows.Forms.PictureBox();
            this.lbl6Izq = new System.Windows.Forms.LinkLabel();
            this.pic6Izq = new System.Windows.Forms.PictureBox();
            this.lblGPSFix = new System.Windows.Forms.Label();
            this.lblGood = new System.Windows.Forms.Label();
            this.lblBad = new System.Windows.Forms.Label();
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
            this.lblPregunta.Text = "MDT Settings";
            this.lblPregunta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBright
            // 
            this.lblBright.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblBright.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Bold);
            this.lblBright.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblBright.Location = new System.Drawing.Point(3, 71);
            this.lblBright.Name = "lblBright";
            this.lblBright.Size = new System.Drawing.Size(593, 54);
            this.lblBright.Text = "Set FTP Settings";
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblTime.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTime.Location = new System.Drawing.Point(3, 134);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(384, 45);
            this.lblTime.Text = "Time Format: 24H";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.label3.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(3, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(432, 45);
            this.label3.Text = "Secure Zone Settings";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.label4.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(3, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(350, 51);
            this.label4.Text = "Program Farebox";
            // 
            // picChBackLi
            // 
            this.picChBackLi.Image = ((System.Drawing.Image)(resources.GetObject("picChBackLi.Image")));
            this.picChBackLi.Location = new System.Drawing.Point(649, 71);
            this.picChBackLi.Name = "picChBackLi";
            this.picChBackLi.Size = new System.Drawing.Size(52, 50);
            this.picChBackLi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picChBackLi.Click += new System.EventHandler(this.picChBackLi_Click);
            // 
            // picTimeFor
            // 
            this.picTimeFor.Image = ((System.Drawing.Image)(resources.GetObject("picTimeFor.Image")));
            this.picTimeFor.Location = new System.Drawing.Point(649, 134);
            this.picTimeFor.Name = "picTimeFor";
            this.picTimeFor.Size = new System.Drawing.Size(52, 50);
            this.picTimeFor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTimeFor.Click += new System.EventHandler(this.picTimeFor_Click);
            // 
            // picResetUnit
            // 
            this.picResetUnit.Image = ((System.Drawing.Image)(resources.GetObject("picResetUnit.Image")));
            this.picResetUnit.Location = new System.Drawing.Point(649, 200);
            this.picResetUnit.Name = "picResetUnit";
            this.picResetUnit.Size = new System.Drawing.Size(52, 50);
            this.picResetUnit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picResetUnit.Click += new System.EventHandler(this.picResetUnit_Click);
            // 
            // picResetSAC
            // 
            this.picResetSAC.Image = ((System.Drawing.Image)(resources.GetObject("picResetSAC.Image")));
            this.picResetSAC.Location = new System.Drawing.Point(649, 262);
            this.picResetSAC.Name = "picResetSAC";
            this.picResetSAC.Size = new System.Drawing.Size(52, 50);
            this.picResetSAC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picResetSAC.Click += new System.EventHandler(this.picResetSAC_Click);
            // 
            // lblWifi
            // 
            this.lblWifi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblWifi.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.lblWifi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblWifi.Location = new System.Drawing.Point(150, 320);
            this.lblWifi.Name = "lblWifi";
            this.lblWifi.Size = new System.Drawing.Size(469, 44);
            this.lblWifi.Text = "Wifi Status:";
            // 
            // lblrmc
            // 
            this.lblrmc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblrmc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblrmc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblrmc.Location = new System.Drawing.Point(150, 364);
            this.lblrmc.Name = "lblrmc";
            this.lblrmc.Size = new System.Drawing.Size(443, 51);
            this.lblrmc.Text = "RMC:";
            // 
            // lblgga
            // 
            this.lblgga.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblgga.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblgga.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblgga.Location = new System.Drawing.Point(150, 426);
            this.lblgga.Name = "lblgga";
            this.lblgga.Size = new System.Drawing.Size(446, 54);
            this.lblgga.Text = "GGA:";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            this.lbl3Der.TabIndex = 43;
            this.lbl3Der.TabStop = false;
            this.lbl3Der.Text = "Next";
            this.lbl3Der.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl3Der.Click += new System.EventHandler(this.lbl3Der_Click_1);
            // 
            // pic3Der
            // 
            this.pic3Der.Image = ((System.Drawing.Image)(resources.GetObject("pic3Der.Image")));
            this.pic3Der.Location = new System.Drawing.Point(691, 422);
            this.pic3Der.Name = "pic3Der";
            this.pic3Der.Size = new System.Drawing.Size(108, 56);
            this.pic3Der.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic3Der.Click += new System.EventHandler(this.lbl3Der_Click_1);
            // 
            // lbl6Izq
            // 
            this.lbl6Izq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(255)))), ((int)(((byte)(19)))));
            this.lbl6Izq.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lbl6Izq.ForeColor = System.Drawing.Color.Black;
            this.lbl6Izq.Location = new System.Drawing.Point(10, 429);
            this.lbl6Izq.Name = "lbl6Izq";
            this.lbl6Izq.Size = new System.Drawing.Size(98, 40);
            this.lbl6Izq.TabIndex = 42;
            this.lbl6Izq.TabStop = false;
            this.lbl6Izq.Text = "Back";
            this.lbl6Izq.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl6Izq.Click += new System.EventHandler(this.lbl6Izq_Click);
            // 
            // pic6Izq
            // 
            this.pic6Izq.Image = ((System.Drawing.Image)(resources.GetObject("pic6Izq.Image")));
            this.pic6Izq.Location = new System.Drawing.Point(2, 423);
            this.pic6Izq.Name = "pic6Izq";
            this.pic6Izq.Size = new System.Drawing.Size(108, 56);
            this.pic6Izq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic6Izq.Click += new System.EventHandler(this.lbl6Izq_Click);
            // 
            // lblGPSFix
            // 
            this.lblGPSFix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblGPSFix.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblGPSFix.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblGPSFix.Location = new System.Drawing.Point(15, 363);
            this.lblGPSFix.Name = "lblGPSFix";
            this.lblGPSFix.Size = new System.Drawing.Size(95, 16);
            this.lblGPSFix.Text = "GPS Not Fixed";
            // 
            // lblGood
            // 
            this.lblGood.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblGood.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblGood.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblGood.Location = new System.Drawing.Point(13, 379);
            this.lblGood.Name = "lblGood";
            this.lblGood.Size = new System.Drawing.Size(95, 16);
            this.lblGood.Text = "Good";
            this.lblGood.Visible = false;
            // 
            // lblBad
            // 
            this.lblBad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblBad.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblBad.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblBad.Location = new System.Drawing.Point(15, 395);
            this.lblBad.Name = "lblBad";
            this.lblBad.Size = new System.Drawing.Size(95, 16);
            this.lblBad.Text = "Bad";
            this.lblBad.Visible = false;
            // 
            // Settings1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(110)))), ((int)(((byte)(165)))));
            this.Controls.Add(this.lblBad);
            this.Controls.Add(this.lblGood);
            this.Controls.Add(this.lblGPSFix);
            this.Controls.Add(this.lbl3Der);
            this.Controls.Add(this.pic3Der);
            this.Controls.Add(this.lbl6Izq);
            this.Controls.Add(this.pic6Izq);
            this.Controls.Add(this.lblgga);
            this.Controls.Add(this.lblrmc);
            this.Controls.Add(this.lblWifi);
            this.Controls.Add(this.picResetSAC);
            this.Controls.Add(this.picResetUnit);
            this.Controls.Add(this.picTimeFor);
            this.Controls.Add(this.picChBackLi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblBright);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.pictureBox3);
            this.Name = "Settings1";
            this.Size = new System.Drawing.Size(800, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picChBackLi;
        private System.Windows.Forms.PictureBox picTimeFor;
        private System.Windows.Forms.PictureBox picResetUnit;
        private System.Windows.Forms.PictureBox picResetSAC;
        public System.Windows.Forms.Label lblBright;
        public System.Windows.Forms.Label lblTime;
        public System.Windows.Forms.Label lblWifi;
        public System.Windows.Forms.Label lblrmc;
        public System.Windows.Forms.Label lblgga;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.LinkLabel lbl3Der;
        public System.Windows.Forms.PictureBox pic3Der;
        public System.Windows.Forms.LinkLabel lbl6Izq;
        public System.Windows.Forms.PictureBox pic6Izq;
        public System.Windows.Forms.Label lblGPSFix;
        public System.Windows.Forms.Label lblGood;
        public System.Windows.Forms.Label lblBad;
    }
}
