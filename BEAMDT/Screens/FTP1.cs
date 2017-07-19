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
    public partial class FTP1 : UserControl
    {
        public FTP1()
        {
            InitializeComponent();
        }
        public void Inicio()
        {
            inputPanel1.Enabled = true;
            txtFTPServer.Text = Classes.clsConfig.FTPServer;
            txtFTPServer.Focus();
    
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            inputPanel1.Enabled = false;
            Prin.Focus();
            Prin.ShowSettings1();
        }


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFTPServer.Text))
            {
                lblResultado.Text = "Invalid Value";
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(3000);
                lblResultado.Text = "";
                return;
            }
            inputPanel1.Enabled = false;
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Classes.clsConfig.FTPServer = txtFTPServer.Text;
            Classes.clsConfig.SaveConfig();
            Prin.ShowFTP2();
        }

    }
}
