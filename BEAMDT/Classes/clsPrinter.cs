using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace BEAMDT.Classes
{
    class clsPrinter
    {
        /*! Este es un objeto privado para manejar la impresora
         *   
         */
        private SerialPort PrinterPort;

        /*!  Constructor de la clase Impresora
         *  \brief Esta clase construye un puerto serial para interactuar con la impresora termica de Fujistu
         *  La velocidad de comunicacion con la impresora es fijo a 19200
         *  \param COMPORT En esta cadena de texto se pasa el nombre del puerto serie que se utiliza con la impresora.
         */
        public clsPrinter(string COMPORT)
        {
            PrinterPort = new SerialPort(COMPORT, 19200);
            PrinterPort.Open();
            Thread.Sleep(30);
        }

        public void ImpresoraClose()
        {
            PrinterPort.Close();
        }


        //Metodos

        /*! \fn     public void PrintStringLine(string sBuffer)  
         *  \brief  Este metodo sirve para escribir una cadena de texto y al finalizar avanza una linea en el papel
         *          despues de enviar los parametros a la impresora realiza una pausa de 80 ms para esperar a que la impresora ejecute 
         *          el comando.
         *  \param  sBuffer Dato que se va a imprimir en el papel, el formato es string.
         */
        public void PrintStringLine(string sBuffer)
        {
            byte[] bBuffer = Encoding.UTF8.GetBytes(sBuffer);
            PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            Thread.Sleep(50);
            bBuffer[0] = 10;
            PrinterPort.Write(bBuffer, 0, 1);
            Thread.Sleep(80);
        }


        /*! \overload   public void PrintStringLine(byte[] baData, byte DataSize)
         *  \brief      Este metodo sirve para imprimir datos desde un arreglo de bytes.
         *  \param      baData Arreglo de bytes desde donde se van a tomar los datos para imprimir.
         *  \param      DataSize Tamaño del arreglo y/o cantidad de datos del arreglo a imprimir.
         */
        public void PrintStringLine(byte[] baData, byte DataSize)
        {

            PrinterPort.Write(baData, 0, DataSize);
            Thread.Sleep(30);
            baData[0] = 10;
            PrinterPort.Write(baData, 0, 1);
            Thread.Sleep(50);
        }

        /*! \fn     public void PrintText(string sBuffer)  
         *  \brief  Metodo que sirve para imprimir texto en una misma linea sin avanzar un espacio en el papel
         *  \param  sBuffer Dato que se va a imprimir en el papel, el formato es string.
         */
        public void PrintText(string sBuffer)
        {
            byte[] bBuffer = Encoding.UTF8.GetBytes(sBuffer);
            PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            Thread.Sleep(50);
        }

        const int LF = 0x0A;
        const int ESC = 0x1B;
        const int GS = 0x1D;
        const int RS = 0x1E;
        const int US = 0x1F;
        const int FS = 0x1C;
        //JUST
        const int Izquierda = 0;
        const int Centro = 1;
        const int Derecha = 2;
        //CODE_BARR
        const int UPCA = 65;
        const int UPCE = 66;
        const int JAN13 = 67;
        const int JAN8 = 68;
        const int CODE39 = 69;
        const int ITF = 70;
        const int CODABAR = 71;
        const int CODE128 = 73;
        ///////////////////////////////BARCODE HEIGHT///////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        //                               COMANDO 79                                   //
        //                                GS h + n                                    //
        ////////////////////////////////////////////////////////////////////////////////
        //                       1=<Tam_Ver_Cod_Barras=<255                           //
        ////////////////////////////////////////////////////////////////////////////////
        public void Bar_Ver_Size(byte Tam_Ver_Cod_Barras)
        {
            byte[] buf = new byte[3];

            //PUTC(GS, Impresora);
            buf[0] = 29;
            //PrinterPort.Write(buf, 0, 1);

            //PUTC('h', Impresora);
            buf[1] = 104;
            //PrinterPort.Write(buf, 0, 1);

            //PUTC(Tam_Ver_Cod_Barras, Impresora);	
            buf[2] = Tam_Ver_Cod_Barras;
            PrinterPort.Write(buf, 0, 3);

        }

        ////////////////////////HRI CHARACTER FONT SELECTION////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        //                              COMANDO 78                                    //
        ////////////////////////////////////////////////////////////////////////////////
        //                                Tamaño                                      //
        //                           Tamano=0   8x16 Dots                             //
        //                           Tamano=1  12x24 Dots                             //
        //                           Tamano=4  24x40 Dots                             //
        //                           Tamano=5  24x48 Dots                             //
        //                           Tamano=6  36x60 Dots                             //
        //                           Tamano=7  Enlarged Numeric                       //
        ////////////////////////////////////////////////////////////////////////////////
        public void Tam_Txt_Cod_Barr(byte Tamano)
        {
            byte[] buf = new byte[4];
            //PUTC(GS, Impresora);
            buf[0] = GS;
            //PrinterPort.Write(buf, 0, 1);

            //PUTC('f', Impresora);
            buf[1] = (byte)'e';
            //PrinterPort.Write(buf, 0, 1);

            //PUTC(Tamano, Impresora);
            buf[2] = 60;
            buf[3] = 60;
            PrinterPort.Write(buf, 0, 4);
            Thread.Sleep(30);
        }


        /////////////////////////HRI CHARACTER PRINT POSITION///////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        //                                  COMANDO 71
        //                                   GS H + n
        ////////////////////////////////////////////////////////////////////////////////
        //Loc_Txt_Codigo_Barras=0  NO IMPRIMIR TEXTO                                  //
        //Loc_Txt_Codigo_Barras=1  IMPRIMIR TEXTO ARRIBA DEL CÓDIGO DE BARRAS         //
        //Loc_Txt_Codigo_Barras=2  IMPRIMIR TEXTO ABAJO DEL CÓDIGO DE BARRAS          //
        //Loc_Txt_Codigo_Barras=3  IMPRIMIR TEXTO ARRIBA Y ABAJO DEL CÓDIGO DE BARRAS //
        ////////////////////////////////////////////////////////////////////////////////
        public void Txt_Pos_CB(byte Loc_Txt_Codigo_Barras)
        {
            byte[] buf = new byte[3];
            //PUTC(GS, Impresora);
            buf[0] = ESC;
            //PrinterPort.Write(buf, 0, 1);

            //PUTC('H', Impresora);
            buf[1] = (byte)'C';
            //PrinterPort.Write(buf, 0, 1);

            //PUTC(Loc_Txt_Codigo_Barras, Impresora);
            buf[2] = Loc_Txt_Codigo_Barras;
            PrinterPort.Write(buf, 0, 3);
            Thread.Sleep(30);
        }
        public void Cod_Barr(byte[] Cod_Barr, byte Estandar)
        {
            byte[] buf = new byte[Cod_Barr.Length + 4];
            byte Cantidad_Datos = 0;
            byte Total_Datos;

            Total_Datos = (byte)Cod_Barr.Length;

            //buf[0] = 0xA;
            //PrinterPort.Write(buf, 0, 1);
            //System.Threading.Thread.Sleep(10);
            //PUTC(GS, Impresora);
            buf[0] = GS;
            //PrinterPort.Write(buf, 0, 1);
            //System.Threading.Thread.Sleep(10);
            //PUTC('k', Impresora);
            buf[1] = (byte)'k';
            //PrinterPort.Write(buf, 1, 1);
            //System.Threading.Thread.Sleep(10);
            //PUTC(Estandar, Impresora);
            buf[2] = Estandar;
            //PrinterPort.Write(buf, 2, 1);
            //System.Threading.Thread.Sleep(10);
            //PUTC(Total_Datos, Impresora);
            buf[3] = Total_Datos;
            // PrinterPort.Write(buf, 3, 1);
            //System.Threading.Thread.Sleep(10);



            while (Cantidad_Datos < Total_Datos)
            {

                //PUTC(Cod_Barr[Cantidad_Datos], Impresora);
                buf[Cantidad_Datos + 4] = (byte)Cod_Barr[Cantidad_Datos];

                //PrinterPort.Write(buf, Cantidad_Datos + 4, 1);
                Cantidad_Datos++;
                //System.Threading.Thread.Sleep(10);
            }
            //buf[Cantidad_Datos + 4] = 0;
            PrinterPort.Write(buf, 0, buf.Length);
            Thread.Sleep(70);
        }




        /*! \overload   public void PrintText(byte[] baData, byte DataSize)
         *  \brief      Metodo para imprimir datos desde un arreglo de bytes sin avanzar una linea en el papel.
         *  \param      baData Arreglo de bytes en donde se encuentran los datos a imprimir.
         *  \param      DataSize Tamaño del arreglo y/o cantidad de datos del arreglo a imprimir.
         */
        public void PrintText(byte[] baData, byte DataSize)
        {
            PrinterPort.Write(baData, 0, DataSize);
            Thread.Sleep(30);
        }


        /*! \fn     public void PaperFullCut()
         *  \brief  Metodo para realizar un corte completo del papel
         */
        public void PaperFullCut()
        {
            byte[] bBuffer = new byte[3] { 29, 86, 0 };
            PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            Thread.Sleep(30);
        }


        /*! \fn     public void PaperPartialCut()
         *  \brief  Metodo que se utiliza para realziar un corte parcial en el papel
         */
        public void PaperPartialCut()
        {
            byte[] bBuffer = new byte[3] { 29, 86, 1 };
            PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            Thread.Sleep(50);
        }

        public void SetLineSpacing(byte Spacing)
        {
            byte[] bBuffer = new byte[3] { 27, 65, Spacing };
            PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            Thread.Sleep(50);
        }

        public void SetHorizontalPrint(byte Spacing)
        {
            byte[] bBuffer = new byte[3] { 27, 65, Spacing };
            PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            Thread.Sleep(30);
        }

        public void SetPageLength(byte Lenght)
        {
            byte[] bBuffer = new byte[3] { 27, 67, Lenght };
            PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            Thread.Sleep(30);
        }


        /*! \fn     public void NLineFeed(byte Lines)
         *  \brief  Metodo que se utiliza para avanzar lineas en el papel dependiendo del parametro que se envie.
         *          El rango de operacion es de 0 ~ 255 lineas
         *  \param  Lines Numero de lineas a avanzar
         */
        public void NLineFeed(byte Lines)
        {
            byte[] bBuffer = new byte[3] { 27, 100, 1 };
            bBuffer[2] = Lines;
            PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            Thread.Sleep(50);
        }


        /*! \fn     public void BWReversePrinting(bool Active) 
         *  \brief  Metodo para activar o desactivar la impresion en reversa (blanca sobre fondo negro)
         *          Cuando la impresion en reversa esta activa la impresora escribe el fondo negro y 
         *          deja el contorno de la letra en blanco
         *  \param  Active Parametro boleano para activar o desactivar el modo de impresion de reversa.
         */
        public void BWReversePrinting(bool Active)
        {
            if (Active)
            {
                byte[] bBuffer = new byte[] { 27, 30 };
                PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            }
            else
            {
                byte[] bBuffer = new byte[] { 27, 31 };
                PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            }
            Thread.Sleep(50);
        }


        /*! \fn     public void TextSpecification(byte Active) 
         *  \brief  Metodo para cambiar la especificacion de alto y ancho del texto de la impresora
         *          el byte de parametro enviado es el siguiente:
         *  \par        b7  undefined
         *  \par        b6  undefined
         *  \par        b5  Double- Height
         *  \par        b4  Double Width
         *  \par        b3  undefined
         *  \par        b2 b1 b0
         *  \par        0  0  0: 08x16 puntos
         *  \par        0  0  1: 12x24 puntos
         *  \par        0  1  0: 16x16 puntos
         *  \par        0  1  1: 24x24 puntos
         *  \param  Active Formato del texto
         */
        public void TextSpecification(byte Active)
        {
            byte[] bBuffer = new byte[] { 27, 33, 0 };

            bBuffer[2] = Active;
            PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            Thread.Sleep(30);
        }


        //GS A+m+n

        /*! \fn     public byte GetPaperStatus()
         *  \brief  Metodo para obtener el estatus de la impresora
         *  \return Regresa el estatus del papel y si no tiene contestacion de la impresora regresa 255
         */
        public byte GetPaperStatus()
        {
            byte[] bBuffer = new byte[] { 28, 114, 0 };
            byte[] bStatus = new byte[4];

            PrinterPort.Write(bBuffer, 0, bBuffer.Length);
            Thread.Sleep(100);

            if (PrinterPort.BytesToRead == 4)
                PrinterPort.Read(bStatus, 0, bStatus.Length);
            else
                bStatus[2] = 255;
            return bStatus[2];
        }


        public string GeneraCadena()
        {
            //Primero hay que generar la cadena
            int SerieTicket = 1;
            int NumeroCamion = 1;
            int TempRoute = 1;
            string CodigoBarras = "";
            int CheckSum = 0;
            DateTime FechaLimite = DateTime.Now;
            CodigoBarras = CodigoBarras + FechaLimite.ToString("dd");
            CodigoBarras = CodigoBarras + FechaLimite.ToString("MM");
            if ((FechaLimite.Year - 2000).ToString().Length < 2)
            {
                CodigoBarras = CodigoBarras + "0" + (FechaLimite.Year - 2000).ToString();
            }
            else
            {
                CodigoBarras = CodigoBarras + (FechaLimite.Year - 2000).ToString();
            }
            CodigoBarras = CodigoBarras + FechaLimite.ToString("HH");
            CodigoBarras = CodigoBarras + FechaLimite.ToString("mm");

            int iCont;
            string RutaPrint = "";
            string SeriePrint = "";
            for (iCont = 0; iCont < 6 - NumeroCamion.ToString().Length; iCont++)
            {
                CodigoBarras = CodigoBarras + "0";
            }
            CodigoBarras = CodigoBarras + NumeroCamion.ToString();
            for (iCont = 0; iCont < 6 - TempRoute.ToString().Length; iCont++)
            {
                CodigoBarras = CodigoBarras + "0";
                RutaPrint = RutaPrint + "0";
            }
            CodigoBarras = CodigoBarras + TempRoute.ToString();
            RutaPrint = RutaPrint + TempRoute.ToString();
            for (iCont = 0; iCont < 4 - SerieTicket.ToString().Length; iCont++)
            {
                CodigoBarras = CodigoBarras + "0";
                SeriePrint = SeriePrint + "0";
            }
            //DatosTarifa.Ticket = clsConfig.SerieTicket;
            SeriePrint = SeriePrint + SerieTicket;
            CodigoBarras = CodigoBarras + SerieTicket.ToString();
            SerieTicket++;
            if (SerieTicket == 10000)
            {
                SerieTicket = 1;
            }
            for (iCont = 0; iCont < 13; iCont++)
            {
                CheckSum = CheckSum ^ int.Parse(CodigoBarras.Substring((iCont * 2), 2));
            }
            if (CheckSum.ToString().Length < 2)
            {
                CodigoBarras = CodigoBarras + "0" + CheckSum.ToString();
            }
            else
            {
                CheckSum = CheckSum % 100;
                if (CheckSum.ToString().Length < 2)
                {
                    CodigoBarras = CodigoBarras + "0" + CheckSum.ToString();
                }
                else
                {
                    CodigoBarras = CodigoBarras + CheckSum.ToString();
                }
            }
            return CodigoBarras;
        }
    }
}
