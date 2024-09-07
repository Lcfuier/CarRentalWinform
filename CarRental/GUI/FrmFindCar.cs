using CarRental.BLL;
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
    public partial class FrmFindCar : Form
    {
        public FrmFindCar()
        {
            InitializeComponent();
        }
        BLLCar BllCar= new BLLCar();
        public delegate void TransData(string id);
        public TransData transData;
        private void FrmFindCar_Load(object sender, EventArgs e)
        {
            dgvCar.DataSource = BllCar.GetAllCarByStatus("Available");
            txtBrand.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string CarId = dgvCar.SelectedRows[0].Cells[0].Value.ToString();
            transData(CarId);
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            txtBrand.Text = "";
            FrmFindCar_Load(sender, e);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            dgvCar.DataSource = BllCar.GetCarByBrand(txtBrand.Text,"Available");
        }
    }
}
