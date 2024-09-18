using CarRental.BLL;
using CarRental.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental.GUI
{
    public partial class FrmRent : Form
    {
        private Form FrmMain;
        public FrmRent()
        {
            InitializeComponent();
        }
        public FrmRent(Form FrmMain)
        {
            InitializeComponent();
            this.FrmMain = FrmMain;
        }
        BLLRental BllRental=new BLLRental();
        BLLCar BllCar=new BLLCar();
        BLLCustomer BllCustomer =new BLLCustomer();
        Rental rental=new Rental();
        Customer customer = new Customer();
        bool add=false;
        string CarIdUpdated;
        private void FrmRent_Load(object sender, EventArgs e)
        {
            dgvRental.DataSource = BllRental.GetRentalDataTable("");
            txtId.DataBindings.Clear();
            txtId.DataBindings.Add("Text", dgvRental.DataSource, "Id");
            txtCarId.DataBindings.Clear();
            txtCarId.DataBindings.Add("Text", dgvRental.DataSource, "CarId");
            txtCustomerId.DataBindings.Clear();
            txtCustomerId.DataBindings.Add("Text", dgvRental.DataSource, "CustomerId");
            dtpStart.DataBindings.Clear();
            dtpStart.DataBindings.Add("Text", dgvRental.DataSource, "StartDate");
            dtpEnd.DataBindings.Clear();
            dtpEnd.DataBindings.Add("Text", dgvRental.DataSource, "EndDate");
            txtFee.DataBindings.Clear();
            txtFee.DataBindings.Add("Text", dgvRental.DataSource, "RentalFee");
            txtFee.DataBindings[0].FormattingEnabled = true;
            txtFee.DataBindings[0].FormatString = "#,## ";
            txtCusName.DataBindings.Clear();
            txtCusName.DataBindings.Add("Text", dgvRental.DataSource, "FullName");
            CarIdUpdated = txtCarId.Text;
        }
        private int GetDateRental()
        {
            DateTime d1 = dtpStart.Value.Date;
            DateTime d2 = dtpEnd.Value.Date;
            TimeSpan t = d2 - d1;
            int RentalDate = Convert.ToInt32(t.TotalDays);
            if (RentalDate == 0)
            {
                return 1;
            }
            return RentalDate;
        }
        private void LoadData()
        {
            rental.Id = txtId.Text;
            rental.StartDate = dtpStart.Value.Date.ToString();
            rental.EndDate = dtpEnd.Value.Date.ToString();
            rental.CarId = txtCarId.Text;
            rental.CustomerId = txtCustomerId.Text;
            rental.Fee = txtFee.Text.Replace(",", "").Replace(".", "");
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmMain.Show();
        }
        private void RentalFee()
        {
            if (txtCarId.Text != "")
            {
                DataTable dt = BllCar.GetCarById(txtCarId.Text);
                int fee = Convert.ToInt32(dt.Rows[0]["Price"].ToString());
                fee = fee * GetDateRental();
                txtFee.Text = fee.ToString();
            }
        }
        private void txtCustomerId_TextChanged(object sender, EventArgs e)
        {
            
        }
        public void GetCustomerId(string value)
        {
            txtCustomerId.Text = value;
            DataTable dt = BllCustomer.GetCustomerById(txtCustomerId.Text);
            string name = dt.Rows[0]["FullName"].ToString();
            txtCusName.Text = name;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FrmFindCustomer f = new FrmFindCustomer();
            f.transData = new FrmFindCustomer.TransData(GetCustomerId);
            f.ShowDialog();
        }
        public void GetCarId(string value)
        {
            txtCarId.Text = value;
            RentalFee();

        }
        private void btnFindCar_Click(object sender, EventArgs e)
        {
            FrmFindCar f = new FrmFindCar();
            f.transData = new FrmFindCar.TransData(GetCarId);
            f.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtFee.Clear();
            txtCustomerId.Clear();
            txtCarId.Clear();
            txtCusName.Clear();
            btnUpdate.Enabled = false;
            btnDel.Enabled = false;
            add = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            txtId.Enabled=false;
            btnAdd.Enabled = false;
            btnDel.Enabled = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoadData();
                BllCar.UpdateStatusCar(CarIdUpdated, "Available");
                BllRental.DeleteRental(rental);
                FrmRent_Load(sender, e); ;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ( txtCustomerId.Text == "" || txtFee.Text == "" || txtCarId.Text == "")
            {
                MessageBox.Show("Không thể để trống các trường", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int time = GetDateRental();
                if (time < 0)
                {
                    MessageBox.Show("Thời gian không hợp lệ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpEnd.Value = dtpStart.Value.AddDays(1);
                }
                else
                {
                    string carId = txtCarId.Text;
                    if (add)
                    {
                        LoadData();
                       if(BllRental.AddRental(rental))
                       {
                            MessageBox.Show("Thêm thành công", "Thông báo");
                            BllCar.UpdateStatusCar(carId, "Rented");
                       }
                        btnUpdate.Enabled = true;
                        btnDel.Enabled = true;
                        add = false;

                    }
                    else
                    {
                        LoadData();
                        if(BllRental.UpdateRental(rental)){
                            MessageBox.Show("Sửa thành công", "Thông báo");
                            if (carId != CarIdUpdated)
                            {
                                BllCar.UpdateStatusCar(carId, "Rented");
                                BllCar.UpdateStatusCar(CarIdUpdated, "Available");
                            }
                        }
                        btnAdd.Enabled = true;
                        btnDel.Enabled = true;
                    }
                }
                
                FrmRent_Load(sender, e);
            }
        }

        private void txtFee_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            RentalFee();
        }
        private void CheckTime()
        {
            
        }
        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            RentalFee();
        }

        private void dgvRental_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CarIdUpdated = txtCarId.Text;
        }

        private void txtFindRental_TextChanged(object sender, EventArgs e)
        {
            if (txtFindRental.Text == "")
            {
                FrmRent_Load(sender, e);
            }
            else
            {
                string str = txtFindRental.Text;
                dgvRental.DataSource = BllRental.GetRentalDataTable(str);
            }
        }
    }
}
