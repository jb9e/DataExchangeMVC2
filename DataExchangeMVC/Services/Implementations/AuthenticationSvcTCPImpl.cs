using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Sockets;
using System.Net;
using System.IO;
using DataExchangeMVC.Services.Interfaces;

namespace DataExchangeMVC.Services
{
    class AuthenticationSvcTCPImpl : IAuthenticationSvc
    {
        TcpClient _tcpClient = new TcpClient();
        IPEndPoint _endPoint = new IPEndPoint(IPAddress.Parse("127.0.01"), 2400);

        public bool Authenticate(string userName, string password)
        {
            try
            {
                _tcpClient.Connect(_endPoint);

                NetworkStream stream = _tcpClient.GetStream();

                string message = userName + "/" + password;

                //byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);

                //stream.Write(msg, 0, msg.Length);

                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);
                writer.Write(message);

                return reader.ReadBoolean();
            }
            catch
            {
                return false;
            }
            finally
            {
                _tcpClient.Close();
            }
        }
    }
}