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
    public partial class FrmUser : Form
    {
        private Form FrmMain;
        private User userDTO;
        public FrmUser()
        {
            InitializeComponent();
        }
        public FrmUser(Form FrmMain,User userDTO)
        {
            InitializeComponent();
            this.FrmMain = FrmMain;
            this.userDTO = userDTO;
        }
        BLLUser BllUser= new BLLUser();
        User user = new User();
        private void txtFindCustomer_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            user.Id=txtId.Text;
            user.UserStatus=cbStatus.Text;
            user.Roles=cbRole.Text;
            user.FullName=txtFullname.Text;
            user.UserName=txtUsername.Text;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddUser f=new FrmAddUser();
            f.ShowDialog();
            FrmUser_Load(sender,e);
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            dgvUser.DataSource = BllUser.GetUserTable("",userDTO.Id);
            txtId.DataBindings.Clear();
            txtId.DataBindings.Add("Text", dgvUser.DataSource, "Id");
            txtFullname.DataBindings.Clear();
            txtFullname.DataBindings.Add("Text", dgvUser.DataSource, "Fullname");
            txtUsername.DataBindings.Clear();
            txtUsername.DataBindings.Add("Text", dgvUser.DataSource, "Username");
            cbRole.DataBindings.Clear();
            cbRole.DataBindings.Add("Text", dgvUser.DataSource, "Roles");
            cbStatus.DataBindings.Clear();
            cbStatus.DataBindings.Add("Text", dgvUser.DataSource, "UserStatus");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmMain.Show();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnDel.Enabled = false;
            txtFullname.Focus();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoadData();
                BllUser.DeleteUser(user);
                FrmUser_Load(sender, e);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtFullname.Text == "" || cbRole.Text == "" || cbStatus.Text == "")
            {
                MessageBox.Show("Không thể để trống các trường", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LoadData();
                BllUser.UpdateUser(user);
                btnAdd.Enabled = true;
                btnDel.Enabled = true;
            }
            FrmUser_Load(sender, e); ;
        }

        private void txtFindUser_TextChanged(object sender, EventArgs e)
        {
            if (txtFindUser.Text == "")
            {
                FrmUser_Load(sender, e);
            }
            else
            {
                string str = txtFindUser.Text;
                dgvUser.DataSource = BllUser.GetUserTable(str,userDTO.Id);
            }
        }
    }
}
