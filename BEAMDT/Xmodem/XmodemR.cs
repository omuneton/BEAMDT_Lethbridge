using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;

namespace Xmodem
{
    public class XmodemR
    {

        private enum EstadosXmodemR
        {
            INICIO,
            IDLE,
            RECIBIENDO,
            PAQUETE,
            VALIDACION,
            ABORTADO,
            TIMEOUT,
            FINALIZADO,
            NO_VALIDO,
            ENVIO_C,
        }
        private const int LARGO_BUFFER = 1029;

        private EstadosXmodemR EstadoXMR;
        private SerialPort spPuertoModem;
        private stTimerPausa timerEspera;
        private stTimerPausa timerSalida;
        private stTimerPausa timerEnvioC;
        private byte[] baBufferRx = new byte[LARGO_BUFFER];
        private int s32IndiceRx;
        private byte bNumeroPaquete;
        private Crc16Ccitt Crc;
        private ArchivoEsc Archivo;
        private bool boNoEnvioC;

        public XmodemR(SerialPort Sp, String FilePath)
        {
            boAsignar_PuertoCom(Sp);
            Asignar_Archivo(FilePath);
        }
        /// <summary>
        /// Metodo que asigna el puerto de comunicaciones serial, debe estar abierto
        /// </summary>
        /// <param name="puertoXmodem">Clase del puerto serie </param>
        /// <returns>regresa TRUE si el puerto ya esta abierto si no FALSE</returns>
        public bool boAsignar_PuertoCom(SerialPort puertoXmodem)
        {
            spPuertoModem = puertoXmodem;
            if (puertoXmodem != null)
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
        /// Metodo para asignar a la rutina del XmodemR el nombre del archivo que sera recibido
        /// 
        /// tiene un tiempo de escape de 60  segundos de la rutina antes de que el transmisor envie el primer byte
        /// tiene un tiempo de escape de 10 segundos entre cada paquete recibido o conmando de xmodem
        /// </summary>
        /// <param name="sArchivoRuta">nombre del archivo y ruta </param>
        public void Asignar_Archivo(string sArchivoRuta)
        {
            Archivo = new ArchivoEsc(sArchivoRuta);
        }
        
        /// <summary>
        /// Metodo donde se ejecuta la rutina de XmodemR
        /// </summary>
        /// <returns>regresa 0 si la rutina se cumplio satisfactoriamento</returns>
        public int Procesar_XmodemR()
        {
            int TimeOutGeneral = 6000;
            int TimeOutEnvio = 2000;
            //spPuertoModem.Open();
            int ContNoValido = 0;
            int s32lTemp;
            byte bTemp;
            byte[] balTemp;
            int s32lResultado = 0;
            bool boFinalizado = false;
            boNoEnvioC = false;
            while(boFinalizado == false)
            {
                
                switch (EstadoXMR)
                {
                    case EstadosXmodemR.INICIO:
                        if (BEAMDT.Program.MenuOn != 0)
                        {
                            return -2;
                        }
                        //Debug.WriteLine("Inicio:" + DateTime.Now.Ticks.ToString());
                        timerEspera = new stTimerPausa();
                        timerSalida = new stTimerPausa();
                        timerEnvioC = new stTimerPausa();
                        Crc = new Crc16Ccitt(InitialCrcValue.Zeros);
                        Archivo.vfnIniciar_Archivo();
                        s32lResultado = 0;
                        timerSalida.vfnAsignar_Tiempo(TimeOutGeneral);
                        
                        EstadoXMR = EstadosXmodemR.ENVIO_C;

                        break;

                    case EstadosXmodemR.ENVIO_C:
                        if (BEAMDT.Program.MenuOn != 0)
                        {
                            return -2;
                        }
                        //Debug.WriteLine("Envio C:" + DateTime.Now.Ticks.ToString());
                        if(timerEnvioC.s64Checar_Tiempo() <= 0)
                        {
                            balTemp = new byte[] { (byte)'C' };
                            spPuertoModem.Write(balTemp, 0, balTemp.Length);
                            timerEnvioC.vfnAsignar_Tiempo(100);

                            EstadoXMR = EstadosXmodemR.IDLE;
                        }
                        
                            

                        break;
                        
                    case EstadosXmodemR.IDLE:
                        if (BEAMDT.Program.MenuOn != 0)
                        {
                            return -2;
                        }
                        if(spPuertoModem.BytesToRead > 0)
                        {
                            boNoEnvioC = true;
                            s32IndiceRx = 0;
                            Array.Clear(baBufferRx, 0, baBufferRx.Length);
                            //timerEspera.vfnAsignar_Tiempo(50);
                            EstadoXMR = EstadosXmodemR.RECIBIENDO;
                        }
                        else
                        {
                            if(timerSalida.s64Checar_Tiempo() <= 0)
                            {
                                EstadoXMR = EstadosXmodemR.TIMEOUT;
                            }
                            else if(boNoEnvioC == false)
                            {
                                EstadoXMR = EstadosXmodemR.ENVIO_C;
                            }
                            
                        }
                        
                    
                        break;

                    case EstadosXmodemR.RECIBIENDO:
                        if (BEAMDT.Program.MenuOn != 0)
                        {
                            return -2;
                        }
                        //Debug.WriteLine("Recibiendo:" + DateTime.Now.Ticks.ToString());
                        if ((s32lTemp = spPuertoModem.BytesToRead) > 0)
                        {
                            //Debug.WriteLine("opcion1");
                            balTemp = new byte[s32lTemp];
                            spPuertoModem.Read(balTemp, 0, s32lTemp);
                            if (s32IndiceRx + s32lTemp > LARGO_BUFFER)
                            {
                                s32lTemp = LARGO_BUFFER - s32IndiceRx;
                            }
                            Array.Copy(balTemp, 0, baBufferRx, s32IndiceRx, s32lTemp);
                            s32IndiceRx = s32IndiceRx + s32lTemp;
                            //timerEspera.vfnSetear_Timer(50);
                            timerEspera.vfnAsignar_Tiempo(100);
                        }
                        else
                        {
                            //Debug.WriteLine("opcion2");
                            System.Threading.Thread.Sleep(80);
                            if (spPuertoModem.BytesToRead == 0)
                            {
                                //if (timerEspera.s64Checar_Tiempo() <= 0)
                                //{
                                timerSalida.vfnAsignar_Tiempo(TimeOutEnvio);
                                EstadoXMR = EstadosXmodemR.PAQUETE;
                                //}
                            }
                        }

                        break;

                    case EstadosXmodemR.PAQUETE:
                        if (BEAMDT.Program.MenuOn != 0)
                        {
                            return -2;
                        }
                        //Debug.WriteLine("Paquete:" + DateTime.Now.Ticks.ToString());
                        if (baBufferRx[0] == (byte)XModem.STX)
                        {
                            bNumeroPaquete = baBufferRx[1];
                            bTemp = (byte)~bNumeroPaquete;

                            if (bTemp == baBufferRx[2])
                            {
                                EstadoXMR = EstadosXmodemR.VALIDACION;
                            }
                            else
                                EstadoXMR = EstadosXmodemR.NO_VALIDO;
                        }
                        else if (baBufferRx[0] == (byte)XModem.CAN)
                        {
                            EstadoXMR = EstadosXmodemR.ABORTADO;
                        }
                        else if (baBufferRx[0] == (byte)XModem.EOT)
                        {
                            EstadoXMR = EstadosXmodemR.FINALIZADO;
                        }
                        else
                            EstadoXMR = EstadosXmodemR.IDLE;

                        break;

                    case EstadosXmodemR.VALIDACION:
                        if (BEAMDT.Program.MenuOn != 0)
                        {
                            return -2;
                        }
                        //Debug.WriteLine("Validacion:" + DateTime.Now.Ticks.ToString());
                        s32lTemp = Crc.computeCRC16C(ref baBufferRx, 3, 1024);
                        ushort u16lCrcTrama = (ushort)((baBufferRx[1027] << 8) | baBufferRx[1028]);
                        if((ushort)s32lTemp == u16lCrcTrama)
                        {
                            ContNoValido = 0;
                            balTemp = new byte[1] { (byte)XModem.ACK };
                            spPuertoModem.Write(balTemp, 0, balTemp.Length);
                            
                            EstadoXMR = EstadosXmodemR.IDLE;
                            Archivo.vfnEscribir_Paquete(baBufferRx, 3, 1024);
                        }
                        else
                        {
                            EstadoXMR = EstadosXmodemR.NO_VALIDO;
                        }

                        break;

                    case EstadosXmodemR.NO_VALIDO:
                        if (BEAMDT.Program.MenuOn != 0)
                        {
                            return -2;
                        }
                        //Debug.WriteLine("No valido:" + DateTime.Now.Ticks.ToString());
                        balTemp = new byte[1] {(byte)XModem.NACK };
                        spPuertoModem.Write(balTemp, 0, balTemp.Length);
                        ContNoValido++;
                        if (ContNoValido > 5)
                        {
                            EstadoXMR = EstadosXmodemR.ABORTADO;
                        }
                        else
                        {
                            EstadoXMR = EstadosXmodemR.IDLE;
                        }
                        

                        break;

                    case EstadosXmodemR.ABORTADO:

                        //Debug.WriteLine("Abort:" + DateTime.Now.Ticks.ToString());
                        s32lResultado = -2;
                        EstadoXMR = EstadosXmodemR.FINALIZADO;

                        break;

                    case EstadosXmodemR.TIMEOUT:
                        //Debug.WriteLine("TimeOut:" + DateTime.Now.Ticks.ToString());
                        s32lResultado = -1;

                        EstadoXMR = EstadosXmodemR.FINALIZADO;

                        break;

                    case EstadosXmodemR.FINALIZADO:
                        //Debug.WriteLine("Finalizado:" + DateTime.Now.Ticks.ToString());
                        boFinalizado= true;
                        Archivo.vfnTerminar_Archivo();
                        break;
                }
                System.Threading.Thread.Sleep(10);
            }
            //spPuertoModem.Close();
            return s32lResultado;
        }
    }
}
