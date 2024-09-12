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
    public partial class FrmAddUser : Form
    {
        public FrmAddUser()
        {
            InitializeComponent();
        }

        BLLUser BllUser=new BLLUser();
        User user = new User();
        private void LoadData()
        {
            user.Id=txtId.Text;
            user.FullName = txtName.Text;
            user.UserName=txtUsername.Text;
            user.PassWordHash=txtPassword.Text;
            user.Roles = cbRole.Text;
            user.UserStatus=cbStatus.Text;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if ( txtName.Text == "" || txtPassword.Text == "" || txtUsername.Text == "" || txtVerifyPass.Text == "" || cbRole.Text == "" || cbStatus.Text == "")
            {
                MessageBox.Show("Không thể để trống các trường", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!txtPassword.Text.Equals(txtVerifyPass.Text))
                {
                    MessageBox.Show("Mật khẩu không trùng nhau", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    LoadData(); 
                    BllUser.CreateUser(user);
                    FrmAddUser_Load(sender,e);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddUser_Load(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtVerifyPass.Clear();
        }
    }
}
