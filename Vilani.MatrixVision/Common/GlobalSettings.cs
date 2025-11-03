using Expert.Common.Library.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Vilani.MatrixVision.Common
{

    static class GlobalSettings
    {



        private static string _globalVar = "";

        public static string GlobalVar
        {
            get { return _globalVar; }
            set { _globalVar = value; }
        }



        public static bool ShowReactanglesOnSourceImage { get; set; }

        public static bool IsManualImagesAreLoaded { get; set; }

        public static bool IsSoftwareTriggerMonitorStarted { get; set; }

        public static string ManulTriggerSourceImagesLocation { get; set; }

        public static bool IsCPUCycleTimeShow { get; set; }

        public static bool IsStartPortOnAppStart { get; set; }

        public static int ImageDeleteDuration { get; set; }

        private static string _okSendText = "OK";

        public static string OKSendText
        {
            get { return _okSendText; }
            set { _okSendText = value; }
        }

        private static string _notOkSendText = "NG";

        public static string NotOKSendText
        {
            get { return _notOkSendText; }
            set { _notOkSendText = value; }
        }


        private static int _referenceImagesSupported = 16;

        public static int TotalReferenceImagesSupported
        {
            get { return _referenceImagesSupported; }
            set { _referenceImagesSupported = value; }
        }


        private static int _memoryExceptionHandledBy = 1;

        public static int MemoryExceptionHandledBy
        {
            get { return _memoryExceptionHandledBy; }
            set { _memoryExceptionHandledBy = value; }
        }

        public static List<TeachImageConfigDTO> BlobConfigSettings { get; set; }
    }
}
