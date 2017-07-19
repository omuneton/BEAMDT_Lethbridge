using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BEAMDT.Classes
{
    public class clsRDR
    {
        public List<int> Choferes = new List<int>();
        public struct Ruta
        { 
            public int Rut;
            public string DescRut;
            public int Corrida;
        }
        public List<int> Camiones = new List<int>();
        public bool RDRCHECK = true;
        private bool LoadCamionesFile(string Path)
        {
            try
            {
                XmlTextReader Reader = new XmlTextReader(Path);
                Camiones.Clear();
                while (Reader.Read())
                {
                    if (Reader.Name == "iCodUnidad" && Reader.NodeType.ToString() == "Element")
                    {
                        Reader.Read();
                        Camiones.Add(int.Parse((Reader.Value.ToString())));
                    }
                }
                Reader.Close();
                if (Camiones.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch 
            {
                return false;
            }
            
        }
        /// <summary>
        /// Loads the valid buses ID from the file
        /// </summary>
        /// <returns></returns>
        public bool LoadCamiones()
        {
            try
            {
                string[] Archivos = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\OutBox\\", "BUS LST.its");
                if (Archivos.Length == 0)
                {
                    Archivos = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\OutBoxBackup\\", "BUS LST.its");
                    if (Archivos.Length == 0)
                    {
                        return false;
                    }
                }
                if (!LoadCamionesFile(Archivos[0]))
                { 
                    Archivos = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\OutBoxBackup\\", "BUS LST.its");
                    if (Archivos.Length == 0)
                    {
                        return false;
                    }
                    if (!LoadCamionesFile(Archivos[0]))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Search for a route
        /// </summary>
        /// <param name="Route"></param>
        /// <returns></returns>
        public bool BuscaRuta(int Route)
        {
            //return true;
            int iCont;
            for (iCont = 0; iCont < Rutas.Count; iCont++)
            {
                if (Rutas[iCont].Rut == Route)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Search for a route description
        /// </summary>
        /// <param name="Route"></param>
        /// <returns></returns>
        public string BuscaRutaDescu(int Route)
        {
            int iCont;
            for (iCont = 0; iCont < Rutas.Count; iCont++)
            {
                if (Rutas[iCont].Rut == Route)
                {
                    return Rutas[iCont].DescRut;
                }
            }
            return "DEF-1";
        }
        /// <summary>
        /// Search for a run.
        /// </summary>
        /// <param name="Route"></param>
        /// <param name="Corrida"></param>
        /// <returns></returns>
        public bool BuscaCorrida(int Route, int Corrida)
        {
            //return true;
            int iCont;
            for (iCont = 0; iCont < Rutas.Count; iCont++)
            {
                if (Rutas[iCont].Rut == Route)
                {
                    if (Corrida <= Rutas[iCont].Corrida)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Search for a run.
        /// </summary>
        /// <param name="Route"></param>
        /// <param name="Corrida"></param>
        /// <returns></returns>
        public bool BuscaChofer(int IDChofer)
        {
            //return true;
            return Choferes.Contains(IDChofer);
        }
        List<Ruta> Rutas = new List<Ruta>();
        public int Version;
        public int LoadRDRFile(string FilePath)
        {
            try
            {
                RDRCHECK = false;
                int TotalRutas, iCont;
                Rutas.Clear();
                Choferes.Clear();
                string[] Archivos = System.IO.Directory.GetFiles(FilePath, "FBX RDR*");
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
                System.IO.StreamReader sr = new System.IO.StreamReader(Archivos[indiceMayor], Encoding.Default);
                string Cadena = "";
                string Temp = "0";
                while (Temp != null)
                {
                    Temp = sr.ReadLine();
                    if (Temp != null && Temp != "")
                    {
                        Cadena = Cadena + Temp;
                    }
                }
                sr.Close();
                //for (iCont = 0; iCont < 7; iCont++)
                //{
                //    Dato = (byte)sr.Read();
                //}

                TotalRutas = Convert.ToByte(Cadena.Substring(4, 2), 16);
                TotalRutas = TotalRutas + (Convert.ToByte(Cadena.Substring(6, 2), 16) << 8);
                int index = 8;
                Ruta TempRut;
                for (iCont = 0; iCont < TotalRutas; iCont++)
                {
                    TempRut = new Ruta();
                    TempRut.Rut = Convert.ToByte(Cadena.Substring(index, 2), 16);
                    index = index + 2;
                    TempRut.Rut = TempRut.Rut + (Convert.ToByte(Cadena.Substring(index, 2), 16) << 8);
                    index = index + 2;
                    TempRut.Corrida = Convert.ToByte(Cadena.Substring(index, 2), 16);
                    index = index + 2;
                    TempRut.DescRut = "";
                    TempRut.DescRut = TempRut.DescRut + (char)Convert.ToByte(Cadena.Substring(index, 2), 16);
                    index = index + 2;
                    TempRut.DescRut = TempRut.DescRut + (char)Convert.ToByte(Cadena.Substring(index, 2), 16);
                    index = index + 2;
                    TempRut.DescRut = TempRut.DescRut + (char)Convert.ToByte(Cadena.Substring(index, 2), 16);
                    index = index + 2;
                    TempRut.DescRut = TempRut.DescRut + (char)Convert.ToByte(Cadena.Substring(index, 2), 16);
                    index = index + 2;
                    TempRut.DescRut = TempRut.DescRut + (char)Convert.ToByte(Cadena.Substring(index, 2), 16);
                    index = index + 2;
                    Rutas.Add(TempRut);
                }

                int TotalChoferes;
                TotalChoferes = Convert.ToByte(Cadena.Substring(index, 2), 16);
                index = index + 2;
                TotalChoferes = TotalChoferes + (Convert.ToByte(Cadena.Substring(index, 2), 16) << 8);
                index = index + 2;
                int TempChofer;
                for (iCont = 0; iCont < TotalChoferes; iCont++)
                {
                    TempChofer = int.Parse((Convert.ToByte(Cadena.Substring(index + 4, 2), 16) >> 4).ToString() + (Convert.ToByte(Cadena.Substring(index + 4, 2), 16) & 0xF).ToString() + (Convert.ToByte(Cadena.Substring(index + 2, 2), 16) >> 4).ToString() + (Convert.ToByte(Cadena.Substring(index + 2, 2), 16) & 0xF).ToString() + (Convert.ToByte(Cadena.Substring(index, 2), 16) >> 4).ToString() + (Convert.ToByte(Cadena.Substring(index, 2), 16) & 0xF).ToString());
                    index = index + 6;
                    Choferes.Add(TempChofer);
                }
                if (Rutas.Count > 0)
                {
                    RDRCHECK = true;
                    return 1;
                }
                else
                {
                    return 0;
                }
                
            }
            catch 
            {
                return -2;
            }
        }
        /// <summary>
        /// Loads the valid Route, run and drivers ID to open the shift.
        /// </summary>
        /// <returns></returns>
        public int LoadRDR()
        {
            try
            {
                int val=LoadRDRFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\InBox\\");
                return val;
                //else if (val != 1)
                //{
                //    val = LoadRDRFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\InBoxBackup\\");
                //    if (val != 1)
                //    {
                //        return val;
                //    }
                //}
            }
            catch
            {
                return -1;
            }
        }
        
    
    }
}
