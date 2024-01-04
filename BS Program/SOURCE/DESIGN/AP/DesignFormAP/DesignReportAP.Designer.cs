namespace DesignFormAP
{
    partial class DesignReportAP
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
            APT00110PrintReport = new Button();
            SuspendLayout();
            // 
            // APT00110PrintReport
            // 
            APT00110PrintReport.Location = new Point(15, 11);
            APT00110PrintReport.Name = "APT00110PrintReport";
            APT00110PrintReport.Size = new Size(186, 23);
            APT00110PrintReport.TabIndex = 0;
            APT00110PrintReport.Text = "APT00110PrintReport";
            APT00110PrintReport.UseVisualStyleBackColor = true;
            APT00110PrintReport.Click += APT00110PrintReport_Click;
            // 
            // DesignReportAP
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(APT00110PrintReport);
            Name = "DesignReportAP";
            Text = "DesignReportAP";
            Load += DesignReportAP_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button APT00110PrintReport;
    }
}