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
    public partial class FrmCar : Form
    {
        private Form FrmMain;
        public FrmCar()
        {
            InitializeComponent();
        }
        public FrmCar(Form FrmMain)
        {
            InitializeComponent();
            this.FrmMain = FrmMain;
            cbFindStatus.Text = "Tất cả";
        }
        BLLCar BllCar = new BLLCar();
        BLLRental BllRental= new BLLRental();
        bool add = false;
        Car car = new Car();
        string statusCar="";
        private void LoadData()
        {
            car.Id = txtId.Text;
            car.Brand = txtBrand.Text;
            car.Model = txtModel.Text;
            car.Price = int.Parse(txtPrice.Text.Replace(",", "").Replace(".", ""));
            car.Status = cbStatus.Text;
            
        }
        private void FrmCar_Load(object sender, EventArgs e)
        {
            cbChange();
            txtId.DataBindings.Clear();
            txtId.DataBindings.Add("Text", dgvCar.DataSource, "Id");
            txtBrand.DataBindings.Clear();
            txtBrand.DataBindings.Add("Text", dgvCar.DataSource, "Brand");
            txtModel.DataBindings.Clear();
            txtModel.DataBindings.Add("Text", dgvCar.DataSource, "Model");
            cbStatus.DataBindings.Clear();
            cbStatus.DataBindings.Add("Text", dgvCar.DataSource, "StatusCar");
            txtPrice.DataBindings.Clear();   
            txtPrice.DataBindings.Add("Text", dgvCar.DataSource, "Price");
            txtPrice.DataBindings[0].FormattingEnabled = true;
            txtPrice.DataBindings[0].FormatString = "#,## ";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtBrand.Clear();
            txtModel.Clear();
            txtPrice.Clear();
            txtId.Focus();
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
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa xe này không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoadData();
                BllCar.DeleteCar(car);
                FrmCar_Load(sender, e); ;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtBrand.Text == "" || txtModel.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Không thể để trống các trường", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (add)
                {
                    LoadData();
                    BllCar.AddCar(car);
                    btnUpdate.Enabled = true;
                    btnDel.Enabled = true;
                    add = false;
                }
                else
                {
                    LoadData();
                    if (statusCar == car.Status)
                    {
                        BllCar.UpdateCar(car);
                    }
                    else
                    {
                        DataTable dt = BllRental.GetRentalByCarId(car.Id);
                        if (dt.Rows.Count == 1)
                        {
                            MessageBox.Show("Xe đang thuê, không thể cập nhật trạng thái !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            BllCar.UpdateCar(car);
                        }
                    }
                    btnAdd.Enabled = true;
                    btnDel.Enabled = true;
                    txtId.Enabled = true; 
                }
                FrmCar_Load(sender, e);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmMain.Show();
        }

        private void txtFindReturn_TextChanged(object sender, EventArgs e)
        {
            if (txtFindCar.Text == "")
            {
                FrmCar_Load(sender, e);
            }
            else
            {
                string str=txtFindCar.Text;
                string statusCar=cbFindStatus.Text;
                if (statusCar == "Tất cả")
                {
                    dgvCar.DataSource = BllCar.GetCarByBrand(str, "");

                }
                else if (statusCar == "Chưa cho thuê")
                {
                    dgvCar.DataSource = BllCar.GetCarByBrand(str, "Available");
                }
                else if (statusCar == "Đã cho thuê")
                {
                    dgvCar.DataSource = BllCar.GetCarByBrand(str,"Rented");
                }
                
            }    
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void cbChange()
        {
            if (cbFindStatus.Text == "Tất cả")
            {
                dgvCar.DataSource = BllCar.GetAllCar();

            }
            else if (cbFindStatus.Text == "Chưa cho thuê")
            {
                dgvCar.DataSource = BllCar.GetCarByStatus("Available");
            }
            else if (cbFindStatus.Text == "Đã cho thuê")
            {
                dgvCar.DataSource = BllCar.GetCarByStatus("Rented");
            }
        }
        private void cbFindStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbChange();
        }

        private void btnDelCb_Click(object sender, EventArgs e)
        {
            cbStatus.Text = "";
            cbStatus.SelectedItem = null;
            FrmCar_Load(sender, e);
        }

        private void dgvCar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            statusCar = dgvCar.SelectedRows[0].Cells[3].Value.ToString();
        }
    }
}
