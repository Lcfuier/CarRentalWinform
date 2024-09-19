namespace CarRental.GUI
{
    partial class FrmRentBill
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
            this.rpvRentBill = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpvRentBill
            // 
            this.rpvRentBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvRentBill.LocalReport.ReportEmbeddedResource = "CarRental.GUI.ReportRentBill.rdlc";
            this.rpvRentBill.Location = new System.Drawing.Point(0, 0);
            this.rpvRentBill.Name = "rpvRentBill";
            this.rpvRentBill.ServerReport.BearerToken = null;
            this.rpvRentBill.Size = new System.Drawing.Size(864, 745);
            this.rpvRentBill.TabIndex = 1;
            // 
            // FrmRentBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(864, 745);
            this.Controls.Add(this.rpvRentBill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmRentBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hóa Đơn Thuê Xe";
            this.Load += new System.EventHandler(this.FrmRentBill_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpvRentBill;
    }
}