using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml;

namespace BEAMDT.Classes
{
    class clsConfig
    {
        public static bool RouteEnabled=false;
        public static bool RunEnabled = false;
        public static bool DirectionEnabled = false;
        public static int DefaultRoute = 0;
        public static int DefaultRun = 0;
        public static int DefaultDirection = 0;
        public static int TimeFormat = 0;
        public static int AutoCloseTime = 0;
        public static int LastHLVersion = 0;
        public static int LastALVersion = 0;
        public static int LastCFVersion = 0;
        public static int LastFTVersion = 0;
        public static int LastPLVersion = 0;
        public static string Password = "1111";
        public static int OffsetHora = 0;
        public static string PasswordFTP = "BEA2014";
        public static string User = "FTPUser";
        public static string FTPServer = "192.168.1.101";
        public static string BluetoothMAC = "4040A705C7C3";
        public static clsVolumen.Volumes Volume = clsVolumen.Volumes.MEDIUM;
        public static clsBrightness.Brightness Brightness = clsBrightness.Brightness.High;
        public static List<string> ListAPMACs = new List<string>();
        private static string Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\DeviceInfo.xml";
        private static string GetMACList()
        {
            string MACsReturn="";
            foreach (string MAC in ListAPMACs)
            {
                MACsReturn = MACsReturn + MAC + ",";
            }
            if (MACsReturn != "")
            {
                MACsReturn = MACsReturn.Substring(0, MACsReturn.Length - 1);
            }
            return MACsReturn;
        }
        public static void SetMACList(string MACList)
        {
            string[] innermaclist = MACList.Split(',');
            ListAPMACs.Clear();
            ListAPMACs.AddRange(innermaclist);
        }
        public static bool SaveConfig()
        {
            clsCrypto oCrypto = new clsCrypto();
            System.IO.File.Delete(Path);
            System.IO.FileStream F = System.IO.File.Create(Path);
            F.Close();
            XmlTextWriter Writer = new XmlTextWriter(Path, Encoding.Default);
            Writer.WriteStartDocument(true);
            Writer.WriteStartElement("Project");
            Writer.WriteElementString("TimeFormat", TimeFormat.ToString());
            Writer.WriteElementString("AutoCloseTime", AutoCloseTime.ToString());
            Writer.WriteElementString("DefaultRouteValue", DefaultRoute.ToString());
            Writer.WriteElementString("DefaultRunValue", DefaultRun.ToString());
            Writer.WriteElementString("DefaultDirectionValue", DefaultDirection.ToString());
            if (clsConfig.RouteEnabled)
            {
                Writer.WriteElementString("RouteEnabled", "YES");
            }
            else
            {
                Writer.WriteElementString("RouteEnabled", "NO");
            }
            if (clsConfig.RunEnabled)
            {
                Writer.WriteElementString("RunEnabled", "YES");
            }
            else
            {
                Writer.WriteElementString("RunEnabled", "NO");
            }
            if (clsConfig.DirectionEnabled)
            {
                Writer.WriteElementString("DirectionEnabled", "YES");
            }
            else
            {
                Writer.WriteElementString("DirectionEnabled", "NO");
            }
            Writer.WriteElementString("Password", oCrypto.Encript(Password));
            Writer.WriteElementString("BluetoothMAC", GetMACList());
            Writer.WriteElementString("FTPServer", FTPServer);
            Writer.WriteElementString("User", User);
            Writer.WriteElementString("PasswordFTP", oCrypto.Encript(PasswordFTP));
            Writer.WriteElementString("LastHLVersion", LastHLVersion.ToString());
            Writer.WriteElementString("LastALVersion", LastALVersion.ToString());
            Writer.WriteElementString("LastFTVersion", LastFTVersion.ToString());
            Writer.WriteElementString("LastCFVersion", LastCFVersion.ToString());
            Writer.WriteElementString("LastPLVersion", LastPLVersion.ToString());
            Writer.WriteElementString("Volume", ((int)Volume).ToString());
            Writer.WriteElementString("Brightness", ((int)Brightness).ToString());
            Writer.WriteEndElement();
            Writer.WriteEndDocument();
            Writer.Close();
            return true;
        }
        /// <summary>
        /// Load the configuration from DeviceInfo.xml
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static bool LoadConf()
        {
            clsCrypto oCrypto = new clsCrypto();
            if (!System.IO.File.Exists(Path))
            {
                SaveConfig();
            }
            string DefaultValue = "";
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(Path);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Columns.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Columns.IndexOf("FTPServer") != -1)
                        {
                            FTPServer = ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("FTPServer")].ToString();
                        }
                        if (ds.Tables[0].Columns.IndexOf("User") != -1)
                        {
                            User = ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("User")].ToString();
                        }
                        if (ds.Tables[0].Columns.IndexOf("PasswordFTP") != -1)
                        {
                            PasswordFTP = oCrypto.Decript(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("PasswordFTP")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("BluetoothMAC") != -1)
                        {
                            SetMACList(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("BluetoothMAC")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("AutoCloseTime") != -1)
                        {
                            AutoCloseTime = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("AutoCloseTime")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("DefaultRouteValue") != -1)
                        {
                            DefaultRoute = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("DefaultRouteValue")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("DefaultRunValue") != -1)
                        {
                            DefaultRun = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("DefaultRunValue")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("TimeFormat") != -1)
                        {
                            TimeFormat = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("TimeFormat")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("DefaultDirectionValue") != -1)
                        {
                            DefaultDirection = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("DefaultDirectionValue")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("Password") != -1)
                        {
                            Password = oCrypto.Decript(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("Password")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("RouteEnabled") != -1)
                        {
                            DefaultValue = ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("RouteEnabled")].ToString();
                            DefaultValue = DefaultValue.ToUpper();
                            if (DefaultValue == "YES")
                            {
                                RouteEnabled = true;
                            }
                            else
                            {
                                RouteEnabled = false;
                            }
                        }
                        if (ds.Tables[0].Columns.IndexOf("RunEnabled") != -1)
                        {
                            DefaultValue = ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("RunEnabled")].ToString();
                            DefaultValue = DefaultValue.ToUpper();
                            if (DefaultValue == "YES")
                            {
                                RunEnabled = true;
                            }
                            else
                            {
                                RunEnabled = false;
                            }
                        }
                        if (ds.Tables[0].Columns.IndexOf("DirectionEnabled") != -1)
                        {
                            DefaultValue = ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("DirectionEnabled")].ToString();
                            DefaultValue = DefaultValue.ToUpper();
                            if (DefaultValue == "YES")
                            {
                                DirectionEnabled = true;
                            }
                            else
                            {
                                DirectionEnabled = false;
                            }
                        }
                        if (ds.Tables[0].Columns.IndexOf("LastHLVersion") != -1)
                        {
                            LastHLVersion = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("LastHLVersion")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("LastALVersion") != -1)
                        {
                            LastALVersion = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("LastALVersion")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("LastCFVersion") != -1)
                        {
                            LastCFVersion = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("LastCFVersion")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("LastFTVersion") != -1)
                        {
                            LastFTVersion = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("LastFTVersion")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("LastPLVersion") != -1)
                        {
                            LastPLVersion = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("LastPLVersion")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("Volume") != -1)
                        {
                            Volume = (clsVolumen.Volumes)int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("Volume")].ToString());
                        }
                        if (ds.Tables[0].Columns.IndexOf("Brightness") != -1)
                        {
                            Brightness = (clsBrightness.Brightness)int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("Brightness")].ToString());
                        }
                    }
                    clsBrightness.setBackLight(Brightness);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }


    }
}
