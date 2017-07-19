using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace BEAMDT.Classes
{
    class clsUtils
    {
        [DllImport("CoreDll.dll", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int PlaySound(string szSound, IntPtr hMod, int flags);

        private static string soundLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\";

        private enum SND
        {
            SYNC = 0x0000,
            ASYNC = 0x0001,
            NODEFAULT = 0x0002,
            MEMORY = 0x0004,
            LOOP = 0x0008,
            NOSTOP = 0x0010,
            NOWAIT = 0x00002000,
            ALIAS = 0x00010000,
            ALIAS_ID = 0x00110000,
            FILENAME = 0x00020000,
            RESOURCE = 0x00040004
        }
        static Thread t;

        static string File;
        public static void Sonido(string filename)
        {
            File = filename;
            t = new Thread(new ThreadStart(Play));
            t.Start();
        }
        private static void Play()
        {
            PlaySound(File);
        }
        public static void PlaySound(string fileName)
        {
            PlaySound(soundLocation + fileName, IntPtr.Zero, (int)(SND.SYNC | SND.FILENAME));
        }
        [DllImport("coredll.dll", SetLastError = true)]
        private extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);

        [DllImport("coredll.dll", SetLastError = true)]
        private extern static uint SetLocalTime(ref SYSTEMTIME lpSystemTime);

        public struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }


        public static void SetTime(int Hours,int Minutes)
        {
            try
            {
                SYSTEMTIME st = new SYSTEMTIME();
                st.wYear = (ushort)DateTime.Now.Year;
                st.wMonth = (ushort)DateTime.Now.Month;
                st.wDay = (ushort)DateTime.Now.Day;
                st.wDayOfWeek = (ushort)DateTime.Now.DayOfWeek;
                st.wHour = (ushort)Hours;
                st.wMinute = (ushort)Minutes;
                st.wSecond = 10;
                st.wMilliseconds = (ushort)DateTime.Now.Millisecond;
                uint result = SetLocalTime(ref st);
                

            }
            catch (Exception ex)
            {
                //clsLog.Write("Time Server Error: " + ex.Message);
            }
        }
        public static void SetShiftBrightness(bool ShiftOpen)
        {
           /* int Valueback = 0;
            int ValueKeys = 0;
            if (ShiftOpen)
            {
                switch (clsConfig.BrightValue)
                {
                    case 1://low
                     
                        Valueback = 5;
                        ValueKeys = 5;
                        break;
                    case 2://medium
                       
                        Valueback = 15;
                        ValueKeys = 15;
                        break;
                    case 3://high
                       
                        Valueback = 50;
                        ValueKeys = 50;
                        break;
                }
                TreqBacklightDriver.SetKeypadIntensity(ValueKeys);
                TreqBacklightDriver.SetIntensity(Valueback);
            }
            else
            {
                Valueback = 2;
                ValueKeys = 2;
                TreqBacklightDriver.SetKeypadIntensity(ValueKeys);
                TreqBacklightDriver.SetIntensity(Valueback);
            }*/
        }
    }
}
