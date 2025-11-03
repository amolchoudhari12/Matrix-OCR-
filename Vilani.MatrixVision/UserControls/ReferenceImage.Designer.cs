namespace Vilani.MatrixVision.UserControls
{
    partial class ReferenceImage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxRefrenceImage = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkSelectImage = new System.Windows.Forms.CheckBox();
            this.numericUpDownScore = new System.Windows.Forms.NumericUpDown();
            this.lblRefImageName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRefrenceImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScore)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxRefrenceImage
            // 
            this.pictureBoxRefrenceImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBoxRefrenceImage, 2);
            this.pictureBoxRefrenceImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxRefrenceImage.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxRefrenceImage.Name = "pictureBoxRefrenceImage";
            this.pictureBoxRefrenceImage.Size = new System.Drawing.Size(192, 145);
            this.pictureBoxRefrenceImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxRefrenceImage.TabIndex = 0;
            this.pictureBoxRefrenceImage.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 216);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxRefrenceImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkSelectImage, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownScore, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblRefImageName, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblResult, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.04663F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.95337F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(198, 214);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // chkSelectImage
            // 
            this.chkSelectImage.AutoSize = true;
            this.chkSelectImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkSelectImage.Location = new System.Drawing.Point(180, 176);
            this.chkSelectImage.Name = "chkSelectImage";
            this.chkSelectImage.Size = new System.Drawing.Size(15, 14);
            this.chkSelectImage.TabIndex = 4;
            this.chkSelectImage.UseVisualStyleBackColor = true;
            this.chkSelectImage.CheckedChanged += new System.EventHandler(this.chkSelectImage_CheckedChanged);
            // 
            // numericUpDownScore
            // 
            this.numericUpDownScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownScore.Location = new System.Drawing.Point(140, 154);
            this.numericUpDownScore.Name = "numericUpDownScore";
            this.numericUpDownScore.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownScore.TabIndex = 2;
            this.numericUpDownScore.ValueChanged += new System.EventHandler(this.numericUpDownScore_ValueChanged);
            // 
            // lblRefImageName
            // 
            this.lblRefImageName.AutoSize = true;
            this.lblRefImageName.BackColor = System.Drawing.SystemColors.Info;
            this.lblRefImageName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRefImageName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.lblRefImageName.Location = new System.Drawing.Point(3, 173);
            this.lblRefImageName.Name = "lblRefImageName";
            this.lblRefImageName.Size = new System.Drawing.Size(131, 20);
            this.lblRefImageName.TabIndex = 3;
            this.lblRefImageName.Text = "lblRefImageName";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Score";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblResult, 2);
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(3, 193);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(192, 21);
            this.lblResult.TabIndex = 5;
            this.lblResult.Text = "label2";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblResult.Visible = false;
            // 
            // ReferenceImage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Name = "ReferenceImage";
            this.Size = new System.Drawing.Size(200, 216);
            this.Load += new System.EventHandler(this.ReferenceImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRefrenceImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxRefrenceImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkSelectImage;
        private System.Windows.Forms.Label lblRefImageName;
        private System.Windows.Forms.NumericUpDown numericUpDownScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblResult;
    }
}
