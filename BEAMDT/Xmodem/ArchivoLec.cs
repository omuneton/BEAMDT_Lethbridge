using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Xmodem
{
    public class ArchivoLec
    {

        private FileStream fsArchivo;
        private string sArchivoRuta;
        private Crc16Ccitt Crc;

        public ArchivoLec()
        {
            Crc = new Crc16Ccitt(InitialCrcValue.Zeros);
        }

        public bool boAsignar_Archivo(string sArchivoRuta)
        {
            this.sArchivoRuta = sArchivoRuta;
            if (File.Exists(sArchivoRuta))
                return true;
            else
                return false;
        }


        public Paquetes Generar_ListaPaquete()
        {
            Paquetes ListaPaquetes = new Paquetes();
            int s32bytesLeidos;
            byte[] baTemp = new byte[1024];
            ushort crcTemp;


            fsArchivo = new FileStream(sArchivoRuta, FileMode.Open, FileAccess.Read);

            do
            {
                DatagramaXModem1K dtDatagrama = new DatagramaXModem1K();
                s32bytesLeidos = fsArchivo.Read(baTemp, 0, 1024);
                if (s32bytesLeidos > 0)
                {
                    dtDatagrama.bStx = (byte)XModem.STX;
                    dtDatagrama.bNum = (byte)(ListaPaquetes.Count + 1);
                    dtDatagrama.bCnm = (byte)~dtDatagrama.bNum;
                    dtDatagrama.baDatos = new byte[1024];

                    if (s32bytesLeidos < 1024)
                    {
                        for (int i = 0; i < 1024; i++)
                        {
                            dtDatagrama.baDatos[i] = 0x1A;
                        }
                    }
                    Array.Copy(baTemp, dtDatagrama.baDatos, s32bytesLeidos);

                    crcTemp = Crc.computeCRC16C(ref dtDatagrama.baDatos, 0, 1024);
                    dtDatagrama.u16Crc = crcTemp;
                    ListaPaquetes.Add(dtDatagrama);
                }
            } while (s32bytesLeidos != 0);

            fsArchivo.Close();
            fsArchivo.Dispose();



            return ListaPaquetes;
        }
    }
}
