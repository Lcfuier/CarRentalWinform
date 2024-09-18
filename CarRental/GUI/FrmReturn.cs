using CarRental.BLL;
using CarRental.DTOs;
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
    public partial class FrmReturn : Form
    {
        private Form FrmMain;
        private User user;
        public FrmReturn()
        {
            InitializeComponent();
        }
        public FrmReturn(Form FrmMain, User user)
        {
            InitializeComponent();
            this.FrmMain = FrmMain;
            this.user = user;   
        }
        BLLRental BllRental = new BLLRental();
        BLLCar BllCar = new BLLCar();
        BLLCustomer BllCustomer = new BLLCustomer();
        BLLReturn BllReturn = new BLLReturn();
        ReturnCar returnCar =new ReturnCar();
        Rental rental = new Rental();
        bool add = false;
        string CarId;
        string RentalId;
        string delayDate;
        private void FrmReturn_Load(object sender, EventArgs e)
        {
            dgvReturn.DataSource = BllReturn.GetReturnDataTable("");
            dgvRental.DataSource = BllRental.GetRentalDataTable("");
            txtId.DataBindings.Clear();
            txtId.DataBindings.Add("Text", dgvReturn.DataSource, "Id");
            //
            txtCarId.DataBindings.Clear();
            txtCarId.DataBindings.Add("Text", dgvReturn.DataSource, "CarId");
            //
            txtCustomerId.DataBindings.Clear();
            txtCustomerId.DataBindings.Add("Text", dgvReturn.DataSource, "CustomerId");
            //
            dtpStart.DataBindings.Clear();
            dtpStart.DataBindings.Add("Text", dgvReturn.DataSource, "StartDate");
            //
            dtpEnd.DataBindings.Clear();
            dtpEnd.DataBindings.Add("Text", dgvReturn.DataSource, "EndDate");
            //
            txtAmount.DataBindings.Clear();
            txtAmount.DataBindings.Add("Text", dgvReturn.DataSource, "RentalFee");
            txtAmount.DataBindings[0].FormattingEnabled = true;
            txtAmount.DataBindings[0].FormatString = "#,##";
            //
            txtFine.DataBindings.Clear();
            txtFine.DataBindings.Add("Text", dgvReturn.DataSource, "FineDelay");
            txtFine.DataBindings[0].FormattingEnabled = true;
            txtFine.DataBindings[0].FormatString = "#,##";
            //
            txtFee.DataBindings.Clear();
            txtFee.DataBindings.Add("Text", dgvReturn.DataSource, "RentalFee");
            txtFee.DataBindings[0].FormattingEnabled = true;
            txtFee.DataBindings[0].FormatString = "#,##";
            //
            txtSur.DataBindings.Clear();
            txtSur.DataBindings.Add("Text", dgvReturn.DataSource, "Surcharge");
            txtSur.DataBindings[0].FormattingEnabled = true;
            txtSur.DataBindings[0].FormatString = "#,##";
            //
            txtNote.DataBindings.Clear();
            txtNote.DataBindings.Add("Text", dgvReturn.DataSource, "Note");
            //
            txtCusName.DataBindings.Clear();
            txtCusName.DataBindings.Add("Text", dgvReturn.DataSource, "FullName");
            
            dtpReturn.DataBindings.Clear();
            dtpReturn.DataBindings.Add("Text", dgvReturn.DataSource, "ReturnDate");
            txtDelay.Text = GetDateReturn().ToString();
            CarId = txtCarId.Text;
        }
        private void LoadData()
        {
            returnCar.Id = txtId.Text;
            returnCar.StartDate = dtpStart.Value.Date.ToString();
            returnCar.EndDate = dtpEnd.Value.Date.ToString();
            returnCar.CarId = txtCarId.Text;
            returnCar.CustomerId = txtCustomerId.Text;
            returnCar.Fee = txtFee.Text.Replace(",", "").Replace(".", "");
            returnCar.ReturnDate=dtpReturn.Value.Date.ToString();
            returnCar.FineDelay = txtFine.Text.Replace(",", "").Replace(".", "");
            returnCar.Amount=txtAmount.Text.Replace(",", "").Replace(".", "");
            returnCar.UserId = user.Id; 
            returnCar.Note= txtNote.Text;
            if (txtSur.Text == "")
            {
                returnCar.Surcharge = "0"; 
            }
            else
            {
               returnCar.Surcharge = txtSur.Text.Replace(",", "").Replace(".", "");
            }
             
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmMain.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtFee.Clear();
            txtCustomerId.Clear();
            txtCarId.Clear();
            txtCusName.Clear();
            txtFine.Clear();
            txtDelay.Clear();
            txtAmount.Clear();
            txtNote.Clear();
            txtSur.Clear();
            btnUpdate.Enabled = false;
            btnDel.Enabled = false;
            add = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnDel.Enabled = false;
        }

        private void dgvRental_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Rental.Id, Rental.StartDate,Rental.EndDate,Rental.RentalFee,Rental.CustomerId,Customer.FullName,Rental.CarId
            if (add)
            {
                dtpStart.Text = dgvRental.SelectedRows[0].Cells[1].Value.ToString();
                dtpEnd.Text = dgvRental.SelectedRows[0].Cells[2].Value.ToString();
                txtFee.Text = dgvRental.SelectedRows[0].Cells[3].Value.ToString();
                txtCustomerId.Text = dgvRental.SelectedRows[0].Cells[4].Value.ToString();
                txtCusName.Text = dgvRental.SelectedRows[0].Cells[5].Value.ToString();
                txtCarId.Text = dgvRental.SelectedRows[0].Cells[6].Value.ToString();
                RentalId = dgvRental.SelectedRows[0].Cells[0].Value.ToString();
                dtpReturn.Value = dtpEnd.Value;
                txtId.Clear();
                ReturnDelay();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoadData();
                BllReturn.DeleteReturn(returnCar);
                FrmReturn_Load(sender, e); 
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ( txtCustomerId.Text == "" || txtFee.Text == "" || txtCarId.Text == ""||txtAmount.Text==""||txtFine.Text==""||txtDelay.Text=="")
            {
                MessageBox.Show("Không thể để trống các trường", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string carId = txtCarId.Text;
                if (add)
                {
                    LoadData();
                    var check=BllReturn.AddReturn(returnCar);
                    if (check)
                    {
                        MessageBox.Show("Thêm thành công", "Thông báo");
                        BllCar.UpdateStatusCar(carId, "Available");
                        BllRental.DeleteRentalById(RentalId);
                        if (MessageBox.Show("Bạn có muốn xuất hóa đơn không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            FrmBill f= new FrmBill(user,returnCar,delayDate,txtCusName.Text);
                            f.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thêm không thành công", "Thông báo");
                    }
                    btnUpdate.Enabled = true;
                    btnDel.Enabled = true;
                    add = false;

                }
                else
                {
                    LoadData();
                    BllReturn.UpdateReturn(returnCar);
                    btnAdd.Enabled = true;
                    btnDel.Enabled = true;
                }
                FrmReturn_Load(sender, e);
            }
        }
        private int GetDateReturn()
        {
            DateTime d1 = dtpEnd.Value.Date;
            DateTime d2 = dtpReturn.Value.Date;
            TimeSpan t = d2 - d1;
            int RentalDate = Convert.ToInt32(t.TotalDays);
            return RentalDate;
        }
        private void ReturnDelay()
        {
            if (txtCarId.Text != "")
            {
                DataTable dt = BllCar.GetCarById(txtCarId.Text);
                int price= Convert.ToInt32(dt.Rows[0]["Price"].ToString());
                int FineDelay = 0;
                int Sur =0;
                if (txtSur.Text == "" || txtSur.Text == "0")
                {
                    Sur = 0;
                }
                else
                {
                    Sur = Convert.ToInt32(txtSur.Text.Replace(",", "").Replace(".", ""));
                }
                int fee = Convert.ToInt32(txtFee.Text.Replace(",", "").Replace(".", ""));
                if(GetDateReturn() > 0)
                {
                  FineDelay  = GetDateReturn() * price + 500000;
                } 
                txtDelay.Text = GetDateReturn().ToString();
                delayDate= GetDateReturn().ToString();
                txtFine.Text = FineDelay.ToString();
                txtAmount.Text = (fee + FineDelay+Sur).ToString();
            }
        }
        private void checkTime()
        {
            int time = GetDateReturn();
            if (time < 0)
            {
                MessageBox.Show("Thời gian không hợp lệ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpReturn.Value = dtpEnd.Value;
            }
            else
            {
                ReturnDelay();
            }
        }
        private void dtpReturn_ValueChanged(object sender, EventArgs e)
        {
            checkTime();
        }

        private void txtFindRental_TextChanged(object sender, EventArgs e)
        {
            if (txtFindRental.Text == "")
            {
                FrmReturn_Load(sender, e);
            }
            else
            {
                string str = txtFindRental.Text;
                dgvRental.DataSource = BllRental.GetRentalDataTable(str);
            }
        }

        private void txtFindReturn_TextChanged(object sender, EventArgs e)
        {
            if (txtFindReturn.Text == "")
            {
                FrmReturn_Load(sender, e);
            }
            else
            {
                string str = txtFindReturn.Text;
                dgvReturn.DataSource = BllReturn.GetReturnDataTable(str);
            }
        }

        private void dgvReturn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSur_TextChanged(object sender, EventArgs e)
        {
            ReturnDelay();
        }
    }
}
