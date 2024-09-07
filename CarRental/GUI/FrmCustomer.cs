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
    public partial class FrmCustomer : Form
    {
        private Form FrmMain;
        public FrmCustomer()
        {
            InitializeComponent();
        }
        public FrmCustomer(Form FrmMain)
        {
            InitializeComponent();
            this.FrmMain = FrmMain;
        }
        BLLCustomer BllCustomer = new BLLCustomer();
        bool add = false;
        Customer customer =new Customer();
        private void LoadData()
        {
            customer.Id = txtId.Text;
            customer.FullName= txtFullname.Text;
            customer.Address= txtAddress.Text;
            customer.PhoneNumber= txtPhonenum.Text;
        }
        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            dgvCustomer.DataSource = BllCustomer.GetCustomerByName("");
            txtId.DataBindings.Clear();
            txtId.DataBindings.Add("Text", dgvCustomer.DataSource, "Id");
            txtFullname.DataBindings.Clear();
            txtFullname.DataBindings.Add("Text", dgvCustomer.DataSource, "FullName");
            txtAddress.DataBindings.Clear();
            txtAddress.DataBindings.Add("Text", dgvCustomer.DataSource, "Addr");
            txtPhonenum.DataBindings.Clear();
            txtPhonenum.DataBindings.Add("Text", dgvCustomer.DataSource, "PhoneNumber");
          
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmMain.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtFullname.Clear();
            txtAddress.Clear();
            txtPhonenum.Clear();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtAddress.Text == "" || txtFullname.Text == "" || txtPhonenum.Text == "")
            {
                MessageBox.Show("Không thể để trống các trường", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (add)
                {
                    LoadData();
                    BllCustomer.AddCustomer(customer);
                    btnUpdate.Enabled = true;
                    btnDel.Enabled = true;
                    add = false;
                }
                else
                {
                    LoadData();
                    BllCustomer.UpdateCustomer(customer);
                    btnAdd.Enabled = true;
                    btnDel.Enabled = true;
                }
                FrmCustomer_Load(sender, e);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoadData();
                BllCustomer.DeleteCustomer(customer);
                FrmCustomer_Load(sender, e); ;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtFindCustomer_TextChanged(object sender, EventArgs e)
        {
            if (txtFindCustomer.Text == "")
            {
                FrmCustomer_Load(sender, e);
            }
            else
            {
                string str = txtFindCustomer.Text;
                dgvCustomer.DataSource = BllCustomer.GetCustomerByName(str);
            }
        }
    }
}
