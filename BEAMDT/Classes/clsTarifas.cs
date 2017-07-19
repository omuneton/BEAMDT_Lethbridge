using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BEAMDT.Classes
{
    public class clsTarifas
    {
        public List<frmPrincipalAlt.PassengerClass> PassengersClass = new List<frmPrincipalAlt.PassengerClass>();
        private string getNombreDia()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "Domingo";
                case DayOfWeek.Monday:
                    return "Lunes";
                case DayOfWeek.Tuesday:
                    return "Martes";
                case DayOfWeek.Wednesday:
                    return "Miercoles";
                case DayOfWeek.Thursday:
                    return "Jueves";
                case DayOfWeek.Friday:
                    return "Viernes";
                case DayOfWeek.Saturday:
                    return "Sabado";
            }
            return "";
        }
        public int LoadPassengerClass()
        {
            //PassengersClass.Clear();
            //frmPrincipalAlt.PassengerClass p = new frmPrincipalAlt.PassengerClass();
            //p.Description = "Adult";
            //p.Fare = 2.5;
            //p.ID = 1;
            //p.TypeOfClass = 0;
            //PassengersClass.Add(p);
            //frmPrincipalAlt.PassengerClass p1 = new frmPrincipalAlt.PassengerClass();
            //p1.Description = "Child";
            //p1.Fare = 2.0;
            //p1.ID = 2;
            //p1.TypeOfClass = 0;
            //PassengersClass.Add(p1);
            //frmPrincipalAlt.PassengerClass p2 = new frmPrincipalAlt.PassengerClass();
            //p2.Description = "Student";
            //p2.Fare = 1.0;
            //p2.ID = 3;
            //p2.TypeOfClass = 0;
            //PassengersClass.Add(p2);
            //frmPrincipalAlt.PassengerClass p3 = new frmPrincipalAlt.PassengerClass();
            //p3.Description = "Senior";
            //p3.Fare = 1.0;
            //p3.ID = 4;
            //p3.TypeOfClass = 0;
            //PassengersClass.Add(p3);
            //return 1;
            try
            {
                PassengersClass.Clear();
                int iCont, Version;
                string[] Archivos = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\InBox\\", "FBX FT*");
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
                XmlNodeList TarifaGeneral = Doc.GetElementsByTagName("TarifaNormal");
                if (TarifaGeneral.Count > 0)
                {
                    Program.GeneralFare = double.Parse(TarifaGeneral[0].InnerText);
                }
                XmlNodeList Lista = Doc.GetElementsByTagName("ClaseMoneda");
                foreach (XmlNode Node in Lista)
                {
                    frmPrincipalAlt.PassengerClass p = new frmPrincipalAlt.PassengerClass();
                    p.ID = int.Parse(Node.ChildNodes[0].InnerText.ToString());
                    string[] desc = Node.ChildNodes[1].InnerText.Split(',');
                    if (desc.Length > 1)
                    {
                        p.Renglon1 = desc[0];
                        p.Renglon2 = desc[1];
                    }
                    p.TypeOfClass = 9;
                    XmlNodeList Horario = Node.ChildNodes;
                    List<XmlNode> HorarioDia = new List<XmlNode>();
                    string Dia = getNombreDia();
                    foreach (XmlNode h in Horario)
                    {
                        if (h.Name == "DiaMoneda")
                        {
                            if (Dia == h.ChildNodes[0].InnerText)
                            {
                                HorarioDia.Add(h.ChildNodes[2]);
                            }
                        }
                    }
                    p.Fare = 0;
                    int minIndex = 0;
                    if (HorarioDia.Count > 0)
                    {
                        for (int i = 0; i < HorarioDia.Count; i++)
                        {
                            string[] hora = HorarioDia[i].ChildNodes[0].InnerText.Split(':');
                            if ((int.Parse(hora[0]) > DateTime.Now.Hour))
                            {
                                minIndex = i;
                            }
                            else if (int.Parse(hora[0]) == DateTime.Now.Hour)
                            {
                                if ((int.Parse(hora[1]) >= DateTime.Now.Minute))
                                {
                                    minIndex = i;
                                }
                            }
                        }
                        p.Fare = double.Parse(HorarioDia[minIndex].ChildNodes[4].InnerText);
                        if (HorarioDia[minIndex].ChildNodes[5].InnerText == "1")
                        {
                            p.Enabled = true;
                        }
                        else
                        {
                            p.Enabled = false;
                        }
                    }
                    PassengersClass.Add(p);
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
