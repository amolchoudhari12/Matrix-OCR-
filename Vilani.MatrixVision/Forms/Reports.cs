using Expert.Common.Library.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vilani.Files.DB.Core;
using Vilani.MatrixVision.Core;
using Vilani.MatrixVision.ViewModels;

namespace Vilani.MatrixVision.Forms
{
    public partial class Reports : Form
    {
        ILog logger = LogManager.GetLogger("RollingFile");

        bool _isReportSaved = true;

        public string ProductName { get; set; }

        public string ReportName { get; set; }

        public List<ReportDTO> reportsSource = new List<ReportDTO>();

        public ReportMenuDTO ReportMenuDTO { get; set; }

        public Reports()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {        
            ReportRDLC form = new ReportRDLC();
            form.ReportData = reportsSource;
            form.ProductName = txtInspectionName.Text;
            form.ReportDate = lblDateTime.Text;
            form.ShowDialog();
        }

        public void SetReportData(List<ReportDTO> reportData)
        {
            this.reportsSource = reportData;
        }


        public void ShowReport()
        {
            gridViewReports.DataSource = reportsSource;
            txtInspectionName.Text = ProductName;
            txtReportName.Text = ReportName;
            lblDateTime.Text = string.Format("{0} at {1}", System.DateTime.Now.ToShortDateString(), System.DateTime.Now.ToShortTimeString());
           
            try
            {
                this.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
            }
           
        }

      

        private void gridViewReports_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            string status = Convert.ToString(gridViewReports.Rows[e.RowIndex].Cells[4].Value);

            if (status == VisionConstants.OK_TEXT)
                gridViewReports.Rows[e.RowIndex].Cells[4].Style.BackColor = Color.LightGreen;
            else if (status == VisionConstants.NOTOK_TEXT)
                gridViewReports.Rows[e.RowIndex].Cells[4].Style.BackColor = Color.LightPink;

            if (e.ColumnIndex != 5)
                gridViewReports.Columns[e.ColumnIndex].ReadOnly = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReportName.Text) || string.IsNullOrEmpty(txtInspectionName.Text))
            {
                MessageBox.Show("Please enter the Product Name & Report Name");
            }
            else
            {


                try
                {


                    if (chkLocation.Checked)
                    {
                        SaveFileDialog dialog = new SaveFileDialog();
                        DialogResult result = dialog.ShowDialog();

                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            var fileName = Path.GetFileName(dialog.FileName);
                            var folderPath = dialog.FileName.Replace(fileName, "");
                            fileName = fileName + ".dat";
                            SaveReportToLocation(folderPath, Path.GetFileName(fileName));
                        }
                    }
                    else
                    {
                        string reportName = GetReportName();
                        SaveReportToDB(reportName);
                    }



                    MessageBox.Show("Report Saved Successfully");
                }
                catch (IOException ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }

        }

        private string GetReportName()
        {
            var date = DateTime.Now.ToString("dd-MMM-yyyy");
            string reportName = string.Format("{0}_{1}_{2}.dat", txtInspectionName.Text, txtReportName.Text, date);
            return reportName;
        }



        private void SaveReportToDB(string reportName)
        {
            List<ReportDTO> reportsData = gridViewReports.DataSource as List<ReportDTO>;
            List<ReportVM> reportViews = ConvertToReportVM(reportsData);
            var storageProcessor = new GenericStorageProcessor<VilaniFileDataSource, ReportVM>(reportName);
            storageProcessor.RemoveAll();

            foreach (var item in reportViews)
            {
                storageProcessor.Add(item);
            }
            _isReportSaved = true;


        }

        private void SaveReportToLocation(string filePath, string fileName)
        {
            List<ReportDTO> reportsData = gridViewReports.DataSource as List<ReportDTO>;
            List<ReportVM> reportViews = ConvertToReportVM(reportsData);
            var storageProcessor = new GenericStorageProcessor<VilaniFileDataSource, ReportVM>(filePath, fileName);
            storageProcessor.RemoveAll();

            foreach (var item in reportViews)
            {
                storageProcessor.Add(item);
            }
            _isReportSaved = true;

        }

        private List<ReportVM> ConvertToReportVM(List<ReportDTO> reportsData)
        {
            List<ReportVM> data = new List<ReportVM>();

            foreach (var item in reportsData)
            {
                ReportVM item1 = new ReportVM();
                item1.SrNo = item.SrNo;
                item1.PartNo = item.PartNo;
                item1.Score = item.Score;
                item1.ActualScore = item.ActualScore;
                item1.Status = item.Status;
                item1.IsChecked = item.IsChecked;
                item1.IsCheckedString = item.IsChecked == true ? VisionConstants.OK_TEXT : VisionConstants.NOTOK_TEXT;
                data.Add(item1);
            }

            return data;

        }

        private void gridViewReports_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 5)
                return;
        }

        private void Reports_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CheckIfReportsToSave())
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save the Report", "Save Report?", MessageBoxButtons.YesNo);

                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(txtReportName.Text) || string.IsNullOrEmpty(txtInspectionName.Text))
                    {
                        MessageBox.Show("Please enter the Product Name & Report Name");
                        e.Cancel = true;
                    }
                    else
                    {
                        try
                        {
                            string reportName = GetReportName();
                            SaveReportToDB(reportName);
                            MessageBox.Show("Report Saved Successfully");
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                        }

                    }
                }
            }
        }

        private bool CheckIfReportsToSave()
        {
            bool toSave = false;

            if (string.IsNullOrEmpty(txtReportName.Text) || string.IsNullOrEmpty(txtInspectionName.Text))
            {
                toSave = true;
            }

            if (_isReportSaved == false)
                toSave = true;

            return toSave;

        }





        private void gridViewReports_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                bool status = Convert.ToBoolean(gridViewReports.Rows[e.RowIndex].Cells[5].Value);
                if (status)
                    gridViewReports.Rows[e.RowIndex].Cells[4].Value = VisionConstants.OK_TEXT;
                else
                    gridViewReports.Rows[e.RowIndex].Cells[4].Value = VisionConstants.NOTOK_TEXT;

                gridViewReports.Refresh();
                _isReportSaved = false;

                gridViewReports.BeginEdit(true);
            }
        }




    }

}



