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
    public partial class ProgramBusDate : UserControl
    {
        public ProgramBusDate()
        {
            InitializeComponent();
        }
        public void Inicio()
        {
            lblResult.Text = "";
        }
        bool toggle = false;
        private void picChBackLi_Click(object sender, EventArgs e)
        {
            if (!toggle)
            {
                toggle = true;
                frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;

                byte[] data = new byte[7];
                data[0] = (byte)(DateTime.Now.Day);
                data[1] = (byte)(DateTime.Now.Month);
                data[2] = (byte)(DateTime.Now.Year - 2000);
                data[3] = (byte)(DateTime.Now.Hour);
                data[4] = (byte)(DateTime.Now.Minute);
                data[5] = (byte)(DateTime.Now.Second);
                data[6] = (byte)(DateTime.Now.DayOfWeek);
                if (prin.Comando((byte)frmPrincipalAlt.ComandosConsolaFarebox.ProgramaFechaHora, (byte)data.Length, data) == 1)
                {
                    lblResult.Text = "Bus date successfully programmed";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    prin.ShowSetProgramBusData();
                    lblResult.Text = "";
                }
                else
                {
                    lblResult.Text = "Bus date not programmed";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    lblResult.Text = "";
                }
                toggle = false;
            }
            
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.Focus();
            prin.ShowSetProgramBusData();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!toggle)
            {
                toggle = true;
                frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;

                byte[] data = new byte[7];
                data[0] = (byte)(dtpDatetime.Value.Day);
                data[1] = (byte)(dtpDatetime.Value.Month);
                data[2] = (byte)(dtpDatetime.Value.Year - 2000);
                data[3] = (byte)(dtpDatetime.Value.Hour);
                data[4] = (byte)(dtpDatetime.Value.Minute);
                data[5] = (byte)(dtpDatetime.Value.Second);
                data[6] = (byte)(dtpDatetime.Value.DayOfWeek);
                
                if (prin.Comando((byte)frmPrincipalAlt.ComandosConsolaFarebox.ProgramaFechaHora, (byte)data.Length, data) == 1)
                {
                    prin.Focus();
                    lblResult.Text = "Bus date successfully programmed";
                    System.Windows.Forms.Application.DoEvents();
                    frmPrincipalAlt.SynchTime(dtpDatetime.Value);
                    System.Threading.Thread.Sleep(3000);
                    prin.ShowSetProgramBusData();
                    lblResult.Text = "";

                }
                else
                {
                    lblResult.Text = "Bus date not programmed";
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(3000);
                    lblResult.Text = "";
                }
                toggle = false;
            }
        }
    }
}
