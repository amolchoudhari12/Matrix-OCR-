using Expert.Common.Library.DTOs;
using mvIMPACT_NET;
using mvIMPACT_NET.blob;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vilani.MatrixVision.Common;
using Vilani.MatrixVision.Core;
using Vilani.MatrixVision.DataBase;

namespace Vilani.MatrixVision.Forms
{
    public partial class TeachImage : Form
    {
        DataBaseAccess dbAccess = new DataBaseAccess();
        private string _sOrgimgPath;
        private string _sOrgimgPathTEMP;
        private System.Drawing.Image _CurrentLoadedImage = null;

        private bool _bIsToMarkROI = false;
        private bool bHaveMouse;
        private System.Drawing.Point ptOriginal = default(System.Drawing.Point);

        private System.Drawing.Point ptLast = default(System.Drawing.Point);

        private System.Drawing.Rectangle rectCropArea;
        List<TeachImageConfigDTO> techImageConfigDTOList = new List<TeachImageConfigDTO>();
        TeachImageConfigDTO _teachImageConfigDTO = null;

        public TeachImage()
        {
            InitializeComponent();
        }



        public TeachImage(System.Drawing.Image currentReferenceImage)
        {
            InitializeComponent();
            _CurrentLoadedImage = this.pictureBoxOriginalImage.Image = currentReferenceImage;
            this.pictureBoxOriginalImage.Update();
            try
            {

            }
            catch (Exception)
            {

                throw;
            }


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
                _CurrentLoadedImage = System.Drawing.Image.FromFile(this._sOrgimgPath);
                this.pictureBoxOriginalImage.Image = _CurrentLoadedImage;
                this.pictureBoxImageForCounts.Image = _CurrentLoadedImage;
            }
        }

        private void numericUpDownForCounts_ValueChanged(object sender, EventArgs e)
        {
            CalculateNumberOfBlobs(1);
        }

        List<MyAreaOfInterest> validRectangles = new List<MyAreaOfInterest>();

