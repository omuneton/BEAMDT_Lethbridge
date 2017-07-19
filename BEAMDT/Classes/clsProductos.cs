using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BEAMDT.Classes
{
    public class clsProductos
    {
        public List<frmPrincipalAlt.Products> Products = new List<frmPrincipalAlt.Products>();
        public int LoadProducts()
        {
            //Products.Clear();
            //frmPrincipalAlt.Products p = new frmPrincipalAlt.Products();
            //p.ID = 1;
            //p.Description = "1 Day";
            //p.Type = 1;
            //p.IDClass = 1;
            //p.Price = 1;
            //p.Money = 0;
            //p.Tickets = 0;
            //p.InitDate = DateTime.Now;
            //p.EndDate = DateTime.Now;
            //p.Days = 1;
            //Products.Add(p);

            //frmPrincipalAlt.Products p2 = new frmPrincipalAlt.Products();
            //p2.ID = 1;
            //p2.Description = "1 Week";
            //p2.Type = 1;
            //p2.IDClass = 1;
            //p2.Price = 2;
            //p2.Money = 0;
            //p2.Tickets = 0;
            //p2.InitDate = DateTime.Now;
            //p2.EndDate = DateTime.Now;
            //p2.Days = 7;
            //Products.Add(p2);

            //frmPrincipalAlt.Products p3 = new frmPrincipalAlt.Products();
            //p3.ID = 1;
            //p3.Description = "1 Month";
            //p3.Type = 2;
            //p3.IDClass = 1;
            //p3.Price = 5;
            //p3.Money = 0;
            //p3.Tickets = 0;
            //p3.InitDate = new DateTime(2016,09,1);
            //p3.EndDate = new DateTime(2016, 09, 30);
            //p3.Days = 0;
            //Products.Add(p3);

            //frmPrincipalAlt.Products p4 = new frmPrincipalAlt.Products();
            //p4.ID = 1;
            //p4.Description = "6 Months";
            //p4.Type = 2;
            //p4.IDClass = 1;
            //p4.Price = 10;
            //p4.Money = 0;
            //p4.Tickets = 0;
            //p4.InitDate = new DateTime(2016, 06, 1);
            //p4.EndDate = new DateTime(2016, 12, 31);
            //p4.Days = 0;
            //Products.Add(p4);

            //return 1;
            try
            {
                Products.Clear();
                int iCont, Version;
                string[] Archivos = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\InBox\\", "FBX PL*");
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
                XmlNodeList Lista = Doc.GetElementsByTagName("Product");
                foreach (XmlNode Node in Lista)
                {
                    frmPrincipalAlt.Products p = new frmPrincipalAlt.Products();
                    p.ID = int.Parse(Node.ChildNodes[0].InnerText.ToString());
                    p.Description = Node.ChildNodes[1].InnerText.ToString();
                    p.Type = int.Parse(Node.ChildNodes[2].InnerText.ToString());
                    p.IDClass = int.Parse(Node.ChildNodes[3].InnerText.ToString());
                    p.Price = double.Parse(Node.ChildNodes[4].InnerText.ToString());
                    p.Money = double.Parse(Node.ChildNodes[5].InnerText.ToString());
                    p.Tickets = int.Parse(Node.ChildNodes[6].InnerText.ToString());
                    p.InitDate = new DateTime(int.Parse(Node.ChildNodes[7].InnerText.ToString().Substring(6, 4)), int.Parse(Node.ChildNodes[7].InnerText.ToString().Substring(3, 2)), int.Parse(Node.ChildNodes[7].InnerText.ToString().Substring(0, 2)));
                    p.EndDate = new DateTime(int.Parse(Node.ChildNodes[8].InnerText.ToString().Substring(6, 4)), int.Parse(Node.ChildNodes[8].InnerText.ToString().Substring(3, 2)), int.Parse(Node.ChildNodes[8].InnerText.ToString().Substring(0, 2)));
                    p.Days = int.Parse(Node.ChildNodes[9].InnerText.ToString());
                    Products.Add(p);
                }
                return 1;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }
    }
}
