using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BEAMDT.Screens
{
    public partial class Principal : UserControl
    {
        public string CurrentScreen="";
        public string CurrentResult = "";
        ctrDerecho oDerecho = new ctrDerecho();
        ctrIzquierdo oIzquierdo = new ctrIzquierdo();
        public Screens.Resultados.ctrResultadoAdjust oResultadoAdjust = new BEAMDT.Screens.Resultados.ctrResultadoAdjust();
        public Screens.Resultados.ctrResultadoMoneda oResultadoMoneda = new BEAMDT.Screens.Resultados.ctrResultadoMoneda();
        public Screens.Resultados.ctrResultadoVenta oResultadoVenta = new BEAMDT.Screens.Resultados.ctrResultadoVenta();
        public Screens.Resultados.ctrResultadoTransNeg oResultadoNegativo = new BEAMDT.Screens.Resultados.ctrResultadoTransNeg();
        public Screens.Resultados.ctrResultadoTrans oResultadoPositivo = new BEAMDT.Screens.Resultados.ctrResultadoTrans();
        public Screens.Resultados.ctrResultadoOpen oResultadoOpen = new Screens.Resultados.ctrResultadoOpen();
        public Screens.Resultados.ctrResultadoMensaje oResultadoMensaje = new Screens.Resultados.ctrResultadoMensaje();
        Teclados.ctrTecladoNumerico oTecladoNumerico = new BEAMDT.Screens.Teclados.ctrTecladoNumerico();
        Teclados.ctrTecladoClases oTecladoClass = new BEAMDT.Screens.Teclados.ctrTecladoClases();
        
        public Control Izquierdo
        {
            set 
            {
                panelIzquierdo.Controls.Clear();
                if (value != null)
                {
                    value.Parent = this.Parent;
                    panelIzquierdo.Controls.Add(value);
                }
            }
            get 
            {
                if (panelIzquierdo.Controls.Count > 0)
                {
                    return panelIzquierdo.Controls[0];
                }
                else
                {
                    return null;
                }
            }
        }
        public Control Derecho
        {
            set
            {
                panelDerecho.Controls.Clear();
                if (value != null)
                {
                    value.Parent = this.Parent;
                    panelDerecho.Controls.Add(value);

                }
                

            }
            get
            {
                if (panelDerecho.Controls.Count > 0)
                {
                    return panelDerecho.Controls[0];
                }
                else
                {
                    return null;
                }
            }
        }
        private delegate void ResultDele(Control p,Control c);
        private void setResult(Control p, Control c)
        {
            p.Controls.Clear();
            if (c != null)
            {
                c.Parent = this.Parent;
                p.Controls.Add(c);
                CurrentResult = c.Name;

            }
        }
        public Control Result
        {
            set
            {
                if (panelArriba.InvokeRequired)
                {
                    this.BeginInvoke(new ResultDele(setResult), new object[] {panelArriba, value });
                }
                else
                {
                    panelArriba.Controls.Clear();
                    if (value != null)
                    {
                        value.Parent = this.Parent;
                        panelArriba.Controls.Add(value);
                        CurrentResult = value.Name;
                    }
                }
            }
            get
            {
                if (panelArriba.Controls.Count > 0)
                {
                    return panelArriba.Controls[0];
                }
                else
                {
                    return null;
                }
            }
        }
        public Control Teclado
        {
            set
            {
                panelAbajo.Controls.Clear();
                if (value != null)
                {
                    value.Parent = this.Parent;
                    panelAbajo.Controls.Add(value);
                }
                

            }
            get
            {
                if (panelAbajo.Controls.Count > 0)
                {
                    return panelAbajo.Controls[0];
                }
                else
                {
                    return null;
                }
            }
        }
        public Control StatusUp
        {
            set
            {
                panelInfoRuta.Controls.Clear();
                if (value != null)
                {
                    value.Parent = this.Parent;
                    panelInfoRuta.Controls.Add(value);
                }
                

            }
            get
            {
                if (panelInfoRuta.Controls.Count > 0)
                {
                    return panelInfoRuta.Controls[0];
                }
                else
                {
                    return null;
                }
            }
        }
        public Control StatusDown
        {
            set
            {
                PanelNotification.Controls.Clear();
                if (value != null)
                {
                    value.Parent = this.Parent;
                    PanelNotification.Controls.Add(value);
                }
                

            }
            get
            {
                if (PanelNotification.Controls.Count > 0)
                {
                    return PanelNotification.Controls[0];
                }
                else
                {
                    return null;
                }
            }
        }
        public void SetResultadoMonedas()
        {
            Result = oResultadoMoneda;       }
        public void SetResultadoPositivo()
        {
            Result = oResultadoPositivo;
        }
        public void SetResultadoNegativo()
        {
            Result = oResultadoNegativo;
        }
        public void SetResultadoOpen()
        {
            Result = oResultadoOpen;
        }
        public void SetResultadoMensaje()
        {
            Result = oResultadoMensaje;
        }
        public void SetResultadoVenta()
        {
            Result = oResultadoVenta;
        }
        private void InitPanels()
        {
            //oDerecho.SetVisible(false, "pic1Der");
            //oDerecho.SetVisible(false, "pic2Der");
            //oDerecho.SetVisible(false, "pic3Der");
            //oDerecho.SetVisible(false, "pic4Der");
            //oDerecho.SetVisible(false, "pic5Der");
            //oDerecho.SetVisible(false, "pic6Der");
            oDerecho.SetEnabled(true, "lbl1Der");
            oDerecho.SetEnabled(true, "lbl2Der");
            oDerecho.SetEnabled(true, "lbl3Der");
            oDerecho.SetEnabled(true, "lbl4Der");
            oDerecho.SetEnabled(true, "lbl5Der");
            oDerecho.SetEnabled(true, "lbl6Der");

            oDerecho.SetMessage("", "lbl1Der");
            oDerecho.SetMessage("", "lbl2Der");
            oDerecho.SetMessage("", "lbl3Der");
            oDerecho.SetMessage("", "lbl4Der");
            oDerecho.SetMessage("", "lbl5Der");
            oDerecho.SetMessage("", "lbl6Der");

            //oIzquierdo.SetVisible(false, "pic1Izq");
            //oIzquierdo.SetVisible(false, "pic2Izq");
            //oIzquierdo.SetVisible(false, "pic3Izq");
            //oIzquierdo.SetVisible(false, "pic4Izq");
            //oIzquierdo.SetVisible(false, "pic5Izq");
            //oIzquierdo.SetVisible(false, "pic6Izq");
            oIzquierdo.SetEnabled(true, "lbl1Izq");
            oIzquierdo.SetEnabled(true, "lbl2Izq");
            oIzquierdo.SetEnabled(true, "lbl3Izq");
            oIzquierdo.SetEnabled(true, "lbl4Izq");
            oIzquierdo.SetEnabled(true, "lbl5Izq");
            oIzquierdo.SetEnabled(true, "lbl6Izq");

            oIzquierdo.SetMessage("", "lbl1Izq");
            oIzquierdo.SetMessage("", "lbl2Izq");
            oIzquierdo.SetMessage("", "lbl3Izq");
            oIzquierdo.SetMessage("", "lbl4Izq");
            oIzquierdo.SetMessage("", "lbl5Izq");
            oIzquierdo.SetMessage("", "lbl6Izq");

            oTecladoClass.SetVisible(false, "lblClass1");
            oTecladoClass.SetVisible(false, "lblClass2");
            oTecladoClass.SetVisible(false, "lblClass3");
            oTecladoClass.SetVisible(false, "lblClass4");
            oTecladoClass.SetVisible(false, "lblClass5");
            oTecladoClass.SetVisible(false, "lblClass6");
            oTecladoClass.SetVisible(false, "lblClass7");
            oTecladoClass.SetVisible(false, "lblClass8");
            oTecladoClass.SetVisible(false, "lblClass9");
            oTecladoClass.SetVisible(false, "lblClass10");
            //oTecladoClass.SetVisible(false, "pic1");
            //oTecladoClass.SetVisible(false, "pic2");
            //oTecladoClass.SetVisible(false, "pic3");
            //oTecladoClass.SetVisible(false, "pic4");
            //oTecladoClass.SetVisible(false, "pic5");
            //oTecladoClass.SetVisible(false, "pic6");
            //oTecladoClass.SetVisible(false, "pic7");
            //oTecladoClass.SetVisible(false, "pic8");
            //oTecladoClass.SetVisible(false, "pic9");
            //oTecladoClass.SetVisible(false, "pic10");

            Izquierdo = null;
            Derecho = null;
            Result = null;
            Teclado = null;

        }
        public Principal()
        {
            InitializeComponent();
            StatusDown = new ctrEstatusDown();
            StatusUp = new ctrEstatusUp();
        }
        private delegate void deleshowmessesp(string Message);
        public void SetMessageStatusDown(string Message)
        {
            if (PanelNotification.InvokeRequired)
            {
                this.BeginInvoke(new deleshowmessesp(SetMessageStatusDown), new object[] { Message });
            }
            else
            {
                if (PanelNotification.Controls.Count > 0)
                {
                    ctrEstatusDown oED = (ctrEstatusDown)PanelNotification.Controls[0];
                    oED.SetMessage(Message, "lblStatus");
                }
            }
        }
        public void SetMessageStatusUp(String Message)
        {
            if (panelInfoRuta.Controls.Count > 0)
            {
                ctrEstatusUp oEU = (ctrEstatusUp)panelInfoRuta.Controls[0];
                oEU.SetMessage(Message, "lblStatus");
            }
        }
        private delegate void deleMessageStatusUp();
        public void SetMessageStatusUp()
        {
            if (panelInfoRuta.InvokeRequired)
            {
                this.BeginInvoke(new deleMessageStatusUp(SetMessageStatusUp), new object[] {  });
            }
            else 
            {
                if (panelInfoRuta.Controls.Count > 0)
                {
                    ctrEstatusUp oEU = (ctrEstatusUp)panelInfoRuta.Controls[0];
                    if (Program.Lock)
                    {
                        oEU.SetMessage(DateTime.Now.ToString("MM/dd/yyyy"), "lblStatus");
                    }
                    else
                    {
                        oEU.SetMessage(GetMessageStatus(), "lblStatus");
                    }
                }
            }
        }
        private string GetMessageStatus()
        {
            string Fecha = DateTime.Now.ToString("MM/dd/yyyy");
            //return Fecha + " Driver ID:" + Program.DriverID.ToString() + " Route:" + Program.RouteNumber.ToString();
            return Fecha + " Route:" + Program.RouteNumber.ToString();
        }
        public void LoadPassengerClassButtons()
        {
            List<frmPrincipalAlt.PassengerClass> ClassList = new List<frmPrincipalAlt.PassengerClass>();
            ClassList = Prin.oTarifa.PassengersClass;
            if (ClassList.Count == 0)
            {
                return;
            }
            int MaxIndex = (int)(ClassList.Count / 10);
            if (ClassList.Count % 10 == 0)
            {
                MaxIndex--;
            }
            if (Program.ClassesIndex < 0)
            {
                Program.ClassesIndex = 0;
            }
            if (Program.ClassesIndex > MaxIndex)
            {
                Program.ClassesIndex = MaxIndex;
            }
            
            int ContPass = 0;
            for (int i = (Program.ClassesIndex * 10); i < ClassList.Count; i++)
            {
                oTecladoClass.SetVisible(true, "lblClass" + (ReturnNewClassButtonOrder(ContPass) + 1).ToString());
                string Desc = "";
                if (ClassList[i].Renglon1 != "Null")
                {
                    Desc = ClassList[i].Renglon1;
                }
                if (ClassList[i].Renglon2 != "Null")
                {
                    Desc = Desc + "\n\r";
                    Desc = Desc + ClassList[i].Renglon2;
                }
                oTecladoClass.SetMessageButton(Desc, "lblClass" + (ReturnNewClassButtonOrder(ContPass) + 1).ToString());
                oTecladoClass.SetEnabled(ClassList[i].Enabled, "lblClass" + (ReturnNewClassButtonOrder(ContPass) + 1).ToString());
                ContPass++;
                if (ContPass == 10)
                {
                    break;
                }
            }
            if (ContPass < 10)
            {
                for (int i = ContPass; i < 10; i++)
                {
                    oTecladoClass.SetMessageButton("", "lblClass" + (ReturnNewClassButtonOrder(i) + 1).ToString());
                    oTecladoClass.SetEnabled(false, "lblClass" + (ReturnNewClassButtonOrder(i) + 1).ToString());
                }
                
            }
        }
        public void LoadSaleButtons()
        {
            if (Prin.oProducts.Products.Count == 0)
            {
                return;
            }
            int MaxIndex = (int)(Prin.oProducts.Products.Count / 10);
            if (Prin.oProducts.Products.Count % 10 == 0)
            {
                MaxIndex--;
            }
            if (Program.ProductIndex < 0)
            {
                Program.ProductIndex = 0;
            }
            if (Program.ProductIndex > MaxIndex)
            {
                Program.ProductIndex = MaxIndex;
            }
            
            int ContPass = 0;
            for (int i = (Program.ProductIndex*10); i < Prin.oProducts.Products.Count;i++)
            {
                oTecladoClass.SetVisible(true, "lblClass" + (ReturnNewClassButtonOrder(ContPass) + 1).ToString());
                oTecladoClass.SetMessageButton(Prin.oProducts.Products[i].Description, "lblClass" + (ReturnNewClassButtonOrder(ContPass) + 1).ToString());
                oTecladoClass.SetEnabled(true, "lblClass" + (ReturnNewClassButtonOrder(ContPass) + 1).ToString());
                ContPass++;
                if (ContPass == 10)
                {
                    break;
                }
            }
            if (ContPass < 10)
            {
                for (int i = ContPass; i < 10; i++)
                {
                    oTecladoClass.SetEnabled(false, "lblClass" + (ReturnNewClassButtonOrder(i) + 1).ToString());
                    oTecladoClass.SetMessageButton("", "lblClass" + (ReturnNewClassButtonOrder(i) + 1).ToString());
                }

            }
            
        }

        private int ReturnNewClassButtonOrder(int Index)
        {
            if (Index < 5)
            {
                return Index;
            }
            else 
            {
                switch (Index)
                { 
                    case 5:
                        return 9;
                    case 6:
                        return 8;
                    case 7:
                        return 7;
                    case 8:
                        return 6;
                    case 9:
                        return 5;
                }
            }
            return 0;
        }
        private void LoadBillList()
        {
            //Load the bill list
            for (int i = 0; i < Prin.oConfigInterfaz.AllowedBills.Count;i++)
            {
                oTecladoClass.SetVisible(true, "lblClass" + (ReturnNewClassButtonOrder(i) + 1).ToString());
                oTecladoClass.SetMessageButton(Prin.oConfigInterfaz.AllowedBills[i].Description, "lblClass" + (ReturnNewClassButtonOrder(i) + 1).ToString());
                oTecladoClass.SetEnabled(Prin.oConfigInterfaz.AllowedBills[i].Enabled, "lblClass" + (ReturnNewClassButtonOrder(i) + 1).ToString());
            }
        }
        public void ResetScreen()
        {
            oResultadoOpen.SetMessage("", "lbl1", 2);
            oResultadoOpen.SetMessage("Waiting...", "lbl2", 2);
            oResultadoOpen.SetMessage("", "lbl3", 2);
            oResultadoOpen.SetMessage("", "lbl4", 2);
        }
        public frmPrincipalAlt Prin;
        public void Inicio(String ScreenToLoad)
        {
            Prin.Focus();
            string LastResult="";
            if (Result != null)
            {
                LastResult = Result.Name;
            }
            if (CurrentScreen == ScreenToLoad && ScreenToLoad != "Shift1" && ScreenToLoad != "Shift2" && ScreenToLoad != "Shift3" && ScreenToLoad != "Shift4")
            {
                return;
            }
            Debug.WriteLine("Load Screen: " + ScreenToLoad);
            InitPanels();
            CurrentScreen = ScreenToLoad;
            
            
            if (ScreenToLoad == "Open")
            {
                Teclado = oTecladoClass;
                ////oResultadoOpen.SetMessage("","lbl1",2);
                ////oResultadoOpen.SetMessage("Waiting...", "lbl2", 2);
                ////oResultadoOpen.SetMessage("", "lbl3", 2);
                ////oResultadoOpen.SetMessage("", "lbl4", 2);

                ////oDerecho.SetVisible(true, "pic1Der");
                ////oDerecho.SetVisible(true, "pic2Der");
                ////oDerecho.SetVisible(true, "pic4Der");
                ////oDerecho.SetVisible(true, "pic6Der");
                //oDerecho.SetVisible(true, "lbl1Der");
                //oDerecho.SetVisible(true, "lbl2Der");
                //oDerecho.SetVisible(true, "lbl4Der");
                //oDerecho.SetVisible(true, "lbl6Der");
                //oDerecho.SetMessage("Sale", "lbl1Der");
                //oDerecho.SetMessage("Count", "lbl2Der");
                //oDerecho.SetMessage("Special", "lbl4Der");
                //oDerecho.SetMessage("Close Shift", "lbl6Der");


                ////oIzquierdo.SetVisible(true, "pic1Izq");
                ////oIzquierdo.SetVisible(true, "pic2Izq");
                ////oIzquierdo.SetVisible(true, "pic3Izq");
                ////oIzquierdo.SetVisible(true, "pic4Izq");
                ////oIzquierdo.SetVisible(true, "pic5Izq");
                ////oIzquierdo.SetVisible(true, "pic6Izq");
                //oIzquierdo.SetVisible(true, "lbl1Izq");
                //oIzquierdo.SetVisible(true, "lbl2Izq");
                //oIzquierdo.SetVisible(true, "lbl3Izq");
                //oIzquierdo.SetVisible(true, "lbl4Izq");
                //oIzquierdo.SetVisible(true, "lbl5Izq");
                //oIzquierdo.SetVisible(true, "lbl6Izq");
                //oIzquierdo.SetMessage("Transfer", "lbl1Izq");
                //oIzquierdo.SetMessage("Change Card", "lbl2Izq");
                //oIzquierdo.SetMessage("Recall", "lbl3Izq");
                //oIzquierdo.SetMessage("Refund", "lbl4Izq");
                //oIzquierdo.SetMessage("Dump", "lbl5Izq");
                //oIzquierdo.SetMessage("Override", "lbl6Izq");


                int IndexB = 0;
                int IndexPanel = 1;
                for (IndexB = 0; IndexB < 6; IndexB++)
                {
                    if (Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Enabled)
                    {
                        oIzquierdo.SetVisible(true, "lbl" + IndexPanel.ToString() + "Izq");
                        oIzquierdo.SetMessage(Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Renglon1 + "\n\r" + Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Renglon2, "lbl" + IndexPanel.ToString() + "Izq");
                        
                    }
                    oIzquierdo.SetEnabled(Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Enabled, "lbl" + IndexPanel.ToString() + "Izq");
                    IndexPanel++;
                }
                IndexPanel = 1;
                for (IndexB = 11; IndexB > 5; IndexB--)
                {
                    if (Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Enabled)
                    {
                        oDerecho.SetVisible(true, "lbl" + IndexPanel.ToString() + "Der");
                        oDerecho.SetMessage(Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Renglon1 + "\n\r" + Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Renglon2, "lbl" + IndexPanel.ToString() + "Der");
                        
                    }
                    oDerecho.SetEnabled(Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Enabled, "lbl" + IndexPanel.ToString() + "Der");
                    IndexPanel++;
                }


                Izquierdo = oIzquierdo;
                Derecho = oDerecho;

                LoadPassengerClassButtons();
                //oTecladoClass.SetVisible(true, "lblClass1");
                ////oTecladoClass.SetVisible(true, "pic1");
                //oTecladoClass.SetMessageButton("Student", "lblClass1");

                //oTecladoClass.SetVisible(true, "lblClass2");
                ////oTecladoClass.SetVisible(true, "pic2");
                //oTecladoClass.SetMessageButton("School", "lblClass2");

                //oTecladoClass.SetVisible(true, "lblClass3");
                ////oTecladoClass.SetVisible(true, "pic3");
                //oTecladoClass.SetMessageButton("Senior", "lblClass3");

                //oTecladoClass.SetVisible(true, "lblClass4");
                ////oTecladoClass.SetVisible(true, "pic4");
                //oTecladoClass.SetMessageButton("Adult", "lblClass4");
                //Teclado = oTecladoClass;


                //SetMessageStatusDown(Program.CoinByPass);
                SetMessageStatusUp(GetMessageStatus());
                oResultadoOpen.lblVersion.Text = "V " + Program.SoftwareVersion.ToString() + ".0";
                Result = oResultadoOpen;

            }
            else if (ScreenToLoad == "Closed")
            {
                
                Teclado = null;
                Izquierdo = null;
                Derecho = null;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                
                //oDerecho.SetVisible(true, "pic1Der");
                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic4Der");
                //oDerecho.SetVisible(true, "pic6Der");
                oDerecho.SetVisible(true, "lbl1Der");
                oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl4Der");
                oDerecho.SetVisible(true, "lbl6Der");
                oDerecho.SetMessage("Sale", "lbl1Der");
                oDerecho.SetMessage("Count", "lbl2Der");
                oDerecho.SetMessage("Special", "lbl4Der");
                oDerecho.SetMessage("Close Shift", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic1Izq");
                //oIzquierdo.SetVisible(true, "pic2Izq");
                //oIzquierdo.SetVisible(true, "pic3Izq");
                //oIzquierdo.SetVisible(true, "pic4Izq");
                //oIzquierdo.SetVisible(true, "pic5Izq");
                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl1Izq");
                oIzquierdo.SetVisible(true, "lbl2Izq");
                oIzquierdo.SetVisible(true, "lbl3Izq");
                oIzquierdo.SetVisible(true, "lbl4Izq");
                oIzquierdo.SetVisible(true, "lbl5Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");
                oIzquierdo.SetMessage("Transfer", "lbl1Izq");
                oIzquierdo.SetMessage("Change Card", "lbl2Izq");
                oIzquierdo.SetMessage("Recall", "lbl3Izq");
                oIzquierdo.SetMessage("Refund", "lbl4Izq");
                oIzquierdo.SetMessage("Dump", "lbl5Izq");
                oIzquierdo.SetMessage("Override", "lbl6Izq");

                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                Result = new Screens.Resultados.ctrResultadoClosed();
            }
            else if (ScreenToLoad == "Shift1")
            {
                oTecladoNumerico.MaxSize = 6;
                oTecladoNumerico.lblEscribir = oResultadoMensaje.lbl3;
                
                oResultadoMensaje.SetMessage("Driver ID:", "lbl1", 2);
                oResultadoMensaje.SetMessage("", "lbl2", 2);
                oResultadoMensaje.SetMessage("", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);
                Teclado = oTecladoNumerico;

                //oDerecho.SetVisible(true,"pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl6Der");

                oDerecho.SetMessage("Clear", "lbl2Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");
                oIzquierdo.SetMessage("Cancel", "lbl6Izq");
                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                if (Program.Lock)
                {
                    string Fecha = DateTime.Now.ToString("MM/dd/yyyy");
                    SetMessageStatusUp(Fecha);
                }
                else
                {
                    SetMessageStatusUp(GetMessageStatus());
                }
                
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "Shift2")
            {
                
                oTecladoNumerico.lblEscribir = oResultadoMensaje.lbl3;
                oTecladoNumerico.MaxSize = 5;
                oResultadoMensaje.SetMessage("Route Number:", "lbl1", 2);
                oResultadoMensaje.SetMessage("", "lbl2", 2);
                oResultadoMensaje.SetMessage("", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);
                Teclado = oTecladoNumerico;

                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl6Der");

                oDerecho.SetMessage("Clear", "lbl2Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");

                oIzquierdo.SetMessage("Cancel", "lbl6Izq");
                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus()); 
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "Shift3")
            {

                oTecladoNumerico.lblEscribir = oResultadoMensaje.lbl3;
                
                oResultadoMensaje.SetMessage("Run Number:", "lbl1", 2);
                oResultadoMensaje.SetMessage("", "lbl2", 2);
                oResultadoMensaje.SetMessage("", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);
                oTecladoNumerico.MaxSize = 3;
                Teclado = oTecladoNumerico;

                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl6Der");

                oDerecho.SetMessage("Clear", "lbl2Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");

                oIzquierdo.SetMessage("Cancel", "lbl6Izq");
                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "Shift4")
            {
                
                oResultadoMensaje.SetMessage("Direction:", "lbl1", 2);
                oResultadoMensaje.SetMessage("", "lbl2", 2);
                oResultadoMensaje.SetMessage("Inbound", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);
                Teclado = null;

                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                oDerecho.SetVisible(true, "lbl1Der");
                oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl6Der");

                oDerecho.SetMessage("Inbound", "lbl1Der");
                oDerecho.SetMessage("Outbound", "lbl2Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");
                oIzquierdo.SetMessage("Cancel", "lbl6Izq");

                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "RouteMenu")
            {

                oResultadoMensaje.SetMessage("Select Option:", "lbl1", 2);
                oResultadoMensaje.SetMessage("", "lbl2", 2);
                oResultadoMensaje.SetMessage("", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);
                Teclado = null;

                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                oDerecho.SetVisible(true, "lbl1Der");
                oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl3Der");
                oDerecho.SetVisible(true, "lbl6Der");

                oDerecho.SetMessage("Change Route", "lbl1Der");
                oDerecho.SetMessage("Close Shift", "lbl2Der");
                oDerecho.SetMessage("Lock", "lbl3Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");
                oIzquierdo.SetMessage("Cancel", "lbl6Izq");

                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "QuestionClose")
            {

                oResultadoMensaje.SetMessage("Close Shift?", "lbl1", 2);
                oResultadoMensaje.SetMessage("", "lbl2", 2);
                oResultadoMensaje.SetMessage("", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);
                Teclado = null;

                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                oDerecho.SetVisible(true, "lbl1Der");
                oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl3Der");
                oDerecho.SetVisible(true, "lbl6Der");

                //oDerecho.SetMessage("Change Route", "lbl1Der");
                //oDerecho.SetMessage("Close Shift", "lbl2Der");
                //oDerecho.SetMessage("Lock", "lbl3Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");
                oIzquierdo.SetMessage("Cancel", "lbl6Izq");

                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                string Fecha = DateTime.Now.ToString("MM/dd/yyyy");
                SetMessageStatusUp(Fecha);
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "Lock")
            {

                oResultadoMensaje.SetMessage("LOCKED", "lbl1", 2);
                oResultadoMensaje.SetMessage("", "lbl2", 2);
                oResultadoMensaje.SetMessage("", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);
                Teclado = null;

                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                //oDerecho.SetVisible(true, "lbl1Der");
                oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl3Der");
                oDerecho.SetVisible(true, "lbl6Der");

                //oDerecho.SetMessage("Change Route", "lbl1Der");
                //oDerecho.SetMessage("Close Shift", "lbl2Der");
                oDerecho.SetMessage("Unlock", "lbl3Der");
                //oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");
                //oIzquierdo.SetMessage("Cancel", "lbl6Izq");

                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                string Fecha = DateTime.Now.ToString("MM/dd/yyyy");
                SetMessageStatusUp(Fecha);
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "Count")
            {

                
                oResultadoMensaje.SetMessage("Choose Passenger type", "lbl1", 2);
                oResultadoMensaje.SetMessage("", "lbl2", 2);
                oResultadoMensaje.SetMessage("", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);
                Teclado = oTecladoClass;

                LoadPassengerClassButtons();

                //oTecladoClass.SetVisible(true, "lblClass1");
                ////oTecladoClass.SetVisible(true, "pic1");
                //oTecladoClass.SetMessageButton("Student", "lblClass1");

                //oTecladoClass.SetVisible(true, "lblClass2");
                ////oTecladoClass.SetVisible(true, "pic2");
                //oTecladoClass.SetMessageButton("School", "lblClass2");

                //oTecladoClass.SetVisible(true, "lblClass3");
                ////oTecladoClass.SetVisible(true, "pic3");
                //oTecladoClass.SetMessageButton("Senior", "lblClass3");

                //oTecladoClass.SetVisible(true, "lblClass4");
                ////oTecladoClass.SetVisible(true, "pic4");
                //oTecladoClass.SetMessageButton("Adult", "lblClass4");

                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                //oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl6Der");

                //oDerecho.SetMessage("Clear", "lbl2Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");

                oIzquierdo.SetMessage("Cancel", "lbl6Izq");
                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "Count1")
            {

                oTecladoNumerico.lblEscribir = oResultadoMensaje.lbl4;
                oTecladoNumerico.MaxSize = 3;
                oResultadoMensaje.SetMessage("Passenger Type:", "lbl1", 2);
                oResultadoMensaje.SetMessage(BEAMDT.Program.CountDesc, "lbl2", 2);
                oResultadoMensaje.SetMessage("Enter Passenger Quantity", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);
                oTecladoNumerico.MaxSize = 3;
                Teclado = oTecladoNumerico;

                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl6Der");

                oDerecho.SetMessage("Clear", "lbl2Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");

                oIzquierdo.SetMessage("Cancel", "lbl6Izq");
                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "Special")
            {
                
                Teclado = null;

                int IndexB = 0;
                int IndexPanel = 1;
                for (IndexB = 0; IndexB < 6; IndexB++)
                {
                    if (Prin.oConfigInterfaz.ButtonsInterfaceSpecial[IndexB].Enabled)
                    {
                        oIzquierdo.SetVisible(true, "lbl" + IndexPanel.ToString() + "Izq");
                        oIzquierdo.SetMessage(Prin.oConfigInterfaz.ButtonsInterfaceSpecial[IndexB].Renglon1 + "\n\r" + Prin.oConfigInterfaz.ButtonsInterfaceSpecial[IndexB].Renglon2, "lbl" + IndexPanel.ToString() + "Izq");
                        
                    }
                    oIzquierdo.SetEnabled(Prin.oConfigInterfaz.ButtonsInterfaceSpecial[IndexB].Enabled, "lbl" + IndexPanel.ToString() + "Izq");
                    IndexPanel++;
                }
                IndexPanel = 1;
                for (IndexB = 11; IndexB > 5; IndexB--)
                {
                    if (Prin.oConfigInterfaz.ButtonsInterfaceSpecial[IndexB].Enabled)
                    {
                        oDerecho.SetVisible(true, "lbl" + IndexPanel.ToString() + "Der");
                        oDerecho.SetMessage(Prin.oConfigInterfaz.ButtonsInterfaceSpecial[IndexB].Renglon1 + "\n\r" + Prin.oConfigInterfaz.ButtonsInterfaceSpecial[IndexB].Renglon2, "lbl" + IndexPanel.ToString() + "Der");
                        
                    }
                    oDerecho.SetEnabled(Prin.oConfigInterfaz.ButtonsInterfaceSpecial[IndexB].Enabled, "lbl" + IndexPanel.ToString() + "Der");
                    IndexPanel++;
                }
                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                //oDerecho.SetVisible(true, "lbl1Der");
                //oDerecho.SetVisible(true, "lbl2Der");
                //oDerecho.SetVisible(true, "lbl6Der");

                ////oDerecho.SetMessage("Clear", "lbl2Der");
                //oDerecho.SetMessage("Cancel", "lbl6Der");
                //oDerecho.SetMessage("Bypass On", "lbl1Der");
                //oDerecho.SetMessage("Bypass Off", "lbl2Der");


                //oDerecho.SetVisible(true, "lbl4Der");
                //oDerecho.SetMessage("Eject Ticket", "lbl4Der");

                ////oDerecho.SetVisible(true, "lbl5Der");
                ////oDerecho.SetMessage("Test Reciv", "lbl5Der");

                //oDerecho.SetVisible(true, "lbl3Der");
                //oDerecho.SetMessage("Bill Override", "lbl3Der");


                //oIzquierdo.SetVisible(true, "lbl1Izq");
                //oIzquierdo.SetVisible(true, "lbl2Izq");
                //oIzquierdo.SetMessage("Test Reciv", "lbl1Izq");
                //oIzquierdo.SetMessage("Test Send", "lbl2Izq");


                Derecho = oDerecho;
                Izquierdo = oIzquierdo;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                if (LastResult.Contains("Moneda"))
                {
                    Result = oResultadoMoneda;
                }
                else
                {
                    Result = oResultadoOpen;
                }
            }
            else if (ScreenToLoad == "BillOverride")
            {

                oResultadoMensaje.SetMessage("Choose Bill Value", "lbl1", 2);
                oResultadoMensaje.SetMessage("", "lbl2", 2);
                oResultadoMensaje.SetMessage("", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);

                LoadBillList();

                Teclado = oTecladoClass;

                //oDerecho.SetVisible(true, "pic2Der");
                oDerecho.SetVisible(true, "pic6Der");

                //oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl6Der");

                //oDerecho.SetMessage("Clear", "lbl2Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");

                oIzquierdo.SetMessage("Cancel", "lbl6Izq");
                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "CashPayment")
            {
                
                
                oResultadoMoneda.SetMessage("","lbl1",2);
                oResultadoMoneda.SetMessage("","lbl2",2);
                Teclado = oTecladoClass;

                LoadPassengerClassButtons();
                //oTecladoClass.SetVisible(true, "lblClass1");
                ////oTecladoClass.SetVisible(true, "pic1");
                //oTecladoClass.SetMessageButton("Student", "lblClass1");

                //oTecladoClass.SetVisible(true, "lblClass2");
                ////oTecladoClass.SetVisible(true, "pic2");
                //oTecladoClass.SetMessageButton("School", "lblClass2");

                //oTecladoClass.SetVisible(true, "lblClass3");
                ////oTecladoClass.SetVisible(true, "pic3");
                //oTecladoClass.SetMessageButton("Senior", "lblClass3");

                //oTecladoClass.SetVisible(true, "lblClass4");
                ////oTecladoClass.SetVisible(true, "pic4");
                //oTecladoClass.SetMessageButton("Adult", "lblClass4");

                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                //oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl6Der");

                //oDerecho.SetMessage("Clear", "lbl2Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

               // oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");

                oIzquierdo.SetMessage("Cancel", "lbl6Izq");
                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                Result = oResultadoMoneda;
            }
            else if (ScreenToLoad == "Sale")
            {

                
                oResultadoMensaje.SetMessage("Choose Product:", "lbl1", 2);
                oResultadoMensaje.SetMessage("", "lbl2", 2);
                oResultadoMensaje.SetMessage("", "lbl3", 2);
                oResultadoMensaje.SetMessage("", "lbl4", 2);
                Teclado = oTecladoClass;

                LoadSaleButtons();

               
                //oDerecho.SetVisible(true, "pic2Der");
                //oDerecho.SetVisible(true, "pic6Der");

                //oDerecho.SetVisible(true, "lbl2Der");
                oDerecho.SetVisible(true, "lbl6Der");

                //oDerecho.SetMessage("Clear", "lbl2Der");
                oDerecho.SetMessage("Enter", "lbl6Der");

                //oIzquierdo.SetVisible(true, "pic6Izq");
                oIzquierdo.SetVisible(true, "lbl6Izq");

                oIzquierdo.SetMessage("Cancel", "lbl6Izq");
                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "Sale1")
            {

                Teclado = null;

                //oDerecho.SetVisible(true, "lbl4Der");
                //oDerecho.SetMessage("Special", "lbl4Der");

                //oDerecho.SetVisible(true, "lbl3Izq");
                //oDerecho.SetMessage("Dump", "lbl3Izq");


                int IndexB = 0;
                int IndexPanel = 1;
                for (IndexB = 0; IndexB < 6; IndexB++)
                {
                    if (Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Funcion == 5 || Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Funcion == 9)
                    {
                        if (Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Enabled)
                        {
                            oIzquierdo.SetVisible(true, "lbl" + IndexPanel.ToString() + "Izq");
                            oIzquierdo.SetMessage(Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Renglon1 + "\n\r" + Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Renglon2, "lbl" + IndexPanel.ToString() + "Izq");

                        }
                        oIzquierdo.SetEnabled(Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Enabled, "lbl" + IndexPanel.ToString() + "Izq");
                    }
                    
                    IndexPanel++;
                }
                IndexPanel = 1;
                for (IndexB = 11; IndexB > 5; IndexB--)
                {
                    if (Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Funcion == 5 || Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Funcion == 9)
                    {
                        if (Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Enabled)
                        {
                            oDerecho.SetVisible(true, "lbl" + IndexPanel.ToString() + "Der");
                            oDerecho.SetMessage(Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Renglon1 + "\n\r" + Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Renglon2, "lbl" + IndexPanel.ToString() + "Der");

                        }
                    }
                    oDerecho.SetEnabled(Prin.oConfigInterfaz.ButtonsInterfaceMain[IndexB].Enabled, "lbl" + IndexPanel.ToString() + "Der");
                    IndexPanel++;
                }




                oIzquierdo.SetVisible(true, "lbl6Izq");
                oIzquierdo.SetMessage("Cancel", "lbl6Izq");
                
                
                Izquierdo = oIzquierdo;
                Derecho = oDerecho;
                //SetMessageStatusDown("");
                SetMessageStatusUp(GetMessageStatus());
                Result = oResultadoMensaje;
            }
            else if (ScreenToLoad == "Volume Farebox" || ScreenToLoad == "Volume" || ScreenToLoad == "Brightness")
            {

                Teclado = null;

                
                //oDerecho.SetVisible(true, "pic2Der");
                oDerecho.SetVisible(true, "pic6Der");

                //oDerecho.SetVisible(true, "lbl1Der");
                //oDerecho.SetVisible(true, "lbl2Der");
                //oDerecho.SetVisible(true, "lbl6Der");

                ////oDerecho.SetMessage("Clear", "lbl2Der");
                oDerecho.SetMessage("Enter", "lbl6Der");
                //oDerecho.SetMessage("Bypass On", "lbl1Der");
                //oDerecho.SetMessage("Bypass Off", "lbl2Der");


                //oDerecho.SetVisible(true, "lbl4Der");
                //oDerecho.SetMessage("Eject Ticket", "lbl4Der");

                ////oDerecho.SetVisible(true, "lbl5Der");
                ////oDerecho.SetMessage("Test Reciv", "lbl5Der");

                //oDerecho.SetVisible(true, "lbl3Der");
                //oDerecho.SetMessage("Bill Override", "lbl3Der");


                //oIzquierdo.SetVisible(true, "lbl6Izq");
                //oIzquierdo.SetVisible(true, "lbl2Izq");
                //oIzquierdo.SetMessage("Cancel", "lbl6Izq");
                //oIzquierdo.SetMessage("Test Send", "lbl2Izq");


                Derecho = oDerecho;
                Izquierdo = oIzquierdo;
                SetMessageStatusUp(GetMessageStatus());
                oResultadoAdjust.Prin = Prin;
                if (ScreenToLoad.Contains("Volume"))
                {
                    oResultadoAdjust.Type = Resultados.ctrResultadoAdjust.AdjustType.Volume;
                }
                else
                {
                    oResultadoAdjust.Type = Resultados.ctrResultadoAdjust.AdjustType.Brightness;
                }
                if (ScreenToLoad.Contains("Farebox"))
                {
                    oResultadoAdjust.CurrentDevice = Resultados.ctrResultadoAdjust.Device.Farebox;
                }
                else
                {
                    oResultadoAdjust.CurrentDevice = Resultados.ctrResultadoAdjust.Device.MDT;
                }
                oResultadoAdjust.Inicio();
                Result = oResultadoAdjust;
                
            }
            SetButtons();
        }
        public string GetLabelText(string lblName)
        {
            string text = "";
            if (Izquierdo != null)
            {
                ctrIzquierdo oIzq = (ctrIzquierdo)Izquierdo;
                foreach (Control c in oIzq.Controls)
                {
                    if (c.Name == lblName)
                    {
                        return c.Text;
                    }
                }
            }
            if (Derecho != null)
            {
                ctrDerecho oDer = (ctrDerecho)Derecho;
                foreach (Control c in oDer.Controls)
                {
                    if (c.Name == lblName)
                    {
                        return c.Text;
                    }
                }
            }
            return text;
        }
        private void SetButtons()
        {
            
            //if (Izquierdo != null)
            //{
            //    ctrIzquierdo oIzq = (ctrIzquierdo)Izquierdo;
            //    frmPrincipalAlt.BotonIzq1 = oIzq.lbl1Izq.Visible;
            //    frmPrincipalAlt.BotonIzq2 = oIzq.lbl2Izq.Visible;
            //    frmPrincipalAlt.BotonIzq3 = oIzq.lbl3Izq.Visible;
            //    frmPrincipalAlt.BotonIzq4 = oIzq.lbl4Izq.Visible;
            //    frmPrincipalAlt.BotonIzq5 = oIzq.lbl5Izq.Visible;
            //    frmPrincipalAlt.BotonIzq6 = oIzq.lbl6Izq.Visible;
            //}
            //else 
            //{
            //    frmPrincipalAlt.BotonIzq1 = false;
            //    frmPrincipalAlt.BotonIzq2 = false;
            //    frmPrincipalAlt.BotonIzq3 = false;
            //    frmPrincipalAlt.BotonIzq4 = false;
            //    frmPrincipalAlt.BotonIzq5 = false;
            //    frmPrincipalAlt.BotonIzq6 = false;
            //}
            //if (Derecho != null)
            //{
            //    ctrDerecho oDer = (ctrDerecho)Derecho;
            //    frmPrincipalAlt.BotonDer1 = oDer.pic1Der.Visible;
            //    frmPrincipalAlt.BotonDer2 = oDer.pic2Der.Visible;
            //    frmPrincipalAlt.BotonDer3 = oDer.pic3Der.Visible;
            //    frmPrincipalAlt.BotonDer4 = oDer.pic4Der.Visible;
            //    frmPrincipalAlt.BotonDer5 = oDer.pic5Der.Visible;
            //    frmPrincipalAlt.BotonDer6 = oDer.pic6Der.Visible;
            //}
            //else
            //{
            //    frmPrincipalAlt.BotonDer1 = false;
            //    frmPrincipalAlt.BotonDer2 = false;
            //    frmPrincipalAlt.BotonDer3 = false;
            //    frmPrincipalAlt.BotonDer4 = false;
            //    frmPrincipalAlt.BotonDer5 = false;
            //    frmPrincipalAlt.BotonDer6 = false;
            //}
        }
       
    }
}
