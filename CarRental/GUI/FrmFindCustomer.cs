using CarRental.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental.GUI
{
    public partial class FrmFindCustomer : Form
    {
        public FrmFindCustomer()
        {
            InitializeComponent();
        }
        BLLCustomer BllCustomer = new BLLCustomer();    
        public delegate void TransData(string id);
        public TransData transData;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmFindCustomer_Load(object sender, EventArgs e)
        {
            dgvCustomer.DataSource = BllCustomer.GetAllCustomer();
            txtFullName.Clear();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            dgvCustomer.DataSource = BllCustomer.GetCustomerByName(txtFullName.Text);
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string CustomerId = dgvCustomer.SelectedRows[0].Cells[0].Value.ToString();
            transData(CustomerId);
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            txtFullName.Text = "";
            FrmFindCustomer_Load(sender,e);
        }
    }
}
