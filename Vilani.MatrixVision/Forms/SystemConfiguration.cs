using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using Vilani.MatrixVision.Common;
using Vilani.MatrixVision.Core;
using Vilani.MatrixVision.DataBase;
using Vilani.MatrixVision.Forms;
using Expert.Common.Library;
using Vilani.MatrixVision.UserControls;
using Vilani.WindowsApp.Forms.Common;
using System.Drawing.Imaging;
using Expert.Common.Library.Enumrations;
using Expert.Common.Library.DTOs;
using Vilani.Licenses;

namespace Vilani.MatrixVision
{



    public partial class SystemConfiguration : Form
    {
        DataBaseAccess dbAccess = new DataBaseAccess();
        SerialPortDTO _serialPort = new SerialPortDTO();
        TcpIPDTO _tcpIp = null;

        public Model _CurrentModel { get; set; }

        public delegate void UpdateModelListHandler();
        public event UpdateModelListHandler UpdateModelList;
        public Image _CurrentCameraImage { get; set; }

        public SystemConfiguration()
        {

            InitializeComponent();
            comboConnectionType.DataSource = Enum.GetNames(typeof(TCPIPConnectionType));
            this.comboBoxParity.DisplayMember = "Name";
            this.comboBoxParity.ValueMember = "Value";
            this.comboBoxParity.DataSource = this.GetParityDataSource();
            this.comboBoxStopBits.DisplayMember = "Name";
            this.comboBoxStopBits.ValueMember = "Value";
            this.comboBoxStopBits.DataSource = this.GetStopBitsDataSource();

            this.comboBoxListOfModels.DisplayMember = "ModelName";
            this.comboBoxListOfModels.ValueMember = "ModelID";
            this.comboBoxListOfModels.DataSource = this.dbAccess.GetModelsListDataSource();

            _serialPort = this.dbAccess.GetSerialPortData(true);

            if (FeatureSettings.SerialPortCommunicationsSupported)
            {
                this.numericUpDownCOM.Value = Convert.ToDecimal(_serialPort.PortNumber.Replace("COM", ""));
                this.numericUpDownBaudRate.Value = Convert.ToDecimal(_serialPort.BaudRate);
                this.numericUpDownDataBits.Value = Convert.ToInt32(_serialPort.DataBits);
                this.comboBoxParity.SelectedValue = Convert.ToInt32(_serialPort.Parity);
                this.comboBoxStopBits.SelectedValue = Convert.ToInt32(_serialPort.StopBits);
            }
            this.txtOK.Text = _serialPort.OK;
            this.txtNotOK.Text = _serialPort.NotOK;

            PopulateTCPIPDetailsByTypeID((int)TCPIPConnectionType.Read);


            comboBoxImageDeleteDuration.DataSource = Enum.GetNames(typeof(DeleteDuration));

            comboBoxMemoryManagement.DisplayMember = "Name";
            comboBoxMemoryManagement.ValueMember = "ID";

            comboBoxMemoryManagement.Items.Add(new ComboBoxItem("None", 1));
            comboBoxMemoryManagement.Items.Add(new ComboBoxItem("Clearing the resources (Without Data loss -Slower)", 2));
            comboBoxMemoryManagement.Items.Add(new ComboBoxItem("Application Restart (Data may loss -Faster)", 3));

            var collections = tabControlSystemConfiguration.TabPages;

            SetupConfiugurationTabs();


            var testPassword = LicenseValidator.Encrypt("vilasanita", VisionConstants.PASSWORD);
            LoadSettings();

        }

        private void SetupConfiugurationTabs()
        {
            if (!FeatureSettings.TCPIPCommunicationsSupported)
                tabControlSystemConfiguration.TabPages.Remove(tabControlSystemConfiguration.TabPages[2]);


            if (!FeatureSettings.SerialPortCommunicationsSupported)
                tabControlSystemConfiguration.TabPages.Remove(tabControlSystemConfiguration.TabPages[1]);
        }

