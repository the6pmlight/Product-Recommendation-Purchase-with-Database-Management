using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SalesDataAccess
{
    public class Connector
    {
        public string ConnectionString { get; set; }
        public SqlConnection Connection { get; set; }

        public Connector()
        {
            ConnectionString = "server=XEMIZ\\SERVER_667; database=SalesData; Integrated Security=True;";
            Connection = new SqlConnection(ConnectionString);
        }

        public Connector(string connection)
        {
            ConnectionString = connection;
            Connection = new SqlConnection(ConnectionString);
        }
        public Connector(string server, string database, string uid, string pwd)
        {
            //C1
            /*            StringBuilder sb = new StringBuilder();
                        sb.Append("server=" + server + ";");
                        sb.Append("database=" + database + ";");
                        sb.Append("uid=" + uid + ";");
                        sb.Append("pwd=" + pwd);
                        ConnectionString = sb.ToString();*/

            //C2
            ConnectionString = "server=" + server + ";database=" + database + ";uid=" + uid + ";pwd=" + pwd;

            Connection = new SqlConnection(ConnectionString);
        }

        public void Open()
        {
            if (Connection == null)
                Connection = new SqlConnection(ConnectionString);
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
        }
        public void Close()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
        }
        public SqlDataReader Query(string query)
        {
            SqlCommand cmd = new SqlCommand(query, Connection);
            //Open();
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public SqlDataReader Query(string query, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddRange(parameters);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
    }
}
