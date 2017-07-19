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
    public partial class ctrDerecho : UserControl
    {
        public ctrDerecho()
        {
            InitializeComponent();
        }

        private void pic1Der_Click(object sender, EventArgs e)
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
        private Control GetLabel(string Name)
        {
            if (Name.Contains("1"))
            {
                return lbl1Der;
            }
            else if (Name.Contains("2"))
            {
                return lbl2Der;
            }
            else if (Name.Contains("3"))
            {
                return lbl3Der;
            }
            else if (Name.Contains("4"))
            {
                return lbl4Der;
            }
            else if (Name.Contains("5"))
            {
                return lbl5Der;
            }
            else 
            {
                return lbl6Der;
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
                        SetButtonEnabled(c.Name, true);
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
            if (Name == "pic1Der")
            {
                frmPrincipalAlt.BotonDer1 = Value;
            }
            else if (Name == "pic2Der")
            {
                frmPrincipalAlt.BotonDer2 = Value;
            }
            else if (Name == "pic3Der")
            {
                frmPrincipalAlt.BotonDer3 = Value;
            }
            else if (Name == "pic4Der")
            {
                frmPrincipalAlt.BotonDer4 = Value;
            }
            else if (Name == "pic5Der")
            {
                frmPrincipalAlt.BotonDer5 = Value;
            }
            else if (Name == "pic6Der")
            {
                frmPrincipalAlt.BotonDer6 = Value;
            }
        }
    }
}
