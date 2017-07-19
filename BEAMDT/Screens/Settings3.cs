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
    public partial class Settings3 : UserControl
    {
        public Settings3()
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
                Horas = ((int)(Classes.clsConfig.AutoCloseTime / 4)).ToString() + " H ";
            }
            if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 1)
            {
                Min = "15 M";
            }
            else if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 2)
            {
                Min = "30 M";
            }
            else if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 3)
            {
                Min = "45 M";
            }
            lblAutoClo.Text ="Auto Close Time: " + Horas + Min;
        }
        

        private void picDefDirec_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSystemInfo();
        }

        private void picChangePass_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            
            prin.oPassword.Option = 1;
            prin.oPassword.SetTextAndLimit("New Password", "", 4);
            prin.ShowPassword();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
           
        }

        private void picAutoClose_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSetAutoClose();
        }

        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSettings2();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Program.ShowTaskbar();
            Cursor.Show();
            Application.Exit();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowProgramConsole();
        }

        
    }
}
