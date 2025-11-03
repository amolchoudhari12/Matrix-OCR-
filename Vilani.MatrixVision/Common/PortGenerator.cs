using Expert.Common.Library;
using Expert.Common.Library.DTOs;
using Expert.Common.Library.Enumrations;
using Expert.Common.Library.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Vilani.MatrixVision.DataBase;

namespace Vilani.MatrixVision.Common
{
    public class PortGenerator
    {
        private DataBaseAccess _dbAccess = new DataBaseAccess();

        ILog logger = LogManager.GetLogger("RollingFile");

        public CommunicationPorts GetPortForCommunication()
        {
            CommunicationPorts result;
            if (FeatureSettings.SerialPortCommunicationsSupported)
            {
                result = this.CreateNewSerialPort();
            }
            else if (FeatureSettings.TCPIPCommunicationsSupported)
            {
                result = this.CreateNewTcpIPPort();
            }
            else
            {
                result = null;
            }
            return result;
        }

        private CommunicationPorts CreateNewTcpIPClientPort()
        {
            VilaniTCPIPCommunication currentTcpIPPort = null;

            try
            {
                TcpIPDTO tcpIPPort = _dbAccess.GetTcpIPPortData((int)TCPIPConnectionType.Both);
                TcpIPDTO tcpIPPortWrite = null;

                if (tcpIPPort == null)
                {
                    tcpIPPort = _dbAccess.GetTcpIPPortData((int)TCPIPConnectionType.Read);
                    tcpIPPortWrite = _dbAccess.GetTcpIPPortData((int)TCPIPConnectionType.Write);
                    currentTcpIPPort = new VilaniTCPIPCommunication(tcpIPPort.IPAddress, Convert.ToInt32(tcpIPPort.PortNumber), tcpIPPortWrite.IPAddress, tcpIPPortWrite.PortNumber);
                }
                else
                {
                    currentTcpIPPort = new VilaniTCPIPCommunication(tcpIPPort.IPAddress, Convert.ToInt32(tcpIPPort.PortNumber), tcpIPPort.IPAddress, tcpIPPort.PortNumber);
                }


                if (tcpIPPort != null)
                {
                    GlobalSettings.OKSendText = tcpIPPort.OK;
                    GlobalSettings.NotOKSendText = tcpIPPort.NotOK;
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error while opening TCP IP port.", ex);
                throw ex;
            }
            // GC
            GC.KeepAlive(currentTcpIPPort);
            return currentTcpIPPort;
        }

        private CommunicationPorts CreateNewTcpIPPort()
        {
            TcpIPVision currentTcpIPPort = null;
            try
            {
                TcpIPDTO tcpIPPort = this._dbAccess.GetTcpIPPortData((int)TCPIPConnectionType.Read);
                TcpIPDTO tcpIPPortWrite = this._dbAccess.GetTcpIPPortData((int)TCPIPConnectionType.Write);

                if (tcpIPPort != null)
                {
                    GlobalSettings.OKSendText = tcpIPPort.OK;
                    GlobalSettings.NotOKSendText = tcpIPPort.NotOK;
                }
                currentTcpIPPort = new TcpIPVision(tcpIPPort.IPAddress, System.Convert.ToInt32(tcpIPPort.PortNumber));
            }
            catch (System.Exception ex)
            {
                this.logger.Error("Error while opening TCP IP port.", ex);
            }
            return currentTcpIPPort;
        }


        private CommunicationPorts CreateNewSerialPort()
        {
            SerialPortVision currentSerailPort = null;
            try
            {
                var _inputSerialPort = _dbAccess.GetSerialPortData(true);

                if (_inputSerialPort != null)
                {
                    GlobalSettings.OKSendText = _inputSerialPort.OK;
                    GlobalSettings.NotOKSendText = _inputSerialPort.NotOK;
                }

                var serialPort = new SerialPort(_inputSerialPort.PortNumber, _inputSerialPort.BaudRate, _inputSerialPort.Parity, _inputSerialPort.DataBits, _inputSerialPort.StopBits);
                currentSerailPort = new SerialPortVision(serialPort);


            }
            catch (Exception ex)
            {
                logger.Error("Error while opening Serial port.", ex);
            }
            return currentSerailPort;
        }
    }
}
