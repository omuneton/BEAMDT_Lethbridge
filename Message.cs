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
    public partial class Message : UserControl
    {
        public Message()
        {
            InitializeComponent();
        }
        //public void SetIconsVisible(bool visible)
        //{
        //    pictureBox1.Visible = visible;
        //    pictureBox2.Visible = visible;
        //    pictureBox3.Visible = visible;
        //    pictureBox4.Visible = visible;
        //    pictureBox5.Visible = visible;
        //    pictureBox6.Visible = visible;
        //    label1.Visible = visible;
        //    label2.Visible = visible;
        //    label3.Visible = visible;
        //    label4.Visible = visible;
        //    label5.Visible = visible;
        //    label6.Visible = visible;
        //}
        //private delegate void messagedelegate(string Text, int align, int row);
        //public void SetMessage(string Text, int align, int row)
        //{
        //    if(row==1)
        //    {
        //        if (lblR1.InvokeRequired)
        //        {
        //            lblR1.BeginInvoke(new messagedelegate(SetMessage), new object[] {Text,align,row});
        //        }
        //        else 
        //        {
        //            lblR1.Text = Text;
        //            if (align == 1)
        //            {
        //                lblR1.TextAlign = ContentAlignment.TopLeft;
        //            }
        //            else if (align == 2)
        //            {
        //                lblR1.TextAlign = ContentAlignment.TopCenter;
        //            }
        //            else
        //            {
        //                lblR1.TextAlign = ContentAlignment.TopRight;
        //            }
        //        }
                
        //    }
        //    else if (row == 2)
        //    {
        //        if (lblR2.InvokeRequired)
        //        {
        //            lblR2.BeginInvoke(new messagedelegate(SetMessage), new object[] { Text, align, row });
        //        }
        //        else
        //        {
        //            lblR2.Text = Text;
        //            if (align == 1)
        //            {
        //                lblR2.TextAlign = ContentAlignment.TopLeft;
        //            }
        //            else if (align == 2)
        //            {
        //                lblR2.TextAlign = ContentAlignment.TopCenter;
        //            }
        //            else
        //            {
        //                lblR2.TextAlign = ContentAlignment.TopRight;
        //            }
        //        }
        //    }
        //    else if (row == 3)
        //    {
        //        if (lblR3.InvokeRequired)
        //        {
        //            lblR3.BeginInvoke(new messagedelegate(SetMessage), new object[] { Text, align, row });

        //        }
        //        else
        //        {
        //            lblR3.Text = Text;
        //            if (align == 1)
        //            {
        //                lblR3.TextAlign = ContentAlignment.TopLeft;
        //            }
        //            else if (align == 2)
        //            {
        //                lblR3.TextAlign = ContentAlignment.TopCenter;
        //            }
        //            else
        //            {
        //                lblR3.TextAlign = ContentAlignment.TopRight;
        //            }
        //        }
        //    }
        //    else if (row == 4)
        //    {
        //        if (lblR4.InvokeRequired)
        //        {
        //            lblR4.BeginInvoke(new messagedelegate(SetMessage), new object[] { Text, align, row });
        //        }
        //        else
        //        {
        //            lblR4.Text = Text;
        //            if (align == 1)
        //            {
        //                lblR4.TextAlign = ContentAlignment.TopLeft;
        //            }
        //            else if (align == 2)
        //            {
        //                lblR4.TextAlign = ContentAlignment.TopCenter;
        //            }
        //            else
        //            {
        //                lblR4.TextAlign = ContentAlignment.TopRight;
        //            }
        //        }
        //    }
            
        //}

        //private void picOpen_Click(object sender, EventArgs e)
        //{
        //    frmPrincipal parent = (frmPrincipal)this.Parent;
        //    byte[] Data = new byte[6];
        //    Data[0] = 1;
        //    Data[1] = 1;
        //    Data[2] = 1;
        //    Data[3] = 1;
        //    Data[4] = 1;
        //    Data[5] = 1;
        //    if (parent.shiftOpen)
        //    {
        //        SetMessage("", 1, 1);
        //        SetMessage("Closed", 2, 2);
        //        SetMessage("", 1, 3);
        //        SetMessage("", 1, 4);
        //        parent.Comando(2, (byte)Data.Length, Data);
        //        parent.shiftOpen = false;
        //    }
        //    else
        //    {
        //        SetMessage("", 1, 1);
        //        SetMessage("Welcome", 2, 2);
        //        SetMessage("Aboard", 2, 3);
        //        SetMessage("", 1, 4);
        //        parent.Comando(1, (byte)Data.Length, Data);
        //        parent.shiftOpen = true;
                
        //    }
        //    //SetIconsVisible(parent.shiftOpen);
        //}

        //private void pic10_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[1];
        //    Data[0] = 1;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.ZonaSegura, (byte)Data.Length, Data);    
        //}

        //private void pictureBox7_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[1];
        //    Data[0] = 0;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.ZonaSegura, (byte)Data.Length, Data);    
        //}

        //private void pictureBox8_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[1];
        //    Data[0] = 1;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.CambioDireccion, (byte)Data.Length, Data);    
        //}

        //private void pictureBox9_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[1];
        //    Data[0] = 1;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.ResetFarebox, 0, Data);
        //}

        //private void pictureBox10_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[1];
        //    Data[0] = 1;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.SolicitaDatosTurno, 0, Data);
        //}

        //private void pictureBox11_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[1];
        //    Data[0] = 1;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.ActivaMensajes, 0, Data);
        //}

        //private void pictureBox12_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[1];
        //    Data[0] = 1;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.DesactivaMensajes, (byte)Data.Length, Data);
        //}

        //private void pictureBox13_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[1];
        //    Data[0] = 1;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.Override, (byte)Data.Length, Data);
        //}

        //private void pictureBox14_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[2];
        //    Data[0] = 1;
        //    Data[1] = 0;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.ProgramaNumBus, (byte)Data.Length, Data);
        //}

        //private void pictureBox15_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[2];
        //    Data[0] = 1;
        //    Data[1] = 0;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.ProgramaRuta, (byte)Data.Length, Data);
        //}

        //private void pictureBox20_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[7];
        //    Data[0] = (byte)DateTime.Now.Day;
        //    Data[1] = (byte)DateTime.Now.Month;
        //    Data[2] = (byte)DateTime.Now.Year;
        //    Data[3] = (byte)DateTime.Now.Hour;
        //    Data[4] = (byte)DateTime.Now.Minute;
        //    Data[5] = (byte)DateTime.Now.Second;
        //    Data[6] = (byte)DateTime.Now.DayOfWeek;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.ProgramaFechaHora, (byte)Data.Length, Data);
        //}

        //private void pictureBox19_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[1];
        //    Data[0] = 1;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.ProgramarOffset, (byte)Data.Length, Data);
        //}

        //private void pictureBox18_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[15];
        //    Data[0] = 0;
        //    Data[1] = 1;
        //    Data[2] = 2;
        //    Data[3] = 3;
        //    Data[4] = 4;
        //    Data[5] = 5;
        //    Data[6] = 6;
        //    Data[7] = 7;
        //    Data[8] = 8;
        //    Data[9] = 9;
        //    Data[10] = 10;
        //    Data[11] = 11;
        //    Data[12] = 12;
        //    Data[13] = 13;
        //    Data[14] = 14;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.EnviaDatosGPS, (byte)Data.Length, Data);
        //}

        //private void pictureBox17_Click(object sender, EventArgs e)
        //{
        //    byte[] Data = new byte[1];
        //    Data[0] = 1;
        //    frmPrincipal Prin = (frmPrincipal)this.Parent;
        //    Prin.Comando((byte)frmPrincipal.ComandosConsolaFarebox.ClaseMoneda, (byte)Data.Length, Data);
        //}

       
    }
}
