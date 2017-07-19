namespace BEAMDT
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.timer1 = new System.Windows.Forms.Timer();
            this.serialportFarebox = new System.IO.Ports.SerialPort(this.components);
            this.serialportprinter = new System.IO.Ports.SerialPort(this.components);
            this.autocloseTimer = new System.Windows.Forms.Timer();
            this.timerHora = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // serialportFarebox
            // 
            this.serialportFarebox.BaudRate = 115200;
            this.serialportFarebox.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialportFarebox_DataReceived);
            // 
            // serialportprinter
            // 
            this.serialportprinter.BaudRate = 19200;
            this.serialportprinter.PortName = "COM2";
            // 
            // autocloseTimer
            // 
            this.autocloseTimer.Tick += new System.EventHandler(this.autocloseTimer_Tick);
            // 
            // timerHora
            // 
            this.timerHora.Enabled = true;
            this.timerHora.Interval = 1000;
            this.timerHora.Tick += new System.EventHandler(this.timerHora_Tick);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(798, 475);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrincipal";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmPrincipal_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPrincipal_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.IO.Ports.SerialPort serialportFarebox;
        private System.IO.Ports.SerialPort serialportprinter;
        public System.Windows.Forms.Timer autocloseTimer;
        private System.Windows.Forms.Timer timerHora;
    }
}

