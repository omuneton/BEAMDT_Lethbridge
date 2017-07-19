using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BEAMDT.Classes
{
    public class clsVolumen
    {
        ///<summary>
        /// Specifies the volume levels.
        /// </summary>
        public enum Volumes : int
        {
            OFF = 0,
            LOW = 858993459,
            NORMAL = 1717986918,
            MEDIUM = -1717986919,
            HIGH = -858993460,
            VERY_HIGH = -1
        }

        
        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int waveOutSetVolume(IntPtr device, int volume);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int waveOutGetVolume(IntPtr device, ref int volume);
        public static Classes.clsVolumen.Volumes VolumeUp(Classes.clsVolumen.Volumes Value)
        {

            switch (Value)
            {
                case BEAMDT.Classes.clsVolumen.Volumes.OFF:
                    return BEAMDT.Classes.clsVolumen.Volumes.LOW;
                case BEAMDT.Classes.clsVolumen.Volumes.LOW:
                    return BEAMDT.Classes.clsVolumen.Volumes.NORMAL;
                case BEAMDT.Classes.clsVolumen.Volumes.MEDIUM:
                    return BEAMDT.Classes.clsVolumen.Volumes.HIGH;
                case BEAMDT.Classes.clsVolumen.Volumes.NORMAL:
                    return BEAMDT.Classes.clsVolumen.Volumes.MEDIUM;
                case BEAMDT.Classes.clsVolumen.Volumes.VERY_HIGH:
                    return BEAMDT.Classes.clsVolumen.Volumes.VERY_HIGH;
                case BEAMDT.Classes.clsVolumen.Volumes.HIGH:
                    return BEAMDT.Classes.clsVolumen.Volumes.VERY_HIGH;
                default:
                    return BEAMDT.Classes.clsVolumen.Volumes.NORMAL;
            }
        }
        public static Classes.clsVolumen.Volumes VolumeDown(Classes.clsVolumen.Volumes Value)
        {

            switch (Value)
            {
                case BEAMDT.Classes.clsVolumen.Volumes.OFF:
                    return BEAMDT.Classes.clsVolumen.Volumes.OFF;
                case BEAMDT.Classes.clsVolumen.Volumes.LOW:
                    return BEAMDT.Classes.clsVolumen.Volumes.OFF;
                case BEAMDT.Classes.clsVolumen.Volumes.MEDIUM:
                    return BEAMDT.Classes.clsVolumen.Volumes.NORMAL;
                case BEAMDT.Classes.clsVolumen.Volumes.NORMAL:
                    return BEAMDT.Classes.clsVolumen.Volumes.LOW;
                case BEAMDT.Classes.clsVolumen.Volumes.VERY_HIGH:
                    return BEAMDT.Classes.clsVolumen.Volumes.HIGH;
                case BEAMDT.Classes.clsVolumen.Volumes.HIGH:
                    return BEAMDT.Classes.clsVolumen.Volumes.MEDIUM;
                default:
                    return BEAMDT.Classes.clsVolumen.Volumes.NORMAL;
            }
        }
        public static string GetValue(Classes.clsVolumen.Volumes Value)
        {
            switch (Value)
            {
                case BEAMDT.Classes.clsVolumen.Volumes.OFF:
                    return "OFF";
                case BEAMDT.Classes.clsVolumen.Volumes.LOW:
                    return "LOW";
                case BEAMDT.Classes.clsVolumen.Volumes.MEDIUM:
                    return "MEDIUM";
                case BEAMDT.Classes.clsVolumen.Volumes.NORMAL:
                    return "NORMAL";
                case BEAMDT.Classes.clsVolumen.Volumes.VERY_HIGH:
                    return "VERY_HIGH";
                case BEAMDT.Classes.clsVolumen.Volumes.HIGH:
                    return "HIGH";
                default:
                    return "OFF";
            }
        }
        public static int GetValueCommand(Classes.clsVolumen.Volumes Value)
        {
            switch (Value)
            {
                case BEAMDT.Classes.clsVolumen.Volumes.OFF:
                    return 0;
                case BEAMDT.Classes.clsVolumen.Volumes.LOW:
                    return 1;
                case BEAMDT.Classes.clsVolumen.Volumes.NORMAL:
                    return 2;
                case BEAMDT.Classes.clsVolumen.Volumes.MEDIUM:
                    return 3;
                case BEAMDT.Classes.clsVolumen.Volumes.VERY_HIGH:
                    return 4;
                case BEAMDT.Classes.clsVolumen.Volumes.HIGH:
                    return 5;
                default:
                    return 0;
            }
        }
        public static Classes.clsVolumen.Volumes GetVolumeFromCommandValue(int Value)
        {
            switch (Value)
            {
                case 0:
                    return BEAMDT.Classes.clsVolumen.Volumes.OFF;
                case 1:
                    return BEAMDT.Classes.clsVolumen.Volumes.LOW;
                case 2:
                    return BEAMDT.Classes.clsVolumen.Volumes.NORMAL;
                case 3:
                    return BEAMDT.Classes.clsVolumen.Volumes.MEDIUM;
                case 4:
                    return BEAMDT.Classes.clsVolumen.Volumes.VERY_HIGH;
                case 5:
                    return BEAMDT.Classes.clsVolumen.Volumes.HIGH;
                default:
                    return 0;
            }
        }
        public static Volumes Volume
        {
            get
            {
                int v = (int)0;

                waveOutGetVolume(IntPtr.Zero, ref v);

                switch (v)
                {
                    case (int)Volumes.OFF:
                        return Volumes.OFF;

                    case (int)Volumes.LOW:
                        return Volumes.LOW;

                    case (int)Volumes.NORMAL:
                        return Volumes.NORMAL;

                    case (int)Volumes.MEDIUM:
                        return Volumes.MEDIUM;

                    case (int)Volumes.HIGH:
                        return Volumes.HIGH;

                    case (int)Volumes.VERY_HIGH:
                        return Volumes.VERY_HIGH;

                    default:
                        return Volumes.OFF;
                }
            }
            set
            {
                waveOutSetVolume(IntPtr.Zero, (int)value);
            }
        }
        
    }
}
