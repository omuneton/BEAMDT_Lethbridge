using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using System.Xml;
using System.Threading;

namespace BEAMDT.Classes
{

    public enum SonidosFarebox : byte
    {
        PASSBACK,
        TRANSFER,
        EXPIRED_CARD,
        INVALID_CARD,
        INSUFFICIENT_FUNDS,
        RETRY,
        EXPIRED_PASS,
        RELOADING,
        PRINTED_TICKET,
        INVALID_TICKET,
        INVALID_ROUTE,
        TRANSFER_TIMEOUT,
        WELCOME_ABOARD,
        RESTRICTED_PASS,
        RESTRICTED_RIDE,
        INVALID_CODE,
        INVALID_CODE2,
        WELCOME,
        WELCOME_ABOARD2,
        WELCOME_ABOARD_JENIFER,
        EXPIRED_TRANSFER,
        TONE_FAIL_120_600MS,
        TONE_FAIL_200_600MS,
        TONE_OK_600_300MS,
        TONE_OK_650_300MS,
        TONE_OK_700_300MS,
        TONE_OK_750_300MS,
        TONE_OK_800_300MS
       
    }
    public class SonidoFarebox
    {
        private static Timer timerTick;
        private string lsRutaArchivos;
        private SoundPlayer spSonido;
        private bool lu1ReproSonido;
        private string[] lsaArchivosSonido = { "PASSBACK.wav","TRANSFER.wav","expired_card.wav","invalid_card.wav","insufficient_funds.wav","RETRY.wav","expired_pass.wav",
                                               "reloading.wav","printed_ticket.wav","invalid_ticket.wav","invalid_route.wav","transfer_time_out.wav","welcome_aboard.wav",
                                               "restricted_pass.wav","restricted_ride.wav","InvalidCode.wav","invalid_code.wav","WELCOME.wav","welcome_aboard2.wav","welcome_aboard_jenifer.wav","Expired_Transfer.wav",
                                                "120_600ms.wav","200_600ms.wav","600_300ms.wav","650_300ms.wav","700_300ms.wav","750_300ms.wav","800_300ms.wav"};
        //private int[] ls32aTiemposSonidos = { 1500, 1300, 1300, 1300,1300,1300,1300,1300,1300,1300,1300,1300,1300,1300,1300,1300,1300,1300,1300,1300};
        private int[] ls32aTiemposSonidos = { 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300,300,300,300,300,300,300,300 };


        /// <summary>
        /// Constructor de la clase 
        /// inicializa el xml que contiene la ruta de los sonidos
        /// inicializa la clase SoundPlayer
        /// inicializa el evento del timer
        /// </summary>
        public SonidoFarebox()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load("\\Program Files\\TestFareboxWFApp\\sonidos.xml");

            lsRutaArchivos = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Sonidos"; ;
            spSonido = new SoundPlayer();

            timerTick = new Timer(new TimerCallback(Timer_Tick), null, Timeout.Infinite, Timeout.Infinite);
        }

        
        /// <summary>
        /// Metodo que reproduce de manera asincrona un sonido predeterminado y seleccionado mediante -SonidosFarebox-
        /// </summary>
        /// <param name="eSonidoFarebox">parametro para indicar cual es el sonido a reproducir</param>
        /// <returns>Valor 0 indica que se pudo reproducir, Valor -1 indica que no se pudo reproducir</returns>
        public int s32Reproducir_SonidoAsin(int indexSonido)
        {
            //SonidosFarebox eSonidoFarebox = GetSonidoFarebox(indexSonido);
            int s32TiempoSonido;
            try
            {
                if (lu1ReproSonido == false)
                {
                    if (System.IO.File.Exists(lsRutaArchivos + "\\" + lsaArchivosSonido[indexSonido]))
                    {
                        spSonido.SoundLocation = lsRutaArchivos + "\\" + lsaArchivosSonido[indexSonido];
                        s32TiempoSonido = ls32aTiemposSonidos[indexSonido];
                        spSonido.Load();
                        spSonido.Play();
                        lu1ReproSonido = true;
                        vfnIniciar_Timer(s32TiempoSonido);
                        return 0;
                    }
                    return 1;
                }
                else
                    return 1;
                
            }
            catch 
            {
                return -1;
                //SystemSounds.Exclamation.Play();
            }
            
        }

        /// <summary>
        /// Metodo que reproduce de manera sincrona un sonido predeterminado y seleccionado mediante -SonidosFarebox-
        /// </summary>
        /// <param name="eSonidoFarebox">parametro para indicar cual es el sonido a reproducir</param>
        /// <returns>Valor 0 indica que se pudo reproducir, Valor -1 indica que no se pudo reproducir</returns>
        public int s32Reproducir_SonidoSin(SonidosFarebox eSonidoFarebox)
        {
            try
            {
                if (lu1ReproSonido == false)
                {
                    spSonido.SoundLocation = lsRutaArchivos + "\\" + lsaArchivosSonido[(byte)eSonidoFarebox];
                    spSonido.Load();
                    spSonido.PlaySync();
                    return 0;
                }
                else
                    return 1;
                
            }
            catch 
            {
                return -1;
                //SystemSounds.Exclamation.Play();
            }
            //SystemSounds.Exclamation.Play();
        }


        /// <summary>
        /// Metodo para el evento del Timer
        /// </summary>
        /// <param name="timer"></param>
        private void Timer_Tick(object timer)
        {
            lu1ReproSonido = false;
        }

        /// <summary>
        /// Metodo para asignar el tiempo que se dispara el evento del timer
        /// </summary>
        /// <param name="s32MiliSegundos">Tiempo para disparar el evento</param>
        private void vfnIniciar_Timer(int s32MiliSegundos)
        {
            timerTick.Change(s32MiliSegundos, Timeout.Infinite);
        }

        /// <summary>
        /// Propiedad de lectura para saber si se esta reproduciendo un sonido o no
        /// </summary>
        public bool u1ReproSonido
        {
            get { return lu1ReproSonido; }
        }
    }
}
