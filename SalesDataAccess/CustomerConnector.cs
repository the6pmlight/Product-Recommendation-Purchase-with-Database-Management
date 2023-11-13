using SalesDataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SalesDataAccess;

namespace SalesDataAccess
{
    public class CustomerConnector:Connector
    {
        public List<Customer> GetAllCustomers()
        {
            List<Customer> list = new List<Customer>();
            string sql = "select * from [Customers]";
            Open();

            SqlDataReader reader = Query(sql);
            while (reader.Read())
            {
                Customer cus = new Customer();

                cus.CustomerID = (uint)reader.GetInt32(0);
                cus.ContactName = reader.GetString(2);

                list.Add(cus);
            }
            reader.Close();
            Close();
            return list;
        }

        public Customer GetCustomer(int customerId)
        {
            string sql = "select * from [Customers] where CustomerID=@customerId"; 
            SqlParameter par = new SqlParameter("@customerId", SqlDbType.Int); 
            par.Value = customerId;

            Customer cus = null;

            Open();

            SqlDataReader reader = Query(sql, new[] { par }); 
            if (reader.Read())
            {
                cus = new Customer();
                cus.CustomerID = (uint)reader.GetInt32(0);
                cus.ContactName = reader.GetString(2);
            }

            reader.Close();
            Close();
            return cus;

        }
    }
}
