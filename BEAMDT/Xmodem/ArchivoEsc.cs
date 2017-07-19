using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xmodem
{
    public class ArchivoEsc
    {

        private FileStream fsArchivo;

        private string sArchivoRuta;

        public ArchivoEsc(string sArchivoRuta)
        {
            this.sArchivoRuta = sArchivoRuta;
        }

        public void vfnIniciar_Archivo()
        {
            fsArchivo = new FileStream(sArchivoRuta, FileMode.Create, FileAccess.Write);
        }

        public void vfnEscribir_Paquete(byte[] baDatos, int s32Inicio, int s32Cantidad)
        {
            fsArchivo.Write(baDatos, s32Inicio, s32Cantidad);   
        }


        public void vfnTerminar_Archivo()
        {
            fsArchivo.Flush();
            fsArchivo.Close();
            fsArchivo.Dispose();
        }
    }
}
