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
    public partial class FTP2 : UserControl
    {
        public FTP2()
        {
            InitializeComponent();
        }
        public void Inicio()
        {
            inputPanel1.Enabled = true;
            txtFTPUser.Text = Classes.clsConfig.User;
            txtFTPUser.Focus();

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.Focus();
            Prin.ShowFTP1();
        }

        
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtFTPUser.Text))
            {
                lblResultado.Text = "Invalid Value";
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(3000);
                lblResultado.Text = "";
                return;
            }
            inputPanel1.Enabled = false;
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Classes.clsConfig.User = txtFTPUser.Text;
            Classes.clsConfig.SaveConfig();
            Prin.ShowFTP3();
        }

        
    }
}
