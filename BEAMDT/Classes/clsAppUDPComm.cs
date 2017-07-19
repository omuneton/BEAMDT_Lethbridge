using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace BEAMDT.Classes
{
    class clsAppUDPComm
    {
        IPEndPoint listenerIP = new IPEndPoint(IPAddress.Any, 65000);
        UdpClient listener;
        byte[] response = new byte[2];
        public void Listen()
        {
            listener = new UdpClient(listenerIP);
            try
            {
                listener.Client.BeginReceive(response,0,response.Length,SocketFlags.None,new AsyncCallback(recv),this);
            }
            catch
            { 
            
            }
        }
        //CallBack
        private void recv(IAsyncResult res)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 65001);
            listener.Client.EndReceive(res);
            byte[] data = new byte[1]; 
            switch (response[0])
            {
                case 1: //Service?
                    if(Program.VueltaAbierta==-1)
                    {
                        data[0] = 2;
                    }
                    else
                    {
                        if ((Program.VueltaAbierta + Program.Service + Program.MenuOn) == 0)
                        {
                            data[0] = 1;
                        }
                        else
                        {
                            data[0] = 2;
                        }
                    }
                    listener.Send(data, data.Length, RemoteIpEndPoint);
                    break;
                case 2: //Set Service?
                    Program.Synching = (int)response[1];
                    data[0] = 1;
                    listener.Send(data, data.Length, RemoteIpEndPoint);
                    break;
            }
            listener.Client.BeginReceive(response, 0, response.Length, SocketFlags.None, new AsyncCallback(recv), this);
        }


    }
}
