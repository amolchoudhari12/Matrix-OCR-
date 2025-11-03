using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Vilani.Licenses;
using Vilani.MatrixVision.Common;
using Vilani.MatrixVision.Core;

namespace Vilani.MatrixVision
{
    public partial class Validation : Form
    {

        public Validation()
        {
            InitializeComponent();
            bool internetStatus = VilaniHelpers.CheckInternet();
            if (internetStatus)
            {
                btnValidate.Visible = true;
                lblStatus.Visible = false;
            }
            else
            {
                btnValidate.Visible = false;
                lblStatus.Text = string.Format("For Activating the license for this PC, you need to connect to 'Internet'\n" +
                    "Please connect to internet for activation. Or contact Support.");
                lblStatus.Visible = true;
            }

        }

        internal bool ValidateLicense()
        {
            return true;
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            bool isValid = false;
            try
            {
                int id = 0;
                int activationNumber = 0;
                LicenseValidator.LicenseForApplication = LicenseFor.VilaniVisionInspectionSystem;
                LicenseValidator.ValidateLicenseKey(txtLicenseKey.Text, txtActivationKey.Text, ref id, ref activationNumber);

                if (id > 0)
                {

                    if (LicenseValidator.ValidateLicenseFromServer(id, activationNumber))
                    {
                        isValid = true;
                        SerialKeyGenrator thisObject = new SerialKeyGenrator();
                        var serialKey = thisObject.GetSerialKey();
                        File.WriteAllText(Path.Combine(Application.StartupPath, "serialkey"), serialKey);
                        this.Hide();
                        LicenseValidator.UpdateLicenseConsumed(id, activationNumber, serialKey);

                        MainContainer form = new MainContainer();
                        form.Show();

                    }
                }

                if (isValid == false)
                    MessageBox.Show("The provided License in not valid Or used multiple times. \nPlease enter valid license or contact Service Provider.", "Invalid License", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("The provided License in not valid Or used multiple times. \nPlease enter valid license or contact Service Provider.", "Invalid License", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

 
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtLicenseKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtActivationKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }





    }
}
