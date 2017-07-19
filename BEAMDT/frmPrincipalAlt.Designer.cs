namespace BEAMDT
{
    partial class frmPrincipalAlt
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPortFarebox = new System.IO.Ports.SerialPort(this.components);
            this.serialPortGPS = new System.IO.Ports.SerialPort(this.components);
            this.SuspendLayout();
            // 
            // serialPortFarebox
            // 
            this.serialPortFarebox.BaudRate = 115200;
            // 
            // serialPortGPS
            // 
            this.serialPortGPS.BaudRate = 115200;
            this.serialPortGPS.PortName = "COM2";
            this.serialPortGPS.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortGPS_DataReceived);
            // 
            // frmPrincipalAlt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrincipalAlt";
            this.Load += new System.EventHandler(this.frmPrincipalAlt_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmPrincipalAlt_Closing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmPrincipalAlt_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPrincipalAlt_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPortGPS;
        public System.IO.Ports.SerialPort serialPortFarebox;
    }
}