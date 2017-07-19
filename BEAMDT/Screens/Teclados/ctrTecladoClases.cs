using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BEAMDT.Screens.Teclados
{
    public partial class ctrTecladoClases : UserControl
    {
        public ctrTecladoClases()
        {
            InitializeComponent();
        }
        private void pic1Izq_Click(object sender, EventArgs e)
        {
            if (!Program.buttontoggle)
            {
                Program.buttontoggle = true;
                frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent.Parent.Parent;
                if ((((Control)sender).Name.Contains("lbl")))
                {
                    prin.HandleButton((Control)sender);
                }
                else
                {
                    prin.HandleButton(GetLabel(((Control)sender).Name));
                }
                
                Program.buttontoggle = false;
            }
            
        }
        private Control GetLabel(string Name)
        {
            if (Name.Contains("10"))
            {
                return lblClass10;
            }
            else if (Name.Contains("2"))
            {
                return lblClass2;
            }
            else if (Name.Contains("3"))
            {
                return lblClass3;
            }
            else if (Name.Contains("4"))
            {
                return lblClass4;
            }
            else if (Name.Contains("5"))
            {
                return lblClass5;
            }
            else if (Name.Contains("6"))
            {
                return lblClass6;
            }
            else if (Name.Contains("7"))
            {
                return lblClass7;
            }
            else if (Name.Contains("8"))
            {
                return lblClass8;
            }
            else if (Name.Contains("9"))
            {
                return lblClass9;
            }
            else 
            {
                return lblClass1;
            }
        }
        private delegate void deleshowmessesp(string Message, string Button);
         public void SetMessageButton(string Message,string Button)
         {
             foreach (Control c in Controls)
             {
                 if (c.Name == Button)
                 {
                     if (c.InvokeRequired)
                     {
                         this.BeginInvoke(new deleshowmessesp(SetMessageButton), new object[] { Message,Button });
                     }
                     else
                     {
                         c.Text = Message;
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
                         this.BeginInvoke(new deleVisible(SetEnabled), new object[] { Enabled, Button });
                     }
                     else
                     {
                         c.Enabled = Enabled;
                         if (Button.Contains("lblClass"))
                         {
                             Button = Button.Replace("lblClass", "");
                             Button = "pic" + Button;
                             SetEnabled(Enabled, Button);
                         }
                     }
                 }
             }
         }
    }
}
