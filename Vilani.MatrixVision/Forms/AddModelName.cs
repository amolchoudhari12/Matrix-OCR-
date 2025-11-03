using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vilani.MatrixVision.DataBase;
using Expert.Common.Library;

namespace Vilani.MatrixVision.Forms
{
    public partial class AddModelName : Form
    {
        DataBaseAccess dbAccess = new DataBaseAccess();
        public int _errorCode { get; set; }

        public delegate void NewModelCreateHandler(int modelID, string modelName);
        public event NewModelCreateHandler NewModelCreateHandlerEvent;


        public AddModelName()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string modelName = txtModelName.Text.ToUpper();
            string plxInputValue = txtPLCInputValue.Text.ToUpper();
            if (modelName.Trim().Length > 0)
            {
                if (this.dbAccess.IsModelExists(modelName))
                {
                    MessageBox.Show("This Model name already exists in the system.\r\nPlease enter a different model name.", "Duplicate Model Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    int modelID = this.dbAccess.AddNewModelDetails(new Model() { ModelName = modelName, PLCInputValue = plxInputValue });
                    if (modelID > 0)
                    {

                       // MessageBox.Show("New Model name has been updated in the system.\r\nPlease select from the Model List to start configuring the details.", "New Model Created.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        NewModelCreateHandlerEvent(modelID, modelName);
                    }
                    else
                    {
                        MessageBox.Show("Unable to create new model.\r\nPlease restart the application and try again.", "Unable to create model", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    this.Close();
                }
            }

        }

    }
}
