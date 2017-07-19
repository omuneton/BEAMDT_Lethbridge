namespace BEAMDT.Screens.Resultados
{
    partial class ctrResultadoAdjust
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrResultadoAdjust));
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.picdown = new System.Windows.Forms.PictureBox();
            this.picUp = new System.Windows.Forms.PictureBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.lblPregunta = new System.Windows.Forms.Label();
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
            // picdown
            // 
            this.picdown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.picdown.Image = ((System.Drawing.Image)(resources.GetObject("picdown.Image")));
            this.picdown.Location = new System.Drawing.Point(235, 172);
            this.picdown.Name = "picdown";
            this.picdown.Size = new System.Drawing.Size(79, 43);
            this.picdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picdown.Click += new System.EventHandler(this.picdown_Click);
            // 
            // picUp
            // 
            this.picUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.picUp.Image = ((System.Drawing.Image)(resources.GetObject("picUp.Image")));
            this.picUp.Location = new System.Drawing.Point(235, 100);
            this.picUp.Name = "picUp";
            this.picUp.Size = new System.Drawing.Size(79, 43);
            this.picUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUp.Click += new System.EventHandler(this.picUp_Click);
            // 
            // lblValor
            // 
            this.lblValor.BackColor = System.Drawing.Color.White;
            this.lblValor.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblValor.Location = new System.Drawing.Point(9, 55);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(528, 33);
            this.lblValor.Text = "24H";
            this.lblValor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPregunta
            // 
            this.lblPregunta.BackColor = System.Drawing.Color.White;
            this.lblPregunta.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblPregunta.Location = new System.Drawing.Point(9, 12);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(528, 36);
            this.lblPregunta.Text = "Console Brightness:";
            this.lblPregunta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrResultadoAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.picdown);
            this.Controls.Add(this.picUp);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.picBackground);
            this.Name = "ctrResultadoAdjust";
            this.Size = new System.Drawing.Size(548, 236);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picBackground;
        private System.Windows.Forms.PictureBox picdown;
        private System.Windows.Forms.PictureBox picUp;
        public System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.Label lblPregunta;
    }
}
