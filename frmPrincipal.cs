using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using BEAMDT.Classes;
using System.IO;

namespace BEAMDT
{
    public partial class frmPrincipal : Form
    {
        public enum returnscreens
        {
            MainScreen, Password, Shift1, Shift2, Shift3, Shift4, MessageEsp1, MessageEsp2, Settings1, Settings2, Settings3, Brightness, Question, QuestionClose, QuestionEnaDis, SetValue
        }
        public enum ComandosFareboxConsola
        { 
            ResetConsola=1,
            EnviaFechaHora=2,
            SolicitudInicioArchivo=3,
            RecepcionExitosaDeArchivo=4,
            ListoEnviarArchivo=5,
            AbrirTurno=6,
            CerrarTurno=7,
            EnviaMensaje=8,
            SolicitaDatosGPS=9,
            EnvioDatosTurno=10
        }
        public enum ComandosConsolaFarebox
        {
            AbrirTurno=1,
            CerrarTurno=2,
            CambioDireccion=3,
            ResetFarebox=4,
            AvisoEnvioArchivo=5,
            EnvioArchivo=6,
            SolicitudEnvioArchivo=7,
            RecepcionExitosaDeArchivo=8,
            BusquedaFarebox=9,
            SolicitaDatosTurno=10,
            ActivaMensajes=11,
            DesactivaMensajes=12,
            ImprimeTransfer=13,
            VentaPase=14,
            OverrideBillete=15,
            ByPassMoneda=16,
            Count=17,
            Override=18,
            Cancel=19,
            Dump=20,
            ChangeCard=21,
            ProgramaNumBus=22,
            ProgramaRuta=23,
            ProgramaFechaHora=24,
            ProgramarOffset=25,
            EnviaDatosGPS=26,
            ClaseMoneda=27,
            ZonaSegura=28
        }
        public bool BotonIzq1 = true;
        public bool BotonIzq2 = true;
        public bool BotonIzq3 = true;
        public bool BotonIzq4 = true;
        public bool BotonIzq5 = true;
        public bool BotonIzq6 = true;

        public bool BotonDer1 = true;
        public bool BotonDer2 = true;
        public bool BotonDer3 = true;
        public bool BotonDer4 = true;
        public bool BotonDer5 = true;
        public bool BotonDer6 = true;

        public bool flagRecall = false;
        public Classes.clsRDR oRDR = new BEAMDT.Classes.clsRDR();
        public Screens.SplashScreen Splash = new BEAMDT.Screens.SplashScreen();
        public Screens.Password Password = new BEAMDT.Screens.Password();
        public Screens.Message Message = new BEAMDT.Screens.Message();
       
        public Screens.Settings1 Settings1 = new BEAMDT.Screens.Settings1();
        public Screens.Settings2 Settings2 = new BEAMDT.Screens.Settings2();
        public Screens.Settings3 Settings3 = new BEAMDT.Screens.Settings3();
        public Screens.Close ClosedScreen = new BEAMDT.Screens.Close();
        public Screens.QuestionEnaDis QuestionEnaDis = new BEAMDT.Screens.QuestionEnaDis();
        public Screens.SetTimeFormat SetTimeFormat = new BEAMDT.Screens.SetTimeFormat();
        public Screens.SetAutoClose SetAutoClose = new BEAMDT.Screens.SetAutoClose();
        public Screens.Principal MainPrin = new BEAMDT.Screens.Principal();
        public Screens.TestScreen TestScreen = new BEAMDT.Screens.TestScreen();
        public bool shiftOpen = false;
        System.Threading.Timer timerwait;
        bool waitflag = false;
        public frmPrincipal()
        {
            InitializeComponent();
        }
       
        

        private delegate void deleshowmainnew();
        //public void ShowMainScreenNew()
        //{
        //    //if (this.InvokeRequired)
        //    //{
        //    //    this.BeginInvoke(new deleshowmainnew(ShowMainScreenNew));
        //    //}
        //    //else
        //    //{
                
        //    //    //MainScreen.setText(MainScreen.lblRuta, "Route: " + BEAMDT.Program.RouteNumber.ToString());
        //    //    //if (BEAMDT.Program.Validator == 0)
        //    //    //{
        //    //    //    MainScreen.picUnlink.Visible = true;
        //    //    //    MainScreen.picLinked.Visible = false;
        //    //    //}
        //    //    //else
        //    //    //{
        //    //    //    MainScreenNew.picUnlink.Visible = false;
        //    //    //    MainScreenNew.picLinked.Visible = true;
        //    //    //}
        //    //    if (BEAMDT.Program.VueltaAbierta == 0)
        //    //    {
        //    //        //MainScreenNew.picOpen.Visible = true;
        //    //        //MainScreenNew.picClose.Visible = false;
        //    //    }
        //    //    else
        //    //    {
        //    //        //MainScreenNew.picOpen.Visible = false;
        //    //        //MainScreenNew.picClose.Visible = true;
        //    //    }
        //    //    if (!BuscaControl("MainScreenNew"))
        //    //    {

        //    //        //MainScreenNew.setVisible(MainScreenNew.lblClase, false);
        //    //        //MainScreenNew.setVisible(MainScreenNew.lblMensaje_1, false);
        //    //        //MainScreenNew.setVisible(MainScreenNew.lblMensaje_2, false);
        //    //        //MainScreenNew.startpuntos();
        //    //        this.Controls.Clear();
        //    //        this.Controls.Add(MainScreenNew);
        //    //    }
        //    //}

        //}
       
