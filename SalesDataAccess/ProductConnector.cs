using SalesDataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDataAccess
{
    public class ProductConnector : Connector
    {
        public List<Product> GetAllProducts()
        {

            List<Product> list = new List<Product>();

            string sql = "select * from Products"; Open();

            SqlDataReader reader = Query(sql);

            while (reader.Read())
            {

                Product p = new Product();

                p.ProductID = (uint)reader.GetInt32(0);
                p.ProductName = reader.GetString(1);

                list.Add(p);
            }
            reader.Close();
            Close();
            return list;
        }

        public Product GetProduct(int productId)
        {
            string sql = "select * from Products where ProductID=@productId";
            SqlParameter par = new SqlParameter("@productId", SqlDbType.Int);
            par.Value = productId;

            Product p = null;

            Open();

            SqlDataReader reader = Query(sql, new[] { par });
            if (reader.Read())
            {
                p = new Product();
                p.ProductID = (uint)reader.GetInt32(0); ;
                p.ProductName = reader.GetString(1);
            }
            reader.Close();
            Close();
            return p;
        }
    }
}
