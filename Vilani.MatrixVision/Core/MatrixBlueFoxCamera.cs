using log4net;
using mvIMPACT_NET;
using mvIMPACT_NET.acquire;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vilani.MatrixVision.Common;
using Expert.Common.Library;
using System.Threading.Tasks;

namespace Vilani.MatrixVision.Core
{

    internal class MatrixBlueFoxCamera : IDisposable
    {
        public delegate void BlueFoxModelReceivedNotificationHandler(List<Item> RefImageFiles, string ModelName, string plcValue);
        public event BlueFoxModelReceivedNotificationHandler ModelReceivedNotification;

        public delegate void ResetReportHandler(string modelValue, string plcValue);
        public event ResetReportHandler ResetReport;

        public delegate void ShowReportHandler(string modelValue, string plcValue);
        public event ShowReportHandler ShowReport;

        public delegate void BlueFoxCameraExceptionHandler(string Message);

        public delegate void ImageSavedEventHandler(string ImagePath);

        public delegate void PatternMatchDetectionHandler(List<PatternResult> lstResult);

        public delegate void UpdateLiveImageHandler(System.Drawing.Image img);

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private DeviceManager devMgr;

        private Device dev = null;

        private Window win = null;

        private VisionCameraThread liveCamThread = null;

        private Thread threadControl = null;

        private Thread threadControlDiMonitor = null;

        private Thread threadControlPatternMonitor = null;

        private Thread threadControlSoftwareTrigger = null;



        public event BlueFoxCameraExceptionHandler ExceptionRaised;

        public event ImageSavedEventHandler ImageSaved;

        public event PatternMatchDetectionHandler PatternMatchDetected;

        public event UpdateLiveImageHandler UpdateLiveImage;

        public delegate void ManualTriggerHandler();
        public event ManualTriggerHandler ManualTriggerEvent;

        public delegate void BlobDetectionHandler(BlobResult blobResult);
        public event BlobDetectionHandler BlobDetected;

        private Device getDeviceFromUserInput(ref DeviceManager devMgr)
        {
            uint num = devMgr.deviceCount();
            Device result;
            if (num == 0u)
            {
                Console.WriteLine("No MATRIX VISION device found!");
                result = null;
            }
            else
            {
                uint num2 = 0u;
                if (num2 >= num)
                {
                    Console.WriteLine("Invalid selection!");
                    result = null;
                }
                else
                {
                    Console.WriteLine("Using device number {0}", num2);
                    result = devMgr.getDevice(num2);
                }
            }
            return result;
        }

        public void StopCamera()
        {
            if (this.liveCamThread != null)
            {
                this.liveCamThread.StopCamera();
            }
        }

        public void InitializeCamera(IntPtr ptr)
        {
            try
            {
                this.devMgr = new DeviceManager();
                this.dev = this.getDeviceFromUserInput(ref this.devMgr);
                if (this.dev == null)
                {
                    this.ExceptionRaised("Device Pointer is NULL");
                }
                else
                {
                    this.liveCamThread = new VisionCameraThread(this.dev, this.win);
                    this.liveCamThread.NotifyException += new VisionCameraThread.CameraExceptionHandler(this.liveCamThread_NotifyException);
                    this.liveCamThread.ImageSavedNotification += new VisionCameraThread.ImageSavedNotificationHandler(this.liveCamThread_ImageSavedNotification);
                    this.liveCamThread.PatternDetected += new VisionCameraThread.PatternDetectionHandler(this.liveCamThread_PatternDetected);
                    this.liveCamThread.ResetReport += new VisionCameraThread.ResetReportHandler(this.liveCamThread_InitialiseReport);
                    this.liveCamThread.ShowReport += new VisionCameraThread.ShowReportHandler(this.liveCamThread_ShowReport);
                    this.liveCamThread.ModelReceivedNotification += new VisionCameraThread.ModelReceivedNotificationHandler(this.liveCamThread_ModelReceivedNotification);
                    this.liveCamThread.UpdateLiveImage += new VisionCameraThread.UpdateLiveImageHandler(this.liveCamThread_UpdateLiveImage);
                    this.liveCamThread.ManualTriggerEvent += liveCamThread_ManualTriggerEvent;
                    this.liveCamThread.BlobDetected += liveCamThread_BlobDetected;

                }
            }
            catch (ImpactException ex)
            {
                Console.WriteLine(ex.errorString);
            }
        }

        void liveCamThread_BlobDetected(BlobResult blobResult)
        {
            this.BlobDetected(blobResult);
        }

        private void liveCamThread_ShowReport(string modelValue, string plcValue)
        {
            this.ShowReport(modelValue, plcValue);
        }

        private void liveCamThread_InitialiseReport(string modelValue, string plcValue)
        {
            this.ResetReport(modelValue, plcValue);
        }



