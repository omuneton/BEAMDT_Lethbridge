using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BEAMDT.Screens.Resultados
{
    public partial class ctrResultadoMensaje : UserControl
    {
        public ctrResultadoMensaje()
        {
            InitializeComponent();
        }
        private delegate string deleshowmessesp1(string Button);
        public string GetMessage(string Button)
        {
            foreach (Control c in Controls)
            {
                if (c.Name == Button)
                {
                    Label lbl = (Label)c;
                    if (c.InvokeRequired)
                    {
                        this.BeginInvoke(new deleshowmessesp1(GetMessage), new object[] { Button });
                    }
                    else
                    {
                        return lbl.Text;
                    }
                }
            }
            return "";
        }
        private delegate void deleshowmessesp(string Message, string Button, int Alineacion);
        public void SetMessage(string Message, string Button,int Alineacion)
        {
            foreach (Control c in Controls)
            {
                if (c.Name == Button)
                {
                    Label lbl = (Label)c;
                    if (c.InvokeRequired)
                    {
                        this.BeginInvoke(new deleshowmessesp(SetMessage), new object[] { Message, Button,Alineacion });
                    }
                    else
                    {
                        if (lbl.Text != Message)
                        {
                            lbl.Text = Message;
                        }
                        if (Alineacion == 1)
                        {
                            lbl.TextAlign = ContentAlignment.TopLeft;
                        }
                        else if (Alineacion == 2)
                        {
                            lbl.TextAlign = ContentAlignment.TopCenter;
                        }
                        else
                        {
                            lbl.TextAlign = ContentAlignment.TopRight;
                        }
                    }
                }
            }
        }
    }
}
