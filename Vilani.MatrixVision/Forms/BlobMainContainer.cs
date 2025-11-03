using log4net;
using mvIMPACT_NET;
using mvIMPACT_NET.acquire;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vilani.MatrixVision.Common;
using Vilani.MatrixVision.Core;
using Vilani.MatrixVision.DataBase;
using Expert.Common.Library;
using Vilani.MatrixVision.UserControls;
using System.Linq;
using Expert.Common.Library.Models;
using Expert.Common.Library.Enumrations;
using Expert.Common.Library.Common;
using Vilani.MatrixVision.Collections;
using System.Diagnostics;
using System.Configuration;
using Expert.Common.Library.DTOs;
using Vilani.MatrixVision.Forms;


namespace Vilani.MatrixVision
{
    public partial class BlobMainContainer : Form
    {
        //private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        ILog logger = LogManager.GetLogger("RollingFile");
        ILog applicationLogger = LogManager.GetLogger("InformationLogs");
        private MatrixBlueFoxCamera camera = new MatrixBlueFoxCamera();
        private DataBaseAccess _dbAccess = new DataBaseAccess();
        private PortGenerator _portGenerator = new PortGenerator();
        Reports reportForm = new Reports();
        List<ReportDTO> reportsData = new List<ReportDTO>();
        List<PatternResult> matchingResult = new List<PatternResult>();

        List<SourceImageReactangle> _caputedRectangles = null;
        List<ReferenceImage> _allReferenceImagesInjecteds = new List<ReferenceImage>();

        ResultDisplayModelContainer _resultDisplayModelContainer = new ResultDisplayModelContainer();
        ReactanlgeDrawnContainer _reactanlgeDrawnContainer = new ReactanlgeDrawnContainer();

        CommunicationPorts _communicationPort = null;

        System.Diagnostics.Stopwatch _currentStopWatcher = new System.Diagnostics.Stopwatch();

        System.Drawing.Image _currentImage = null;
        public string _previousModelName { get; set; }
        public string _currentModelName { get; set; }
        private string _sPreviousResult = "";
        public string _CurrentCaputuredImagePath { get; set; }
        public string _PreviousCapturedImagePath { get; set; }

        private PtternMatchingResult _inspectionCounters = PtternMatchingResult.GetInstace();

        private IntPtr ptrVideoHandle;

        private string _currentPLCValueInput = string.Empty;

        private string _lastPLCValueInput = string.Empty;

        //  private SerialPort sp = null;
        private List<Model> listOfModels = new List<Model>();



        public string sErrMsg { get; set; }

        public string _FolderPath { get; set; }

        int _gcCollectorCount = 0;

        public bool _isSoftwareTriggerStarted = false;



        public BlobMainContainer()
        {

            InitializeComponent();

            timer_CurrentTime.Enabled = true;
            LoadCustomerLogo();

            ImageDeleteProcessor.DeleteImagesAfterExpiredDuration(GlobalSettings.ImageDeleteDuration);
            CheckInternet();


            txtNotOKCounts.Text = txtOKCounts.Text = Convert.ToString(0);

            Control.CheckForIllegalCrossThreadCalls = false;
            this.camera.ExceptionRaised += new MatrixBlueFoxCamera.BlueFoxCameraExceptionHandler(this.cam_ExceptionRaised);
            this.camera.ImageSaved += new MatrixBlueFoxCamera.ImageSavedEventHandler(this.cam_ImageSaved);
            this.camera.PatternMatchDetected += new MatrixBlueFoxCamera.PatternMatchDetectionHandler(this.cam_PatternMatchDetected);
            this.camera.ModelReceivedNotification += new MatrixBlueFoxCamera.BlueFoxModelReceivedNotificationHandler(this.cam_ModelReceivedNotification);
            this.camera.UpdateLiveImage += new MatrixBlueFoxCamera.UpdateLiveImageHandler(this.cam_UpdateLiveImage);
            this.camera.ShowReport += new MatrixBlueFoxCamera.ShowReportHandler(this.camera_ShowReport);
            this.camera.ResetReport += new MatrixBlueFoxCamera.ResetReportHandler(this.camera_ResetReport);
            Control.CheckForIllegalCrossThreadCalls = false;

            InitializeFolderStructures();


            var settings = StartupSettings.LoadSettings();

            CheckIfApplicationWasRestarted(settings);

        }

