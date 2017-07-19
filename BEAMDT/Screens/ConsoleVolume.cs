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
    public partial class ConsoleVolume : UserControl
    {
        public ConsoleVolume()
        {
            InitializeComponent();
        }
        Classes.clsVolumen.Volumes OriginalVolume;
        public void Init()
        {
            OriginalVolume = Classes.clsConfig.Volume;
            lblValor.Text = Classes.clsVolumen.GetValue(Classes.clsConfig.Volume);
        }
        
        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            Classes.clsConfig.Volume = OriginalVolume;
            Classes.clsVolumen.Volume = Classes.clsConfig.Volume;
            
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowProgramConsole();
        }

        private void picUp_Click(object sender, EventArgs e)
        {
            Classes.clsConfig.Volume = Classes.clsVolumen.VolumeUp(Classes.clsConfig.Volume);
            Classes.clsVolumen.Volume = Classes.clsConfig.Volume;
            System.Media.SystemSounds.Beep.Play();
            lblValor.Text = Classes.clsVolumen.GetValue(Classes.clsConfig.Volume);
        }

        private void picdown_Click(object sender, EventArgs e)
        {
            Classes.clsConfig.Volume = Classes.clsVolumen.VolumeDown(Classes.clsConfig.Volume);
            Classes.clsVolumen.Volume = Classes.clsConfig.Volume;
            System.Media.SystemSounds.Beep.Play();
            lblValor.Text = Classes.clsVolumen.GetValue(Classes.clsConfig.Volume);
        }

        private void lbl3Der_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowProgramConsole();
            Classes.clsConfig.SaveConfig();
        }

    }
}
