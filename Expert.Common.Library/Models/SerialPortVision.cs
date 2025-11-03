
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace Expert.Common.Library
{
    public class SerialPortVision : CommunicationPorts
    {
        public SerialPortVision(SerialPort serialPort)
        {
            this.SerialPort = serialPort;
            this.Type = Enumrations.CommunicationPortType.Serial;
        }

        public SerialPort SerialPort { get; set; }

        public override void Write(string message)
        {
            if (SerialPort.IsOpen)
            {
                SerialPort.Write(message);
            }
        }


        public override void Open()
        {
            if (SerialPort != null)
            {
                SerialPort.Open();
            }
        }

        public override void Close()
        {
            if (SerialPort != null && SerialPort.IsOpen)
            {
                SerialPort.DiscardInBuffer();
                SerialPort.DiscardOutBuffer();
                SerialPort.Close();
            }
        }

        public override bool IsOpen()
        {
            if (SerialPort != null)
            {
                return SerialPort.IsOpen;
            }
            return false;
        }

        public override string DisplayText()
        {
            return string.Format("Serial Port Configuration: {0}, {1}, {2}, {3}, {4}", new object[]
                            {
                                SerialPort.PortName,
                                SerialPort.BaudRate,
                                Convert.ToInt32(SerialPort.Parity),
                                SerialPort.DataBits,
                                Convert.ToInt32(SerialPort.StopBits)
                            });
        }

        public override void AcceptClient()
        {
            throw new NotImplementedException();
        }

        public override void CloseClient()
        {
            throw new NotImplementedException();
        }

        public override string ReadNext()
        {
            string receivedModel = string.Empty;
            int bytesToRead = this.SerialPort.BytesToRead;

            if (bytesToRead > 0)
            {
                byte[] array = new byte[bytesToRead];

                this.SerialPort.Read(array, 0, bytesToRead);
                receivedModel = Encoding.ASCII.GetString(array);
            }

            return receivedModel;
        }
    }
}