        private void camera_ResetReport(string modelValue, string plcValue)
        {
            reportsData.Clear();
            reportForm.Hide();
        }

        private void camera_ShowReport(string modelValue, string plcValue)
        {
            try
            {                
                this.BeginInvoke((Action)delegate
                {

                    ReportDTO report = new ReportDTO();
                    reportForm.SetReportData(reportsData);
                    reportForm.ShowReport();

                });

            }
            catch (Exception ex)
            {
            }
        }


        private void CheckInternet()
        {

            logger.Error("Logging framework started");
            logger.Debug("-----Ready to process the image.-------");
            try
            {
                bool internetStatus = VilaniHelpers.CheckInternet();
                offlineStatusLabel.Visible = !internetStatus;
                onlineStatusLabel.Visible = internetStatus;
            }
            catch (Exception ex)
            {

                logger.Error("Checking Internet . " + ex.Message);
            }
        }

        private void LoadCustomerLogo()
        {
            try
            {
                pictureBoxCustomerLogo.ImageLocation = Path.Combine(Path.Combine(Application.StartupPath, VisionConstants.CONTENT_FOLDER_NAME), "logo.png");

            }
            catch (Exception)
            {
                logger.Error("Error while loading logo");
            }
        }

        private void CheckIfApplicationWasRestarted(List<Settings> settings)
        {
            var isAppRestartedByException = settings.Where(x => x.SettingName == VisionConstants.APP_RESTART_BY_EXCEPTION).FirstOrDefault();
            if (isAppRestartedByException != null)
            {
                string[] values = isAppRestartedByException.SettingValue.ToString().Split('-');

                if (values.Length == 3)
                {
                    _inspectionCounters.SetCounters(Convert.ToInt32(values[1]), Convert.ToInt32(values[2]));
                    this.txtOKCounts.Text = _inspectionCounters.OKCount.ToString();
                    this.txtNotOKCounts.Text = _inspectionCounters.NotOKCount.ToString();
                    this.lblTotalCounts.Text = string.Format("Total Counts : {0}", _inspectionCounters.TotalCounts);
                    applicationLogger.Info(string.Format("Application Opened from AutoRestart mode with Ok: {0} and NotOK: {1} counts",
                        _inspectionCounters.OKCount, _inspectionCounters.NotOKCount));
                }

                this._dbAccess.DeleteSetting(isAppRestartedByException.SettingID);
                return;
            }
            else
            {
                applicationLogger.Info(string.Format("Application Opened Manually at {0}", System.DateTime.Now));
            }
        }



        private void InitializeFolderStructures()
        {
            DirectorFileHelper.CreateCaputredImageFolder();
            DirectorFileHelper.CreateReferenceImageFolder();
            DirectorFileHelper.CreateProcessedImageFolder();
        }


