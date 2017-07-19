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
    public partial class ProgramBusNumber : UserControl
    {
        public ProgramBusNumber()
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
            CurrCar = Value.Length;
            Primer = true;
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
        bool toggle = false;
        private void picok_Click(object sender, EventArgs e)
        {
            if (!toggle)
            {
                toggle = true;
                frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
                if (string.IsNullOrEmpty(lblValor.Text))
                {
                    lblResult.Text = "Invalid Value";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    lblResult.Text = "";
                    return;
                }
                if (int.Parse(lblValor.Text)>65535)
                {
                    lblResult.Text = "Invalid Value";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    lblResult.Text = "";
                    return;
                }
                byte[] data = new byte[2];
                data[0] = (byte)(int.Parse(lblValor.Text) & 0xFF);
                data[1] = (byte)((int.Parse(lblValor.Text)>>8) & 0xFF);
                if (prin.Comando((byte)frmPrincipalAlt.ComandosConsolaFarebox.ProgramaNumBus, (byte)data.Length, data) == 1)
                {
                    Program.BusNumber = (int)data[0] + (int)(data[1] << 8);
                    lblResult.Text = "Bus number successfully programmed";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    prin.ShowSetProgramBusData();
                }
                else
                {
                    lblResult.Text = "Bus number not programmed";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                }
                lblResult.Text = "";
                toggle = false;
            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowSetProgramBusData();
        }

    }
}
