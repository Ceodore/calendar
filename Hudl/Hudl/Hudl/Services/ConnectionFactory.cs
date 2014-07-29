using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Hudl.Services
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection GetConnection()
        {
            const string server = "BERV-PC\\SQLEXPRESS";
            const string database = "Berv_Concerts";
            const string userId = "sa";
            const string password = "hudltest";

            const string connectionString = "Data Source=" + server + ";Database=" + database + ";UID=" + userId + ";Password=" + password;
            return new SqlConnection(connectionString);
        }
    }
}
