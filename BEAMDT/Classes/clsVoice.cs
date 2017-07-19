using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BEAMDT.Classes
{
    class clsVoice
    {
        [DllImport("VoiceWrapper.dll", CharSet = CharSet.Auto)]
        public static extern int Init([MarshalAs(UnmanagedType.LPWStr)] string VoicePath);
        [DllImport("VoiceWrapper.dll")]
        public static extern int PlayVoice([MarshalAs(UnmanagedType.LPWStr)] string Message);
        [DllImport("VoiceWrapper.dll")]
        public static extern int ChangeLanguage(int index);
        [DllImport("VoiceWrapper.dll")]
        public static extern int End();

    }
}
