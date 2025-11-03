namespace Vilani.MatrixVision
{
    partial class CreatePatternImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePatternImage));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBoxReference = new System.Windows.Forms.PictureBox();
            this.btnSelectScanAOI = new System.Windows.Forms.Button();
            this.btnCreateImage = new System.Windows.Forms.Button();
            this.txtAreaOfInterest = new System.Windows.Forms.TextBox();
            this.btnSaveReferenceImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReferenceImageName = new System.Windows.Forms.TextBox();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.txtImageName = new System.Windows.Forms.TextBox();
            this.pictureBoxLive = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSetScore = new System.Windows.Forms.Label();
            this.numericUpDownSetScore = new System.Windows.Forms.NumericUpDown();
            this.lblManadatory = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSetScore)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBoxReference);
            this.groupBox2.Location = new System.Drawing.Point(672, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(576, 425);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // pictureBoxReference
            // 
            this.pictureBoxReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxReference.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxReference.Name = "pictureBoxReference";
            this.pictureBoxReference.Size = new System.Drawing.Size(570, 406);
            this.pictureBoxReference.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxReference.TabIndex = 0;
            this.pictureBoxReference.TabStop = false;
            // 
            // btnSelectScanAOI
            // 
            this.btnSelectScanAOI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectScanAOI.Location = new System.Drawing.Point(958, 432);
            this.btnSelectScanAOI.Name = "btnSelectScanAOI";
            this.btnSelectScanAOI.Size = new System.Drawing.Size(287, 34);
            this.btnSelectScanAOI.TabIndex = 27;
            this.btnSelectScanAOI.Text = "Select Scan AOI";
            this.btnSelectScanAOI.UseVisualStyleBackColor = true;
            this.btnSelectScanAOI.Click += new System.EventHandler(this.btnSelectScanAOI_Click);
            // 
            // btnCreateImage
            // 
            this.btnCreateImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateImage.Location = new System.Drawing.Point(675, 432);
            this.btnCreateImage.Name = "btnCreateImage";
            this.btnCreateImage.Size = new System.Drawing.Size(277, 34);
            this.btnCreateImage.TabIndex = 26;
            this.btnCreateImage.Text = "Create Image";
            this.btnCreateImage.UseVisualStyleBackColor = true;
            this.btnCreateImage.Click += new System.EventHandler(this.btnCreateImage_Click);
            // 
            // txtAreaOfInterest
            // 
            this.txtAreaOfInterest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAreaOfInterest.Location = new System.Drawing.Point(865, 488);
            this.txtAreaOfInterest.Multiline = true;
            this.txtAreaOfInterest.Name = "txtAreaOfInterest";
            this.txtAreaOfInterest.ReadOnly = true;
            this.txtAreaOfInterest.Size = new System.Drawing.Size(380, 35);
            this.txtAreaOfInterest.TabIndex = 22;
            // 
            // btnSaveReferenceImage
            // 
            this.btnSaveReferenceImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveReferenceImage.Location = new System.Drawing.Point(865, 560);
            this.btnSaveReferenceImage.Name = "btnSaveReferenceImage";
            this.btnSaveReferenceImage.Size = new System.Drawing.Size(380, 34);
            this.btnSaveReferenceImage.TabIndex = 24;
            this.btnSaveReferenceImage.Text = "Save Reference Image";
            this.btnSaveReferenceImage.UseVisualStyleBackColor = true;
            this.btnSaveReferenceImage.Click += new System.EventHandler(this.btnSaveReferenceImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(715, 534);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "Enter Image Name";
            // 
            // txtReferenceImageName
            // 
            this.txtReferenceImageName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferenceImageName.Location = new System.Drawing.Point(865, 528);
            this.txtReferenceImageName.Name = "txtReferenceImageName";
            this.txtReferenceImageName.Size = new System.Drawing.Size(219, 26);
            this.txtReferenceImageName.TabIndex = 22;
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadImage.Location = new System.Drawing.Point(11, 548);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(647, 43);
            this.btnUploadImage.TabIndex = 20;
            this.btnUploadImage.Text = "Update/Upload Existing Image";
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // txtImageName
            // 
            this.txtImageName.Location = new System.Drawing.Point(11, 498);
            this.txtImageName.Multiline = true;
            this.txtImageName.Name = "txtImageName";
            this.txtImageName.ReadOnly = true;
            this.txtImageName.Size = new System.Drawing.Size(648, 44);
            this.txtImageName.TabIndex = 21;
            // 
            // pictureBoxLive
            // 
            this.pictureBoxLive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxLive.Location = new System.Drawing.Point(11, 6);
            this.pictureBoxLive.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxLive.Name = "pictureBoxLive";
            this.pictureBoxLive.Size = new System.Drawing.Size(648, 486);
            this.pictureBoxLive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLive.TabIndex = 18;
            this.pictureBoxLive.TabStop = false;
            this.pictureBoxLive.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxLive_Paint);
            this.pictureBoxLive.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLive_MouseDown);
            this.pictureBoxLive.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLive_MouseMove);
            this.pictureBoxLive.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLive_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(672, 499);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "AOI(Left,Top,Width,Height)";
            // 
            // lblSetScore
            // 
            this.lblSetScore.AutoSize = true;
            this.lblSetScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSetScore.Location = new System.Drawing.Point(1090, 534);
            this.lblSetScore.Name = "lblSetScore";
            this.lblSetScore.Size = new System.Drawing.Size(70, 17);
            this.lblSetScore.TabIndex = 30;
            this.lblSetScore.Text = "Set Score";
            // 
            // numericUpDownSetScore
            // 
            this.numericUpDownSetScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownSetScore.Location = new System.Drawing.Point(1180, 529);
            this.numericUpDownSetScore.Name = "numericUpDownSetScore";
            this.numericUpDownSetScore.Size = new System.Drawing.Size(65, 26);
            this.numericUpDownSetScore.TabIndex = 31;
            // 
            // lblManadatory
            // 
            this.lblManadatory.AutoSize = true;
            this.lblManadatory.Enabled = false;
            this.lblManadatory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManadatory.ForeColor = System.Drawing.Color.Red;
            this.lblManadatory.Location = new System.Drawing.Point(846, 537);
            this.lblManadatory.Name = "lblManadatory";
            this.lblManadatory.Size = new System.Drawing.Size(13, 17);
            this.lblManadatory.TabIndex = 32;
            this.lblManadatory.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(846, 499);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 17);
            this.label3.TabIndex = 33;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(1161, 534);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 17);
            this.label4.TabIndex = 34;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1106, 472);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Indicates Manadatory Fields";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(1090, 470);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 17);
            this.label6.TabIndex = 36;
            this.label6.Text = "*";
            // 
            // CreatePatternImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 606);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblManadatory);
            this.Controls.Add(this.numericUpDownSetScore);
            this.Controls.Add(this.lblSetScore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBoxLive);
            this.Controls.Add(this.btnSelectScanAOI);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.txtImageName);
            this.Controls.Add(this.txtAreaOfInterest);
            this.Controls.Add(this.btnCreateImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveReferenceImage);
            this.Controls.Add(this.txtReferenceImageName);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreatePatternImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Reference Image";
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSetScore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAreaOfInterest;
        private System.Windows.Forms.Button btnSaveReferenceImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReferenceImageName;
        private System.Windows.Forms.Button btnSelectScanAOI;
        private System.Windows.Forms.Button btnCreateImage;
        private System.Windows.Forms.PictureBox pictureBoxReference;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.TextBox txtImageName;
        private System.Windows.Forms.PictureBox pictureBoxLive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSetScore;
        private System.Windows.Forms.NumericUpDown numericUpDownSetScore;
        private System.Windows.Forms.Label lblManadatory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