        private void CalculateNumberOfBlobs(int order)
        {
            validRectangles.Clear();

            Cursor.Current = Cursors.WaitCursor;
            var factor = numericUpDownCropPercent.Value == 0 ? 1 : (numericUpDownCropPercent.Value / 100);
            int valueToCompare = 0;
            int areaToCompare = 0;
            if (string.IsNullOrEmpty(this._sOrgimgPath))
            {
                MessageBox.Show("Invalid Image ");
                return;
            }


            try
            {
                mvIMPACT_NET.Image imgSource = gBase.loadImage(this._sOrgimgPath);
                mvIMPACT_NET.Image imgSourceCrop = null;

                if (order == 1)
                {
                    var y = imgSource.height - imgSource.height * factor;
                    var height = imgSource.height - y;
                    imgSourceCrop = new mvIMPACT_NET.Image(imgSource, 0, (int)y, imgSource.width, (int)height);
                    valueToCompare = (int)numericUpDownImageFactor1.Value;
                    areaToCompare = Convert.ToInt32(numericUpDownAreaGreaterThan1.Value);


                }
                else
                {
                    imgSourceCrop = new mvIMPACT_NET.Image(imgSource, 0, 0, imgSource.width, imgSource.height);
                    valueToCompare = (int)numericUpDownImageFactor2.Value;
                    areaToCompare = Convert.ToInt32(numericUpDownAreaGreater2.Value);
                }

                imgSourceCrop = gBase.compareGreater(imgSourceCrop, valueToCompare);

                if (checkBoxFillHoles1.Checked && order == 1)
                {
                    imgSourceCrop = gBase.fillHoles(imgSourceCrop);
                }
                else if (checkBoxFillHoles2.Checked && order == 2)
                {
                    imgSourceCrop = gBase.extractHoles(imgSourceCrop);
                    imgSourceCrop = gBase.fillHoles(imgSourceCrop);
                }


                string pathToSave = Path.Combine(DirectorFileHelper.GetProcessedImageFolderPath, string.Format("{0}.jpg", System.DateTime.Now.Millisecond.ToString()));
                gBase.saveImage(imgSourceCrop, pathToSave);

                BlobFeatureList blobFeatures = new BlobFeatureList();

                //Get Parameters
                int areaID = blobFeatures.addArea();
                int areaConvexID = blobFeatures.addAreaConvex();
                int extremeBoxID = blobFeatures.addExtremeBox();

                if (order == 1)
                {
                    this.pictureBoxImageForCounts.Image = System.Drawing.Image.FromFile(pathToSave);

                }
                else
                {
                    this.pictureBoxBinaryImage.Image = System.Drawing.Image.FromFile(pathToSave);
                }

                BlobResultList blobResults = gBlob.analyze(imgSourceCrop, blobFeatures);
                int iNumBlobs = blobResults.getNumberOfBlobs();

                List<string> data = new List<string>();
                int interestedAreasCount = 0;
                for (int i = 0; i < iNumBlobs; i++)
                {
                    Array area = blobResults.getSingleResult(areaID, i, 0, 0, 0);

                    data.Add(string.Format("Blob {0} Area = {1}\n", i + 1, Convert.ToInt32(area.GetValue(0))));

                    if (Convert.ToInt32(area.GetValue(0)) > areaToCompare)
                    {
                        Array extremeBox = blobResults.getSingleResult(extremeBoxID, i, 0, 0, 0);
                        interestedAreasCount++;
                        var rect = new MyAreaOfInterest()
                        {
                            ID = i,
                            X = Convert.ToInt32(extremeBox.GetValue(0)),
                            Y = Convert.ToInt32(extremeBox.GetValue(1)),
                            Width = Convert.ToInt32(extremeBox.GetValue(2)) - Convert.ToInt32(extremeBox.GetValue(0)),
                            Height = Convert.ToInt32(extremeBox.GetValue(3)) - Convert.ToInt32(extremeBox.GetValue(1))
                        };

                        validRectangles.Add(rect);
                    }
                }


                if (order == 1)
                {
                    lblNumberOfBlobs.Text = string.Format("Total Blobs : {0}", interestedAreasCount);
                    listBoxTotalAreas.DataSource = data;
                }
                else
                {
                    validRectangles = SortPoints(validRectangles);
                    lblTotalBlobCounts.Text = string.Format("Total Blobs as holes : {0}", interestedAreasCount);
                    listBoxTotalAreas2.DataSource = data;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            Cursor.Current = Cursors.Default;
        }

        private void SetMinMaxDistances(List<MyAreaOfInterest> validRectangles)
        {
            throw new NotImplementedException();
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

        private void numericUpDownAreaGreaterThan_ValueChanged(object sender, EventArgs e)
        {
            CalculateNumberOfBlobs(1);
        }

        private void btnAreaOfInterest_Click(object sender, EventArgs e)
        {
            _bIsToMarkROI = true;
        }

        private void numericUpDownImageFactor2_ValueChanged(object sender, EventArgs e)
        {
            CalculateNumberOfBlobs(2);
        }

        private void pictureBoxBinaryImage_Paint(object sender, PaintEventArgs e)
        {

            int coutner = 0;
            foreach (var item in validRectangles)
            {
                item.ID = coutner + 1;
                // var imagePath = Path.Combine(Path.Combine(Application.StartupPath, VisionConstants.CAP_IMG_FOLDER_NAME), item.Key);
                System.Drawing.Image image = _CurrentLoadedImage;
                int width = image.Width;
                int height = image.Height;


                System.Drawing.Rectangle displayRectangle = this.pictureBoxBinaryImage.DisplayRectangle;
                float widthFactor = (float)width / (float)displayRectangle.Width;
                float hightFactor = (float)height / (float)displayRectangle.Height;

                int tarWidth = Convert.ToInt32(item.Width / widthFactor);
                int tarHeight = Convert.ToInt32(item.Height / hightFactor);

                int tarX = Convert.ToInt32(item.X / widthFactor);
                int tarY = Convert.ToInt32(item.Y / hightFactor);



                string text2 = item.ID.ToString();
                using (Font font2 = new Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Point))
                {
                    System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(tarX, tarY, (int)tarWidth, (int)tarHeight);

                    System.Drawing.Rectangle textRect = new System.Drawing.Rectangle(tarX, tarY - 25, (int)tarWidth + 5, (int)tarHeight);


                    // Specify the text is wrapped.
                    TextFormatFlags flags = TextFormatFlags.WordBreak;
                    TextRenderer.DrawText(e.Graphics, text2, font2, textRect, Color.Green, flags);
                    e.Graphics.DrawRectangle(Pens.Green, System.Drawing.Rectangle.Round(srcRect));

                }


                coutner++;
            }


        }

        private void checkBoxFillHoles1_CheckedChanged(object sender, EventArgs e)
        {
            CalculateNumberOfBlobs(1);
        }

        private void numericUpDownCropPercent_ValueChanged(object sender, EventArgs e)
        {
            CalculateNumberOfBlobs(1);
        }

        private void numericUpDownAreaGreater2_ValueChanged(object sender, EventArgs e)
        {
            CalculateNumberOfBlobs(2);
        }

        private void checkBoxFillHoles2_CheckedChanged(object sender, EventArgs e)
        {
            CalculateNumberOfBlobs(2);
        }

        private void btnCalculateMinMax_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValidHoles.Text))
            {
                string[] ids = txtValidHoles.Text.Split(',');
                List<MyAreaOfInterest> validPoints = new List<MyAreaOfInterest>();
                List<int> minDistances = new List<int>();

                foreach (var item in ids)
                {
                    var point = validRectangles.Where(x => x.ID == Convert.ToInt32(item)).FirstOrDefault();

                    if (point != null)
                        validPoints.Add(point);
                }

                for (int i = 0; i < validPoints.Count - 1; i++)
                {
                    var dist = validPoints[i + 1].X - validPoints[i].X;
                    minDistances.Add(dist);

                }

                minDistances.Sort();
                txtMinRange.Text = Convert.ToString(minDistances[0]);
                txtMaxRange.Text = Convert.ToString(minDistances[minDistances.Count - 1]);

            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                this.dbAccess.GetTeachImageConfiguration();
                saveTeachImageConfigUIValues();
                _teachImageConfigDTO = new TeachImageConfigDTO();
                _teachImageConfigDTO.iErrCode = this.dbAccess.UpdateTeachImageConfiguration(techImageConfigDTOList);
                
                ShowFeedbackMessage();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        private void ShowFeedbackMessage()
        {
            if (_teachImageConfigDTO.iErrCode == 1)
            {
                MessageBox.Show("Congratulations! Configuration saved Successfully!\r\n", "Configuration Save Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Error while updating the configuration.\r\nPlease verify all the necessary fields are present & restart the application.", "Error while Update", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void saveTeachImageConfigUIValues()
        {

            var techImaheDTO1 = new TeachImageConfigDTO();
            techImaheDTO1.ImageFactor = Convert.ToInt16(this.numericUpDownImageFactor1.Value);
            techImaheDTO1.AreaGreaterThan = Convert.ToInt16(this.numericUpDownAreaGreaterThan1.Value);
            techImaheDTO1.CropArea = this.comboBoxCropType.Text;
            techImaheDTO1.CropAreaPercent = Convert.ToInt16(this.numericUpDownCropPercent.Value);
            techImaheDTO1.MinRange = 0;
            techImaheDTO1.MaxRange = 0;
            techImaheDTO1.FillHoles = checkBoxFillHoles1.Checked;


            var techImaheDTO2 = new TeachImageConfigDTO();
            techImaheDTO2.ImageFactor = Convert.ToInt16(this.numericUpDownImageFactor2.Value);
            techImaheDTO2.AreaGreaterThan = Convert.ToInt16(this.numericUpDownAreaGreater2.Value);
            techImaheDTO2.FillHoles = checkBoxFillHoles2.Checked;
            techImaheDTO2.validHoles = this.txtValidHoles.Text;
            techImaheDTO2.MinRange = Convert.ToInt16(this.txtMinRange.Text);
            techImaheDTO2.MaxRange = Convert.ToInt16(this.txtMaxRange.Text);



            techImageConfigDTOList.Add(techImaheDTO1);
            techImageConfigDTOList.Add(techImaheDTO2);

        }

    }
}
