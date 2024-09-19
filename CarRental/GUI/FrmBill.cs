using CarRental.BLL;
using CarRental.DTOs;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;
using ZXing;
using System.IO;
using System.Drawing.Imaging;
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
            Bitmap qrCode = CreateBitMap();
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
            Bitmap bmp=CreateBitMap();
            using (MemoryStream ms=new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Bmp); 
                QRDataSet reportData=new QRDataSet();
                QRDataSet.QRCodeRow qrCodeRow = reportData.QRCode.NewQRCodeRow();
                qrCodeRow.Image=ms.ToArray();
                reportData.QRCode.AddQRCodeRow(qrCodeRow);
                
                ReportDataSource reportDataSource=new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value= reportData.QRCode;
                rpvBill.LocalReport.DataSources.Clear();
                rpvBill.LocalReport.DataSources.Add(reportDataSource);
                this.rpvBill.RefreshReport();
            }

            this.rpvBill.RefreshReport();

        }
        private Bitmap CreateBitMap()
        {
            var qrcode_text = $"2|99|{"0337722248"}|{"Phạm Khánh Duy"}|{"phamkhanhduy.contact@gmail.com"}|0|0|{returnCar.Amount}";
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions() { Width = 250, Height = 250, Margin = 0, PureBarcode = false };
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            Bitmap bitmap = barcodeWriter.Write(qrcode_text);
            Bitmap logo = resizeImage(Properties.Resources.logo_momo_png_1, 32, 32) as Bitmap;
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(logo, new Point((bitmap.Width - logo.Width) / 2, (bitmap.Height - logo.Height) / 2));
            return bitmap; 
        }
        public Image resizeImage(Image image, int new_height, int new_width)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((Image)new_image);
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }
        private void lbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
