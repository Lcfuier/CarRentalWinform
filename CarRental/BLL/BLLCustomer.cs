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
    public class BLLCustomer
    {
        DataAccessLayer Dal = new DataAccessLayer();
        public DataTable GetAllCustomer()
        {
            string s = "Select * from Customer";
            return Dal.GetDataTable(s);
        }
        public DataTable GetCustomerByName(string name)
        {
            string s;
            if (name != "")
            {
                s = $"Select * from Customer Where FullName like N'%" + name + "%'";
            }
            else
            {
                s = "Select * from Customer";
            }
            return Dal.GetDataTable(s);
        }
        public DataTable GetCustomerById(string id)
        {
            string s = $"Select * from Customer where id ='"+id+"'";
            return Dal.GetDataTable(s);
        }
        public void AddCustomer(Customer customer)
        {
            string sqlQuery = $"Insert into Customer values('" + customer.Id+ "',N'" + customer.FullName + "',N'" + customer.Address+ "','" + customer.PhoneNumber +"')";
            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Thêm thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm không thành công", "Thông báo");
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            string sqlQuery = $"UPDATE Customer set FullName = N'" + customer.FullName + "',Addr=N'" + customer.Address+ "',PhoneNumber='" + customer.PhoneNumber + "' Where Id='" + customer.Id + "'";
            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Sửa thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Sửa không thành công", "Thông báo");
            }
        }
        public void DeleteCustomer(Customer customer)
        {
            string sqlQuery = @"Delete From Customer Where Id='" + customer.Id+ "'";

            if (Dal.RunQuery(sqlQuery))
            {
                MessageBox.Show("Delete thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Delete không thành công", "Thông báo");
            }
        }
    }
}
