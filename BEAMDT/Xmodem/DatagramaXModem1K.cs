using System;
using System.Collections.Generic;
using System.Text;

namespace Xmodem
{
    public class DataGrama
    {

        private int s32LargoDatos;
        public static byte[] vfnObtener_Bytes(DatagramaXModem1K stDatagrama)
        {
            byte[] baDatos = new byte[1029];

            baDatos[0] = stDatagrama.bStx;
            baDatos[1] = stDatagrama.bNum;
            baDatos[2] = stDatagrama.bCnm;

            Array.Copy(stDatagrama.baDatos, 0, baDatos, 3, 1024);

            baDatos[1027] = (byte)(stDatagrama.u16Crc >> 8);
            baDatos[1028] = (byte)(stDatagrama.u16Crc);
            //Array.Copy(BitConverter.GetBytes(stDatagrama.u16Crc), 0, baDatos, 1027 , 2) ;
            
            return baDatos;
        }
    }
    
    
    public struct DatagramaXModem1K
    {
        public byte bStx;
        public byte bNum;
        public byte bCnm;
        public byte[] baDatos;
        public ushort u16Crc;

       

        
    }
}
