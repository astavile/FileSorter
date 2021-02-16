
namespace AltiumFileGenerator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSetFileSize = new System.Windows.Forms.Label();
            this.nudFileSize = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.prgBarCreation = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.nudFileSize)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSetFileSize
            // 
            this.lblSetFileSize.AutoSize = true;
            this.lblSetFileSize.Location = new System.Drawing.Point(12, 9);
            this.lblSetFileSize.Name = "lblSetFileSize";
            this.lblSetFileSize.Size = new System.Drawing.Size(145, 20);
            this.lblSetFileSize.TabIndex = 0;
            this.lblSetFileSize.Text = "Set the file size (Gb):";
            // 
            // nudFileSize
            // 
            this.nudFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFileSize.Location = new System.Drawing.Point(183, 7);
            this.nudFileSize.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudFileSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFileSize.Name = "nudFileSize";
            this.nudFileSize.Size = new System.Drawing.Size(152, 27);
            this.nudFileSize.TabIndex = 1;
            this.nudFileSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(141, 100);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(94, 29);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Run";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(241, 100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 29);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // prgBarCreation
            // 
            this.prgBarCreation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBarCreation.Location = new System.Drawing.Point(12, 54);
            this.prgBarCreation.Name = "prgBarCreation";
            this.prgBarCreation.Size = new System.Drawing.Size(323, 17);
            this.prgBarCreation.Step = 1;
            this.prgBarCreation.TabIndex = 4;
            this.prgBarCreation.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(347, 141);
            this.Controls.Add(this.prgBarCreation);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.nudFileSize);
            this.Controls.Add(this.lblSetFileSize);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(365, 188);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(365, 188);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test file generator";
            ((System.ComponentModel.ISupportInitialize)(this.nudFileSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSetFileSize;
        private System.Windows.Forms.NumericUpDown nudFileSize;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar prgBarCreation;
    }
}

