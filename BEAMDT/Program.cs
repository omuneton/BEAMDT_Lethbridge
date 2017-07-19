using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace BEAMDT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static int SoftwareVersion = 26;
        public static DateTime timestamp;
        public static bool FlagRecall = false;
        public static bool AlUpdate = false;
        public static bool FtUpdate = false;
        public static bool CfUpdate = false;
        public static bool HlUpdate = false;
        public static bool MlUpdate = false;
        public static bool PlUpdate = false;
        public static bool FbxUpdate = false;
        public static bool Sending = false;
        public static bool Refresh = false;
        public static bool FromClose = false;
        public static bool SyncingFarebox = false;
        public static int contTramas = 0;
        public static int contTramasBAD = 0;
        public static int LastSynching = 0;
        public static int AlCurrentVer = 0;
        public static int FtCurrentVer = 0;
        public static int CfCurrentVer = 0;
        public static int HlCurrentVer = 0;
        public static int PlCurrentVer = 0;
        public static int MlCurrentVer = 0;

        public static int AlFBXVer = 0;
        public static int FtAFBXVer = 0;
        public static int FtBFBXVer = 0;
        public static int CurFtFBXVer = 0;
        public static int CfFBXVer = 0;
        public static int HlFBXVer = 0;
        public static int PlFBXVer = 0;
        public static int MlFBXVer = 0;


        public static DateTime TimeStampOpenClose;
        public static bool CoinsDisabled = false;
        public static bool SetWiFiOn = false;
        public static bool SetWiFiOff = false;
        public static bool BotonEnabled = true;
        public static bool Cancel = false;
        public static int Synching = 0;
        public static bool GPSOK = false;
        public static bool NoGPS = false;
        public static bool RDRUpdate = false;
        public static bool Lock = false;
        public static bool SaleProcess = false;
        public static int MoneyTransaction = 0;
        public static bool IgnoreMessages = false;
        public static int BusNumber = 0;
        public static int VueltaAbierta=-1;
        public static int SacFileVersion = 0;
        public static int RDRVersion = 0;
        public static int ZonaSegura=0;
        public static int RouteNumber = 0;
        public static int SecsToSync = 900;
        public static int Run = 0;
        public static int Direction = 0;
        public static int DriverID = 0;
        public static int Validator = 0;
        public static int Offset = 0;
        public static int ClassesIndex = 0;
        public static int ProductIndex = 0;
        public static int TimerStatus = 5000;
        public static byte IDCommand = 0;
        public static double GeneralFare = 1.0;
        public static string LastButton = "";
        public static string RouteDesc = "";
        public static string CountDesc = "";
        public static string SaleDesc = "";
        public static string FareboxVersion = "0-0-0-0";
        public static string SaleDescription = "";
        public static string CoinByPass = "";
        private static bool initTaskBarState = false;
        public static bool buttontoggle = false;
        public static int Service = 0;
        public static int MenuOn = 0;
        public static bool PendingSync = false;
        public static bool btoggle = false;
        public static bool OpenShiftFromCard = false;
        public static bool ChangeRoute = false;
        public static bool ExecutingProcessClosed = false;
        public static bool ProcessDevices = false;
        public static bool UDPListen = true;
        public static bool DebugOn = false;
        public static Classes.clsVolumen.Volumes FareboxVolume=BEAMDT.Classes.clsVolumen.Volumes.NORMAL;
        public static Classes.clsVolumen.Volumes FareboxVolumeTemp = BEAMDT.Classes.clsVolumen.Volumes.NORMAL;
        [MTAThread]
        static void Main()
        {
            IntPtr taskBarHWnd = FindWindow("HHTaskBar", string.Empty);
            initTaskBarState = IsWindowVisible(taskBarHWnd);
            Classes.clsBrightness.init();
            Program.HideTaskbar();
            Cursor.Hide();
            Program.timestamp = DateTime.Now;
            
            Application.Run(new frmPrincipalAlt());

            Classes.clsBrightness.end();
            //if (initTaskBarState) ShowTaskbar();
        }
        [DllImport("coredll.dll")]
        public static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("coredll.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        private const int SW_HIDEWINDOW = 0x0000;
        private const int SW_SHOWNORMAL = 0x0001;

        [DllImport("coredll.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        public static void HideTaskbar()
        {
            IntPtr taskBarHWnd = FindWindow("HHTaskBar", string.Empty);
            ShowWindow(taskBarHWnd, SW_HIDEWINDOW);
        }

        public static void ShowTaskbar()
        {
            IntPtr taskBarHWnd = FindWindow("HHTaskBar", string.Empty);
            ShowWindow(taskBarHWnd, SW_SHOWNORMAL);
        }
    }
}