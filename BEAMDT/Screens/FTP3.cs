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
    public partial class FTP3 : UserControl
    {
        public FTP3()
        {
            InitializeComponent();
        }
        public void Inicio()
        {
            inputPanel1.Enabled = true;
            txtFTPPassword.Text = Classes.clsConfig.PasswordFTP;
            txtFTPPassword.Focus();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowFTP2();
        }

        

        private void picOk_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFTPPassword.Text))
            {
                lblResultado.Text = "Invalid Value";
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(3000);
                lblResultado.Text = "";
                return;
            }
            inputPanel1.Enabled = false;
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Classes.clsConfig.PasswordFTP = txtFTPPassword.Text;
            Classes.clsConfig.SaveConfig();
            Prin.Focus();
            Prin.ShowSettings1();
        }


    }
}
