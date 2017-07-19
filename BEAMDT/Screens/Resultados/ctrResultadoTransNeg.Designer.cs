namespace BEAMDT.Screens.Resultados
{
    partial class ctrResultadoTransNeg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrResultadoTransNeg));
            this.PicRes = new System.Windows.Forms.PictureBox();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // PicRes
            // 
            this.PicRes.Image = ((System.Drawing.Image)(resources.GetObject("PicRes.Image")));
            this.PicRes.Location = new System.Drawing.Point(205, 15);
            this.PicRes.Name = "PicRes";
            this.PicRes.Size = new System.Drawing.Size(140, 87);
            this.PicRes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lbl3
            // 
            this.lbl3.BackColor = System.Drawing.Color.White;
            this.lbl3.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.lbl3.Location = new System.Drawing.Point(14, 183);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(513, 45);
            this.lbl3.Text = "Welcome";
            this.lbl3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl2
            // 
            this.lbl2.BackColor = System.Drawing.Color.White;
            this.lbl2.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.lbl2.Location = new System.Drawing.Point(14, 138);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(513, 45);
            this.lbl2.Text = "01234567891234";
            this.lbl2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picBackground
            // 
            this.picBackground.Image = ((System.Drawing.Image)(resources.GetObject("picBackground.Image")));
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(548, 236);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // ctrResultadoTransNeg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.Controls.Add(this.PicRes);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.picBackground);
            this.Name = "ctrResultadoTransNeg";
            this.Size = new System.Drawing.Size(548, 236);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PicRes;
        public System.Windows.Forms.Label lbl3;
        public System.Windows.Forms.Label lbl2;
        public System.Windows.Forms.PictureBox picBackground;
    }
}
