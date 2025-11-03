namespace Vilani.MatrixVision.Forms
{
    partial class TeachImage
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
            this.pictureBoxOriginalImage = new System.Windows.Forms.PictureBox();
            this.pictureBoxImageForCounts = new System.Windows.Forms.PictureBox();
            this.pictureBoxBinaryImage = new System.Windows.Forms.PictureBox();
            this.numericUpDownImageFactor1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNumberOfBlobs = new System.Windows.Forms.Label();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxTotalAreas = new System.Windows.Forms.ListBox();
            this.numericUpDownAreaGreaterThan1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCropType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownCropPercent = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAreaGreater2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownImageFactor2 = new System.Windows.Forms.NumericUpDown();
            this.lblTotalBlobCounts = new System.Windows.Forms.Label();
            this.listBoxTotalAreas2 = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxFillHoles1 = new System.Windows.Forms.CheckBox();
            this.checkBoxFillHoles2 = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtValidHoles = new System.Windows.Forms.TextBox();
            this.btnCalculateMinMax = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtMaxRange = new System.Windows.Forms.TextBox();
            this.txtMinRange = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageForCounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBinaryImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageFactor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAreaGreaterThan1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCropPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAreaGreater2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageFactor2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxOriginalImage
            // 
            this.pictureBoxOriginalImage.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxOriginalImage.Name = "pictureBoxOriginalImage";
            this.pictureBoxOriginalImage.Size = new System.Drawing.Size(417, 394);
            this.pictureBoxOriginalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginalImage.TabIndex = 0;
            this.pictureBoxOriginalImage.TabStop = false;
            // 
            // pictureBoxImageForCounts
            // 
            this.pictureBoxImageForCounts.Location = new System.Drawing.Point(621, 12);
            this.pictureBoxImageForCounts.Name = "pictureBoxImageForCounts";
            this.pictureBoxImageForCounts.Size = new System.Drawing.Size(591, 283);
            this.pictureBoxImageForCounts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImageForCounts.TabIndex = 1;
            this.pictureBoxImageForCounts.TabStop = false;
            // 
            // pictureBoxBinaryImage
            // 
            this.pictureBoxBinaryImage.Location = new System.Drawing.Point(621, 312);
            this.pictureBoxBinaryImage.Name = "pictureBoxBinaryImage";
            this.pictureBoxBinaryImage.Size = new System.Drawing.Size(591, 314);
            this.pictureBoxBinaryImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBinaryImage.TabIndex = 2;
            this.pictureBoxBinaryImage.TabStop = false;
            this.pictureBoxBinaryImage.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxBinaryImage_Paint);
            // 
            // numericUpDownImageFactor1
            // 
            this.numericUpDownImageFactor1.Location = new System.Drawing.Point(529, 10);
            this.numericUpDownImageFactor1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownImageFactor1.Name = "numericUpDownImageFactor1";
            this.numericUpDownImageFactor1.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownImageFactor1.TabIndex = 4;
            this.numericUpDownImageFactor1.ValueChanged += new System.EventHandler(this.numericUpDownForCounts_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Image Factor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(435, 543);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Valid holes (specy by ,)";
            // 
            // lblNumberOfBlobs
            // 
            this.lblNumberOfBlobs.AutoSize = true;
            this.lblNumberOfBlobs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfBlobs.Location = new System.Drawing.Point(435, 278);
            this.lblNumberOfBlobs.Name = "lblNumberOfBlobs";
            this.lblNumberOfBlobs.Size = new System.Drawing.Size(87, 17);
            this.lblNumberOfBlobs.TabIndex = 12;
            this.lblNumberOfBlobs.Text = "For Counts";
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadImage.Location = new System.Drawing.Point(12, 426);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(417, 27);
            this.btnUploadImage.TabIndex = 21;
            this.btnUploadImage.Text = "Update/Upload Existing Image";
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(435, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Area Greater Than";
            // 
            // listBoxTotalAreas
            // 
            this.listBoxTotalAreas.FormattingEnabled = true;
            this.listBoxTotalAreas.Location = new System.Drawing.Point(435, 180);
            this.listBoxTotalAreas.Name = "listBoxTotalAreas";
            this.listBoxTotalAreas.Size = new System.Drawing.Size(165, 95);
            this.listBoxTotalAreas.TabIndex = 24;
            // 
            // numericUpDownAreaGreaterThan1
            // 
            this.numericUpDownAreaGreaterThan1.Location = new System.Drawing.Point(529, 38);
            this.numericUpDownAreaGreaterThan1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownAreaGreaterThan1.Name = "numericUpDownAreaGreaterThan1";
            this.numericUpDownAreaGreaterThan1.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownAreaGreaterThan1.TabIndex = 25;
            this.numericUpDownAreaGreaterThan1.ValueChanged += new System.EventHandler(this.numericUpDownAreaGreaterThan_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(435, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Crop Area";
            // 
            // comboBoxCropType
            // 
            this.comboBoxCropType.FormattingEnabled = true;
            this.comboBoxCropType.Items.AddRange(new object[] {
            "Left",
            "Top",
            "Right",
            "Bottom"});
            this.comboBoxCropType.Location = new System.Drawing.Point(529, 75);
            this.comboBoxCropType.Name = "comboBoxCropType";
            this.comboBoxCropType.Size = new System.Drawing.Size(71, 21);
            this.comboBoxCropType.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(435, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Crop Area %";
            // 
            // numericUpDownCropPercent
            // 
            this.numericUpDownCropPercent.Location = new System.Drawing.Point(529, 103);
            this.numericUpDownCropPercent.Name = "numericUpDownCropPercent";
            this.numericUpDownCropPercent.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownCropPercent.TabIndex = 29;
            this.numericUpDownCropPercent.ValueChanged += new System.EventHandler(this.numericUpDownCropPercent_ValueChanged);
            // 
            // numericUpDownAreaGreater2
            // 
            this.numericUpDownAreaGreater2.Location = new System.Drawing.Point(547, 349);
            this.numericUpDownAreaGreater2.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownAreaGreater2.Name = "numericUpDownAreaGreater2";
            this.numericUpDownAreaGreater2.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownAreaGreater2.TabIndex = 36;
            this.numericUpDownAreaGreater2.ValueChanged += new System.EventHandler(this.numericUpDownAreaGreater2_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(453, 356);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Area Greater Than";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(453, 323);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Image Factor";
            // 
            // numericUpDownImageFactor2
            // 
            this.numericUpDownImageFactor2.Location = new System.Drawing.Point(547, 321);
            this.numericUpDownImageFactor2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownImageFactor2.Name = "numericUpDownImageFactor2";
            this.numericUpDownImageFactor2.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownImageFactor2.TabIndex = 33;
            this.numericUpDownImageFactor2.ValueChanged += new System.EventHandler(this.numericUpDownImageFactor2_ValueChanged);
            // 
            // lblTotalBlobCounts
            // 
            this.lblTotalBlobCounts.AutoSize = true;
            this.lblTotalBlobCounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBlobCounts.Location = new System.Drawing.Point(443, 511);
            this.lblTotalBlobCounts.Name = "lblTotalBlobCounts";
            this.lblTotalBlobCounts.Size = new System.Drawing.Size(87, 17);
            this.lblTotalBlobCounts.TabIndex = 37;
            this.lblTotalBlobCounts.Text = "For Counts";
            // 
            // listBoxTotalAreas2
            // 
            this.listBoxTotalAreas2.FormattingEnabled = true;
            this.listBoxTotalAreas2.Location = new System.Drawing.Point(443, 426);
            this.listBoxTotalAreas2.Name = "listBoxTotalAreas2";
            this.listBoxTotalAreas2.Size = new System.Drawing.Size(165, 82);
            this.listBoxTotalAreas2.TabIndex = 38;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(440, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Fill Holes";
            // 
            // checkBoxFillHoles1
            // 
            this.checkBoxFillHoles1.AutoSize = true;
            this.checkBoxFillHoles1.Location = new System.Drawing.Point(529, 149);
            this.checkBoxFillHoles1.Name = "checkBoxFillHoles1";
            this.checkBoxFillHoles1.Size = new System.Drawing.Size(63, 17);
            this.checkBoxFillHoles1.TabIndex = 40;
            this.checkBoxFillHoles1.Text = "Yes/No";
            this.checkBoxFillHoles1.UseVisualStyleBackColor = true;
            this.checkBoxFillHoles1.CheckedChanged += new System.EventHandler(this.checkBoxFillHoles1_CheckedChanged);
            // 
            // checkBoxFillHoles2
            // 
            this.checkBoxFillHoles2.AutoSize = true;
            this.checkBoxFillHoles2.Location = new System.Drawing.Point(544, 393);
            this.checkBoxFillHoles2.Name = "checkBoxFillHoles2";
            this.checkBoxFillHoles2.Size = new System.Drawing.Size(63, 17);
            this.checkBoxFillHoles2.TabIndex = 42;
            this.checkBoxFillHoles2.Text = "Yes/No";
            this.checkBoxFillHoles2.UseVisualStyleBackColor = true;
            this.checkBoxFillHoles2.CheckedChanged += new System.EventHandler(this.checkBoxFillHoles2_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(455, 393);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "Fill Holes";
            // 
            // txtValidHoles
            // 
            this.txtValidHoles.Location = new System.Drawing.Point(435, 559);
            this.txtValidHoles.Name = "txtValidHoles";
            this.txtValidHoles.Size = new System.Drawing.Size(173, 20);
            this.txtValidHoles.TabIndex = 43;
            // 
            // btnCalculateMinMax
            // 
            this.btnCalculateMinMax.Location = new System.Drawing.Point(435, 585);
            this.btnCalculateMinMax.Name = "btnCalculateMinMax";
            this.btnCalculateMinMax.Size = new System.Drawing.Size(172, 23);
            this.btnCalculateMinMax.TabIndex = 45;
            this.btnCalculateMinMax.Text = "Calculate Min-Max";
            this.btnCalculateMinMax.UseVisualStyleBackColor = true;
            this.btnCalculateMinMax.Click += new System.EventHandler(this.btnCalculateMinMax_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveChanges.Location = new System.Drawing.Point(852, 632);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(172, 41);
            this.btnSaveChanges.TabIndex = 46;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(1040, 632);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(172, 41);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtMaxRange
            // 
            this.txtMaxRange.Location = new System.Drawing.Point(503, 640);
            this.txtMaxRange.Name = "txtMaxRange";
            this.txtMaxRange.Size = new System.Drawing.Size(104, 20);
            this.txtMaxRange.TabIndex = 48;
            // 
            // txtMinRange
            // 
            this.txtMinRange.Location = new System.Drawing.Point(503, 614);
            this.txtMinRange.Name = "txtMinRange";
            this.txtMinRange.Size = new System.Drawing.Size(104, 20);
            this.txtMinRange.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(440, 617);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Min Range";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(437, 643);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 51;
            this.label11.Text = "Max Range";
            // 
            // TeachImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 677);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMinRange);
            this.Controls.Add(this.txtMaxRange);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnCalculateMinMax);
            this.Controls.Add(this.txtValidHoles);
            this.Controls.Add(this.checkBoxFillHoles2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.checkBoxFillHoles1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listBoxTotalAreas2);
            this.Controls.Add(this.lblTotalBlobCounts);
            this.Controls.Add(this.numericUpDownAreaGreater2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDownImageFactor2);
            this.Controls.Add(this.numericUpDownCropPercent);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxCropType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownAreaGreaterThan1);
            this.Controls.Add(this.listBoxTotalAreas);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.lblNumberOfBlobs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownImageFactor1);
            this.Controls.Add(this.pictureBoxBinaryImage);
            this.Controls.Add(this.pictureBoxImageForCounts);
            this.Controls.Add(this.pictureBoxOriginalImage);
            this.Name = "TeachImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TeachImage";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginalImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageForCounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBinaryImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageFactor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAreaGreaterThan1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCropPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAreaGreater2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageFactor2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxOriginalImage;
        private System.Windows.Forms.PictureBox pictureBoxImageForCounts;
        private System.Windows.Forms.PictureBox pictureBoxBinaryImage;
        private System.Windows.Forms.NumericUpDown numericUpDownImageFactor1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNumberOfBlobs;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBoxTotalAreas;
        private System.Windows.Forms.NumericUpDown numericUpDownAreaGreaterThan1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxCropType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownCropPercent;
        private System.Windows.Forms.NumericUpDown numericUpDownAreaGreater2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownImageFactor2;
        private System.Windows.Forms.Label lblTotalBlobCounts;
        private System.Windows.Forms.ListBox listBoxTotalAreas2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxFillHoles1;
        private System.Windows.Forms.CheckBox checkBoxFillHoles2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtValidHoles;
        private System.Windows.Forms.Button btnCalculateMinMax;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtMaxRange;
        private System.Windows.Forms.TextBox txtMinRange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
    }
}