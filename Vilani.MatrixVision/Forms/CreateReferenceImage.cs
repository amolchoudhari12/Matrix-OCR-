
using mvIMPACT_NET;
using mvIMPACT_NET.match;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vilani.MatrixVision.Common;
using Vilani.MatrixVision.Core;
using Vilani.MatrixVision.DataBase;
using Expert.Common.Library;
using System.Configuration;
using Expert.Common.Library.Enumrations;


namespace Vilani.MatrixVision
{


    public partial class CreateReferenceImage : Form
    {

        DataBaseAccess dbAccess = new DataBaseAccess();

        public delegate void ReferenceImageAssignedHandler();
        public event ReferenceImageAssignedHandler ReferenceImageAssignedEvent;


        private bool bHaveMouse;
        private System.Drawing.Point ptOriginal = default(System.Drawing.Point);

        private System.Drawing.Point ptLast = default(System.Drawing.Point);

        private System.Drawing.Rectangle rectCropArea;

        private string _sOrgimgPath = "";

        private Bitmap target = null;
        private bool _bIsToMarkROI = false;

        public int _ModelID { get; set; }
        public string _ModelName { get; set; }

        public CreateReferenceImage()
        {
            InitializeComponent();

        }

        private System.Drawing.Image _CurrentLoadedImage = null;


