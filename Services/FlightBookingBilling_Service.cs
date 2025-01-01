using C_Coursework.Controller;
using C_Coursework.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace C_Coursework.Services
{
 public class FlightBookingBilling_Service
    {
        private readonly SqlConnection _connection;
        public int customerID;

        public FlightBookingBilling_Service()
        {
            _connection = DatabaseConnection.GetConnection();
        }

        public void AddFlightBookingBilling(FlightBookingBilling flightBookingBilling)
        {
            _connection.Close();
            _connection.Open();

            string query = "INSERT INTO FlightBookingBilling(BookingID, CustomerID, TotalAmount, PaymentMethod, PaymentStatus, TransactionID, Currency,PaidAmount,DueAmount,Discount) VALUES(@BookingID, @CustomerID, @TotalAmount, @PaymentMethod, @PaymentStatus, @TransactionID, @Currency,@PaidAmount,@DueAmount,@Discount)";            
            SqlCommand command = new SqlCommand(query, _connection);

            command.Parameters.AddWithValue("@BookingID", flightBookingBilling.BookingID);
            command.Parameters.AddWithValue("@CustomerID", flightBookingBilling.CustomerID);
            command.Parameters.AddWithValue("@TotalAmount", flightBookingBilling.TotalAmount);
            command.Parameters.AddWithValue("@PaidAmount", flightBookingBilling.PaidAmount);
            command.Parameters.AddWithValue("@DueAmount", flightBookingBilling.DueAmount);
            command.Parameters.AddWithValue("@PaymentMethod", flightBookingBilling.PaymentMethod);
            command.Parameters.AddWithValue("@PaymentStatus", flightBookingBilling.PaymentStatus);
            command.Parameters.AddWithValue("@TransactionID", flightBookingBilling.TransactionID);
            command.Parameters.AddWithValue("@Currency", flightBookingBilling.Currency);
            command.Parameters.AddWithValue("@Discount", flightBookingBilling.Discount);
            command.ExecuteNonQuery();
        }
        public DataTable GetFlightBookingBilling()
        {
            string query = "SELECT * FROM FlightBookingBilling";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
