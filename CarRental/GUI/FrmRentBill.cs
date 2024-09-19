using CarRental.DTOs;
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
    public partial class FrmRentBill : Form
    {
        private User user;
        private Rental rental;
        private string Cusname;
        public FrmRentBill()
        {
            InitializeComponent();
        }
        public FrmRentBill(User user, Rental rental,string cusname)
        {
            InitializeComponent();
            this.user = user;
            this.rental = rental;
            this.Cusname = cusname;
        }

        private void FrmRentBill_Load(object sender, EventArgs e)
        {
            //đặt đường dẫn
            rpvRentBill.LocalReport.ReportEmbeddedResource = "CarRental.GUI.ReportRentBill.rdlc";

            ReportParameterCollection parameters = new ReportParameterCollection
                {
                 new ReportParameter("TenKhachHang",Cusname),
                new ReportParameter("TenNhanVien",user.FullName),
                new ReportParameter("BienSoXe",rental.CarId),
                new ReportParameter("NgayThue",rental.StartDate),
                 new ReportParameter("NgayTraDuKien",rental.EndDate),
                new ReportParameter("TongTien",rental.Fee),

            };

            rpvRentBill.LocalReport.SetParameters(parameters);
            this.rpvRentBill.RefreshReport();
        }

        private void rpvRentBill_Load(object sender, EventArgs e)
        {

        }

        private void lbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
