namespace BEAMDT.Screens
{
    partial class Principal
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
            this.PanelNotification = new System.Windows.Forms.Panel();
            this.panelIzquierdo = new System.Windows.Forms.Panel();
            this.panelDerecho = new System.Windows.Forms.Panel();
            this.panelInfoRuta = new System.Windows.Forms.Panel();
            this.panelArriba = new System.Windows.Forms.Panel();
            this.panelAbajo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PanelNotification
            // 
            this.PanelNotification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.PanelNotification.Location = new System.Drawing.Point(127, 435);
            this.PanelNotification.Name = "PanelNotification";
            this.PanelNotification.Size = new System.Drawing.Size(547, 42);
            // 
            // panelIzquierdo
            // 
            this.panelIzquierdo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(112, 480);
            // 
            // panelDerecho
            // 
            this.panelDerecho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.panelDerecho.Location = new System.Drawing.Point(688, 0);
            this.panelDerecho.Name = "panelDerecho";
            this.panelDerecho.Size = new System.Drawing.Size(112, 480);
            // 
            // panelInfoRuta
            // 
            this.panelInfoRuta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.panelInfoRuta.Location = new System.Drawing.Point(127, 3);
            this.panelInfoRuta.Name = "panelInfoRuta";
            this.panelInfoRuta.Size = new System.Drawing.Size(547, 44);
            // 
            // panelArriba
            // 
            this.panelArriba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.panelArriba.Location = new System.Drawing.Point(127, 55);
            this.panelArriba.Name = "panelArriba";
            this.panelArriba.Size = new System.Drawing.Size(547, 235);
            // 
            // panelAbajo
            // 
            this.panelAbajo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.panelAbajo.Location = new System.Drawing.Point(125, 298);
            this.panelAbajo.Name = "panelAbajo";
            this.panelAbajo.Size = new System.Drawing.Size(547, 128);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.Controls.Add(this.panelAbajo);
            this.Controls.Add(this.panelArriba);
            this.Controls.Add(this.panelInfoRuta);
            this.Controls.Add(this.panelDerecho);
            this.Controls.Add(this.panelIzquierdo);
            this.Controls.Add(this.PanelNotification);
            this.Name = "Principal";
            this.Size = new System.Drawing.Size(800, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelNotification;
        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.Panel panelDerecho;
        private System.Windows.Forms.Panel panelInfoRuta;
        private System.Windows.Forms.Panel panelAbajo;
        public System.Windows.Forms.Panel panelArriba;
    }
}
