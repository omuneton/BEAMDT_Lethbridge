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
    public partial class FareboxVolume : UserControl
    {
        public FareboxVolume()
        {
            InitializeComponent();
        }

        Classes.clsVolumen.Volumes OriginalVolume;
        public void Init()
        {
            OriginalVolume = Program.FareboxVolume;
            lblValor.Text = Classes.clsVolumen.GetValue(Program.FareboxVolume);
        }
        

        private void picUp_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            bool noDown=false;
            if (Program.FareboxVolume == BEAMDT.Classes.clsVolumen.Volumes.VERY_HIGH)
            {
                noDown = true;
            }
            Program.FareboxVolume = Classes.clsVolumen.VolumeUp(Program.FareboxVolume);
            byte[] Data = new byte[1];
            Data[0] = (byte)Classes.clsVolumen.GetValueCommand(Program.FareboxVolume);
            if (prin.Comando((byte)frmPrincipalAlt.ComandosConsolaFarebox.ProgramarVolumen, 1, Data) == 1)
            {
                lblValor.Text = Classes.clsVolumen.GetValue(Program.FareboxVolume);
                lblResult.Text = "Bus volume successfully programed";
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(3000);
                lblResult.Text = "";
            }
            else
            {
                lblResult.Text = "Bus volume not programmed";
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(3000);
                lblResult.Text = "";
                if (!noDown)
                {
                    Program.FareboxVolume = Classes.clsVolumen.VolumeDown(Program.FareboxVolume);
                }
            }
        }

        private void picdown_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            bool noUp = false;
            if (Program.FareboxVolume == BEAMDT.Classes.clsVolumen.Volumes.OFF)
            {
                noUp = true;
            }
            Program.FareboxVolume = Classes.clsVolumen.VolumeDown(Program.FareboxVolume);
            byte[] Data = new byte[1];
            Data[0] = (byte)Classes.clsVolumen.GetValueCommand(Program.FareboxVolume);
            if (prin.Comando((byte)frmPrincipalAlt.ComandosConsolaFarebox.ProgramarVolumen, 1, Data) == 1)
            {
                lblValor.Text = Classes.clsVolumen.GetValue(Program.FareboxVolume);
                lblResult.Text = "Bus volume successfully programed";
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(3000);
                lblResult.Text = "";
            }
            else
            {
                lblResult.Text = "Bus volume not programmed";
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(3000);
                lblResult.Text = "";
                if (!noUp)
                {
                    Program.FareboxVolume = Classes.clsVolumen.VolumeUp(Program.FareboxVolume);
                }
            }
        }

        private void lbl3Der_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSetProgramBusData();
        }
        
        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSetProgramBusData();
        }
    }
}
