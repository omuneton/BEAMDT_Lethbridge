using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using OpenNETCF;
using OpenNETCF.Net;

namespace BEAMDT.Classes
{
    class clsNetworkStatus
    {
        public static bool ResetWifi = false;
        public struct AccessPointInfo
        {
            public string Name;
            public string MAC;
        }
        public static List<AccessPointInfo> GetAccessPoints()
        {
            List<AccessPointInfo> APInformation = new List<AccessPointInfo>();
            try
            {
                OpenNETCF.Net.NetworkInformation.WirelessZeroConfigNetworkInterface INw;

                APInformation.Clear();
                foreach (OpenNETCF.Net.NetworkInformation.INetworkInterface ni in OpenNETCF.Net.NetworkInformation.WirelessNetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni is OpenNETCF.Net.NetworkInformation.WirelessZeroConfigNetworkInterface)
                    {  // wireless zero config.
                        INw = (OpenNETCF.Net.NetworkInformation.WirelessZeroConfigNetworkInterface)ni;
                        OpenNETCF.Net.NetworkInformation.AccessPointCollection APs = INw.NearbyAccessPoints;
                        foreach (OpenNETCF.Net.NetworkInformation.AccessPoint ap in APs)
                        {
                            AccessPointInfo APInfo = new AccessPointInfo();
                            APInfo.Name = ap.Name;
                            APInfo.MAC = ap.PhysicalAddress.ToString();
                            APInformation.Add(APInfo);
                        }
                        break;
                    }
                }
                
            }
            catch
            { 
            
            }
            return APInformation;
            
        }
        public static void WifiOff()
        {
            uint status = 0;
            Micronet.Ce500.DotNetApi.Wireless.MIC_WirelessModuleGetPowerStatus(out status);
            if (status == 1)
            {
                Micronet.Ce500.DotNetApi.Wireless.MIC_WirelessModulePower(0, 1);
            }
            
        }
        public static void WifiOn()
        {
            uint status = 0;
            Micronet.Ce500.DotNetApi.Wireless.MIC_WirelessModuleGetPowerStatus(out status);
            if (status == 0)
            {
                Micronet.Ce500.DotNetApi.Wireless.MIC_WirelessModulePower(1, 1);
            }
        }
        public static void WifiReset()
        {
            Micronet.Ce500.DotNetApi.Wireless.MIC_WirelessModulePower(0, 1);
            System.Threading.Thread.Sleep(2000);
            Micronet.Ce500.DotNetApi.Wireless.MIC_WirelessModulePower(1, 1);
        }
        public static void CheckSafeZone()
        {
            try
            {
                Program.ZonaSegura = 0;
                OpenNETCF.Net.NetworkInformation.WirelessZeroConfigNetworkInterface INw;

                foreach (OpenNETCF.Net.NetworkInformation.INetworkInterface ni in OpenNETCF.Net.NetworkInformation.WirelessNetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni is OpenNETCF.Net.NetworkInformation.WirelessZeroConfigNetworkInterface)
                    {  // wireless zero config.
                        INw = (OpenNETCF.Net.NetworkInformation.WirelessZeroConfigNetworkInterface)ni;
                        OpenNETCF.Net.NetworkInformation.AccessPointCollection APs = INw.NearbyAccessPoints;
                        foreach (OpenNETCF.Net.NetworkInformation.AccessPoint ap in APs)
                        {
                            if (clsConfig.ListAPMACs.Contains(ap.PhysicalAddress.ToString()))
                            {
                                if (ResetWifi)
                                {
                                    ResetWifi = false;
                                    WifiReset();
                                }
                                Program.ZonaSegura = 1;
                                return;
                            }
                        }
                        break;
                    }
                }

            }
            catch
            {
               
                Program.ZonaSegura = 0;
            }
            if (ResetWifi)
            {
                ResetWifi = false;
                WifiReset();
            }
        }
        //public static int IsConnected()
        //{
            
        //    bool ret = false;
        //    try
        //    {
        //        // Returns the Device Name
        //        string HostName = Dns.GetHostName();
        //        IPHostEntry thisHost = Dns.GetHostEntry(HostName);
        //        string thisIpAddr = thisHost.AddressList[0].Address.ToString();

        //        if (thisIpAddr != IPAddress.Parse("127.0.0.1").ToString())
        //        {
        //            return 1;
        //        }
        //        else 
        //        {
        //            return 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return -1;
        //    }   
        //}
        //public static void IsSecureZone()
        //{
        //    Program.ZonaSegura = 0;
        //    try
        //    {
        //        var adapters = OpenNETCF.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
        //        foreach (OpenNETCF.Net.NetworkInformation.INetworkInterface nic in adapters)
        //        {
        //            if (nic is OpenNETCF.Net.NetworkInformation.WirelessNetworkInterface)
        //            {
        //                OpenNETCF.Net.NetworkInformation.WirelessNetworkInterface WI = (OpenNETCF.Net.NetworkInformation.WirelessNetworkInterface)nic;
        //                string adapterName = WI.Name;
        //                string ssidName = WI.AssociatedAccessPoint;
        //                string signalStrength = WI.SignalStrength.ToString();
        //                if (!string.IsNullOrEmpty(ssidName) && clsConfig.ListAPMACs.Contains(nic.PhysicalAddress.ToString()))
        //                {
        //                    Program.ZonaSegura = 1;
        //                    return;
        //                }
        //            }

        //    }
        //    catch
        //    {
        //        Program.ZonaSegura = 0;
        //    }
        //}

        public static string IsConnected()
        {
            try
            {
                Program.ZonaSegura = 0;
                var adapters = OpenNETCF.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                foreach (OpenNETCF.Net.NetworkInformation.INetworkInterface nic in adapters)
                {
                    if (nic is OpenNETCF.Net.NetworkInformation.WirelessNetworkInterface)
                    {
                        OpenNETCF.Net.NetworkInformation.WirelessNetworkInterface WI = (OpenNETCF.Net.NetworkInformation.WirelessNetworkInterface)nic;
                        string adapterName = WI.Name;
                        string ssidName = WI.AssociatedAccessPoint;
                        string signalStrength = WI.SignalStrength.ToString();
                        if (string.IsNullOrEmpty(ssidName))
                        {
                            //if (ResetWifi)
                            //{
                            //    ResetWifi = false;
                            //    WifiReset();
                            //}
                            return "";
                        }
                        else
                        {
                            //if (ResetWifi)
                            //{
                            //    ResetWifi = false;
                            //    WifiReset();
                            //}
                            //if (clsConfig.ListAPMACs.Contains(WI.AssociatedAccessPointMAC.ToString()))
                            //{
                            //    Program.ZonaSegura = 1;
                            //}
                            return ssidName + ": Connected";
                        }
                    }

                }
                //try
                //{
                //    String HostName = System.Net.Dns.GetHostName();
                //    IPHostEntry thisHost = System.Net.Dns.GetHostByName(HostName);
                //    string thisIpAddr = thisHost.AddressList[0].ToString();
                //    if (thisIpAddr != System.Net.IPAddress.Parse("127.0.0.1").ToString())
                //    {
                //        return "Connected";
                //    }
                //}
                //catch (Exception ex)
                //{
                //    return "";
                //}
                return "";
            }
            catch 
            {
                Program.ZonaSegura = 0;
                return "";
            }
        }
    }
}
