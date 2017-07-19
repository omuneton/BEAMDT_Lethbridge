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
    public partial class QuestionEnaDis : UserControl
    {
        public int ValType = 0;
        public QuestionEnaDis()
        {
            InitializeComponent();
        }

        private void picEna_Click(object sender, EventArgs e)
        {
            if (ValType == 1)//route
            {
                Classes.clsConfig.RouteEnabled = true;
            }
            else if (ValType == 2)//run
            {
                Classes.clsConfig.RunEnabled = true;
            }
            else //direction
            {
                Classes.clsConfig.DirectionEnabled = true;
            }
            Classes.clsConfig.SaveConfig();
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            if (ValType == 1)//route
            {
                Prin.ShowSettings2();
            }
            else if (ValType == 2)//run
            {
                Prin.ShowSettings2();
            }
            else //direction
            {
                Prin.ShowSettings3();
            }
           
        }

        private void picDis_Click(object sender, EventArgs e)
        {
            if (ValType == 1)//route
            {
                Classes.clsConfig.RouteEnabled = false;
            }
            else if (ValType == 2)//run
            {
                Classes.clsConfig.RunEnabled = false;
            }
            else //direction
            {
                Classes.clsConfig.DirectionEnabled = false;
            }
            Classes.clsConfig.SaveConfig();
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            if (ValType == 1)//route
            {
                Prin.ShowSettings2();
            }
            else if (ValType == 2)//run
            {
                Prin.ShowSettings2();
            }
            else //direction
            {
                Prin.ShowSettings3();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            if (ValType == 1)//route
            {
                Prin.ShowSettings2();
            }
            else if (ValType == 2)//run
            {
                Prin.ShowSettings2();
            }
            else //direction
            {
                Prin.ShowSettings2();
            }
        }

    }
}
