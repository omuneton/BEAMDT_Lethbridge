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
    public partial class ProgramBusData : UserControl
    {
        public ProgramBusData()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowSettings1();
        }

        private void picChBackLi_Click_1(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.oProgramBusNumber.SetTextAndLimit("Program Bus Number", Program.BusNumber.ToString(), 5);
            Prin.ShowSetProgramBusNumber();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowSetProgramBusDate();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowFareboxVolume();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowFareboxBrightness();
        }
    }
}
