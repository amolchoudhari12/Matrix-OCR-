using Expert.Common.Library.DTOs;
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
    public partial class ReportsHistory : Form
    {

        public string InspectionName { get; set; }

        public List<ReportDTO> reportsSource = new List<ReportDTO>();

        List<ReportMenuDTO> data = null;


        public ReportsHistory()
        {
            InitializeComponent();
            LoadReportMenus();

        }

        private void LoadReportMenus()
        {
            List<ReportMenuDTO> data = GetReportMenus();
            gridViewReports.DataSource = data;


        }

        private List<ReportMenuDTO> GetReportMenus()
        {
            data = new List<ReportMenuDTO>();

            string[] fileEntries = Directory.GetFiles(Path.Combine(VilaniFileDataSource.GetDataSourceDirectory(), "Reports"));
            string inspectionName = string.Empty;


            foreach (var item in fileEntries)
            {
                FileInfo fi = new FileInfo("path");
                var created = fi.LastWriteTime;

                var fileName = Path.GetFileName(item);
                string[] fileAttr = fileName.Split('_');
                DateTime? creationDate = null;

                try
                {
                    if (fileAttr.Count() == 3)
                    {
                        inspectionName = fileAttr[0];

                        creationDate = Convert.ToDateTime(fileAttr[2].Split('.')[0]);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }

                creationDate = creationDate == null ? created : creationDate;

                data.Add(new ReportMenuDTO()
                {
                    ProductName = inspectionName,
                    ReportName = Path.GetFileName(item),
                    ReportDate = creationDate.Value,
                    Action = "Open Report"
                });
            }
            return data;
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var dataSource = GetReportMenus().Where(x => x.ReportDate.Date == dateTimePicker1.Value.Date).ToList();
            gridViewReports.DataSource = dataSource;
        }
        Reports reportForm = new Reports();



        private void gridViewReports_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var _selectedProductName = Convert.ToString(gridViewReports.Rows[e.RowIndex].Cells[0].Value);
                var _selectedReportName = Convert.ToString(gridViewReports.Rows[e.RowIndex].Cells[1].Value);
                var _selectedReportDate = Convert.ToString(gridViewReports.Rows[e.RowIndex].Cells[2].Value);

                var storageProcessor = new GenericStorageProcessor<VilaniFileDataSource, ReportVM>(_selectedReportName);
                var data = storageProcessor.GetAll();
                ReportDTO report = new ReportDTO();
                reportForm.SetReportData(ConvertToDTOs(data));
                reportForm.ProductName = _selectedProductName;
                reportForm.ReportName = _selectedReportName.Split('.')[0];
                reportForm.ShowReport();

            }
        }

        private List<ReportDTO> ConvertToDTOs(List<ReportVM> reportsData)
        {
            List<ReportDTO> data = new List<ReportDTO>();

            foreach (var item in reportsData)
            {
                ReportDTO item1 = new ReportDTO();
                item1.SrNo = item.SrNo;
                item1.PartNo = item.PartNo;
                item1.Score = item.Score;
                item1.ActualScore = item.ActualScore;
                item1.Status = item.Status;
                item1.IsChecked = item.IsChecked;
                data.Add(item1);
            }

            return data;
        }

        private void gridViewReports_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            gridViewReports.Rows[e.RowIndex].Cells[3].Style.BackColor = Color.FromKnownColor(KnownColor.Control);

        }

        private void btnOpenReport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var fileName = Path.GetFileName(dialog.FileName);
                var folderPath = dialog.FileName.Replace(fileName, "");
                fileName = fileName + ".dat";

                var storageProcessor = new GenericStorageProcessor<VilaniFileDataSource, ReportVM>(folderPath, fileName);
                var data = storageProcessor.GetAll();
                ReportDTO report = new ReportDTO();
                reportForm.SetReportData(ConvertToDTOs(data));
                reportForm.ShowReport();
            }
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            gridViewReports.DataSource = GetReportMenus().Where(x => x.ProductName.ToLower() == txtProduct.Text.ToLower()).ToList();
        }

        private void txtClear_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, 01, 01);
           txtProduct.Text = string.Empty;
        }
    }

}



