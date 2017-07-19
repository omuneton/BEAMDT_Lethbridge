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
    public partial class ProgramConsole : UserControl
    {
        public ProgramConsole()
        {
            InitializeComponent();
        }
        public void Inicio()
        { 
           
        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
           
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowConsoleVolume();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowConsoleBrightness();
        }

        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSettings3();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = Classes.clsMemoryStatus.GetStatus();
        }
    }
}
