using SalesDataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDataAccess
{
    public class DataEntryConnector : Connector
    {
        public List<DataEntry> GetAllDataEntries()
        {
            List<DataEntry> list = new List<DataEntry>();

            string sql = "SELECT c.CustomerID, od.[ProductID] " +
            "FROM[dbo].[Customers] AS c, " +
            "[dbo].[Orders] AS o, " +
            "[dbo].[Order Details] AS od " +
            "where c.CustomerID = o.CustomerID " +
            "and o.[OrderID] = od.[OrderID]";

            Open();
            SqlDataReader reader = Query(sql);
            while(reader.Read())
            {
                DataEntry de = new DataEntry(); 
                de.CustomerID = (uint)reader.GetInt32(0);
                de.ProductID = (uint)reader.GetInt32(1); 
                list.Add(de);
            }
            reader.Close();
            Close();
            return list;
        }
    }
}
