using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BEAMDT.Classes
{
    public class clsConfigInterfaz
    {
        public List<frmPrincipalAlt.BillDenomination> AllowedBills = new List<frmPrincipalAlt.BillDenomination>();
        public List<frmPrincipalAlt.BotonInterfaz> ButtonsInterfaceMain = new List<frmPrincipalAlt.BotonInterfaz>();
        public List<frmPrincipalAlt.BotonInterfaz> ButtonsInterfaceSpecial = new List<frmPrincipalAlt.BotonInterfaz>();
        public bool LoadOffset()
        {
            try
            {
                int iCont, Version;
                string[] Archivos = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\InBox\\", "FBX CF*");
                if (Archivos.Length == 0)
                {
                    return false;
                }
                int indiceMayor = -1;
                if (Archivos.Length > 1)
                {
                    int MayorVersion = 0;

                    for (iCont = 0; iCont < Archivos.Length; iCont++)
                    {
                        string[] Arch1 = Archivos[iCont].Split('\\');
                        if (MayorVersion < Convert.ToInt16(Arch1[Arch1.Length - 1].Split(' ')[2], 16))
                        {
                            MayorVersion = Convert.ToInt16(Arch1[Arch1.Length - 1].Split(' ')[2], 16);
                            indiceMayor = iCont;
                        }
                    }
                }
                else
                {
                    indiceMayor = 0;
                }
                string[] Arch = Archivos[indiceMayor].Split('\\');
                string[] nVer = Arch[Arch.Length - 1].Split(' ');
                Version = Convert.ToInt16(nVer[2], 16);
                BEAMDT.Program.RDRVersion = Version;
                XmlDocument Doc = new XmlDocument();
                Doc.Load(Archivos[indiceMayor]);
                XmlNodeList Offset = Doc.GetElementsByTagName("OffsetHusoHorario");
                if (Offset.Count > 0)
                {
                    Program.Offset = int.Parse(Offset[0].InnerText);
                }
                return true;
            }
            catch 
            {
                return false;
            }
            
        }
        public int LoadButtonsBills()
        {
            //AllowedBills.Clear();
            //frmPrincipalAlt.BillDenomination b = new frmPrincipalAlt.BillDenomination();
            //b.Description = "1 Dll";
            //b.Enabled = true;
            //b.Value = 1;
            //AllowedBills.Add(b);
            //frmPrincipalAlt.BillDenomination b2 = new frmPrincipalAlt.BillDenomination();
            //b2.Description = "5 Dll";
            //b2.Enabled = true;
            //b2.Value = 5;
            //AllowedBills.Add(b2);
            //frmPrincipalAlt.BillDenomination b3 = new frmPrincipalAlt.BillDenomination();
            //b3.Description = "10 Dll";
            //b3.Enabled = true;
            //b3.Value = 10;
            //AllowedBills.Add(b3);
            //frmPrincipalAlt.BillDenomination b4 = new frmPrincipalAlt.BillDenomination();
            //b4.Description = "20 Dll";
            //b4.Enabled = true;
            //b4.Value = 20;
            //AllowedBills.Add(b4);
            
            //return 1;
            try
            {
                AllowedBills.Clear();
                ButtonsInterfaceMain.Clear();
                ButtonsInterfaceSpecial.Clear();
                int iCont, Version;
                string[] Archivos = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\InBox\\", "FBX CF*");
                if (Archivos.Length == 0)
                {
                    return -1;
                }
                int indiceMayor = -1;
                if (Archivos.Length > 1)
                {
                    int MayorVersion = 0;

                    for (iCont = 0; iCont < Archivos.Length; iCont++)
                    {
                        string[] Arch1 = Archivos[iCont].Split('\\');
                        if (MayorVersion < Convert.ToInt16(Arch1[Arch1.Length - 1].Split(' ')[2], 16))
                        {
                            MayorVersion = Convert.ToInt16(Arch1[Arch1.Length - 1].Split(' ')[2], 16);
                            indiceMayor = iCont;
                        }
                    }
                }
                else
                {
                    indiceMayor = 0;
                }
                string[] Arch = Archivos[indiceMayor].Split('\\');
                string[] nVer = Arch[Arch.Length - 1].Split(' ');
                Version = Convert.ToInt16(nVer[2], 16);
                BEAMDT.Program.RDRVersion = Version;
                XmlDocument Doc = new XmlDocument();
                Doc.Load(Archivos[indiceMayor]);
                XmlNodeList Lista = Doc.GetElementsByTagName("BillOverride");
                if (Lista.Count > 0)
                {
                    int IndexMensajeBoton = 0;
                    int IndexDenominacion = 0;
                    foreach (XmlNode Node in Lista[0].ChildNodes)
                    {

                        if (Node.Name.Contains("HabilitarBoton"))
                        {
                            frmPrincipalAlt.BillDenomination b = new frmPrincipalAlt.BillDenomination();
                            if (Node.InnerText == "1")
                            {
                                b.Enabled = true;
                            }
                            else
                            {
                                b.Enabled = false;
                            }
                            AllowedBills.Add(b);
                        }

                        if (Node.Name.Contains("MensajeBoton"))
                        {
                            frmPrincipalAlt.BillDenomination b = AllowedBills[IndexMensajeBoton];
                            b.Description = "";
                            if (Node.InnerText != "Null")
                            {
                                b.Description = Node.InnerText;
                            }
                            AllowedBills[IndexMensajeBoton] = b;
                            IndexMensajeBoton++;
                        }

                        if (Node.Name.Contains("DenominacionBilleteBoton"))
                        {
                            frmPrincipalAlt.BillDenomination b = AllowedBills[IndexDenominacion];
                            b.Value = double.Parse(Node.InnerText);
                            AllowedBills[IndexDenominacion] = b;
                            IndexDenominacion++;
                        }

                    }
                }
                Lista = Doc.GetElementsByTagName("MainScreen");
                if (Lista.Count > 0)
                {
                    int IndexBoton = 0;
                    int IndexFuncion = 0;
                    foreach (XmlNode Node in Lista[0].ChildNodes)
                    {

                        if (Node.Name.Contains("HabilitarBoton"))
                        {
                            frmPrincipalAlt.BotonInterfaz b = new frmPrincipalAlt.BotonInterfaz();
                            if (Node.InnerText == "1")
                            {
                                b.Enabled = true;
                            }
                            else
                            {
                                b.Enabled = false;
                            }
                            ButtonsInterfaceMain.Add(b);
                        }

                        if (Node.Name.Contains("MensajeBoton"))
                        {
                            frmPrincipalAlt.BotonInterfaz b = ButtonsInterfaceMain[IndexBoton];
                            b.Renglon1 = "";
                            b.Renglon2 = "";
                            if (Node.InnerText.Split(',')[0] != "Null")
                            {
                                b.Renglon1 = Node.InnerText.Split(',')[0];
                            }
                            if (Node.InnerText.Split(',')[1] != "Null")
                            {
                                b.Renglon2 = Node.InnerText.Split(',')[1];
                            }

                            ButtonsInterfaceMain[IndexBoton] = b;
                            IndexBoton++;
                        }

                        if (Node.Name.Contains("FuncionBoton"))
                        {
                            frmPrincipalAlt.BotonInterfaz b = ButtonsInterfaceMain[IndexFuncion];
                            b.Funcion = int.Parse(Node.InnerText);
                            ButtonsInterfaceMain[IndexFuncion] = b;
                            IndexFuncion++;
                        }

                    }
                }
                
                Lista = Doc.GetElementsByTagName("SpecialScreen");
                if (Lista.Count > 0)
                {
                    int IndexBoton = 0;
                    int IndexFuncion = 0;
                    foreach (XmlNode Node in Lista[0].ChildNodes)
                    {

                        if (Node.Name.Contains("HabilitarBoton"))
                        {
                            frmPrincipalAlt.BotonInterfaz b = new frmPrincipalAlt.BotonInterfaz();
                            if (Node.InnerText == "1")
                            {
                                b.Enabled = true;
                            }
                            else
                            {
                                b.Enabled = false;
                            }
                            ButtonsInterfaceSpecial.Add(b);
                        }

                        if (Node.Name.Contains("MensajeBoton"))
                        {
                            frmPrincipalAlt.BotonInterfaz b = ButtonsInterfaceSpecial[IndexBoton];
                            b.Renglon1 = "";
                            b.Renglon2 = "";
                            if (Node.InnerText.Split(',')[0] != "Null")
                            {
                                b.Renglon1 = Node.InnerText.Split(',')[0];
                            }
                            if (Node.InnerText.Split(',')[1] != "Null")
                            {
                                b.Renglon2 = Node.InnerText.Split(',')[1];
                            }
                            ButtonsInterfaceSpecial[IndexBoton] = b;
                            IndexBoton++;
                        }

                        if (Node.Name.Contains("FuncionBoton"))
                        {
                            frmPrincipalAlt.BotonInterfaz b = ButtonsInterfaceSpecial[IndexFuncion];
                            b.Funcion = int.Parse(Node.InnerText);
                            ButtonsInterfaceSpecial[IndexFuncion] = b;
                            IndexFuncion++;
                        }

                    }

                }
                return 1;
            }
            catch 
            {
                return -2;
            }
        }
    }
}
