namespace Vilani.MatrixVision
{
    partial class Validation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Validation));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLicenseKey = new System.Windows.Forms.TextBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtActivationKey = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(607, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to Expert Vision Inspection System";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(82, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(298, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please enter License Key Below:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtLicenseKey
            // 
            this.txtLicenseKey.Location = new System.Drawing.Point(87, 144);
            this.txtLicenseKey.Multiline = true;
            this.txtLicenseKey.Name = "txtLicenseKey";
            this.txtLicenseKey.Size = new System.Drawing.Size(546, 100);
            this.txtLicenseKey.TabIndex = 2;
            this.txtLicenseKey.TextChanged += new System.EventHandler(this.txtLicenseKey_TextChanged);
            // 
            // btnValidate
            // 
            this.btnValidate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidate.Location = new System.Drawing.Point(278, 364);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(160, 41);
            this.btnValidate.TabIndex = 3;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(82, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(298, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Please enter Activate Key below:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtActivationKey
            // 
            this.txtActivationKey.Location = new System.Drawing.Point(87, 298);
            this.txtActivationKey.Multiline = true;
            this.txtActivationKey.Name = "txtActivationKey";
            this.txtActivationKey.Size = new System.Drawing.Size(546, 30);
            this.txtActivationKey.TabIndex = 5;
            this.txtActivationKey.TextChanged += new System.EventHandler(this.txtActivationKey_TextChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Crimson;
            this.lblStatus.Location = new System.Drawing.Point(345, 413);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "lblStatus";
            this.lblStatus.Visible = false;
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // Validation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 435);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtActivationKey);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.txtLicenseKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Validation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expert License Validation ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLicenseKey;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtActivationKey;
        private System.Windows.Forms.Label lblStatus;

    }
}
