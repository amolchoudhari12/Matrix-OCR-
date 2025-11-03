namespace Vilani.MatrixVision.Forms
{
    partial class ReportsHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsHistory));
            this.gridViewReports = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblInspectionName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnOpenReport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.txtClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReports)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewReports
            // 
            this.gridViewReports.AllowUserToDeleteRows = false;
            this.gridViewReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridViewReports.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridViewReports.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridViewReports.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.gridViewReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewReports.Location = new System.Drawing.Point(12, 81);
            this.gridViewReports.Name = "gridViewReports";
            this.gridViewReports.Size = new System.Drawing.Size(612, 550);
            this.gridViewReports.TabIndex = 1;
            this.gridViewReports.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewReports_CellEnter);
            this.gridViewReports.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridViewReports_CellFormatting);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(533, 648);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblInspectionName
            // 
            this.lblInspectionName.AutoSize = true;
            this.lblInspectionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspectionName.Location = new System.Drawing.Point(12, 18);
            this.lblInspectionName.Name = "lblInspectionName";
            this.lblInspectionName.Size = new System.Drawing.Size(121, 17);
            this.lblInspectionName.TabIndex = 5;
            this.lblInspectionName.Text = "Reports History";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(354, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(423, 52);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 7;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btnOpenReport
            // 
            this.btnOpenReport.Location = new System.Drawing.Point(412, 18);
            this.btnOpenReport.Name = "btnOpenReport";
            this.btnOpenReport.Size = new System.Drawing.Size(128, 23);
            this.btnOpenReport.TabIndex = 8;
            this.btnOpenReport.Text = "Open Saved Report";
            this.btnOpenReport.UseVisualStyleBackColor = true;
            this.btnOpenReport.Click += new System.EventHandler(this.btnOpenReport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Product Name";
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(97, 55);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(210, 20);
            this.txtProduct.TabIndex = 10;
            this.txtProduct.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            // 
            // txtClear
            // 
            this.txtClear.Location = new System.Drawing.Point(546, 18);
            this.txtClear.Name = "txtClear";
            this.txtClear.Size = new System.Drawing.Size(75, 23);
            this.txtClear.TabIndex = 11;
            this.txtClear.Text = "Clear Filter";
            this.txtClear.UseVisualStyleBackColor = true;
            this.txtClear.Click += new System.EventHandler(this.txtClear_Click);
            // 
            // ReportsHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 688);
            this.Controls.Add(this.txtClear);
            this.Controls.Add(this.txtProduct);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpenReport);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblInspectionName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gridViewReports);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportsHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports";
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridViewReports;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblInspectionName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnOpenReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Button txtClear;

    }
}