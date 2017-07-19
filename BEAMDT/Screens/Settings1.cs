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
    public partial class Settings1 : UserControl
    {
        public Settings1()
        {
            InitializeComponent();
        }

        public void Inicio(string RMC,string GGA)
        {
            
            lblrmc.Text = "RMC: " + RMC;
            lblgga.Text = "GGA: " + GGA;
            if (Program.GPSOK)
            {
                lblGPSFix.Text = "GPS Fix";
            }
            else
            {
                lblGPSFix.Text = "GPS Not Fixed";
            }
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            if (Classes.clsConfig.TimeFormat == 1)
            {
                lblTime.Text = "Time Format: 12H";
            }
            else
            {
                lblTime.Text = "Time Format: 24H";
            }
            string wifistatus=Classes.clsNetworkStatus.IsConnected();
            if (wifistatus.Contains("Connected"))
            {
                lblWifi.Text = "WIFI Status: " + wifistatus;
            }
            else
            {
                lblWifi.Text = "WIFI Status: Not Connected";
            }
            
        }

        

        

        private void picChBackLi_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowFTP1();
        }

        private void picTimeFor_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSetTimeFormat();
        }

        private void picResetUnit_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowBluetooth1();
        }

        private void picResetSAC_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSetProgramBusData();
        }

        private void pic1Der_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ReceiveFile();
        }

        private void lbl3Der_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;

            //prin.SendFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\UsbdpFP.sys");
            prin.SendFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\PKTPT FT 0090 20150710 170052.its");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
                if (Program.GPSOK)
                {
                    lblGPSFix.Text = "GPS Fix";
                }
                else
                {
                    lblGPSFix.Text = "GPS Not Fixed";
                }
                lblrmc.Text = "RMC: " + prin.RMC;
                lblgga.Text = "GGA: " + prin.GGA;
                lblBad.Text = "Bad: " + Program.contTramasBAD;
                lblGood.Text = "Good: " + Program.contTramas;
                string wifistatus = Classes.clsNetworkStatus.IsConnected();
                if (wifistatus.Contains("Connected"))
                {
                    lblWifi.Text = "WIFI Status: " + wifistatus;
                }
                else
                {
                    lblWifi.Text = "WIFI Status: Not Connected";
                }
                Application.DoEvents();
            }
            catch
            {
                timer1.Enabled = false;
            }
        }

        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            if (BEAMDT.Program.VueltaAbierta == 1)
            {

                //Prin.ShowMainScreen();
            }
            else
            {

                Prin.ShowClose();
                Prin.StartProcessClosed();
            }
        }

        private void lbl3Der_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSettings2();
        }
    }
}
