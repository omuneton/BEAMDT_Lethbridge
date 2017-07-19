using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BEAMDT
{
    public partial class ctrIzquierdo : UserControl
    {
        public ctrIzquierdo()
        {
            InitializeComponent();
        }
        private Control GetLabel(string Name)
        {
            if (Name.Contains("1"))
            {
                return lbl1Izq;
            }
            else if (Name.Contains("2"))
            {
                return lbl2Izq;
            }
            else if (Name.Contains("3"))
            {
                return lbl3Izq;
            }
            else if (Name.Contains("4"))
            {
                return lbl4Izq;
            }
            else if (Name.Contains("5"))
            {
                return lbl5Izq;
            }
            else
            {
                return lbl6Izq;
            }
        }
        private void pic1Izq_Click(object sender, EventArgs e)
        {
            if (!Program.buttontoggle)
            {
                Program.buttontoggle = true;
                frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent.Parent.Parent;
                if (!(((Control)sender).Name.Contains("lbl")))
                {
                    prin.HandleButton(GetLabel(((Control)sender).Name));
                }
                else
                {
                    prin.HandleButton((Control)sender);
                }
                
                Program.buttontoggle = false;
            }
        }
        private delegate void deleshowmessesp(string Message, string Button);
        public void SetMessage(string Message, string Button)
        {
            foreach (Control c in Controls)
            {
                if (c.Name == Button)
                {
                    if (c.InvokeRequired)
                    {
                        this.BeginInvoke(new deleshowmessesp(SetMessage), new object[] { Message, Button });
                    }
                    else
                    {
                        c.Text = Message;
                        return;
                    }
                }
            }
        }
        private delegate void deleVisible(bool Visible, string Button);
        public void SetVisible(bool Visible, string Button)
        {
            foreach (Control c in Controls)
            {
                if (c.Name == Button)
                {
                    if (c.InvokeRequired)
                    {
                        this.BeginInvoke(new deleVisible(SetVisible), new object[] { Visible, Button });
                    }
                    else
                    {
                        c.Visible = Visible;
                        return;
                    }
                }
            }
        }
        private delegate void deleEnabled(bool Enabled, string Button);
        public void SetEnabled(bool Enabled, string Button)
        {
            foreach (Control c in Controls)
            {
                if (c.Name == Button)
                {
                    if (c.InvokeRequired)
                    {
                        this.BeginInvoke(new deleEnabled(SetEnabled), new object[] { Enabled, Button });
                    }
                    else
                    {
                        c.Enabled = Enabled;
                        return;
                    }
                }
            }
        }
        private void SetButtonEnabled(string Name, bool Value)
        {
            if (Name == "pic1Izq")
            {
                frmPrincipalAlt.BotonIzq1 = Value;
            }
            else if (Name == "pic2Izq")
            {
                frmPrincipalAlt.BotonIzq2 = Value;
            }
            else if (Name == "pic3Izq")
            {
                frmPrincipalAlt.BotonIzq3 = Value;
            }
            else if (Name == "pic4Izq")
            {
                frmPrincipalAlt.BotonIzq4 = Value;
            }
            else if (Name == "pic5Izq")
            {
                frmPrincipalAlt.BotonIzq5 = Value;
            }
            else if (Name == "pic6Izq")
            {
                frmPrincipalAlt.BotonIzq6 = Value;
            }
        }
    }
}