        public CreateReferenceImage(System.Drawing.Image currentReferenceImage)
        {
            InitializeComponent();
            _CurrentLoadedImage = this.pictureBoxLive.Image = currentReferenceImage;
            this.pictureBoxLive.Update();
            this.bHaveMouse = false;
            txtImageName.Text = string.Format("File Info:\nThis is live Image directly taken from camera");
            btnSaveReferenceImage.Text = "Save Reference Image";          
        }



        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                _sOrgimgPath = open.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromFile(this._sOrgimgPath);
                //   System.Drawing.Image image2 = this.resizeImage(image, new System.Drawing.Size(image.Width / 2, image.Height / 2));
                _CurrentLoadedImage = this.pictureBoxLive.Image = image;
                txtImageName.Text = string.Format("File Info:\n The Image {0} is uploaded from disk", open.SafeFileName);
            }
        }

        private System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, System.Drawing.Size size)
        {
            int width = imgToResize.Width;
            int height = imgToResize.Height;
            float num = (float)size.Width / (float)width;
            float num2 = (float)size.Height / (float)height;
            float num3;
            if (num2 < num)
            {
                num3 = num2;
            }
            else
            {
                num3 = num;
            }
            int width2 = (int)((float)width * num3);
            int height2 = (int)((float)height * num3);
            Bitmap bitmap = new Bitmap(width2, height2);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(imgToResize, 0, 0, width2, height2);
            graphics.Dispose();
            return bitmap;
        }


        private void pictureBoxLive_MouseDown(object sender, MouseEventArgs e)
        {
            this.bHaveMouse = true;
            this.ptOriginal.X = e.X;
            this.ptOriginal.Y = e.Y;
            this.ptLast.X = -1;
            this.ptLast.Y = -1;
            this.rectCropArea = new System.Drawing.Rectangle(new System.Drawing.Point(e.X, e.Y), default(System.Drawing.Size));

        }



        private void pictureBoxLive_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point point = new System.Drawing.Point(e.X, e.Y);
            if (this.bHaveMouse)
            {
                if (this.ptLast.X != -1)
                {
                }
                // this.ptLast = point;
                if (e.X > this.ptOriginal.X && e.Y > this.ptOriginal.Y)
                {
                    this.rectCropArea.Width = e.X - this.ptOriginal.X;
                    this.rectCropArea.Height = e.Y - this.ptOriginal.Y;
                }
                else if (e.X < this.ptOriginal.X && e.Y > this.ptOriginal.Y)
                {
                    this.rectCropArea.Width = this.ptOriginal.X - e.X;
                    this.rectCropArea.X = e.X;
                    this.rectCropArea.Y = this.ptOriginal.Y;
                }
                else if (e.X > this.ptOriginal.X && e.Y < this.ptOriginal.Y)
                {
                    this.rectCropArea.Width = e.X - this.ptOriginal.X;
                    this.rectCropArea.Height = this.ptOriginal.Y - e.Y;
                    this.rectCropArea.X = this.ptOriginal.X;
                    this.rectCropArea.Y = e.Y;
                }
                else
                {
                    this.rectCropArea.Width = this.ptOriginal.X - e.X;
                    this.rectCropArea.Height = this.ptOriginal.Y - e.Y;
                    this.rectCropArea.X = e.X;
                    this.rectCropArea.Y = e.Y;
                }
                this.pictureBoxLive.Refresh();
            }

        }

        private System.Drawing.Rectangle rectAOI;

        private void pictureBoxLive_MouseUp(object sender, MouseEventArgs e)
        {
            this.bHaveMouse = false;

            decimal factor = Convert.ToDecimal(_CurrentLoadedImage.Width) / Convert.ToDecimal(this.pictureBoxLive.Width);

            if (this.ptLast.X != -1)
            {
                System.Drawing.Point point = new System.Drawing.Point(e.X, e.Y);
            }
            if (this._bIsToMarkROI)
            {
                this.txtAreaOfInterest.Text = string.Format("{0},{1},{2},{3}", new object[]
				{
					Convert.ToInt32(this.rectCropArea.X * factor),
					Convert.ToInt32(this.rectCropArea.Y * factor),
					Convert.ToInt32(this.rectCropArea.Width * factor),
					Convert.ToInt32(this.rectCropArea.Height * factor)
				});
                rectAOI.X = Convert.ToInt32(this.rectCropArea.X * factor);
                rectAOI.Y = Convert.ToInt32(this.rectCropArea.Y * factor);
                rectAOI.Width = Convert.ToInt32(this.rectCropArea.Width * factor);
                rectAOI.Height = Convert.ToInt32(this.rectCropArea.Height * factor);
                this._bIsToMarkROI = false;
            }
            this.ptLast.X = -1;
            this.ptLast.Y = -1;
            this.ptOriginal.X = -1;
            this.ptOriginal.Y = -1;




        }

        private void pictureBoxLive_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.LightGreen);
            pen.DashStyle = DashStyle.Solid;
            e.Graphics.DrawRectangle(pen, this.rectCropArea);

        }



        private static double GetScore(Array matches3)
        {
            double score = 0;
            int counter = 0;

            foreach (var item in matches3)
            {
                score += ((MatchData)item).score;
                counter++;
            }
            if (counter > 0)
                score = score / counter;
            return score;
        }

        private static Array ProcessImage(int model1Width, int model1Height, mvIMPACT_NET.Image referenceImage, PatModel model1)
        {
            //search for model
            PatMatchResult matchResult = gMatch.findPatModel(model1, referenceImage);
            Array matches1 = matchResult.getMatches(0, 0, 0);
            return matches1;
        }

        private void btnSaveReferenceImage_Click(object sender, EventArgs e)
        {
            var counter = Convert.ToInt32(ConfigurationManager.AppSettings["SupportedReferenceImages"]);

            if (string.IsNullOrEmpty(this.txtAreaOfInterest.Text))
            {
                MessageBox.Show("Please select Area Of Interest", "Invalid AOI", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (string.IsNullOrEmpty(this.txtReferenceImageName.Text))
            {
                MessageBox.Show("Please enter valid Image Name", "Invalid Image Name", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (this.txtReferenceImageName.Text.Contains("-"))
            {
                MessageBox.Show("Hypen(-) character is not allowed in the Reference FileName.\nPlease use underscore if required.", "Invalid Character", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (this.numericUpDownSetScore.Value == 0)
            {
                MessageBox.Show("Please enter valid Score Value", "Invalid Score", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                try
                {
                    string imageName = this.txtReferenceImageName.Text + string.Format("_{0}.jpg", this.txtAreaOfInterest.Text.Replace(',', '.'));
                    DirectorFileHelper.SaveReferenceImage(this.target, imageName);

                    if (_ModelID > 0)
                    {
                        ReferenceImageDB image = new ReferenceImageDB();
                        image.ModelID = _ModelID;
                        image.Score = Convert.ToInt32(numericUpDownSetScore.Value);
                        image.NumberOfOccurances = 1;
                        image.ImageName = imageName;
                        image.ImageSize = txtImageSize.Text;
                        image.AOISize = txtAreaOfInterest.Text;
                        image.ImagePath = DirectorFileHelper.GetReferenceImageFilePath(imageName);
                        var errorID = this.dbAccess.AddModelReferenceImage(image);
                        //  MessageBox.Show(string.Format("Reference Image {0} is assigned to the Model '{1}' Successfully.", imageName, _ModelName), "Reference Image Assigned", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ReferenceImageAssignedEvent();
                        this.Close();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while saving the image. Details are as below:\r\n" + ex.Message, "Unable to save Reference Image.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                MessageBox.Show("Reference Image Updated into the system.\r\nYou can configure this reference image in Configuration Window of the application.", "Reference Image Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.pictureBoxReference.Image = null;
                this.txtReferenceImageName.Text = "";
                this.txtAreaOfInterest.Text = "";
            }
        }

        public void SetSaveButtonText(string btnText)
        {
            btnSaveReferenceImage.Text = btnText;
            lblSetScore.Enabled = true;
            numericUpDownSetScore.Enabled = true;

        }



        private void btnCreateImage_Click(object sender, EventArgs e)
        {
            this.pictureBoxReference.Refresh();
            System.Drawing.Image image = null;

            if (string.IsNullOrEmpty(_sOrgimgPath))
                image = _CurrentLoadedImage;
            else
                image = System.Drawing.Image.FromFile(this._sOrgimgPath);
            int width = image.Width;
            int height = image.Height;
            System.Drawing.Rectangle rectangle = this.rectCropArea;
            System.Drawing.Rectangle displayRectangle = this.pictureBoxLive.DisplayRectangle;
            float num = (float)rectangle.Width / (float)displayRectangle.Width;
            float num2 = (float)rectangle.Height / (float)displayRectangle.Height;
            float num3 = num * (float)width;
            float num4 = num2 * (float)height;
            int num5 = width / displayRectangle.Width;
            num5 *= rectangle.X;

            if (num5 <= 1) num5 = rectangle.X;

            int num6 = height / displayRectangle.Height;
            num6 *= rectangle.Y;

            if (num6 <= 1) num6 = rectangle.Y;

            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(num5, num6, (int)num3, (int)num4);
            decimal factor = Convert.ToDecimal(_CurrentLoadedImage.Width) / Convert.ToDecimal(this.pictureBoxLive.Width);


            if (srcRect.Width > 0 && srcRect.Height > 0)
            {
                this.target = new Bitmap(srcRect.Width, srcRect.Height);
                using (Graphics graphics = Graphics.FromImage(this.target))
                {
                    graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, this.target.Width, this.target.Height), srcRect, GraphicsUnit.Pixel);

                    this.txtImageSize.Text = string.Format("{0},{1},{2},{3}", new object[]
				{
					0,
					0,
					image.Width,
				    image.Height
				});
                }
                this.pictureBoxReference.Image = this.target;

            }


        }

        private void btnSelectScanAOI_Click(object sender, EventArgs e)
        {
            this._bIsToMarkROI = true;
        }



        private void txtReferenceImageName_TextChanged(object sender, EventArgs e)
        {
            if (txtReferenceImageName.Text.Length > 0) SetSaveButtonText(true); else SetSaveButtonText(false);
        }

        private void SetSaveButtonText(bool flag)
        {
            btnSaveReferenceImage.Enabled = flag;
        }

        private void numericUpDownSetScore_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownSetScore.Value > 0) SetSaveButtonText(true); else SetSaveButtonText(false);
        }

        private void txtAreaOfInterest_TextChanged(object sender, EventArgs e)
        {
            if (txtAreaOfInterest.Text.Length > 0) SetSaveButtonText(true); else SetSaveButtonText(false);
        }

      

    }
}
