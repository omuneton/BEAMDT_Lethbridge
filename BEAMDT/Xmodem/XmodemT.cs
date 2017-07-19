using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;



namespace Xmodem
{
    

    public class XmodemT
    {

        private Crc16Ccitt crcXmodem;
        private enum EstadosXmodemT
        {
            INICIO,
            ESPERA,
            PAQUETE,
            REENVIO,
            ABORTADO,
            TIMEOUT,
            FINALIZADO,

        }


        private EstadosXmodemT estadosXM;
        private SerialPort spPuertoModem;
        private List<DatagramaXModem1K> listaPaquetes;
        public XmodemT(SerialPort sp, String FilePath)
        {
            boAsignar_Archivo(FilePath);
            boAsignar_PuertoCom(sp);
        }

        /// <summary>
        /// Metodo que asigna el puerto de comunicaciones serial, debe estar abierto
        /// </summary>
        /// <param name="puertoXmodem">Clase del puerto serie </param>
        /// <returns>regresa true si el puerto ya esta abierto si no false</returns>
        public bool boAsignar_PuertoCom(SerialPort puertoXmodem)
        {
            spPuertoModem = puertoXmodem;           
            if(puertoXmodem != null)
            {
                //if (puertoXmodem.IsOpen)
                //{
                //    puertoXmodem.Close();
                //    return true;
                //}
            }
            return false;
        }

        /// <summary>
        /// Metodo para asignar a la rutina del XmodemT un archivo que sera transferido
        /// </summary>
        /// <param name="sArchivoRuta">nombre del archivo y ruta </param>
        /// <returns>regresa TRUE si se encontro el archivo, en caso contrario FALSE</returns>
        public bool boAsignar_Archivo(string sArchivoRuta)
        {           
            ArchivoLec oArchivo = new ArchivoLec();
            if (oArchivo.boAsignar_Archivo(sArchivoRuta))
            {
                listaPaquetes = oArchivo.Generar_ListaPaquete();
                return true;
            }
            else
                return false;            
        }
        

        /// <summary>
        /// Metodo donde se ejecuta la rutina de XmodemT 
        /// 
        /// tiene un tiempo de escape de 60  segundos de la rutina antes de que el receptor envie el primer byte
        /// tiene un tiempo de escape de 10 segundos entre cada ACK o NACK recibido
        /// </summary>
        /// <returns>regresa 0 si la rutina se cumplio satisfactoriamento</returns>
        public int Procesar_XmodemT()
        {
            estadosXM = EstadosXmodemT.INICIO;
            int s32lCantidadPaquetes = 0;
            int s32lContadorPaquetes = 0;
            byte[] baPaquete;
            byte bComando;
            int s32lReintentos = 0;
            int s32Resultado = 0;
            bool finalizado = false;

            stTimerPausa timerSalida = new stTimerPausa();
            
            while(finalizado == false )
            {
                switch(estadosXM)
                {
                    case EstadosXmodemT.INICIO:
                        if (BEAMDT.Program.MenuOn == 1)
                        {
                            return -2;
                        }
                        s32lCantidadPaquetes = listaPaquetes.Count;
                        s32lContadorPaquetes = 1;
                        spPuertoModem.DiscardInBuffer();
                        //s32lContadorPaquetes++;
                        estadosXM = EstadosXmodemT.ESPERA;
                        timerSalida.vfnAsignar_Tiempo(60000);

                    break;

                    case EstadosXmodemT.ESPERA:
                        if (BEAMDT.Program.MenuOn == 1)
                        {
                            return -2;
                        }
                        if(spPuertoModem.BytesToRead > 0)
                        {
                            timerSalida.vfnAsignar_Tiempo(10000);
                            bComando = (byte)spPuertoModem.ReadByte();
                            if(bComando == (byte)'C')
                            {
                                estadosXM = EstadosXmodemT.PAQUETE;
                            }
                            else if(bComando == (byte)XModem.ACK)
                            {
                                s32lContadorPaquetes++;
                                estadosXM = EstadosXmodemT.PAQUETE;
                            }
                            else if(bComando == (byte)XModem.NACK)
                            {
                                estadosXM = EstadosXmodemT.REENVIO;
                            }
                            else if(bComando == (byte)XModem.EOT)
                            {
                                estadosXM = EstadosXmodemT.FINALIZADO;
                            }
                            else
                            {
                                
                            }
                            spPuertoModem.DiscardInBuffer();   
                        }
                        else
                        {
                            if(timerSalida.s64Checar_Tiempo() <= 0)
                                estadosXM = EstadosXmodemT.ABORTADO;
                        }

                    break;

                    case EstadosXmodemT.PAQUETE:
                    if (BEAMDT.Program.MenuOn == 1)
                    {
                        return -2;
                    }
                        s32lReintentos = 0;
                        if (s32lContadorPaquetes <= s32lCantidadPaquetes)
                        {
                                                   
                            baPaquete = DataGrama.vfnObtener_Bytes(listaPaquetes[s32lContadorPaquetes - 1]);
                            spPuertoModem.Write(baPaquete, 0, baPaquete.Length);
                            
                            estadosXM = EstadosXmodemT.ESPERA;
                        }
                        else
                        {
                            estadosXM = EstadosXmodemT.FINALIZADO;
                        }
                       
                    break;

                    case EstadosXmodemT.ABORTADO:

                        s32Resultado = -2;
                        estadosXM = EstadosXmodemT.FINALIZADO;

                    break;

                    case EstadosXmodemT.REENVIO:
                    if (BEAMDT.Program.MenuOn == 1)
                    {
                        return -2;
                    }
                    if (s32lReintentos < 5)
                    {
                        baPaquete = DataGrama.vfnObtener_Bytes(listaPaquetes[s32lContadorPaquetes - 1]);
                        spPuertoModem.Write(baPaquete, 0, baPaquete.Length);
                        s32lReintentos++;

                        estadosXM = EstadosXmodemT.ESPERA;
                    }
                    else
                        estadosXM = EstadosXmodemT.TIMEOUT;
                            
                    break;

                    case EstadosXmodemT.FINALIZADO:

                        byte[] baTemp = new byte[1];
                        if (s32Resultado == 0)
                            baTemp[0] = (byte)XModem.EOT;
                        else
                            baTemp[0] = (byte)XModem.CAN;
                        
                        spPuertoModem.Write(baTemp, 0, baTemp.Length);
                        finalizado = true;

                    break;


                    case EstadosXmodemT.TIMEOUT:

                        s32Resultado = -1;
                        estadosXM = EstadosXmodemT.FINALIZADO;

                    break;
                }

            }
            //spPuertoModem.Close();
            return s32Resultado;

        }



    }
}