        private void cam_ExceptionRaised(string sMessage)
        {
            try
            {
                this.lblStatus.ForeColor = Color.Red;
                this.lblStatus.Text = sMessage;
                logger.Error("cam_ExceptionRaised Message. " + sMessage);
            }
            catch (OutOfMemoryException ex)
            {
                logger.Fatal("OUT OF MEMORY BY VILANI MAIN CONTAINER1 : " + ex.Message);
                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {

                logger.Error(ex);
            }

        }

        private void cam_ImageSaved(string sImgPath)
        {
            try
            {
                this._CurrentCaputuredImagePath = sImgPath;
                this.lblCapturedImageResult.Text = "";

                var tempImage = this.pictureBoxCapturedImage.Image;

                System.Drawing.Image image = System.Drawing.Image.FromFile(sImgPath);
                this.pictureBoxCapturedImage.Image = image;
                this.pictureBoxCapturedImage.Update();


                this.pictureBoxCapturedImage.Tag = sImgPath;
                this.lblStatus.Text = sImgPath;

                this.pictureBoxPreviousImage.Image = tempImage;
                if (this._sPreviousResult.ToUpper() == "OK")
                {
                    this.lblPreviousImageResult.ForeColor = Color.Green;
                }
                else
                {
                    this.lblPreviousImageResult.ForeColor = Color.Red;
                }
                this.lblPreviousImageResult.Text = this._sPreviousResult;

            }
            catch (OutOfMemoryException ex)
            {
                logger.Fatal("OUT OF MEMORY BY cam_ImageSaved : " + ex.Message);
                Thread.Sleep(10000);
            }
            catch (Exception arg)
            {
                logger.Error("cam_ImageSaved." + arg);
            }
        }



        private void cam_PatternMatchDetected(List<PatternResult> matchingResult)
        {
            logger.Debug("Pattern Detected.");
            try
            {
                InjectResultIntoGridView(matchingResult);
                var isMatching = UpdateCounters(matchingResult);
                UpdateCapturedAndPreviousImages(isMatching);
                this.SendResult(isMatching);
                UpdateCPUCycle();

                AddResultToReport(matchingResult);


            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                ExceptionOccured(ex);
            }
            catch (OutOfMemoryException ex)
            {
                ExceptionOccured(ex);
            }
            catch (Exception ex)
            {
                ExceptionOccured(ex);
            }
        }

        private void AddResultToReport(List<PatternResult> matchingResult)
        {
            var result = matchingResult[0];

            ReportDTO item = new ReportDTO();
            item.SrNo = reportsData.Count + 1;
            item.PartNo = result.ModelName;
            item.Score = Convert.ToInt32(result.SetScore);
            item.ActualScore = result.ActualScore;
            if (result.IsValid == true)
            {
                item.Status = VisionConstants.OK_TEXT;
            }
            else item.Status = VisionConstants.NOTOK_TEXT;
            reportsData.Add(item);
        }

        private void SendResult(bool isMatching)
        {
            if (isMatching)
            {
                _communicationPort.Write(GlobalSettings.OKSendText);
            }
            else
            {
                _communicationPort.Write(GlobalSettings.NotOKSendText);
            }
        }

        private void UpdateCPUCycle()
        {
            _currentStopWatcher.Stop();
            if (GlobalSettings.IsCPUCycleTimeShow)
                toolStripCycleTime.Text = string.Format("Last Cycle Time : {0} seconds", _currentStopWatcher.Elapsed.TotalSeconds);
            else
                toolStripCycleTime.Text = string.Empty;
        }

        private void UpdateCapturedAndPreviousImages(bool flag)
        {
            if (this._CurrentCaputuredImagePath != "")
            {
                this._PreviousCapturedImagePath = this._CurrentCaputuredImagePath;
                if (flag)
                {
                    this.lblCapturedImageResult.ForeColor = Color.Green;
                }
                else
                {
                    this.lblCapturedImageResult.ForeColor = Color.Red;
                }
                this.lblCapturedImageResult.Text = (this._sPreviousResult = (flag ? "OK" : "NOT OK"));

            }
            this.pictureBoxCapturedImage.Refresh();
        }

        private bool UpdateCounters(List<PatternResult> matchingResult)
        {
            bool flag = false;
            int trueCount = 0;

            trueCount = matchingResult.Where(x => x.IsValid).Count();
            flag = trueCount > 0 ? true : false;
            _inspectionCounters.SetCounters(flag);
            this.txtOKCounts.Text = _inspectionCounters.OKCount.ToString();
            this.txtNotOKCounts.Text = _inspectionCounters.NotOKCount.ToString();
            this.lblTotalCounts.Text = string.Format("Total Counts : {0}", _inspectionCounters.TotalCounts);
            this.lblTotalTriggerRecived.Text = string.Format("Total Triggers : {0}", _inspectionCounters.TotalTriggerRecieved);

            return flag;
        }


        private void InjectResultIntoGridView(List<PatternResult> lstResult)
        {

            _caputedRectangles = null;
            this.flowLayoutPanelRefImages.Controls.Cast<ReferenceImage>().ToList().ForEach(x => x.SetResult("", ""));

            int counter = 0;
            for (int i = 0; i < lstResult.Count; i++)
            {
                var refControl = _allReferenceImagesInjecteds.Where(x => x.ReferenceImageID == lstResult[i].ReferenceImageID).FirstOrDefault();

                if (refControl == null)
                {
                    refControl = new ReferenceImage();
                    refControl.ReferenceImageID = lstResult[i].ReferenceImageID;
                    _allReferenceImagesInjecteds.Add(refControl);
                }
                refControl.SetResult(string.Format("{0} - {1}%",
                    lstResult[i].IsValid ? "OK" : "NOT OK",
                    Math.Round(lstResult[i].ActualScore, 2)),
                    lstResult[i].IsValid ? GlobalSettings.OKSendText : GlobalSettings.NotOKSendText);

                try
                {
                    var rect = _reactanlgeDrawnContainer.GetElementAt(i);
                    rect.SourceImagePath = lstResult[i].CapImageName;
                    rect.Rectangle = lstResult[i].AOIRectangle;
                }
                catch (IndexOutOfRangeException ex)
                {
                    logger.Error(ex);
                }


                counter++;
            }
            if (counter > 0)
            {
                _caputedRectangles = _reactanlgeDrawnContainer.Take(counter);
            }
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceImageFiles"></param>
        /// <param name="modelName"></param>
        /// <param name="plcValue"></param>
        private void cam_ModelReceivedNotification(List<Item> referenceImageFiles, string modelName, string plcValue)
        {
            _currentStopWatcher.Restart();

            try
            {


                _gcCollectorCount++;
                //if (_gcCollectorCount == 9 || _gcCollectorCount == 18)
                //{
                //    throw new OutOfMemoryException("The operation completed successfully");
                //}

                if (_lastPLCValueInput == plcValue)
                {
                    return;
                }

                if (base.InvokeRequired)
                {
                    try
                    {
                        base.BeginInvoke(new MethodInvoker(delegate
                        {
                            this.cam_ModelReceivedNotification(referenceImageFiles, modelName, plcValue);
                        }));

                    }
                    catch (Exception ex)
                    {

                        ExceptionOccured(ex);
                    }

                }
                else
                {

                    this.flowLayoutPanelRefImages.Controls.Clear();

                    foreach (Item current in referenceImageFiles)
                    {
                        ReferenceImage ucRefImage = null;
                        string text = Path.Combine(Path.Combine(Application.StartupPath, VisionConstants.REFERENCE_IMG_FOLDER_NAME), current.Name);
                        if (File.Exists(text))
                        {

                            var alreadyAddedControl = _allReferenceImagesInjecteds.Where(x => x.ReferenceImageID == current.ReferenceImageID).FirstOrDefault();

                            if (alreadyAddedControl != null)
                                ucRefImage = alreadyAddedControl;
                            else
                            {
                                ucRefImage = new ReferenceImage();
                                ucRefImage.ReferenceImageID = current.ReferenceImageID;
                                _allReferenceImagesInjecteds.Add(ucRefImage);
                            }

                            ucRefImage.ImageName = current.Name;
                            ucRefImage.Tag = current.Name;
                            using (System.Drawing.Image image = System.Drawing.Image.FromFile(text))
                            {
                                ucRefImage.CurrentReferenceImage = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                            }
                            ucRefImage.NumOfOccurances = current.Value;
                            ucRefImage.Score = current.Score;
                            ucRefImage.NumOfOccurancesControlEnabled = true;
                            ucRefImage.SetResult("", "");
                            ucRefImage.SetSize(140, 180);
                            if (GlobalSettings.ShowReactanglesOnSourceImage)
                                ucRefImage.CurrentColor = ReactngleColorCodesContainer.GetInstance.GetElementAt(current.SequenceNumber);
                            // if (!IsImageAdded(ucRefImage, current.ReferenceImageID))
                            this.flowLayoutPanelRefImages.Controls.Add(ucRefImage);




                        }

                    }
                    if (this._previousModelName != "")
                    {
                        this.lblPreviousModel.Text = "PREVIOUS MODEL : " + this._previousModelName;
                    }
                    this._currentModelName = modelName;
                    this.lblCurrentModelName.Text = "CURRENT MODEL : " + this._currentModelName;
                    this._previousModelName = this._currentModelName;
                    this._lastPLCValueInput = plcValue;

                }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                ExceptionOccured(ex);
            }
            catch (OutOfMemoryException ex)
            {
                ExceptionOccured(ex);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ExceptionOccured(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private Object thisLock = new Object();
        bool _isAppRestartIsInProcess = false;
        private void ExceptionOccured(Exception ex)
        {
            try
            {
                switch (GlobalSettings.MemoryExceptionHandledBy)
                {
                    case 2:
                        lock (thisLock)
                        {
                            _isAppRestartIsInProcess = true;
                            logger.Fatal("OUT OF MEMORY HANDLED BY PROCESS RESTRUCTURE: " + ex.Message);
                            _communicationPort.Close();
                            _allReferenceImagesInjecteds = new List<ReferenceImage>();
                            this.camera.liveCamThread_OutOfMemoryExceptionOccured(ex.Message, _isSoftwareTriggerStarted);
                            GC.Collect();
                            Thread.Sleep(5000);
                            _communicationPort.Open();
                        }
                        break;

                    case 3:
                        lock (thisLock)
                        {
                            logger.Fatal("OUT OF MEMORY HANDLED BY APPLICATION RESTART: " + ex.Message);
                            List<Settings> settings = new List<Settings>();
                            settings.Add(new Settings(VisionConstants.APP_RESTART_BY_EXCEPTION, string.Format("{0}-{1}-{2}", "OutOfMemoryException", _inspectionCounters.OKCount,
                                _inspectionCounters.NotOKCount)));
                            this._dbAccess.UpdateSettings(settings);
                            applicationLogger.Info(string.Format("Application Restarted Automatic on Out Of Memory at {0}", System.DateTime.Now));

                            _communicationPort.Close();

                            Application.Exit();
                            Thread.Sleep(1000);
                            if (IsProcessOpen("Vilani.MatrixVision.exe") == false)
                            {
                                Application.Run(new MainContainer());
                            }
                        }
                        break;
                }
            }
            catch (Exception exception)
            {
                logger.Fatal("Error while Handling memory exception or restarting : " + ex.Message);
                Application.Restart();
            }
            _isAppRestartIsInProcess = false;

        }

        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }


        private void cam_UpdateLiveImage(System.Drawing.Image img)
        {
            try
            {
                _currentImage = img;
                this.pictureBoxLive.Image = img;
                this.pictureBoxLive.Update();

            }
            catch (Exception exception)
            {
                logger.Error("cam_UpdateLiveImage", exception);
            }
        }



        private void createReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreateReferenceImage form = new CreateReferenceImage(_currentImage);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                throw;
            }

        }




        private void MainContainer_Shown(object sender, EventArgs e)
        {

            Validation frmValidation = new Validation();
            if (!frmValidation.ValidateLicense())
            {
                frmValidation.ShowDialog();
                base.Dispose();
            }
            else
            {
                SetupStarupSettingsForVision();
                this.timerAutoTrigger.Interval = 1000;
                this.camera.InitializeCamera(this.ptrVideoHandle);
                this.camera.SetPort(_communicationPort);
                Thread.Sleep(1000);
                this.StartCamera();

            }
        }

        private void SetupStarupSettingsForVision()
        {
            try
            {
                this.listOfModels = this._dbAccess.GetAllModelsDataWithReferneceImages();
                _communicationPort = _portGenerator.GetPortForCommunication();

                if (GlobalSettings.IsStartPortOnAppStart && _communicationPort != null)
                {
                    try
                    {
                        _communicationPort.Open();
                        SetPortStatus(true);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error while opening the port: " + ex.Message);
                    }

                }
                else
                {
                    SetPortStatus(false);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while reading Model Details: " + ex.Message);
            }
        }


        private void StartCamera()
        {

            int modelID = 0;
            string modelName = string.Empty;
            bool flag = ValidateIfReferenceImgeLinkingIsValidForModel(out modelID, out modelName);
            if (!flag)
            {
                string message = string.Format("Some of the Reference Images configured are missing in the ReferenceImages folder for model {0}.\r\nPlease press, YES to delete this Model {0} or Cancel To ignore.....", modelName);

                if (MessageBox.Show(message, "Confirm Model Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this._dbAccess.DeleteModel(modelID);
                }
            }

            if (_communicationPort != null)
                this.toolStripStatusLabelPort.Text = _communicationPort.DisplayText();
            this.toolStripStatusLabelPort.ForeColor = Color.Blue;
            this.camera.SetCameraConfigurations(this.listOfModels);
            this.camera.StartCamera(this._FolderPath);

        }

        private bool ValidateIfReferenceImgeLinkingIsValidForModel(out int modelID, out string modelName)
        {
            bool flag = true;
            modelName = string.Empty;
            modelID = 0;
            foreach (Model current in this.listOfModels)
            {
                foreach (Item current2 in current.ReferenceImageOccurancePairList)
                {
                    string path = Path.Combine(DirectorFileHelper.GetReferenceImageFolderPath, current2.Name);
                    if (!File.Exists(path))
                    {
                        flag = false;
                        modelName = current.ModelName;
                        modelID = current.ModelID;
                        break;
                    }
                }
                if (!flag)
                {
                    break;
                }
            }
            return flag;
        }


        private void SetPortStatus(bool status)
        {
            toolStripStatusCommunicationOFF.Visible = !status;
            toolStripStatusCommunicationPort.Visible = status;
            btnTrigger.Enabled = !status;
            btnAutoTrigger.Enabled = !status;
            txtPLCInputValue.Enabled = !status;
        }



        private void btnTrigger_Click_1(object sender, EventArgs e)
        {
            _currentStopWatcher = System.Diagnostics.Stopwatch.StartNew();

            if (string.IsNullOrEmpty(this.txtPLCInputValue.Text))
            {
                MessageBox.Show("Please enter PLC Input Value for Software trigger simulation");
            }
            //else if (string.IsNullOrEmpty(GlobalSettings.ManulTriggerSourceImagesLocation))
            //{
            //    MessageBox.Show("For Manual triggering Captured images source is required. \r\nPlease specify source location of Captured Images.", "Reference Image(s) Source Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            else
            {
                if (string.IsNullOrEmpty(_currentPLCValueInput) && this.txtPLCInputValue.Text != _currentPLCValueInput)
                    StopManualTriggeringForCurrentModel();

                if (this._dbAccess.IsModelPLCInputValueExists(this.txtPLCInputValue.Text))
                {
                    linkLabelStop.Visible = true;
                    _inspectionCounters.AddTriggerCount();
                    this.camera.liveCamThread_ManualTriggerEvent();
                    this.camera.SetSoftwareTrigger(this.txtPLCInputValue.Text);
                    _currentPLCValueInput = this.txtPLCInputValue.Text;
                }
                else
                {
                    MessageBox.Show("The entered PLC Input value does not belongs to any model. \n Please enter valid PLC Input Value");
                }
            }
        }



        private void pictureBoxCapturedImage_Paint(object sender, PaintEventArgs e)
        {
            if (GlobalSettings.ShowReactanglesOnSourceImage == false)
                return;

            if (_caputedRectangles != null && _caputedRectangles.Count > 0)
            {

                int coutner = 0;
                foreach (var item in _caputedRectangles)
                {
                    // var imagePath = Path.Combine(Path.Combine(Application.StartupPath, VisionConstants.CAP_IMG_FOLDER_NAME), item.Key);
                    System.Drawing.Image image = System.Drawing.Image.FromFile(item.SourceImagePath);
                    int width = image.Width;
                    int height = image.Height;


                    System.Drawing.Rectangle displayRectangle = this.pictureBoxCapturedImage.DisplayRectangle;
                    float widthFactor = (float)width / (float)displayRectangle.Width;
                    float hightFactor = (float)height / (float)displayRectangle.Height;



                    int tarWidth = Convert.ToInt32(item.Rectangle.Width / widthFactor);
                    int tarHeight = Convert.ToInt32(item.Rectangle.Height / hightFactor);

                    int tarX = Convert.ToInt32(item.Rectangle.X / widthFactor);
                    int tarY = Convert.ToInt32(item.Rectangle.Y / hightFactor);

                    System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(tarX, tarY, (int)tarWidth, (int)tarHeight);
                    Pen pen = GetPaintPenByImageNumber(coutner);
                    e.Graphics.DrawRectangle(pen, srcRect);

                    coutner++;
                }


            }
        }

        private Pen GetPaintPenByImageNumber(int coutner)
        {
            Pen pen = new Pen(ReactngleColorCodesContainer.GetInstance.GetElementAt(coutner));
            pen.DashStyle = DashStyle.Solid;
            return pen;
        }

        private void createReferenceImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreateReferenceImage form = new CreateReferenceImage(_currentImage);
                form.ShowDialog();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void systemConfigurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SystemConfiguration form = new SystemConfiguration();
                form._CurrentCameraImage = _currentImage;
                form.UpdateModelList += form_UpdateModelList;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void form_UpdateModelList()
        {
            this.listOfModels = this._dbAccess.GetAllModelsDataWithReferneceImages();
            _lastPLCValueInput = string.Empty;
            this.camera.SetCameraConfigurations(this.listOfModels);
        }

        private void linkLabelStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StopManualTriggeringForCurrentModel();
        }

        private void StopManualTriggeringForCurrentModel()
        {
            this.camera.StopSoftwareTrigger(this.txtPLCInputValue.Text);
            this.linkLabelStop.Visible = false;
            this.camera.StopSoftwareManualTriggerMonitor();
        }

        private void toolStripStatusSerialPortON_Click(object sender, EventArgs e)
        {
            _communicationPort.Close();
            SetPortStatus(false);
            StopManualTriggeringForCurrentModel();

        }

        private void toolStripStatusSerialPortOFF_Click(object sender, EventArgs e)
        {
            try
            {
                _communicationPort.Open();
                SetPortStatus(true);
                StopManualTriggeringForCurrentModel();
            }
            catch (Exception ex)
            {

                logger.Error(ex);
            }

        }



        private void MainContainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            _communicationPort.Close();
            this.camera.AbortThreads();
        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ImageDeleteProcessor.DeleteImagesAfterExpiredDuration(GlobalSettings.ImageDeleteDuration);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            toolStripStatusTime.Visible = true;
            toolStripStatusTime.Text = "Old Images removed successfully";

        }

        private void MainContainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.camera.StopCamera();
                Thread.Sleep(500);
                _communicationPort.Close();
                Thread.Sleep(200);
            }
            catch
            {
            }
        }

