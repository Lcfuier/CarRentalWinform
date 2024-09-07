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
    public partial class FrmInformation : Form
    {
        private Form FrmMain;
        private User user;
        public FrmInformation()
        {
            InitializeComponent();
        }
        BLLUser BllUser=new BLLUser();
        public FrmInformation(Form FrmMain,User user)
        {
            InitializeComponent();
            this.FrmMain = FrmMain;
            this.user = user;
        }
        private void FrmInformation_Load(object sender, EventArgs e)
        {
            txtId.Text = user.Id;
            txtName.Text = user.FullName;
            txtUsername.Text = user.UserName;
            txtPassword.Clear(); 
            txtNewPass.Clear();
            txtVerify.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtId.Text == "" || txtName.Text == "" || txtUsername.Text == "" || txtNewPass.Text == "" || txtPassword.Text == "" || txtVerify.Text == "")
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(!txtNewPass.Text.Equals(txtVerify.Text))
                {
                    MessageBox.Show("Mật khẩu không trùng nhau", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var newPassword=txtVerify.Text;
                    var password=txtPassword.Text;
                    if(!BllUser.CheckPassword(password, user.PassWordHash, user.Salt))
                    {
                        MessageBox.Show("Mật khẩu không chính xác", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        user.FullName = txtName.Text;
                        user.PassWordHash = newPassword;
                        BllUser.UpdateInformation(user);
                    }
                    
                }
            }
            FrmInformation_Load(sender,e);
        }
    }
}