        public void liveCamThread_OutOfMemoryExceptionOccured(string sMessage, bool _isSoftwareTriggerStarted)
        {
            logger.Debug("**********************Stopping Application************************");
            this.liveCamThread.StopCamera();
            Thread.Sleep(3000);

            if (_isSoftwareTriggerStarted)
                liveCamThread_ManualTriggerEvent();

            logger.Debug("**********************Started Application Again************************");
            this.liveCamThread.StartCamera();
        }


        public void liveCamThread_ManualTriggerEvent()
        {
            this.liveCamThread.EnqnueLocalImagesForTesting();

            if (!GlobalSettings.IsSoftwareTriggerMonitorStarted)
            {
                this.threadControlSoftwareTrigger = new Thread(new ThreadStart(this.liveCamThread.StartSoftwareTriggerMonitor));
                this.threadControlSoftwareTrigger.Start();
                //Task.Factory.StartNew(this.liveCamThread.StartSoftwareTriggerMonitor);
                GlobalSettings.IsSoftwareTriggerMonitorStarted = true;
            }

        }

        private void liveCamThread_UpdateLiveImage(System.Drawing.Image img)
        {
            this.UpdateLiveImage(img);
        }

        private void liveCamThread_ModelReceivedNotification(List<Item> sRefImageFiles, string sModelName, string plcValue)
        {

            this.ModelReceivedNotification(sRefImageFiles, sModelName, plcValue);


        }

        private void liveCamThread_PatternDetected(List<PatternResult> lstResult)
        {

            this.PatternMatchDetected(lstResult);

        }

        private void liveCamThread_ImageSavedNotification(string sFilePath)
        {
            this.ImageSaved(sFilePath);
        }

        private void liveCamThread_NotifyException(string sMessage)
        {
            this.ExceptionRaised(sMessage);
        }

        internal void StartCamera(string sFldrPath)
        {

            try
            {
                this.liveCamThread.sFolderPath = sFldrPath;

                //this.threadControl = new Thread(new ThreadStart(this.liveCamThread.StartLiveAcuireCamera));
                //this.threadControl.Start();

                //this.threadControlDiMonitor = new Thread(new ThreadStart(this.liveCamThread.StartPortMonitorNew));
                //this.threadControlDiMonitor.Start();

                //this.threadControlPatternMonitor = new Thread(new ThreadStart(this.liveCamThread.StartPatternMatching));
                //this.threadControlPatternMonitor.Start();

                Task.Factory.StartNew(this.liveCamThread.StartLiveAcuireCamera);
                Task.Factory.StartNew(this.liveCamThread.StartPortMonitorNew);
                Task.Factory.StartNew(this.liveCamThread.StartPatternMatching);
                //while (!this.threadControl.Join(0))
                //{
                //    gBase.dispatchMessages();
                //}

                this.liveCamThread.StartCamera();
                GC.KeepAlive(this.dev);
                GC.KeepAlive(this.devMgr);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        internal void SetCameraConfigurations(List<Model> _lstModels)
        {
            this.liveCamThread.referenceImageFolderPath = DirectorFileHelper.GetReferenceImageFolderPath;
            this.liveCamThread.sourceImageFolderPath = DirectorFileHelper.GetCapturedImageFolderPath;
            this.liveCamThread.listOfModels = _lstModels;
        }

        internal void SetPort(CommunicationPorts port)
        {
            this.liveCamThread.SetPort(port);
        }

        internal void SetSoftwareTrigger(string sPlcInputValue)
        {
            this.liveCamThread.SetSoftwareTrigger(sPlcInputValue);
        }

        internal void StopSoftwareTrigger(string sPlcInputValue)
        {
            this.liveCamThread.StopSoftwareTrigger(sPlcInputValue);
        }

        public void Dispose()
        {
            if (this != null)
                this.Dispose();


        }

        public void AbortThreads()
        {

            if (this.threadControlSoftwareTrigger != null && this.threadControlSoftwareTrigger.IsAlive)
                this.threadControlSoftwareTrigger.Abort();


            if (this.threadControlPatternMonitor != null)
                this.threadControlPatternMonitor.Abort();

            if (this.threadControlDiMonitor != null)
                this.threadControlDiMonitor.Abort();

            if (this.threadControl != null)
                this.threadControl.Abort();

        }




        internal void StopSoftwareManualTriggerMonitor()
        {
            GlobalSettings.IsManualImagesAreLoaded = false;
            GlobalSettings.IsSoftwareTriggerMonitorStarted = false;

            if (this.threadControlSoftwareTrigger != null && this.threadControlSoftwareTrigger.IsAlive)
                this.threadControlSoftwareTrigger.Abort();
        }

        internal void StartCamera()
        {
            this.liveCamThread.StartCamera();
        }

        public void SetReadyToProcessNextModel()
        {
            this.liveCamThread.SetReadyToProcessNextModel();
        }
    }
}
