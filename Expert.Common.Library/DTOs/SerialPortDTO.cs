using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Expert.Common.Library.DTOs
{
    public class SerialPortDTO
    {
        public string PortNumber { get; set; }

        public int BaudRate { get; set; }

        public Parity Parity { get; set; }

        public StopBits StopBits { get; set; }

        public int DataBits { get; set; }

        public string OK { get; set; }

        public string NotOK { get; set; }

        public int iErrCode { get; set; }

        public bool IsInputPort { get; set; }
    }
}
