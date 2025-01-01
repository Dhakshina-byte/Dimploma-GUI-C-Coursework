using C_Coursework.Controller;
using C_Coursework.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Coursework.Services
{
    public class Booking_Service
    {
        private readonly SqlConnection connection;

        public Booking_Service() 
        {
            connection = DatabaseConnection.GetConnection();
        }

        public void AddBooking(Booking booking)
        {
            connection.Close();
            connection.Open();

            string query = " INSERT INTO Flight_Booking(_From, Depart, _To, _Return, Class, Adult, Children, Infants, Flight_Number, Customer_ID, TotalPrice) VALUES (@From, @Depart, @To, @Return, @Class, @Adult, @Children, @Infants, @Flight_Number, @Customer_ID, @TotalPrice)"; 
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@From", booking.Departure);
            cmd.Parameters.AddWithValue("@Depart", booking.DepartureDate);
            cmd.Parameters.AddWithValue("@To", booking.Arrival);
            cmd.Parameters.AddWithValue("@Return", booking.ArrivalDate);
            cmd.Parameters.AddWithValue("@Class", booking.classtype);
            cmd.Parameters.AddWithValue("@Adult", booking.adultct);
            cmd.Parameters.AddWithValue("@Children", booking.childct);
            cmd.Parameters.AddWithValue("@Infants", booking.infantct);
            cmd.Parameters.AddWithValue("@Flight_Number", booking.FlightNumber);
            cmd.Parameters.AddWithValue("@Customer_ID", booking.userID);
            cmd.Parameters.AddWithValue("@TotalPrice", booking.totalprice);


         
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
