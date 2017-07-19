using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BEAMDT.Screens.Resultados
{
    public partial class ctrResultadoAdjust : UserControl
    {
        public enum AdjustType
        {
            Brightness,
            Volume
        }
        public enum Device
        {
            MDT,
            Farebox
        }
        public frmPrincipalAlt Prin;
        public AdjustType Type = AdjustType.Brightness;
        public Device CurrentDevice = Device.MDT;
        public ctrResultadoAdjust()
        {
            InitializeComponent();
        }
        Classes.clsBrightness.Brightness OriginalValue;
        Classes.clsVolumen.Volumes OriginalVolume;
        public void Inicio()
        {
            if (Type == AdjustType.Brightness)
            {
                lblPregunta.Text = "Console Brightness:";
                OriginalValue = Classes.clsConfig.Brightness;
                lblValor.Text = GetValue(Classes.clsConfig.Brightness);
            }
            else
            {
                
                if (CurrentDevice == Device.MDT)
                {
                    lblPregunta.Text = "Console Volume:";
                    OriginalVolume = Classes.clsConfig.Volume;
                    lblValor.Text = Classes.clsVolumen.GetValue(Classes.clsConfig.Volume);
                }
                else
                {
                    lblPregunta.Text = "Farebox Volume:";
                    Program.FareboxVolumeTemp = Program.FareboxVolume;
                    lblValor.Text = Classes.clsVolumen.GetValue(Program.FareboxVolume);
                }
                
            }
        }
        private void picdown_Click(object sender, EventArgs e)
        {
            if (Type == AdjustType.Brightness)
            {
                Classes.clsConfig.Brightness = ValueDown(Classes.clsConfig.Brightness);
                Classes.clsBrightness.setBackLight(Classes.clsConfig.Brightness);
                lblValor.Text = GetValue(Classes.clsConfig.Brightness);
            }
            else
            {
                if (CurrentDevice == Device.MDT)
                {
                    Classes.clsConfig.Volume = Classes.clsVolumen.VolumeDown(Classes.clsConfig.Volume);
                    Classes.clsVolumen.Volume = Classes.clsConfig.Volume;
                    System.Media.SystemSounds.Beep.Play();
                    lblValor.Text = Classes.clsVolumen.GetValue(Classes.clsConfig.Volume);
                }
                else
                {
                    Program.FareboxVolumeTemp = Classes.clsVolumen.VolumeDown(Program.FareboxVolumeTemp);
                    lblValor.Text = Classes.clsVolumen.GetValue(Program.FareboxVolumeTemp);
                }
                
            }
        }

        private void picUp_Click(object sender, EventArgs e)
        {
            if (Type == AdjustType.Brightness)
            {
                Classes.clsConfig.Brightness = ValueUp(Classes.clsConfig.Brightness);
                Classes.clsBrightness.setBackLight(Classes.clsConfig.Brightness);
                lblValor.Text = GetValue(Classes.clsConfig.Brightness);
            }
            else
            {
                if (CurrentDevice == Device.MDT)
                {
                    Classes.clsConfig.Volume = Classes.clsVolumen.VolumeUp(Classes.clsConfig.Volume);
                    Classes.clsVolumen.Volume = Classes.clsConfig.Volume;
                    System.Media.SystemSounds.Beep.Play();
                    lblValor.Text = Classes.clsVolumen.GetValue(Classes.clsConfig.Volume);
                }
                else
                { 
                    Program.FareboxVolumeTemp = Classes.clsVolumen.VolumeUp(Program.FareboxVolumeTemp);
                    lblValor.Text = Classes.clsVolumen.GetValue(Program.FareboxVolumeTemp);
                }
            }
        }
        private string GetValue(Classes.clsBrightness.Brightness Value)
        {
            switch (Value)
            {
                case Classes.clsBrightness.Brightness.Low:
                    return "LOW";
                case Classes.clsBrightness.Brightness.Medium:
                    return "MEDIUM";
                case Classes.clsBrightness.Brightness.MediumLow:
                    return "MEDIUM LOW";
                case Classes.clsBrightness.Brightness.MediumHigh:
                    return "MEDIUM HIGH";
                case Classes.clsBrightness.Brightness.High:
                    return "HIGH";
                default:
                    return "LOW";
            }
        }
        private Classes.clsBrightness.Brightness ValueDown(Classes.clsBrightness.Brightness Value)
        {
            switch (Value)
            {
                case Classes.clsBrightness.Brightness.Low:
                    return Classes.clsBrightness.Brightness.Low;
                case Classes.clsBrightness.Brightness.Medium:
                    return Classes.clsBrightness.Brightness.MediumLow;
                case Classes.clsBrightness.Brightness.MediumLow:
                    return Classes.clsBrightness.Brightness.Low;
                case Classes.clsBrightness.Brightness.MediumHigh:
                    return Classes.clsBrightness.Brightness.Medium;
                case Classes.clsBrightness.Brightness.High:
                    return Classes.clsBrightness.Brightness.MediumHigh;
                default:
                    return Classes.clsBrightness.Brightness.High;
            }
        }

        private Classes.clsBrightness.Brightness ValueUp(Classes.clsBrightness.Brightness Value)
        {
            switch (Value)
            {
                case Classes.clsBrightness.Brightness.Low:
                    return Classes.clsBrightness.Brightness.MediumLow;
                case Classes.clsBrightness.Brightness.Medium:
                    return Classes.clsBrightness.Brightness.MediumHigh;
                case Classes.clsBrightness.Brightness.MediumLow:
                    return Classes.clsBrightness.Brightness.Medium;
                case Classes.clsBrightness.Brightness.MediumHigh:
                    return Classes.clsBrightness.Brightness.High;
                case Classes.clsBrightness.Brightness.High:
                    return Classes.clsBrightness.Brightness.High;
                default:
                    return Classes.clsBrightness.Brightness.High;
            }
        }
       
    }
}
