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
        }
        BLLCar BllCar = new BLLCar();
        bool add = false;
        Car car = new Car();
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
            dgvCar.DataSource = BllCar.GetAllCar();
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
            txtId.Focus();
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
                    BllCar.UpdateCar(car);
                    btnAdd.Enabled = true;
                    btnDel.Enabled = true;
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
                dgvCar.DataSource = BllCar.GetCarByBrand(str,"");
            }    
        }
    }
}
