using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expert.Common.Library.DTOs
{
    public class TcpIPDTO
    {
        public int TcpIPID { get; set; }
        public int PortNumber { get; set; }
        public string ConnectionName { get; set; }
        public string IPAddress { get; set; }
        public int ConnectionType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string OK { get; set; }
        public string NotOK { get; set; }
    }
}
