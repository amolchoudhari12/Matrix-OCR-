using Expert.Common.Library.DTOs;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vilani.MatrixVision.ViewModels;


namespace Vilani.MatrixVision.Forms
{
    public partial class ReportRDLC : Form
    {
        public ReportRDLC()
        {
            InitializeComponent();
        }

        public List<ReportDTO> ReportData { get; set; }


        public string ProductName { get; set; }

        public string ReportName { get; set; }

        public string ReportDate { get; set; }

        private void ReportRDLC_Load(object sender, EventArgs e)
        {
            reportViewer1.Reset();
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource reportDSDetail = new ReportDataSource("VisionReportDataSet", GetData());
            ReportDataSource reportDSDetail2 = new ReportDataSource("ProductNameDateDatSet", GetProductData());
            reportViewer1.LocalReport.ReportPath = "VisionReport.rdlc";
            reportViewer1.LocalReport.DataSources.Add(reportDSDetail);
            reportViewer1.LocalReport.DataSources.Add(reportDSDetail2);
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.RefreshReport();
            reportViewer1.Visible = true;
            this.reportViewer1.RefreshReport();
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("SrNo");
            dt.Columns.Add("PartNo");
            dt.Columns.Add("ActualScore");
            dt.Columns.Add("Score");
            dt.Columns.Add("Status");
            dt.Columns.Add("IsChecked");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ReportDate");
            dt.Columns.Add("ReportName");

            foreach (var item in this.ReportData)
            {
                DataRow row = dt.NewRow();
                row["SrNo"] = item.SrNo;
                row["PartNo"] = item.PartNo;
                row["ActualScore"] = item.ActualScore;
                row["Score"] = item.Score;
                row["Status"] = item.Status;
                row["IsChecked"] = item.IsChecked == true ? "Checked" : "Not Checked";
                row["ProductName"] = ProductName;
                row["ReportName"] = ReportName;
                row["ReportDate"] = ReportDate;
                dt.Rows.Add(row);
            }

            return dt;
        }

        private DataTable GetProductData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ReportDate");
            dt.Columns.Add("ReportName");


            DataRow row = dt.NewRow();
            row["ProductName"] = ProductName;
            row["ReportName"] = ReportName;
            row["ReportDate"] = ReportDate;
            dt.Rows.Add(row);


            return dt;
        }
    }
}
