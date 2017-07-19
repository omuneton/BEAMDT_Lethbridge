using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace BEAMDT.Classes
{
    class clsStatus
    {
        private string Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Status.xml";
        public void WriteStatus()
        {
            //System.IO.File.Delete(Path);
            //System.IO.FileStream F = System.IO.File.Create(Path);
            //F.Close();
            try
            {
                using (XmlTextWriter Writer = new XmlTextWriter(Path, Encoding.Default))
                {
                    Writer.WriteStartDocument(true);
                    Writer.WriteStartElement("Project");
                    Writer.WriteElementString("SoftwareVersion", Program.SoftwareVersion.ToString());
                    Writer.WriteElementString("TimeStamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    Writer.WriteElementString("Shift", Program.VueltaAbierta.ToString());
                    Writer.WriteElementString("BusNumber", Program.BusNumber.ToString());
                    Writer.WriteElementString("Service", Program.Service.ToString());
                    Writer.WriteElementString("FareboxVersion", Program.FareboxVersion);
                    Writer.WriteElementString("SafeZone", Program.ZonaSegura.ToString());
                    Writer.WriteElementString("MenuOn", (Program.MenuOn.ToString()) );
                    Writer.WriteEndElement();
                    Writer.WriteEndDocument();
                    Writer.Flush();
                    Writer.Close();
                }
            }
            catch 
            { 
            
            }
            
            
        }
        private string MyStatusPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\DaemonStatus.xml";
        public bool ReadStatus(ref int FTPSync)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(MyStatusPath);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Columns.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Columns.IndexOf("FTPSync") != -1)
                        {
                            FTPSync = int.Parse(ds.Tables[0].Rows[0].ItemArray[ds.Tables[0].Columns.IndexOf("FTPSync")].ToString());
                        }
                    }
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }
    }
}
