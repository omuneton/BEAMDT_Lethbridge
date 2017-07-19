using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BEAMDT.Screens
{
    public partial class ConsoleBrightness : UserControl
    {
        public ConsoleBrightness()
        {
            InitializeComponent();
        }
        Classes.clsBrightness.Brightness OriginalValue;
        public void Init()
        {
            OriginalValue = Classes.clsConfig.Brightness;
            lblValor.Text = GetValue(Classes.clsConfig.Brightness);
        }


        private void lbl6Izq_Click(object sender, EventArgs e)
        {
            Classes.clsConfig.Brightness = OriginalValue;
            Classes.clsBrightness.setBackLight(OriginalValue);
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowProgramConsole();
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
        private void picUp_Click(object sender, EventArgs e)
        {
            Classes.clsConfig.Brightness = ValueUp(Classes.clsConfig.Brightness);
            Classes.clsBrightness.setBackLight(Classes.clsConfig.Brightness);
            lblValor.Text = GetValue(Classes.clsConfig.Brightness);
        }

        private void picdown_Click(object sender, EventArgs e)
        {
            Classes.clsConfig.Brightness = ValueDown(Classes.clsConfig.Brightness);
            Classes.clsBrightness.setBackLight(Classes.clsConfig.Brightness);
            lblValor.Text = GetValue(Classes.clsConfig.Brightness);
        }

        private void lbl3Der_Click(object sender, EventArgs e)
        {
            frmPrincipalAlt prin = (frmPrincipalAlt)this.Parent;
            prin.ShowProgramConsole();
            Classes.clsConfig.SaveConfig();
        }
    }
}
