using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BEAMDT.Screens
{
    public partial class Close : UserControl
    {
        public Close()
        {
            InitializeComponent();
        }
        private delegate void deleshowmessesp(string Message, string Button, int Alineacion);
        public void SetMessage(string Message, string Button, int Alineacion)
        {
            foreach (Control c in Controls)
            {
                if (c.Name == Button)
                {
                    Label lbl = (Label)c;
                    if (c.InvokeRequired)
                    {
                        this.BeginInvoke(new deleshowmessesp(SetMessage), new object[] { Message, Button, Alineacion });
                    }
                    else
                    {
                        if (lbl.Text != Message)
                        {
                            lbl.Text = Message;
                        }
                        if (Alineacion == 1)
                        {
                            lbl.TextAlign = ContentAlignment.TopLeft;
                        }
                        else if (Alineacion == 2)
                        {
                            lbl.TextAlign = ContentAlignment.TopCenter;
                        }
                        else
                        {
                            lbl.TextAlign = ContentAlignment.TopRight;
                        }
                    }
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (DateTime.Now.Subtract(Program.TimeStampOpenClose).TotalSeconds < 15)
            {
                return;
            }
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.OpenShift();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if (Program.VueltaAbierta != -1)
            {
                frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
                Prin.OpenShift();
            }
        }

        private void pic1Izq_Click(object sender, EventArgs e)
        {

        }

        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            if (DateTime.Now.Subtract(Program.TimeStampOpenClose).TotalSeconds < 15)
            {
                return;
            }
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.OpenShift();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Program.VueltaAbierta = 1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void SafeZoneDetect()
        {
            Classes.clsNetworkStatus.ResetWifi=true;
        }
        private void linkLabel1_Click_1(object sender, EventArgs e)
        {
            SafeZoneDetect();
        }

        private void lbl6Der_GotFocus(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            if (Prin != null)
            {
                Prin.Focus();
            }
        }
    }
}
