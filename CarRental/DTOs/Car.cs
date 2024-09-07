using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DTOs
{
    public class Car
    {
        private string _Id;
        private string _Brand;
        private string _Model;
        private string _Status;
        private int _Price;
        public Car() { }    
        public Car(string Id, string Brand, string Model,string Status,int Price)
        {
            _Id = Id;
            _Brand = Brand;
            _Model = Model;
            _Price = Price;
            _Status=Status;
        }
        public string Id
        {
            get
            {
                return _Id;
            }
            set { _Id = value; }
        }
        public string Brand
        {
            get
            {
                return _Brand;
            }
            set { _Brand = value; }
        }
        public string Model
        {
            get
            {
                return _Model;
            }
            set { _Model = value; }
        }
        public string Status
        {
            get
            {
                return _Status;
            }
            set { _Status= value; }
        }
        public int Price
        {
            get
            {
                return _Price;
            }
            set { _Price = value; }
        }
    }
}
