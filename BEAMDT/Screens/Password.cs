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
    public partial class Password : UserControl
    {
        
        public Password()
        {
            InitializeComponent();
        }
        public int Option = 0;
        public int Limit = 0;
        public int CurrCar = 0;
        bool Primer = false;
        public void SetTextAndLimit(string Title, string Value, int limit)
        {
            lblPregunta.Text = Title;
            lblValor.Text = Value;
            Limit = limit;
            CurrCar = 0;
            Primer = false;
        }
        private void pic1_Click(object sender, EventArgs e)
        {
            if (Limit > CurrCar)
            {
                if (!Primer)
                {
                    Primer = true;
                    lblValor.Text = "1";
                    CurrCar++;
                }
                else
                {
                    lblValor.Text = lblValor.Text + "1";
                    CurrCar++;
                }

            }
        }

        private void picbac_Click(object sender, EventArgs e)
        {
            if (CurrCar > 0)
            {
                lblValor.Text = lblValor.Text.Substring(0, lblValor.Text.Length - 1);
                CurrCar--;
            }
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            if (Limit > CurrCar)
            {
                if (!Primer)
                {
                    Primer = true;
                    lblValor.Text = "2";
                    CurrCar++;
                }
                else
                {
                    lblValor.Text = lblValor.Text + "2";
                    CurrCar++;
                }

            }
        }

        private void pic3_Click(object sender, EventArgs e)
        {
            if (Limit > CurrCar)
            {
                if (!Primer)
                {
                    Primer = true;
                    lblValor.Text = "3";
                    CurrCar++;
                }
                else
                {
                    lblValor.Text = lblValor.Text + "3";
                    CurrCar++;
                }

            }
        }

        private void pic4_Click(object sender, EventArgs e)
        {
            if (Limit > CurrCar)
            {
                if (!Primer)
                {
                    Primer = true;
                    lblValor.Text = "4";
                    CurrCar++;
                }
                else
                {
                    lblValor.Text = lblValor.Text + "4";
                    CurrCar++;
                }

            }
        }

        private void pic5_Click(object sender, EventArgs e)
        {
            if (Limit > CurrCar)
            {
                if (!Primer)
                {
                    Primer = true;
                    lblValor.Text = "5";
                    CurrCar++;
                }
                else
                {
                    lblValor.Text = lblValor.Text + "5";
                    CurrCar++;
                }

            }
        }

        private void pic6_Click(object sender, EventArgs e)
        {
            if (Limit > CurrCar)
            {
                if (!Primer)
                {
                    Primer = true;
                    lblValor.Text = "6";
                    CurrCar++;
                }
                else
                {
                    lblValor.Text = lblValor.Text + "6";
                    CurrCar++;
                }

            }
        }

        private void pic7_Click(object sender, EventArgs e)
        {
            if (Limit > CurrCar)
            {
                if (!Primer)
                {
                    Primer = true;
                    lblValor.Text = "7";
                    CurrCar++;
                }
                else
                {
                    lblValor.Text = lblValor.Text + "7";
                    CurrCar++;
                }

            }
        }

        private void pic8_Click(object sender, EventArgs e)
        {
            if (Limit > CurrCar)
            {
                if (!Primer)
                {
                    Primer = true;
                    lblValor.Text = "8";
                    CurrCar++;
                }
                else
                {
                    lblValor.Text = lblValor.Text + "8";
                    CurrCar++;
                }

            }
        }

        private void pic9_Click(object sender, EventArgs e)
        {
            if (Limit > CurrCar)
            {
                if (!Primer)
                {
                    Primer = true;
                    lblValor.Text = "9";
                    CurrCar++;
                }
                else
                {
                    lblValor.Text = lblValor.Text + "9";
                    CurrCar++;
                }

            }
        }

        private void pic0_Click(object sender, EventArgs e)
        {
            if (Limit > CurrCar)
            {
                if (!Primer)
                {
                    Primer = true;
                    lblValor.Text = "0";
                    CurrCar++;
                }
                else
                {
                    lblValor.Text = lblValor.Text + "0";
                    CurrCar++;
                }

            }
        }

        private void picok_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            if (Option == 0)
            {
                if (lblValor.Text == Classes.clsConfig.Password)
                {
                    prin.ShowSettings1();
                }
                else
                {
                    //prin.MessageSpecial.ClearMessage();
                    //prin.MessageSpecial.SetMessage("Incorrect", 2, 2);
                    //prin.MessageSpecial.SetMessage("Password", 2, 3);
                    
                }
            }
            else
            { 
                if(!String.IsNullOrEmpty(lblValor.Text) && lblValor.Text.Length==4)
                {
                    Classes.clsConfig.Password = lblValor.Text;
                    Classes.clsConfig.SaveConfig();
                    prin.ShowSettings3();
                }
                
            }
        }

        private void picback_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            if (Option == 0)
            {
                if (BEAMDT.Program.VueltaAbierta == 1)
                {
                    Prin.ShowClose();
                }
                else
                {
                    Classes.clsUtils.SetShiftBrightness(false);
                   // Prin.Comando(frmPrincipal.ACTIVAMENS, 0, new byte[1]);
                   // Prin.ShowMessageEsp(1);
                }
            }
            else
            {
                Prin.ShowSettings3();
            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowClose();
        }

       

       

       

      
    }
}
