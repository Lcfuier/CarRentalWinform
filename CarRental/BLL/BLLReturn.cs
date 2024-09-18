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
    public class BLLReturn
    {

        DataAccessLayer Dal = new DataAccessLayer();
        public DataTable GetAllReturn()
        {
            string s = "Select * from ReturnCar ORDER BY ReturnCar.Id DESC";
            return Dal.GetDataTable(s);
        }
        public DataTable GetReturnDataTable(string str)
        {
            string s;
            if (str == "")
            {
                s= $@"SELECT ReturnCar.Id, ReturnCar.StartDate,ReturnCar.EndDate,ReturnCar.RentalFee,ReturnCar.CustomerId,Customer.FullName,ReturnCar.CarId,ReturnCar.Note,ReturnCar.Surcharge,ReturnCar.ReturnDate,ReturnCar.FineDelay,ReturnCar.TotalAmount
                FROM ReturnCar
                INNER JOIN Customer ON ReturnCar.CustomerId=Customer.Id
                ORDER BY ReturnCar.Id DESC";
            }
            else
            {
                 s= $@"SELECT ReturnCar.Id, ReturnCar.StartDate,ReturnCar.EndDate,ReturnCar.RentalFee,ReturnCar.CustomerId,Customer.FullName,ReturnCar.CarId,ReturnCar.Note,ReturnCar.Surcharge,ReturnCar.ReturnDate,ReturnCar.FineDelay,ReturnCar.TotalAmount
                FROM ReturnCar
                INNER JOIN Customer ON ReturnCar.CustomerId=Customer.Id
                Where Customer.FullName Like N'%" +str+ "%' ORDER BY ReturnCar.Id DESC";
            }
            return Dal.GetDataTable(s);
        }
        public bool AddReturn(ReturnCar returnCar)
        {
            string sqlQuery = $@"Insert into ReturnCar (StartDate,EndDate,RentalFee,ReturnDate,FineDelay,TotalAmount,CarId,CustomerId,UserId,Note,Surcharge)
                values('" + returnCar.StartDate + "','" + returnCar.EndDate + "','" + returnCar.Fee + "','" +returnCar.ReturnDate +"', '" + returnCar.FineDelay + "','" + returnCar.Amount + "','" +returnCar.CarId + "','" +returnCar.CustomerId +"','"+returnCar.UserId+"','"+returnCar.Note+"','"+returnCar.Surcharge+"')";
            if (Dal.RunQuery(sqlQuery))
            {
                return true;
                
            }
            else
            {
                return false;
                
            }
        }
        public void UpdateReturn(ReturnCar returnCar)
        {
            string sqlQuery = $"UPDATE ReturnCar set StartDate = '" + returnCar.StartDate + "',EndDate='" + returnCar.EndDate + "',RentalFee='" + returnCar.Fee + "',ReturnDate='" + returnCar.ReturnDate + "',FineDelay='" + returnCar.FineDelay + "',TotalAmount='" + returnCar.Amount + "',CarId='" + returnCar.CarId + "',CustomerId='" + returnCar.CustomerId + "',Note='"+returnCar.Note+ "',Surcharge='"+returnCar.Surcharge+"',UserId='"+returnCar.UserId+"' Where Id='" + returnCar.Id + "'";
            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Sửa thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Sửa không thành công", "Thông báo");
            }
        }
        public void DeleteReturn(ReturnCar returnCar)
        {
            string sqlQuery = @"Delete From ReturnCar Where Id='" + returnCar.Id + "'";

            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Delete thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Delete không thành công", "Thông báo");
            }
        }
        public DataTable GetReturnReport(string dateStar,string dateEnd)
        {
            string s = $@"select * from ReturnCar
                            where ReturnDate BETWEEN '"+dateStar+"' AND '"+dateEnd+"'";
            return Dal.GetDataTable(s);
        }
    }
}
