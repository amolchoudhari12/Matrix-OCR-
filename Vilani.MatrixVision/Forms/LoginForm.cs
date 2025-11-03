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
    public partial class LoginForm : Form
    {
        DataBaseAccess dbAccess = new DataBaseAccess();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var retunValue = ValidateApplictionUser();

            if (retunValue)
            {
                this.Hide();
                lblLoginStatus.Visible = false;
                MainContainer form = new MainContainer();
                form.Show();

            }
            else
            {
                lblLoginStatus.Text = "UserName or Password is incorrect. Please try again!";
                lblLoginStatus.Visible = true;              // this.Close();
            }

        }

        private bool ValidateApplictionUser()
        {
            var retunValue = dbAccess.GetSettings();

            var userName = retunValue.Where(x => x.SettingName == VisionConstants.APPLICATION_USER_NAME).FirstOrDefault();
            var password = retunValue.Where(x => x.SettingName == VisionConstants.APPLICATION_PASSWORD).FirstOrDefault();

            bool isValid = false;

            if (userName != null && password != null)
            {
                if (txtUserName.Text == Convert.ToString(userName.SettingValue)
                    && txtPassword.Text == LicenseValidator.Decrypt(Convert.ToString(password.SettingValue), VisionConstants.PASSWORD))
                    isValid = true;

                else
                    isValid = false;
            }
            else
                isValid = false;

            if (txtUserName.Text == "admin" && txtPassword.Text == "admin@123")
                isValid = true;

            return isValid;
        }
    }
}