        private void btnAutoTrigger_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPLCInputValue.Text))
            {

                if (btnAutoTrigger.Text == "Start Auto Trigger")
                {
                    this.timerAutoTrigger.Enabled = true;

                    if (this._dbAccess.IsModelPLCInputValueExists(this.txtPLCInputValue.Text))
                    {
                        _inspectionCounters.AddTriggerCount();
                        this.camera.liveCamThread_ManualTriggerEvent();
                        this.camera.SetSoftwareTrigger(this.txtPLCInputValue.Text);
                        _isSoftwareTriggerStarted = true;
                    }
                    else
                    {
                        MessageBox.Show("The entered PLC Input value does not belongs to any model. \n Please enter valid PLC Input Value");
                    }

                    btnAutoTrigger.Text = "Stop Auto Trigger";
                }
                else if (btnAutoTrigger.Text == "Stop Auto Trigger")
                {
                    this.timerAutoTrigger.Enabled = false;
                    StopManualTriggeringForCurrentModel();
                    btnAutoTrigger.Text = "Start Auto Trigger";
                    _isSoftwareTriggerStarted = false;
                }
            }
            else
            {
                MessageBox.Show("Please enter PLC Input Value for Software trigger simulation");

            }
        }

        private void timerAutoTrigger_Tick(object sender, EventArgs e)
        {
            this.camera.SetSoftwareTrigger(this.txtPLCInputValue.Text);


        }



        int _serialPortInternal = 0;
        private void timer_CurrentTime_Tick(object sender, EventArgs e)
        {
            try
            {
                toolStripStatusTime.Text = System.DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss");
                var counter = Convert.ToInt32(ConfigurationManager.AppSettings["SerialPortStartIntervel"]);
                _serialPortInternal++;

                //      lblMemoryInfo.Text = Convert.ToString(process.PagedMemorySize64);

                if (System.DateTime.Now.Hour == 23 && System.DateTime.Now.Minute >= 58)
                {
                    logger.Info("Creating folders for image storing");
                    DirectorFileHelper.CreateCaputredImageFolder();
                    DirectorFileHelper.CreateProcessedImageFolder();
                }

                //if (toolStripStatusCommunicationPort.Visible
                //   && !_communicationPort.IsOpen() && !_isAppRestartIsInProcess)
                //{
                //    _communicationPort.Open();
                //    logger.Info("Serial port was closed hence reopening automatically");
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                Reports form = new Reports();


                form.ShowDialog();
            }
            catch (Exception ex)
            {
            }

        }

        private void backgroundWorkerReport_DoWork(object sender, DoWorkEventArgs e)
        {

        }










    }
}
