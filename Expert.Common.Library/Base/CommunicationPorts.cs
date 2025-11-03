using Expert.Common.Library.Enumrations;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Expert.Common.Library
{
    public abstract class CommunicationPorts
    {
        public static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CommunicationPortType Type { get; set; }

        public abstract void Write(string message);

        public abstract void Open();

        public abstract void Close();

        public abstract bool IsOpen();

        public abstract string DisplayText();

        public abstract void AcceptClient();

        public abstract void CloseClient();

        public abstract string ReadNext();

    }
}