using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Expert.Common.Library.Models
{
    public class VilaniTCPIPCommunication : CommunicationPorts
    {
        private TcpClient client = null;
        private NetworkStream nwStream = null;

        private TcpClient clientSender = null;
        private NetworkStream nwStreamSender = null;

        public int PortNumber { get; set; }
        public string IPAddressString { get; set; }

        public int PortNumberSender { get; set; }
        public string IPAddressStringSender { get; set; }

        public VilaniTCPIPCommunication(string ipAddress, int portNumber, string ipAddressForSend, int portNumberForSend)
        {
            try
            {
                this.PortNumber = portNumber;
                this.IPAddressString = ipAddress;
                this.IPAddressStringSender = ipAddressForSend;
                this.PortNumberSender = portNumberForSend;
                client = new TcpClient(this.IPAddressString, this.PortNumber);
                nwStream = client.GetStream();

                clientSender = new TcpClient(this.IPAddressStringSender, this.PortNumberSender);
                nwStreamSender = clientSender.GetStream();


                GC.KeepAlive(clientSender);
                GC.KeepAlive(nwStreamSender);

                GC.KeepAlive(client);
                GC.KeepAlive(nwStream);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public VilaniTCPIPCommunication()
        {

        }
        //---create a TCPClient object at the IP and port no.---


        public override string ReadNext()
        {
            try
            {
                if (!this.IsOpen())
                    this.Open();

                byte[] buffer = new byte[client.ReceiveBufferSize];


                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = Encoding.Unicode.GetString(buffer, 0, bytesRead);

                return dataReceived;

            }
            catch (IOException ex)
            {
                logger.Error(ex.Message);
                nwStream.Flush();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                nwStream.Flush();
            }
            return string.Empty;

        }

        public override void Write(string message)
        {
            try
            {
                if (!IsSenderOpen())
                    OpenSender();

                byte[] MyMessage = System.Text.Encoding.UTF8.GetBytes(message);
                nwStreamSender.Write(MyMessage, 0, MyMessage.Length);
            }
            catch (OutOfMemoryException ex)
            {
                logger.Error(ex.Message);
                nwStreamSender.Flush();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                nwStreamSender.Flush();
            }

        }

        private bool IsSenderOpen()
        {

            if (clientSender != null && nwStreamSender != null)
                return true;

            return false;
        }

        public override void Open()
        {
            try
            {
                client = new TcpClient(this.IPAddressString, this.PortNumber);
                nwStream = client.GetStream();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void OpenSender()
        {
            try
            {
                clientSender = new TcpClient(this.IPAddressString, this.PortNumber);
                nwStreamSender = clientSender.GetStream();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public override void Close()
        {
            if (client != null)
                client.Close();
            if (nwStream != null)
                nwStream.Close();


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
                               IPAddressString,
                                PortNumber                            });
        }

        public override void AcceptClient()
        {
            throw new NotImplementedException();
        }

        public override void CloseClient()
        {

        }


    }
}
