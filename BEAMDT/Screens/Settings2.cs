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
    public partial class Settings2 : UserControl
    {
        public Settings2()
        {
            InitializeComponent();
        }
        public void Inicio()
        {
            if (Classes.clsConfig.RouteEnabled)
            {
                lblRouEna.Text = "Default Route Enabled: Yes";
            }
            else
            {
                lblRouEna.Text = "Default Route Enabled: No";
            }
            lblRouVal.Text = "Default Route Value: " + Classes.clsConfig.DefaultRoute.ToString();
            if (Classes.clsConfig.RunEnabled)
            {
                lblRunEna.Text = "Default Run Enabled: Yes";
            }
            else
            {
                lblRunEna.Text = "Default Run Enabled: No";
            }
            lblRunval.Text = "Default Run Value: " + Classes.clsConfig.DefaultRun.ToString();
            if (Classes.clsConfig.DirectionEnabled)
            {
                lblDefDirEna.Text = "Default Direction: Yes";
            }
            else
            {
                lblDefDirEna.Text = "Default Direction: No";
            }
            if (Classes.clsConfig.DefaultDirection == 0)
            {
                lblDefDiVa.Text = "Default Direction Value: Inb";
            }
            else
            {
                lblDefDiVa.Text = "Default Direction Value: Outb";
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSettings3();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void picDefRoute_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.oQuestionEnaDis.lblPregunta.Text = "Default Route";
            
            prin.oQuestionEnaDis.ValType = 1;
            prin.ShowQuestionEnaDis();
        }

        private void picDefRun_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.oQuestionEnaDis.lblPregunta.Text = "Default Run";
            prin.oQuestionEnaDis.ValType = 2;
            prin.ShowQuestionEnaDis();
        }

        private void picDefRouteVal_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.oSetValue.SetTextAndLimit("Default Route Value", Classes.clsConfig.DefaultRoute.ToString(), 5);
            prin.oSetValue.ValType = 1;
            prin.ShowSetValue();
        }

        private void picDefRunVal_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.oSetValue.SetTextAndLimit("Default Run Value", Classes.clsConfig.DefaultRun.ToString(), 3);
            prin.oSetValue.ValType = 2;
            prin.ShowSetValue();
        }

        private void picDefDirec_Click(object sender, EventArgs e)
        {

        }

        private void picDefDirec_Click_1(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.oQuestionEnaDis.lblPregunta.Text = "Default Direction";
            //prin.oQuestionEnaDis.returscreen = frmPrincipalAlt.returnscreens.Settings3;
            prin.oQuestionEnaDis.ValType = 3;
            prin.ShowQuestionEnaDis();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSetDirection();
        }

        private void lbl3Der_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSettings3();
        }

        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSettings1();
        }

      
    }
}
