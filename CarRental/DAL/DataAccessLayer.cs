using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental.DAL
{
    public class DataAccessLayer
    {
        private SqlDataAdapter da;
        private SqlConnection sqlcon;
        private DataTable dt;
        // xây dựng hàm kết nối dữ liệu 
        private void Connected()
        {
            string strcon = @"Data Source=DESKTOP-BCBACEV\SQLEXPRESS;Initial Catalog=CarRental;Integrated Security=True";
            try
            {
                if (sqlcon == null)
                {
                    sqlcon = new SqlConnection(strcon);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //hàm đổ dữ liệu vào dt
        public DataTable GetDataTable(string strQuery)
        {
            Connected();
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            da = new SqlDataAdapter(strQuery, sqlcon);
            dt = new DataTable();
            da.Fill(dt);
            if (sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
            }
            return dt;
        }
        //xây dựng hàm chạy query
        public bool RunQuery(string strQuery)
        {
            int check = 0;
            try
            {
                Connected();
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand(strQuery, sqlcon);
                check = cmd.ExecuteNonQuery();
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            if (check > 0)
            {
                return true;
            }
            return false;

        }
    }
}
