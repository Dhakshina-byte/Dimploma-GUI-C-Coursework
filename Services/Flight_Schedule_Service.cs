using C_Coursework.Controller;
using C_Coursework.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Coursework.Services
{
    
    public class Flight_Schedule_Service
    {
        private readonly SqlConnection connection;
        

        public Flight_Schedule_Service()
        {
           connection = DatabaseConnection.GetConnection();

        }
        public DataTable GetAllSchedule()
        {
               string query = "SELECT * FROM Flights_Schedule ";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
        }
        public DataTable SearchSchedule(string Departure, string Arrival, string DepartureDate, string ArrivalDate)
        {
            string query = "SELECT * FROM Flights_Schedule WHERE _From LIKE @Departure AND _To LIKE @Arrival AND Depart LIKE @DepartureDate AND _Return LIKE @ArrivalDate";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Arrival", Departure);
                cmd.Parameters.AddWithValue("@Departure", Arrival);
                cmd.Parameters.AddWithValue("@DepartureDate", DepartureDate);
                cmd.Parameters.AddWithValue("@ArrivalDate",  ArrivalDate);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable Getflightnumber()
        {
            string query = "SELECT Flight_Number FROM Flights";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public void Insertschedule(Flight_Schedule flight_Schedule) 
        {
            try
            {
                connection.Close();
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Flights_Schedule(Flight_Number,_From,Depart,_Return,_To,_Status) VALUES (@FlightNumber,@_From,@Depart,@_Return,@_To,@_Status)", connection))
                {
                    cmd.Parameters.AddWithValue("@FlightNumber", flight_Schedule.FlightNumber);
                    cmd.Parameters.AddWithValue("@_From", flight_Schedule.Departure);
                    cmd.Parameters.AddWithValue("@Depart", flight_Schedule.DepartureDate);
                    cmd.Parameters.AddWithValue("@_Return",flight_Schedule.ArrivalDate);
                    cmd.Parameters.AddWithValue("@_To",flight_Schedule.Arrival);
                    cmd.Parameters.AddWithValue("@_Status",flight_Schedule.Status);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        public void UpdateSchedule(Flight_Schedule flight_Schedule)
        {
            connection.Close();
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    string ScheduleQuery = "UPDATE Flights_Schedule SET _Return=@_Return,_Status=@_Status WHERE  Flight_Number=@Flight_Number AND  Depart=@Depart ";
                    using (SqlCommand cmd = new SqlCommand(ScheduleQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Flight_Number", flight_Schedule.FlightNumber);
                        cmd.Parameters.AddWithValue("@Depart", flight_Schedule.DepartureDate);
                        cmd.Parameters.AddWithValue("@_Return", flight_Schedule.ArrivalDate);
                        cmd.Parameters.AddWithValue("@_Status", flight_Schedule.Status);

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
