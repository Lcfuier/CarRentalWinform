using CarRental.BLL;
using CarRental.DTOs;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CarRental.GUI
{
    public partial class FrmBill : Form
    {
        private User user;
        private ReturnCar returnCar;
        private string delayDate;
        private string Cusname;
        public FrmBill()
        {
            InitializeComponent();
        }
        public FrmBill(User user,ReturnCar returnCar, string delayDate,string CustomerName)
        {
            InitializeComponent();
            this.user = user;
            this.returnCar = returnCar;
            this.delayDate = delayDate;
            this.Cusname = CustomerName;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmBill_Load(object sender, EventArgs e)
        {

           
                //đặt đường dẫn
                rpvBill.LocalReport.ReportEmbeddedResource = "CarRental.GUI.ReportBill.rdlc";

               ReportParameterCollection parameters = new ReportParameterCollection
                {
                 new ReportParameter("TenKhachHang",Cusname),
                 new ReportParameter("TenNhanVien",user.FullName),
                 new ReportParameter("BienSoXe",returnCar.CarId),
                 new ReportParameter("NgayThue",returnCar.StartDate),
                 new ReportParameter("NgayTraDuKien",returnCar.EndDate),
                 new ReportParameter("NgayTra",returnCar.ReturnDate),
                 new ReportParameter("TienThue",returnCar.Fee),
                 new ReportParameter("SoNgayTre",delayDate),
                 new ReportParameter("TienPhat",returnCar.FineDelay),
                 new ReportParameter("TongTien",returnCar.Amount),
                 new ReportParameter("Note",returnCar.Note),
                 new ReportParameter("Surcharge",returnCar.Surcharge),
            };
                
                rpvBill.LocalReport.SetParameters(parameters);
                this.rpvBill.RefreshReport();
           
        }

        private void lbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
