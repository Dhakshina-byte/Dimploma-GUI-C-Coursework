using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Coursework.Controller
{
    public class DatabaseConnection
    {
        private static readonly string connectionString = "Data Source=OM3GA;Initial Catalog=Flight_Booking_System;Integrated Security=True";
        private static SqlConnection connection;

        public static SqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection(connectionString);
            }
            return connection;
        }
    }
}
