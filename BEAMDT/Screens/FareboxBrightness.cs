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
    public partial class FareboxBrightness : UserControl
    {
        public FareboxBrightness()
        {
            InitializeComponent();
        }

        public void Init()
        {

        }

        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSetProgramBusData();
        }
    }
}
