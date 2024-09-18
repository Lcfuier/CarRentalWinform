using CarRental.BLL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental.GUI
{
    public partial class FrmReport : Form
    {
        private Form FrmMain;
        public FrmReport()
        {
            InitializeComponent();
        }
        public FrmReport(Form FrmMain)
        {
            InitializeComponent();
            this.FrmMain = FrmMain;
        }
        BLLReturn BllReturn = new BLLReturn();  
        private void FrmReport_Load(object sender, EventArgs e)
        {
           
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
           
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
            FrmMain.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //đặt đường dẫn
                rpvReturn.LocalReport.ReportEmbeddedResource = "CarRental.GUI.ReportReturnCar.rdlc";
                //tạo rpDataSource
                ReportDataSource rp = new ReportDataSource();
                rp.Name = "DataSet1";//đặt tên trùng report
                rp.Value = BllReturn.GetReturnReport( dtpStart.Value.ToString(), dtpEnd.Value.ToString());
                rpvReturn.LocalReport.DataSources.Clear();
                rpvReturn.LocalReport.DataSources.Add(rp);

                this.rpvReturn.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
