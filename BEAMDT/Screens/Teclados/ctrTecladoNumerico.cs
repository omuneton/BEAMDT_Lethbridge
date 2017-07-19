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
    public partial class ctrTecladoNumerico : UserControl
    {
        public int MaxSize=0;
        public Label lblEscribir;
        public ctrTecladoNumerico()
        {
            InitializeComponent();
        }

        private void pic1_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length < MaxSize)
            {
                lblEscribir.Text = lblEscribir.Text + "1";
            }
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length < MaxSize)
            {
                lblEscribir.Text = lblEscribir.Text + "2";
            }
        }

        private void pic3_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length < MaxSize)
            {
                lblEscribir.Text = lblEscribir.Text + "3";
            }
        }

        private void pic4_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length < MaxSize)
            {
                lblEscribir.Text = lblEscribir.Text + "4";
            }
        }

        private void pic5_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length < MaxSize)
            {
                lblEscribir.Text = lblEscribir.Text + "5";
            }
        }

        private void pic6_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length < MaxSize)
            {
                lblEscribir.Text = lblEscribir.Text + "6";
            }
        }

        private void pic7_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length < MaxSize)
            {
                lblEscribir.Text = lblEscribir.Text + "7";
            }
        }

        private void pic8_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length < MaxSize)
            {
                lblEscribir.Text = lblEscribir.Text + "8";
            }
        }

        private void pic9_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length < MaxSize)
            {
                lblEscribir.Text = lblEscribir.Text + "9";
            }
        }

        private void pic0_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length < MaxSize)
            {
                lblEscribir.Text = lblEscribir.Text + "0";
            }
        }

        private void picbac_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length > 0)
            {
                lblEscribir.Text = lblEscribir.Text.Substring(0, lblEscribir.Text.Length - 1);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (lblEscribir.Text.Length > 0)
            {
                lblEscribir.Text = lblEscribir.Text.Substring(0, lblEscribir.Text.Length - 1);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
