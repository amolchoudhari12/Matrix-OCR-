using log4net;
using mvIMPACT_NET;
using mvIMPACT_NET.acquire;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vilani.MatrixVision.Common;
using Expert.Common.Library;
using System.Linq;
using Expert.Common.Library.Models;
using mvIMPACT_NET.blob;
using System.Collections;
using mvIMPACT_NET.match;
using System.Configuration;

namespace Vilani.MatrixVision.Core
{
    internal class VisionCameraThread
    {
        public delegate void CameraExceptionHandler(string sMessage);
        public event CameraExceptionHandler NotifyException;

        public delegate void ModelReceivedNotificationHandler(List<Item> referenceImageFiles, string modelValue, string plcValue);
        public event ModelReceivedNotificationHandler ModelReceivedNotification;

        public delegate void ResetReportHandler(string modelValue, string plcValue);
        public event ResetReportHandler ResetReport;

        public delegate void ShowReportHandler(string modelValue, string plcValue);
        public event ShowReportHandler ShowReport;

        public delegate void ImageSavedNotificationHandler(string sFilePath);
        public event ImageSavedNotificationHandler ImageSavedNotification;

        public delegate void PatternDetectionHandler(List<PatternResult> lstResult);
        public event PatternDetectionHandler PatternDetected;

        public delegate void UpdateLiveImageHandler(System.Drawing.Image img);
        public event UpdateLiveImageHandler UpdateLiveImage;

        public delegate void ManualTriggerHandler();
        public event ManualTriggerHandler ManualTriggerEvent;

        public delegate void BlobDetectionHandler(BlobResult blobResult);
        public event BlobDetectionHandler BlobDetected;

        private PtternMatchingResult _inspectionCounters = PtternMatchingResult.GetInstace();

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool isToStopThread = false;

        private Device devicePointer;

        private Window windowPointer;

        private Request ReqPtr;

        public string sFolderPath = "";

        public int iDiNumberTrigSrc = 0;

        public List<Model> listOfModels = null;

        public string referenceImageFolderPath = "";

        public string sourceImageFolderPath = "";

        public int modelScore = 0;

        private string modelName = "";

        private int modelID = 0;

        private int iNumOfValidConditions = 0;

        private List<Item> _ReferenceImageFileColl = new List<Item>();

        private string sourceImageFileName = "";

        //  private SerialPort sp = null;

        private CommunicationPorts _communicationPort = null;

        private Queue<string> qFilesForSimulation = new Queue<string>();

        private string fileForSim = "";

        private volatile bool isToStoreImage = false;

        private volatile string triggerModelValue = "";

        private string receivedModel = "";

        private volatile bool _isReadyToProcessNext = true;

        private volatile bool isSoftwareTriggered = false;

        private string softwareTriggerPlcInputValue = "";

        private string aoiFileName = "";

        private PatternMatching patMatch = new PatternMatching();

        private List<PatternDetails> listFileDetails = new List<PatternDetails>();




        public VisionCameraThread(Device devObj, Window winObj)
        {
            this.NotifyException += new CameraExceptionHandler(this.CameraThread_NotifyException);
            this.devicePointer = devObj;
            this.windowPointer = winObj;
            try
            {
                this.devicePointer.open();


            }
            catch (ImpactAcquireException ex)
            {
                this.NotifyException(ex.Message);
            }
        }

        private void CameraThread_NotifyException(string sMessage)
        {
        }

        public void StopCamera()
        {
            this.isToStopThread = true;
            this._isReadyToProcessNext = false;

        }

