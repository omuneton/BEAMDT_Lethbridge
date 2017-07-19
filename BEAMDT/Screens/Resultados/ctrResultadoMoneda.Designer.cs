namespace BEAMDT.Screens.Resultados
{
    partial class ctrResultadoMoneda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrResultadoMoneda));
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // picBackground
            // 
            this.picBackground.Image = ((System.Drawing.Image)(resources.GetObject("picBackground.Image")));
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(548, 236);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lbl4
            // 
            this.lbl4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(155)))), ((int)(((byte)(65)))));
            this.lbl4.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.lbl4.ForeColor = System.Drawing.Color.White;
            this.lbl4.Location = new System.Drawing.Point(340, 133);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(162, 45);
            this.lbl4.Text = "$999.99";
            this.lbl4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl2
            // 
            this.lbl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(179)))), ((int)(((byte)(177)))));
            this.lbl2.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.lbl2.ForeColor = System.Drawing.Color.White;
            this.lbl2.Location = new System.Drawing.Point(340, 49);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(162, 45);
            this.lbl2.Text = "$999.99";
            this.lbl2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(18, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(498, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(18, 128);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(498, 56);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lbl1
            // 
            this.lbl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(179)))), ((int)(((byte)(177)))));
            this.lbl1.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.lbl1.ForeColor = System.Drawing.Color.White;
            this.lbl1.Location = new System.Drawing.Point(110, 49);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(197, 45);
            this.lbl1.Text = "Deposited";
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl3
            // 
            this.lbl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(155)))), ((int)(((byte)(65)))));
            this.lbl3.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.lbl3.ForeColor = System.Drawing.Color.White;
            this.lbl3.Location = new System.Drawing.Point(100, 134);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(207, 45);
            this.lbl3.Text = "Balance";
            this.lbl3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrResultadoMoneda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.picBackground);
            this.Name = "ctrResultadoMoneda";
            this.Size = new System.Drawing.Size(548, 236);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picBackground;
        public System.Windows.Forms.Label lbl4;
        public System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Label lbl1;
        public System.Windows.Forms.Label lbl3;
    }
}
