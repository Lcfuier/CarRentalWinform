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
    public partial class FrmMain : Form
    {
        private Form FrmLogin;
        private User user;
        public FrmMain()
        {
            InitializeComponent();
        }
        public FrmMain(Form FrmLogin,User user)
        {
            InitializeComponent();
            this.FrmLogin = FrmLogin;
            this.user = user;
        }
        BLLUser BllUser =new BLLUser();
        BLLCustomer BllCustomer =new BLLCustomer();
        BLLCar BllCar=new BLLCar();
        private void btnCar_Click(object sender, EventArgs e)
        {
            FrmCar f= new FrmCar(this);
            this.Hide();
            f.ShowDialog();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmCustomer f = new FrmCustomer(this);
            this.Hide();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmRent f = new FrmRent(this);
            this.Hide();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmReturn f = new FrmReturn(this,user);
            this.Hide();
            f.ShowDialog();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            if (user.Roles != "Admin")
            {
                MessageBox.Show("Bạn không phải Admin!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FrmUser f = new FrmUser(this,user);
                this.Hide();
                f.ShowDialog();
            }
            
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            var dtCar = BllCar.GetAllCar();
            var dtUser = BllUser.GetAllUser();
            var dtCustomer=BllCustomer.GetAllCustomer();
            int numCar = dtCar.Rows.Count;
            int numUser = dtUser.Rows.Count;
            int numCustomer = dtCustomer.Rows.Count;
            lbTotalCar.Text = numCar.ToString();
            lbTotalCus.Text = numCustomer.ToString();
            lbName.Text = user.FullName;
            if (user.Roles == "User")
            {
                btnUser.Enabled=false;
                btnReport.Enabled=false;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (user.Roles != "Admin")
            {
                MessageBox.Show("Bạn không phải Admin!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FrmReport f = new FrmReport(this);
                this.Hide();
                f.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            user = null;
            this.Close();
            FrmLogin.Show();
        }

        private void btnInform_Click(object sender, EventArgs e)
        {
            FrmInformation f = new FrmInformation(this,user);
            f.ShowDialog();
            FrmMain_Load(sender,e);
        }
    }
}
