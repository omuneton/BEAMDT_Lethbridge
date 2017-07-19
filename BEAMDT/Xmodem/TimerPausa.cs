//*******************************************************************************************************************************************
//
//!     \file       TimerPausa.cs
//!     \brief      Libreria para manejar tiempos de espera para las maquinas de transiciones
//!     \version    V1.0.0.1     
//!     \date       15-09-2015
//!     \autor      Ing. Ismael Antonio Garcia Ramirez 
//
//      fix         se agrego la condicion de compilacion para poder utilizar la libreria ya sea en PC o GHI
//      fix         se cambio el namespace para hacer la libreria mas generica  
//               
//      habilite #define G120 cuando utilice la libreria para la plataforma GHI G120
//      habilite #define PC cuando utlice la libreria para una aplicacion de PC
//
//*********************************************************************************************************************************************

using System;
#if MF_FRAMEWORK_VERSION_V4_2
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
#endif

namespace Xmodem
{
    
    public struct stTimerPausa
    {
        //private long ticks;
        private TimeSpan tsTiempoFinal;
        public void vfnAsignar_Tiempo(int milisegundos)
        {
            TimeSpan tsTemp = new TimeSpan(0,0,0,0,milisegundos);
#if MF_FRAMEWORK_VERSION_V4_2
            tsTiempoFinal = Utility.GetMachineTime().Add(tsTemp);          
#else
             tsTiempoFinal = DateTime.Now.TimeOfDay.Add(tsTemp);
#endif
        }

        public long s64Checar_Tiempo()
        {
#if MF_FRAMEWORK_VERSION_V4_2
            TimeSpan tsTemp = Microsoft.SPOT.Hardware.Utility.GetMachineTime();         
#else
            TimeSpan tsTemp = DateTime.Now.TimeOfDay;
#endif
            return  tsTiempoFinal.Ticks - tsTemp.Ticks; 
        }

    }

}
