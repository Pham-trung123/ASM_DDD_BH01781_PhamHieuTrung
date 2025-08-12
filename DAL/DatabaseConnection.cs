using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystem_Trung.DAL
{
    public class DatabaseConnection
    {
        // IMPORTANT: Change this connection string to match your SQL Server setup
        private static readonly string connectionString = @"Server=E44T742\SQLEXPRESS05;Database=StoreManagementDB;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