        private void LoadSettings()
        {
            foreach (var item in StartupSettings.LoadSettings())
            {
                switch (item.SettingName)
                {
                    case VisionConstants.SETTINGS_SHOW_REACTNGLES:
                        chkShowReactanglesOnSourceImage.Checked = Convert.ToBoolean(item.SettingValue);
                        break;

                    case VisionConstants.SOURCE_IMAGES_TRIGGER_LOCATION:
                        txtSourceImagePath.Text = Convert.ToString(item.SettingValue);
                        break;

                    case VisionConstants.SHOW_CPU_CYCLE_TIME:
                        chkShowCPUCycleTime.Checked = Convert.ToBoolean(item.SettingValue);
                        break;

                    case VisionConstants.PASSWORD:
                        txtPassword.Text = LicenseValidator.Decrypt(Convert.ToString(item.SettingValue), VisionConstants.PASSWORD);
                        break;

                    case VisionConstants.START_PORT_ON_STARTUP:
                        chkSerialPortStart.Checked = Convert.ToBoolean(item.SettingValue);
                        break;

                    case VisionConstants.IMAGE_DELETE_DURATION:
                        comboBoxImageDeleteDuration.SelectedIndex = Convert.ToInt32(item.SettingValue);
                        break;

                    case VisionConstants.MEMORY_EXCEPTION_HANDLED_BY:

                        switch (Convert.ToInt32(item.SettingValue))
                        {
                            case 1:
                                comboBoxMemoryManagement.SelectedIndex = 0;

                                break;
                            case 2:
                                comboBoxMemoryManagement.SelectedIndex = 1;
                                break;
                            case 3:
                                comboBoxMemoryManagement.SelectedIndex = 2;
                                break;
                            default:
                                break;
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        private List<Item> GetStopBitsDataSource()
        {
            List<Item> list = new List<Item>();
            Array values = Enum.GetValues(typeof(StopBits));
            byte[] array = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                array[i] = Convert.ToByte(values.GetValue(i));
            }
            byte[] array2 = array;
            for (int j = 0; j < array2.Length; j++)
            {
                byte b = array2[j];
                list.Add(new Item
                {
                    Name = Enum.GetName(typeof(StopBits), b),
                    Value = (int)b
                });
            }
            return list;
        }

        private List<Item> GetParityDataSource()
        {
            List<Item> list = new List<Item>();
            Array values = Enum.GetValues(typeof(Parity));
            byte[] array = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                array[i] = Convert.ToByte(values.GetValue(i));
            }
            byte[] array2 = array;
            for (int j = 0; j < array2.Length; j++)
            {
                byte b = array2[j];
                list.Add(new Item
                {
                    Name = Enum.GetName(typeof(Parity), b),
                    Value = (int)b
                });
            }
            return list;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {

            SaveFormUIValues();
            this.dbAccess.UpdateSerialPortConfiguration(_serialPort);
            ShowFeedbackMessage();
        }

        private void ShowFeedbackMessage()
        {
            if (_serialPort.iErrCode == 0)
            {
                MessageBox.Show("Congratulations! Configuration saved Successfully!\r\n", "Configuration Save Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Error while updating the configuration.\r\nPlease verify all the necessary fields are present & restart the application.", "Error while Update", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void SaveFormUIValues()
        {
            var baudRate = this.numericUpDownBaudRate.Value;
            var parity = Convert.ToInt32(this.comboBoxParity.SelectedValue);
            _serialPort.PortNumber = this.numericUpDownCOM.Value.ToString();
            _serialPort.BaudRate = Convert.ToInt32(baudRate);
            _serialPort.Parity = (Parity)parity;
            _serialPort.StopBits = (StopBits)this.comboBoxStopBits.SelectedValue;
            _serialPort.DataBits = Convert.ToInt32(this.numericUpDownDataBits.Value);
            _serialPort.OK = this.txtOK.Text;
            _serialPort.NotOK = this.txtNotOK.Text;
        }

        private void btnSaveOutputConfig_Click(object sender, EventArgs e)
        {
            SaveFormUIValues();
            _serialPort.iErrCode = this.dbAccess.UpdateSerialPortConfiguration(_serialPort);
            ShowFeedbackMessage();
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            AddModelName form = new AddModelName();
            form.NewModelCreateHandlerEvent += form_NewModelCreateHandlerEvent;
            form.ShowDialog();
        }

        void form_NewModelCreateHandlerEvent(int modelID, string modelName)
        {
            this.comboBoxListOfModels.DataSource = this.dbAccess.GetModelsListDataSource();
            this.comboBoxListOfModels.SelectedValue = modelID;

        }

        private void comboBoxListOfModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadModelDetails();
        }

        private void LoadModelDetails()
        {
            try
            {
                var modelID = Convert.ToInt32(this.comboBoxListOfModels.SelectedValue);
                _CurrentModel = this.dbAccess.GetModelDetails(modelID);
                _CurrentModel.ReferenceImages = this.dbAccess.GetModelReferenceImages(modelID);
                txtPLCInputValue.Text = _CurrentModel.PLCInputValue;
                this.UpdateReferenceImages(_CurrentModel.ReferenceImages);
                btnDeleteSelectedImages.Enabled = _CurrentModel.ReferenceImages.Count > 0 ? true : false;
                txtModelName.Text = _CurrentModel.ModelName;
                chkInvertResult.Checked = _CurrentModel.InvertResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to get the Model Details.\r\n", "Unable to get model details.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

        }

        private void UpdateReferenceImages(List<ReferenceImageDB> refImagesNames)
        {
            this.flowLayoutPanelRefImage.Controls.Clear();
            foreach (ReferenceImageDB current in refImagesNames)
            {
                string text2 = Path.Combine(DirectorFileHelper.GetReferenceImageFilePath(current.ImageName));
                if (File.Exists(text2))
                {
                    ReferenceImage ucRefImage = new ReferenceImage();
                    ucRefImage.ReferenceImageID = current.ReferenceImageID;
                    ucRefImage.ImageName = current.ImageName;
                    ucRefImage.ImageLocation = text2;
                    ucRefImage.Tag = current.ImageName;
                    using (FileStream stream = new FileStream(text2, FileMode.Open, FileAccess.Read))
                    {
                        ucRefImage.CurrentReferenceImage = Image.FromStream(stream);
                    }
                    // ucRefImage.CurrentReferenceImage = Image.FromFile(text2);
                    ucRefImage.IsImageSelected = false;
                    ucRefImage.NumOfOccurances = current.NumberOfOccurances;
                    ucRefImage.Score = current.Score;
                    ucRefImage.SetCheckBoxVisible(true);
                    this.flowLayoutPanelRefImage.Controls.Add(ucRefImage);

                }
            }
        }

        private void btnDeleteModel_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Deleting the model will remove the associated configurations.\r\nAre you sure you want to delete the selected model?", "Confirm Model Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var modelName = this.comboBoxListOfModels.SelectedText.ToString();
                var id = this.dbAccess.DeleteModel(Convert.ToInt32(this.comboBoxListOfModels.SelectedValue));

                if (id > 0)
                {
                    MessageBox.Show(string.Format("Deleted the selected model {0} successfully.", modelName));
                    this.comboBoxListOfModels.DataSource = this.dbAccess.GetModelsListDataSource();
                    this.LoadModelDetails();
                }
                else
                {
                    MessageBox.Show("Unable to Delete the model.");
                }
            }
        }

        private void btnAddReferenceImage_Click(object sender, EventArgs e)
        {
            CreateReferenceImage form = new CreateReferenceImage(_CurrentCameraImage);
            form.SetSaveButtonText(string.Format("Add Reference Image to Model {0}", _CurrentModel.ModelName));
            form._ModelID = _CurrentModel.ModelID;
            form._ModelName = _CurrentModel.ModelName;
            form.ReferenceImageAssignedEvent += form_ReferenceImageAssignedEvent;
            form.ShowDialog();
        }

        void form_ReferenceImageAssignedEvent()
        {
            LoadModelDetails();
        }

        private void btnDeleteSelectedImages_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deleting the image(s) will remove it from Model.\r\nAre you sure you want to delete the selected Images?", "Confirm Images(s) Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                List<Control> list = new List<Control>();
                for (int i = 0; i < this.flowLayoutPanelRefImage.Controls.Count; i++)
                {
                    Control control = this.flowLayoutPanelRefImage.Controls[i];
                    if (((ReferenceImage)control).IsImageSelected)
                    {
                        list.Add(control);
                        this.dbAccess.DeleteReferenceImage(((ReferenceImage)control).ReferenceImageID);
                        //FileInfo file = new FileInfo(((ReferenceImage)control).ImageLocation);

                        //if (file.Exists)
                        //{
                        //    file.Delete();
                        //}

                    }
                }
                foreach (Control current in list)
                {
                    this.flowLayoutPanelRefImage.Controls.Remove(current);
                }
            }
        }



        private void SystemConfiguration_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void SystemConfiguration_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void btnSaveModelConfiguration_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.txtPLCInputValue.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("PLC Input Value cannot be empty!", "In-Complete Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(this.txtModelName.Text))
                {
                    MessageBox.Show("Please fill the Model Name.", "In-Complete Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (this.flowLayoutPanelRefImage.Controls.Count <= 0)
                {
                    MessageBox.Show("Please select at least one reference image for a model.", "InComplete Detail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                _CurrentModel.ModelName = txtModelName.Text;
                _CurrentModel.PLCInputValue = txtPLCInputValue.Text;
                _CurrentModel.InvertResult = chkInvertResult.Checked;

                dbAccess.UpdateModelDetails(_CurrentModel);

                for (int i = 0; i < this.flowLayoutPanelRefImage.Controls.Count; i++)
                {
                    Control control = this.flowLayoutPanelRefImage.Controls[i];
                    var currentReferenceImage = GetCurrentReferenceImage(((ReferenceImage)control).ReferenceImageID);
                    if (currentReferenceImage != null)
                    {
                        currentReferenceImage.Score = ((ReferenceImage)control).Score;
                        this.dbAccess.UpdateModelReferenceImageScore(((ReferenceImage)control).ReferenceImageID,
                            currentReferenceImage.Score);
                    }
                }

                _CurrentModel.ModelName = txtModelName.Text;
                _CurrentModel.PLCInputValue = txtPLCInputValue.Text;
                this.dbAccess.UpdateModelDetails(_CurrentModel);
                UpdateModelList();
                MessageBox.Show("Model updated into the system.\r\n", "Details updated.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //  this.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private ReferenceImageDB GetCurrentReferenceImage(int referenceImageID)
        {
            foreach (var item in _CurrentModel.ReferenceImages)
            {
                if (item.ReferenceImageID == referenceImageID)
                    return item;
            }
            return null;
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            List<Settings> settings = new List<Settings>();
            settings.Add(new Settings(VisionConstants.SETTINGS_SHOW_REACTNGLES, chkShowReactanglesOnSourceImage.Checked.ToString()));
            settings.Add(new Settings(VisionConstants.SOURCE_IMAGES_TRIGGER_LOCATION, txtSourceImagePath.Text));
            settings.Add(new Settings(VisionConstants.SHOW_CPU_CYCLE_TIME, chkShowCPUCycleTime.Checked.ToString()));
            settings.Add(new Settings(VisionConstants.PASSWORD, LicenseValidator.Encrypt(txtPassword.Text.ToString(), VisionConstants.PASSWORD)));
            settings.Add(new Settings(VisionConstants.START_PORT_ON_STARTUP, chkSerialPortStart.Checked.ToString()));
            settings.Add(new Settings(VisionConstants.IMAGE_DELETE_DURATION, comboBoxImageDeleteDuration.SelectedIndex.ToString()));
            settings.Add(new Settings(VisionConstants.IMAGE_DELETE_DURATION, comboBoxOpticalInspection.SelectedIndex.ToString()));
            settings.Add(new Settings(VisionConstants.APPLICATION_USER_NAME, txtAppUserName.Text));
            settings.Add(new Settings(VisionConstants.APPLICATION_PASSWORD, LicenseValidator.Encrypt(txtAppPassword.Text.ToString(), VisionConstants.PASSWORD)));
            var item = comboBoxMemoryManagement.SelectedItem as ComboBoxItem;
            settings.Add(new Settings(VisionConstants.MEMORY_EXCEPTION_HANDLED_BY, item.ID.ToString()));

            this.dbAccess.UpdateSettings(settings);
            StartupSettings.LoadSettings();
            this.UpdateModelList();
            MessageBox.Show("Settings Saved Successfully");
        }

        private void btnSettingsClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool isOpening = false;
        private void tabControlSystemConfiguration_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Text == "Settings" && isOpening == false)
            {
                isOpening = true;
                Password fom = new Password();
                fom.ValidationEvent += fom_ValidationEvent;
                fom.ShowDialog();

            }
            isOpening = false;
        }

        void fom_ValidationEvent(bool result)
        {
            if (result)
            {
                tabControlSystemConfiguration.SelectedIndex = 3;

            }
            else
            {
                tabControlSystemConfiguration.SelectedIndex = 0;
            }
        }

        private void btnAddExisting_Click(object sender, EventArgs e)
        {

            // open file dialog 
            OpenFileDialog open = new OpenFileDialog();
            //     open.InitialDirectory = Path.Combine(Application.StartupPath, VisionConstants.REFERENCE_IMG_FOLDER_NAME);
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {

                if (this.dbAccess.IsReferenceImageAlreadyExists(_CurrentModel.ModelID, open.SafeFileName))
                {
                    MessageBox.Show(string.Format("This reference image '{0} is already assigned to Model '{1}'. \n Please try another image.", open.SafeFileName, _CurrentModel.ModelName), "Duplicate Reference Image", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (!IsFileNameValid(open.SafeFileName))
                {
                    MessageBox.Show(string.Format("Invalid reference file name.\n Suggested file format eg. XXX_1.2.3.4.jpg", open.SafeFileName, _CurrentModel.ModelName), "Duplicate Reference Image", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    ReferenceImageDB image = new ReferenceImageDB();
                    image.ModelID = _CurrentModel.ModelID;
                    image.Score = Convert.ToInt32(65);
                    image.NumberOfOccurances = 1;
                    image.ImageName = open.SafeFileName;
                    image.ImagePath = open.FileName;
                    var errorID = this.dbAccess.AddModelReferenceImage(image);
                    var thisRefPath = DirectorFileHelper.GetReferenceImageFilePath(open.SafeFileName);

                    if (!File.Exists(thisRefPath))
                    {
                        using (FileStream stream = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))
                        {
                            var currentReferenceImage = Image.FromStream(stream);
                            currentReferenceImage.Save(thisRefPath);
                        }
                    }
                    _CurrentModel.ReferenceImages = this.dbAccess.GetModelReferenceImages(_CurrentModel.ModelID);

                    //  MessageBox.Show(string.Format("Reference Image {0} is assigned to the Model '{1}' Successfully.", image.ImageName, _CurrentModel.ModelName), "Reference Image Assigned", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    LoadModelDetails();
                }
            }
        }

        private bool IsFileNameValid(string sRefImgPath)
        {
            try
            {
                string text = sRefImgPath.Substring(sRefImgPath.LastIndexOf('_') + 1).Replace(".jpg", "");
                string[] array = text.Split(new char[]
				{
					'.'
				});

                foreach (var item in array)
                {
                    int num = Convert.ToInt32(item);
                }

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Manual Trigger Image Location";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = fbd.SelectedPath;
                this.txtSourceImagePath.Text = @sSelectedPath;
            }
        }

        private void comboBoxImageDeleteDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = comboBoxImageDeleteDuration.SelectedIndex;

            switch (id)
            {
                case 1:

                case 2:
                default:
                    break;
            }
        }

        private void btnClearPath_Click(object sender, EventArgs e)
        {
            txtSourceImagePath.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveIPConfiguration_Click(object sender, EventArgs e)
        {
            try
            {

                if (IsFormValid())
                {
                    SaveFormTCPIPUIValues();
                    this.dbAccess.UpdateTcpIPPortData(_tcpIp);
                    ShowFeedbackMessage();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private bool IsFormValid()
        {
            string Message = string.Empty;
            bool isValid = true;

            if (string.IsNullOrEmpty(txtConnectionName.Text))
            {
                Message += "Please enter Connection Name\n";
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtPortNumber.Text))
            {
                Message += "Please enter Port Number\n";
                isValid = false;
            }


            if (string.IsNullOrEmpty(txtIPAddress.Text))
            {
                Message += "Please enter IPAddress\n";
                isValid = false;

            }
            else if (!CheckIPValid())
            {
                Message += "IP Address is Not Valid\n";
                isValid = false;
            }



            if (!isValid)
                MessageBox.Show(Message, "Please correct below errors");

            return isValid;
        }

        private void SaveFormTCPIPUIValues()
        {
            _tcpIp = new TcpIPDTO();
            _tcpIp.ConnectionName = this.txtConnectionName.Text;
            _tcpIp.ConnectionType = (int)((TCPIPConnectionType)Enum.Parse(typeof(TCPIPConnectionType), this.comboConnectionType.SelectedValue.ToString()));
            _tcpIp.CreatedDate = System.DateTime.Now;
            _tcpIp.IPAddress = this.txtIPAddress.Text;
            _tcpIp.PortNumber = Convert.ToInt32(this.txtPortNumber.Text);
            _tcpIp.OK = this.txtOKTcpconfig.Text;
            _tcpIp.NotOK = this.TxtNotOkTcpConfig.Text;


        }

        private bool CheckIPValid()
        {
            bool isValid = true;

            string[] IpNumber = this.txtIPAddress.Text.Split('.');

            if (IpNumber.Length == 4)
            {
                foreach (var number in IpNumber)
                {
                    if (number != "" && Convert.ToInt32(number) <= 255 && Convert.ToInt32(number) >= 0)
                    {
                        isValid = true;
                    }

                    else
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        private void txtIPAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void comboConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedConnectionType = (int)comboConnectionType.SelectedIndex + 1;
                PopulateTCPIPDetailsByTypeID(selectedConnectionType);





            }
            catch (Exception)
            {

                throw;
            }

        }

        private void PopulateTCPIPDetailsByTypeID(int selectedConnectionType)
        {
            var _tcpIp1 = this.dbAccess.GetTcpIPPortData(selectedConnectionType);

            if (_tcpIp1 != null)
            {
                this.txtConnectionName.Text = _tcpIp1.ConnectionName;
                this.txtIPAddress.Text = _tcpIp1.IPAddress;
                this.txtPortNumber.Text = _tcpIp1.PortNumber == null ? "" : Convert.ToString(_tcpIp1.PortNumber);
                this.TxtNotOkTcpConfig.Text = _tcpIp1.NotOK;
                this.txtOKTcpconfig.Text = _tcpIp1.OK;

            }
            else
            {

                this.txtConnectionName.Text = "";
                this.txtIPAddress.Text = "";
                this.txtPortNumber.Text = "";
                this.txtOKTcpconfig.Text = "";
                this.TxtNotOkTcpConfig.Text = "";
            }

        }
















    }
}
