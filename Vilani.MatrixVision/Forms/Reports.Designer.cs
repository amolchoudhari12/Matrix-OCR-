namespace Vilani.MatrixVision.Forms
{
    partial class Reports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reports));
            this.gridViewReports = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReportName = new System.Windows.Forms.TextBox();
            this.txtInspectionName = new System.Windows.Forms.TextBox();
            this.chkLocation = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReports)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewReports
            // 
            this.gridViewReports.AllowUserToDeleteRows = false;
            this.gridViewReports.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridViewReports.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridViewReports.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.gridViewReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewReports.Location = new System.Drawing.Point(12, 61);
            this.gridViewReports.Name = "gridViewReports";
            this.gridViewReports.Size = new System.Drawing.Size(652, 525);
            this.gridViewReports.TabIndex = 1;
            this.gridViewReports.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridViewReports_CellBeginEdit);
            this.gridViewReports.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridViewReports_CellFormatting);
            this.gridViewReports.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewReports_CellValueChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(576, 601);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 28);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Product Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(479, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Date Time:";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(544, 12);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(59, 13);
            this.lblDateTime.TabIndex = 7;
            this.lblDateTime.Text = "Date Time:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(482, 601);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 28);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Report Name:";
            // 
            // txtReportName
            // 
            this.txtReportName.Location = new System.Drawing.Point(108, 35);
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Size = new System.Drawing.Size(254, 20);
            this.txtReportName.TabIndex = 10;
            // 
            // txtInspectionName
            // 
            this.txtInspectionName.Location = new System.Drawing.Point(108, 9);
            this.txtInspectionName.Name = "txtInspectionName";
            this.txtInspectionName.Size = new System.Drawing.Size(254, 20);
            this.txtInspectionName.TabIndex = 11;
            // 
            // chkLocation
            // 
            this.chkLocation.AutoSize = true;
            this.chkLocation.Location = new System.Drawing.Point(482, 36);
            this.chkLocation.Name = "chkLocation";
            this.chkLocation.Size = new System.Drawing.Size(133, 17);
            this.chkLocation.TabIndex = 12;
            this.chkLocation.Text = "Specify Save Location";
            this.chkLocation.UseVisualStyleBackColor = true;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 646);
            this.Controls.Add(this.chkLocation);
            this.Controls.Add(this.txtInspectionName);
            this.Controls.Add(this.txtReportName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.gridViewReports);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Reports_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridViewReports;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReportName;
        private System.Windows.Forms.TextBox txtInspectionName;
        private System.Windows.Forms.CheckBox chkLocation;

    }
}