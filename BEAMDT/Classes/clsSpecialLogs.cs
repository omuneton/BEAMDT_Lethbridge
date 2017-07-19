using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BEAMDT.Classes
{
    class clsSpecialLogs
    {
        public static string Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Logs";
        public static void WriteLog(string App, string Message)
        {
            try
            {
                if (!System.IO.Directory.Exists(Path))
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
                string WritePath = Path + "\\Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                StreamWriter sw = new StreamWriter(WritePath, true);
                sw.WriteLine(App + " -- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss : ") + Message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
