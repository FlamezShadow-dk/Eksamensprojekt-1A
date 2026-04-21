using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SnapPexOverview.PersistenceLayer
{
    public class Repository
    {
        protected string ConnectionString;

        protected Repository()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        protected SqlConnection CreateConnection()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            return con;
        }
    }
}
