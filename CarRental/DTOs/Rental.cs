using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DTOs
{
    public class Rental
    {
        private string _Id;
        private string _CustomerId;
        private string _CarId;
        private string _StartDate;
        private string _EndDate;
        private string _Fee;
        public Rental() { }
        public Rental(string id, string customerId, string carId, string startDate, string endDate, string fee)
        {
            _Id = id;
            _CustomerId = customerId;
            _CarId = carId;
            _StartDate = startDate;
            _EndDate = endDate;
            _Fee = fee;
        }
        public string Id
        {
            get
            {
                return _Id;
            }
            set { _Id = value; }
        }
        public string StartDate
        {
            get
            {
                return _StartDate;
            }
            set { _StartDate= value; }
        }
        public string EndDate
        {
            get
            {
                return _EndDate;
            }
            set { _EndDate= value; }
        }
        public string CustomerId
        {
            get
            {
                return _CustomerId;
            }
            set { _CustomerId= value; }
        }
        public string CarId
        {
            get
            {
                return _CarId;
            }
            set { _CarId= value; }
        }
        public string Fee
        {
            get
            {
                return _Fee;
            }
            set { _Fee= value; }
        }
    }
}
