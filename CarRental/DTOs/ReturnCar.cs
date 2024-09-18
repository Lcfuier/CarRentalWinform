using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DTOs
{
    public class ReturnCar
    {
        private string _Id;
        private string _CustomerId;
        private string _CarId;
        private string _StartDate;
        private string _EndDate;
        private string _ReturnDate;
        private string _Fee;
        private string _FineDelay;
        private string _Amount;
        private string _UserId;
        private string _Note;
        private string _Surcharge;
        public ReturnCar() { }
        public ReturnCar(string id, string customerId, string carId, string startDate, string endDate, string fee, string fineDelay, string amount, string returnDate,string UserId, string note, string surcharge)
        {
            _Id = id;
            _CustomerId = customerId;
            _CarId = carId;
            _StartDate = startDate;
            _EndDate = endDate;
            _Fee = fee;
            _FineDelay = fineDelay;
            _Amount = amount;
            _ReturnDate = returnDate;
            _UserId = UserId;
            _Note = note;
            _Surcharge = surcharge;
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
            set { _StartDate = value; }
        }
        public string EndDate
        {
            get
            {
                return _EndDate;
            }
            set { _EndDate = value; }
        }
        public string CustomerId
        {
            get
            {
                return _CustomerId;
            }
            set { _CustomerId = value; }
        }
        public string CarId
        {
            get
            {
                return _CarId;
            }
            set { _CarId = value; }
        }
        public string Fee
        {
            get
            {
                return _Fee;
            }
            set { _Fee = value; }
        }
        public string FineDelay
        {
            get
            {
                return _FineDelay;
            }
            set { _FineDelay = value; }
        }
        public string Amount
        {
            get
            {
                return _Amount;
            }
            set { _Amount = value; }
        }
        public string ReturnDate
        {
            get
            {
                return _ReturnDate;
            }
            set { _ReturnDate = value; }
        }
        public string UserId
        {
            get
            {
                return _UserId;
            }
            set { _UserId = value; }
        }
        public string Note
        {
            get
            {
                return _Note;
            }
            set { _Note = value; }
        }
        public string Surcharge
        {
            get
            {
                return _Surcharge;
            }
            set { _Surcharge = value; }
        }
    }
}
