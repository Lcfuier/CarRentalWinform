using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DTOs
{
    public class User
    {
        private string _Id;
        private string _FullName;
        private string _UserName;
        private string _PassWordHash;
        private string _Roles;
        private string _UserStatus;
        private string _Salt;
        public User() { }   
        public User(string id, string fullName, string userName, string passWordHash, string roles, string userStatus, string salt)
        {
            _Id = id;
            _FullName = fullName;
            _UserName = userName;
            _PassWordHash = passWordHash;
            _Roles = roles;
            _UserStatus = userStatus;
            _Salt = salt;
        }
        public string Id
        {
            get
            {
                return _Id;
            }
            set { _Id = value; }
        }
        public string FullName
        {
            get
            {
                return _FullName;
            }
            set { _FullName = value; }
        }
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set { _UserName= value; }
        }
        public string PassWordHash
        {
            get
            {
                return _PassWordHash;
            }
            set { _PassWordHash= value; }
        }
        public string Roles
        {
            get
            {
                return _Roles;
            }
            set { _Roles = value; }
        }
        public string UserStatus
        {
            get
            {
                return _UserStatus;
            }
            set { _UserStatus = value; }
        }
        public string Salt
        {
            get
            {
                return _Salt;
            }
            set { _Salt = value; }
        }
    }
}