        public struct ScreenButton
        {
            public int Index;
            public string Label;
            public bool Enabled;
            public string BackColor;
            public string TextColor;
            public int Count;
        }
        public List<ScreenButton> screenbuttons = new List<ScreenButton>();
        private delegate void deleshowmess();
        public void ShowMessage()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new deleshowmess(ShowMessage));
            }
            else
            {
                this.Controls.Clear();
                this.Controls.Add(Message);
            }
        }
        private delegate void deleshowmessesp(int messtype);
        public void ShowMessageEsp(int messtype)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new deleshowmessesp(ShowMessageEsp), new object[] { messtype });
            }
            else
            {
                this.Controls.Clear();
                Message.SetMessage("", 1, 1);
                Message.SetMessage("", 1, 2);
                Message.SetMessage("", 1, 3);
                Message.SetMessage("", 1, 4);
                switch (messtype)
                { 
                    case 0://Linking Farebox
                        Message.SetMessage("Linking", 2, 2);
                        Message.SetMessage("Farebox", 2, 3);
                        break;
                    case 1://Waiting...
                        Message.SetMessage("Waiting...", 2, 2);
                        break;
                    case 2://Waiting...
                        Message.SetMessage("Welcome", 2, 2);
                        Message.SetMessage("Aboard", 2, 3);
                        break;
                    case 3://Waiting...
                        Message.SetMessage("Closed", 2, 2);
                        break;
                }
                this.Controls.Add(Message);
            }
        }

        private delegate void deleshowclosed(int messtype);
        public void ShowClosed(int messtype)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new deleshowclosed(ShowClosed), new object[] { messtype });
            }
            else
            {
                this.Controls.Clear();
                //Closed.SetMessage("", 1, 1);
                //Closed.SetMessage("", 1, 2);
                //Closed.SetMessage("", 1, 3);
                //Closed.SetMessage("", 1, 4);
                switch (messtype)
                {
                    case 0://Linking Farebox
                        //Closed.SetMessage("Linking", 2, 2);
                        //Closed.SetMessage("Farebox", 2, 3);
                        break;
                    case 1://Waiting...
                        //Closed.SetMessage("Waiting...", 2, 2);
                        break;
                    case 2://Waiting...
                        //Closed.SetMessage("Welcome", 2, 2);
                        //Closed.SetMessage("Aboard", 2, 3);
                        break;
                    case 3://Waiting...
                        //Closed.SetMessage("Closed", 2, 2);
                        break;
                }
                this.Controls.Add(ClosedScreen);
            }
        }

        private delegate void deleshowopen(int messtype);
        public void ShowOpen(int messtype)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new deleshowclosed(ShowOpen), new object[] { messtype });
            }
            else
            {
                this.Controls.Clear();
                //Closed.SetMessage("", 1, 1);
                //Closed.SetMessage("", 1, 2);
                //Closed.SetMessage("", 1, 3);
                //Closed.SetMessage("", 1, 4);
                switch (messtype)
                {
                    case 0://Linking Farebox
                        //Closed.SetMessage("Linking", 2, 2);
                        //Closed.SetMessage("Farebox", 2, 3);
                        break;
                    case 1://Waiting...
                        //Closed.SetMessage("Waiting...", 2, 2);
                        break;
                    case 2://Waiting...
                        //Closed.SetMessage("Welcome", 2, 2);
                        //Closed.SetMessage("Aboard", 2, 3);
                        break;
                    case 3://Waiting...
                        //Closed.SetMessage("Closed", 2, 2);
                        break;
                }
                
            }
        }
        
        public void ShowSetAutoClose()
        {
            string Horas = "";
            string Min = "";
            if (Classes.clsConfig.AutoCloseTime == 0)
            {
                Min = "Disabled";
            }
            else if (Classes.clsConfig.AutoCloseTime / 4 > 0)
            {
                Horas = ((int)(Classes.clsConfig.AutoCloseTime / 4)).ToString() + " Hours ";
            }
            if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 1)
            {
                Min = "15 Minutes";
            }
            else if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 2)
            {
                Min = "30 Minutes";
            }
            else if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 3)
            {
                Min = "45 Minutes";
            }
            SetAutoClose.lblValor.Text = Horas + Min;
            this.Controls.Clear();
            this.Controls.Add(SetAutoClose);
        }    
        public void ShowPassword()
        {
            this.Controls.Clear();
            this.Controls.Add(Password);
        }
        public void ShowTestScreen()
        {
            
            this.Controls.Clear();
            this.Controls.Add(TestScreen);
            TestScreen.inicio();
        }
        public void ShowPrincipal()
        {
            //Izquierdo oIzquierdo = new Izquierdo();
            Screens.ctrDerecho oDerecho = new BEAMDT.Screens.ctrDerecho();
            Screens.ctrEstatusDown oStatusDown = new BEAMDT.Screens.ctrEstatusDown();
            Screens.ctrEstatusUp oStatusUp = new BEAMDT.Screens.ctrEstatusUp();
            //Screens.ctrResult oResult = new BEAMDT.Screens.ctrResult();
            //Screens.ctrTeclado oTeclado = new BEAMDT.Screens.ctrTeclado();
            Screens.Teclados.ctrTecladoNumerico oTecladoNum = new BEAMDT.Screens.Teclados.ctrTecladoNumerico();
           // MainPrin.Izquierdo = oIzquierdo;
            //MainPrin.Derecho = null;
            //MainPrin.Result = oResult;
            //MainPrin.StatusUp = oStatusUp;
            //MainPrin.StatusDown = oStatusDown;
            //MainPrin.Teclado = oTecladoNum;
            //oResult.SetMessage("", "lbl1");
            //oResult.SetMessage("", "lbl2");
            //oResult.SetMessage("", "lbl3");
            //oResult.SetMessage("", "lbl4");
            //oTecladoNum.lblEscribir = oResult.lbl1;
            this.Controls.Clear();
            this.Controls.Add(MainPrin);
        }
      
       
        
        public void ShowQuestionEnaDis()
        {
            this.Controls.Clear();
            this.Controls.Add(QuestionEnaDis);
        }
        
        public void ShowSettings1()
        {
            if (Classes.clsConfig.TimeFormat == 0)
            {
                Settings1.lblTime.Text = "Time Format: 24H";
            }
            else
            {
                Settings1.lblTime.Text = "Time Format: 12H";
            }
            if (Classes.clsConfig.BrightValue == 1)
            {
                Settings1.lblBright.Text = "Open Shift Backlight: Low";
            }
            else if (Classes.clsConfig.BrightValue == 2)
            {
                Settings1.lblBright.Text = "Open Shift Backlight: Med";
            }
            else 
            {
                Settings1.lblBright.Text = "Open Shift Backlight: High";
            }
            Settings1.lblRDR.Text = "RDR File Version: " + BEAMDT.Program.RDRVersion.ToString();
            this.Controls.Clear();
            this.Controls.Add(Settings1);
        }
        public void ShowSettings2()
        {
            if (Classes.clsConfig.RouteEnabled)
            {
                Settings2.lblRouEna.Text = "Default Route Enabled: Yes";
            }
            else
            {
                Settings2.lblRouEna.Text = "Default Route Enabled: No";
            }
            Settings2.lblRouVal.Text = "Default Route Value: " + Classes.clsConfig.DefaultRoute.ToString();
            if (Classes.clsConfig.RunEnabled)
            {
                Settings2.lblRunEna.Text = "Default Run Enabled: Yes";
            }
            else
            {
                Settings2.lblRunEna.Text = "Default Run Enabled: No";
            }
            Settings2.lblRouVal.Text = "Default Run Value: " + Classes.clsConfig.DefaultRun.ToString();
            this.Controls.Clear();
            this.Controls.Add(Settings2);
        }
        public void ShowSettings3()
        {
            if (Classes.clsConfig.DirectionEnabled)
            {
                Settings3.lblDefDirEna.Text = "Default Direction: Yes";
            }
            else
            {
                Settings3.lblDefDirEna.Text = "Default Direction: No";
            }
            //inbound=0
            if (Classes.clsConfig.DefaultDirection==0)
            {
                Settings3.lblDefDiVa.Text = "Default Direction Value: I";
            }
            else
            {
                Settings3.lblDefDiVa.Text = "Default Direction Value: O";
            }
            string Horas = "";
            string Min = "";
            if (Classes.clsConfig.AutoCloseTime == 0)
            {
                Min = "Disabled";
            }
            else if (Classes.clsConfig.AutoCloseTime / 4 > 0)
            {
                Horas = ((int)(Classes.clsConfig.AutoCloseTime / 4)).ToString() + " H";
            }
            if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 1)
            {
                Min = "15 M";
            }
            else if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 2)
            {
                Min = "30 M";
            }
            else if (Classes.clsConfig.AutoCloseTime > 0 && Classes.clsConfig.AutoCloseTime % 4 == 3)
            {
                Min = "45 M";
            }
            Settings3.lblAutoClo.Text = "Auto Close Time: " + Horas + " " +Min;
            this.Controls.Clear();
            this.Controls.Add(Settings3);
        }
        
        public void ShowSetTimeFormat()
        {
            if (Classes.clsConfig.TimeFormat == 0)
            {
                SetTimeFormat.lblValor.Text = "24H";
            }
            else
            {
                SetTimeFormat.lblValor.Text = "12H";
            }
            this.Controls.Clear();
            this.Controls.Add(SetTimeFormat);
        }
        public void LoadButtonConfig()
        {
            screenbuttons.Clear();
            if (!System.IO.File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) +"\\ButtonConfig.xml"))
            {
                return;
            }
            DataSet ds = new DataSet();
            ds.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\ButtonConfig.xml");
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ScreenButton sb = new ScreenButton();
                    sb.Index = int.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                    sb.Label = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    if (ds.Tables[0].Rows[i].ItemArray[2].ToString() == "0")
                    {
                        sb.Enabled = false;
                    }
                    else
                    {
                        sb.Enabled = true;
                    }
                    sb.TextColor = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                    sb.BackColor = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                    sb.Count = 0;
                    screenbuttons.Add(sb);
                }
            }
        }
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            
            if (!System.IO.File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\DeviceConfig.xml"))
            {
                Classes.clsConfig.SaveConfig(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\DeviceConfig.xml");
            }
            else
            {
                Classes.clsConfig.LoadConf(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\ButtonConfig.xml");
            }
            if (!System.IO.File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\ButtonConfig.xml"))
            {
                LoadButtonConfig();
            }

            //SmartFTPClient.FtpClient Client = new SmartFTPClient.FtpClient();

            //Client.Login("ftpuser","Bea2014");

            Classes.clsUtils.SetShiftBrightness(true);
            this.Controls.Add(Splash);
            timer1.Enabled = true;
            //MainScreen.startpuntos();
            //serialportFarebox.ReceivedBytesThreshold = 1000;
            serialportFarebox.Encoding = System.Text.Encoding.Default;
            //serialportFarebox.ReceivedBytesThreshold = 10000;
            serialportFarebox.Open();
            //System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\PKTPT FT 0090 20150710 170052.its"
            /*//Xmodem.XmodemT XmodemTrans = new Xmodem.XmodemT(serialportFarebox, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Buslist.txt");
            //XmodemTrans.Procesar_XmodemT();
            //Comando(ABRETURNOMDT, 6, DatosComando);
            Xmodem.XmodemR xmodemreceive = new Xmodem.XmodemR(serialportFarebox, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\testxmodem.txt");
            xmodemreceive.Procesar_XmodemR();
            //serialportFarebox.Write("HOLA");
            //oRDR.LoadRDRFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase));*/
        }
        private void timerwait_Tick(object sender)
        {
            waitflag = true;
            
        }
        public void wait(int interval)
        {
            waitflag = false;
            timerwait = new System.Threading.Timer(new System.Threading.TimerCallback(timerwait_Tick), null, interval, System.Threading.Timeout.Infinite);
            while (!waitflag)
            {
                Application.DoEvents();
            }
        }
        System.Threading.Timer timerwait1;
        bool waitflag1;
        private void timerwait_Tick1(object sender)
        {
            waitflag1 = true;
            
        }
        public void wait1(int interval)
        {
            waitflag1 = false;
            timerwait1 = new System.Threading.Timer(new System.Threading.TimerCallback(timerwait_Tick1), null, interval, System.Threading.Timeout.Infinite);
            while (!waitflag1)
            {
                Application.DoEvents();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //ShowMainScreenNew();


            ShowPrincipal();

            //ShowMessageEsp(0);
            //ShowTestScreen();
            //Message.SetIconsVisible(false);
            //serialportFarebox.ReadExisting();
            //serialportFarebox.ReceivedBytesThreshold = 1;
            //BEAMDT.Program.Validator = Comando(9, 0, new byte[1]);
            //timer1.Enabled = false;
            ////ShowClosed(0);
            ////ShowOpen(0);
            //if (BEAMDT.Program.Validator == 10)
            //{
                
            //    Message.SetMessage("", 1, 1);
            //    Message.SetMessage("Closed", 2, 2);
            //    Message.SetMessage("", 1, 3);
            //    Message.SetMessage("", 1, 4);
            //    //timer1.Interval = 000;
            //    timer1.Enabled = false;
            //    shiftOpen = false;
            //}
            //else if(BEAMDT.Program.Validator == 1)
            //{
            //    shiftOpen = true;
            //    timer1.Enabled = false;
            //    Message.SetMessage("", 1, 1);
            //    Message.SetMessage("Welcome", 2, 2);
            //    Message.SetMessage("Aboard", 2, 3);
            //    Message.SetMessage("", 1, 4);
            //    /*if (Comando(VERSIONRDRMDT, 0, new byte[1]) != 0)
            //    {
            //        if (BEAMDT.Program.SacFileVersion != BEAMDT.Program.RDRVersion || !oRDR.RDRCHECK)
            //        {
            //            //Comando(SOLICITARDR, 0, new byte[1]);

            //        }
            //    }
            //    if (BEAMDT.Program.Validator == 1)
            //    {
            //        LoadButtonConfig();
            //        //MainScreenNew.LoadButtons(screenbuttons);
            //        BEAMDT.Program.VueltaAbierta = 1;
            //        Comando(SOLICITARUTA, 0, new byte[1]);
            //        Comando(SOLICITACHOF, 0, new byte[1]);
            //        Comando(SOLICITACORR, 0, new byte[1]);
            //        Classes.clsUtils.SetShiftBrightness(true);
            //        if (Classes.clsConfig.AutoCloseTime > 0)
            //        {
            //            autocloseTimer.Enabled = false;
            //            autocloseTimer.Interval = ((((int)(Classes.clsConfig.AutoCloseTime / 4)) * 60) * 60000) + ((((int)(Classes.clsConfig.AutoCloseTime % 4)) * 15) * 60000);
            //            autocloseTimer.Enabled = true;
            //        }
            //        ShowMainScreen();
            //    }
            //    else
            //    {
            //        BEAMDT.Program.VueltaAbierta = 0;
            //        Classes.clsUtils.SetShiftBrightness(false);
            //        ShowMessageEsp(1);
            //    }
            //    wait(1);
            //    Comando(ACTIVAMENS, 0, new byte[1]);
            //    */

            //}
            ////Message.SetIconsVisible(shiftOpen);
            
        }

        void EscribeMensaje(byte[] Dat)
        {
            if (BEAMDT.Program.VueltaAbierta == 0)
            {
                //SetMensaje(1, "", 1);
                //SetMensaje(2, "", 1);
                //SetMensaje(3, "", 1);
                //SetMensaje(4, "", 1);
                Message.SetMessage("", 0, 1);
                Message.SetMessage("", 0, 2);
                Message.SetMessage("", 0, 3);
                Message.SetMessage("", 0, 4);
            }
            else
            {
                //MainScreenNew.SetMessage("", 1, 2);
                //MainScreenNew.SetMessage("", 1, 3);
            }

            byte iResultado;
            byte iCont, iCont3, iBandCero, iCont2, iDigitos, iUbicacion, iDig;
            byte iContTrama, iLongMens, iRenglon;
            byte[] Mensaje = new byte[14];
            iResultado = 0;
            iCont = 0;
            iBandCero = 0;
            iDigitos = 10;
            iDig = 0;
            iRenglon = 0;
            iUbicacion = 0;
            iCont2 = 0;
            iContTrama = 0;
            iLongMens = 0;
            iResultado = Dat[2];
            for (iCont3 = 0; iCont3 < 14; iCont3++)
            {
                Mensaje[iCont3] = 0;
            }
            switch (iResultado)
            {
                case 0: //Mensaje normal
                    iContTrama = 13;
                    for (iCont = 1; iCont <= Dat[12]; iCont++)
                    {
                        iRenglon = Dat[iContTrama];
                        iContTrama++;
                        iUbicacion = Dat[iContTrama];
                        iContTrama++;
                        iContTrama++;
                        iLongMens = Dat[iContTrama];
                        iContTrama++;
                        for (iCont2 = 0; iCont2 < iLongMens; iCont2++)
                        {
                            Mensaje[iCont2] = Dat[iContTrama];
                            iContTrama++;
                        }
                        iContTrama++;
                        if (BEAMDT.Program.VueltaAbierta == 0)
                        {
                            //SetMensaje(iUbicacion, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iRenglon);
                            Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, iRenglon);
                        }
                        else
                        {
                            //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, iRenglon);
                        }

                        for (iCont3 = 0; iCont3 < 14; iCont3++)
                        {
                            Mensaje[iCont3] = 0;
                        }
                    }
                    break;

                case 1: //Mensaje Saldo dinero
                    iContTrama = 13;
                    iRenglon = Dat[iContTrama];
                    iContTrama++;
                    iUbicacion = Dat[iContTrama];
                    iContTrama++;
                    iContTrama++;
                    iLongMens = Dat[iContTrama];
                    iContTrama++;
                    for (iCont2 = 0; iCont2 < iLongMens; iCont2++)
                    {
                        Mensaje[iCont2] = Dat[iContTrama];
                        iContTrama++;
                    }
                    if (BEAMDT.Program.VueltaAbierta == 0)
                    {
                        Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, iRenglon);
                    }
                    else
                    {
                        //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, iRenglon);
                    }
                    for (iCont3 = 0; iCont3 < 14; iCont3++)
                    {
                        Mensaje[iCont3] = 0;
                    }
                    iContTrama += 1;
                    Mensaje[iDig] = (byte)'$';
                    iDig++;
                    if ((Dat[iContTrama] == 0) && (Dat[iContTrama + 1] == 0) && (Dat[iContTrama + 2] == 0) && (Dat[iContTrama + 3] == 0))
                    {
                        Mensaje[1] = (byte)'0';
                        Mensaje[2] = (byte)'0';
                        Mensaje[3] = (byte)'.';
                        Mensaje[4] = (byte)'0';
                        Mensaje[5] = (byte)'0';

                        if (BEAMDT.Program.VueltaAbierta == 0)
                        {
                            Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 3);
                        }
                        else
                        {
                            //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 3);
                        }
                    }
                    else
                    {
                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama]) >> 4));
                            iDig++;
                        }
                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama] & 0xF));
                            iDig++;
                        }
                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 1] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 1]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 1]) >> 4));
                            iDig++;
                        }


                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 1] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 1] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 1] & 0xF));
                            iDig++;
                        }


                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 2] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 2]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 2]) >> 4));
                            iDig++;
                        }

                        iBandCero = 1;
                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 2] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 2] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 2] & 0xF));
                            iDig++;
                        }
                        Mensaje[iDig] = (byte)'.';
                        iDig++;

                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 3] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 3]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 3]) >> 4));
                            iDig++;
                        }

                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 3] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 3] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 3] & 0xF));
                            iDig++;
                        }

                        if (BEAMDT.Program.VueltaAbierta == 0)
                        {
                            Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 3);
                        }
                        else
                        {
                            //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 3);
                        }
                    }



                    break;

                case 2: //Mensaje saldo viajes
                    iContTrama = 13;
                    iRenglon = Dat[iContTrama];
                    iContTrama++;
                    iUbicacion = Dat[iContTrama];
                    iContTrama++;
                    iContTrama++;
                    iLongMens = Dat[iContTrama];
                    iContTrama++;
                    for (iCont2 = 0; iCont2 < iLongMens; iCont2++)
                    {
                        Mensaje[iCont2] = Dat[iContTrama];
                        iContTrama++;
                    }

                    if (BEAMDT.Program.VueltaAbierta == 0)
                    {
                        Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, iRenglon);
                    }
                    else
                    {
                        //MainScreen.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, iRenglon);
                    }
                    for (iCont3 = 0; iCont3 < 14; iCont3++)
                    {
                        Mensaje[iCont3] = 0;
                    }
                    iContTrama += 2;
                    Mensaje[0] = (byte)(48 + (Dat[iContTrama] >> 4));
                    Mensaje[1] = (byte)(48 + (Dat[iContTrama] & 0xF));
                    Mensaje[2] = 47;
                    Mensaje[3] = (byte)(48 + (Dat[iContTrama + 1] >> 4));
                    Mensaje[4] = (byte)(48 + (Dat[iContTrama + 1] & 0xF));
                    Mensaje[5] = 47;
                    Mensaje[6] = (byte)(48 + (Dat[iContTrama + 2] >> 4));
                    Mensaje[7] = (byte)(48 + (Dat[iContTrama + 2] & 0xF));

                    if (BEAMDT.Program.VueltaAbierta == 0)
                    {
                        Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 3);
                    }
                    else
                    {
                        //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 3);
                    }
                    break;

                case 3: //Mensaje fecha
                    iContTrama = 13;
                    iRenglon = Dat[iContTrama];
                    iContTrama++;
                    iUbicacion = Dat[iContTrama];
                    iContTrama++;
                    iContTrama++;
                    iLongMens = Dat[iContTrama];
                    iContTrama++;
                    for (iCont2 = 0; iCont2 < iLongMens; iCont2++)
                    {
                        Mensaje[iCont2] = Dat[iContTrama];
                        iContTrama++;
                    }
                    if (BEAMDT.Program.VueltaAbierta == 0)
                    {
                        Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, iRenglon);
                    }
                    else
                    {
                        //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, iRenglon);
                    }
                    for (iCont3 = 0; iCont3 < 14; iCont3++)
                    {
                        Mensaje[iCont3] = 0;
                    }
                    iContTrama += 3;
                    if ((Dat[iContTrama] == 0) && (Dat[iContTrama + 1] == 0))
                    {
                        Mensaje[0] = (byte)'0';

                        if (BEAMDT.Program.VueltaAbierta == 0)
                        {
                            Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 3);
                        }
                        else
                        {
                            //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 3);
                        }
                    }
                    else
                    {
                        if (iBandCero == 0)
                        {
                            if (Dat[iContTrama] > 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama] & 15));
                                iDig++;
                                iBandCero = 1;
                            }
                        }
                        else
                        {

                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama] & 15));
                            iDig++;
                        }
                        if (iBandCero == 0)
                        {
                            if (Dat[iContTrama + 1] >> 4 != 0)
                            {

                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 1] >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                        }
                        else
                        {

                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 1] >> 4));
                            iDig++;
                        }

                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 1] & 0x0F) != 0)
                            {

                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 1] & 15));
                                iDig++;
                                iBandCero = 1;
                            }
                        }
                        else
                        {

                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 1] & 15));
                            iDig++;
                        }
                        if (BEAMDT.Program.VueltaAbierta == 0)
                        {
                            Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 3);
                        }
                        else
                        {
                            //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 3);

                        }
                    }
                    break;
                case 4: //Dinero depositado
                    iContTrama = 13;
                    for (iCont = 1; iCont <= Dat[12]; iCont++)
                    {
                        iRenglon = Dat[iContTrama];
                        iContTrama++;
                        iUbicacion = Dat[iContTrama];
                        iContTrama++;
                        iContTrama++;
                        iLongMens = Dat[iContTrama];
                        iContTrama++;
                        for (iCont2 = 0; iCont2 < iLongMens; iCont2++)
                        {
                            Mensaje[iCont2] = Dat[iContTrama];
                            iContTrama++;
                        }
                        iContTrama++;
                        if (BEAMDT.Program.VueltaAbierta == 0)
                        {
                            Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, iRenglon);
                        }
                        else
                        {
                            //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, iRenglon);

                        }
                        for (iCont3 = 0; iCont3 < 14; iCont3++)
                        {
                            Mensaje[iCont3] = 0;
                        }
                        //MostrarMensaje(Mensaje,iLongMens,iUbicacion,tahoma28,iRenglon);
                    }
                    iBandCero = 0;
                    iDigitos = 10;
                    iDig = 0;
                    Mensaje[iDig] = (byte)'$';
                    iDig++;
                    if ((Dat[iContTrama] == 0) && (Dat[iContTrama + 1] == 0) && (Dat[iContTrama + 2] == 0) && (Dat[iContTrama + 3] == 0))
                    {
                        Mensaje[1] = (byte)'0';
                        Mensaje[2] = (byte)'0';
                        Mensaje[3] = (byte)'.';
                        Mensaje[4] = (byte)'0';
                        Mensaje[5] = (byte)'0';

                        if (BEAMDT.Program.VueltaAbierta == 0)
                        {
                            Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 2);
                        }
                        else
                        {
                            //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 2);

                        }
                    }
                    else
                    {
                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama]) >> 4));
                            iDig++;
                        }

                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama] & 0xF));
                            iDig++;
                        }
                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 1] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 1]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 1]) >> 4));
                            iDig++;
                        }


                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 1] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 1] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 1] & 0xF));
                            iDig++;
                        }


                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 2] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 2]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 2]) >> 4));
                            iDig++;
                        }

                        iBandCero = 1;
                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 2] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 2] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 2] & 0xF));
                            iDig++;
                        }
                        Mensaje[iDig] = (byte)'.';
                        iDig++;

                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 3] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 3]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 3]) >> 4));
                            iDig++;
                        }

                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 3] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 3] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 3] & 0xF));
                            iDig++;
                        }

                        if (BEAMDT.Program.VueltaAbierta == 0)
                        {
                            Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 2);
                        }
                        else
                        {
                            //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 2);

                        }
                    }
                    for (iCont3 = 0; iCont3 < 14; iCont3++)
                    {
                        Mensaje[iCont3] = 0;
                    }
                    iContTrama += 4;


                    iBandCero = 0;
                    iDigitos = 10;
                    iDig = 0;
                    Mensaje[iDig] = (byte)'$';
                    iDig++;
                    if ((Dat[iContTrama] == 0) && (Dat[iContTrama + 1] == 0) && (Dat[iContTrama + 2] == 0) && (Dat[iContTrama + 3] == 0))
                    {
                        Mensaje[1] = (byte)'0';
                        Mensaje[2] = (byte)'0';
                        Mensaje[3] = (byte)'.';
                        Mensaje[4] = (byte)'0';
                        Mensaje[5] = (byte)'0';

                        if (BEAMDT.Program.VueltaAbierta == 0)
                        {
                            Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 4);
                        }
                        else
                        {
                            //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 4);

                        }
                    }
                    else
                    {
                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama]) >> 4));
                            iDig++;
                        }

                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama] & 0xF));
                            iDig++;
                        }
                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 1] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 1]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 1]) >> 4));
                            iDig++;
                        }


                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 1] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 1] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 1] & 0xF));
                            iDig++;
                        }


                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 2] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 2]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 2]) >> 4));
                            iDig++;
                        }

                        iBandCero = 1;
                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 2] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 2] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 2] & 0xF));
                            iDig++;
                        }
                        Mensaje[iDig] = (byte)'.';
                        iDig++;

                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 3] >> 4) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 3]) >> 4));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + ((Dat[iContTrama + 3]) >> 4));
                            iDig++;
                        }

                        if (iBandCero == 0)
                        {
                            if ((Dat[iContTrama + 3] & 0xF) != 0)
                            {
                                Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 3] & 0xF));
                                iDig++;
                                iBandCero = 1;
                            }
                            else
                            {
                                iDigitos--;
                            }
                        }
                        else
                        {
                            Mensaje[iDig] = (byte)(48 + (Dat[iContTrama + 3] & 0xF));
                            iDig++;
                        }

                        if (BEAMDT.Program.VueltaAbierta == 0)
                        {
                            Message.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 4);
                        }
                        else
                        {
                            //MainScreenNew.SetMessage(System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), 2, 4);
                        }
                    }
                    for (iCont3 = 0; iCont3 < 14; iCont3++)
                    {
                        Mensaje[iCont3] = 0;
                    }



                    break;
            }
            if (BEAMDT.Program.VueltaAbierta == 0)
            {
                
                    ShowMessage();
                

            }
            else
            {
                /*MainScreenNew.setVisible(MainScreenNew.lblMensaje_1, true);
                MainScreenNew.setVisible(MainScreenNew.lblMensaje_2, true);
                MainScreenNew.setVisible(MainScreenNew.lblClase, false);
                MainScreenNew.setVisible(MainScreenNew.lblDospuntos, false);
                MainScreenNew.setVisible(MainScreenNew.lblHora, false);
                MainScreenNew.setVisible(MainScreenNew.lblMin, false);
                MainScreenNew.setVisible(MainScreenNew.lblRuta, false);*/
                //MainScreenNew.timerBack.Enabled = true;
                /*if (!BuscaControl("MainScreen"))
                {
                    ShowMainScreen();
                }*/
            }

        }
        byte[] DatosComando = new byte[200];
        byte[] LastDatos = new byte[200];
        byte[] Buf = new byte[1];
        byte checksum(byte iTamano,byte iInicio,byte[] datos)
        {
            byte iCont;
            byte Check;
            Check=datos[iInicio];
            for (iCont=(byte)(iInicio+1);iCont<=iTamano+1;iCont++)
            {
                Check=(byte)(Check^datos[iCont]);
            }   
            return Check;
        }
        public void ReciveRDR()
        {
            while (!oRDR.RDRCHECK)
            {
                if (serialportFarebox.BytesToRead > 0)
                {
                    if (serialportFarebox.ReadByte() == 2)
                    {
                        wait(1);
                        DatosComando[0] = (byte)serialportFarebox.ReadByte();
                        for (int iCont = 0; iCont < DatosComando[0]; iCont++)
                        {
                            //wait(4);
                            DatosComando[iCont + 1] = (byte)serialportFarebox.ReadByte();
                        }
                        //wait(4);
                        byte Check = (byte)serialportFarebox.ReadByte();
                        //wait(4);
                        byte End = (byte)serialportFarebox.ReadByte(); ;
                        if ((Check == checksum(DatosComando[0], 0, DatosComando)) && (End == 3))
                        {
                            switch (DatosComando[1])
                            {


                                //Manda la version del archivo RDR lo que significa que inmediatamente despues
                                //se transmitira el archivo RDR
                                case 4:
                                    Buf[0] = 1;
                                    serialportFarebox.Write(Buf, 0, 1);
                                    //OffsetCom = 30;
                                    //x = 10;
                                    //SizeRecivedFile = 0;
                                    //SAC_FileVersion = (WORD)DatosComando[2] + (WORD)(DatosComando[3] * 256);
                                    //Buf[0] = 1;
                                    //Serial_Write(1, 1, Buf);
                                    //I2C_Writebyte(0, 0, 5);
                                    //BANDMENSAJE = 1;
                                    break;

                                //Transmision del archivo RDR
                                case 5:
                                    Buf[0] = 1;
                                    serialportFarebox.Write(Buf, 0, 1);
                                    //Si es el primer paquete del archivo
                                    if (iBandAct == 0)
                                    {
                                        //Almacenamos el tamaño del archivo
                                        SizeFile = (int)DatosComando[2] + (int)(DatosComando[3] * 256);
                                        rdrtemp = new byte[SizeFile]; ;
                                        OffsetCom = 0;
                                        //Guardamos los datos en un buffer temporal
                                        for (int iCont = 0; iCont < DatosComando[0] - 3; iCont++)
                                        {
                                            rdrtemp[iCont] = DatosComando[iCont + 4];
                                            OffsetCom++;
                                        }
                                        iBandAct = 1;

                                    }
                                    //Si no es el primer paquete
                                    else
                                    {
                                        //Guardamos los datos en un buffer temporal
                                        for (int iCont = 0; iCont < DatosComando[0] - 1; iCont++)
                                        {
                                            if (OffsetCom < rdrtemp.Length)
                                            {
                                                rdrtemp[OffsetCom] = DatosComando[iCont + 2];
                                                OffsetCom++;
                                            }
                                        }

                                        if (OffsetCom > SizeFile - 2)
                                        {

                                            iBandAct = 0;
                                            //Mandamos el ACK al SAC
                                            string Ver = BEAMDT.Program.SacFileVersion.ToString("X");
                                            while (Ver.Length < 4)
                                            {
                                                Ver = "0" + Ver;
                                            }
                                            string rdrName = "RDR " + Ver + " " + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".its";
                                            System.IO.FileStream sr = new System.IO.FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\" + rdrName, System.IO.FileMode.Create);
                                            sr.Write(rdrtemp, 0, rdrtemp.Length);
                                            sr.Flush();
                                            sr.Close();
                                            oRDR.RDRCHECK = true;
                                            BEAMDT.Program.RDRVersion = BEAMDT.Program.SacFileVersion;
                                            oRDR.LoadRDRFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase));

                                        }

                                    }
                                    //BANDMENSAJE = 1;
                                    break;

                            }

                        }
                        else
                        {
                            Buf[0] = 3;
                            serialportFarebox.Write(Buf, 0, 1);
                        }
                    }

                }
                Application.DoEvents();
            }
            
        }
        public byte Comando(byte iComm, byte size, byte[] Datos)
        {
           serialportFarebox.ReceivedBytesThreshold = 1000;
           byte Reintentos;
           byte[] Bufr=new byte[9];
           byte[] Data = new byte[100];
           byte[] DataAck = new byte[100];
           byte Salir2;
           byte iCont;
           
           Reintentos=0;
           Salir2=0;
           iCont=0;
           Data[0]=2;
           Data[1]=(byte)(size+1);
           Data[2] = 0;
           Data[3] = iComm;
           for (iCont=0;iCont<size;iCont++)
           {
              Data[iCont+4]=Datos[iCont];
           }
           Data[4+iCont]=checksum((byte)(size+3),1,Data);
           Data[4+iCont+1]=3;
       Reenviar:
           serialportFarebox.ReadExisting();
           Salir2 = 0;
           for (iCont = 0; iCont < (size + 6); iCont++)
           {
               //wait(1);
               Buf[0] = Data[iCont];
               serialportFarebox.Write(Buf, 0, 1);
           }
           wait(100);
           bool gotack = false;
           while (Salir2 == 0)
           {
               if (serialportFarebox.BytesToRead > 0)
               {
                   if (serialportFarebox.ReadByte() == 2)
                   {
                       wait(5);
                       DataAck[0] = (byte)serialportFarebox.ReadByte();
                       wait(5);
                       DataAck[1] = (byte)serialportFarebox.ReadByte();
                       for (int iCont2 = 0; iCont2 < DataAck[0]; iCont2++)
                       {
                           wait(1);
                           DataAck[iCont2 + 2] = (byte)serialportFarebox.ReadByte();
                       }
                       if (DataAck[1] != 1)
                       {
                           return 0;
                       }

                       //wait(4);
                       byte Check = (byte)serialportFarebox.ReadByte();
                       //wait(2);
                       byte End = (byte)serialportFarebox.ReadByte(); ;
                       if ((Check == checksum(DataAck[0], 0, DataAck)) && (End == 3))
                       {
                           gotack = true;
                       }
                   }
               }

               if (gotack)
               {
                   Salir2 = 1;
                   if (DataAck[3] == 0)
                   {
                       if (iComm == (byte)ComandosConsolaFarebox.BusquedaFarebox)//BUSQUEDASAC
                       {
                           serialportFarebox.ReceivedBytesThreshold = 1;
                           if (DataAck[4] == 0)
                           {
                               return 10;
                           }
                           else
                           {
                               return 1;
                           }
                       }
                       if (iComm == (byte)ComandosConsolaFarebox.ActivaMensajes)//Activa Mens
                       {
                           serialportFarebox.ReceivedBytesThreshold = 1;
                           if (Bufr[5] == 0)
                           {
                               return 10;
                           }
                           else
                           {
                               return 1;
                           }
                       }
                       if (iComm == (byte)ComandosConsolaFarebox.DesactivaMensajes) //Desactiva Mens
                       {
                           serialportFarebox.ReceivedBytesThreshold = 1;
                           if (Bufr[5] == 0)
                           {
                               return 10;
                           }
                           else
                           {
                               return 1;
                           }
                       }
                       if (iComm == (byte)ComandosConsolaFarebox.AbrirTurno)//abre turno
                       {
                           if (Classes.clsConfig.AutoCloseTime > 0)
                           {
                               autocloseTimer.Enabled = false;
                               autocloseTimer.Interval = ((((int)(Classes.clsConfig.AutoCloseTime / 4)) * 60) * 60000) + ((((int)(Classes.clsConfig.AutoCloseTime % 4)) * 15) * 60000);
                               autocloseTimer.Enabled = true;
                           }
                           BEAMDT.Program.VueltaAbierta = 1;
                           serialportFarebox.ReceivedBytesThreshold = 1;
                           return 1;
                       }
                       if (iComm == (byte)ComandosConsolaFarebox.CerrarTurno)//Cierra turno
                       {
                           if (Classes.clsConfig.AutoCloseTime > 0)
                           {
                               autocloseTimer.Enabled = false;
                           }
                           BEAMDT.Program.VueltaAbierta = 0;
                           serialportFarebox.ReceivedBytesThreshold = 1;
                           return 1;
                       }
                       else
                       {
                           serialportFarebox.ReceivedBytesThreshold = 1;
                           return 1;
                       }


                   }
                   else
                   {

                       Reintentos++;
                       wait(2);
                       if (Reintentos < 3)
                       {
                           goto Reenviar;
                       }
                       else
                       {
                           serialportFarebox.ReceivedBytesThreshold = 1;
                           return 0;
                       }
                   }
               }
               else
               {
                   Reintentos++;
                   wait(2);
                   if (Reintentos < 3)
                   {
                       goto Reenviar;
                   }
                   else
                   {
                       serialportFarebox.ReceivedBytesThreshold = 1;
                       return 0;
                   }
               
               }
               
           }
           serialportFarebox.ReceivedBytesThreshold = 1;
           return 0;
        }
        byte[] rdrtemp;
        int iBandAct = 0;
        int OffsetCom = 0;
        int SizeFile = 0;
        
        private void serialportFarebox_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] ack = { 2,2,1,0,0,0,3 }; 
            if (serialportFarebox.BytesToRead>0)
            {
                if (serialportFarebox.ReadByte() == 2)
                {
                    wait(5);
                    DatosComando[0] = (byte)serialportFarebox.ReadByte();
                    wait(5);
                    DatosComando[1] = (byte)serialportFarebox.ReadByte();
                    for (int iCont = 0; iCont < DatosComando[0]; iCont++)
                    {
                        //wait(4);
                        DatosComando[iCont + 2] = (byte)serialportFarebox.ReadByte(); 
                    }
                    if (DatosComando[1] != 0)
                    {
                        return;
                    }
                    
                    //wait(4);
                    byte Check = (byte)serialportFarebox.ReadByte();
                    //wait(2);
                    byte End = (byte)serialportFarebox.ReadByte(); ;
                    if ((Check == checksum(DatosComando[0], 0, DatosComando)) && (End == 3))
                    {
                        //int Contint = 0;
                        //for (int iCont = 0; iCont < DatosComando[0]; iCont++)
                        //{
                        //    if (iCont == 1)
                        //    {
                        //        DatosComando[Contint] = DatosComando[iCont];
                        //        Contint++;
                        //    }

                        //}
                        /*if (DatosComando[1] != ENVIAHORA && DatosComando[1] != PIDEGPS && VueltaAbierta == 1)
                        {
                            if (AutoCloseTime > 0)
                            {
                                if (Timer_Query(TimerCerrarTurno) == 1)
                                {
                                    Timer_Release(TimerCerrarTurno);
                                }
                                TimerCerrarTurno = Timer_Request((DWORD)AutoCloseTime * (DWORD)60 * (DWORD)1000);

                            }
                        }*/
                        ack[3] = DatosComando[2];
                        ack[4] = 0;
                        ack[5] = checksum(4, 1, ack);
                        switch (DatosComando[2])
                        {
                                //Se abrio turno desde el sac usando tarjeta
                            case (byte)ComandosFareboxConsola.AbrirTurno:
                                ack[4] = 0;
                                serialportFarebox.Write(ack,0,ack.Length);
                                BEAMDT.Program.VueltaAbierta = 1;
                                Classes.clsUtils.SetShiftBrightness(true);
                                BEAMDT.Program.RouteNumber = (int)DatosComando[2] + (int)(DatosComando[3] * 256);
                                //Route_Number_Desc[0] = 0;
                                //Route_Number_Desc[1] = 0;
                                //Route_Number_Desc[2] = 0;
                                //Route_Number_Desc[3] = 0;
                                //Route_Number_Desc[4] = 0;
                                BEAMDT.Program.DriverID = (int)DatosComando[4] + (((int)DatosComando[5]) * 256) + (((int)DatosComando[6]) * 65536);
                                BEAMDT.Program.Run = DatosComando[7];
                                //BANDMENSAJE = 1;
                                break;
                            //Se cerro turno desde el SAC utilizando tarjeta
                            case (byte)ComandosFareboxConsola.CerrarTurno:
                                ack[4] = 0;
                                serialportFarebox.Write(ack, 0, ack.Length);
                                BEAMDT.Program.VueltaAbierta = 0;
                                Classes.clsUtils.SetShiftBrightness(false);
                                
                                //Pantalla(2);
                                //BANDMENSAJE = 1;
                                break;
                            case (byte)ComandosFareboxConsola.SolicitudInicioArchivo:
                                ack[4] = 0;
                                serialportFarebox.Write(ack, 0, ack.Length);
                                fileConfimation = true;
                                break;

                            case (byte)ComandosFareboxConsola.ListoEnviarArchivo:
                                ack[4] = 0;
                                serialportFarebox.Write(ack, 0, ack.Length);
                                fileConfimation = true;
                                serialportFarebox.ReceivedBytesThreshold = 1000;
                                Xmodem.XmodemR xmodemreceive = new Xmodem.XmodemR(serialportFarebox, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\testxmodem.txt");
                                xmodemreceive.Procesar_XmodemR();
                                //serialportFarebox.Close();
                                serialportFarebox.ReceivedBytesThreshold = 1;
                                //serialportFarebox.Open();
                                Comando(8, 1, ack);
                                break; 
                                
                            //Llego un mensaje del SAC para desplegarlo en la pantalla de la
                            //navman
                            case (byte)ComandosFareboxConsola.EnviaMensaje:
                                ack[4] = 0;
                                serialportFarebox.Write(ack, 0, ack.Length);
                                byte[] DatosMuestra = new byte[200];
                                for (int iCont = 0; iCont < DatosComando.Length-1; iCont++)
                                {
                                    DatosMuestra[iCont] = DatosComando[iCont+1];
                                }
                                if (DatosComando[3] == 0 || DatosComando[3] == 4)
                                {
                                    
                                    flagRecall = false;
                                    EscribeMensaje(DatosMuestra);
                                    if (BEAMDT.Program.VueltaAbierta == 1)
                                    {
                                        //MainScreenNew.startback();
                                        //MainScreenNew.LastIndex = -1;
                                    }
                                    
                                }
                                else
                                {
                                    if (Classes.clsConfig.AutoCloseTime > 0)
                                    {
                                        autocloseTimer.Enabled = false;
                                        autocloseTimer.Interval = ((((int)(Classes.clsConfig.AutoCloseTime / 4)) * 60) * 60000) + ((((int)(Classes.clsConfig.AutoCloseTime % 4)) * 15) * 60000);
                                        autocloseTimer.Enabled = true;
                                    }
                                    flagRecall = true;
                                    for (int iCont = 0; iCont < DatosMuestra.Length; iCont++)
                                    {
                                        LastDatos[iCont] = DatosMuestra[iCont];
                                    }
                                    //MainScreenNew.LastIndex = -1;
                                    //ContSaldo++;
                                    //if (ContSaldo==2)
                                    //{
                                    //	ContSaldo=0;
                                    //	TimerSaldo=Timer_Request(2);
                                    //	while(Timer_Query(TimerSaldo) != 2) {};
                                    //	Timer_Release(TimerSaldo);
                                    //	Comando(ACTIVAMENS,0,DatosComando);
                                    //}
                                }
                                break;
                            case (byte)ComandosFareboxConsola.EnvioDatosTurno:
                                ack[4] = 0;
                                serialportFarebox.Write(ack, 0, ack.Length);
                                if (DatosComando[3] == 0)
                                {
                                    shiftOpen = false;
                                }
                                else
                                {
                                    shiftOpen = true;
                                }
                                //DriverID
                                //Route
                                //Run
                                break;
                            //Manda la version del archivo RDR lo que significa que inmediatamente despues
                            //se transmitira el archivo RDR
                            case (byte)ComandosFareboxConsola.RecepcionExitosaDeArchivo:
                                ack[4] = 0;
                                serialportFarebox.Write(ack, 0, ack.Length);
                                if (DatosComando[3] == 0)
                                {
                                    fileSuccess = 1;
                                }
                                else
                                {
                                    fileSuccess = 2;
                                }
                                //OffsetCom = 30;
                                //x = 10;
                                //SizeRecivedFile = 0;
                                //SAC_FileVersion = (WORD)DatosComando[2] + (WORD)(DatosComando[3] * 256);
                                //Buf[0] = 1;
                                //Serial_Write(1, 1, Buf);
                                //I2C_Writebyte(0, 0, 5);
                                //BANDMENSAJE = 1;
                                break;


                           
                            //Reset de la namvan
                            case (byte)ComandosFareboxConsola.ResetConsola:
                                ack[4] = 0;
                                serialportFarebox.Write(ack, 0, ack.Length);
                                //VueltaAbierta = 0;
                                //return 6;
                                break;

                            //Recibe minuto y hora
                            case (byte)ComandosFareboxConsola.EnviaFechaHora:
                                ack[4] = 0;
                                serialportFarebox.Write(ack, 0, ack.Length);
                                int HORA=0;
                                int MINUTOS=0;
                                
                                if (DatosComando[2] <= 23 && DatosComando[3] <= 59 )
                                {
                                    HORA = DatosComando[2];
                                    MINUTOS = DatosComando[3];
                                }
                                Classes.clsUtils.SetTime(HORA, MINUTOS);
                                
                                if (DatosComando[4] == DatosComando[5])
                                {
                                    if (DatosComando[4] != Classes.clsConfig.OffsetHora)
                                    {
                                        Classes.clsConfig.OffsetHora = DatosComando[4];
                                    }
                                }
                                //BANDMENSAJE = 0;
                                //if (locked == 0)
                                //{
                                //    Pantalla(3);
                                //}
                                //ShowMainScreen();
                                
                                break;
                            case (byte)ComandosFareboxConsola.SolicitaDatosGPS:
                                ack[4] = 0;
                                serialportFarebox.Write(ack, 0, ack.Length);
                                //TimerEnvGPS = Timer_Request(20);
                                //while (Timer_Query(TimerEnvGPS) != 2) { };
                                //Timer_Release(TimerEnvGPS);
                                //Coordenada[0] = 1;
                                //if (GPS == 0)
                                //{
                                //    DatosComando[11] = 100;
                                //    DatosComando[0] = 91;
                                //}
                                //else
                                //{
                                //    DatosComando[0] = Coordenada[1];
                                //    DatosComando[1] = Coordenada[2];
                                //    DatosComando[2] = Coordenada[3];
                                //    DatosComando[3] = Coordenada[4];
                                //    DatosComando[4] = Coordenada[5];
                                //    DatosComando[5] = Coordenada[6];
                                //    DatosComando[6] = Coordenada[7];
                                //    DatosComando[7] = Coordenada[8];
                                //    DatosComando[8] = Coordenada[9];
                                //    DatosComando[9] = Coordenada[10];
                                //    DatosComando[10] = Coordenada[11];
                                //    DatosComando[11] = Coordenada[12];
                                //    DatosComando[12] = Coordenada[13];
                                //    DatosComando[13] = Coordenada[14];
                                //    DatosComando[14] = Coordenada[15];
                                //    DatosComando[15] = Coordenada[16];
                                //    DatosComando[16] = Coordenada[17];
                                //}
                                //ComandoEspGPS(MANDAGPS, 17, DatosComando);

                                break;
                        }

                    }
                    else
                    {
                        ack[3] = DatosComando[1];
                        ack[4] = 1;
                        ack[5] = checksum(4, 1, ack);
                        serialportFarebox.Write(ack, 0, ack.Length);
                    }
                }

            }
        }

        private void frmPrincipal_Closing(object sender, CancelEventArgs e)
        {
            serialportFarebox.Close();
            BEAMDT.Program.ShowTaskbar();
        }
        public bool ReceiveFile()
        {
            int datasize = 8;
            //TODO calcular CRC
            byte[] data = new byte[datasize];
            fileConfimation = false;
            fileSuccess = 0;
            if (Comando(7, 0, data) == 1)
            {
                //TODO: Esperamos la confirmacion del archivo
                //poner time out
                while (!fileConfimation)
                {
                    Application.DoEvents();
                }
                
            }
            return false;
        }
        //-----------------------------------------------------------------------
        bool fileConfimation = false;
        int fileSuccess = 0;
        public bool SendFile(string FilePath)
        {
            int datasize = 9;
            string filename=System.IO.Path.GetFileName(FilePath);
            datasize = datasize + filename.Length;
            System.IO.FileInfo filesize = new FileInfo(FilePath);
            System.IO.StreamReader sr = new StreamReader(FilePath);
            string Data = sr.ReadToEnd();
            sr.Close();
            //TODO calcular CRC
            Xmodem.Crc16Ccitt oCRC = new Xmodem.Crc16Ccitt(Xmodem.InitialCrcValue.Zeros);
             
            int CRC = (int)oCRC.computeCRC16C(Encoding.Default.GetBytes(Data));
            byte[] data = new byte[datasize];
            fileConfimation = false;
            fileSuccess = 0;
            if (Comando(5, 0, data)==1)
            { 
                //TODO: Esperamos la confirmacion del archivo
                //poner time out
                while (!fileConfimation)
                {
                    Application.DoEvents();
                }
                data[0] = 1;
                data[1] = (byte)(Data.Length&0xFF);
                data[2] = (byte)((Data.Length >> 8) & 0xFF);
                data[3] = (byte)((Data.Length >> 16) & 0xFF);
                data[4] = (byte)((Data.Length >> 24) & 0xFF);
                data[5] = (byte)(CRC & 0xFF);
                data[6] = (byte)((CRC >> 8) & 0xFF);
                data[7] = (byte)filename.Length;
                int indexData=8;
                for (int i = 0; i < filename.Length; i++)
                {
                    data[indexData] = (byte)filename[i];
                    indexData++;
                }

                if (Comando((byte)ComandosConsolaFarebox.EnvioArchivo, (byte)data.Length, data) == 1)
                {
                    wait(2000);
                    serialportFarebox.ReceivedBytesThreshold = 10000;
                    Xmodem.XmodemT XmodemTrans = new Xmodem.XmodemT(serialportFarebox, FilePath);
                    XmodemTrans.Procesar_XmodemT();
                    //serialportFarebox.Close();
                    serialportFarebox.ReceivedBytesThreshold = 1;
                    //serialportFarebox.Open();
                    
                    //TODO Esperar confirmacion 
                    //agregar time out
                    //while (fileSuccess==0)
                    //{
                    //    Application.DoEvents();
                    //}
                    
                }
            }
            return false;
        }


        //-----------------------------------------------------------------------
        private void frmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            byte[] Data;
            //if (Classes.clsConfig.AutoCloseTime > 0)
            //{
            //    autocloseTimer.Enabled = false;
            //    autocloseTimer.Interval = ((((int)(Classes.clsConfig.AutoCloseTime / 4)) * 60) * 60000) + ((((int)(Classes.clsConfig.AutoCloseTime % 4)) * 15) * 60000);
            //    autocloseTimer.Enabled = true;
            //}
            
            switch(e.KeyValue)
            {
                case 112://1
                    if (!BotonIzq1)
                    {
                        return;
                    }
                    Data = new byte[2];
                    Data[0]=0x64;
                    Data[1] = 0x0;
                    Comando(15, 2, Data);    
                    //if (BEAMDT.Program.VueltaAbierta == 1 && flagRecall)
                    //{
                    //    EscribeMensaje(LastDatos);
                    //    //MainScreenNew.startback();
                    //}
                    break;
                case 113://2
                    if (!BotonIzq2)
                    {
                        return;
                    }
                    Data = new byte[1];
                    Data[0] = 0x64;
                    Comando(16, 0, Data); 
                    //if (BEAMDT.Program.VueltaAbierta == 1)
                    //{
                    //    /*if (MainScreenNew.LastIndex != -1)
                    //    {
                    //        MainScreenNew.CancelIndex(MainScreenNew.LastIndex);
                    //    }*/
                    //}
                    break;
                case 114://3
                    if (!BotonIzq3)
                    {
                        return;
                    }
                    Data = new byte[1];
                    Data[0] = 0x64;
                    Comando(20, 0, Data); 
                    //Comando(DESACTIVAMENS, 0, new byte[1]);
                    //Classes.clsUtils.SetShiftBrightness(true);
                    //if (BEAMDT.Program.VueltaAbierta == 0)
                    //{
                    //    Question.lblPregunta.Text = "Open a new Shift?";
                    //    Question.lblValor.Text = "";
                    //    Question.forwardscreen = returnscreens.Shift1;
                    //    Question.returnscreen = returnscreens.MessageEsp2;
                    //    ShowQuestion();
                    //}
                    //else
                    //{
                    //    ShowQuestionClose();
                    //}
                    break;
                case 115://2
                    if (!BotonIzq4)
                    {
                        return;
                    }
                    Data = new byte[1];
                    Data[0] = 0x1;
                    Comando(19, 1, Data); 
                    break;
                case 116://2
                    if (!BotonIzq5)
                    {
                        return;
                    }
                    Data = new byte[2];
                    Data[0] = 0x1;
                    Data[1] = 0x5;
                    Comando(17, 2, Data); 
                    break;
                case 117://2
                    if (!BotonIzq6)
                    {
                        return;
                    }
                    Data = new byte[6];
                    Data[0] = 1;
                    Data[1] = 1;
                    Data[2] = 1;
                    Data[3] = 1;
                    Data[4] = 1;
                    Data[5] = 1;
                    if (shiftOpen)
                    {
                        Message.SetMessage("", 1, 1);
                        Message.SetMessage("Closed", 2, 2);
                        Message.SetMessage("", 1, 3);
                        Message.SetMessage("", 1, 4);
                        Comando(2, (byte)Data.Length, Data);
                        shiftOpen = false;
                    }
                    else
                    {
                        Message.SetMessage("", 1, 1);
                        Message.SetMessage("Welcome", 2, 2);
                        Message.SetMessage("Aboard", 2, 3);
                        Message.SetMessage("", 1, 4);
                        Comando(1, (byte)Data.Length, Data);
                        shiftOpen = true;
                    }
                    //Message.SetIconsVisible(shiftOpen);
                    //if (BuscaControl("MainScreenNew"))
                    //{
                    //    Comando(DESACTIVAMENS, 0, new byte[1]);
                    //    //Classes.clsUtils.SetShiftBrightness(true);
                    //    Password.Option = 0;
                    //    Password.SetTextAndLimit("Password", "", 4);
                    //    ShowPassword();
                    //}
                    //else if (BuscaControl("Message"))
                    //{
                    //    Comando(DESACTIVAMENS, 0, new byte[1]);
                    //    //Classes.clsUtils.SetShiftBrightness(true);
                    //    Password.Option = 0;
                    //    Password.SetTextAndLimit("Password", "", 4);
                    //    ShowPassword();
                    //}
                    //else if (BuscaControl("Password"))
                    //{
                    //    this.Close();
                    //    Application.Exit();
                    //}
                    break;
                case 118://2
                    if (!BotonDer1)
                    {
                        return;
                    }
                    Data = new byte[1];
                    Comando(13, 0, Data);
                    //if (BEAMDT.Program.VueltaAbierta == 1)
                    //{
                    //    //Classes.clsPrinter oPrinter = new Classes.clsPrinter(serialportprinter.PortName);
                    //    //DateTime IssueTime = DateTime.Now;
                    //    //string Cad = IssueTime.ToString("ddMMHHmm");
                    //    //string Route = BEAMDT.Program.RouteNumber.ToString();

                    //    //oPrinter.TextSpecification(2);
                    //    //oPrinter.PrintStringLine(" MADISON COUNTY TRANSIT");
                    //    //oPrinter.NLineFeed(1);
                    //    //oPrinter.TextSpecification(35);
                    //    //oPrinter.PrintStringLine("    TRANSFER");
                    //    //oPrinter.NLineFeed(1);
                    //    //string CadRoute = "    ROUTE: " + Route;
                    //    //if (Route.Length > 1)
                    //    //{
                    //    //    int max = Route.Length - 1;
                    //    //    while (max > 0)
                    //    //    {
                    //    //        max--;
                    //    //        CadRoute = CadRoute.Substring(1, CadRoute.Length - 1);
                    //    //    }
                    //    //}
                    //    //oPrinter.PrintStringLine(CadRoute);
                    //    //oPrinter.NLineFeed(1);
                    //    //oPrinter.TextSpecification(3);
                    //    //oPrinter.PrintStringLine("   ISSUED AT:");
                    //    //oPrinter.TextSpecification(2);
                    //    //oPrinter.PrintStringLine(IssueTime.ToString("    HH:mm MM/dd/yyyy"));
                    //    //oPrinter.NLineFeed(1);
                    //    //oPrinter.TextSpecification(3);
                    //    //oPrinter.PrintStringLine("  VALID UNTIL:");
                    //    //oPrinter.TextSpecification(3);
                    //    //oPrinter.PrintStringLine(IssueTime.AddHours(1).ToString("HH:mm MM/dd/yyyy"));
                    //    //oPrinter.NLineFeed(1);
                    //    //oPrinter.Bar_Ver_Size(200);
                    //    //oPrinter.PrintText("  ");
                    //    //oPrinter.Cod_Barr(System.Text.Encoding.Default.GetBytes(Cad), 69);          //CODIGO DE BARRAS
                    //    //oPrinter.NLineFeed(5);
                    //    //oPrinter.PaperFullCut();
                    //    //oPrinter.ImpresoraClose();
                    //}
                    
                    break;
                case 119://2
                    if (!BotonDer2)
                    {
                        return;
                    }
                    Data = new byte[4];
                    Data[0] = 1;
                    Data[1] = 7;
                    Data[2] = 1;
                    Data[3] = 1;
                    Comando(14,(byte)Data.Length, Data);    
                //if (BEAMDT.Program.VueltaAbierta == 1)
                    //{
                    //    //MainScreenNew.LoadCount();
                    //}
                    break;
                case 120://2
                    if (!BotonDer3)
                    {
                        return;
                    }
                    Data = new byte[9];
                    Data[0] = 2;
                    Data[1] = 1;
                    Data[2] = 5;
                    Data[3] = 16;
                    Data[4] = 30;
                    Data[5] = 5;
                    Data[6] = 16;
                    Data[7] = 1;
                    Data[8] = 1;
                    Comando(14, (byte)Data.Length, Data);    
                    break;
                case 121://2
                    if (!BotonDer4)
                    {
                        return;
                    }
                    Data = new byte[2];
                    Data[0] = 20;
                    Data[1] = 0;
                    Comando(21, 0, Data);    
                    break;
                case 122://2
                    if (!BotonDer5)
                    {
                        return;
                    }
                    //Data = new byte[2];
                    //Comando(16, 0, Data);    
                    ReceiveFile();
                    //serialportFarebox.ReceivedBytesThreshold = 100000;
                    //Xmodem.XmodemR xmodemreceive = new Xmodem.XmodemR(serialportFarebox, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\testxmodem.txt");
                    //xmodemreceive.Procesar_XmodemR();
                    //serialportFarebox.ReceivedBytesThreshold = 1;
                    break;
                case 123://2
                    if (!BotonDer6)
                    {
                        return;
                    }
                    SendFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\PKTPT FT 0090 20150710 170052.its");
                    
                    
                //if (BuscaControl("Password"))
                    //{
                    //    if (Password.CurrCar > 0)
                    //    {
                    //        Password.lblValor.Text = Password.lblValor.Text.Substring(0, Password.lblValor.Text.Length - 1);
                    //        Password.CurrCar--;
                    //    }
                    //}
                    //else if (BuscaControl("MainScreenNew"))
                    //{
                    //    Comando(DESACTIVAMENS, 0, new byte[1]);
                    //    //Classes.clsUtils.SetShiftBrightness(true);
                    //    if (BEAMDT.Program.VueltaAbierta == 0)
                    //    {

                    //        ShowShift1();
                    //    }
                    //    else
                    //    {
                    //        ShowQuestionClose();
                    //    }
                    //}
                    //else if (BuscaControl("Message"))
                    //{
                    //    if (BEAMDT.Program.Validator == 0)
                    //    {
                    //        timer1.Enabled = false;
                    //        if (BEAMDT.Program.VueltaAbierta == 0)
                    //        {

                    //            ShowShift1();
                    //        }
                    //        else
                    //        {
                    //            ShowQuestionClose();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Comando(DESACTIVAMENS, 0, new byte[1]);
                    //        //Classes.clsUtils.SetShiftBrightness(true);
                    //        if (BEAMDT.Program.VueltaAbierta == 0)
                    //        {

                    //            ShowShift1();
                    //        }
                    //        else
                    //        {
                    //            ShowQuestionClose();
                    //        }
                    //    }
                    //}
                    //else if (BuscaControl("Shift1"))
                    //{
                    //    if (Shift1.CurrCar > 0)
                    //    {
                    //        Shift1.lblValor.Text = Shift1.lblValor.Text.Substring(0, Shift1.lblValor.Text.Length - 1);
                    //        Shift1.CurrCar--;
                    //    }
                    //}
                    //else if (BuscaControl("Shift2"))
                    //{
                    //    if (Shift2.CurrCar > 0)
                    //    {
                    //        Shift2.lblValor.Text = Shift2.lblValor.Text.Substring(0, Shift2.lblValor.Text.Length - 1);
                    //        Shift2.CurrCar--;
                    //    }
                    //}
                    break;
                case 32:// verde
                    //if (BuscaControl("QuestionClose"))
                    //{
                    //    byte[] dat = new byte[6];
                    //    dat[0] = (byte)(BEAMDT.Program.RouteNumber & 0xFF);
                    //    dat[1] = (byte)((BEAMDT.Program.RouteNumber >> 8) & 0xFF);
                    //    dat[2] = (byte)(BEAMDT.Program.DriverID & 0xFF);
                    //    dat[3] = (byte)((BEAMDT.Program.DriverID >> 8) & 0xFF);
                    //    dat[4] = (byte)((BEAMDT.Program.DriverID >> 16) & 0xFF);
                    //    dat[5] = (byte)(BEAMDT.Program.Run & 0xFF);
                    //    Comando(frmPrincipal.CIERRATURNOMDT, 6, dat);
                    //    if (BEAMDT.Program.Validator == 0)
                    //    {
                    //        BEAMDT.Program.VueltaAbierta = 0;
                    //    }
                    //    flagRecall = false;
                    //    ShowMessageEsp(1);
                    //    wait1(20);
                    //    Comando(frmPrincipal.ACTIVAMENS, 0, new byte[1]);
                    //}
                    //else if (BuscaControl("Password"))
                    //{
                    //    Comando(frmPrincipal.ACTIVAMENS, 0, new byte[1]);
                    //    if (BEAMDT.Program.VueltaAbierta == 0)
                    //    {
                    //        // Classes.clsUtils.SetShiftBrightness(false);
                    //        ShowMessageEsp(1);
                    //    }
                    //    else
                    //    {
                    //        //Classes.clsUtils.SetShiftBrightness(true);
                    //        if (!BuscaControl("MainScreenNew"))
                    //        {
                    //            ShowMainScreen();
                    //        }

                    //    }
                    //}
                    //else if (BuscaControl("Shift1"))
                    //{
                    //    if (String.IsNullOrEmpty(Shift1.lblValor.Text))
                    //    {
                    //        return;
                    //    }
                    //    Question.lblPregunta.Text = "Is the ID correct?";
                    //    Question.lblValor.Text = Shift1.lblValor.Text;
                    //    Question.returnscreen = frmPrincipal.returnscreens.Shift1;
                    //    Question.forwardscreen = frmPrincipal.returnscreens.Shift2;
                    //    ShowQuestion();



                    //}
                    //else if (BuscaControl("Shift2"))
                    //{
                    //    if (String.IsNullOrEmpty(Shift2.lblValor.Text))
                    //    {
                    //        return;
                    //    }
                    //    Question.lblPregunta.Text = "Is the Route correct?";
                    //    Question.lblValor.Text = Shift2.lblValor.Text;
                    //    Question.returnscreen = frmPrincipal.returnscreens.Shift2;
                    //    Question.forwardscreen = frmPrincipal.returnscreens.Shift3;
                    //    ShowQuestion();
                    //}
                    //else if (BuscaControl("Question"))
                    //{

                    //    switch (Question.forwardscreen)
                    //    {
                    //        case frmPrincipal.returnscreens.Settings1:
                    //            ShowSettings1();
                    //            break;
                    //        case frmPrincipal.returnscreens.Settings2:
                    //            ShowSettings2();
                    //            break;
                    //        case frmPrincipal.returnscreens.Settings3:
                    //            ShowSettings3();
                    //            break;
                    //        case frmPrincipal.returnscreens.Shift1:

                    //            ShowShift1();
                    //            break;
                    //        case frmPrincipal.returnscreens.Shift2:
                    //            BEAMDT.Program.DriverID = int.Parse(Question.lblValor.Text);
                    //            if (Classes.clsConfig.RouteEnabled && Classes.clsConfig.RunEnabled && Classes.clsConfig.DirectionEnabled)
                    //            {

                    //                BEAMDT.Program.RouteDesc = "DEF-1";
                    //                BEAMDT.Program.RouteNumber = Classes.clsConfig.DefaultRoute;
                    //                BEAMDT.Program.Run = Classes.clsConfig.DefaultRun;
                    //                BEAMDT.Program.Direction = Classes.clsConfig.DefaultDirection;
                    //                //MainScreenNew.setText(MainScreenNew.lblRuta, "Route: " + BEAMDT.Program.RouteNumber.ToString());
                    //                byte[] dat = new byte[6];
                    //                byte[] dat1 = new byte[1];
                    //                dat[0] = (byte)(BEAMDT.Program.RouteNumber & 0xFF);
                    //                dat[1] = (byte)((BEAMDT.Program.RouteNumber >> 8) & 0xFF);
                    //                dat[2] = (byte)(BEAMDT.Program.DriverID & 0xFF);
                    //                dat[3] = (byte)((BEAMDT.Program.DriverID >> 8) & 0xFF);
                    //                dat[4] = (byte)((BEAMDT.Program.DriverID >> 16) & 0xFF);
                    //                dat[5] = (byte)(BEAMDT.Program.Run & 0xFF);
                    //                dat1[0] = (byte)(BEAMDT.Program.Direction & 0xFF);
                    //                LoadButtonConfig();
                    //                //MainScreenNew.LoadButtons(screenbuttons);
                    //                if (Comando(frmPrincipal.ABRETURNOMDT, 6, dat) == 1)
                    //                {
                    //                    //wait1(20);
                    //                    Comando(frmPrincipal.CAMBIODIR, 1, dat1);
                    //                    //ShowMessageEsp(1);
                    //                    Comando(frmPrincipal.ACTIVAMENS, 0, dat);
                    //                    ShowMainScreen();
                    //                }
                    //                else
                    //                {
                    //                    BEAMDT.Program.VueltaAbierta = 1;
                    //                    ShowMainScreen();
                    //                }

                    //            }
                    //            else
                    //            {
                    //                ShowShift2();
                    //            }
                    //            break;
                    //        case frmPrincipal.returnscreens.Shift3:
                    //            BEAMDT.Program.RouteNumber = int.Parse(Question.lblValor.Text);
                    //            BEAMDT.Program.RouteDesc = Question.lblValor.Text;
                    //            BEAMDT.Program.Run = Classes.clsConfig.DefaultRun;
                    //            BEAMDT.Program.Direction = Classes.clsConfig.DefaultDirection;
                    //            //MainScreenNew.setText(MainScreenNew.lblRuta, "Route: " + BEAMDT.Program.RouteNumber.ToString());
                    //            byte[] dat11 = new byte[6];
                    //            byte[] dat111 = new byte[1];
                    //            dat11[0] = (byte)(BEAMDT.Program.RouteNumber & 0xFF);
                    //            dat11[1] = (byte)((BEAMDT.Program.RouteNumber >> 8) & 0xFF);
                    //            dat11[2] = (byte)(BEAMDT.Program.DriverID & 0xFF);
                    //            dat11[3] = (byte)((BEAMDT.Program.DriverID >> 8) & 0xFF);
                    //            dat11[4] = (byte)((BEAMDT.Program.DriverID >> 16) & 0xFF);
                    //            dat11[5] = (byte)(BEAMDT.Program.Run & 0xFF);
                    //            dat111[0] = (byte)(BEAMDT.Program.Direction & 0xFF);
                    //            LoadButtonConfig();
                    //            //MainScreenNew.LoadButtons(screenbuttons);
                    //            if (Comando(frmPrincipal.ABRETURNOMDT, 6, dat11) == 1)
                    //            {
                    //                //wait1(20);
                    //                Comando(frmPrincipal.CAMBIODIR, 1, dat111);
                    //                //ShowMessageEsp(1);
                    //                Comando(frmPrincipal.ACTIVAMENS, 0, dat11);
                    //                ShowMainScreen();
                    //            }
                    //            else
                    //            {
                    //                BEAMDT.Program.VueltaAbierta = 1;
                    //                ShowMainScreen();
                    //            }

                    //            break;
                    //    }
                    //}
                    break;
                case 27://rojo
                    Program.ShowTaskbar();
                    Cursor.Show();
                    Application.Exit();
                    //if (BuscaControl("QuestionClose"))
                    //{
                    //    if (!BuscaControl("MainScreen"))
                    //    {
                    //        ShowMainScreen();
                    //    }

                    //    Comando(frmPrincipal.ACTIVAMENS, 0, new byte[1]);
                    //}
                    //else
                    //    if (BuscaControl("Password"))
                    //    {
                    //        Comando(frmPrincipal.ACTIVAMENS, 0, new byte[1]);
                    //        if (BEAMDT.Program.VueltaAbierta == 0)
                    //        {
                    //            // Classes.clsUtils.SetShiftBrightness(false);
                    //            ShowMessageEsp(1);
                    //        }
                    //        else
                    //        {
                    //            //Classes.clsUtils.SetShiftBrightness(true);
                    //            if (!BuscaControl("MainScreen"))
                    //            {
                    //                ShowMainScreen();
                    //            }

                    //        }
                    //    }
                    //    else if (BuscaControl("Shift1"))
                    //    {
                    //        Comando(frmPrincipal.ACTIVAMENS, 0, new byte[1]);
                    //        if (BEAMDT.Program.VueltaAbierta == 0)
                    //        {
                    //            // Classes.clsUtils.SetShiftBrightness(false);
                    //            ShowMessageEsp(1);
                    //        }
                    //        else
                    //        {
                    //            //Classes.clsUtils.SetShiftBrightness(true);
                    //            if (!BuscaControl("MainScreen"))
                    //            {
                    //                ShowMainScreen();
                    //            }

                    //        }

                    //    }
                    //    else if (BuscaControl("Shift2"))
                    //    {
                    //        ShowShift1();
                    //    }
                    //    else if (BuscaControl("Question"))
                    //    {
                    //        switch (Question.returnscreen)
                    //        {
                    //            case frmPrincipal.returnscreens.Settings1:
                    //                ShowSettings1();
                    //                break;
                    //            case frmPrincipal.returnscreens.Settings2:
                    //                ShowSettings2();
                    //                break;
                    //            case frmPrincipal.returnscreens.Settings3:
                    //                ShowSettings3();
                    //                break;
                    //            case frmPrincipal.returnscreens.Shift1:
                    //                ShowShift1();
                    //                break;
                    //            case frmPrincipal.returnscreens.Shift2:
                    //                ShowShift2();
                    //                break;

                    //            case frmPrincipal.returnscreens.MessageEsp2:
                    //                ShowMessageEsp(1);
                    //                Comando(frmPrincipal.ACTIVAMENS, 0, new byte[1]);
                    //                break;

                    //        }

                    //    }
                    break;
                case 13://centro
                    break;
                case 37://izq
                    break;
                case 39://der
                    break;
                case 38://arr
                    break;
                case 40://aba
                    break;
            }
            
        }

        private void autocloseTimer_Tick(object sender, EventArgs e)
        {
            autocloseTimer.Enabled = false;
            //byte[] dat = new byte[6];
            //dat[0] = (byte)(BEAMDT.Program.RouteNumber & 0xFF);
            //dat[1] = (byte)((BEAMDT.Program.RouteNumber >> 8) & 0xFF);
            //dat[2] = (byte)(BEAMDT.Program.DriverID & 0xFF);
            //dat[3] = (byte)((BEAMDT.Program.DriverID >> 8) & 0xFF);
            //dat[4] = (byte)((BEAMDT.Program.DriverID >> 16) & 0xFF);
            //dat[5] = (byte)(BEAMDT.Program.Run & 0xFF);
            //if (Comando(frmPrincipal.CIERRATURNOMDT, 6, dat) == 1)
            //{
            //    BEAMDT.Program.VueltaAbierta = 0;
            //    Classes.clsUtils.SetShiftBrightness(false);
            //    flagRecall = false;
            //}
            //ShowMessageEsp(1);
            //Comando(frmPrincipal.ACTIVAMENS, 0, new byte[1]);
        }
        private delegate bool messagedelegate1(string controlName);
        public bool BuscaControl(string controlName)
        {
           
                foreach (Control c in this.Controls)
                {
                    if (c.Name == controlName)
                    {
                        return true;
                    }
                }
                return false;
           
        }
        private void SetMensaje(int Indice,String Mensaje,int Alineacion)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name == "Principal")
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc.Name == "panelArriba")
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                foreach (Control cccc in ccc.Controls)
                                {
                                    switch (Indice)
                                    { 
                                        case 1:
                                            if (cccc.Name == "lbl1")
                                            {
                                                Label lbl = (Label)ccc;
                                                lbl.Text = Mensaje;
                                                if (Alineacion == 1)
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopRight;
                                                }
                                                else if (Alineacion == 2)
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopLeft;
                                                }
                                                else 
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopCenter;
                                                }
                                            }
                                            break;
                                        case 2:
                                            if (cccc.Name == "lbl2")
                                            {
                                                Label lbl = (Label)ccc;
                                                lbl.Text = Mensaje;
                                                if (Alineacion == 1)
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopRight;
                                                }
                                                else if (Alineacion == 2)
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopLeft;
                                                }
                                                else
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopCenter;
                                                }
                                            }
                                            break;
                                        case 3:
                                            if (cccc.Name == "lbl3")
                                            {
                                                Label lbl = (Label)ccc;
                                                lbl.Text = Mensaje;
                                                if (Alineacion == 1)
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopRight;
                                                }
                                                else if (Alineacion == 2)
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopLeft;
                                                }
                                                else
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopCenter;
                                                }
                                            }
                                            break;
                                        case 4:
                                            if (cccc.Name == "lbl4")
                                            {
                                                Label lbl = (Label)ccc;
                                                lbl.Text = Mensaje;
                                                if (Alineacion == 1)
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopRight;
                                                }
                                                else if (Alineacion == 2)
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopLeft;
                                                }
                                                else
                                                {
                                                    lbl.TextAlign = ContentAlignment.TopCenter;
                                                }
                                            }
                                            break;
                                    }
                                    if (cccc.Name == "lblMinutos")
                                    {
                                        cccc.Text = DateTime.Now.ToString("mm tt");
                                    }
                                    if (cccc.Name == "lblDosPuntos")
                                    {
                                        if (toggle)
                                        {
                                            toggle = false;
                                            cccc.Text = ":";
                                        }
                                        else
                                        {
                                            toggle = true;
                                            cccc.Text = " ";
                                        }
                                    }
                                    if (cccc.Name == "lblTime")
                                    {
                                        cccc.Text = DateTime.Now.ToString("hh");
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        public void HandleButton(PictureBox PicEvent)
        {
            switch (PicEvent.Name)
            { 
                case "pic1Der":
                    
                    break;
                case "pic2Der":

                    break;
                case "pic3Der":

                    break;
                case "pic4Der":

                    break;
                case "pic5Der":

                    break;
                case "pic6Der":

                    break;
            }
        }

        bool toggle = false;
        private void timerHora_Tick(object sender, EventArgs e)
        {
            foreach(Control c in this.Controls)
            {
                if (c.Name == "Principal")
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc.Name == "panelArriba")
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                foreach (Control cccc in ccc.Controls)
                                {
                                    if (cccc.Name == "lblMinutos")
                                    {
                                        cccc.Text = DateTime.Now.ToString("mm tt");
                                    }
                                    if (cccc.Name == "lblDosPuntos")
                                    {
                                        if (toggle)
                                        {
                                            toggle = false;
                                            cccc.Text = ":";
                                        }
                                        else
                                        {
                                            toggle = true;
                                            cccc.Text = " ";
                                        }
                                    }
                                    if (cccc.Name == "lblTime")
                                    {
                                        cccc.Text = DateTime.Now.ToString("hh");
                                    }
                                }
                               
                            }
                        }
                    }
                }
            }
        }
        //public bool BuscaControl(string controlName)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.BeginInvoke(new messagedelegate1(BuscaControl), new object[] { controlName });
        //    }
        //    else
        //    {
        //        foreach (Control c in this.Controls)
        //        {
        //            if (c.Name == controlName)
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //    return false;
        //}
       
    }
}