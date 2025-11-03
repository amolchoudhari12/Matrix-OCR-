using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vilani.MatrixVision.UserControls
{
    public partial class ReferenceImgResult : UserControl
    {
        public Image CurrentReferenceImage
        {
            get
            {
                return this.pictureBoxRefrenceImage.Image;
            }
            set
            {
                this.pictureBoxRefrenceImage.Image = value;
            }
        }

        private string _imageName;
        public string ImageName
        {
            get
            {
                return _imageName;
            }
            set
            {
                _imageName = value;
                lblImageName.Text = _imageName;
                ToolTip yourToolTip = new ToolTip();

                yourToolTip.ToolTipIcon = ToolTipIcon.None;
                yourToolTip.IsBalloon = true;
                yourToolTip.ShowAlways = true;
                yourToolTip.SetToolTip(lblImageName, _imageName);

            }
        }

        private string _result;
        public string ActualResult
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                lblResult.Text = _result;
            }
        }

        public ReferenceImgResult()
        {
            InitializeComponent();
        }

    }
}
