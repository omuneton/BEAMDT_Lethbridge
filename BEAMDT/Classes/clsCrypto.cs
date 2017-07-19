using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace BEAMDT.Classes
{
    class clsCrypto
    {
        public DateTime GetNTPTime()
        {
            // 0x1B == 0b11011 == NTP version 3, client - see RFC 2030
            byte[] ntpPacket = new byte[] { 0x1B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            IPAddress[] addressList = Dns.GetHostEntry("pool.ntp.org").AddressList;

            if (addressList.Length == 0)
            {
                // error
                return DateTime.MinValue;
            }

            IPEndPoint ep = new IPEndPoint(addressList[0], 123);
            UdpClient client = new UdpClient();
            client.Connect(ep);
            client.Send(ntpPacket, ntpPacket.Length);
            byte[] data = client.Receive(ref ep);

            // receive date data is at offset 32
            // Data is 64 bits - first 32 is seconds - we'll toss the fraction of a second
            // it is not in an endian order, so we must rearrange
            byte[] endianSeconds = new byte[4];
            endianSeconds[0] = (byte)(data[32 + 3] & (byte)0x7F); // turn off MSB (some servers set it)
            endianSeconds[1] = data[32 + 2];
            endianSeconds[2] = data[32 + 1];
            endianSeconds[3] = data[32 + 0];
            uint seconds = BitConverter.ToUInt32(endianSeconds, 0);

            return (new DateTime(1900, 1, 1)).AddSeconds(seconds);
        }
        public string JSHash(string str)
        {
            uint hash = 1315423911;

            for (int i = 0; i < str.Length; i++)
            {
                hash ^= ((hash << 5) + str[i] + (hash >> 2));
            }
            string h = hash.ToString("X");
            if (h.Length == 8)
            {
                return h;
            }
            if (h.Length > 8)
            {
                return h.Substring(0, 8);
            }
            if (h.Length < 8)
            {
                int l = h.Length;
                for (int i = 0; i < 8 - l; i++)
                {
                    h = h + "0";
                }
                return h;
            }
            return h;
        }
        public string Encript(string Cad)
        {
            RijndaelManaged algoritmo = new RijndaelManaged();
            byte[] Key = new byte[16];
            Key[0] = (byte)'B';
            Key[1] = (byte)'E';
            Key[2] = (byte)'A';
            Key[3] = (byte)'1';
            Key[4] = (byte)'2';
            Key[5] = (byte)'3';
            Key[6] = (byte)'4';
            Key[7] = (byte)'5';
            Key[8] = (byte)'B';
            Key[9] = (byte)'E';
            Key[10] = (byte)'A';
            Key[11] = (byte)'1';
            Key[12] = (byte)'2';
            Key[13] = (byte)'3';
            Key[14] = (byte)'4';
            Key[15] = (byte)'5';
            MemoryStream memStream = new MemoryStream();
            ICryptoTransform EncryptorDecryptor = algoritmo.CreateEncryptor(Key, Key);
            CryptoStream crStream = new CryptoStream(memStream, EncryptorDecryptor, CryptoStreamMode.Write);
            StreamWriter strWriter = new StreamWriter(crStream);

            strWriter.Write(Cad);

            strWriter.Flush();
            crStream.FlushFinalBlock();
            byte[] pwd_byte;
            pwd_byte = new byte[memStream.Length];
            memStream.Position = 0;
            memStream.Read(pwd_byte, 0, (int)pwd_byte.Length);
            string pwd_str;


            pwd_str = System.Text.Encoding.Default.GetString(pwd_byte, 0, pwd_byte.Length);
            return pwd_str;
        }
        public string Decript(string Cad)
        {
            Rijndael Algorithm = new RijndaelManaged();
            byte[] Key = new byte[16];
            Key[0] = (byte)'B';
            Key[1] = (byte)'E';
            Key[2] = (byte)'A';
            Key[3] = (byte)'1';
            Key[4] = (byte)'2';
            Key[5] = (byte)'3';
            Key[6] = (byte)'4';
            Key[7] = (byte)'5';
            Key[8] = (byte)'B';
            Key[9] = (byte)'E';
            Key[10] = (byte)'A';
            Key[11] = (byte)'1';
            Key[12] = (byte)'2';
            Key[13] = (byte)'3';
            Key[14] = (byte)'4';
            Key[15] = (byte)'5';

            //creating new Memory stream as stream for input string      
            MemoryStream memStream = new MemoryStream(System.Text.Encoding.Default.GetBytes(Cad));

            //Decryptor creating 
            ICryptoTransform EncryptorDecryptor = Algorithm.CreateDecryptor(Key, Key);

            //setting memory stream position
            memStream.Position = 0;

            //creating new instance of Crupto stream
            CryptoStream crStream = new CryptoStream(memStream, EncryptorDecryptor, CryptoStreamMode.Read);

            //reading stream
            StreamReader strReader = new StreamReader(crStream);

            //returnig decrypted string
            return strReader.ReadToEnd();

        }
    }
}

