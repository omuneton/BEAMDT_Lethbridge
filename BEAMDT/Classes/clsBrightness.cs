using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Micronet.Ce500.Sdk.Backlight;

namespace BEAMDT.Classes
{
    class clsBrightness
    {
        public enum Brightness : int
        { 
            Low = 100,
            MediumLow = 75,
            Medium = 50,
            MediumHigh = 25,
            High = 0
        }

        #region P-Invoke
        public class SYSTEM_POWER_STATUS_EX2
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public int BatteryLifeTime;
            public int BatteryFullLifeTime;
            public byte Reserved2;
            public byte BackupBatteryFlag;
            public byte BackupBatteryLifePercent;
            public byte Reserved3;
            public int BackupBatteryLifeTime;
            public int BackupBatteryFullLifeTime;
            public int BatteryVoltage;
            public int BatteryCurrent;
            public int BatteryAverageCurrent;
            public int BatteryAverageInterval;
            public int BatterymAHourConsumed;
            public int BatteryTemperature;
            public int BackupBatteryVoltage;
            public byte BatteryChemistry;
        }

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U4)]
        private static extern int GetSystemPowerStatusEx2(SYSTEM_POWER_STATUS_EX2 systemPowerStatusEx2, [MarshalAs(UnmanagedType.U4), In] int len, [MarshalAs(UnmanagedType.Bool), In] bool update);

        #endregion

        /// <summary>
        /// Controls display's backlight
        /// </summary>
        private static DeviceBacklight.Display display = new DeviceBacklight.Display();

        /// <summary>
        /// Contrlols keypad backlight
        /// </summary>
        private static DeviceBacklight.Keypad keypad = new DeviceBacklight.Keypad();

        /// <summary>
        /// Original values of backlight settings
        /// </summary>
        private static bool originalKeypadLightSensorDependency = false;
        private static bool originalDisplayLightSensorDependency = false;

        /// <summary>
        /// HIGH(current) display backlight value
        /// </summary>
        private static int originalDisplayACHigh;

        /// <summary>
        /// MEDIUM display backlight value in case of medium room light.
        /// (Usabale only in case of lightsensor dependency mode)
        /// </summary>
        private static int originalDisplayACMedium;

        /// <summary>
        /// LOW display backlight value in case of low light.
        /// (Usabale only in case of lightsensor dependency mode)
        /// </summary>
        private static int originalDisplayACLow;

        /// <summary>
        /// HIGH(current) display backlight value
        /// </summary>
        private static int originalDisplayBatteryHigh;

        /// <summary>
        /// MEDIUM display backlight value in case of medium room light.
        /// (Usabale only in case of lightsensor dependency mode)
        /// </summary>
        private static int originalDisplayBatteryMedium;

        /// <summary>
        /// LOW display backlight value in case of low room light.
        /// (Usabale only in case of lightsensor dependency mode)
        /// </summary>
        private static int originalDisplayBatteryLow;

        /// <summary>
        /// Current backlight value of keypad
        /// </summary>
        private static int originalKeypadHigh;

        /// <summary>
        /// Keypad's backlight value in case of medium room light.
        /// (Usabale only in case of lightsensor dependency mode)
        /// </summary>
        private static int originalKeypadMedium;

        /// <summary>
        /// Keypad's backlight value in case of low room light.
        /// (Usabale only in case of lightsensor dependency mode)
        /// </summary>
        private static int originalKeypadLow;

        /// <summary>
        /// Power status of MDT device
        /// </summary>
        private static SYSTEM_POWER_STATUS_EX2 systemPowerStatusEx2 = new SYSTEM_POWER_STATUS_EX2();
        public static void setBackLight(Brightness Value)
        {
            // Retrieve display backlight settings according to current power status
            if (systemPowerStatusEx2.ACLineStatus == 1)
            {
                display.AC.LightSensor.High = (int)Value;
            }
            else
            {
                display.Battery.LightSensor.High = (int)Value;
            }
            
        }
        public static Brightness getBackLight()
        {
            return 0;
        }
        public static void init()
        {
            

            // Retrieve keypad backlight settings
            //trackBarKbd.Value = 100 - keypad.LightSensor.High;


            // Deternine current power status
            GetSystemPowerStatusEx2(systemPowerStatusEx2, Marshal.SizeOf(systemPowerStatusEx2), true);

            //// Retrieve display backlight settings according to current power status
            //if (systemPowerStatusEx2.ACLineStatus == 1)
            //    trackBarScreen.Value = 100 - display.AC.LightSensor.High;
            //else
            //    trackBarScreen.Value = 100 - display.Battery.LightSensor.High;

            // Store oroginal backlight settings
            originalDisplayLightSensorDependency = display.LightSensorDependant;
            originalKeypadLightSensorDependency = keypad.LightSensorDependant;

            // Cancel the dependency for the sample
            display.LightSensorDependant = false;
            keypad.LightSensorDependant = false;

            originalDisplayACHigh = display.AC.LightSensor.High;
            originalDisplayACMedium = display.AC.LightSensor.Medium;
            originalDisplayACLow = display.AC.LightSensor.Low;

            originalDisplayBatteryHigh = display.Battery.LightSensor.High;
            originalDisplayBatteryMedium = display.Battery.LightSensor.Medium;
            originalDisplayBatteryLow = display.Battery.LightSensor.Low;

            originalKeypadHigh = keypad.LightSensor.High;
            originalKeypadMedium = keypad.LightSensor.Medium;
            originalKeypadLow = keypad.LightSensor.Low;
        }
        public static void end()
        {
            // Restore oroginal settings
            display.LightSensorDependant = originalDisplayLightSensorDependency;
            keypad.LightSensorDependant = originalKeypadLightSensorDependency;

            display.AC.LightSensor.High = originalDisplayACHigh;
            display.AC.LightSensor.Medium = originalDisplayACMedium;
            display.AC.LightSensor.Low = originalDisplayACLow;

            display.Battery.LightSensor.High = originalDisplayBatteryHigh;
            display.Battery.LightSensor.Medium = originalDisplayBatteryMedium;
            display.Battery.LightSensor.Low = originalDisplayBatteryLow;

            keypad.LightSensor.High = originalKeypadHigh;
            keypad.LightSensor.Medium = originalKeypadMedium;
            keypad.LightSensor.Low = originalKeypadLow;
        }
    }
}
