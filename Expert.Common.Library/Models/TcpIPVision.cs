using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace Expert.Common.Library.Models
{
    public class TcpIPVision : CommunicationPorts
    {
        public IPAddress IPAddress { get; set; }
        public int PortNumber { get; set; }


        public bool IsOpenFlag { get; set; }

        public TcpListener TcpListener = null;
        private TcpClient client = null;
        private NetworkStream nwStream = null;

        private bool _isAcceptingStarted = false;

        public TcpIPVision(string ipAddress, int portNumber)
        {
            IPAddress = IPAddress.Parse(ipAddress);

            TcpListener = new TcpListener(IPAddress, portNumber);
            Type = Enumrations.CommunicationPortType.TcpIP;
            PortNumber = portNumber;
            Task.Factory.StartNew(this.AcceptClient);
        }

        public override void Write(string message)
        {
            if (IsOpen())
            {
                // message = string.Format("({0})\n", message);
                logger.Info("Sending message out : " + message);
                byte[] MyMessage = System.Text.Encoding.UTF8.GetBytes(message);
                nwStream.Write(MyMessage, 0, MyMessage.Length);
            }
        }

        public override void Open()
        {
            if (TcpListener != null)
            {
                TcpListener.Start();
                IsOpenFlag = true;

            }
        }

        public override void AcceptClient()
        {
            try
            {
                while (true)
                {
                    if (TcpListener != null && IsOpenFlag)
                    {
                        client = TcpListener.AcceptTcpClient();
                        nwStream = client.GetStream();
                        _isAcceptingStarted = true;
                    }
                }
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
            }

        }

        public override void CloseClient()
        {
            if (client != null)
            {
                client.Close();
                _isAcceptingStarted = false;

            }
        }


        public override void Close()
        {
            if (TcpListener != null)
            {
                TcpListener.Stop();
                IsOpenFlag = false;

            }
        }

        public override bool IsOpen()
        {
            if (client != null && nwStream != null)
                return true;

            return false;
        }

        public override string DisplayText()
        {
            return string.Format("TCP-IP Port Configuration: IP : {0}, Port: {1}", new object[]
                            {
                               IPAddress,
                                PortNumber                            });
        }


        public override string ReadNext()
        {
            try
            {
                byte[] buffer = new byte[client.ReceiveBufferSize];

                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                return dataReceived;
            }
            catch (IOException ex)
            {
                this.Open();
            }
            catch (Exception ex)
            {
                IsOpenFlag = false;
            }

            return string.Empty;

        }
    }
}
