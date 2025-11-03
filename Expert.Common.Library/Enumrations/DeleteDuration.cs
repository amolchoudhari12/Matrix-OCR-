using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expert.Common.Library.Enumrations
{
    public enum DeleteDuration
    {
        Never = 0,
        After1Day = 1,
        After2Day = 2,
        After3Day = 3,
        After4Day = 4,
        After5Day = 5,
        After6Day = 6,
        After7Day = 7,
        After1Week = 8,
        After2Week = 9,
        After3Week = 10,
        After1Month = 11,
        After2Month = 12,
        After3Month = 13,
        After4Month = 14,
        After5Month = 15,
        After6Month = 16,
        After9Month = 17,
        After1Year = 18,

    }

    public enum MeomoryExceptionHanndedBy
    {
        //     Clearing the resources (Without Data loss -Slower)
        //Application Restart (Data may loss -Faster)
        None = 0,
        ClearResources = 1,
        ApplicationRestart = 2

    }

    public enum TCPIPConnectionType
    {
        //     Clearing the resources (Without Data loss -Slower)
        //Application Restart (Data may loss -Faster)
       
        Read = 1,
        Write = 2,
        Both = 3

    }

    public enum CommunicationPortType
    {
        Serial,
        TcpIP,
        ModBus
    }

    public enum VisionInspectionType
    {
        SinglePatternMatching,
        SerialPatternMatching,
        BlobAnalysis,
        BlobAndSerialPatternMatching
    }

     public enum CropType
    {
        Left=1,
            Top=2,
            Right=3,
            Bottom=4
    }
}
