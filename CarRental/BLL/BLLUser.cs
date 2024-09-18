using CarRental.DAL;
using CarRental.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental.BLL
{
    public class BLLUser
    {
        DataAccessLayer Dal = new DataAccessLayer();
        private string HashPassword(string password, byte[] salt)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                // Concatenate password and salt
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                // Hash the concatenated password and salt
                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                // Concatenate the salt and hashed password for storage
                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
        }
        static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16]; // Adjust the size based on your security requirements
                rng.GetBytes(salt);
                return salt;
            }
        }

        public void CreateUser(User user)
        {
            DataTable dtId = Dal.GetDataTable($"Select * From Users Where Id='" + user.Id + "'");
            if(dtId.Rows.Count > 0)
            {
                MessageBox.Show("Id đã tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataTable dtUsername = Dal.GetDataTable($"Select * from Users where Username = '" + user.UserName + "'");
                if(dtUsername.Rows.Count > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var salt = GenerateSalt();
                    string passwordHash = HashPassword(user.PassWordHash, salt);
                    string saltString = Convert.ToBase64String(salt);
                    string sqlQuery = $@"Insert into Users (FullName,Username,PassWordHash,Roles,UserStatus,Salt)
                        values('"  + user.FullName + "','" + user.UserName + "','" + passwordHash + "','" + user.Roles + "', '" + user.UserStatus + "',N'" + saltString + "')";
                    if (Dal.RunQuery(sqlQuery))
                    {
                        MessageBox.Show("Thêm thành công", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Thêm không thành công", "Thông báo");
                    }
                }
            }
            
        }
        public DataTable GetUserTable(string str,string userId)
        {
            string s;
            if (str == "")
            {
                s = $@"Select Id,FullName,UserName,Roles,UserStatus From Users Where Not Id ='"+userId+"'"; 
            }
            else
            {
                s= $@"Select Id,FullName,UserName,Roles,UserStatus From Users
                    Where Fullname Like N'%"+str+ "%'AND Not Id ='"+userId+"'";
            }
            return Dal.GetDataTable(s);
        }
        public void DeleteUser(User user)
        {
            string sqlQuery = @"Delete From Users Where Id='" + user.Id + "'";

            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Delete thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Delete không thành công", "Thông báo");
            }
        }
        public void UpdateUser(User user)
        {
            string sqlQuery = $"UPDATE Users set Fullname = N'" + user.FullName + "',Roles='" + user.Roles + "',UserStatus='" + user.UserStatus + "' Where Id='" + user.Id + "'";
            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Sửa thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Sửa không thành công", "Thông báo");
            }
        }
        public DataTable GetAllUser()
        {
            string str = "Select * from Users";
            return Dal.GetDataTable(str);
            
        }
        public bool CheckPassword(string passwordEntern,string passwordHash,string saltString)
        {
            var SaltByte = Convert.FromBase64String(saltString);
            string password = HashPassword(passwordEntern, SaltByte);
            return password == passwordHash;
        }
        public bool Login(User user)
        {
            DataTable dt = Dal.GetDataTable($"Select * From Users Where Username='" +user.UserName  + "'");
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else 
            {
                var passwordStore= dt.Rows[0]["PassWordHash"].ToString();
                var salt= dt.Rows[0]["Salt"].ToString();
                if (!CheckPassword(user.PassWordHash, passwordStore, salt))
                {
                    MessageBox.Show("Mật khẩu không chính xác", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    var status = dt.Rows[0]["UserStatus"].ToString();
                    if(status == "Passive")
                    {
                        MessageBox.Show("Tài khoản của bạn đã bị khóa, liên hệ admin để mở lại!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                
            }
        }
        public DataTable GetUserByUsername (string username)
        {
           return Dal.GetDataTable($"Select * From Users Where Username='" + username + "'");
        }
        public void UpdateInformation(User user)
        {
            var SaltByte = Convert.FromBase64String(user.Salt);
            string password = HashPassword(user.PassWordHash, SaltByte);
            string sqlQuery = $"UPDATE Users set Fullname = N'" + user.FullName + "',PassWordHash='" + password + "' Where Id='" + user.Id + "'";
            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Cập nhật thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Cập nhật không thành công", "Thông báo");
            }
        }
    }
}
