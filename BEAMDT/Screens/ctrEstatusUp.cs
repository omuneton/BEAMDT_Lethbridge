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
    public partial class ctrEstatusUp : UserControl
    {
        public ctrEstatusUp()
        {
            InitializeComponent();
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
                    }
                }
            }
        }
    }
}
