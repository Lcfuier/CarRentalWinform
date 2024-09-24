using CarRental.DAL;
using CarRental.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental.BLL
{
    public class BLLCar
    {
        DataAccessLayer Dal=new DataAccessLayer();
        public DataTable GetAllCar()
        {
            string s = "Select * from Car";
            return Dal.GetDataTable(s);
        }
        public void AddCar(Car car)
        {
            DataTable dt = Dal.GetDataTable("Select * from Car Where Id='" + car.Id + "'");
            if (dt.Rows.Count>0)
            {
                MessageBox.Show("Biển số xe đã tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {


                string sqlQuery = $"Insert into Car values('" + car.Id + "','" + car.Brand + "','" + car.Model + "','" + car.Status + "','" + car.Price + "')";
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
        public void UpdateCar(Car car)
        {
            string sqlQuery = $"UPDATE Car set Brand = '" + car.Brand + "',Model='" + car.Model + "',Price='" + car.Price + "',StatusCar='" + car.Status+ "' Where Id='" + car.Id + "'";
            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Sửa thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Sửa không thành công", "Thông báo");
            }
        }
        public void DeleteCar(Car car)
        {
            string sqlQuery = @"Delete From Car Where Id='" + car.Id + "'";

            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Delete thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Delete không thành công", "Thông báo");
            }
        }
        
        public DataTable GetCarById(string id)
        {
            string s = "Select * from Car where Id= '" + id+ "'";
            return Dal.GetDataTable(s);
        }
        public void UpdateStatusCar(string Id,string status)
        {
            string sqlQuery = $"UPDATE Car set StatusCar='"+status+"' Where Id='" + Id + "'";
            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Đã cập nhật trạng thái xe", "Thông báo");
            }
            else
            {
                MessageBox.Show("Cập nhật trạng thái xe không thành công", "Thông báo");
            }

        }
        public DataTable GetAllCarByStatus(string status)
        {
            string s = "Select * from Car Where StatusCar like '%"+status+"%'" ;
            return Dal.GetDataTable(s);
        }
        public DataTable GetCarByBrand(string brand,string status)
        {
            string s;
            if (status == "")
            {
                s = "Select * from Car where Brand like N'%" + brand + "%'";
            }
            else
            {
                 s= "Select * from Car where Brand like N'%" + brand + "%'And StatusCar='"+status+"'";
            }
            return Dal.GetDataTable(s);
        }
        public DataTable GetCarByStatus(string status)
        {
            string s;
            s = "Select * from Car where StatusCar='"+status+"'";
            return Dal.GetDataTable(s);
        }

        public DataTable GetRevenueByCarReport(string dateStar, string dateEnd)
        {
            string s;
            s = $@"SELECT 
                        C.Id,
                        C.Brand,
                        C.Model,
                        COUNT(RC.Id) AS TotalRentals,
                        SUM(RC.RentalFee) AS TotalRentalRevenue,
                        SUM(RC.FineDelay) AS TotalFines,
	                    SUM(RC.Surcharge) AS TotalSurcharge,
                        SUM(RC.TotalAmount) AS TotalRevenue

                    FROM 
                        Car C
                     INNER JOIN ReturnCar RC ON C.Id = RC.CarId 
                where RC.ReturnDate BETWEEN '"+dateStar+"' AND '"+dateEnd+"' GROUP BY C.Id, C.Brand, C.Model ORDER BY TotalRevenue DESC";
            return Dal.GetDataTable(s);
        }
    }
}
