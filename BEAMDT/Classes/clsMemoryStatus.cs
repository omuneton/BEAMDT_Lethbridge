using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BEAMDT.Classes
{
    class clsMemoryStatus
    {
        [DllImport("coredll.dll")]
        public static extern void GlobalMemoryStatus(ref MEMORYSTATUS lpBuffer);

        public struct MEMORYSTATUS
        {
            public int dwLength;
            public int dwMemoryLoad;
            public int dwTotalPhys;
            public int dwAvailPhys;
            public int dwTotalPageFile;
            public int dwAvailPageFile;
            public int dwTotalVirtual;
            public int dwAvailVirtual;
        };

        const string CRLF = "\r\n";
        public static string GetStatus()
        {
            MEMORYSTATUS ms = new MEMORYSTATUS();
            ms.dwLength = Marshal.SizeOf(ms);
            GlobalMemoryStatus(ref ms);
            string strAppName = "Memory Status";
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append("Memory Load = ");
            sbMessage.Append(ms.dwMemoryLoad.ToString() + "%");
            sbMessage.Append(CRLF);
            sbMessage.Append("Total RAM = ");
            sbMessage.Append(ms.dwTotalPhys.ToString("#,##0"));
            sbMessage.Append(CRLF);
            sbMessage.Append("Avail RAM = ");
            sbMessage.Append(ms.dwAvailPhys.ToString("#,##0"));
            sbMessage.Append(CRLF);
            sbMessage.Append("Total Page = ");
            sbMessage.Append(ms.dwTotalPageFile.ToString("#,##0"));
            sbMessage.Append(CRLF);
            sbMessage.Append("Avail Page = ");
            sbMessage.Append(ms.dwAvailPageFile.ToString("#,##0"));
            sbMessage.Append(CRLF);
            sbMessage.Append("Total Virt = ");
            sbMessage.Append(ms.dwTotalVirtual.ToString("#,##0"));
            sbMessage.Append(CRLF);
            sbMessage.Append("Avail Virt = ");
            sbMessage.Append(ms.dwAvailVirtual.ToString("#,##0"));
            return sbMessage.ToString();
        }
    }
}
