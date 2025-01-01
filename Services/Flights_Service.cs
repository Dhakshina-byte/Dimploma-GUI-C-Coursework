using C_Coursework.Controller;
using C_Coursework.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Coursework.Services
{
   public class Flights_Service
    {
        private readonly SqlConnection connection;
        
        public Flights_Service()
        {
            connection = DatabaseConnection.GetConnection();
        }
        public void InsertFlights(Flights flights)
        {
            try
            {
                connection.Close();
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Flights(Flight_Number,Airline_name,Plane_Type) VALUES (@FlightNumber,@AirLineName,@AirplaneType)", connection))
                {
                    cmd.Parameters.AddWithValue("@AirLineName", flights.AirLinename);
                    cmd.Parameters.AddWithValue("@FlightNumber", flights.FlightNumber);
                    cmd.Parameters.AddWithValue("@AirplaneType", flights.Airplanetype);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Flights_seats(Flight_Number, Economy_Seats,Premium_Economy_Seats,Business_Seats,First_Class_Seats,Total) VALUES(@FlightNumber,@Eseat,@EPseat,@Bseat,@Fseat,@Totalseat);", connection))
                {
                    cmd.Parameters.AddWithValue("@FlightNumber", flights.FlightNumber);
                    cmd.Parameters.AddWithValue("@Eseat", flights.Eseat);
                    cmd.Parameters.AddWithValue("@EPseat", flights.EPseat);
                    cmd.Parameters.AddWithValue("@Bseat", flights.Bseat);
                    cmd.Parameters.AddWithValue("@Fseat", flights.Fseat);
                    cmd.Parameters.AddWithValue("@Totalseat", flights.totalseat);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Flights_Availble_seats(Flight_Number,Economy_Seats,Premium_Economy_Seats,Business_Seats,First_Class_Seats,Total) VALUES(@FlightNumber,@Eseat,@EPseat,@Bseat,@Fseat,@Totalseat);", connection))
                {
                    cmd.Parameters.AddWithValue("@FlightNumber", flights.FlightNumber);
                    cmd.Parameters.AddWithValue("@Eseat", flights.Eseat);
                    cmd.Parameters.AddWithValue("@EPseat", flights.EPseat);
                    cmd.Parameters.AddWithValue("@Bseat", flights.Bseat);
                    cmd.Parameters.AddWithValue("@Fseat", flights.Fseat);
                    cmd.Parameters.AddWithValue("@Totalseat", flights.totalseat);
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
        public DataTable GetAllFlights()
        {
            {
                string query = "SELECT *FROM Flights INNER JOIN Flights_seats ON  Flights.Flight_Number = Flights_seats.Flight_Number;";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public void Deleteflight(string FlightNumber)
        {
            {
                connection.Close();
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Flights_seats WHERE Flight_Number = @Flight_Number", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Flight_Number", FlightNumber);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Flights_Availble_seats WHERE Flight_Number = @Flight_Number", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Flight_Number", FlightNumber);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Flights WHERE Flight_Number = @Flight_Number", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Flight_Number", FlightNumber);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void UpdateFlight(Flights flights)
        {
            {
                connection.Close();
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string FlightsQuery = "Update Flights set Airline_name=@AirLineName,Plane_Type=@AirplaneType where Flight_Number = @Flight_Number";
                        using (SqlCommand cmd = new SqlCommand(FlightsQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Flight_Number", flights.FlightNumber);
                            cmd.Parameters.AddWithValue("@AirLineName", flights.AirLinename);
                            cmd.Parameters.AddWithValue("@FlightNumber", flights.FlightNumber);
                            cmd.Parameters.AddWithValue("@AirplaneType", flights.Airplanetype);
                            cmd.ExecuteNonQuery();
                        }
                        string FlightsAVBQuery = "Update Flights_Availble_seats set Economy_Seats=@Eseat,Premium_Economy_Seats=@EPseat,Business_Seats=@Bseat,First_Class_Seats=@Fseat,Total=@Totalseat where Flight_Number = @Flight_Number";
                        using (SqlCommand cmd = new SqlCommand(FlightsAVBQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Eseat", flights.Eseat);
                            cmd.Parameters.AddWithValue("@EPseat", flights.EPseat);
                            cmd.Parameters.AddWithValue("@Bseat", flights.Bseat);
                            cmd.Parameters.AddWithValue("@Fseat", flights.Fseat);
                            cmd.Parameters.AddWithValue("@Totalseat", flights.totalseat);
                            cmd.Parameters.AddWithValue("@Flight_Number", flights.FlightNumber);
                            cmd.ExecuteNonQuery();
                        }

                        string FlightsSeaQuery = "Update Flights_seats set Economy_Seats=@Eseat,Premium_Economy_Seats=@EPseat,Business_Seats=@Bseat,First_Class_Seats=@Fseat,Total=@Totalseat where Flight_Number = @Flight_Number";

                        using (SqlCommand cmd = new SqlCommand(FlightsSeaQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Eseat", flights.Eseat);
                            cmd.Parameters.AddWithValue("@EPseat", flights.EPseat);
                            cmd.Parameters.AddWithValue("@Bseat", flights.Bseat);
                            cmd.Parameters.AddWithValue("@Fseat", flights.Fseat);
                            cmd.Parameters.AddWithValue("@Totalseat", flights.totalseat);
                            cmd.Parameters.AddWithValue("@Flight_Number", flights.FlightNumber);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public DataTable SearchFlights(string search)
        {
            {
                string query = "SELECT * FROM Flights INNER JOIN Flights_seats ON Flights.Flight_Number = Flights_seats.Flight_Number WHERE Flights.Flight_Number LIKE @search OR Flights.Airline_name LIKE @search OR Flights.Plane_Type LIKE @search";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@search", search);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
