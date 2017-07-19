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
    public partial class TestScreen : UserControl
    {
        public TestScreen()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        public void inicio()
        {
            timer1.Enabled = true;
            //cli = new BEAMDT.Bluetooth.BluetoothCliente();
            //cli.StartScan("TP-LINK_Music");
        }
        Bluetooth.BluetoothCliente cli;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //label2.Text = "Procesando";
            //this.Update();
            //Bluetooth.BluetoothCliente cli = new BEAMDT.Bluetooth.BluetoothCliente();
            ////if (cli.ZonaSegura("TP-LINK_Music"))
            //if (cli.ZonaSegura("Logitech BT Adapter"))
            //{
            //    label1.Text = "En zona segura";
            //}
            //else
            //{
            //    label1.Text = "No estoy en zona segura";
            //}
            //label2.Text = "Esperando";
            //timer1.Enabled = true;

            timer1.Enabled = true;
            //label2.Text = cli.Mensaje;
            //label17.Text = cli.Dev;
            this.Update();
           
        }

        private void label6_ParentChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void label11_ParentChanged(object sender, EventArgs e)
        {

        }

        private void label10_ParentChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

       

        
    }
}
