using CarRental.DAL;
using CarRental.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental.BLL
{
    public class BLLRental
    {
        DataAccessLayer Dal = new DataAccessLayer();
        public DataTable GetAllRental()
        {
            string s = "Select * from Rental";
            return Dal.GetDataTable(s);
        }
        public DataTable GetRentalDataTable(string str)
        {
            string s;
            if (str == "")
            {
                s= $@"SELECT Rental.Id, Rental.StartDate,Rental.EndDate,Rental.RentalFee,Rental.CustomerId,Customer.FullName,Rental.CarId
                FROM Rental
                INNER JOIN Customer ON Rental.CustomerId=Customer.Id";
            }
            else
            {
               s= $@"SELECT Rental.Id, Rental.StartDate,Rental.EndDate,Rental.RentalFee,Rental.CustomerId,Customer.FullName,Rental.CarId
                FROM Rental
                INNER JOIN Customer ON Rental.CustomerId=Customer.Id
                Where Customer.FullName Like '%"+str+"%'";
            }
            return Dal.GetDataTable(s);
        }

        public void AddRental(Rental rental)
        {
            string sqlQuery = $"Insert into Rental values('" + rental.Id + "','" + rental.StartDate + "','" + rental.EndDate + "','" + rental.Fee + "','" + rental.CarId+"','" + rental.CustomerId+"')";
            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Thêm thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm không thành công", "Thông báo");
            }
        }
        public void UpdateRental(Rental rental)
        {
            string sqlQuery = $"UPDATE Rental set StartDate = '" + rental.StartDate+ "',EndDate='" + rental.EndDate + "',RentalFee='" + rental.Fee + "',CarId='" + rental.CarId+ "',CustomerId='" + rental.CustomerId+"' Where Id='" + rental.Id + "'";
            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Sửa thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Sửa không thành công", "Thông báo");
            }
        }
        public void DeleteRental(Rental rental)
        {
            string sqlQuery = @"Delete From Rental Where Id='" + rental.Id + "'";

            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Delete thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Delete không thành công", "Thông báo");
            }
        }
        public void DeleteRentalById(string Id)
        {
            string sqlQuery = @"Delete From Rental Where Id='" + Id + "'";

            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Đã cập nhật lại bảng thuê xe", "Thông báo");
            }
            else
            {
                MessageBox.Show("Delete thuê xe không thành công", "Thông báo");
            }
        }
    }
}
