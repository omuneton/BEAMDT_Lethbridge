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
    public partial class SetValue : UserControl
    {
        public SetValue()
        {
            InitializeComponent();
        }
        public int ValType = 0;
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
        public void Inicio()
        {
            if (ValType == 1)//route
            {
                lblValor.Text = Classes.clsConfig.DefaultRoute.ToString();
            }
            else
            {
                lblValor.Text = Classes.clsConfig.DefaultRun.ToString();
            }
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
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            if (ValType == 1)//route
            {
                if (string.IsNullOrEmpty(lblValor.Text))
                {
                    lblResult.Text = "Invalid value";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    lblResult.Text = "";
                    return;
                }
                if (int.Parse(lblValor.Text) > 65535)
                {
                    lblResult.Text = "Invalid value";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    lblResult.Text = "";
                    return;
                }
                if (!Prin.oRDR.BuscaRuta(int.Parse(lblValor.Text)))
                {
                    lblResult.Text = "Invalid value";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    lblResult.Text = "";
                    return;
                }
                Classes.clsConfig.DefaultRoute = int.Parse(lblValor.Text);
            }
            else if (ValType == 2)//run
            {
                if (string.IsNullOrEmpty(lblValor.Text))
                {
                    lblResult.Text = "Invalid value";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    lblResult.Text = "";
                    return;
                }
                if (int.Parse(lblValor.Text) > 255)
                {
                    lblResult.Text = "Invalid value";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    lblResult.Text = "";
                    return;
                }
                Classes.clsConfig.DefaultRun = int.Parse(lblValor.Text);
            }
            
            Classes.clsConfig.SaveConfig();
            
            
            Prin.ShowSettings2();
            
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            
            Prin.ShowSettings2();
            
        }

        

        
    }
}
