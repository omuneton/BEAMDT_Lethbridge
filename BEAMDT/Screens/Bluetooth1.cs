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
    public partial class Bluetooth1 : UserControl
    {
        public Bluetooth1()
        {
            InitializeComponent();
        }
        public void Inicio()
        {
            listBox1.Items.Clear();
            lblCurrent.Text ="Current Addresses:";
            foreach (string MAC in Classes.clsConfig.ListAPMACs)
            {
                if (!String.IsNullOrEmpty(MAC))
                {
                    listBox1.Items.Add(MAC);
                }
            }

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            Prin.ShowSettings1();
        }


        private void picOk_Click_1(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
                string MACList = "";
                for (int i = 0; i < listBox1.Items.Count;i++)
                {
                    MACList = MACList + listBox1.Items[i].ToString() + ",";
                }
                if (MACList != "")
                {
                    MACList = MACList.Substring(0, MACList.Length);
                }
                Classes.clsConfig.SetMACList(MACList);
                Classes.clsConfig.SaveConfig();
                Prin.ShowSettings1();
            }
            else
            {
                lblMessage.Text = "Please select a device";
                Application.DoEvents();
                System.Threading.Thread.Sleep(3000);
                lblMessage.Text = "";
            }
            //if(dgDevices.se)
            //if (string.IsNullOrEmpty(txtBluetoothAddress.Text))
            //{
            //    lblResultado.Text = "Invalid Value";
            //    System.Windows.Forms.Application.DoEvents();
            //    System.Threading.Thread.Sleep(3000);
            //    lblResultado.Text = "";
            //    return;
            //}
            //frmPrincipalAlt Prin = (frmPrincipalAlt)this.Parent;
            //Classes.clsConfig.BluetoothMAC = txtBluetoothAddress.Text;
            //Classes.clsConfig.SaveConfig();
            //Prin.ShowSettings1();
        }


        private void picAutoClose_Click_1(object sender, EventArgs e)
        {
            lblMessage.Text = "Scanning for devices please wait...";
            Application.DoEvents();
            //Bluetooth.BluetoothCliente BC = new BEAMDT.Bluetooth.BluetoothCliente();
            //BluetoothDeviceInfo[] Devices = BC.GetDevices();
            DataTable DeviceData = new DataTable();
            DeviceData.Columns.Add("Device");
            DeviceData.Columns.Add("Address");
            List<Classes.clsNetworkStatus.AccessPointInfo> AccessPointList = Classes.clsNetworkStatus.GetAccessPoints();
            //foreach (BluetoothDeviceInfo d in Devices)
            //{
            //    DataRow dr = DeviceData.NewRow();
            //    dr[0] = d.DeviceName;
            //    dr[1] = d.DeviceAddress;
            //    DeviceData.Rows.Add(dr);
            //}
            foreach (Classes.clsNetworkStatus.AccessPointInfo d in AccessPointList)
            {
                DataRow dr = DeviceData.NewRow();
                dr[0] = d.Name;
                dr[1] = d.MAC;
                DeviceData.Rows.Add(dr);
            }
            dgDevices.TableStyles.Clear();
            DataGridTableStyle dgtsStyle = new DataGridTableStyle();
            dgtsStyle.MappingName = DeviceData.ToString();
            DataGridTextBoxColumn dgtsColumn = new DataGridTextBoxColumn();
            dgtsColumn.MappingName = "Device";
            dgtsColumn.HeaderText = "Device";
            dgtsColumn.Width=(int)(dgDevices.Size.Width/2);
            dgtsStyle.GridColumnStyles.Add(dgtsColumn);
            dgtsColumn = new DataGridTextBoxColumn();
            dgtsColumn.MappingName = "Address";
            dgtsColumn.HeaderText = "Address";
            dgtsColumn.Width = (int)(dgDevices.Size.Width / 2); ;
            dgtsStyle.GridColumnStyles.Add(dgtsColumn);
            dgDevices.TableStyles.Add(dgtsStyle);
            dgDevices.DataSource = DeviceData;
            lblMessage.Text = "";
        }

        private void dgDevices_MouseUp(object sender, MouseEventArgs e)
        {
            if(dgDevices.CurrentRowIndex!=-1)
            {
                dgDevices.Select(dgDevices.CurrentRowIndex);
            }
        }

        private void picbac_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (dgDevices.CurrentRowIndex != -1)
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                { 
                    if(listBox1.Items[i].ToString()==((DataTable)dgDevices.DataSource).Rows[dgDevices.CurrentRowIndex].ItemArray[1].ToString())
                    {
                        return;
                    }
                }
                listBox1.Items.Add(((DataTable)dgDevices.DataSource).Rows[dgDevices.CurrentRowIndex].ItemArray[1].ToString());
            }
        }

        private void picbac_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

     
    }
}
