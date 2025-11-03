using Expert.Common.Library.Enumrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vilani.MatrixVision.Common
{
    public static class FeatureSettings
    {
        private static bool _tcPIPCommunicationsSupported = true;

        public static bool TCPIPCommunicationsSupported
        {
            get { return _tcPIPCommunicationsSupported; }
            set { _tcPIPCommunicationsSupported = value; }
        }

        private static bool _serialPortCommunicationsSupported = false;

        public static bool SerialPortCommunicationsSupported
        {
            get { return _serialPortCommunicationsSupported; }
            set { _serialPortCommunicationsSupported = value; }
        }


        private static bool _tcpIPClientCommunicationsSupported = true;

        public static bool TCPIPClientCommunicationsSupported
        {
            get { return _tcpIPClientCommunicationsSupported; }
            set { _tcpIPClientCommunicationsSupported = value; }
        }

        private static bool _isReportingSupported = false;

        public static bool IsReportingSupprted
        {
            get { return _isReportingSupported; }
            set { _isReportingSupported = value; }
        }


        private static VisionInspectionType _visionInspectionType = Expert.Common.Library.Enumrations.VisionInspectionType.SinglePatternMatching;


        public static VisionInspectionType VisionInspectionType
        {
            get { return _visionInspectionType; }
            set { _visionInspectionType = value; }
        }
    }
}
