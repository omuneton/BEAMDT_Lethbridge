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
    public partial class SetAutoClose : UserControl
    {
        public int Valor = 0;
        public SetAutoClose()
        {
            InitializeComponent();
        }
        public void Inicio()
        {
            string Horas = "";
            string Min = "";
            if (Classes.clsConfig.AutoCloseTime == 0)
            {
                Min = "Disabled";
            }
            else if (Classes.clsConfig.AutoCloseTime / 4 > 0)
            {
                Horas = ((int)(Classes.clsConfig.AutoCloseTime / 4)).ToString() + " Hours ";
            }
            if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 1)
            {
                Min = "15 Minutes";
            }
            else if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 2)
            {
                Min = "30 Minutes";
            }
            else if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 3)
            {
                Min = "45 Minutes";
            }
            lblValor.Text = Horas + Min;
        }
        private void picdown_Click(object sender, EventArgs e)
        {
            if (Valor > 0)
            {
                Valor--;
            }
            string Horas = "";
            string Min = "";
            if (Valor == 0)
            {
                Min = "Disabled";
            }
            else if (Valor / 4 > 0)
            {
                Horas = ((int)(Valor / 4)).ToString() + " Hours ";
            }
            if (Valor > 0 && Valor % 4 == 1)
            {
                Min = "15 Minutes";
            }
            else if (Valor > 0 && Valor % 4 == 2)
            {
                Min = "30 Minutes";
            }
            else if (Valor > 0 && Valor % 4 == 3)
            {
                Min = "45 Minutes";
            }
            lblValor.Text = Horas + Min;
        }

        private void picUp_Click(object sender, EventArgs e)
        {
            if (Valor < 24)
            {
                Valor++;
            }
            string Horas = "";
            string Min = "";
            if (Valor == 0)
            {
                Min = "Disabled";
            }
            else if (Valor / 4 > 0)
            {
                Horas = ((int)(Valor / 4)).ToString() + " Hours ";
            }
            if (Valor > 0 && Valor % 4 == 1)
            {
                Min = "15 Minutes";
            }
            else if (Valor > 0 && Valor % 4 == 2)
            {
                Min = "30 Minutes";
            }
            else if (Valor > 0 && Valor % 4 == 3)
            {
                Min = "45 Minutes";
            }
            lblValor.Text = Horas + Min;
        }

        

        private void picback_Click_1(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowSettings3();
        }

        private void picOk_Click(object sender, EventArgs e)
        {
            Classes.clsConfig.AutoCloseTime = Valor;
            Classes.clsConfig.SaveConfig();
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowSettings3();
        }

    }
}
