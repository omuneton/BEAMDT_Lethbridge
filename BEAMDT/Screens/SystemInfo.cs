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
    public partial class SystemInfo : UserControl
    {
        public SystemInfo()
        {
            InitializeComponent();
        }
        public void Inicio()
        {
            
            lblSystemVersion.Text = Program.SoftwareVersion.ToString();
            lblActionVersion.Text = Program.AlCurrentVer.ToString();
            lblConfigVersion.Text = Program.CfCurrentVer.ToString();
            lblFareVersion.Text = Program.FtCurrentVer.ToString();
            lblHotVersion.Text = Program.HlCurrentVer.ToString();
            lblRDRVersion.Text = Program.RDRVersion.ToString();
            lblProductVersion.Text = Program.PlCurrentVer.ToString();
            lblMacListVersion.Text = Program.MlCurrentVer.ToString();

            lblSystemVersionFBX.Text = Program.FareboxVersion.ToString();
            lblActionVersionFBX.Text = Program.AlFBXVer.ToString();
            lblConfigVersionFBX.Text = Program.CfFBXVer.ToString();
            lblFareVersionFBX.Text = Program.FtAFBXVer.ToString();
            lblHotVersionFBX.Text = Program.HlFBXVer.ToString();
            lblProductVersionFBX.Text = Program.PlFBXVer.ToString();
            lblMacListVersionFBX.Text = Program.MlFBXVer.ToString();
        }


        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSettings3();
        }

     
    }
}
