using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vilani.Licenses;
using Vilani.MatrixVision.Core;
using Vilani.MatrixVision.DataBase;

namespace Vilani.MatrixVision.Forms
{
    public partial class Password : Form
    {
        private DataBaseAccess _dbAccess = new DataBaseAccess();
        private string _validPassword = "";

        public delegate void ValidationHandler(bool result);
        public event ValidationHandler ValidationEvent;


        public Password()
        {
            InitializeComponent();
            var actualPassword = StartupSettings.LoadSettings().Where(x => x.SettingName == VisionConstants.PASSWORD).FirstOrDefault();
            if (actualPassword != null)
                _validPassword = LicenseValidator.Decrypt(actualPassword.SettingValue.ToString(), VisionConstants.PASSWORD);
        }


        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (_validPassword == txtPassword.Text)
            {
                ValidationEvent(true);
                this.Close();
            }
            else
            {
                lblError.Text = "Wrong Password. Please try again";
                lblError.Visible = true;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ValidationEvent(false);
            this.Close();
        }

       

    }
}
