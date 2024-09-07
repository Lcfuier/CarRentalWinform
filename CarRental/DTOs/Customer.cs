using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DTOs
{
    public class Customer
    {
        private string _Id;
        private string _FullName;
        private string _Address;
        private string _PhoneNumber;
        public Customer() { }
        public Customer(string id, string fullName, string address, string phoneNumber)
        {
            _Id = id;
            _FullName = fullName;
            _Address = address;
            _PhoneNumber = phoneNumber;
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
        public string Address
        {
            get
            {
                return _Address;
            }
            set { _Address= value; }
        }
        public string PhoneNumber
        {
            get
            {
                return _PhoneNumber;
            }
            set { _PhoneNumber= value; }
        }
    }
}
