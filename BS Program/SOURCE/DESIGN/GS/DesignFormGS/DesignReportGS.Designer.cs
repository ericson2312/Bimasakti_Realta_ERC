namespace DesignFormGS
{
    partial class DesignReportGS
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
            GSM01500PrintCenter = new Button();
            SuspendLayout();
            // 
            // GSM01500PrintCenter
            // 
            GSM01500PrintCenter.Location = new Point(12, 12);
            GSM01500PrintCenter.Name = "GSM01500PrintCenter";
            GSM01500PrintCenter.Size = new Size(170, 23);
            GSM01500PrintCenter.TabIndex = 0;
            GSM01500PrintCenter.Text = "GSM01500PrintCenter";
            GSM01500PrintCenter.UseVisualStyleBackColor = true;
            GSM01500PrintCenter.Click += GSM01500PrintCenter_Click;
            // 
            // DesignReportGS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(GSM01500PrintCenter);
            Name = "DesignReportGS";
            Text = "DesignReportGS";
            Load += DesignReportGS_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button GSM01500PrintCenter;
    }
}