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
    public partial class SetTimeFormat : UserControl
    {
        public SetTimeFormat()
        {
            InitializeComponent();
        }
        public void Inicio()
        {
            if (Classes.clsConfig.TimeFormat == 0)
            {
                lblValor.Text = "24H";
            }
            else
            {
                lblValor.Text = "12H";
            }
        }
        public void SetValue(int Value)
        {
            if (Value == 0)
            {
                lblValor.Text = "24H";
            }
            else
            {
                lblValor.Text = "12H";
            }
        }
        private void picdown_Click(object sender, EventArgs e)
        {
            if (lblValor.Text == "12H")
            {
                lblValor.Text = "24H"; 
            }
            else
            {
                lblValor.Text = "12H";
            }
        }

        private void picUp_Click(object sender, EventArgs e)
        {
            if (lblValor.Text == "12H")
            {
                lblValor.Text = "24H";
            }
            else
            {
                lblValor.Text = "12H";
            }
        }

        private void picback_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowSettings1();
        }

        private void picOk_Click(object sender, EventArgs e)
        {
            if (lblValor.Text == "12H")
            {
                Classes.clsConfig.TimeFormat = 1;
            }
            else
            {
                Classes.clsConfig.TimeFormat = 0;
            }
            Classes.clsConfig.SaveConfig();
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowSettings1();
        }

    }
}
