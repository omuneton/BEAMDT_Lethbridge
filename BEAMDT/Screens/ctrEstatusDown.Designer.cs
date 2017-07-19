namespace BEAMDT.Screens
{
    partial class ctrEstatusDown
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrEstatusDown));
            this.lblStatus = new System.Windows.Forms.Label();
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Black;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(255)))), ((int)(((byte)(19)))));
            this.lblStatus.Location = new System.Drawing.Point(16, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(522, 20);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picBackground
            // 
            this.picBackground.Image = ((System.Drawing.Image)(resources.GetObject("picBackground.Image")));
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(551, 42);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // ctrEstatusDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.picBackground);
            this.Name = "ctrEstatusDown";
            this.Size = new System.Drawing.Size(551, 42);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.PictureBox picBackground;
    }
}
