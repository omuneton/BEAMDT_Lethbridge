using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
//using InTheHand;
//using InTheHand.Net.Sockets;
using System.Runtime.InteropServices;

namespace BEAMDT.Bluetooth
{
    public class BluetoothCliente
    {
        //bool Completed = true;
        //InTheHand.Net.Sockets.BluetoothClient Client = new InTheHand.Net.Sockets.BluetoothClient();
        //public void StartBluetooth()
        //{
        //    if (Completed)
        //    {
        //        Completed = false;
        //        Client.BeginDiscoverDevices(255, true, true, true, false, new AsyncCallback(DiscoverDevicesCallback), Client);
        //    }
            
        //}
        //void DiscoverDevicesCallback(IAsyncResult result)
        //{
        //    //Get back the BluetoothClient which was passed as the state parameter in BeginDiscoverDevices
        //    BluetoothClient thisDevice = result.AsyncState as BluetoothClient;
        //    //Check if the scanning is completed
        //    if (result.IsCompleted)
        //    {
        //        Completed = true;
        //        //Get the list of obtained devices and end the discovery process
        //        BluetoothDeviceInfo[] devices = thisDevice.EndDiscoverDevices(result);
        //        Program.ZonaSegura = 0;
        //        foreach (BluetoothDeviceInfo d in devices)
        //        {
        //            //System.Windows.Forms.MessageBox.Show(d.DeviceName);
        //            if (d.DeviceAddress.ToString() == Classes.clsConfig.BluetoothMAC)
        //            {
        //                Program.ZonaSegura=1;
        //                break;
        //            }
        //        }
        //    }
        //}
        ////public bool ZonaSegura(string deviceAddress)
        ////{
        ////    //GetDiscoverableDevices();
        ////    InTheHand.Net.Sockets.BluetoothClient Client = new InTheHand.Net.Sockets.BluetoothClient();
        ////    BluetoothDeviceInfo[] devices = Client.DiscoverDevicesInRange();
        ////    foreach (BluetoothDeviceInfo d in devices)
        ////    {
        ////        //System.Windows.Forms.MessageBox.Show(d.DeviceName);
        ////        if (d.DeviceAddress.ToString() == deviceAddress)
        ////        {
        ////            return true;
        ////        }
        ////    }
        ////    return false;
        ////}
        //public BluetoothDeviceInfo[] GetDevices()
        //{
        //    return Client.DiscoverDevices();
        //    //return Client.BeginDiscoverDevices();
        //}
        ////public string Mensaje="Inicio";
        ////public string Dev="";
        ////public void DeviceDetected(object state,InTheHand.Net.Bluetooth.DiscoverDevicesEventArgs e)
        ////{ 
        ////    foreach (BluetoothDeviceInfo d in e.Devices)
        ////    {
        ////        Dev = d.DeviceName;
        ////        //System.Windows.Forms.MessageBox.Show(d.DeviceName);
        ////        if (d.DeviceName == DeviceBuscar)
        ////        {
        ////            Mensaje = "Estoy en zona segura";
        ////            return;
        ////        }
        ////    }
        ////}
        ////public void DeviceDetectedEnd(object state, InTheHand.Net.Bluetooth.DiscoverDevicesEventArgs e)
        ////{
        ////    Mensaje = "Termine";
        ////    Comp.DiscoverDevicesProgress -= DeviceDetected;
        ////    Comp.DiscoverDevicesComplete -= DeviceDetectedEnd;
        ////    Comp = null;
            
        ////}
        ////InTheHand.Net.Bluetooth.BluetoothComponent Comp;
        ////public bool StartScan(string deviceName)
        ////{
        ////    DeviceBuscar = deviceName;
        ////    Comp = new InTheHand.Net.Bluetooth.BluetoothComponent();
        ////    Comp.DiscoverDevicesProgress += new EventHandler<InTheHand.Net.Bluetooth.DiscoverDevicesEventArgs>(DeviceDetected);
        ////    Comp.DiscoverDevicesComplete += new EventHandler<InTheHand.Net.Bluetooth.DiscoverDevicesEventArgs>(DeviceDetectedEnd);
        ////    Comp.DiscoverDevicesAsync(255, false, false, false, true, null);
            
        ////    return false;
        ////}
    }
}
