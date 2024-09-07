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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace CarRental.GUI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        BLLUser BllUser = new BLLUser();
        User userDto=new User();
        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (txtusername.Text == "" || txtpassword.Text == "")
            {
                MessageBox.Show("Nhập tài khoản và mật khẩu !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                userDto.UserName = txtusername.Text;
                userDto.PassWordHash = txtpassword.Text;
                var login=BllUser.Login(userDto);
                if (!login)
                {
                    
                }
                else
                {
                    User user=new User();
                    DataTable dt = BllUser.GetUserByUsername(userDto.UserName);
                    user.Id= dt.Rows[0]["Id"].ToString();
                    user.FullName= dt.Rows[0]["Fullname"].ToString();
                    user.UserName= dt.Rows[0]["Username"].ToString();
                    user.PassWordHash= dt.Rows[0]["PassWordHash"].ToString();
                    user.Roles= dt.Rows[0]["Roles"].ToString();
                    user.UserStatus= dt.Rows[0]["UserStatus"].ToString();
                    user.Salt= dt.Rows[0]["Salt"].ToString();
                    this.Hide();
                    FrmMain frmMain = new FrmMain(this,user);
                    frmMain.ShowDialog();
                }
            }
            FrmLogin_Load(sender, e);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            login_showPass.Checked=false;
            txtpassword.PasswordChar = '●';
            txtpassword.Clear();
        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            txtpassword.PasswordChar = login_showPass.Checked ? '\0':'●' ;
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
