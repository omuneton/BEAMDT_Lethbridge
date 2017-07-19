namespace BEAMDT.Screens.Resultados
{
    partial class ctrResultadoOpen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrResultadoOpen));
            this.lblTime = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.White;
            this.lblTime.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.Green;
            this.lblTime.Location = new System.Drawing.Point(391, 10);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(136, 32);
            this.lblTime.Text = "00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl2
            // 
            this.lbl2.BackColor = System.Drawing.Color.White;
            this.lbl2.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.lbl2.Location = new System.Drawing.Point(14, 80);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(513, 45);
            this.lbl2.Text = "Waiting...";
            this.lbl2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl3
            // 
            this.lbl3.BackColor = System.Drawing.Color.White;
            this.lbl3.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.lbl3.Location = new System.Drawing.Point(14, 128);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(513, 45);
            this.lbl3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl4
            // 
            this.lbl4.BackColor = System.Drawing.Color.White;
            this.lbl4.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.lbl4.Location = new System.Drawing.Point(14, 173);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(513, 45);
            this.lbl4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl1
            // 
            this.lbl1.BackColor = System.Drawing.Color.White;
            this.lbl1.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.lbl1.Location = new System.Drawing.Point(14, 35);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(513, 45);
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picBackground
            // 
            this.picBackground.Image = ((System.Drawing.Image)(resources.GetObject("picBackground.Image")));
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(548, 236);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.White;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblVersion.ForeColor = System.Drawing.Color.Green;
            this.lblVersion.Location = new System.Drawing.Point(16, 204);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(54, 20);
            this.lblVersion.Text = "V 255.0";
            // 
            // ctrResultadoOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.picBackground);
            this.Name = "ctrResultadoOpen";
            this.Size = new System.Drawing.Size(548, 236);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblTime;
        public System.Windows.Forms.Label lbl2;
        public System.Windows.Forms.Label lbl3;
        public System.Windows.Forms.Label lbl4;
        public System.Windows.Forms.Label lbl1;
        public System.Windows.Forms.PictureBox picBackground;
        public System.Windows.Forms.Label lblVersion;
    }
}