        public void StartCamera(Device devObj, Window winObj)
        {
            this.isToStopThread = false;
            this._isReadyToProcessNext = true;
            this.devicePointer = devObj;
            this.windowPointer = winObj;
            try
            {
                this.devicePointer.open();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

        }

        public void StartCamera()
        {
            this.isToStopThread = false;
            this._isReadyToProcessNext = true;

        }



        public void StartLiveAcuireCamera()
        {

            Statistics statistics = new Statistics(ref this.devicePointer);
            FunctionInterface functionInterface = new FunctionInterface(ref this.devicePointer);
            SystemSettings systemSettings = new SystemSettings(ref this.devicePointer);


            int num = systemSettings.requestCount.read();
            for (int i = 0; i < num; i++)
            {
                functionInterface.imageRequestSingle();
            }
            int num2 = -1;
            int nr = -1;
            DateTime now = DateTime.Now;

            Bitmap img = null;
            Bitmap bitmap = null;
            while (!this.isToStopThread)
            {
                num2 = functionInterface.imageRequestWaitFor(5000);
                if (functionInterface.isRequestNrValid(num2))
                {
                    this.ReqPtr = functionInterface.getRequest(num2);
                    if (functionInterface.isRequestOK(ref this.ReqPtr))
                    {
                        try
                        {
                            var tempImage = this.ReqPtr.getIMPACTImage();
                            img = tempImage.convertToBitmap();

                            if (this.isToStoreImage && this.sourceImageFolderPath.Length > 0)
                            {


                                VisionCameraThread.logger.Debug("Trigger Received & State of Ready NEXT is " + this._isReadyToProcessNext);
                                now = DateTime.Now;
                                this.sourceImageFileName = DirectorFileHelper.GetCapturedImageFilePath(string.Format("Captured_MODEL_{0}_{1}.jpg", this.triggerModelValue, now.ToString("dd_MMM_yyyy_HH_mm_ss_fff")));
                                if (GlobalSettings.IsManualImagesAreLoaded)
                                {
                                    if (this.qFilesForSimulation.Count > 0)
                                    {
                                        this.fileForSim = this.qFilesForSimulation.Dequeue();
                                    }
                                    bitmap = (Bitmap)System.Drawing.Image.FromFile(this.fileForSim);
                                    File.Copy(this.fileForSim, this.sourceImageFileName);
                                }
                                else
                                {
                                    tempImage.convertToBitmap().Save(this.sourceImageFileName, ImageFormat.Jpeg);
                                }
                                if (this._ReferenceImageFileColl != null)
                                {
                                    VisionCameraThread.logger.Debug("Reference File Collection present");
                                    this.listFileDetails.Add(new PatternDetails
                                    {
                                        RefImageFiles = this._ReferenceImageFileColl,
                                        SrcImageFile = this.sourceImageFileName,
                                        ModelName = this.modelName,
                                        ModelID = this.modelID,
                                        Score = this.modelScore
                                    });
                                    this.triggerModelValue = "";
                                    this.isToStoreImage = false;
                                    this.ImageSavedNotification(this.sourceImageFileName);
                                }
                                VisionCameraThread.logger.Debug("Reference File Collection NOT present");
                            }
                        }
                        catch (OutOfMemoryException ex)
                        {
                            logger.Fatal("OUT OF MEMORY BY VILANI AMOL 1: " + ex.Message);
                            //  OutOfMemoryExceptionOccured(ex.Message, "serial");
                        }
                        catch (Exception ex)
                        {
                            this.NotifyException("Error while saving the image to the selected location. " + ex.Message);
                            VisionCameraThread.logger.Error("Trigger Exception Catch.", ex);
                        }
                        if (GlobalSettings.IsManualImagesAreLoaded && bitmap != null)
                        {
                            img = bitmap;
                        }
                        this.UpdateLiveImage(img);


                    }
                    if (functionInterface.isRequestNrValid(nr))
                    {
                        functionInterface.imageRequestUnlock(nr);
                    }
                    nr = num2;
                    functionInterface.imageRequestSingle();
                }
                else
                {
                    this.NotifyException("Error while checking isRequestNrValid...");
                }
                GC.KeepAlive(this.ReqPtr);
                //  GC.KeepAlive(this.tempImage);
                Thread.Sleep(50);
            }
            if (functionInterface.isRequestNrValid(num2))
            {
                functionInterface.imageRequestUnlock(num2);
            }
            functionInterface.imageRequestReset(0, 0);
            this.devicePointer.close();
        }
        Random num = new Random();
        private Object thisLock = new Object();


        public void StartPortMonitorNew()
        {
            try
            {
                while (!this.isToStopThread)
                {
                    Thread.Sleep(50);
                    if (this._communicationPort != null)
                    {
                        if (this._communicationPort.IsOpen())
                        {
                            try
                            {


                                logger.Debug("Port Data Bytes Available: Reading in progress ");
                                this.receivedModel = this._communicationPort.ReadNext();

                                if (this._isReadyToProcessNext)
                                {

                                    logger.Debug("-----Ready to process the image.For-------" + this.receivedModel);
                                    logger.Debug("-----Flag of _isReadyToProcessNext : " + _isReadyToProcessNext.ToString());

                                    this.receivedModel = this.receivedModel.ToUpper().Trim();
                                    logger.Debug("ReceivedModel is " + this.receivedModel);
                                    _inspectionCounters.AddTriggerCount();


                                    if (!string.IsNullOrEmpty(this.receivedModel) && this.receivedModel.Length > 2)
                                    {
                                        this.receivedModel = this.receivedModel.Substring(0, 2);
                                        logger.Debug("Received Model's valid 2 characters: *" + this.receivedModel + "*");
                                    }

                                    //pooja code
                                    if (this.receivedModel == "00")
                                    {
                                        this.ResetReport(this.modelName, this.PLCValue);

                                    }
                                    if (this.receivedModel == "99")
                                    {
                                        this.ShowReport(this.modelName, this.PLCValue);

                                    }

                                    var currentModel = this.listOfModels.Where(x => x.PLCInputValue.ToUpper().Trim() == this.receivedModel.ToUpper().Trim()).FirstOrDefault();
                                    if (currentModel != null)
                                    {
                                        this._ReferenceImageFileColl = currentModel.ReferenceImageOccurancePairList;
                                        this.modelName = currentModel.ModelName + " - " + currentModel.PLCInputValue;
                                        this.PLCValue = this.receivedModel.ToUpper().Trim();
                                        this.modelID = currentModel.ModelID;

                                        if (this._ReferenceImageFileColl.Count > 0)
                                        {
                                            this.triggerModelValue = this.receivedModel;
                                            this.isToStoreImage = true;
                                            this.ModelReceivedNotification(this._ReferenceImageFileColl, this.modelName, this.PLCValue);

                                        }
                                    }

                                }
                            }
                            catch (OutOfMemoryException ex)
                            {
                                logger.Fatal("OUT OF MEMORY BY VILANI StartPortMonitorNew1 : " + ex.Message);
                                // OutOfMemoryExceptionOccured(ex.Message, "serial");
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Serial Read", ex);
                            }
                        }
                    }
                }
            }
            catch (OutOfMemoryException ex)
            {
                logger.Fatal("OUT OF MEMORY BY VILANI StartPortMonitorNew2 : " + ex.Message);
                // OutOfMemoryExceptionOccured(ex.Message, "serial");
            }
            catch (Exception ex)
            {
                logger.Error("Overall Error: ", ex);
            }
        }

        public void StartSoftwareTriggerMonitor()
        {
            while (!this.isToStopThread)
            {
                Thread.Sleep(50);
                if (this.isSoftwareTriggered)
                {
                    try
                    {
                        this.receivedModel = "";
                        this._ReferenceImageFileColl = null;
                        this.iNumOfValidConditions = 0;
                        this.modelName = "";
                        this.receivedModel = this.softwareTriggerPlcInputValue;
                        if (this._isReadyToProcessNext)
                        {
                            var currentModel = this.listOfModels.Where(x => x.PLCInputValue.ToUpper().Trim() == this.receivedModel.ToUpper().Trim()).FirstOrDefault();
                            if (currentModel != null)
                            {
                                this._ReferenceImageFileColl = currentModel.ReferenceImageOccurancePairList;
                                this.modelName = currentModel.ModelName + " - " + currentModel.PLCInputValue;
                                this.PLCValue = this.receivedModel.ToUpper().Trim();
                                this.modelID = currentModel.ModelID;

                                if (this._ReferenceImageFileColl.Count > 0)
                                {
                                    this.triggerModelValue = this.receivedModel;
                                    this.isToStoreImage = true;
                                    this.ModelReceivedNotification(this._ReferenceImageFileColl, this.modelName, this.PLCValue);

                                    //this is for test

                                }
                            }
                            this.isSoftwareTriggered = false;
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        logger.Fatal("OUT OF MEMORY BY VILANI AMOL5 : " + ex.Message);
                        //OutOfMemoryExceptionOccured(ex.Message, "serial");
                    }
                    catch (Exception exception)
                    {
                        logger.Error("Software trigger Monitor.", exception);
                    }
                }
            }
        }



        private mvIMPACT_NET.Image ExtractImageNEW(string sSrcImgPath, string sRefImgPath)
        {
            Bitmap bitmap = new Bitmap(sSrcImgPath);
            mvIMPACT_NET.Image extractedImage = null;
            System.Drawing.Rectangle rect = default(System.Drawing.Rectangle);

            Bitmap bitmap2 = null;
            try
            {

                string text = sRefImgPath.Substring(sRefImgPath.LastIndexOf('_') + 1).Replace(".jpg", "");
                string[] array = text.Split(new char[]
				{
					'.'
				});
                rect = new System.Drawing.Rectangle
                {
                    X = Convert.ToInt32(array[0]),
                    Y = Convert.ToInt32(array[1]),
                    Width = Convert.ToInt32(array[2]),
                    Height = Convert.ToInt32(array[3])
                };


                bitmap2 = bitmap.Clone(rect, bitmap.PixelFormat);

                // if (!string.IsNullOrEmpty(GlobalSettings.ManulTriggerSourceImagesLocation))
                {
                    var fileName = Path.GetFileName(sSrcImgPath);
                    this.aoiFileName = string.Format("Processed_{0}.jpg", fileName);
                    bitmap2.Save(DirectorFileHelper.GetPorcessedImageFilePath(this.aoiFileName), ImageFormat.Jpeg);
                }

                if (!string.IsNullOrEmpty(this.aoiFileName))
                    extractedImage = gBase.loadImage(DirectorFileHelper.GetPorcessedImageFilePath(this.aoiFileName));


            }
            catch (Exception exception)
            {
                logger.Error(exception);
            }
            finally
            {
                if (bitmap2 != null)
                    bitmap2.Dispose();
                // if (bitmap != null)
                //  bitmap.Dispose();
            }
            return extractedImage;
        }
        BlobResult blobResult = null;
        int _currentPatternID = 0;
        public void StartPatternMatching()
        {
            mvIMPACT_NET.Image imgReference = null;
            mvIMPACT_NET.Image imgMainImage = null;
            List<Item> list = new List<Item>();
            string text = "";
            PatternDetails patternDetails = null;
            List<PatternResult> list2 = new List<PatternResult>();

            try
            {
                logger.Debug("Starting PatternMatch");
                while (!this.isToStopThread)
                {
                    if (!this.isToStoreImage)
                    {
                        if (this.listFileDetails.Count > 0)
                        {
                            this._isReadyToProcessNext = false;
                            try
                            {
                                patternDetails = this.listFileDetails[0];
                                list = patternDetails.RefImageFiles;
                                text = patternDetails.SrcImageFile;
                                int score = patternDetails.Score;

                                if (list2 != null)
                                {
                                    list2.Clear();
                                }
                            }
                            catch (NullReferenceException ex)
                            {
                                logger.Debug("NULL Expectation");
                                logger.Error(ex);
                            }
                            catch (Exception exception)
                            {
                                logger.Debug("OTHER Expectation");
                                logger.Error(exception);
                            }


                            try
                            {
                                blobResult = new BlobResult();
                                blobResult.ListOfValidBlobs = new List<MyAreaOfInterest>();
                                blobResult.ListOfInvalidBlobs = new List<MyAreaOfInterest>();
                                string SourceImagePath = text;
                                _currentPatternID = 0;

                                foreach (Item current in list)
                                {
                                    try
                                    {
                                        if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.BlobAnalysis)
                                        {
                                            imgMainImage = gBase.loadImage(text);
                                        }
                                        else if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.SinglePatternMatching)
                                        {
                                            imgMainImage = this.ExtractImageNEW(text, current.Name);
                                        }
                                        else if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.BlobAndSerialPatternMatching)
                                        {
                                            imgMainImage = this.ExtractImageNEW(text, current.Name);
                                        }
                                        else if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.SerialPatternMatching)
                                        {
                                            imgMainImage = this.ExtractImageNEW(text, current.Name);
                                        }

                                        imgReference = gBase.loadImage(DirectorFileHelper.GetReferenceImageFilePath(current.Name));

                                    }
                                    catch (OutOfMemoryException ex)
                                    {
                                        logger.Fatal("OUT OF MEMORY BY VILANI AMOL13 : " + ex.Message);
                                        //OutOfMemoryExceptionOccured(ex.Message, "serial");
                                    }
                                    catch (Exception exception)
                                    {
                                        imgMainImage = gBase.loadImage(text);
                                        logger.Error("PTMATCH. loading image:" + text, exception);
                                        logger.Error("excp.loading ref.image:" + DirectorFileHelper.GetReferenceImageFilePath(current.Name), exception);
                                    }

                                    DateTime now = DateTime.Now;
                                    double _outputScore = 0;
                                    _currentPatternID++;
                                    if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.BlobAnalysis)
                                    {
                                        BlobResult blobResult1 = ApplyBlobAnalysis(imgMainImage);
                                        this.BlobDetected(blobResult1);
                                        break;
                                    }
                                    else if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.BlobAndSerialPatternMatching)
                                    {
                                        ApplyPatternMatchingAnalysis(imgMainImage, imgReference, current.Name);
                                    }
                                    else if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.SerialPatternMatching)
                                    {
                                        ApplyPatternMatchingAnalysis(imgMainImage, imgReference, SourceImagePath);

                                    }
                                    else if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.SinglePatternMatching)
                                    {

                                        int num = this.patMatch.CheckImageOccuranceViaMatrix(imgReference, imgMainImage, 60, current.Score, out _outputScore);
                                        DateTime now2 = DateTime.Now;
                                        bool flag = _outputScore > current.Score ? true : false;            //OLD way of comparing the result :  num == current.Value;
                                        list2.Add(new PatternResult
                                        {
                                            ModelID = patternDetails.ModelID,
                                            ReferenceImageID = current.ReferenceImageID,
                                            CapImageName = text,
                                            RefImageName = current.Name,
                                            IsValid = flag,
                                            ActualOccurances = num,
                                            SetScore = current.Score,
                                            ActualScore = _outputScore,
                                            ModelName = patternDetails.ModelName,
                                            AOIRectangle = GetRectangle(current.Name)
                                        });
                                        if (flag)
                                        {
                                            break;
                                        }


                                    }

                                }
                                if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.BlobAndSerialPatternMatching)
                                {
                                    blobResult.TotalCounts = 18;// GetNumberOfBlobs(imgMainImage);
                                    blobResult.ListOfValidBlobs = SortPoints(blobResult.ListOfValidBlobs);

                                    FindInvalidBlobs(blobResult);


                                    this.BlobDetected(blobResult);
                                }
                                else if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.SinglePatternMatching)
                                {
                                    this.PatternDetected(list2);
                                }
                                else if (FeatureSettings.VisionInspectionType == Expert.Common.Library.Enumrations.VisionInspectionType.SerialPatternMatching)
                                {
                                    this.BlobDetected(blobResult);
                                }


                                this._isReadyToProcessNext = true;

                                //    patternDetails.RefImageFiles.Clear();
                                this.listFileDetails.Remove(patternDetails);
                                // GC.KeepAlive(list);                               


                            }

                            catch (OutOfMemoryException ex)
                            {
                                logger.Fatal("OUT OF MEMORY BY In Pattern Matching : " + ex.Message);
                                logger.Fatal(string.Format("Skipping model {0} and starting new Model Processing", this.receivedModel));
                                this._isReadyToProcessNext = true;
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Pattern Matching Exception 2 :" + ex);
                                logger.Fatal(string.Format("Skipping model {0} and starting new Model Processing", this.receivedModel));
                                this._isReadyToProcessNext = true;
                            }


                        }
                    }
                    Thread.Sleep(50);
                }

            }
            catch (OutOfMemoryException ex)
            {
                logger.Fatal("OUT OF MEMORY BY VILANI AMOL : " + ex.Message);
                logger.Fatal(string.Format("Skipping model {0} and starting new Model Processing", this.receivedModel));
                this._isReadyToProcessNext = true;
            }
            catch (Exception ex)
            {
                logger.Debug("Starting PatternMatch Overall Error : " + ex.Message);
                logger.Fatal(string.Format("Skipping model {0} and starting new Model Processing", this.receivedModel));
                this._isReadyToProcessNext = true;
            }
            finally
            {
                if (imgMainImage != null)
                    imgMainImage.Dispose();
            }

        }

        private void FindInvalidBlobs(BlobResult blobResult)
        {
            var maxRange = Convert.ToInt32(ConfigurationManager.AppSettings["MaxValidRange"]);

            for (int i = 0; i < blobResult.ListOfValidBlobs.Count - 1; i++)
            {
                int diff = 0;
                if (i == 0)
                {
                    if (blobResult.ListOfValidBlobs[i].X > maxRange + blobResult.AOIXPosition)
                    {
                        var result = new MyAreaOfInterest();
                        result.X = blobResult.ListOfValidBlobs[i].X + blobResult.ListOfValidBlobs[i].Width;
                        result.Y = blobResult.ListOfValidBlobs[i].Y + blobResult.ListOfValidBlobs[i].Y / 2;
                        result.Width = maxRange - blobResult.ListOfValidBlobs[i].Width;
                        result.Height = 10;
                        blobResult.ListOfInvalidBlobs.Add(result);
                    }
                }

                diff = blobResult.ListOfValidBlobs[i + 1].X - blobResult.ListOfValidBlobs[i].X;

                if (diff > maxRange)
                {
                    var result = new MyAreaOfInterest();
                    result.X = blobResult.ListOfValidBlobs[i].X + blobResult.ListOfValidBlobs[i].Width;
                    result.Y = blobResult.ListOfValidBlobs[i].Y + blobResult.ListOfValidBlobs[i].Y / 2;
                    result.Width = diff - blobResult.ListOfValidBlobs[i + 1].Width;
                    result.Height = 10;
                    blobResult.ListOfInvalidBlobs.Add(result);

                }


            }
        }

        public void SetReadyToProcessNext(bool flag)
        {
            this._isReadyToProcessNext = flag;
        }

        private int GetNumberOfBlobs(mvIMPACT_NET.Image imgMainImage)
        {
            if (GlobalSettings.BlobConfigSettings == null)
                return -1;

            int interestedAreasCount = 0;
            var settings = GlobalSettings.BlobConfigSettings[GlobalSettings.BlobConfigSettings.Count - 1];

            var factor1 = settings.ImageFactor;
            decimal cropFactor = settings.CropAreaPercent == 0 ? 1 : Convert.ToDecimal(settings.CropAreaPercent / 100);

            var y = imgMainImage.height - imgMainImage.height * cropFactor;
            var height = imgMainImage.height - y;
            mvIMPACT_NET.Image imgSourceCrop = new mvIMPACT_NET.Image(imgMainImage, 0, (int)y, imgMainImage.width, (int)height);
            var areaToCompare = Convert.ToInt32(settings.AreaGreaterThan);


            imgSourceCrop = gBase.compareGreater(imgSourceCrop, factor1);

            if (settings.FillHoles)
                imgSourceCrop = gBase.fillHoles(imgSourceCrop);

            string pathToSave = Path.Combine(DirectorFileHelper.GetProcessedImageFolderPath, string.Format("{0}.jpg", System.DateTime.Now.Millisecond.ToString()));
            gBase.saveImage(imgSourceCrop, pathToSave);



            BlobFeatureList features = new BlobFeatureList();
            int areaID = features.addArea();
            int boxID = features.addExtremeBox();
            int holes = features.addNumberHoles();

            var savedImage = System.Drawing.Image.FromFile(pathToSave);

            BlobResultList result = gBlob.analyze(imgSourceCrop, features);

            int numberOfBlobs = result.getNumberOfBlobs();
            ArrayList aoi1 = new ArrayList();

            List<MyAreaOfInterest> pointsOfInetrest = new List<MyAreaOfInterest>();

            for (int i = 0; i < numberOfBlobs; i++)
            {
                Array area = result.getSingleResult(areaID, i, 0, 0, 0);
                Array box = result.getSingleResult(boxID, i, 0, 0, 0);

                var width = Convert.ToInt32(box.GetValue(2)) - Convert.ToInt32(box.GetValue(0));
                var modelhieght = Convert.ToInt32(box.GetValue(3)) - Convert.ToInt32(box.GetValue(1));



                if (Convert.ToInt32(area.GetValue(0)) > areaToCompare)
                {
                    interestedAreasCount++;

                    pointsOfInetrest.Add(new MyAreaOfInterest()
                    {
                        ID = i,
                        X = Convert.ToInt32(box.GetValue(0)),
                        Y = Convert.ToInt32(box.GetValue(1)),
                        Width = Convert.ToInt32(box.GetValue(2)) - Convert.ToInt32(box.GetValue(0)),
                        Height = Convert.ToInt32(box.GetValue(3)) - Convert.ToInt32(box.GetValue(1))
                    });
                }

            }
            pointsOfInetrest = SortPoints(pointsOfInetrest);



            for (int i = 0; i < pointsOfInetrest.Count - 1; i++)
            {

                var diff = pointsOfInetrest[i + 1].X - pointsOfInetrest[i].X;

                if (diff > settings.MaxRange)
                {
                    var x = pointsOfInetrest[i].X;
                    var width = pointsOfInetrest[i + 1].Width - pointsOfInetrest[i].X;
                    // create ellipse of interest and set some properties
                    var yH = blobResult.ListOfValidBlobs[0].Y;

                    blobResult.ListOfInvalidBlobs.Add(new MyAreaOfInterest()
                    {
                        ID = i,
                        X = x,
                        Y = yH,
                        Width = pointsOfInetrest[i].Width + diff - 10,
                        Height = pointsOfInetrest[i].Height + 25
                    });


                }
            }


            return interestedAreasCount;


        }




        private BlobResult ApplyPatternMatchingAnalysis(mvIMPACT_NET.Image imgMainImage, mvIMPACT_NET.Image imgReference, string sRefImgPath)
        {

            string text = sRefImgPath.Substring(sRefImgPath.LastIndexOf('_') + 1).Replace(".jpg", "");
            string[] array = text.Split(new char[]
				{
					'.'
				});

            var X = Convert.ToInt32(array[0]);
            var Y = Convert.ToInt32(array[1]);


            //setup first model and display it in the image
            PatModel model1 = new PatModel(imgReference, 0, 0, imgReference.width, imgReference.height);
            model1.initialAcceptanceScore = Convert.ToInt32(ConfigurationManager.AppSettings["Score"]);
            model1.finalAcceptanceScore = Convert.ToInt32(ConfigurationManager.AppSettings["Score"]);
            model1.numberOfMatches = (int)PatMatchMode.pmmFindAll;

            //search for model
            PatMatchResult matchResult = gMatch.findPatModel(model1, imgMainImage);
            Array matches1 = matchResult.getMatches(0, 0, 0);


            //mark all occurences of model1 
            ArrayList aoi1 = new ArrayList();
            List<MyAreaOfInterest> pointsOfInetrest = new List<MyAreaOfInterest>();
            int counter;
            for (counter = 0; counter < matchResult.getNumberOfMatches(0, 0, 0); counter++)
            {
                MatchData aMatch = (MatchData)matches1.GetValue(counter);

                pointsOfInetrest.Add(new MyAreaOfInterest()
                {
                    PatternID = _currentPatternID,
                    ID = counter + 1,
                    X = X + (int)aMatch.x - imgReference.width / 2,
                    Y = Y + (int)aMatch.y - imgReference.height / 2,
                    Width = (int)imgReference.width,
                    Height = (int)imgReference.height,

                });
            }

            blobResult.AOIXPosition = X;
            blobResult.AOIYPosition = Y;
            blobResult.ListOfValidBlobs.AddRange(pointsOfInetrest);
            return blobResult;
        }


        private BlobResult ApplyBlobAnalysis(mvIMPACT_NET.Image imgMainImage)
        {
            BlobResult resultOfBlobs = new BlobResult();
            try
            {
                if (GlobalSettings.BlobConfigSettings == null)
                    return null;

                int interestedAreasCount = 0;
                var settings = GlobalSettings.BlobConfigSettings[GlobalSettings.BlobConfigSettings.Count - 1];


                mvIMPACT_NET.Image image = imgMainImage;


                mvIMPACT_NET.Image image2 = gBase.compareGreater(image, settings.ImageFactor);


                image2 = gBase.extractHoles(image2);

                if (settings.FillHoles)
                    image2 = gBase.fillHoles(image2);



                BlobFeatureList features = new BlobFeatureList();
                int areaID = features.addArea();
                int boxID = features.addExtremeBox();
                int holes = features.addNumberHoles();

                BlobResultList result = gBlob.analyze(image2, features);

                int numberOfBlobs = result.getNumberOfBlobs();
                ArrayList aoi1 = new ArrayList();
                List<MyAreaOfInterest> pointsOfInetrest = new List<MyAreaOfInterest>();
                for (int i = 0; i < numberOfBlobs; i++)
                {
                    Array area = result.getSingleResult(areaID, i, 0, 0, 0);
                    Array box = result.getSingleResult(boxID, i, 0, 0, 0);

                    var width = Convert.ToInt32(box.GetValue(2)) - Convert.ToInt32(box.GetValue(0));
                    var modelhieght = Convert.ToInt32(box.GetValue(3)) - Convert.ToInt32(box.GetValue(1));

                    if (Convert.ToInt32(area.GetValue(0)) > settings.AreaGreaterThan)
                    {
                        pointsOfInetrest.Add(new MyAreaOfInterest()
                        {
                            ID = i,
                            X = Convert.ToInt32(box.GetValue(0)),
                            Y = Convert.ToInt32(box.GetValue(1)),
                            Width = Convert.ToInt32(box.GetValue(2)) - Convert.ToInt32(box.GetValue(0)),
                            Height = Convert.ToInt32(box.GetValue(3)) - Convert.ToInt32(box.GetValue(1))
                        });
                    }



                }

                resultOfBlobs.ListOfValidBlobs = SortPoints(pointsOfInetrest);
                resultOfBlobs.ListOfInvalidBlobs = new List<MyAreaOfInterest>();



                for (int i = 0; i < pointsOfInetrest.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        if (pointsOfInetrest[i].X > settings.MaxRange)
                        {
                            var x = pointsOfInetrest[i].X + 20;
                            var width = pointsOfInetrest[i + 1].Width - pointsOfInetrest[i].X;
                            // create ellipse of interest and set some properties

                            resultOfBlobs.ListOfInvalidBlobs.Add(new MyAreaOfInterest() { X = x, Y = pointsOfInetrest[i].Y - 10, Width = pointsOfInetrest[i].Width - 35, Height = pointsOfInetrest[i].Height + 25 });

                        }
                    }
                    var diff = pointsOfInetrest[i + 1].X - pointsOfInetrest[i].X;

                    if (diff > settings.MaxRange)
                    {
                        var x = pointsOfInetrest[i].X + 20;
                        var width = pointsOfInetrest[i + 1].Width - pointsOfInetrest[i].X;
                        // create ellipse of interest and set some properties

                        resultOfBlobs.ListOfInvalidBlobs.Add(new MyAreaOfInterest() { X = x, Y = pointsOfInetrest[i].Y - 10, Width = pointsOfInetrest[i].Width + diff - 35, Height = pointsOfInetrest[i].Height + 25 });

                    }
                }




            }
            catch (ImpactException e)
            {
                Console.WriteLine(e.errorString);
            }

            return resultOfBlobs;
        }


        private static List<MyAreaOfInterest> SortPoints(List<MyAreaOfInterest> pointsOfInetrest)
        {
            int tempX = 0;
            int tempY = 0;
            int tempWidth = 0;
            int tempHeight = 0;

            for (int i = 0; i < pointsOfInetrest.Count; i++)
            {
                for (int j = 0; j < pointsOfInetrest.Count - 1; j++)
                {
                    if (pointsOfInetrest[i].X < pointsOfInetrest[j].X)
                    {

                        tempX = pointsOfInetrest[j].X;
                        tempY = pointsOfInetrest[j].Y;
                        tempWidth = pointsOfInetrest[j].Width;
                        tempHeight = pointsOfInetrest[j].Height;

                        pointsOfInetrest[j].X = pointsOfInetrest[i].X;
                        pointsOfInetrest[j].Y = pointsOfInetrest[i].Y;
                        pointsOfInetrest[j].Width = pointsOfInetrest[i].Width;
                        pointsOfInetrest[j].Height = pointsOfInetrest[i].Height;

                        pointsOfInetrest[i].X = tempX;
                        pointsOfInetrest[i].Y = tempY;
                        pointsOfInetrest[i].Width = tempWidth;
                        pointsOfInetrest[i].Height = tempHeight;
                    }

                }
            }
            return pointsOfInetrest;
        }
        private System.Drawing.Rectangle GetRectangle(string sRefImgPath)
        {
            string text = sRefImgPath.Substring(sRefImgPath.LastIndexOf('_') + 1).Replace(".jpg", "");
            string[] array = text.Split(new char[]
				{
					'.'
				});

            var rect = new System.Drawing.Rectangle
            {
                X = Convert.ToInt32(array[0]),
                Y = Convert.ToInt32(array[1]),
                Width = Convert.ToInt32(array[2]),
                Height = Convert.ToInt32(array[3])
            };

            return rect;
        }


        internal void SetPort(CommunicationPorts port)
        {
            this._communicationPort = port;
        }

        internal void SetSoftwareTrigger(string sPlcInputValue)
        {
            this.softwareTriggerPlcInputValue = sPlcInputValue;
            this.isSoftwareTriggered = true;
            //  this.bIsToStopThread = false;
        }

        internal void StopSoftwareTrigger(string sPlcInputValue)
        {
            this.isSoftwareTriggered = false;
            // this.bIsToStopThread = true;
        }

        public void EnqnueLocalImagesForTesting()
        {
            if (!GlobalSettings.IsManualImagesAreLoaded)
            {
                if (!string.IsNullOrEmpty(GlobalSettings.ManulTriggerSourceImagesLocation) && Directory.Exists(GlobalSettings.ManulTriggerSourceImagesLocation))
                {
                    this.qFilesForSimulation.Clear();
                    String searchFolder = @GlobalSettings.ManulTriggerSourceImagesLocation;
                    var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
                    var files = GetFilesFrom(searchFolder, filters, false);
                    string[] array = files;
                    for (int j = 0; j < array.Length; j++)
                    {
                        string item = array[j];
                        this.qFilesForSimulation.Enqueue(item);
                    }
                    GlobalSettings.IsManualImagesAreLoaded = true;
                }


            }
        }



        public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        public string PLCValue { get; set; }

        internal void SetReadyToProcessNextModel()
        {
            this._isReadyToProcessNext = true;
        }
    }

    public class MyAreaOfInterest
    {
        public int PatternID { get; set; }
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsProcessed { get; set; }
        public string SourceImagePath { get; set; }
    }

    public class BlobResult
    {
        public int TotalCounts { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
        public int AOIXPosition { get; set; }
        public int AOIYPosition { get; set; }
        public List<MyAreaOfInterest> ListOfValidBlobs { get; set; }
        public List<MyAreaOfInterest> ListOfInvalidBlobs { get; set; }


        public bool IsValid()
        {
            if (ListOfValidBlobs == null || ListOfValidBlobs.Count == 0)
                return false;

            var quality = Convert.ToInt32(ConfigurationManager.AppSettings["Score"]);
            var diff = Convert.ToInt32(ConfigurationManager.AppSettings["MaxGapDifference"]);

            if (quality > 50 && quality < 63)
            {
                var result = ListOfInvalidBlobs.Where(x => x.Width > diff).FirstOrDefault();
                if (result != null)
                    return false;
                else
                    return true;
            }

            if (quality > 63 && quality < 68)
            {

                var mxWidth = ListOfValidBlobs.Max(x => x.Width);
                var result = ListOfInvalidBlobs.Where(x => x.Width > mxWidth).FirstOrDefault();
                if (result != null)
                    return false;
                else
                    return true;
            }


            if (ListOfInvalidBlobs.Count > 1)
                return false;
            if (ListOfValidBlobs.Count == 0)
                return false;


            return true;
        }

        public List<MyCenter> GetAllValidCenters()
        {
            List<MyCenter> centers = new List<MyCenter>();

            foreach (var item in ListOfValidBlobs)
            {
                int x = item.X + item.Width / 2;
                int y = item.Y + item.Height / 2;

                centers.Add(new MyCenter(x, y));
            }

            return centers;
        }
    }


    public class MyCenter
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MyCenter(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
