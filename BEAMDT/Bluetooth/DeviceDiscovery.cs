using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BEAMDT.Bluetooth
{
    class DevicesDiscoveryWrapper
    {
        ////Declaring a C# method that is implemented in an unmanaged DLL. 
        ////The method DevicesDiscovery is declared with the
        ////static and extern modifiers and has the DllImport attribute 
        ////which tells the compiler that the implementation comes 
        ////from WMBluetoothWrapper.dll.
        
        // [DllImport("WMBluetoothWrapper.dll")]
        //private static extern System.Int32 DevicesDiscovery
        //    ([MarshalAs(UnmanagedType.LPWStr)] StringBuilder listDevices);
            
        //private static string devicesList;
        //private static int numberDevices;
        //// Used for the Singleton design pattern, 
        //// the DevicesDiscoveryWrapper object will be instantiated once 
        //// and can be accessed only by calling the GetDevicesDiscoveryWrapper method.
        //private static DevicesDiscoveryWrapper _wrapper = null;

        //// The constructor is made private for the Singleton design pattern
        //private DevicesDiscoveryWrapper()
        //{
        //    devicesList = null;
        //    numberDevices = 0;
        //}
        
        
        //// You can use this public and static method in order to retrieve the unique 
        //// DevicesDiscoveryWrapper object through which 
        //// you can run the devices discovery procedure
        //public static DevicesDiscoveryWrapper GetDevicesDiscoveryWrapper()
        //{
        //    if(_wrapper == null)
        //    {
        //        _wrapper = new DevicesDiscoveryWrapper();
        //    }
            
        //    return _wrapper;
        //}
        
        //// You can use this getter to retrieve the list of found devices 
        //// (Ids + Addresses) once you run the                  
        //// RunDevicesDiscovery method.
        //public string GetDevices
        //{
        //    get
        //    {
        //        lock (this)
        //        {
        //            return devicesList;
        //        }
        //    }
        //}
        
        //// You can use this getter to retrieve the number of found devices 
        //// once you run the RunDevicesDiscovery method.
        //public int GetNumberOfDevices
        //{
        //    get
        //    {
        //        lock (this)
        //        {
        //            return numberDevices;
        //        }
        //    }
        //}
        
        //// This is the main method which make a P/Invoke to DevicesDiscovery method.
        //// You can execute a thread periodically and 
        //// specify this method as a start point
        //// and in the same time you can access the number 
        //// and the list of retrieved devices
        //// using another thread via the other getters: GetNumberOfDevices and GetDevices
        //public void RunDevicesDiscovery()
        //{
        //    lock (this)
        //    {
        //        StringBuilder tmpListDevices = new StringBuilder();
        //        // Max capacity, to change if needed
        //        tmpListDevices.Capacity = 1024;
        //        int res = DevicesDiscovery(tmpListDevices);
        //        if( res == -1)
        //        {
        //                throw new Exception("Error occurred while calling the unmanaged DevicesDiscovery functions: " + tmpListDevices.ToString());
                
        //        }else
        //        {
        //            // All is ok            
        //            devicesList = tmpListDevices.ToString();
        //            numberDevices = res;
        //        }
        //    }
        //}
    }
}
