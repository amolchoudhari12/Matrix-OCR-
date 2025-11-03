using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Vilani.MatrixVision.Core;
using Vilani.MatrixVision.Common;

namespace Vilani.MatrixVision.UserControls
{
    public partial class ReferenceImage : UserControl, IDisposable
    {
        public ReferenceImage()
        {
            InitializeComponent();
        }

        public int ReferenceImageID { get; set; }
        public string ImageLocation { get; set; }

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
                lblRefImageName.Text = _imageName;
                ToolTip yourToolTip = new ToolTip();

                yourToolTip.ToolTipIcon = ToolTipIcon.None;
                yourToolTip.IsBalloon = true;
                yourToolTip.ShowAlways = true;

                yourToolTip.SetToolTip(lblRefImageName, _imageName);

            }
        }

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

        public bool IsImageSelected { get; set; }

        public int NumOfOccurances { get; set; }

        private int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                this.numericUpDownScore.Value = Convert.ToInt32(_score);
            }
        }

        private Color _color;
        public Color CurrentColor
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                this.pictureBoxRefrenceImage.BackColor = value;
                this.chkSelectImage.BackColor = value;
                this.chkSelectImage.Enabled = false;
                this.lblRefImageName.ForeColor = value;
            }
        }

        public bool NumOfOccurancesControlEnabled { get; set; }



        private void chkSelectImage_CheckedChanged(object sender, EventArgs e)
        {
            IsImageSelected = chkSelectImage.Checked;
        }



        private void numericUpDownScore_ValueChanged(object sender, EventArgs e)
        {
            Score = Convert.ToInt32(numericUpDownScore.Value);
        }

        public void SetNumericControlReadonly()
        {
            numericUpDownScore.ReadOnly = true;
        }

        public void SetCheckBoxVisible(bool flag)
        {
            chkSelectImage.Visible = flag;
        }

        private void ReferenceImage_Load(object sender, EventArgs e)
        {
            if (NumOfOccurancesControlEnabled)
            {
                numericUpDownScore.ReadOnly = true;
                chkSelectImage.Visible = false;
            }
        }


        public void SetResult(string result, string type)
        {
            if (type == GlobalSettings.OKSendText)
                this.lblResult.ForeColor = Color.Green;
            else if (type == GlobalSettings.NotOKSendText)
                this.lblResult.ForeColor = Color.Red;
            else
                this.lblResult.ForeColor = Color.Black;

            lblResult.Text = result;
            lblResult.Visible = true;
        }


        internal void SetSize(int p1, int p2)
        {
            this.Height = p1;
            this.Width = p2;
        }
    }
}
