using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using OpenNETCF.Net.Ftp;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace BEAMDT
{

    public partial class frmPrincipalAlt : Form
    {
        public enum ComandosFareboxConsola
        {
            ResetConsola = 1,
            EnviaFechaHora = 2,
            SolicitudInicioArchivo = 3,
            RecepcionExitosaDeArchivo = 4,
            ListoEnviarArchivo = 5,
            AbrirTurno = 6,
            CerrarTurno = 7,
            EnviaMensaje = 8,
            SolicitaDatosGPS = 9,
            EnvioDatosTurno = 10,
            Sincronizacion = 11,
            StatusDispositivos = 12,
            TransaccionFinalizada = 13,
            DesactivaBoton = 14,
            SolicitaZonaSegura = 15,
            ApagarEquipo = 16,
            TicketsLoaded = 17,
            CoinsDisable = 18
        }
        public enum ComandosConsolaFarebox
        {
            AbrirTurno = 1,
            CerrarTurno = 2,
            CambioDireccion = 3,
            ResetFarebox = 4,
            AvisoEnvioArchivo = 5,
            EnvioArchivo = 6,
            SolicitudEnvioArchivo = 7,
            RecepcionExitosaDeArchivo = 8,
            BusquedaFarebox = 9,
            SolicitaDatosTurno = 10,
            ActivaMensajes = 11,
            DesactivaMensajes = 12,
            ImprimeTransfer = 13,
            VentaPase = 14,
            OverrideBillete = 15,
            ByPassMoneda = 16,
            Count = 17,
            Override = 18,
            Cancel = 19,
            Dump = 20,
            ChangeCard = 21,
            ProgramaNumBus = 22,
            ProgramaRuta = 23,
            ProgramaFechaHora = 24,
            ProgramarOffset = 25,
            EnviaDatosGPS = 26,
            ClaseMoneda = 27,
            ZonaSegura = 28,
            SolicitarFechaHora = 29,
            Refresh = 30,
            Service = 31,
            EstadoDispositivos = 32,
            ProgramaQR = 33,
            ProgramarVolumen = 34,
            TransferNotRead = 35,
            TIcketsLoaded = 36,
            OpenShiftCancel = 37
        }
        public struct BillDenomination
        {
            public bool Enabled;
            public string Description;
            public double Value;
        }
        public struct BotonInterfaz
        {
            public bool Enabled;
            public int Funcion;
            public string Renglon1;
            public string Renglon2;
        }
        public struct PassengerClass
        {
            public int ID;
            public string Renglon1;
            public string Renglon2;
            public int TypeOfClass;
            public double Fare;
            public bool Enabled;
        }
        public struct Products
        {
            public int ID;
            public string Description;
            public int IDClass;
            public double Price;
            public int Type;
            public int Tickets;
            public double Money;
            public int Days;
            public DateTime InitDate;
            public DateTime EndDate;
        }
        public struct StatusDevices
        {
            public int Monedero;
            public int Billetero;
            public int Impresora;
            public int LectorRFID;
            public int LectorQRBarras;
            public int PapelImpresora;
            public int Consola;
            public int Cashbox;
        }
        public StatusDevices statusDevices = new StatusDevices();
        public struct StatusSensores
        {
            public int TapaSuperior;
            public int PuertaServicio;
            public int PuertaValores;
        }
        public StatusSensores statusSensores = new StatusSensores();
        public static bool BotonIzq1 = true;
        public static bool BotonIzq2 = true;
        public static bool BotonIzq3 = true;
        public static bool BotonIzq4 = true;
        public static bool BotonIzq5 = true;
        public static bool BotonIzq6 = true;

        public static bool BotonDer1 = true;
        public static bool BotonDer2 = true;
        public static bool BotonDer3 = true;
        public static bool BotonDer4 = true;
        public static bool BotonDer5 = true;
        public static bool BotonDer6 = true;

        public static bool BotonOk = false;
        public static bool BotonCancel = false;
        public static bool BotonArriba = false;
        public static bool BotonAbajo = false;
        public static bool BotonDerecha = false;
        public static bool BotonIzquierda = false;
        public static bool BotonCentro = false;

        public List<BillDenomination> ListBills = new List<BillDenomination>();
        public List<BillDenomination> ListPassengerClases = new List<BillDenomination>();
        public List<BillDenomination> ListMoneyClases = new List<BillDenomination>();
        public List<BillDenomination> ListProducts = new List<BillDenomination>();

        System.Threading.Timer timerRefresh;
        System.Threading.Timer timerwaitComm;
        System.Threading.Timer timerclock;
        System.Threading.Timer timerclockGPS;
        System.Threading.Timer timerStatus;
        System.Threading.Timer timercerrado;
        System.Threading.Timer timerprocesInicio;
        System.Threading.Timer timerprocessDispositivos;
        System.Threading.Timer timerCierraTurno;

        DateTime LastGPS = new DateTime();
        DateTime LastNoGPS = new DateTime();

        public string DataGPS;
        string Velocidad;
        string Angulo;
        string Longitd;
        string Latitud;
        string Lon;
        string Lat;
        DateTime UTCDatetime = new DateTime(1900, 1, 1, 0, 0, 0);
        int Satelites;
        float Fix;
        float HDP;
        string Valida = "";
        byte[] Coordenada = new byte[17];

        public Classes.clsRDR oRDR = new BEAMDT.Classes.clsRDR();
        public Classes.clsProductos oProducts = new BEAMDT.Classes.clsProductos();
        public Classes.clsTarifas oTarifa = new BEAMDT.Classes.clsTarifas();
        public Classes.clsConfigInterfaz oConfigInterfaz = new BEAMDT.Classes.clsConfigInterfaz();
        public Bluetooth.BluetoothCliente oBluetoothClient = new BEAMDT.Bluetooth.BluetoothCliente();

        public string RMC = "";
        public string GGA = "";
        public bool shiftOpen = false;
        public int fileSuccess = 0;
        Screens.SplashScreen oSplashScreen = new BEAMDT.Screens.SplashScreen();
        Screens.Principal oPrincipal = new BEAMDT.Screens.Principal();
        Screens.Close oClose = new BEAMDT.Screens.Close();
        Screens.Settings1 oSettings1 = new BEAMDT.Screens.Settings1();
        Screens.Settings2 oSettings2 = new BEAMDT.Screens.Settings2();
        Screens.Settings3 oSettings3 = new BEAMDT.Screens.Settings3();
        Screens.SystemInfo oSystemInfo = new BEAMDT.Screens.SystemInfo();
        public Screens.FTP1 oFtp1 = new BEAMDT.Screens.FTP1();
        public Screens.FTP2 oFtp2 = new BEAMDT.Screens.FTP2();
        public Screens.FTP3 oFtp3 = new BEAMDT.Screens.FTP3();
        public Screens.QuestionEnaDis oQuestionEnaDis = new BEAMDT.Screens.QuestionEnaDis();
        Screens.ProgramBusData oProgramBusData = new BEAMDT.Screens.ProgramBusData();
        Screens.ProgramBusDate oProgramBusDate = new BEAMDT.Screens.ProgramBusDate();
        Screens.FareboxVolume oFareboxVolume = new BEAMDT.Screens.FareboxVolume();
        Screens.FareboxBrightness oFareboxBrightness = new BEAMDT.Screens.FareboxBrightness();
        public Screens.ProgramBusNumber oProgramBusNumber = new BEAMDT.Screens.ProgramBusNumber();
        Screens.SetTimeFormat oSetTimeFormat = new BEAMDT.Screens.SetTimeFormat();
        Screens.SetAutoClose oSetAutoClose = new BEAMDT.Screens.SetAutoClose();
        Screens.SetDirection oSetDirection = new BEAMDT.Screens.SetDirection();
        public Screens.SetValue oSetValue = new BEAMDT.Screens.SetValue();
        Screens.Bluetooth1 oBluetooth1 = new BEAMDT.Screens.Bluetooth1();
        Screens.Bluetooth1 oBluetooth2 = new BEAMDT.Screens.Bluetooth1();
        public Screens.Password oPassword = new BEAMDT.Screens.Password();
        public Screens.ProgramConsole oProgramConsole = new BEAMDT.Screens.ProgramConsole();
        public Screens.ConsoleBrightness oConsoleBrightness = new BEAMDT.Screens.ConsoleBrightness();
        public Screens.ConsoleVolume oConsoleVolume = new BEAMDT.Screens.ConsoleVolume();


        private void InitButtons()
        {
            BotonIzq1 = false;
            BotonIzq2 = false;
            BotonIzq3 = false;
            BotonIzq4 = false;
            BotonIzq5 = false;
            BotonIzq6 = false;

            BotonDer1 = false;
            BotonDer2 = false;
            BotonDer3 = false;
            BotonDer4 = false;
            BotonDer5 = false;
            BotonDer6 = false;

            BotonOk = false;
            BotonCancel = false;
            BotonArriba = false;
            BotonAbajo = false;
            BotonDerecha = false;
            BotonIzquierda = false;
            BotonCentro = false;

        }
        public void ShowSplashScreen()
        {
            this.Controls.Clear();
            this.Controls.Add(oSplashScreen);
            this.Focus();
        }
        public void ShowPassword()
        {
            Program.MenuOn = 1;
            Program.IgnoreMessages = true;
            oPassword.SetTextAndLimit("Password", "", 4);
            this.Controls.Clear();
            this.Controls.Add(oPassword);
            this.Focus();
        }
        public void ShowSettings1()
        {
            oSettings1.Inicio(RMC, GGA);
            this.Controls.Clear();
            this.Controls.Add(oSettings1);
            oSettings1.timer1.Enabled = true;
            this.Focus();
        }
        public void ShowSystemInfo()
        {
            Get_Local_Files_Version();
            oSystemInfo.Inicio();
            this.Controls.Clear();
            this.Controls.Add(oSystemInfo);
            this.Focus();
        }
        public void ShowConsoleBrightness()
        {
            this.Controls.Clear();
            oConsoleBrightness.Init();
            this.Controls.Add(oConsoleBrightness);
            this.Focus();
        }
        public void ShowConsoleVolume()
        {
            this.Controls.Clear();
            oConsoleVolume.Init();
            this.Controls.Add(oConsoleVolume);
            this.Focus();
        }
        public void ShowProgramConsole()
        {
            this.Controls.Clear();
            oProgramConsole.Inicio();
            this.Controls.Add(oProgramConsole);
            this.Focus();
        }
        public void ShowSetDirection()
        {
            this.Controls.Clear();
            this.Controls.Add(oSetDirection);
            this.Focus();
        }
        public void ShowSettings2()
        {
            this.Controls.Clear();
            oSettings2.Inicio();
            this.Controls.Add(oSettings2);
            this.Focus();
        }
        public void ShowSettings3()
        {

            this.Controls.Clear();
            oSettings3.Inicio();
            this.Controls.Add(oSettings3);
            this.Focus();
        }
        public void ShowFTP1()
        {

            this.Controls.Clear();
            oFtp1.Inicio();
            this.Controls.Add(oFtp1);
            this.Focus();
        }
        public void ShowFTP2()
        {

            this.Controls.Clear();
            oFtp2.Inicio();
            this.Controls.Add(oFtp2);
            this.Focus();
        }
        public void ShowFTP3()
        {

            this.Controls.Clear();
            oFtp3.Inicio();
            this.Controls.Add(oFtp3);
            this.Focus();
        }
        public void ShowSetTimeFormat()
        {
            oSetTimeFormat.Inicio();
            this.Controls.Clear();
            this.Controls.Add(oSetTimeFormat);
            this.Focus();
        }
        public void ShowSetAutoClose()
        {
            this.Controls.Clear();
            oSetAutoClose.Inicio();
            this.Controls.Add(oSetAutoClose);
            this.Focus();
        }
        public void ShowSetProgramBusData()
        {
            this.Controls.Clear();
            this.Controls.Add(oProgramBusData);
            this.Focus();
        }
        public void ShowSetProgramBusDate()
        {
            this.Controls.Clear();
            this.Controls.Add(oProgramBusDate);
            this.Focus();
        }
        public void ShowSetProgramBusNumber()
        {
            this.Controls.Clear();
            this.Controls.Add(oProgramBusNumber);
            this.Focus();
        }
        public void ShowFareboxBrightness()
        {
            this.Controls.Clear();
            oFareboxBrightness.Init();
            this.Controls.Add(oFareboxBrightness);
            this.Focus();
        }
        public void ShowFareboxVolume()
        {
            this.Controls.Clear();
            oFareboxVolume.Init();
            this.Controls.Add(oFareboxVolume);
            this.Focus();
        }
        public void ShowSetValue()
        {
            this.Controls.Clear();
            oSetValue.Inicio();
            this.Controls.Add(oSetValue);
            this.Focus();
        }
        private void CleanLogFolder()
        {
            string FolderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Logs";
            string[] Files = System.IO.Directory.GetFiles(FolderPath);
            foreach (string file in Files)
            {
                bool fileErase = false;
                FileInfo fInfo = new FileInfo(file);
                if ((DateTime.Now.Year - fInfo.CreationTime.Year) > 0)
                {
                    fileErase = true;
                }
                if ((DateTime.Now.Month - fInfo.CreationTime.Month) > 0)
                {
                    fileErase = true;
                }
                if ((DateTime.Now.Day - fInfo.CreationTime.Day) > 0)
                {
                    fileErase = true;
                }
                if (fileErase)
                {
                    File.Delete(file);
                }
            }
        }

        private delegate void deleshowclose();
        public void ShowClose()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new deleshowclose(ShowClose));
            }
            else
            {
                Program.RouteNumber = 0;
                Program.DriverID = 0;
                Program.Run = 0;
                Program.IgnoreMessages = false;
                this.Controls.Clear();
                Program.MenuOn = 0;
                oClose.lblVersion.Text = "V " + Program.SoftwareVersion.ToString() + ".0";
                if (Program.FromClose)
                {
                    oClose.lbl6Der.Visible = false;
                    oClose.picOpen.Visible = false;
                    Program.FromClose = false;
                    oClose.SetMessage("", "lbl1", 2);
                    oClose.SetMessage("Please", "lbl2", 2);
                    oClose.SetMessage("Wait", "lbl3", 2);
                    oClose.SetMessage("", "lbl4", 2);
                }
                this.Controls.Add(oClose);
                if (Program.SetWiFiOn)
                {
                    this.Refresh();
                    this.Update();
                    Program.SetWiFiOn = false;
                    Classes.clsNetworkStatus.WifiOn();
                    oClose.lbl6Der.Visible = true;
                    oClose.picOpen.Visible = true;
                }
                this.Focus();
                StartProcessClosed();
            }
        }
        public void ShowBluetooth1()
        {
            this.Controls.Clear();
            oBluetooth1.Inicio();
            this.Controls.Add(oBluetooth1);
            this.Focus();
        }
        public void ShowQuestionEnaDis()
        {

            this.Controls.Clear();
            this.Controls.Add(oQuestionEnaDis);
            this.Focus();
        }


        private delegate void ResultDele(String screentoload);
        public void ShowPrincipal(String ScreenToLoad)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ResultDele(ShowPrincipal), new object[] { ScreenToLoad });
            }
            else
            {
                this.Controls.Clear();
                oPrincipal.Prin = this;
                ActiveScreenGen = ScreenToLoad;
                oPrincipal.Inicio(ScreenToLoad);
                this.Controls.Add(oPrincipal);
                if (Program.SetWiFiOff)
                {
                    this.Update();
                    this.Refresh();
                    Program.SetWiFiOff = false;
                    Classes.clsNetworkStatus.WifiOff();
                }
                this.Focus();
            }
        }
        private delegate void ResultDele11(String screentoload, bool ResetScreen);
        public void ShowPrincipal(String ScreenToLoad,bool ResetScreen)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ResultDele11(ShowPrincipal), new object[] { ScreenToLoad,ResetScreen });
            }
            else
            {
                this.Controls.Clear();
                oPrincipal.Prin = this;
                ActiveScreenGen = ScreenToLoad;
                oPrincipal.Inicio(ScreenToLoad);
                if (ScreenToLoad == "Open" && ResetScreen)
                {
                    oPrincipal.ResetScreen();
                }
                this.Controls.Add(oPrincipal);
                if (Program.SetWiFiOff)
                {
                    StartTimerRefresh();
                    this.Update();
                    this.Refresh();
                    Program.SetWiFiOff = false;
                    Classes.clsNetworkStatus.WifiOff();
                }
                this.Focus();
            }
        }

        public frmPrincipalAlt()
        {
            InitializeComponent();
        }

        private void frmPrincipalAlt_Load(object sender, EventArgs e)
        {
            CleanLogFolder();
            Classes.clsNetworkStatus.WifiOn();
            //Classes.clsBrightness.setBackLight(BEAMDT.Classes.clsBrightness.Brightness.Low);
            ReciveEvent = new System.IO.Ports.SerialDataReceivedEventHandler(serialPortFarebox_DataReceived);
            serialPortFarebox.DataReceived += ReciveEvent;
            LastGPS = DateTime.Now;
            LastNoGPS = DateTime.Now;
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Inbox"))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Inbox");
            }
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Outbox"))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Outbox");
            }
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Outbox_bkp"))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Outbox_bkp");
            }
            serialPortFarebox.Encoding = System.Text.Encoding.Default;
            serialPortGPS.Encoding = System.Text.Encoding.Default;
            Classes.clsConfig.LoadConf();
            Get_Local_Files_Version_Init();
            serialPortFarebox.Open();
            serialPortGPS.Open();
            serialPortGPS.Write("AT$GPSLCL=1,17\r");
            //ShowSplashScreen();
            ShowClose();
            StartTimer(1000);
            StartTimerGPS(3000);
            StartTimerStatus(Program.TimerStatus);
            StartProcessClosed();
            StartProcessInicio();
            //SetDevicesStatus(255);
            //SetSensorsStatus(255);
            StartProcessDispositivos();
            if (oConfigInterfaz.LoadOffset())
            {
                //SetTimeZone();
            }
            startUDPListener();
            //SynchSystemTime(new DateTime(2016,09,27,2,52,0));
            //SynchTime(new DateTime(2016, 09, 26, 21, 55, 0));
            //StartProcessBluetooth();
            //oBluetoothClient.StartBluetooth();
        }
        private void startUDPListener()
        {
            System.Threading.Thread t = new Thread(new ThreadStart(UDPListener));
            t.Start();
        }
        private void UDPListener()
        {
            Classes.clsAppUDPComm UDPListener = new BEAMDT.Classes.clsAppUDPComm();
            UDPListener.Listen();
        }
        private int GetTransactionFiles()
        {
            //return ReceiveFile();
            int Cont = 0;
            int reint = 0;
            bool Arch = true;
            while (Arch && Program.MenuOn==0)
            {
                int res = ReceiveFile();
                if (res == 0)
                {
                    Arch = false;
                }
                else if (res == 1)
                {
                    Cont++;
                }
                else
                {
                    reint++;
                }
                if (reint > 1)
                {
                    return Cont;
                }
                System.Threading.Thread.Sleep(200);
                if (BEAMDT.Program.MenuOn != 0)
                {
                    return Cont;
                }
            }
            return Cont;
        }
        private void UpdateFarebox()
        {
            int reintAL = 0;
            int reintHL = 0;
            int reintML = 0;
            int reintFT = 0;
            int reintPL = 0;
            int reintCF = 0;
            int reintFX = 0;
            string AppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string FilePath = AppPath + "\\Inbox\\";
            string[] Archivos;
            if (Program.MlUpdate || Program.AlUpdate || Program.FtUpdate || Program.PlUpdate || Program.HlUpdate || Program.CfUpdate || Program.FbxUpdate)
            {
                SetMessageClosed("Updating Farebox Files");
            }
            while ((Program.MlUpdate || Program.AlUpdate || Program.FtUpdate || Program.PlUpdate || Program.HlUpdate || Program.CfUpdate || Program.FbxUpdate) && Program.MenuOn == 0)
            {
                if (BEAMDT.Program.MenuOn != 0)
                {
                    return;
                }
                if (Program.AlUpdate)
                {
                    Archivos = System.IO.Directory.GetFiles(FilePath, "FBX AL*");
                    if (Archivos.Length > 0)
                    {
                        if (SendFile(Archivos[0]))
                        {
                            Program.AlUpdate = false;
                            Classes.clsConfig.LastALVersion = Program.AlCurrentVer;
                            Classes.clsConfig.SaveConfig();
                        }
                        else
                        {
                            reintAL++;
                        }
                        if (reintAL > 2)
                        {
                            return;
                        }
                    }
                }
                if (Program.FtUpdate)
                {
                    Archivos = System.IO.Directory.GetFiles(FilePath, "FBX FT*");
                    if (Archivos.Length > 0)
                    {
                        if (SendFile(Archivos[0]))
                        {
                            Program.FtUpdate = false;
                            Classes.clsConfig.LastFTVersion = Program.FtCurrentVer;
                            Classes.clsConfig.SaveConfig();
                        }
                        else
                        {
                            reintFT++;
                        }
                        if (reintFT > 2)
                        {
                            return;
                        }
                    }
                }


                if (Program.HlUpdate)
                {
                    Archivos = System.IO.Directory.GetFiles(FilePath, "FBX HL*");
                    if (Archivos.Length > 0)
                    {
                        if (SendFile(Archivos[0]))
                        {
                            Program.HlUpdate = false;
                            Classes.clsConfig.LastHLVersion = Program.HlCurrentVer;
                            Classes.clsConfig.SaveConfig();
                        }
                        else
                        {
                            reintHL++;
                        }
                        if (reintHL > 2)
                        {
                            return;
                        }
                    }
                }

                if (Program.MlUpdate)
                {
                    Archivos = System.IO.Directory.GetFiles(FilePath, "FBX ML*");
                    if (Archivos.Length > 0)
                    {
                        if (SendFile(Archivos[0]))
                        {
                            Program.MlUpdate = false;
                            //Classes.clsConfig.LastMLVersion = Program.MlCurrentVer;
                            Classes.clsConfig.SaveConfig();
                        }
                        else
                        {
                            reintML++;
                        }
                        if (reintML > 2)
                        {
                            return;
                        }
                    }
                }

                if (Program.CfUpdate)
                {
                    Archivos = System.IO.Directory.GetFiles(FilePath, "FBX CF*");
                    if (Archivos.Length > 0)
                    {
                        if (SendFile(Archivos[0]))
                        {
                            Program.CfUpdate = false;
                            Classes.clsConfig.LastCFVersion = Program.CfCurrentVer;
                            Classes.clsConfig.SaveConfig();
                        }
                        else
                        {
                            reintCF++;
                        }
                        if (reintCF > 2)
                        {
                            return;
                        }
                    }
                }

                if (Program.PlUpdate)
                {
                    Archivos = System.IO.Directory.GetFiles(FilePath, "FBX PL*");
                    if (Archivos.Length > 0)
                    {
                        if (SendFile(Archivos[0]))
                        {
                            Program.PlUpdate = false;
                            Classes.clsConfig.LastPLVersion = Program.PlCurrentVer;
                            Classes.clsConfig.SaveConfig();
                        }
                        else
                        {
                            reintPL++;
                        }
                        if (reintPL > 2)
                        {
                            return;
                        }
                    }
                }
                if (Program.FbxUpdate)
                {
                    Archivos = System.IO.Directory.GetFiles(FilePath, "FAREBOX*");
                    if (Archivos.Length > 0)
                    {
                        if (SendFile(Archivos[0]))
                        {
                            Program.FbxUpdate = false;
                            Classes.clsConfig.SaveConfig();
                            System.Threading.Thread.Sleep(1000);
                            System.IO.File.Delete(Archivos[0]);
                        }
                        else
                        {
                            reintFX++;
                        }
                        if (reintFX > 2)
                        {
                            return;
                        }
                    }
                }
            }
        }

        private void ProcessClosed(object sender)
        {
            try
            {
                if (Program.MenuOn==1 || Program.VueltaAbierta != 0 || Program.SyncingFarebox)
                {
                    Program.ExecutingProcessClosed = false;
                    return;
                }
                Bluetooth.BluetoothCliente oBluetoothClient = new BEAMDT.Bluetooth.BluetoothCliente();
                if (Program.Synching == 0)
                {
                    Program.SecsToSync = 900;
                    //int FTPSync = 0;
                    Classes.clsStatus oStatus = new BEAMDT.Classes.clsStatus();
                    //oStatus.ReadStatus(ref FTPSync);
                    //if (FTPSync == 1)
                    //{

                    //}
                    Program.Service = 1;
                    Random r = new Random(DateTime.Now.Millisecond);
                    int Interval = r.Next(1000, 3000);
                    System.Threading.Thread.Sleep(Interval);
                    if (Program.Synching == 1)
                    {
                        Program.Service = 0;
                        timercerrado = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessClosed), null, Program.SecsToSync * 1000, System.Threading.Timeout.Infinite);
                        return;
                    }
                    SetMessageClosed("Entering Service Mode");
                    byte[] Dat = new byte[1];
                    Dat[0] = 1;
                    
                    if (Comando((byte)ComandosConsolaFarebox.Service, 1, Dat) == 0)
                    {
                        Program.Service = 0;
                        //oStatus.WriteStatus();
                        timercerrado = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessClosed), null, Program.SecsToSync * 1000, System.Threading.Timeout.Infinite);
                        return;
                    }
                    Thread.Sleep(500);
                    OpenButtonVisible(false);
                    //Pedir Archivos al farebox
                    SetMessageClosed("Checking Farebox");
                    GetTransactionFiles();
                    if (BEAMDT.Program.MenuOn != 0)
                    {
                        return;
                    }
                    //Checar FTP
                    if (Program.SyncingFarebox)
                    {
                        Program.Service = 0;
                        //oStatus.WriteStatus();
                        Dat[0] = 0;
                        Comando((byte)ComandosConsolaFarebox.Service, 1, Dat);
                        timercerrado = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessClosed), null, Program.SecsToSync * 1000, System.Threading.Timeout.Infinite);
                        return;
                    }
                    Get_Local_Files_Version();
                    if (BEAMDT.Program.MenuOn != 0)
                    {
                        return;
                    }
                    //Actualizar Farebox
                    UpdateFarebox();
                    System.Windows.Forms.Application.DoEvents();
                    Dat[0] = 0;
                    Comando((byte)ComandosConsolaFarebox.Service, 1, Dat);
                    Program.Service = 0;
                    //oStatus.WriteStatus();
                    SetMessageClosed("");
                    if (Program.VueltaAbierta != -1)
                    {
                        OpenButtonVisible(true);
                    }
                    Program.Synching = 0;
                    if (Program.PendingSync)
                    {
                        Program.PendingSync = false;
                        StartProcessInicio();
                    }
                }
                else
                {
                    Program.Service = 0;
                    timercerrado = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessClosed), null, Program.SecsToSync * 1000, System.Threading.Timeout.Infinite);
                    return;
                }
                timercerrado = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessClosed), null, Program.SecsToSync * 1000, System.Threading.Timeout.Infinite);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        int CurIndexStatusDispositivos = 0;
        int MaxDevices = 0;
        private void ProcessStatusDispositivos(object sender)
        {
            int MaxShowDevices = 3;
            string DevicesStatus = "";
            List<string> ListDeviceStatus = new List<string>();
            try
            {
                if (GetSensorStatus() > 0 || GetDevicesStatus() > 0)
                {
                    ListDeviceStatus.AddRange(GetDevicesMessages());
                    ListDeviceStatus.AddRange(GetSensorMessages());
                    if (MaxDevices != ListDeviceStatus.Count)
                    {
                        CurIndexStatusDispositivos = 0;
                        MaxDevices = ListDeviceStatus.Count;
                    }
                    if (Program.VueltaAbierta == 1)
                    {
                        if (Program.CoinByPass != "")
                        {
                            MaxShowDevices = 2;
                            DevicesStatus = Program.CoinByPass;
                            if (MaxDevices > 0)
                            {
                                DevicesStatus = DevicesStatus + "-";
                            }
                        }
                        int i = 0;
                        for (i = CurIndexStatusDispositivos; i < ListDeviceStatus.Count; i++)
                        {
                            DevicesStatus = DevicesStatus + ListDeviceStatus[i] + "-";
                            MaxShowDevices--;
                            if (MaxShowDevices == 0)
                            {
                                CurIndexStatusDispositivos = i;
                                break;
                            }
                        }
                        if (CurIndexStatusDispositivos >= MaxDevices || i == ListDeviceStatus.Count)
                        {
                            CurIndexStatusDispositivos = 0;
                        }
                        DevicesStatus = DevicesStatus.Substring(0, DevicesStatus.Length - 1);
                        oPrincipal.SetMessageStatusDown(DevicesStatus);
                    }

                    timerprocessDispositivos = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessStatusDispositivos), null, 5000, System.Threading.Timeout.Infinite);
                }
                else
                {
                    if (Program.VueltaAbierta == 1)
                    {
                        if (Program.CoinByPass != "")
                        {
                            DevicesStatus = Program.CoinByPass;
                        }
                        oPrincipal.SetMessageStatusDown(DevicesStatus);
                    }
                    Program.ProcessDevices = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        bool toggle = false;
        private void timerHora_Tick(object sender, EventArgs e)
        {

        }
        int DatosTurno = -1;
        int DatosTurnoArrive = 0;
        int DatosFechaArrive = 0;
        void StartTimerRefresh()
        {
            timerRefresh = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessRefresh), null, 20000, System.Threading.Timeout.Infinite);
        }
        void StartProcessInicio()
        {
            if (!Program.SyncingFarebox)
            {
                Program.SyncingFarebox = true;
                timerprocesInicio = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessInicio), null, 750, System.Threading.Timeout.Infinite);
            }

        }
        private bool SendOpen()
        {
            byte[] Data = new byte[6];
            Data[0] = (byte)(Program.RouteNumber & 0xFF);
            Data[1] = (byte)((Program.RouteNumber >> 8) & 0xFF);
            Data[2] = (byte)(Program.DriverID & 0xFF);
            Data[3] = (byte)((Program.DriverID >> 8) & 0xFF);
            Data[4] = (byte)((Program.DriverID >> 16) & 0xFF);
            Data[5] = (byte)(Program.Run);
            if (Comando((byte)ComandosConsolaFarebox.AbrirTurno, (byte)Data.Length, Data) == 1)
            {
                Program.BotonEnabled = true;
                Data[0] = (byte)Program.Direction;
                Comando((byte)ComandosConsolaFarebox.CambioDireccion, 1, Data);
                if (timerCierraTurno != null)
                {
                    timerCierraTurno.Change(Timeout.Infinite, Timeout.Infinite);
                }
                int Interval = ((((int)(Classes.clsConfig.AutoCloseTime / 4)) * 60) * 60000) + ((((int)(Classes.clsConfig.AutoCloseTime % 4)) * 15) * 60000);
                if (Interval > 0)
                {
                    timerCierraTurno = new System.Threading.Timer(new System.Threading.TimerCallback(timerAutoClose_Tick), null, Interval, System.Threading.Timeout.Infinite);
                }
                Classes.clsConfig.TimeFormat = 0;
                Classes.clsConfig.SaveConfig();
                Program.VueltaAbierta = 1;
                Program.TimeStampOpenClose = GetNow();
                Program.Refresh = false;
                Program.OpenShiftFromCard = false;
                return true;
            }
            else
            {
                return false;
            }

        }
        private bool SendChangeRoute()
        {
            byte[] Data = new byte[2];
            Data[0] = (byte)(Program.RouteNumber & 0xFF);
            Data[1] = (byte)((Program.RouteNumber >> 8) & 0xFF);
            if (Comando((byte)ComandosConsolaFarebox.ProgramaRuta, (byte)Data.Length, Data) == 1)
            {
                if (timerCierraTurno != null)
                {
                    timerCierraTurno.Change(Timeout.Infinite, Timeout.Infinite);
                }
                int Interval = ((((int)(Classes.clsConfig.AutoCloseTime / 4)) * 60) * 60000) + ((((int)(Classes.clsConfig.AutoCloseTime % 4)) * 15) * 60000);
                if (Interval > 0)
                {
                    timerCierraTurno = new System.Threading.Timer(new System.Threading.TimerCallback(timerAutoClose_Tick), null, Interval, System.Threading.Timeout.Infinite);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool SendClosed()
        {
            byte[] Data = new byte[6];
            Data[0] = (byte)(Program.RouteNumber & 0xFF);
            Data[1] = (byte)((Program.RouteNumber >> 8) & 0xFF);
            Data[2] = (byte)(Program.DriverID & 0xFF);
            Data[3] = (byte)((Program.DriverID >> 8) & 0xFF);
            Data[4] = (byte)((Program.DriverID >> 16) & 0xFF);
            Data[5] = (byte)(Program.Run);
            if (Comando((byte)ComandosConsolaFarebox.CerrarTurno, (byte)Data.Length, Data) == 1)
            {
                Program.BotonEnabled = true;
                Program.SecsToSync = 20;
                StartProcessClosed();
                Program.VueltaAbierta = 0;
                Program.TimeStampOpenClose = GetNow();
                
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool LoadInboxFiles()
        {
            int ReturnVal = oProducts.LoadProducts();
            if (ReturnVal == -1)
            {
                SetMessageClosed("Product List File Not Found");
                System.Threading.Thread.Sleep(5000);
                SetMessageClosed("");
                return false;
            }
            else if (ReturnVal == -2)
            {
                SetMessageClosed("Product List File Exception");
                System.Threading.Thread.Sleep(5000);
                SetMessageClosed("");
                return false;
            }
            ReturnVal = oTarifa.LoadPassengerClass();
            if (ReturnVal == -1)
            {
                SetMessageClosed("Fare File Not Found");
                System.Threading.Thread.Sleep(5000);
                SetMessageClosed("");
                return false;
            }
            else if (ReturnVal == -2)
            {
                SetMessageClosed("Fare File Exception");
                System.Threading.Thread.Sleep(5000);
                SetMessageClosed("");
                return false;
            }
            ReturnVal = oConfigInterfaz.LoadButtonsBills();
            if (ReturnVal == -1)
            {
                SetMessageClosed("Config File Not Found");
                System.Threading.Thread.Sleep(5000);
                SetMessageClosed("");
                return false;
            }
            else if (ReturnVal == -2)
            {
                SetMessageClosed("Config File Exception");
                System.Threading.Thread.Sleep(5000);
                SetMessageClosed("");
                return false;
            }
            ReturnVal = oRDR.LoadRDR();
            if (ReturnVal == -1)
            {
                SetMessageClosed("RDR File Not Found");
                System.Threading.Thread.Sleep(5000);
                SetMessageClosed("");
                return false;
            }
            else if (ReturnVal == -2)
            {
                SetMessageClosed("RDR File Exception");
                System.Threading.Thread.Sleep(5000);
                SetMessageClosed("");
                return false;
            }
            return true;
        }
        void ProcessRefresh(object sender)
        {
            if (!Program.Refresh)
            {
                int res = Comando((byte)ComandosConsolaFarebox.Refresh, 0, new byte[1]);
                int reint = 0;
                while (res != 1 && reint < 2)
                {
                    res = Comando((byte)ComandosConsolaFarebox.Refresh, 0, new byte[1]);
                }
            }
        }
        void ProcessInicio(object sender)
        {
            DatosTurnoArrive = 0;
            DatosFechaArrive = 0;
            int ContTimeOut = 0;
            if (Comando((byte)ComandosConsolaFarebox.BusquedaFarebox, 0, new byte[1]) == 1)
            {
                Comando((byte)ComandosConsolaFarebox.SolicitaDatosTurno, 0, new byte[1]);
                while (DatosTurnoArrive == 0)
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                    ContTimeOut++;
                    if (ContTimeOut == 400)
                    {
                        timerprocesInicio = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessInicio), null, 750, System.Threading.Timeout.Infinite);
                        return;
                    }
                }
                ContTimeOut = 0;
                Comando((byte)ComandosConsolaFarebox.SolicitarFechaHora, 0, new byte[1]);
                while (DatosFechaArrive == 0)
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                    ContTimeOut++;
                    if (ContTimeOut == 400)
                    {
                        timerprocesInicio = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessInicio), null, 750, System.Threading.Timeout.Infinite);
                        return;
                    }
                }

                if (DatosTurno == 0)
                {
                    
                    Program.VueltaAbierta = 0;
                    ShowClose();
                    Program.SecsToSync = 900;
                    StartProcessClosed();
                }
                else if (DatosTurno == 1)
                {
                    
                    OpenButtonVisible(true);
                    if (!LoadInboxFiles())
                    {
                        return;
                    }
                    Classes.clsNetworkStatus.WifiOff();
                    Program.VueltaAbierta = 1;
                    ShowPrincipal("Open");
                    if (timerCierraTurno != null)
                    {
                        timerCierraTurno.Change(Timeout.Infinite, Timeout.Infinite);
                    }
                    int Interval = ((((int)(Classes.clsConfig.AutoCloseTime / 4)) * 60) * 60000) + ((((int)(Classes.clsConfig.AutoCloseTime % 4)) * 15) * 60000);
                    if (Interval > 0)
                    {
                        timerCierraTurno = new System.Threading.Timer(new System.Threading.TimerCallback(timerAutoClose_Tick), null, Interval, System.Threading.Timeout.Infinite);
                    }
                }
                else
                {
                    timerprocesInicio = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessInicio), null, 750, System.Threading.Timeout.Infinite);
                    return;
                }
                System.Threading.Thread.Sleep(500);
                int res = Comando((byte)ComandosConsolaFarebox.Refresh, 0, new byte[1]);
                int reint = 0;
                while (res != 1 && reint < 2)
                {
                    res = Comando((byte)ComandosConsolaFarebox.Refresh, 0, new byte[1]);
                }
            }
            else
            {
                timerprocesInicio = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessInicio), null, 750, System.Threading.Timeout.Infinite);
            }
        }
        private delegate void ResultActiveControl();
        public string getActiveControl()
        {
            foreach (Control c in this.Controls)
            {
                return c.Name;
            }
            return "";
        }
        private delegate void ResultActiveLabel();
        public string getLabelText(String lblName)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name == "Principal")
                {
                    Screens.Principal prin = (Screens.Principal)c;
                    return prin.GetLabelText(lblName);
                }
            }
            return "";
        }
        public string getActiveScreen()
        {
            foreach (Control c in this.Controls)
            {
                try
                {
                    Screens.Principal Prin = (Screens.Principal)c;
                    return Prin.CurrentScreen;
                }
                catch
                {
                    return "";
                }

            }
            return "";
        }
        private void ProcessCommand(String CommText)
        {
            byte[] Data;
            switch (CommText)
            {
                case "Transfer":
                    Data = new byte[1];
                    Comando((byte)ComandosConsolaFarebox.ImprimeTransfer, 0, Data);
                    //if (BEAMDT.Program.VueltaAbierta == 1)
                    //{
                    //    /*if (MainScreenNew.LastIndex != -1)
                    //    {
                    //        MainScreenNew.CancelIndex(MainScreenNew.LastIndex);
                    //    }*/
                    //}
                    break;
                case "Change Card":
                    Data = new byte[1];
                    Comando((byte)ComandosConsolaFarebox.ChangeCard, 0, Data);
                    break;
                case "Dump":
                    Data = new byte[1];
                    Comando((byte)ComandosConsolaFarebox.Dump, 0, Data);
                    break;
                case "Override":
                    Data = new byte[1];
                    Comando((byte)ComandosConsolaFarebox.Override, 0, Data);
                    break;
                case "TransferNotRead":
                    Data = new byte[1];
                    Comando((byte)ComandosConsolaFarebox.TransferNotRead, 0, Data);
                    break;
                case "QR":
                    Data = new byte[1];
                    Data[0] = 1;
                    Comando((byte)ComandosConsolaFarebox.ProgramaQR, 1, Data);
                    break;
                case "Bypass On":
                    Data = new byte[1];
                    Data[0] = 1;
                    if (Comando((byte)ComandosConsolaFarebox.ByPassMoneda, 1, Data) == 1)
                    {
                        Program.CoinByPass = "Coin Bypass On";
                        if (GetSensorStatus() == 0 && GetDevicesStatus() == 0)
                        {
                            oPrincipal.SetMessageStatusDown(Program.CoinByPass);
                        }
                    }
                    break;
                case "Bypass Off":
                    Data = new byte[1];
                    Data[0] = 0;
                    if (Comando((byte)ComandosConsolaFarebox.ByPassMoneda, 1, Data) == 1)
                    {
                        Program.CoinByPass = "";
                        if (GetSensorStatus() == 0 && GetDevicesStatus() == 0)
                        {
                            oPrincipal.SetMessageStatusDown(Program.CoinByPass);
                        }
                    }
                    break;
                case "Bypass 1 Coin":
                    Data = new byte[1];
                    Data[0] = 2;
                    if (Comando((byte)ComandosConsolaFarebox.ByPassMoneda, 1, Data) == 1)
                    {
                        Program.CoinByPass = "Coin Bypass On";
                        if (GetSensorStatus() == 0 && GetDevicesStatus() == 0)
                        {
                            oPrincipal.SetMessageStatusDown(Program.CoinByPass);
                        }
                    }
                    break;
                case "Eject Ticket":
                    Data = new byte[1];
                    Data[0] = 1;
                    Comando((byte)ComandosConsolaFarebox.Cancel, 1, Data);
                    break;
            }
        }
        private string GetFunction(String Screen, int Function)
        {
            if (Screen == "Open")
            {
                switch (Function)
                {
                    case 0:
                        return "";
                    case 1:
                        return "Transfer";
                    case 2:
                        return "Change Card";
                    case 3:
                        return "Recall";
                    case 4:
                        return "Refund";
                    case 5:
                        return "Dump";
                    case 6:
                        return "Override";
                    case 7:
                        return "Sale";
                    case 8:
                        return "Count";
                    case 9:
                        return "Special";
                    case 10:
                        return "Shift Control";
                    case 11:
                        return "QR";
                    case 12:
                        return "TransferNotRead";
                    default:
                        return "";
                }
            }
            else if (Screen == "Special")
            {
                switch (Function)
                {
                    case 0:
                        return "";
                    case 1:
                        return "Bypass On";
                    case 2:
                        return "Bypass Off";
                    case 3:
                        return "Bill Override";
                    case 4:
                        return "Eject Ticket";
                    case 5:
                        return "Cancel";
                    case 6:
                        return "Bypass 1 Coin";
                    case 7:
                        return "Volume";
                    case 8:
                        return "Brightness";
                    case 9:
                        return "Time Format";
                    case 10: //Volumen farebox
                        return "Volumen Farebox";
                    case 11:
                        return "Brightness Farebox";
                    default:
                        return "";
                }
            }
            else if (Screen == "Sale1")
            {
                switch (Function)
                {
                    case 0:
                        return "";
                    case 1:
                        return "Transfer";
                    case 2:
                        return "Change Card";
                    case 3:
                        return "Recall";
                    case 4:
                        return "Refund";
                    case 5:
                        return "Dump";
                    case 6:
                        return "Override";
                    case 7:
                        return "Sale";
                    case 8:
                        return "Count";
                    case 9:
                        return "Special";
                    case 10:
                        return "Shift Control";
                    default:
                        return "";
                }
            }
            return "";
        }
        bool fromSale = false;
        public void ProcessEvent(string ActiveControl, string ActiveScreen, string controlEvent, string controlText)
        {
            if (ActiveControl == "Principal" && ActiveScreen == "Open" && (controlText == "Transfer" || controlText == "Change Card" || controlText == "Recall" || controlText == "Refund" || controlText == "Count" || controlText == "Shift Control"))
            {
                if (!Program.BotonEnabled)
                {
                    return;
                }
            }
            if (DateTime.Now.Subtract(Program.timestamp).TotalSeconds < 2 && Program.LastButton == controlEvent)
            {
                return;
            }
            else
            {
                Program.LastButton = controlEvent;
                Program.timestamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            }
            if (Program.VueltaAbierta == 1 && Classes.clsConfig.AutoCloseTime > 0)
            {
                if (timerCierraTurno != null)
                {
                    timerCierraTurno.Change(Timeout.Infinite, Timeout.Infinite);
                }
                int Interval = ((((int)(Classes.clsConfig.AutoCloseTime / 4)) * 60) * 60000) + ((((int)(Classes.clsConfig.AutoCloseTime % 4)) * 15) * 60000);
                if (Interval > 0)
                {
                    timerCierraTurno = new System.Threading.Timer(new System.Threading.TimerCallback(timerAutoClose_Tick), null, Interval, System.Threading.Timeout.Infinite);
                }
            }
            byte[] Data;
            if (controlEvent.Contains("Der") || controlEvent.Contains("Izq"))
            {
                if (ActiveControl == "Principal")
                {
                    if (ActiveScreen == "Open")
                    {
                        switch (controlEvent)
                        {
                            case "picArriba":
                                break;
                            case "picAbajo":
                                break;
                        }
                        switch (controlText)
                        {
                            case "Transfer":
                                Program.BotonEnabled = false;
                                ProcessCommand(controlText);
                                break;
                            case "Change Card":
                                //Change card
                                ProcessCommand(controlText);
                                break;
                            case "Recall":
                                if (Program.FlagRecall)
                                {
                                    EscribeMensaje(LastDatos);
                                    Application.DoEvents();
                                    System.Threading.Thread.Sleep(3000);
                                    EscribeMensaje(LastTransactionMoneda);
                                }
                                break;
                            case "Refund":
                                ProcessCommand(controlText);
                                break;
                            case "Dump":
                                ProcessCommand(controlText);
                                break;
                            case "QR":

                                ProcessCommand(controlText);
                                break;
                            case "TransferNotRead":
                                ProcessCommand(controlText);
                                break;
                            case "Override":
                                ProcessCommand(controlText);
                                break;
                            case "Sale":
                                if (Program.CoinsDisabled)
                                {
                                    return;
                                }
                                Program.SaleDesc = "";
                                Program.IgnoreMessages = true;
                                ShowPrincipal("Sale");
                                break;
                            case "Count":
                                Program.IgnoreMessages = true;
                                ShowPrincipal("Count");
                                break;
                            case "Special":
                                if (ActiveScreen == "Open")
                                {
                                    fromSale = false;
                                }
                                else
                                {
                                    fromSale = true;
                                }
                                ShowPrincipal("Special");
                                break;
                            case "Shift Control":
                                if (DateTime.Now.Subtract(Program.TimeStampOpenClose).TotalSeconds < 15)
                                {
                                    return;
                                }
                                Program.IgnoreMessages = true;
                                ShowPrincipal("RouteMenu");
                                break;

                        }
                    }
                    else if (ActiveScreen == "Special")
                    {
                        switch (controlText)
                        {
                            case "Bypass On":
                                Program.IgnoreMessages = false;
                                ProcessCommand(controlText);
                                ShowPrincipal("Open");
                                break;
                            case "Bypass Off":
                                Program.IgnoreMessages = false;
                                ProcessCommand(controlText);
                                ShowPrincipal("Open");
                                break;
                            case "Bypass 1 Coin":
                                Program.IgnoreMessages = false;
                                ProcessCommand(controlText);
                                ShowPrincipal("Open");
                                break;
                            case "Bill Override":
                                if (Program.CoinsDisabled)
                                {
                                    return;
                                }
                                Program.IgnoreMessages = true;
                                ShowPrincipal("BillOverride");
                                break;
                            case "Eject Ticket":
                                ProcessCommand(controlText);
                                break;
                            case "Brightness":
                                ShowPrincipal("Brightness");
                                break;
                            case "Volume":
                                ShowPrincipal("Volume");
                                break;
                            case "Volumen Farebox":
                                ShowPrincipal("Volume Farebox");
                                break;
                            case "Time Format":
                                if (Classes.clsConfig.TimeFormat == 1)
                                {
                                    Classes.clsConfig.TimeFormat = 0;
                                }
                                else
                                {
                                    Classes.clsConfig.TimeFormat = 1;
                                }
                                Classes.clsConfig.SaveConfig();
                                break;
                            case "Cancel":
                                Program.IgnoreMessages = false;
                                if (fromSale)
                                {
                                    ShowPrincipal("Sale1");
                                }
                                else
                                {
                                    ShowPrincipal("Open");
                                }
                                EscribeMensaje(LastTransactionMoneda);
                                break;
                        }
                    }
                    else if (ActiveScreen == "Sale1")
                    {
                        switch (controlText)
                        {
                            case "Special":
                                fromSale = true;
                                ShowPrincipal("Special");
                                break;
                            case "Dump":
                                ShowPrincipal("Sale");
                                Program.IgnoreMessages = true;
                                if (Program.MoneyTransaction == 1)
                                {

                                    ProcessCommand("Dump");
                                }
                                else
                                {
                                    Data = new byte[1];
                                    Data[0] = 2;
                                    Comando((byte)ComandosConsolaFarebox.Cancel, 1, Data);
                                    Program.Cancel = true;
                                }
                                break;
                            case "Cancel":
                                ProcessCommand("Dump");
                                ShowPrincipal("Open");
                                break;
                        }
                    }
                    else if (ActiveScreen == "Sale")
                    {
                        switch (controlEvent)
                        {
                            case "lbl6Der":
                                if (BEAMDT.Program.SaleDesc == "")
                                {
                                    SetMensaje(2, "Please Select ", 2);
                                    SetMensaje(3, "a Product", 2);
                                    System.Threading.Thread.Sleep(2000);
                                    SetMensaje(2, "", 2);
                                    SetMensaje(3, "", 2);
                                }
                                else
                                {
                                    Program.Cancel = false;
                                    Program.IgnoreMessages = false;
                                    ShowPrincipal("Sale1");
                                    Data = new byte[1];
                                    foreach (Products p in oProducts.Products)
                                    {
                                        if (BEAMDT.Program.SaleDesc == p.Description)
                                        {
                                            switch (p.Type)
                                            {
                                                case 0: //E-purse
                                                    break;
                                                case 1: //Rides
                                                    break;
                                                case 2: //pass days
                                                    Data = new byte[6];
                                                    Data[0] = (byte)p.Type;
                                                    Data[1] = (byte)p.Days;
                                                    Data[2] = (byte)(((int)(p.Price * 100)) & 0xFF);
                                                    Data[3] = (byte)(((int)(p.Price * 100) >> 8) & 0xFF);
                                                    Data[4] = (byte)((p.ID) & 0xFF);
                                                    Data[5] = (byte)((p.ID >> 8) & 0xFF);
                                                    break;
                                                case 3: //pass
                                                    Data = new byte[11];
                                                    Data[0] = (byte)p.Type;
                                                    Data[1] = (byte)p.InitDate.Day;
                                                    Data[2] = (byte)p.InitDate.Month;
                                                    Data[3] = (byte)(p.InitDate.Year - 2000);
                                                    Data[4] = (byte)p.EndDate.Day;
                                                    Data[5] = (byte)p.EndDate.Month;
                                                    Data[6] = (byte)(p.EndDate.Year - 2000);
                                                    Data[7] = (byte)(((int)(p.Price * 100)) & 0xFF);
                                                    Data[8] = (byte)(((int)(p.Price * 100) >> 8) & 0xFF);
                                                    Data[9] = (byte)((p.ID) & 0xFF);
                                                    Data[10] = (byte)((p.ID >> 8) & 0xFF);
                                                    break;
                                            }
                                            if (p.Type == 2 || p.Type == 3)
                                            {
                                                Program.SaleProcess = true;
                                                if (Comando((byte)ComandosConsolaFarebox.VentaPase, (byte)Data.Length, Data) == 1)
                                                {

                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                                break;
                            case "lbl6Izq":
                                Program.IgnoreMessages = false;
                                ShowPrincipal("Open");
                                EscribeMensaje(LastTransactionMoneda);
                                break;
                        }
                    }
                    else if (ActiveScreen == "Count")
                    {
                        switch (controlEvent)
                        {
                            case "lbl6Der":
                                if (Program.CountDesc == "")
                                {
                                    SetMensaje(2, "Please Select ", 2);
                                    SetMensaje(3, "a Passenger Type", 2);
                                    System.Threading.Thread.Sleep(2000);
                                    SetMensaje(2, "", 2);
                                    SetMensaje(3, "", 2);
                                }
                                else
                                {
                                    //oPrincipal.oResultadoMensaje.SetMessage(BEAMDT.Program.CountDesc, "lbl2", 2);
                                    ShowPrincipal("Count1");
                                }
                                break;
                            case "lbl6Izq":
                                Program.IgnoreMessages = false;
                                ShowPrincipal("Open");
                                //if (Program.MoneyTransaction)
                                //{
                                EscribeMensaje(LastTransactionMoneda);
                                //oPrincipal.SetResultadoMonedas();
                                //}
                                break;
                        }
                    }
                    else if (ActiveScreen == "Count1")
                    {
                        switch (controlEvent)
                        {
                            case "lbl6Der":
                                Data = new byte[2];
                                string Pass = oPrincipal.oResultadoMensaje.GetMessage("lbl4");
                                string passengerClass = oPrincipal.oResultadoMensaje.GetMessage("lbl2");
                                if (String.IsNullOrEmpty(Pass))
                                {
                                    SetMensaje(4, "Insert a number", 2);
                                    System.Threading.Thread.Sleep(2000);
                                    SetMensaje(4, "", 2);
                                }
                                else
                                {
                                    bool bError = false;
                                    int Val = 0;
                                    foreach (char c in Pass)
                                    {
                                        if (!char.IsDigit(c))
                                        {
                                            bError = true;
                                            break;
                                        }
                                    }
                                    if (Pass.Length > 3)
                                    {
                                        bError = true;
                                    }
                                    if (!bError)
                                    {
                                        Val = int.Parse(Pass);
                                        if (Val < 0 || Val > 100)
                                        {
                                            bError = true;
                                        }
                                    }
                                    if (bError)
                                    {
                                        SetMensaje(4, "Invalid value", 2);
                                        System.Threading.Thread.Sleep(2000);
                                        SetMensaje(4, "", 2);
                                    }
                                    else
                                    {
                                        Data[1] = byte.Parse(Pass);
                                        Program.IgnoreMessages = false;
                                        ShowPrincipal("Open");
                                        Data[0] = 0;
                                        foreach (PassengerClass p in oTarifa.PassengersClass)
                                        {
                                            string Desc = "";
                                            if (p.Renglon1 != "Null")
                                            {
                                                Desc = p.Renglon1;
                                            }
                                            if (p.Renglon2 != "Null")
                                            {
                                                Desc = Desc + " ";
                                                Desc = Desc + p.Renglon2;
                                            }
                                            if (Desc == passengerClass)
                                            {
                                                Data[0] = (byte)p.ID;
                                                break;
                                            }
                                        }
                                        Comando((byte)ComandosConsolaFarebox.Count, (byte)Data.Length, Data);
                                    }


                                }
                                break;
                            case "lbl2Der":
                                SetMensaje(4, "", 2);
                                break;
                            case "lbl6Izq":
                                Program.CountDesc = "";
                                ShowPrincipal("Count");
                                break;
                        }
                    }
                    else if (ActiveScreen == "BillOverride")
                    {
                        switch (controlEvent)
                        {
                            case "lbl6Der":
                                Data = new byte[2];
                                string Val = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                if (String.IsNullOrEmpty(Val))
                                {
                                    SetMensaje(2, "Please Select ", 2);
                                    SetMensaje(3, "a Bill", 2);
                                    System.Threading.Thread.Sleep(2000);
                                    SetMensaje(2, "", 2);
                                    SetMensaje(3, "", 2);

                                }
                                else
                                {
                                    int bill = 0;
                                    foreach (BillDenomination b in oConfigInterfaz.AllowedBills)
                                    {
                                        if (b.Description == Val)
                                        {
                                            bill = (int)(b.Value * 100);
                                            break;
                                        }
                                    }
                                    Data[0] = (byte)(bill & 0xFF);
                                    Data[1] = (byte)((bill >> 8) & 0xFF);
                                    Program.IgnoreMessages = false;
                                    if (Program.SaleProcess)
                                    {
                                        ShowPrincipal("Sale1");
                                    }
                                    else
                                    {
                                        ShowPrincipal("Open");
                                    }
                                    //if (Program.MoneyTransaction)
                                    //{
                                    EscribeMensaje(LastTransactionMoneda);
                                    //oPrincipal.SetResultadoMonedas();
                                    //}
                                    Comando((byte)ComandosConsolaFarebox.OverrideBillete, (byte)Data.Length, Data);
                                }
                                break;
                            case "lbl6Izq":
                                Program.IgnoreMessages = false;
                                ShowPrincipal("Special");
                                if (Program.MoneyTransaction > 0)
                                {
                                    EscribeMensaje(LastTransactionMoneda);
                                    //oPrincipal.SetResultadoMonedas();
                                }
                                break;
                        }
                    }
                    else if (ActiveScreen == "Shift1")
                    {
                        switch (controlEvent)
                        {
                            case "lbl2Der":
                                SetMensaje(3, "", 2);
                                break;
                            case "lbl6Izq":
                                if (Program.Lock)
                                {
                                    ShowPrincipal("Lock");
                                }
                                else 
                                {
                                    if (Program.OpenShiftFromCard == true)
                                    {
                                        Comando((byte)ComandosConsolaFarebox.OpenShiftCancel, 0, new Byte[1]);
                                        Program.OpenShiftFromCard = false;
                                    }
                                    Program.IgnoreMessages = false;
                                    ShowClose();
                                }
                                
                                break;
                            case "lbl6Der":
                                int Val = 0;
                                bool bError = false;
                                string shiftData = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                if (string.IsNullOrEmpty(shiftData))
                                {
                                    bError = true;
                                }
                                else
                                {
                                    foreach (char c in shiftData)
                                    {
                                        if (!char.IsDigit(c))
                                        {
                                            bError = true;
                                            break;
                                        }
                                    }
                                    if (shiftData.Length > 8)
                                    {
                                        bError = true;
                                    }
                                    if (!bError)
                                    {
                                        Val = int.Parse(shiftData);
                                        if (Val < 0 || Val > 16777216)
                                        {
                                            bError = true;
                                        }
                                        else
                                        {
                                            if (!oRDR.BuscaChofer(Val))
                                            {
                                                bError = true;
                                            }
                                        }
                                    }
                                }
                                if (!bError)
                                {
                                    if (Program.Lock)
                                    {
                                        Program.Lock = false;
                                        Program.IgnoreMessages = false;
                                        ShowPrincipal("Open");   
                                    }
                                    else
                                    {
                                        Program.DriverID = Val;
                                        if (Classes.clsConfig.RouteEnabled)
                                        {
                                            if (!oRDR.BuscaRuta(Classes.clsConfig.DefaultRoute))
                                            {
                                                ShowPrincipal("Shift2");
                                            }
                                            else
                                            {
                                                if (Classes.clsConfig.RunEnabled)
                                                {
                                                    if (!oRDR.BuscaCorrida(Classes.clsConfig.DefaultRoute, Classes.clsConfig.DefaultRun))
                                                    {
                                                        Program.RouteNumber = Classes.clsConfig.DefaultRoute;
                                                        ShowPrincipal("Shift3");
                                                    }
                                                    else
                                                    {
                                                        if (Classes.clsConfig.DirectionEnabled)
                                                        {
                                                            Program.Run = Classes.clsConfig.DefaultRun;
                                                            Program.RouteNumber = Classes.clsConfig.DefaultRoute;
                                                            Program.Direction = Classes.clsConfig.DefaultDirection;

                                                            if (SendOpen())
                                                            {
                                                                Program.SetWiFiOff = true;
                                                                Program.IgnoreMessages = false;
                                                                ShowPrincipal("Open",true);

                                                            }
                                                            else
                                                            {
                                                                string Temp3 = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                                                string Temp2 = oPrincipal.oResultadoMensaje.GetMessage("lbl2");
                                                                SetMensaje(2, "Please", 2);
                                                                SetMensaje(3, "Try Again", 2);
                                                                System.Threading.Thread.Sleep(2000);
                                                                SetMensaje(2, Temp2, 2);
                                                                SetMensaje(3, Temp3, 2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Program.RouteNumber = Classes.clsConfig.DefaultRoute;
                                                            Program.Run = Classes.clsConfig.DefaultRun;
                                                            ShowPrincipal("Shift4");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Program.RouteNumber = Classes.clsConfig.DefaultRoute;
                                                    ShowPrincipal("Shift3");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ShowPrincipal("Shift2");
                                        }
                                    }
                                }
                                else
                                {
                                    SetMensaje(2, "Invalid", 2);
                                    SetMensaje(3, "Driver ID", 2);
                                    System.Threading.Thread.Sleep(2000);
                                    SetMensaje(2, "", 2);
                                    SetMensaje(3, "", 2);
                                }
                                break;
                        }
                    }
                    else if (ActiveScreen == "Shift2")
                    {
                        switch (controlEvent)
                        {
                            case "lbl2Der":
                                SetMensaje(3, "", 2);
                                break;
                            case "lbl6Izq":
                                Program.IgnoreMessages = false;
                                if (Program.ChangeRoute)
                                {
                                    Program.ChangeRoute = false;
                                    ShowPrincipal("Open");
                                }
                                else
                                {
                                    if (Program.OpenShiftFromCard == true)
                                    {
                                        byte[] Temp = new byte[1];
                                        Comando((byte)ComandosConsolaFarebox.OpenShiftCancel, 0, Temp);
                                        Program.OpenShiftFromCard = false;
                                    }
                                    ShowClose();
                                }
                                break;
                            case "lbl6Der":
                                int Val = 0;
                                bool bError = false;
                                string shiftData = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                if (string.IsNullOrEmpty(shiftData))
                                {
                                    bError = true;
                                }
                                else
                                {
                                    foreach (char c in shiftData)
                                    {
                                        if (!char.IsDigit(c))
                                        {
                                            bError = true;
                                            break;
                                        }
                                    }
                                    if (shiftData.Length > 5)
                                    {
                                        bError = true;
                                    }
                                    if (!bError)
                                    {
                                        Val = int.Parse(shiftData);
                                        if (Val < 0 || Val > 65535)
                                        {
                                            bError = true;
                                        }
                                        else
                                        {
                                            if (!oRDR.BuscaRuta(Val))
                                            {
                                                bError = true;
                                            }
                                        }
                                    }
                                }
                                if (!bError)
                                {
                                    Program.RouteNumber = Val;
                                    if (Program.ChangeRoute)
                                    {

                                        if (SendChangeRoute())
                                        {
                                            Program.ChangeRoute = false;
                                            Program.IgnoreMessages = false;
                                            Program.RouteNumber = Val;
                                            ShowPrincipal("Open");
                                        }
                                        else
                                        {
                                            string Temp3 = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                            string Temp2 = oPrincipal.oResultadoMensaje.GetMessage("lbl2");
                                            SetMensaje(2, "Please", 2);
                                            SetMensaje(3, "Try Again", 2);
                                            System.Threading.Thread.Sleep(2000);
                                            SetMensaje(2, Temp2, 2);
                                            SetMensaje(3, Temp3, 2);
                                        }
                                    }
                                    else
                                    {

                                        if (Classes.clsConfig.RunEnabled)
                                        {
                                            if (!oRDR.BuscaCorrida(Program.RouteNumber, Classes.clsConfig.DefaultRun))
                                            {
                                                Program.RouteNumber = Classes.clsConfig.DefaultRoute;
                                                ShowPrincipal("Shift3");
                                            }
                                            else
                                            {
                                                if (Classes.clsConfig.DirectionEnabled)
                                                {
                                                    Program.Run = Classes.clsConfig.DefaultRun;
                                                    Program.Direction = Classes.clsConfig.DefaultDirection;

                                                    if (SendOpen())
                                                    {
                                                        Program.SetWiFiOff = true;
                                                        Program.IgnoreMessages = false;
                                                        ShowPrincipal("Open",true);

                                                    }
                                                    else
                                                    {
                                                        string Temp3 = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                                        string Temp2 = oPrincipal.oResultadoMensaje.GetMessage("lbl2");
                                                        SetMensaje(2, "Please", 2);
                                                        SetMensaje(3, "Try Again", 2);
                                                        System.Threading.Thread.Sleep(2000);
                                                        SetMensaje(2, Temp2, 2);
                                                        SetMensaje(3, Temp3, 2);
                                                    }
                                                }
                                                else
                                                {
                                                    Program.Run = Classes.clsConfig.DefaultRun;
                                                    ShowPrincipal("Shift4");
                                                }
                                            }

                                        }
                                        else
                                        {
                                            ShowPrincipal("Shift3");
                                        }
                                    }
                                }
                                else
                                {
                                    SetMensaje(2, "Invalid", 2);
                                    SetMensaje(3, "Route", 2);
                                    System.Threading.Thread.Sleep(2000);
                                    SetMensaje(2, "", 2);
                                    SetMensaje(3, "", 2);
                                }
                                break;
                        }
                    }
                    else if (ActiveScreen == "Shift3")
                    {
                        switch (controlEvent)
                        {
                            case "lbl2Der":
                                SetMensaje(3, "", 2);
                                break;
                            case "lbl6Izq":
                                Program.IgnoreMessages = false;
                                if (Program.OpenShiftFromCard == true)
                                {
                                    byte[] Temp = new byte[1];
                                    Comando((byte)ComandosConsolaFarebox.OpenShiftCancel, 0, Temp);
                                    Program.OpenShiftFromCard = false;
                                }
                                ShowClose();
                                break;
                            case "lbl6Der":
                                bool bError = false;
                                int Val = 0;
                                string shiftData = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                if (string.IsNullOrEmpty(shiftData))
                                {
                                    bError = true;
                                }
                                else
                                {
                                    foreach (char c in shiftData)
                                    {
                                        if (!char.IsDigit(c))
                                        {
                                            bError = true;
                                            break;
                                        }
                                    }
                                    if (shiftData.Length > 3)
                                    {
                                        bError = true;
                                    }
                                    if (!bError)
                                    {
                                        Val = int.Parse(shiftData);
                                        if (Val < 0 || Val > 255)
                                        {
                                            bError = true;
                                        }
                                        else
                                        {
                                            if (!oRDR.BuscaCorrida(Program.RouteNumber, Val))
                                            {
                                                bError = true;
                                            }
                                        }
                                    }
                                }
                                if (!bError)
                                {
                                    Program.Run = Val;
                                    if (Classes.clsConfig.DirectionEnabled)
                                    {
                                        Program.Direction = Classes.clsConfig.DefaultDirection;

                                        if (SendOpen())
                                        {
                                            Program.SetWiFiOff = true;
                                            Program.IgnoreMessages = false;
                                            ShowPrincipal("Open",true);

                                        }
                                        else
                                        {
                                            string Temp3 = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                            string Temp2 = oPrincipal.oResultadoMensaje.GetMessage("lbl2");
                                            SetMensaje(2, "Please", 2);
                                            SetMensaje(3, "Try Again", 2);
                                            System.Threading.Thread.Sleep(2000);
                                            SetMensaje(2, Temp2, 2);
                                            SetMensaje(3, Temp3, 2);
                                        }
                                    }
                                    else
                                    {
                                        ShowPrincipal("Shift4");
                                    }
                                }
                                else
                                {
                                    SetMensaje(2, "Invalid", 2);
                                    SetMensaje(3, "Run Number", 2);
                                    System.Threading.Thread.Sleep(2000);
                                    SetMensaje(2, "", 2);
                                    SetMensaje(3, "", 2);
                                }
                                break;
                        }
                    }
                    else if (ActiveScreen == "Shift4")
                    {
                        switch (controlEvent)
                        {
                            case "lbl1Der":
                                SetMensaje(3, "Inbound", 2);
                                break;
                            case "lbl2Der":
                                SetMensaje(3, "Outbound", 2);
                                break;
                            case "lbl6Izq":
                                Program.IgnoreMessages = false;
                                if (Program.OpenShiftFromCard == true)
                                {
                                    byte[] Temp = new byte[1];
                                    Comando((byte)ComandosConsolaFarebox.OpenShiftCancel, 0, Temp);
                                    Program.OpenShiftFromCard = false;
                                }
                                ShowClose();
                                break;
                            case "lbl6Der":
                                if (string.IsNullOrEmpty(oPrincipal.oResultadoMensaje.GetMessage("lbl3")))
                                {
                                    SetMensaje(2, "Please select", 2);
                                    SetMensaje(3, "a direction", 2);
                                    System.Threading.Thread.Sleep(3000);
                                    SetMensaje(2, "", 2);
                                    SetMensaje(3, "", 2);
                                }
                                else
                                {
                                    if (oPrincipal.oResultadoMensaje.GetMessage("lbl3") == "Inbound")
                                    {
                                        Program.Direction = 0;
                                    }
                                    else
                                    {
                                        Program.Direction = 1;
                                    }

                                    if (SendOpen())
                                    {
                                        Program.SetWiFiOff = true;
                                        Program.IgnoreMessages = false;
                                        ShowPrincipal("Open");

                                    }
                                    else
                                    {
                                        string Temp3 = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                        string Temp2 = oPrincipal.oResultadoMensaje.GetMessage("lbl2");
                                        SetMensaje(2, "Please", 2);
                                        SetMensaje(3, "Try Again", 2);
                                        System.Threading.Thread.Sleep(2000);
                                        SetMensaje(2, Temp2, 2);
                                        SetMensaje(3, Temp3, 2);
                                    }

                                }

                                break;
                        }
                    }
                    else if (ActiveScreen == "RouteMenu")
                    {
                        switch (controlEvent)
                        {
                            case "lbl1Der":
                                SetMensaje(3, "Change Route", 2);
                                break;
                            case "lbl2Der":
                                SetMensaje(3, "Close Shift", 2);
                                break;
                            case "lbl3Der":
                                SetMensaje(3, "Lock Device", 2);
                                break;
                            case "lbl6Izq":
                                Program.BotonEnabled = true;
                                Program.IgnoreMessages = false;
                                ShowPrincipal("Open");
                                break;
                            case "lbl6Der":
                                if (string.IsNullOrEmpty(oPrincipal.oResultadoMensaje.GetMessage("lbl3")))
                                {
                                    SetMensaje(2, "Please select", 2);
                                    SetMensaje(3, "an option", 2);
                                    System.Threading.Thread.Sleep(3000);
                                    SetMensaje(2, "", 2);
                                    SetMensaje(3, "", 2);
                                }
                                else
                                {

                                    string option = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                    if (option == "Close Shift")
                                    {
                                        if (SendClosed())
                                        {
                                            Program.SetWiFiOn = true;
                                            Program.IgnoreMessages = false;
                                            ShowClose();

                                        }
                                        else
                                        {
                                            string Temp3 = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                            string Temp2 = oPrincipal.oResultadoMensaje.GetMessage("lbl2");
                                            SetMensaje(2, "Please", 2);
                                            SetMensaje(3, "Try Again", 2);
                                            System.Threading.Thread.Sleep(2000);
                                            SetMensaje(2, Temp2, 2);
                                            SetMensaje(3, Temp3, 2);
                                        }

                                    }
                                    else if (option == "Lock Device")
                                    {
                                        Program.Lock = true;
                                        ShowPrincipal("Lock");
                                    }
                                    else
                                    {
                                        Program.ChangeRoute = true;
                                        ShowPrincipal("Shift2");
                                    }
                                }
                                break;
                        }
                    }
                    else if (ActiveScreen == "Lock")
                    {
                        switch (controlEvent)
                        {
                            case "lbl2Der":
                               // ShowPrincipal("QuestionClose");
                                break;
                            case "lbl3Der":
                                //SetMensaje(3, "Unlock", 2);
                                ShowPrincipal("Shift1");
                                break;
                        }
                    }
                    else if (ActiveScreen == "QuestionClose")
                    {
                        switch (controlEvent)
                        {
                            case "lbl6Izq":
                                ShowPrincipal("Lock");
                                break;
                            case "lbl6Der":
                                if (SendClosed())
                                {
                                    Program.SetWiFiOn = true;
                                    Program.IgnoreMessages = false;
                                    Program.Lock = false;
                                    ShowClose();

                                }
                                else
                                {
                                    string Temp3 = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                    string Temp2 = oPrincipal.oResultadoMensaje.GetMessage("lbl2");
                                    SetMensaje(2, "Please", 2);
                                    SetMensaje(3, "Try Again", 2);
                                    System.Threading.Thread.Sleep(2000);
                                    SetMensaje(2, Temp2, 2);
                                    SetMensaje(3, Temp3, 2);
                                }

                                
                                break;
                        }
                    }
                    else if (ActiveScreen == "Brightness")
                    {
                        switch (controlEvent)
                        {
                            case "lbl6Izq":
                                ShowPrincipal("Special");
                                Classes.clsConfig.SaveConfig();
                                break;
                            case "lbl6Der":
                                ShowPrincipal("Special");
                                Classes.clsConfig.SaveConfig();
                                break;
                        }
                    }
                    else if (ActiveScreen == "Volume" || ActiveScreen == "Volume Farebox")
                    {
                        switch (controlEvent)
                        {
                            //case "lbl6Izq":
                            //    ShowPrincipal("Special");
                            //    Classes.clsConfig.SaveConfig();
                            //    break;
                            case "lbl6Der":
                                if (ActiveScreen == "Volume Farebox")
                                {
                                    byte[] Data1 = new byte[1];
                                    Data1[0] = (byte)Classes.clsVolumen.GetValueCommand(Program.FareboxVolumeTemp);
                                    if (Comando((byte)frmPrincipalAlt.ComandosConsolaFarebox.ProgramarVolumen, 1, Data1) == 1)
                                    {
                                        Program.FareboxVolume = Program.FareboxVolumeTemp;
                                    }
                                }
                                ShowPrincipal("Special");
                                Classes.clsConfig.SaveConfig();
                                break;
                        }
                    }
                }
                else if (ActiveControl == "Close")
                {
                    switch (controlEvent)
                    {
                        case "lbl1Izq":
                            //if (Program.Synching == 0 && Program.Service == 0)
                            //{
                                ShowPassword();
                            //}
                            //else
                            //{
                            //    SyncWarning();
                            //}
                            break;
                        case "lbl2Izq": //Manual Sync
                            timercerrado.Change(Timeout.Infinite, Timeout.Infinite);
                            ProcessClosed(new object());
                            break;
                        case "lbl5Izq":
                            //if(Program.VueltaAbierta!=-1)
                            //{
                            //oClose.SafeZoneDetect();
                            //}
                            break;
                        case "lbl6Der":
                            if(oClose.lbl6Der.Visible)
                            {
                                if (DateTime.Now.Subtract(Program.TimeStampOpenClose).TotalSeconds < 15)
                                {
                                    return;
                                }
                                OpenShift();
                            }
                            break;
                    }
                }
                else if (ActiveControl == "Settings1")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            oSettings1.timer1.Enabled = false;

                            if (BEAMDT.Program.VueltaAbierta == 1)
                            {

                                //Prin.ShowMainScreen();
                            }
                            else
                            {

                                ShowClose();
                                StartProcessClosed();
                            }
                            break;
                        case "lbl6Der":
                            oSettings1.timer1.Enabled = false;
                            ShowSettings2();
                            break;
                    }
                }
                else if (ActiveControl == "Settings2")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            ShowSettings1();
                            break;
                        case "lbl6Der":
                            ShowSettings3();
                            break;
                    }
                }
                else if (ActiveControl == "Settings3")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            ShowSettings2();
                            break;
                        case "lbl6Der":
                            break;
                    }
                }
                else if (ActiveControl == "SetValue")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            if (oSetValue.ValType == 1)//route
                            {
                                ShowSettings2();
                            }
                            else if (oSetValue.ValType == 2)//run
                            {
                                ShowSettings2();
                            }
                            break;
                        case "lbl6Der":
                            break;
                    }
                }
                else if (ActiveControl == "SetTimeFormat")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            ShowSettings1();
                            break;
                        case "lbl6Der":
                            if (oSetTimeFormat.lblValor.Text == "12H")
                            {
                                Classes.clsConfig.TimeFormat = 1;
                            }
                            else
                            {
                                Classes.clsConfig.TimeFormat = 0;
                            }
                            Classes.clsConfig.SaveConfig();
                            ShowSettings1();
                            break;
                    }
                }
                else if (ActiveControl == "SetDirection")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            ShowSettings2();
                            break;
                        case "lbl6Der":
                            break;
                    }
                }
                else if (ActiveControl == "SetAutoClose")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            ShowSettings3();
                            break;
                        case "lbl6Der":
                            Classes.clsConfig.AutoCloseTime = oSetAutoClose.Valor;
                            Classes.clsConfig.SaveConfig();
                            ShowSettings3();
                            break;
                    }
                }
                else if (ActiveControl == "QuestionEnaDis")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            if (oQuestionEnaDis.ValType == 1)//route
                            {
                                ShowSettings2();
                            }
                            else if (oQuestionEnaDis.ValType == 2)//run
                            {
                                ShowSettings2();
                            }
                            else //direction
                            {
                                ShowSettings2();
                            }
                            break;
                        case "lbl6Der":
                            break;
                    }
                }
                else if (ActiveControl == "ProgramBusNumber")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            ShowSetProgramBusData();
                            break;
                        case "lbl6Der":
                            break;
                    }
                }
                else if (ActiveControl == "ProgramBusDate")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            ShowSetProgramBusData();
                            break;
                        case "lbl6Der":
                            break;
                    }
                }
                else if (ActiveControl == "ProgramBusData")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            ShowSettings1();
                            break;
                        case "lbl6Der":
                            break;
                    }
                }
                else if (ActiveControl == "FTP1")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            oFtp1.inputPanel1.Enabled = false;
                            ShowSettings1();
                            break;
                        case "lbl6Der":
                            if (string.IsNullOrEmpty(oFtp1.txtFTPServer.Text))
                            {
                                oFtp1.lblResultado.Text = "Invalid Value";
                                System.Windows.Forms.Application.DoEvents();
                                System.Threading.Thread.Sleep(3000);
                                oFtp1.lblResultado.Text = "";
                                return;
                            }
                            oFtp1.inputPanel1.Enabled = false;
                            Classes.clsConfig.FTPServer = oFtp1.txtFTPServer.Text;
                            Classes.clsConfig.SaveConfig();
                            ShowFTP2();
                            break;
                    }
                }
                else if (ActiveControl == "FTP2")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            oFtp2.inputPanel1.Enabled = false;
                            ShowFTP1();
                            break;
                        case "lbl6Der":
                            if (string.IsNullOrEmpty(oFtp2.txtFTPUser.Text))
                            {
                                oFtp2.lblResultado.Text = "Invalid Value";
                                System.Windows.Forms.Application.DoEvents();
                                System.Threading.Thread.Sleep(3000);
                                oFtp2.lblResultado.Text = "";
                                return;
                            }
                            oFtp2.inputPanel1.Enabled = false;
                            Classes.clsConfig.User = oFtp2.txtFTPUser.Text;
                            Classes.clsConfig.SaveConfig();
                            ShowFTP3();
                            break;
                    }
                }
                else if (ActiveControl == "FTP3")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            oFtp3.inputPanel1.Enabled = false;
                            ShowFTP2();
                            break;
                        case "lbl6Der":
                            if (string.IsNullOrEmpty(oFtp3.txtFTPPassword.Text))
                            {
                                oFtp3.lblResultado.Text = "Invalid Value";
                                System.Windows.Forms.Application.DoEvents();
                                System.Threading.Thread.Sleep(3000);
                                oFtp3.lblResultado.Text = "";
                                return;
                            }
                            oFtp3.inputPanel1.Enabled = false;
                            Classes.clsConfig.PasswordFTP = oFtp3.txtFTPPassword.Text;
                            Classes.clsConfig.SaveConfig();
                            ShowSettings1();
                            break;
                    }
                }
                else if (ActiveControl == "Bluetooth1")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            ShowSettings1();
                            break;
                        case "lbl6Der":
                            if (oBluetooth1.dgDevices.CurrentRowIndex != -1)
                            {
                                Classes.clsConfig.BluetoothMAC = ((DataTable)oBluetooth1.dgDevices.DataSource).Rows[oBluetooth1.dgDevices.CurrentRowIndex].ItemArray[1].ToString();
                                Classes.clsConfig.SaveConfig();
                                ShowSettings1();
                            }
                            else
                            {
                                oBluetooth1.lblMessage.Text = "Please select a device";
                                Application.DoEvents();
                                System.Threading.Thread.Sleep(3000);
                                oBluetooth1.lblMessage.Text = "";
                            }
                            break;
                    }
                }
                else if (ActiveControl == "Password")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            if (oPassword.Option == 0)
                            {
                                if (BEAMDT.Program.VueltaAbierta == 1)
                                {
                                    ShowClose();
                                }
                                else
                                {
                                    Classes.clsUtils.SetShiftBrightness(false);
                                    // Prin.Comando(frmPrincipal.ACTIVAMENS, 0, new byte[1]);
                                    // Prin.ShowMessageEsp(1);
                                }
                            }
                            else
                            {
                                ShowSettings3();
                            }
                            break;
                    }
                }
                else if (ActiveControl == "SystemInfo")
                {
                    switch (controlEvent)
                    {
                        case "lbl6Izq":
                            ShowSettings3();
                            break;
                        case "lbl6Der":
                            break;
                    }
                }

            }
            else if (controlEvent.Contains("lblClass"))
            {
                if (ActiveControl == "Principal" && ActiveScreen == "Count")
                {
                    controlText = controlText.Replace("\n\r", " ");
                    Program.SaleDescription = controlText;
                    Program.MoneyTransaction = 0;
                    SetMensaje(3, controlText, 2);
                    BEAMDT.Program.CountDesc = controlText;
                    //Close
                }
                if (ActiveControl == "Principal" && ActiveScreen == "BillOverride")
                {
                    controlText = controlText.Replace("\n\r", " ");
                    SetMensaje(3, controlText, 2);
                }
                if (ActiveControl == "Principal" && ActiveScreen == "Sale")
                {
                    controlText = controlText.Replace("\n\r", " ");
                    Program.SaleDescription = controlText;
                    //Program.MoneyTransaction = 0;
                    SetMensaje(3, controlText, 2);
                    BEAMDT.Program.SaleDesc = controlText;
                }
                if (ActiveControl == "Principal" && ActiveScreen == "Open")
                {
                    if (Program.CoinsDisabled)
                    {
                        return;
                    }
                    Program.BotonEnabled = false;
                    controlText = controlText.Replace("\n\r", " ");
                    Program.MoneyTransaction = 0;
                    Data = new byte[1];
                    double fee = 1.0;
                    foreach (PassengerClass p in oTarifa.PassengersClass)
                    {
                        string Desc = "";
                        if (p.Renglon1 != "Null")
                        {
                            Desc = p.Renglon1;
                        }
                        if (p.Renglon2 != "Null")
                        {
                            Desc = Desc + " ";
                            Desc = Desc + p.Renglon2;
                        }
                        if (controlText == Desc)
                        {
                            Data[0] = (byte)p.ID;
                            fee = p.Fare;
                            break;
                        }
                    }
                    Program.SaleDescription = controlText + " fee: " + fee.ToString("$0.00");
                    Comando((byte)ComandosConsolaFarebox.ClaseMoneda, (byte)Data.Length, Data);
                }

            }
            else if (controlEvent.Contains("pic"))
            {
                switch (controlEvent)
                {
                    case "picOk":
                        if (ActiveControl == "Principal")
                        {



                        }
                        break;
                    case "picCancel":

                        break;
                    case "picCentro":

                        break;
                    case "picIzquierda":

                        break;
                    case "picDerecha":

                        break;
                    case "picArriba":
                        if (ActiveControl == "Principal" && ActiveScreen == "Open")
                        {
                            Program.ClassesIndex--;

                            oPrincipal.LoadPassengerClassButtons();
                        }
                        if (ActiveControl == "Principal" && ActiveScreen == "Count")
                        {
                            Program.ClassesIndex--;

                            oPrincipal.LoadPassengerClassButtons();
                        }
                        if (ActiveControl == "Principal" && ActiveScreen == "Sale")
                        {
                            Program.ProductIndex--;
                            oPrincipal.LoadSaleButtons();
                        }
                        break;
                    case "picAbajo":
                        if (ActiveControl == "Principal" && ActiveScreen == "Open")
                        {
                            Program.ClassesIndex++;
                            oPrincipal.LoadPassengerClassButtons();
                        }
                        if (ActiveControl == "Principal" && ActiveScreen == "Count")
                        {
                            Program.ClassesIndex++;
                            oPrincipal.LoadPassengerClassButtons();
                        }
                        if (ActiveControl == "Principal" && ActiveScreen == "Sale")
                        {
                            Program.ProductIndex++;
                            oPrincipal.LoadSaleButtons();
                        }
                        break;
                }
            }
            this.Focus();
        }
        public void OpenShift()
        {
            
            int ReturnVal = oRDR.LoadRDR();
            if (ReturnVal == -2)
            {
                SetMessageClosed("RDR File Exception");
                System.Threading.Thread.Sleep(5000);
                SetMessageClosed("");
                return;
            }
            else if (ReturnVal == -1)
            {
                SetMessageClosed("RDR File Not Found");
                System.Threading.Thread.Sleep(5000);
                SetMessageClosed("");
                return;
            }
            if (!LoadInboxFiles())
            {
                return;
            }
            Program.MenuOn = 1;
            Program.IgnoreMessages = true;
            ShowPrincipal("Shift1");
            
        }
        public void SyncWarning()
        {
            string Temp1 = oClose.lbl1.Text;
            string Temp2 = oClose.lbl2.Text;
            string Temp3 = oClose.lbl3.Text;
            string Temp4 = oClose.lbl4.Text;
            ContentAlignment Alin1 = oClose.lbl1.TextAlign;
            ContentAlignment Alin2 = oClose.lbl2.TextAlign;
            ContentAlignment Alin3 = oClose.lbl3.TextAlign;
            ContentAlignment Alin4 = oClose.lbl4.TextAlign;

            oClose.lbl1.TextAlign = ContentAlignment.TopCenter;
            oClose.lbl2.TextAlign = ContentAlignment.TopCenter;
            oClose.lbl3.TextAlign = ContentAlignment.TopCenter;
            oClose.lbl4.TextAlign = ContentAlignment.TopCenter;
            oClose.lbl1.Text = "";
            oClose.lbl2.Text = "Console";
            oClose.lbl3.Text = "Syncing With Farebox";
            oClose.lbl4.Text = "Please Wait";
            oClose.Update();
            System.Threading.Thread.Sleep(2000);
            oClose.lbl1.Text = Temp1;
            oClose.lbl2.Text = Temp2;
            oClose.lbl3.Text = Temp3;
            oClose.lbl4.Text = Temp4;
            oClose.lbl1.TextAlign = Alin1;
            oClose.lbl2.TextAlign = Alin2;
            oClose.lbl3.TextAlign = Alin3;
            oClose.lbl4.TextAlign = Alin4;
        }
        public int ReceiveFile()
        {
            try
            {
                int datasize = 8;
                byte[] data = new byte[datasize];
                fileConfimation = false;
                fileSuccess = 0;
                bool timeoutConf = false;
                if (Comando((byte)ComandosConsolaFarebox.SolicitudEnvioArchivo, 0, data) == 1)
                {
                    int Cont = 0;
                    while (!fileConfimation && !timeoutConf)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(10);
                        Cont++;
                        if (Cont == 200)
                        {
                            timeoutConf = true;
                        }
                        if (BEAMDT.Program.MenuOn != 0)
                        {
                            return 0;
                        }
                    }
                    if (timeoutConf)
                    {
                        return 2;
                    }
                    if (DatosComando[4] == 0)
                    {
                        return 0;
                    }
                    int FileSize = (int)DatosComandoDownload[5] + (int)(DatosComandoDownload[6] << 8) + (int)(DatosComandoDownload[7] << 16) + (int)(DatosComandoDownload[8] << 24);
                    int CRC = (int)DatosComandoDownload[9] + (int)(DatosComandoDownload[10] << 8);
                    int FileNameSize = (int)DatosComandoDownload[11];
                    string FileName = "";
                    for (int i = 0; i < FileNameSize; i++)
                    {
                        FileName = FileName + (char)DatosComandoDownload[12 + i];
                    }
                    serialPortFarebox.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFarebox_DataReceived);
                    //serialPortFarebox.ReceivedBytesThreshold = int.MaxValue;
                    
                    Xmodem.XmodemR xmodemreceive = new Xmodem.XmodemR(serialPortFarebox, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Outbox\\" + FileName);
                    int res = 0;
                    try
                    {
                        SetMessageClosed("Getting Transactions From Farebox");
                        res = xmodemreceive.Procesar_XmodemR();
                    }
                    catch
                    {
                        res = 1;
                    }

                    //serialPortFarebox.ReceivedBytesThreshold = 1;
                    serialPortFarebox.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFarebox_DataReceived);
                    if (Program.MenuOn != 0)
                    {
                        return 0;
                    }
                    byte[] ack = new byte[1];
                    int result = 0;
                    if (res == 0)
                    {
                        Xmodem.Crc16Ccitt crc16 = new Xmodem.Crc16Ccitt(Xmodem.InitialCrcValue.Zeros);
                        FileStream Fs = new FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Outbox\\" + FileName, FileMode.Open);
                        byte[] FileData = new byte[FileSize];
                        Fs.Read(FileData, 0, FileSize);
                        Fs.Close();

                        int CalcuCRC = crc16.computeCRC16C(FileData);
                        //int CalcuCRC = 0;
                        if (CalcuCRC != CRC)
                        {
                            System.IO.File.Delete(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Outbox\\" + FileName);
                            ack[0] = 1;
                        }
                        else
                        {
                            FileStream Fw = new FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Outbox\\" + FileName, FileMode.Create);
                            Fw.Write(FileData, 0, FileSize);
                            Fw.Flush();
                            Fw.Close();
                            ack[0] = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            System.IO.File.Delete(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Outbox\\" + FileName);
                        }
                        catch
                        {

                        }
                        ack[0] = 1;
                    }
                    if (ack[0] == 0)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 2;
                    }
                    Comando((byte)ComandosConsolaFarebox.RecepcionExitosaDeArchivo, 1, ack);
                    return result;
                }
                return 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }

        }


        public bool SendFile(string FilePath)
        {
            int datasize = 9;
            fileSuccess = 0;
            string filename = System.IO.Path.GetFileName(FilePath);
            datasize = datasize + filename.Length;

            System.IO.FileInfo fi = new FileInfo(FilePath);
            //System.IO.StreamReader sr = new StreamReader(FilePath);
            //string Data = sr.ReadToEnd();
            //sr.Close();

            Xmodem.Crc16Ccitt oCRC = new Xmodem.Crc16Ccitt(Xmodem.InitialCrcValue.Zeros);
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            byte[] fileData = new byte[fi.Length];

            fs.Read(fileData, 0, fileData.Length);
            fs.Close();

            int CRC = (int)oCRC.computeCRC16C(fileData);
            byte[] data = new byte[datasize];
            fileConfimation = false;
            fileSuccess = 0;
            if (Comando((byte)ComandosConsolaFarebox.AvisoEnvioArchivo, 0, data) == 1)
            {
                bool timeOutSend = false;
                int cont = 0;
                while (!fileConfimation && !timeOutSend)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                    cont++;
                    if (cont == 50)
                    {
                        timeOutSend = true;
                    }
                    if (BEAMDT.Program.MenuOn != 0)
                    {
                        //Classes.clsSpecialLogs.WriteLog("BEAMDT", "Cancel By menu");
                        return false;
                    }
                }
                if (timeOutSend)
                {
                    return false;
                }
                data[0] = 1;
                data[1] = (byte)(fileData.Length & 0xFF);
                data[2] = (byte)((fileData.Length >> 8) & 0xFF);
                data[3] = (byte)((fileData.Length >> 16) & 0xFF);
                data[4] = (byte)((fileData.Length >> 24) & 0xFF);
                data[5] = (byte)(CRC & 0xFF);
                data[6] = (byte)((CRC >> 8) & 0xFF);
                data[7] = (byte)filename.Length;
                int indexData = 8;
                for (int i = 0; i < filename.Length; i++)
                {
                    data[indexData] = (byte)filename[i];
                    indexData++;
                }

                if (Comando((byte)ComandosConsolaFarebox.EnvioArchivo, (byte)data.Length, data) == 1)
                {
                    timeOutSend = false;
                    cont = 0;
                    fileConfimation = false;
                    //System.Threading.Thread.Sleep(500);
                    Xmodem.XmodemT XmodemTrans = new Xmodem.XmodemT(serialPortFarebox, FilePath);
                    serialPortFarebox.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFarebox_DataReceived);
                    int res = 0;
                    try
                    {
                        res = XmodemTrans.Procesar_XmodemT();
                    }
                    catch
                    {
                        res = 1;
                    }

                    serialPortFarebox.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFarebox_DataReceived);
                    if (BEAMDT.Program.MenuOn != 0)
                    {
                        //Classes.clsSpecialLogs.WriteLog("BEAMDT", "Cancel By menu");
                        return false;
                    }
                    if (res != 0)
                    {
                        string ErrorMessage = "";
                        if (res == -2)
                        {
                            ErrorMessage = "XModem Abortado";
                        }
                        if (res == -1)
                        {
                            ErrorMessage = "XModem TimeOut";
                        }
                        //Classes.clsSpecialLogs.WriteLog("BEAMDT", ErrorMessage);
                        return false;
                    }
                    while (!fileConfimation && !timeOutSend)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(10);
                        cont++;
                        if (cont == 300)
                        {
                            timeOutSend = true;
                        }
                    }
                    if (timeOutSend)
                    {
                        //Classes.clsSpecialLogs.WriteLog("BEAMDT", "Timeout send");
                        return false;
                    }
                    if (fileSuccess == 1)
                    {
                        return true;
                    }
                    else
                    {
                        //Classes.clsSpecialLogs.WriteLog("BEAMDT", "No file success");
                        return false;
                    }
                }
            }
            return false;
        }
        object llock = new object();

        private void frmPrincipalAlt_KeyDown(object sender, KeyEventArgs e)
        {
            //this.KeyPreview = false;
            //oClose.textBox1.Focus();
            ProcessButton(e);

            //this.Focus();
            //this.KeyPreview = true;
            //// create the wrapper object, passing in the desired number of iterations
            //ThreadStartDelegateWrapper wrapper = new ThreadStartDelegateWrapper();
            //wrapper.e = e;
            //wrapper.Prin = this;
            //// create and start the thread
            //ThreadStart ts = new ThreadStart(wrapper.Worker);
            //Thread t = new Thread(ts);
            //t.Start();
        }
        bool fileConfimation = false;



        private delegate void ResultOpen(bool visible);
        private void OpenButtonVisible(bool visible)
        {
            if (oClose.picOpen.InvokeRequired)
            {
                oClose.picOpen.BeginInvoke(new ResultOpen(OpenButtonVisible), new object[] { visible });
            }
            else
            {
                if (!oClose.picOpen.Visible)
                {
                    oClose.picOpen.Visible = visible;
                    oClose.lbl6Der.Visible = visible;
                    System.Windows.Forms.Application.DoEvents();
                }
            }
        }
        private byte GetDevicesStatus()
        {
            return (byte)(statusDevices.Consola | statusDevices.Cashbox | statusDevices.PapelImpresora | statusDevices.Monedero | statusDevices.Billetero | statusDevices.Impresora | statusDevices.LectorRFID | statusDevices.LectorQRBarras);
        }
        private byte GetSensorStatus()
        {
            return (byte)(statusSensores.TapaSuperior | statusSensores.PuertaServicio | statusSensores.PuertaValores);
        }
        private List<string> GetDevicesMessages()
        {
            List<string> DevicesMessages = new List<string>();
            if (statusDevices.Monedero > 0)
            {
                DevicesMessages.Add("Coins");
            }
            if (statusDevices.Billetero > 0)
            {
                DevicesMessages.Add("Bills");
            }
            if (statusDevices.Impresora > 0)
            {
                DevicesMessages.Add("Printer");
            }
            if (statusDevices.LectorRFID > 0)
            {
                DevicesMessages.Add("RFID");
            }
            if (statusDevices.LectorQRBarras > 0)
            {
                DevicesMessages.Add("QRReader");
            }
            if (statusDevices.PapelImpresora > 0)
            {
                DevicesMessages.Add("Paper");
            }
            if (statusDevices.Consola > 0)
            {
                DevicesMessages.Add("Consola");
            }
            if (statusDevices.Cashbox > 0)
            {
                DevicesMessages.Add("Cashbox");
            }
            return DevicesMessages;
        }
        private List<string> GetSensorMessages()
        {
            List<string> SensorMessages = new List<string>();
            if (statusSensores.TapaSuperior > 0)
            {
                SensorMessages.Add("Top Door");
            }
            if (statusSensores.PuertaServicio > 0)
            {
                SensorMessages.Add("Service Door");
            }
            if (statusSensores.PuertaValores > 0)
            {
                SensorMessages.Add("Cash Door");
            }
            return SensorMessages;
        }
        private void SetDevicesStatus(byte devicestatus)
        {
            statusDevices.Monedero = (devicestatus & 1);
            statusDevices.Billetero = (devicestatus & 2);
            statusDevices.Impresora = (devicestatus & 4);
            statusDevices.LectorRFID = (devicestatus & 8);
            statusDevices.LectorQRBarras = (devicestatus & 16);
            statusDevices.PapelImpresora = (devicestatus & 32);
            statusDevices.Consola = (devicestatus & 64);
            statusDevices.Cashbox = (devicestatus & 128);
        }
        private void SetSensorsStatus(byte sensorstatus)
        {
            statusSensores.TapaSuperior = (sensorstatus & 1);
            statusSensores.PuertaServicio = (sensorstatus & 2);
            statusSensores.PuertaValores = (sensorstatus & 4);
        }
        private void serialPortFarebox_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] ack = { 2, 2, 1, 0, 0, 0, 0, 3 };
                if (serialPortFarebox.BytesToRead > 0)
                {
                    if (serialPortFarebox.ReadByte() == 2)
                    {
                        for (int iCont = 0; iCont < DatosComando.Length; iCont++)
                        {
                            DatosComando[iCont] = 0;
                        }

                        Receiving = true;
                        System.Threading.Thread.Sleep(10);
                        if (serialPortFarebox.BytesToRead > 0) // size
                        {
                            DatosComando[0] = (byte)serialPortFarebox.ReadByte();
                        }

                        if (serialPortFarebox.BytesToRead > 0) //tipo trama
                        {
                            DatosComando[1] = (byte)serialPortFarebox.ReadByte();
                        }
                        if (serialPortFarebox.BytesToRead > 0) //id commando
                        {
                            DatosComando[2] = (byte)serialPortFarebox.ReadByte();
                        }
                        //System.Threading.Thread.Sleep(10);
                        for (int iCont = 0; iCont < DatosComando[0]; iCont++)
                        {
                            if (serialPortFarebox.BytesToRead > 0)
                            {
                                DatosComando[iCont + 3] = (byte)serialPortFarebox.ReadByte();
                            }
                        }
                        byte Check = 0;
                        System.Threading.Thread.Sleep(20);
                        if (serialPortFarebox.BytesToRead > 0)
                        {
                            Check = (byte)serialPortFarebox.ReadByte();
                        }
                        byte End = 0;
                        if (serialPortFarebox.BytesToRead > 0)
                        {
                            End = (byte)serialPortFarebox.ReadByte();
                        }
                        if ((Check == checksum(DatosComando[0], 0, DatosComando)) && (End == 3) && DatosComando[1] == 0)
                        {
                            ack[3] = DatosComando[2];
                            ack[4] = DatosComando[3];
                            ack[5] = 0;
                            ack[6] = checksum(4, 1, ack);
                            serialPortFarebox.Write(ack, 0, ack.Length);
                            Receiving = false;
                            switch (DatosComando[3])
                            {
                                case (byte)ComandosFareboxConsola.CoinsDisable:
                                    if (DatosComando[4] == 1)
                                    {
                                        Program.CoinsDisabled = false;
                                    }
                                    else
                                    {
                                        Program.CoinsDisabled = true;
                                    }
                                    break;
                                case (byte)ComandosFareboxConsola.ApagarEquipo:
                                    Classes.clsReboot.PowerOff();
                                    break;
                                //Solicita estado de zona segura
                                case (byte)ComandosFareboxConsola.SolicitaZonaSegura:
                                    byte[] Dat = new byte[1];
                                    Dat[0] = (byte)Program.ZonaSegura;
                                    Comando((byte)ComandosConsolaFarebox.ZonaSegura, (byte)Dat.Length, Dat);
                                    break;
                                //Estatus dispositivos y sensores
                                case (byte)ComandosFareboxConsola.StatusDispositivos:
                                    SetDevicesStatus(DatosComando[4]);
                                    SetSensorsStatus(DatosComando[7]);
                                    if (DatosComando[4] > 0 || DatosComando[7] > 0)
                                    {
                                        StartProcessDispositivos();
                                    }

                                    break;
                                //Peticion del farebox para resincronizar el inicio
                                case (byte)ComandosFareboxConsola.Sincronizacion:
                                    if (Program.VueltaAbierta != -1)
                                    {
                                        if (Program.VueltaAbierta == 1)
                                        {
                                            System.Threading.Thread.Sleep(3000);
                                        }
                                        if (Program.Service == 1)
                                        {
                                            Program.PendingSync = true;
                                        }
                                        else
                                        {
                                            StartProcessInicio();
                                        }

                                    }
                                    break;

                                //Se abrio turno desde el sac usando tarjeta
                                case (byte)ComandosFareboxConsola.AbrirTurno:
                                    BEAMDT.Program.DriverID = (int)DatosComando[4] + (((int)DatosComando[5]) * 256) + (((int)DatosComando[6]) * 65536);
                                    int ReturnVal = oRDR.LoadRDR();
                                    if (ReturnVal == -2)
                                    {
                                        SetMessageClosed("RDR File Exception");
                                        System.Threading.Thread.Sleep(5000);
                                        SetMessageClosed("");
                                        return;
                                    }
                                    else if (ReturnVal == -1)
                                    {
                                        SetMessageClosed("RDR File Not Found");
                                        System.Threading.Thread.Sleep(5000);
                                        SetMessageClosed("");
                                        return;
                                    }
                                    if (!LoadInboxFiles())
                                    {
                                        return;
                                    }
                                    Program.MenuOn = 1;
                                    Program.IgnoreMessages = true;
                                    Program.OpenShiftFromCard = true;
                                    if (Classes.clsConfig.RouteEnabled)
                                    {
                                        if (!oRDR.BuscaRuta(Classes.clsConfig.DefaultRoute))
                                        {
                                            ShowPrincipal("Shift2");
                                        }
                                        else
                                        {
                                            if (Classes.clsConfig.RunEnabled)
                                            {
                                                if (!oRDR.BuscaCorrida(Classes.clsConfig.DefaultRoute, Classes.clsConfig.DefaultRun))
                                                {
                                                    Program.RouteNumber = Classes.clsConfig.DefaultRoute;
                                                    ShowPrincipal("Shift3");
                                                }
                                                else
                                                {
                                                    if (Classes.clsConfig.DirectionEnabled)
                                                    {
                                                        Program.Run = Classes.clsConfig.DefaultRun;
                                                        Program.RouteNumber = Classes.clsConfig.DefaultRoute;
                                                        Program.Direction = Classes.clsConfig.DefaultDirection;

                                                        if (SendOpen())
                                                        {
                                                            Program.SetWiFiOff = true;
                                                            Program.IgnoreMessages = false;
                                                            ShowPrincipal("Open", true);

                                                        }
                                                        else
                                                        {
                                                            string Temp3 = oPrincipal.oResultadoMensaje.GetMessage("lbl3");
                                                            string Temp2 = oPrincipal.oResultadoMensaje.GetMessage("lbl2");
                                                            SetMensaje(2, "Please", 2);
                                                            SetMensaje(3, "Try Again", 2);
                                                            System.Threading.Thread.Sleep(2000);
                                                            SetMensaje(2, Temp2, 2);
                                                            SetMensaje(3, Temp3, 2);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Program.RouteNumber = Classes.clsConfig.DefaultRoute;
                                                        Program.Run = Classes.clsConfig.DefaultRun;
                                                        ShowPrincipal("Shift4");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Program.RouteNumber = Classes.clsConfig.DefaultRoute;
                                                ShowPrincipal("Shift3");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ShowPrincipal("Shift2");
                                    }
                                    break;
                                //Se cerro turno desde el SAC utilizando tarjeta
                                case (byte)ComandosFareboxConsola.CerrarTurno:
                                    Program.BotonEnabled = true;
                                    Program.SecsToSync = 20;
                                    StartProcessClosed();
                                    Program.VueltaAbierta = 0;
                                    Program.TimeStampOpenClose = GetNow();
                                    Program.SetWiFiOn = true;
                                    Program.IgnoreMessages = false;
                                    Program.Lock = false;
                                    ShowClose();
                                    break;
                                case (byte)ComandosFareboxConsola.SolicitudInicioArchivo:
                                    fileConfimation = true;
                                    break;

                                case (byte)ComandosFareboxConsola.ResetConsola:
                                    //softReset();
                                    break;

                                case (byte)ComandosFareboxConsola.ListoEnviarArchivo:
                                    for (int iCont = 0; iCont < DatosComando.Length; iCont++)
                                    {
                                        DatosComandoDownload[iCont] = DatosComando[iCont];
                                    }
                                    fileConfimation = true;
                                    break;

                                //Llego un mensaje del SAC para desplegarlo en la pantalla de la
                                //navman
                                case (byte)ComandosFareboxConsola.EnviaMensaje:
                                    Program.Refresh = true;
                                    if (Program.VueltaAbierta == 1 && Classes.clsConfig.AutoCloseTime > 0)
                                    {
                                        timerCierraTurno.Change(Timeout.Infinite, Timeout.Infinite);
                                        int Interval=((((int)(Classes.clsConfig.AutoCloseTime / 4)) * 60) * 60000) + ((((int)(Classes.clsConfig.AutoCloseTime % 4)) * 15) * 60000);
                                        if (Interval > 0)
                                        {
                                            timerCierraTurno = new System.Threading.Timer(new System.Threading.TimerCallback(timerAutoClose_Tick), null, Interval, System.Threading.Timeout.Infinite);
                                        }
                                    }
                                    byte[] DatosMuestra = new byte[200];
                                    for (int iCont = 0; iCont < DatosComando.Length - 1; iCont++)
                                    {
                                        DatosMuestra[iCont] = DatosComando[iCont + 1];
                                    }
                                    if (DatosComando[4] == 0 || DatosComando[4] == 4)
                                    {
                                        for (int iCont = 0; iCont < DatosMuestra.Length; iCont++)
                                        {
                                            LastTransactionMoneda[iCont] = DatosMuestra[iCont];
                                        }
                                        if (LastTransactionMoneda[5] == 0 && LastTransactionMoneda[3] != 4 && Program.VueltaAbierta == 1)
                                        {
                                            Program.SaleDescription = "General fee: " + Program.GeneralFare.ToString("$0.00");
                                            Program.MoneyTransaction = 0;
                                            Program.SaleProcess = false;
                                            if (!Program.Lock)
                                            {
                                                Program.BotonEnabled = false;
                                            }
                                        }
                                        else if (LastTransactionMoneda[3] == 4)
                                        {
                                            Program.BotonEnabled = false;
                                            Program.MoneyTransaction++;
                                        }
                                        if (!Program.IgnoreMessages && Program.VueltaAbierta != -1 && !Program.Lock)
                                        {
                                            Program.SyncingFarebox = false;
                                            EscribeMensaje(DatosMuestra);
                                        }
                                        if (Program.VueltaAbierta == 0)
                                        {
                                            OpenButtonVisible(true);
                                        }
                                    }
                                    else
                                    {
                                        Program.FlagRecall = true;
                                        for (int iCont = 0; iCont < DatosMuestra.Length; iCont++)
                                        {
                                            LastDatos[iCont] = DatosMuestra[iCont];
                                        }
                                    }
                                    break;
                                case (byte)ComandosFareboxConsola.EnvioDatosTurno:
                                    DatosTurnoArrive = 1;
                                    if (DatosComando[4] == 0)
                                    {
                                        Program.VueltaAbierta = 0;
                                        DatosTurno = 0;
                                    }
                                    else
                                    {
                                        Program.VueltaAbierta = 1;
                                        DatosTurno = 1;
                                        Program.DriverID = (int)DatosComando[5] + (int)(DatosComando[6] << 8) + (int)(DatosComando[7] << 16);
                                        Program.RouteNumber = (int)DatosComando[8] + (int)(DatosComando[9] << 8);
                                        Program.Run = DatosComando[10];
                                        shiftOpen = true;
                                    }
                                    if (DatosComando[11] == 1)
                                    {
                                        Program.CoinByPass = "Coin Bypass On";
                                    }
                                    else
                                    {
                                        Program.CoinByPass = "";
                                    }

                                    if (GetSensorStatus() == 0 && GetDevicesStatus() == 0)
                                    {
                                        oPrincipal.SetMessageStatusDown(Program.CoinByPass);
                                    }

                                    Application.DoEvents();
                                    Program.BusNumber = (int)DatosComando[12] + (int)(DatosComando[13] << 8);
                                    Program.FareboxVersion = DatosComando[17].ToString() + "-" + DatosComando[16].ToString() + "-" + DatosComando[15].ToString() + "-" + DatosComando[14].ToString();
                                    Program.FareboxVolume = Classes.clsVolumen.GetVolumeFromCommandValue((int)DatosComando[18]);
                                    Program.FtAFBXVer = (int)DatosComando[19] + (int)(DatosComando[20] << 8);
                                    Program.FtBFBXVer = (int)DatosComando[21] + (int)(DatosComando[22] << 8);
                                    Program.HlFBXVer = (int)DatosComando[23] + (int)(DatosComando[24] << 8);
                                    Program.AlFBXVer = (int)DatosComando[25] + (int)(DatosComando[26] << 8);
                                    Program.PlFBXVer = (int)DatosComando[27] + (int)(DatosComando[28] << 8);
                                    Program.CfFBXVer = (int)DatosComando[29] + (int)(DatosComando[30] << 8);
                                    Program.MlFBXVer = (int)DatosComando[31] + (int)(DatosComando[32] << 8);
                                    Program.CurFtFBXVer = DatosComando[33];
                                    break;
                                //Manda la version del archivo RDR lo que significa que inmediatamente despues
                                //se transmitira el archivo RDR
                                case (byte)ComandosFareboxConsola.RecepcionExitosaDeArchivo:
                                    fileConfimation = true;
                                    if (DatosComando[4] == 0)
                                    {
                                        fileSuccess = 1;
                                    }
                                    else
                                    {
                                        fileSuccess = 2;
                                    }
                                    break;

                                //Recibe minuto y hora
                                case (byte)ComandosFareboxConsola.EnviaFechaHora:
                                    DatosFechaArrive = 1;
                                    //int ho = DatosComando[4];
                                    DateTime NewTime = new DateTime(DatosComando[4] + 2000, DatosComando[5], DatosComando[6], DatosComando[7], DatosComando[8], DatosComando[9]);
                                    if (!Program.GPSOK)
                                    {
                                        SynchTime(NewTime);
                                    }
                                    break;
                                case (byte)ComandosFareboxConsola.SolicitaDatosGPS:
                                    if (!Program.GPSOK)
                                    {
                                        Coordenada[11] = 100;
                                        Coordenada[0] = 91;
                                    }
                                    Comando((byte)ComandosConsolaFarebox.EnviaDatosGPS, 17, Coordenada);
                                    break;
                            }

                        }
                        else
                        {
                            Receiving = false;
                            bool noData = true;
                            for (int iCont = 0; iCont < DatosComando.Length; iCont++)
                            {
                                if (DatosComando[iCont] != 0)
                                {
                                    noData = false;
                                    break;
                                }
                            }
                            serialPortFarebox.DiscardInBuffer();
                            if (DatosComando[1] != 1 && !noData)
                            {
                                ack[3] = 0;
                                ack[4] = 0;
                                ack[5] = 1;
                                ack[6] = checksum(4, 1, ack);
                                serialPortFarebox.Write(ack, 0, ack.Length);
                                serialPortFarebox.DiscardInBuffer();
                            }
                        }
                    }
                    else
                    {
                        serialPortFarebox.DiscardInBuffer();
                    }

                }
            }
            catch (Exception ex)
            {
                Receiving = false;
                Program.BotonEnabled = true;
                //MessageBox.Show(ex.Message);
            }

        }
        string ActiveScreenGen = "";
        string ActiveControlGen = "";
        public void HandleButton(Control clicked)
        {

            string ActiveScreen = "";
            string ActiveControl = getActiveControl();
            if (ActiveControl == "Principal")
            {
                ActiveScreen = getActiveScreen();
            }
            ActiveScreenGen = ActiveScreen;
            string clickedtext = clicked.Text;
            if (ActiveScreen == "Open" || ActiveScreen == "Special" || ActiveScreen == "Sale1")
            {
                int currentfunction = 0;
                if (ActiveScreen == "Open" || ActiveScreen == "Sale1")
                {
                    foreach (BotonInterfaz b in oConfigInterfaz.ButtonsInterfaceMain)
                    {
                        string Desc = "";
                        if (b.Renglon1 != "Null")
                        {
                            Desc = b.Renglon1;
                        }
                        Desc = Desc + "\n\r";
                        if (b.Renglon2 != "Null")
                        {
                            Desc = Desc + b.Renglon2;
                        }
                        if (Desc == clickedtext)
                        {
                            currentfunction = b.Funcion;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (BotonInterfaz b in oConfigInterfaz.ButtonsInterfaceSpecial)
                    {
                        string Desc = "";
                        if (b.Renglon1 != "Null")
                        {
                            Desc = b.Renglon1;
                        }
                        Desc = Desc + "\n\r";
                        if (b.Renglon2 != "Null")
                        {
                            Desc = Desc + b.Renglon2;
                        }
                        if (Desc == clickedtext)
                        {
                            currentfunction = b.Funcion;
                            break;
                        }
                    }
                }
                string tempclickedtext = GetFunction(ActiveScreen, currentfunction);
                if (tempclickedtext != "")
                {
                    clickedtext = tempclickedtext;
                }
            }
            ActiveControlGen = ActiveControl;
            //OKCANCEL MAPPING JUST IN CASE
            //if (ActiveControl == "Principal" && (ActiveScreen == "Count" || ActiveScreen == "Count1" || ActiveScreen == "Sale" || ActiveScreen == "Sale1" || ActiveScreen == "Shift1" || ActiveScreen == "Shift2" || ActiveScreen == "Shift3" || ActiveScreen == "Shift4"))
            //{
            //    if (clicked.Name == "picOk")
            //    {
            //        clicked.Name = "lbl6Izq";
            //        clickedtext = "Acept";
            //    }
            //    if (clicked.Name == "picCancel")
            //    {
            //        clicked.Name = "lbl6Der";
            //        clickedtext = "Cancel";
            //    }
            //}
            //if (ActiveControl == "Principal" && ActiveScreen == "Special")
            //{
            //    if (clicked.Name == "picCancel")
            //    {
            //        clicked.Name = "lbl6Der";
            //        clickedtext = "Cancel";
            //    }
            //}
            ProcessEvent(ActiveControl, ActiveScreen, clicked.Name, clickedtext);
        }

        public bool Receiving = false;
        System.IO.Ports.SerialDataReceivedEventHandler ReciveEvent;
        private void CleanDataRecieveEvent()
        {
            int Count = 0;
            while (Count < 20)
            {
                serialPortFarebox.DataReceived -= ReciveEvent;
                Count++;
            }
        }
        private void SetDataReciveEvent()
        {
            serialPortFarebox.DataReceived += ReciveEvent;
        }
        public byte Comando(byte iComm, byte size, byte[] Datos)
        {
            System.Threading.ManualResetEvent CommandEvent = new ManualResetEvent(true);

            if (Receiving)
            {
                System.Threading.Thread.Sleep(100);
                //waitComando(100);
            }
            if (Receiving)///port still busy
            {
                return 0;
            }
            Program.Sending = true;
            CommandEvent.Reset();
            Program.IDCommand++;
            if (Program.IDCommand == 255)
            {
                Program.IDCommand = 1;
            }
            CleanDataRecieveEvent();
            try
            {

                //serialPortFarebox.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFarebox_DataReceived);
                //serialPortFarebox.ReceivedBytesThreshold = 10000;

                byte Reintentos;
                byte[] Bufr = new byte[9];
                byte[] Data = new byte[100];
                byte[] DataAck = new byte[100];
                byte Salir2;
                byte iCont;

                Reintentos = 0;
                Salir2 = 0;
                iCont = 0;
                Data[0] = 2;
                Data[1] = (byte)(size + 1);
                Data[2] = 0;
                Data[3] = Program.IDCommand;
                Data[4] = iComm;
                for (iCont = 0; iCont < size; iCont++)
                {
                    Data[iCont + 5] = Datos[iCont];
                }
                Data[5 + iCont] = checksum((byte)(size + 3), 1, Data);
                Data[5 + iCont + 1] = 3;
            Reenviar:
                serialPortFarebox.ReadExisting();
                Salir2 = 0;
                //for (iCont = 0; iCont < (size + 6); iCont++)
                //{
                //    //wait(1);
                //    Buf[0] = Data[iCont];
                //    serialPortFarebox.Write(Buf, 0, 1);
                //}
                serialPortFarebox.Write(Data, 0, size + 7);


                bool gotack = false;
                int ContWait = 0;
                while (Salir2 == 0)
                {
                    //waitComando(10);
                    System.Threading.Thread.Sleep(10);
                    if (serialPortFarebox.BytesToRead > 0)
                    {
                        if (serialPortFarebox.ReadByte() == 2)
                        {
                            ///waitComando(10);
                            if (serialPortFarebox.BytesToRead > 0) //size
                            {
                                DataAck[0] = (byte)serialPortFarebox.ReadByte();
                            }
                            if (serialPortFarebox.BytesToRead > 0) //tipo trama
                            {
                                DataAck[1] = (byte)serialPortFarebox.ReadByte();
                            }
                            if (serialPortFarebox.BytesToRead > 0) //id command
                            {
                                DataAck[2] = (byte)serialPortFarebox.ReadByte();
                            }
                            for (int iCont2 = 0; iCont2 < DataAck[0]; iCont2++)
                            {
                                if (serialPortFarebox.BytesToRead > 0)
                                {
                                    DataAck[iCont2 + 3] = (byte)serialPortFarebox.ReadByte();
                                }
                            }

                            //wait(4);
                            byte Check = 0;
                            if (serialPortFarebox.BytesToRead > 0)
                            {
                                Check = (byte)serialPortFarebox.ReadByte();
                            }
                            //wait(2);
                            byte End = 0;
                            if (serialPortFarebox.BytesToRead > 0)
                            {
                                End = (byte)serialPortFarebox.ReadByte();
                            }
                            if ((Check == checksum(DataAck[0], 0, DataAck)) && (End == 3) && DataAck[1] == 1)
                            {
                                gotack = true;
                            }
                            else
                            {
                                serialPortFarebox.DiscardInBuffer();
                            }
                        }
                        else
                        {
                            //waitComando(10);
                            System.Threading.Thread.Sleep(10);
                            serialPortFarebox.DiscardInBuffer();
                        }
                    }

                    if (gotack)
                    {
                        Salir2 = 1;
                        if (DataAck[4] == 0)
                        {
                            //serialPortFarebox.ReceivedBytesThreshold = 1;
                            Program.Sending = false;
                            CommandEvent.Set();
                            SetDataReciveEvent();
                            //serialPortFarebox.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFarebox_DataReceived);

                            if (iComm == (byte)ComandosConsolaFarebox.BusquedaFarebox)//BUSQUEDASAC
                            {
                                return 1;
                            }
                            if (iComm == (byte)ComandosConsolaFarebox.ActivaMensajes)//Activa Mens
                            {
                                return 1;
                            }
                            if (iComm == (byte)ComandosConsolaFarebox.DesactivaMensajes) //Desactiva Mens
                            {
                                return 1;
                            }
                            if (iComm == (byte)ComandosConsolaFarebox.AbrirTurno)//abre turno
                            {
                                if (Classes.clsConfig.AutoCloseTime > 0)
                                {
                                    //autocloseTimer.Enabled = false;
                                    //autocloseTimer.Interval = ((((int)(Classes.clsConfig.AutoCloseTime / 4)) * 60) * 60000) + ((((int)(Classes.clsConfig.AutoCloseTime % 4)) * 15) * 60000);
                                    //autocloseTimer.Enabled = true;
                                }
                                BEAMDT.Program.VueltaAbierta = 1;
                                return 1;
                            }
                            if (iComm == (byte)ComandosConsolaFarebox.CerrarTurno)//Cierra turno
                            {
                                if (Classes.clsConfig.AutoCloseTime > 0)
                                {
                                    //autocloseTimer.Enabled = false;
                                }
                                BEAMDT.Program.VueltaAbierta = 0;
                                return 1;
                            }
                            else
                            {

                                return 1;
                            }


                        }
                        else
                        {

                            Reintentos++;
                            //waitComando(10);
                            System.Threading.Thread.Sleep(10);
                            if (Reintentos < 3)
                            {
                                goto Reenviar;
                            }
                            else
                            {
                                //serialPortFarebox.ReceivedBytesThreshold = 1;
                                Program.Sending = false;
                                //serialPortFarebox.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFarebox_DataReceived);
                                SetDataReciveEvent();
                                CommandEvent.Set();
                                return 0;
                            }
                        }
                    }
                    ContWait++;
                    if (ContWait == 80)
                    {
                        Reintentos++;
                        //waitComando(10);
                        System.Threading.Thread.Sleep(10);
                        if (Reintentos < 3)
                        {
                            goto Reenviar;
                        }
                        else
                        {
                            //serialPortFarebox.ReceivedBytesThreshold = 1;
                            //serialPortFarebox.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFarebox_DataReceived);
                            SetDataReciveEvent();
                            CommandEvent.Set();
                            Program.Sending = false;
                            return 0;
                        }
                    }
                }

            }
            catch
            {

            }
            //serialPortFarebox.ReceivedBytesThreshold = 1;
            Program.Sending = false;
            //serialPortFarebox.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFarebox_DataReceived);
            SetDataReciveEvent();
            CommandEvent.Set();
            return 0;
        }
        bool waitflag = false;
        bool waitflagComm = false;
        private Object thisLock = new Object();
        private Object thisLock1 = new Object();
        private void timerwait_Tick(object sender)
        {
            lock (thisLock)
            {
                waitflag = true;
            }
        }

        private void timerwait_TickComm(object sender)
        {
            lock (thisLock1)
            {
                waitflagComm = true;
            }

        }
        private void timerclockGPS_Tick(object sender)
        {
            try
            {
                if (DateTime.Now.Subtract(LastGPS).TotalSeconds > 10)
                {
                    Program.NoGPS = true;
                    Program.GPSOK = false;
                    RMC = "";
                    GGA = "";
                    serialPortGPS.Write("AT$GPSLCL=1,17\r");
                }
                if (Program.NoGPS)
                {
                    if (DateTime.Now.Subtract(LastNoGPS).TotalSeconds > 60)
                    {
                        LastNoGPS = DateTime.Now;
                        Coordenada[11] = 100;
                        Coordenada[0] = 91;
                        if (Program.VueltaAbierta != -1 && !Program.SyncingFarebox && Program.Service == 0)
                        {
                            Comando((byte)ComandosConsolaFarebox.EnviaDatosGPS, 17, Coordenada);
                        }
                    }

                }
                else
                {
                    if ((Program.contTramas == 0 || Program.contTramas > 30) && Program.Service == 0 && Program.GPSOK)
                    {
                        Program.contTramas = 1;
                        Program.contTramasBAD = 1;
                        SynchSystemTime(UTCDatetime);
                        byte[] data = new byte[7];
                        data[0] = (byte)DateTime.Now.Day;
                        data[1] = (byte)DateTime.Now.Month;
                        data[2] = (byte)(DateTime.Now.Year - 2000);
                        data[3] = (byte)DateTime.Now.Hour;
                        data[4] = (byte)DateTime.Now.Minute;
                        data[5] = (byte)DateTime.Now.Second;
                        data[6] = (byte)DateTime.Now.DayOfWeek;
                        if (Program.VueltaAbierta != -1 && !Program.SyncingFarebox && Program.Service == 0)
                        {
                            Comando((byte)ComandosConsolaFarebox.ProgramaFechaHora, (byte)data.Length, data);
                            System.Threading.Thread.Sleep(2000);
                            Comando((byte)ComandosConsolaFarebox.EnviaDatosGPS, 17, Coordenada);
                        }
                    }
                    if ((Program.contTramasBAD == 0 || Program.contTramasBAD > 30) && Program.Service == 0 && !Program.GPSOK)
                    {
                        Program.contTramas = 1;
                        Program.contTramasBAD = 1;
                        if (Program.VueltaAbierta != -1 && !Program.SyncingFarebox && Program.Service == 0)
                        {
                            Coordenada[11] = 100;
                            Coordenada[0] = 91;
                            Comando((byte)ComandosConsolaFarebox.EnviaDatosGPS, 17, Coordenada);
                        }

                    }
                }


            }
            catch
            {

            }
            timerclockGPS = new System.Threading.Timer(new System.Threading.TimerCallback(timerclockGPS_Tick), null, 3000, System.Threading.Timeout.Infinite);
        }
        private void timerStatus_Tick(object sender)
        {
            try
            {
                CleanLogFolder();
                Classes.clsStatus oStatus = new BEAMDT.Classes.clsStatus();
                if (Program.Service == 0 && Program.VueltaAbierta != 1)
                {
                    //int FTPSync = 0;
                    //oStatus.ReadStatus(ref FTPSync);
                    if (Program.Synching == 1)
                    {
                        if (Program.LastSynching == 0)
                        {
                            Program.LastSynching = 1;
                        }
                        SetMessageClosed("Checking for FTP updates...");
                        System.Threading.Thread.Sleep(2000);
                        //oStatus.ReadStatus(ref FTPSync);
                        if (Program.Synching == 2)
                        {
                            SetMessageClosed("FTP Server Syncing...");
                        }
                    }
                    else if (Program.Synching == 2)
                    {
                        if (Program.LastSynching == 1)
                        {
                            Program.LastSynching = 2;
                        }
                        SetMessageClosed("FTP Server Syncing...");
                    }
                    else
                    {
                        if (Program.LastSynching == 1 || Program.LastSynching == 2)
                        {
                            Program.LastSynching = 0;
                        }
                        SetMessageClosed("");
                        Program.Synching = 0;
                        Get_Local_Files_Version();
                        if (Program.MlUpdate || Program.AlUpdate || Program.FtUpdate || Program.PlUpdate || Program.HlUpdate || Program.CfUpdate || Program.FbxUpdate)
                        {
                            timercerrado.Change(Timeout.Infinite, Timeout.Infinite);
                            ProcessClosed(this);
                        }


                    }
                }
                oPrincipal.SetMessageStatusUp();
                oStatus.WriteStatus();
                if (Program.VueltaAbierta == 1)
                {
                    Program.TimerStatus = 180000;
                }
                else
                {
                    Program.TimerStatus = 5000;
                }

            }
            catch
            {

            }
            StartTimerStatus(Program.TimerStatus);
        }
        private delegate void deleshowmessesp11(object sender);
        private DateTime GetNow()
        {
            SYSTEMTIME CurTime = new SYSTEMTIME();
            GetLocalTime(ref CurTime);
            return new DateTime(CurTime.wYear, CurTime.wMonth, CurTime.wDay, CurTime.wHour, CurTime.wMinute, CurTime.wSecond);
        }
        private void timerclock_Tick(object sender)
        {
            if (GetNow().Hour == 4 && GetNow().Minute == 10 && GetNow().Second == 30)
            {
                Classes.clsReboot.HardReset();
            }
            if (Program.VueltaAbierta != 1 && ((GetNow().Second % 5) == 0))
            {
                string wifistatus = Classes.clsNetworkStatus.IsConnected();
                if (wifistatus.Contains("Connected"))
                {
                    SetMessageWIFI("WIFI Status: " + wifistatus);
                }
                else
                {
                    SetMessageWIFI("WIFI Status: Not Connected");
                }
                //Classes.clsNetworkStatus.CheckSafeZone();
                //if (Program.ZonaSegura == 1)
                //{
                //    SetMessageSecure("Secure Zone Detected");
                //}
                //else
                //{
                //    SetMessageSecure("Secure Zone Not Detected");
                //}

            }
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new deleshowmessesp11(timerclock_Tick), new object[] { sender });
            }
            else
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
                                        if (cccc.Name == "lblTime")
                                        {
                                            if (Classes.clsConfig.TimeFormat == 1)
                                            {
                                                cccc.Text = GetNow().ToString("hh:mm tt");
                                            }
                                            else
                                            {
                                                cccc.Text = GetNow().ToString("HH:mm");
                                            }

                                        }
                                    }

                                }
                            }
                        }
                    }
                    else if (c.Name == "Close")
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc.Name == "lblTime")
                            {
                                if (Classes.clsConfig.TimeFormat == 1)
                                {
                                    cc.Text = GetNow().ToString("hh:mm tt");
                                }
                                else
                                {
                                    cc.Text = GetNow().ToString("HH:mm");
                                }
                            }
                        }
                    }
                }
                timerclock = new System.Threading.Timer(new System.Threading.TimerCallback(timerclock_Tick), null, 1000, System.Threading.Timeout.Infinite);
                //Application.DoEvents();
            }

        }
        public void StartTimer(int interval)
        {
            timerclock = new System.Threading.Timer(new System.Threading.TimerCallback(timerclock_Tick), null, interval, System.Threading.Timeout.Infinite);
        }

        public void StartTimerGPS(int interval)
        {
            timerclockGPS = new System.Threading.Timer(new System.Threading.TimerCallback(timerclockGPS_Tick), null, interval, System.Threading.Timeout.Infinite);
        }
        public void StartTimerStatus(int interval)
        {
            timerStatus = new System.Threading.Timer(new System.Threading.TimerCallback(timerStatus_Tick), null, interval, System.Threading.Timeout.Infinite);
        }
        public void StartProcessClosed()
        {
            if (!Program.ExecutingProcessClosed)
            {
                Program.ExecutingProcessClosed = true;
                timercerrado = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessClosed), null, Program.SecsToSync * 1000, System.Threading.Timeout.Infinite);
            }

        }
        public void StartProcessDispositivos()
        {
            if (!Program.ProcessDevices)
            {
                Program.ProcessDevices = true;
                timerprocessDispositivos = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessStatusDispositivos), null, 5000, System.Threading.Timeout.Infinite);
            }

        }
        //public void wait(int interval)
        //{
        //    waitflag = false;
        //    timerwait = new System.Threading.Timer(new System.Threading.TimerCallback(timerwait_Tick), null, interval, System.Threading.Timeout.Infinite);
        //    while (!waitflag)
        //    {
        //        Application.DoEvents();
        //    }
        //}

        public void waitComando(int interval)
        {
            waitflagComm = false;
            timerwaitComm = new System.Threading.Timer(new System.Threading.TimerCallback(timerwait_TickComm), null, interval, System.Threading.Timeout.Infinite);
            while (!waitflagComm)
            {
                Application.DoEvents();
            }
        }

        byte[] DatosComando = new byte[200];
        byte[] DatosComandoDownload = new byte[200];
        byte[] LastDatos = new byte[200];
        byte[] LastTransactionMoneda = new byte[200];
        byte[] Buf = new byte[1];
        byte checksum(byte iTamano, byte iInicio, byte[] datos)
        {
            byte iCont;
            byte Check;
            Check = datos[iInicio];
            for (iCont = (byte)(iInicio + 1); iCont <= iTamano + 2; iCont++)
            {
                Check = (byte)(Check ^ datos[iCont]);
            }
            return Check;
        }
        void EscribeMensaje(byte[] Dat)
        {
            if (Dat[5] == 0 && Dat[3] == 0 && Program.VueltaAbierta == 1 && ActiveScreenGen != "Open")
            {
                ShowPrincipal("Open");
            }

            //SetMensaje(1, "", 1);
            //SetMensaje(2, "", 1);
            //SetMensaje(3, "", 1);
            //SetMensaje(4, "", 1);
            //this.Refresh();
            // Message.SetMessage("", 0, 1);
            // Message.SetMessage("", 0, 2);
            //Message.SetMessage("", 0, 3);
            //Message.SetMessage("", 0, 4);

            string Depositado = "";
            byte iResultado;
            byte iCont, iLeds, iCont3, iBandCero, iCont2, iDigitos, iUbicacion, iDig, iBocina;
            byte iContTrama, iLongMens, iRenglon,iRenglones;
            byte Evento;
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
            iResultado = Dat[3];
            iLeds = Dat[5];
            //bocina
            iBocina = Dat[6];
            Evento = Dat[9];
            string ResultName = "";
            if (iLeds == 4)
            {
                ResultName = "ctrResultadoTrans";
            }
            else if (iLeds == 1)
            {
                ResultName = "ctrResultadoTransNeg";
            }
            else
            {
                ResultName = "ctrResultadoOpen";
            }
            if (ResultName != "")
            {
                SetMensajeScreen(1, "", 1, ResultName);
                SetMensajeScreen(2, "", 1, ResultName);
                SetMensajeScreen(3, "", 1, ResultName);
                SetMensajeScreen(4, "", 1, ResultName);
            }

            for (iCont3 = 0; iCont3 < 14; iCont3++)
            {
                Mensaje[iCont3] = 0;
            }
            switch (iResultado)
            {
                case 0: //Mensaje normal
                    string TextMessage = "";
                    iContTrama = 14;
                    for (iCont = 0; iCont < Dat[13]; iCont++)
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
                        SetMensajeScreen(iRenglon, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, ResultName);
                        TextMessage = TextMessage + System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length);
                        for (iCont3 = 0; iCont3 < 14; iCont3++)
                        {
                            Mensaje[iCont3] = 0;
                        }
                    }
                    if (!TextMessage.Contains("Printing...") && !TextMessage.Contains("Printed") && !TextMessage.Contains("Ticket"))
                    {
                        Program.SaleProcess = false;
                        Program.MoneyTransaction = 0;
                        Program.BotonEnabled = true;
                    }
                    break;

                case 1: //Mensaje Saldo dinero


                    iContTrama = 14;

                    for (iCont = 0; iCont < Dat[13]; iCont++)
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
                        SetMensajeScreen(iRenglon, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, ResultName);
                        for (iCont3 = 0; iCont3 < 14; iCont3++)
                        {
                            Mensaje[iCont3] = 0;
                        }
                    }

                    //iContTrama += 1;
                    Mensaje[iDig] = (byte)'$';
                    iDig++;
                    if ((Dat[iContTrama] == 0) && (Dat[iContTrama + 1] == 0) && (Dat[iContTrama + 2] == 0) && (Dat[iContTrama + 3] == 0))
                    {
                        Mensaje[1] = (byte)'0';
                        Mensaje[2] = (byte)'.';
                        Mensaje[3] = (byte)'0';
                        Mensaje[4] = (byte)'0';

                        SetMensajeScreen(3, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, ResultName);
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

                        SetMensajeScreen(3, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, ResultName);
                    }



                    break;

                case 2: //Mensaje fecha

                    iContTrama = 14;

                    for (iCont = 0; iCont < Dat[13]; iCont++)
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
                        SetMensajeScreen(iRenglon, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, ResultName);
                        for (iCont3 = 0; iCont3 < 14; iCont3++)
                        {
                            Mensaje[iCont3] = 0;
                        }
                    }
                    
                    iContTrama += 1;
                    Mensaje[0] = (byte)(48 + (Dat[iContTrama] >> 4));
                    Mensaje[1] = (byte)(48 + (Dat[iContTrama] & 0xF));
                    Mensaje[2] = 47;
                    Mensaje[3] = (byte)(48 + (Dat[iContTrama + 1] >> 4));
                    Mensaje[4] = (byte)(48 + (Dat[iContTrama + 1] & 0xF));
                    Mensaje[5] = 47;
                    Mensaje[6] = (byte)(48 + (Dat[iContTrama + 2] >> 4));
                    Mensaje[7] = (byte)(48 + (Dat[iContTrama + 2] & 0xF));

                    SetMensajeScreen(3, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, ResultName);
                    break;

                case 3: //Saldo viajes

                    iContTrama = 14;

                    for (iCont = 0; iCont < Dat[13]; iCont++)
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
                        SetMensajeScreen(iRenglon, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, ResultName);
                        for (iCont3 = 0; iCont3 < 14; iCont3++)
                        {
                            Mensaje[iCont3] = 0;
                        }
                    }
                    iContTrama += 2;
                    if ((Dat[iContTrama] == 0) && (Dat[iContTrama + 1] == 0))
                    {
                        Mensaje[0] = (byte)'0';

                        SetMensajeScreen(3, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, ResultName);
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
                        SetMensajeScreen(3, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, ResultName);
                    }
                    break;
                case 4: //Dinero depositado
                    if (Program.MoneyTransaction == 1)
                    {
                        SetMensajeScreen(1, "", 1, "ctrResultadoVenta");
                        SetMensajeScreen(2, "", 1, "ctrResultadoVenta");
                        SetMensajeScreen(3, "", 1, "ctrResultadoVenta");
                        SetMensajeScreen(4, "", 1, "ctrResultadoVenta");
                    }
                    else
                    {
                        SetMensajeScreen(2, "", 1, "ctrResultadoVenta");
                        SetMensajeScreen(4, "", 1, "ctrResultadoVenta");
                    }

                    iContTrama = 14;
                    for (iCont = 1; iCont <= Dat[13]; iCont++)
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
                        if (Program.MoneyTransaction == 1)
                        {
                            SetMensajeScreen(iRenglon, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, "ctrResultadoVenta");
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
                        Mensaje[2] = (byte)'.';
                        Mensaje[3] = (byte)'0';
                        Mensaje[4] = (byte)'0';
                        SetMensajeScreen(2, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, "ctrResultadoVenta");
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
                        Depositado = System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length);
                        if (!Program.Cancel)
                        {
                            SetMensajeScreen(2, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, "ctrResultadoVenta");
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
                        Mensaje[2] = (byte)'.';
                        Mensaje[3] = (byte)'0';
                        Mensaje[4] = (byte)'0';

                        SetMensajeScreen(4, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, "ctrResultadoVenta");
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
                        if (!Program.Cancel)
                        {
                            SetMensajeScreen(4, System.Text.Encoding.Default.GetString(Mensaje, 0, Mensaje.Length), iUbicacion, "ctrResultadoVenta");
                        }
                    }
                    for (iCont3 = 0; iCont3 < 14; iCont3++)
                    {
                        Mensaje[iCont3] = 0;
                    }
                    if (Program.MoneyTransaction > 0)
                    {
                        SetMensajeScreen(5, Program.SaleDescription, 2, "ctrResultadoVenta");
                    }
                    if (Program.Cancel)
                    {
                        Program.Cancel = false;
                        Program.SaleDescription = "General fee: " + Program.GeneralFare.ToString("$0.00");
                        double dineroDepositado;
                        if (Depositado == "")
                        {
                            dineroDepositado = 0;
                        }
                        else
                        {
                            dineroDepositado = double.Parse(Depositado.Substring(1));
                        }

                        double dineroFaltante = 0;
                        if (dineroDepositado > Program.GeneralFare)
                        {
                            dineroFaltante = 0;
                        }
                        else
                        {
                            dineroFaltante = Program.GeneralFare - dineroDepositado;
                        }
                        SetMensajeScreen(4, "$" + dineroFaltante.ToString("0.00"), iUbicacion, "ctrResultadoVenta");
                        SetMensajeScreen(2, "$" + dineroDepositado.ToString("0.00"), iUbicacion, "ctrResultadoVenta");
                        SetMensajeScreen(5, Program.SaleDescription, 2, "ctrResultadoVenta");
                    }

                    break;
            }

            if (iResultado == 0)
            {
                if (iLeds == 4)
                {
                    if (oPrincipal.CurrentResult != "ctrResultadoTrans")
                    {
                        oPrincipal.SetResultadoPositivo();
                    }

                }
                else if (iLeds == 1)
                {
                    if (oPrincipal.CurrentResult != "ctrResultadoTransNeg")
                    {
                        oPrincipal.SetResultadoNegativo();
                    }

                }
                else
                {
                    if (oPrincipal.CurrentResult != "ctrResultadoOpen")
                    {
                        oPrincipal.SetResultadoOpen();
                    }

                }

            }
            else if (iResultado == 1 || iResultado == 2 || iResultado == 3)
            {
                if (iLeds == 4)
                {
                    if (oPrincipal.CurrentResult != "ctrResultadoTrans")
                    {
                        oPrincipal.SetResultadoPositivo();
                    }

                }
                else
                {
                    if (oPrincipal.CurrentResult != "ctrResultadoTransNeg")
                    {
                        oPrincipal.SetResultadoNegativo();
                    }
                }

            }
            else if (iResultado == 4)
            {
                if (oPrincipal.CurrentResult != "ctrResultadoVenta")
                {
                    oPrincipal.SetResultadoVenta();
                }
            }
            if ((iBocina & 128) > 0)
            {
                Classes.SonidoFarebox SF = new BEAMDT.Classes.SonidoFarebox();
                SF.s32Reproducir_SonidoAsin((int)(iBocina & 127));
            }


        }

        private delegate void deleshowmessesp1(int Indice, String Mensaje, int Alineacion, string ResultName);
        private void SetMensajeScreen(int Indice, String Mensaje, int Alineacion, string ResultName)
        {
            string button = "";
            switch (Indice)
            {
                case 1: button = "lbl1";
                    break;
                case 2: button = "lbl2";
                    break;
                case 3: button = "lbl3";
                    break;
                case 4: button = "lbl4";
                    break;
                case 5: button = "lblProducto";
                    break;
            }
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new deleshowmessesp1(SetMensajeScreen), new object[] { Indice, Mensaje, Alineacion, ResultName });
            }
            else
            {
                foreach (Control c in this.Controls)
                {
                    if (c.Name == "Principal")
                    {
                        Screens.Principal oPrin = (BEAMDT.Screens.Principal)c;
                        switch (ResultName)
                        {
                            case "ctrResultadoMensaje":
                                oPrin.oResultadoMensaje.SetMessage(Mensaje, button, Alineacion);
                                return;
                            case "ctrResultadoMoneda":
                                oPrin.oResultadoMoneda.SetMessage(Mensaje, button, Alineacion);
                                return;
                            case "ctrResultadoOpen":
                                oPrin.oResultadoOpen.SetMessage(Mensaje, button, Alineacion);
                                return;
                            case "ctrResultadoTrans":
                                oPrin.oResultadoPositivo.SetMessage(Mensaje, button, Alineacion);
                                return;
                            case "ctrResultadoTransNeg":
                                oPrin.oResultadoNegativo.SetMessage(Mensaje, button, Alineacion);
                                return;
                            case "ctrResultadoVenta":
                                oPrin.oResultadoVenta.SetMessage(Mensaje, button, Alineacion);
                                return;

                        }

                    }
                    else if (c.Name == "Close")
                    {
                        Screens.Close oClose = (BEAMDT.Screens.Close)c;
                        oClose.SetMessage(Mensaje, button, Alineacion);
                    }
                }
            }

        }


        private delegate void deleshowmessesp(int Indice, String Mensaje, int Alineacion);
        private void SetMensaje(int Indice, String Mensaje, int Alineacion)
        {
            string button = "";
            switch (Indice)
            {
                case 1: button = "lbl1";
                    break;
                case 2: button = "lbl2";
                    break;
                case 3: button = "lbl3";
                    break;
                case 4: button = "lbl4";
                    break;
            }
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new deleshowmessesp(SetMensaje), new object[] { Indice, Mensaje, Alineacion });
            }
            else
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
                                    switch (ccc.Name)
                                    {
                                        case "ctrResultadoClosed":
                                            Screens.Resultados.ctrResultadoClosed oRes = (BEAMDT.Screens.Resultados.ctrResultadoClosed)ccc;
                                            oRes.SetMessage(Mensaje, button, Alineacion);
                                            return;
                                        case "ctrResultadoMensaje":
                                            Screens.Resultados.ctrResultadoMensaje oRes1 = (BEAMDT.Screens.Resultados.ctrResultadoMensaje)ccc;
                                            oRes1.SetMessage(Mensaje, button, Alineacion);
                                            return;
                                        case "ctrResultadoMoneda":
                                            Screens.Resultados.ctrResultadoMoneda oRes2 = (BEAMDT.Screens.Resultados.ctrResultadoMoneda)ccc;
                                            oRes2.SetMessage(Mensaje, button, Alineacion);
                                            return;
                                        case "ctrResultadoOpen":
                                            Screens.Resultados.ctrResultadoOpen oRes3 = (BEAMDT.Screens.Resultados.ctrResultadoOpen)ccc;
                                            oRes3.SetMessage(Mensaje, button, Alineacion);
                                            return;
                                        case "ctrResultadoTrans":
                                            Screens.Resultados.ctrResultadoTrans oRes4 = (BEAMDT.Screens.Resultados.ctrResultadoTrans)ccc;
                                            oRes4.SetMessage(Mensaje, button, Alineacion);
                                            return;
                                        case "ctrResultadoTransNeg":
                                            Screens.Resultados.ctrResultadoTransNeg oRes5 = (BEAMDT.Screens.Resultados.ctrResultadoTransNeg)ccc;
                                            oRes5.SetMessage(Mensaje, button, Alineacion);
                                            return;
                                        case "ctrResultadoVenta":
                                            Screens.Resultados.ctrResultadoVenta oRes6 = (BEAMDT.Screens.Resultados.ctrResultadoVenta)ccc;
                                            oRes6.SetMessage(Mensaje, button, Alineacion);
                                            return;

                                    }
                                }
                            }
                        }
                    }
                    else if (c.Name == "Close")
                    {
                        Screens.Close oClose = (BEAMDT.Screens.Close)c;
                        oClose.SetMessage(Mensaje, button, Alineacion);
                    }
                }
            }

        }

        private void frmPrincipalAlt_Closing(object sender, CancelEventArgs e)
        {
            serialPortGPS.Close();
            serialPortFarebox.Close();
            Program.UDPListen = false;
        }


        [DllImport("coredll.dll", SetLastError = true)]
        private extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);
        [DllImport("coredll.dll", SetLastError = true)]
        private extern static uint SetLocalTime(ref SYSTEMTIME lpSystemTime);
        [DllImport("coredll.dll", SetLastError = true)]
        private extern static uint GetLocalTime(ref SYSTEMTIME lpSystemTime);



        [DllImport("coredll.dll", SetLastError = true)]
        private extern static uint GetTimeZoneInformation(out TIME_ZONE_INFORMATION lpTimeZoneInformation);
        [DllImport("coredll.dll", SetLastError = true)]
        private extern static bool SetTimeZoneInformation(ref TIME_ZONE_INFORMATION lpTimeZoneInformation);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TIME_ZONE_INFORMATION
        {
            public int Bias;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string StandardName;
            public SYSTEMTIME StandardDate;
            public int StandardBias;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DaylightName;
            public SYSTEMTIME DaylightDate;
            public int DaylightBias;
        }

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

        public static void SetTimeZone()
        {
            object TimeZone = Registry.LocalMachine.OpenSubKey("Time", true).GetValue("TimeZoneInformation", "");
            TIME_ZONE_INFORMATION TI = new TIME_ZONE_INFORMATION();
            uint CurrentTimeConfig = GetTimeZoneInformation(out TI);
            int Hours = (Program.Offset & 0xF);
            int Min = ((Program.Offset >> 4) & 0x3);
            Min = Min * 15;
            int newBias = Min + (Hours * 60);
            int Resta = ((Program.Offset >> 6) & 0x64);
            if (Resta == 1)
            {
                newBias = newBias * -1;
            }
            //if (CurrentTimeConfig == 1) //Standard
            //{
            //    TI.StandardBias = newBias;
            //}
            //else if (CurrentTimeConfig == 2) //Daylight savings
            //{
            //    TI.DaylightBias = newBias;
            //}
            //else
            //{
            TI.Bias = newBias;
            //}
            bool result = SetTimeZoneInformation(ref TI);
        }

        public static void SynchTime(DateTime time)
        {
            try
            {
                SYSTEMTIME st = new SYSTEMTIME();
                st.wYear = (ushort)time.Year;
                st.wMonth = (ushort)time.Month;
                st.wDay = (ushort)time.Day;
                st.wDayOfWeek = (ushort)time.DayOfWeek;
                st.wHour = (ushort)time.Hour;
                st.wMinute = (ushort)time.Minute;
                st.wSecond = (ushort)time.Second;
                st.wMilliseconds = (ushort)time.Millisecond;
                uint result = SetLocalTime(ref st);
                System.Threading.Thread.Sleep(200);
                result = SetSystemTime(ref st);
                System.Threading.Thread.Sleep(200);
                result = SetLocalTime(ref st);
                System.Threading.Thread.Sleep(200);

            }
            catch
            {

            }
        }

        public static void SynchSystemTime(DateTime time)
        {
            try
            {
                SYSTEMTIME st = new SYSTEMTIME();
                st.wYear = (ushort)time.Year;
                st.wMonth = (ushort)time.Month;
                st.wDay = (ushort)time.Day;
                st.wDayOfWeek = (ushort)time.DayOfWeek;
                st.wHour = (ushort)time.Hour;
                st.wMinute = (ushort)time.Minute;
                st.wSecond = (ushort)time.Second;
                st.wMilliseconds = (ushort)time.Millisecond;
                uint result = SetSystemTime(ref st);

            }
            catch
            {

            }
        }


        private sList_File HL;
        private sList_File AL;
        private sList_File FT;
        private sList_File PL;
        private sList_File CF;
        private sList_File RDR;
        private sList_File FT_B;
        private int Version_HL = 0;
        private int Version_AL = 0;
        private int Version_FT = 0;
        private int Version_PL = 0;
        private int Version_CF = 0;
        private int Version_RDR = 0;
        private int Version_FT_B;

        struct sList_File
        {
            public string Local_Name;
            public int Local_Version;
            public long Local_Size;
            public string Remote_Name;
            public int Remote_Version;
            public long Remote_Size;
        }

        private void Get_Local_Files_Version_Init()
        {
            string FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Inbox\\";
            string[] Archivos;

            //Obtiene los datos de la Lista de MACs
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX ML*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Program.MlCurrentVer)
                    {
                        Program.MlCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                        //if (Classes.clsConfig.LastHLVersion != Program.HlCurrentVer)
                        //{
                        //    Program.HlUpdate = true;
                        //}
                    }
                }
            }
            else
            {
                Program.MlCurrentVer = 0;
            }
            
            //Obtiene los datos de la Lista Negra
            Version_HL = 0;
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX HL*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Program.HlCurrentVer)
                    {
                        Program.HlCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                        //if (Classes.clsConfig.LastHLVersion != Program.HlCurrentVer)
                        //{
                        //    Program.HlUpdate = true;
                        //}
                    }
                }
            }
            else
            {
                Program.HlCurrentVer = 0;
            }

            //Obtiene los datos de la Lista de Acciones
            Version_AL = 0;
            Array.Clear(Archivos, 0, Archivos.Length);
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX AL*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Version_AL)
                    {
                        Program.AlCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                        //if (Classes.clsConfig.LastALVersion != Program.AlCurrentVer)
                        //{
                        //    Program.AlUpdate = true;
                        //}

                    }
                }
            }
            else
            {
                Program.AlCurrentVer = 0;
            }

            //Obtiene los datos de la Lista de Productos
            Version_PL = 0;
            Array.Clear(Archivos, 0, Archivos.Length);
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX PL*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Version_PL)
                    {
                        Program.PlCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                        //if (Classes.clsConfig.LastPLVersion != Program.PlCurrentVer)
                        //{
                        //    Program.PlUpdate = true;
                        //}
                    }
                }
            }
            else
            {
                Program.PlCurrentVer = 0;
            }

            //Obtiene los datos de la Tabla de Tarifas
            Version_FT = 0;
            Array.Clear(Archivos, 0, Archivos.Length);
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX FT*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Version_FT)
                    {
                        Program.FtCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                        //if (Classes.clsConfig.LastFTVersion != Program.FtCurrentVer)
                        //{
                        //    Program.FtUpdate = true;
                        //}
                    }
                }
            }
            else
            {
                Program.FtCurrentVer = 0;
            }

            //Obtiene los datos del Archivo de Configuración 
            Version_CF = 0;
            Array.Clear(Archivos, 0, Archivos.Length);
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX CF*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Version_CF)
                    {
                        Program.CfCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                        //if (Classes.clsConfig.LastCFVersion != Program.CfCurrentVer)
                        //{
                        //    Program.CfUpdate = true;
                        //}
                    }
                }
            }
            else
            {
                Program.CfCurrentVer = 0;
            }

            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX RDR*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(8, 4), 16) > Program.HlCurrentVer)
                    {
                        Program.RDRVersion = Convert.ToInt32(FileName.Substring(8, 4), 16);
                        //if (Classes.clsConfig.LastHLVersion != Program.HlCurrentVer)
                        //{
                        //    Program.HlUpdate = true;
                        //}
                    }
                }
            }
            else
            {
                Program.RDRVersion = 0;
            }
        }


        public void Get_Local_Files_Version()
        {
            string FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Inbox\\";
            string[] Archivos;


            //Obtiene los datos de la Lista de MACs
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX ML*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Program.MlCurrentVer)
                    {
                        Program.MlCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                    }
                    if (Program.MlFBXVer != Program.MlCurrentVer)
                    {
                        Program.MlUpdate = true;
                    }
                }
            }
            else
            {
                Program.MlCurrentVer = 0;
            }

            //Obtiene los datos de la Lista Negra
            Version_HL = 0;
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX HL*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Program.HlCurrentVer)
                    {
                        Program.HlCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                    }
                    if (Program.HlFBXVer < Program.HlCurrentVer)
                    {
                        Program.HlUpdate = true;
                    }
                }
            }
            else
            {
                Program.HlCurrentVer = 0;
            }

            //Obtiene los datos de la Lista de Acciones
            Version_AL = 0;
            Array.Clear(Archivos, 0, Archivos.Length);
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX AL*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Version_AL)
                    {
                        Program.AlCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                    }
                    if (Program.AlFBXVer < Program.AlCurrentVer)
                    {
                        Program.AlUpdate = true;
                    }
                }
            }
            else
            {
                Program.AlCurrentVer = 0;
            }

            //Obtiene los datos de la Lista de Productos
            Version_PL = 0;
            Array.Clear(Archivos, 0, Archivos.Length);
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX PL*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Version_PL)
                    {
                        Program.PlCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                    }
                    if (Program.PlFBXVer < Program.PlCurrentVer)
                    {
                        Program.PlUpdate = true;
                    }
                }
            }
            else
            {
                Program.PlCurrentVer = 0;
            }

            //Obtiene los datos de la Tabla de Tarifas
            Version_FT = 0;
            Array.Clear(Archivos, 0, Archivos.Length);
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX FT*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Version_FT)
                    {
                        Program.FtCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                    }
                    if (Program.FtAFBXVer < Program.FtCurrentVer)
                    {
                        Program.FtUpdate = true;
                    }
                }
            }
            else
            {
                Program.FtCurrentVer = 0;
            }

            //Obtiene los datos del Archivo de Configuración 
            Version_CF = 0;
            Array.Clear(Archivos, 0, Archivos.Length);
            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX CF*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(7, 4), 16) > Version_CF)
                    {
                        Program.CfCurrentVer = Convert.ToInt32(FileName.Substring(7, 4), 16);
                    }
                    if (Program.CfFBXVer < Program.CfCurrentVer)
                    {
                        Program.CfUpdate = true;
                    }
                }
            }
            else
            {
                Program.CfCurrentVer = 0;
            }
            Array.Clear(Archivos, 0, Archivos.Length);
            Archivos = System.IO.Directory.GetFiles(FilePath, "FAREBOX*");
            if (Archivos.Length > 0)
            {
                Program.FbxUpdate = true;
            }

            Archivos = System.IO.Directory.GetFiles(FilePath, "FBX RDR*");
            if (Archivos.Length > 0)
            {
                int TotReg = Archivos.Length;
                for (int i = 0; i < TotReg; i++)
                {
                    FileInfo oFileInfo = new FileInfo(Archivos[i]);
                    string FileName = oFileInfo.Name;
                    if (Convert.ToInt32(FileName.Substring(8, 4), 16) > Program.RDRVersion)
                    {
                        Program.RDRVersion = Convert.ToInt32(FileName.Substring(8, 4), 16);
                        //if (Classes.clsConfig.LastHLVersion != Program.HlCurrentVer)
                        //{
                        //    Program.HlUpdate = true;
                        //}
                    }
                }
            }
            else
            {
                Program.RDRVersion = 0;
            }
        }

        // #endregion       
        //#endregion

        private void timerAutoClose_Tick(object sender)
        {
            ShowClose();
            byte[] Data = new byte[6];
            Data[0] = (byte)(Program.RouteNumber & 0xFF);
            Data[1] = (byte)((Program.RouteNumber >> 8) & 0xFF);
            Data[2] = (byte)(Program.DriverID & 0xFF);
            Data[3] = (byte)((Program.DriverID >> 8) & 0xFF);
            Data[4] = (byte)((Program.DriverID >> 16) & 0xFF);
            Data[5] = (byte)(Program.Run);
            Comando((byte)ComandosConsolaFarebox.CerrarTurno, (byte)Data.Length, Data);
            StartProcessClosed();
            Program.VueltaAbierta = 0;
        }
        private void ProcessButton(KeyEventArgs e)
        {
            if (Program.Service == 1)
            {
                return;
            }

            string picName = "";

            switch (e.KeyValue)
            {
                case 112://1
                    //if (!BotonIzq1)
                    //{
                    //    return;
                    //}
                    picName = "lbl1Izq";
                    break;
                case 113://2
                    //if (!BotonIzq2)
                    //{
                    //    return;
                    //}
                    picName = "lbl2Izq";

                    break;
                case 114://3
                    //if (!BotonIzq3)
                    //{
                    //    return;
                    //}
                    picName = "lbl3Izq";
                    break;
                case 115://2
                    //if (!BotonIzq4)
                    //{
                    //    return;
                    //}
                    picName = "lbl4Izq";
                    break;
                case 116://2
                    //if (!BotonIzq5)
                    //{
                    //    return;
                    //}
                    picName = "lbl5Izq";
                    break;
                case 117://2
                    //if (!BotonIzq6)
                    //{
                    //    return;
                    //}
                    picName = "lbl6Izq";

                    break;
                case 118://2
                    //if (!BotonDer1)
                    //{
                    //    return;
                    //}
                    picName = "lbl1Der";

                    break;
                case 119://2
                    //if (!BotonDer2)
                    //{
                    //    return;
                    //}
                    picName = "lbl2Der";
                    break;
                case 120://2
                    //if (!BotonDer3)
                    //{
                    //    return;
                    //}
                    picName = "lbl3Der";
                    break;
                case 121://2
                    //if (!BotonDer4)
                    //{
                    //    return;
                    //}
                    picName = "lbl4Der";
                    break;
                case 122://2
                    //if (!BotonDer5)
                    //{
                    //    return;
                    //}
                    picName = "lbl5Der";
                    break;
                case 123://2
                    //if (!BotonDer6)
                    //{
                    //    return;
                    //}
                    picName = "lbl6Der";
                    break;
                case 32:// verde
                    //if (!BotonOk)
                    //{
                    //    return;
                    //}
                    picName = "picOk";
                    //picName = "lbl6Izq";
                    break;
                case 27://rojo
                    //if (!BotonCancel)
                    //{
                    //    return;
                    //}
                    picName = "picCancel";
                    //picName = "lbl6Der";


                    break;
                case 13://centro
                    //if (!BotonCentro)
                    //{
                    //    return;
                    //}
                    //Program.ShowTaskbar();
                    //Cursor.Show();
                    //Application.Exit();
                    picName = "picCentro";
                    break;
                case 37://izq
                    //if (!BotonIzquierda)
                    //{
                    //    return;
                    //}
                    picName = "picIzquierda";
                    break;
                case 39://der
                    //if (!BotonDerecha)
                    //{
                    //    return;
                    //}
                    picName = "picDerecha";
                    break;
                case 38://arr
                    //if (!BotonArriba)
                    //{
                    //    return;
                    //}
                    picName = "picArriba";
                    break;
                case 40://aba
                    //if (!BotonAbajo)
                    //{
                    //    return;
                    //}
                    picName = "picAbajo";
                    break;
            }
            string controlText = "";
            if (picName.Contains("lbl"))
            {
                controlText = getLabelText(picName);
            }
            Control c = new Control();
            c.Name = picName;
            c.Text = controlText;
            HandleButton(c);
        }
        private void frmPrincipalAlt_KeyUp(object sender, KeyEventArgs e)
        {
            //if (DateTime.Now.Subtract(Program.ButtonTimeStamp).Seconds > 2)
            //{
            //    Classes.clsReboot.shutdownDevice();
            //}

        }
        private string[] splitGPS(string TramaGPS, string Separator)
        {
            List<string> splitData = new List<string>();
            while (TramaGPS.IndexOf(Separator) != -1)
            {
                splitData.Add(TramaGPS.Substring(0, TramaGPS.IndexOf(Separator)));
                TramaGPS = TramaGPS.Substring(TramaGPS.IndexOf(Separator) + Separator.Length, TramaGPS.Length - (TramaGPS.IndexOf(Separator) + Separator.Length));
            }
            splitData.Add(TramaGPS);
            return splitData.ToArray();
        }

        private int CountChar(string Cad, char c)
        {
            int count = 0;
            foreach (char cc in Cad)
            {
                if (c == cc)
                {
                    count++;
                }
            }
            return count;
        }
        private void serialPortGPS_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                DataGPS = DataGPS + serialPortGPS.ReadExisting();
                if (DataGPS.IndexOf("\r\n") != -1 && CountChar(DataGPS, '\n') >= 2)
                {
                    LastGPS = DateTime.Now;
                    if (DataGPS.Contains("AT-Command Interpreter ready"))
                    {
                        serialPortGPS.Write("AT$GPSLCL=1,17\r");
                        DataGPS = "";
                    }
                    if (DataGPS.Contains("$GPRMC") && DataGPS.Contains("$GPGGA"))
                    {
                        string[] tramas = splitGPS(DataGPS, "\r\n");
                        tramas[0] = tramas[0].Replace("\n", "");
                        tramas[1] = tramas[1].Replace("\n", "");

                        tramas[0] = tramas[0].Replace("\r", "");
                        tramas[1] = tramas[1].Replace("\r", "");
                        RMC = tramas[0];
                        GGA = tramas[1];
                        if (tramas.Length >= 2)
                        {
                            if (tramas[0].Length > 70 && tramas[1].Length > 70 && tramas[0].StartsWith("$GPRMC") && tramas[1].StartsWith("$GPGGA") && tramas[0].Substring(tramas[0].Length - 3, 1) == "*" && tramas[1].Substring(tramas[1].Length - 3, 1) == "*")
                            {
                                ProcesaTramaGPS();
                            }
                            else
                            {
                                Program.contTramasBAD++;
                                Program.GPSOK = false;
                            }
                            serialPortGPS.DiscardInBuffer();
                        }
                    }
                    DataGPS = "";
                }
            }
            catch
            {
                DataGPS = "";
            }
        }

        private bool ProcesaTramaGPS()
        {
            Program.NoGPS = false;
            getPos(RMC);
            getSat(GGA);
            SetPos();


            return true;
        }

        private void getPos(string innerRMC)
        {
            //$GPRMC,163155.00,A,2040.805176,N,10327.254883,W,0.0,255.8,060816,6.1,W,A*38
            string[] DataRMC = innerRMC.Split(',');
            try
            {
                UTCDatetime = new DateTime(int.Parse(DataRMC[9].Substring(4, 2)) + 2000, int.Parse(DataRMC[9].Substring(2, 2)), int.Parse(DataRMC[9].Substring(0, 2)), int.Parse(DataRMC[1].Substring(0, 2)), int.Parse(DataRMC[1].Substring(2, 2)), int.Parse(DataRMC[1].Substring(4, 2)));
            }
            catch
            {
                UTCDatetime = new DateTime(2000, 1, 1, 0, 0, 0);
            }
            Velocidad = DataRMC[7];
            Angulo = DataRMC[8];
            Longitd = DataRMC[5];
            Latitud = DataRMC[3];
            Lon = DataRMC[6];
            Lat = DataRMC[4];
            Valida = DataRMC[2];

        }

        private void SetPos()
        {

            /*FixQuality=1 
            UsedSat>3 
            Hdop<5% */
            if (Fix == 1 && Satelites > 3 && HDP <= 5 && Valida == "A")
            {
                Program.GPSOK = true;
                Program.contTramas++;
                Coordenada[0] = (byte)Lat[0];
                Coordenada[1] = byte.Parse(Latitud.Substring(0, 2));
                Coordenada[2] = byte.Parse(Latitud.Substring(2, 2));
                Coordenada[3] = (byte)(int.Parse(Latitud.Substring(5, 4)) & 0xFF);
                Coordenada[4] = (byte)((int.Parse(Latitud.Substring(5, 4)) >> 8) & 0xFF);
                Coordenada[5] = (byte)Lon[0];
                Coordenada[6] = byte.Parse(Longitd.Substring(0, 3));
                Coordenada[7] = byte.Parse(Longitd.Substring(3, 2));
                Coordenada[8] = (byte)(int.Parse(Longitd.Substring(6, 4)) & 0xFF);
                Coordenada[9] = (byte)((int.Parse(Longitd.Substring(6, 4)) >> 8) & 0xFF);
                Coordenada[10] = (byte)(float.Parse(Angulo)/30);
                Coordenada[11] = (byte)UTCDatetime.Day;
                Coordenada[12] = (byte)UTCDatetime.Month;
                Coordenada[13] = (byte)(UTCDatetime.Year - 2000);
                Coordenada[14] = (byte)UTCDatetime.Hour;
                Coordenada[15] = (byte)UTCDatetime.Minute;
                Coordenada[16] = (byte)UTCDatetime.Second;
            }
            else
            {
                Program.contTramasBAD++;
                Program.GPSOK = false;
            }
        }

        private void getSat(string innerGGA)
        {
            /*FixQuality=1 
            UsedSat>3 
            Hdop<5% */
            //$GPGGA,163155.00,2040.805176,N,10327.254883,W,1,05,2.4,1883.7,M,-18.1,M,,*64
            string[] DataGGA = innerGGA.Split(',');
            Satelites = int.Parse(DataGGA[7]);
            Fix = float.Parse(DataGGA[6]);
            HDP = float.Parse(DataGGA[8]);

        }

        private delegate void deleshowmessesp111(string Message);
        private void SetMessageClosed(string Message)
        {
            if (oClose.lblStatus.InvokeRequired)
            {
                this.BeginInvoke(new deleshowmessesp111(SetMessageClosed), new object[] { Message });
            }
            else
            {
                oClose.lblStatus.Text = Message;
                Application.DoEvents();
            }
        }


        private delegate void delesWIFI(string Message);
        private void SetMessageWIFI(string Message)
        {
            if (oClose.lblWifiStatus.InvokeRequired)
            {
                this.BeginInvoke(new delesWIFI(SetMessageWIFI), new object[] { Message });
            }
            else
            {
                oClose.lblWifiStatus.Text = Message;
                Application.DoEvents();
            }
        }
        private delegate void delesSecure(string Message);
        private void SetMessageSecure(string Message)
        {
            if (oClose.lblSecureZone.InvokeRequired)
            {
                this.BeginInvoke(new delesSecure(SetMessageSecure), new object[] { Message });
            }
            else
            {
                oClose.lblSecureZone.Text = Message;
                Application.DoEvents();
            }
        }

    }

}