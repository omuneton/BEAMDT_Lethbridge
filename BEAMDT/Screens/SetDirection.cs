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
    public partial class SetDirection : UserControl
    {
        public SetDirection()
        {
            InitializeComponent();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Classes.clsConfig.DefaultDirection = 0;
            Classes.clsConfig.SaveConfig();
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowSettings2();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Classes.clsConfig.DefaultDirection = 1;
            Classes.clsConfig.SaveConfig();
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowSettings2();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowSettings2();
            
        }
    }
}
