using C_Coursework.Model;
using C_Coursework.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;

namespace C_Coursework.Controller
{
    public class FlightController
    {
        private readonly Flights_Service _dbContext;
        public FlightController()
        {
            _dbContext = new Flights_Service();
        }
        public void SaveFlight(Flights flight)
        {
            try
            {
                _dbContext.InsertFlights(flight);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving flight: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int CalculateTotalSeats(string economy, string firstClass, string business, string premiumEconomy)
        {
            int total = 0;
            if (int.TryParse(economy, out int ecoSeats)) total += ecoSeats;
            if (int.TryParse(firstClass, out int fClassSeats)) total += fClassSeats;
            if (int.TryParse(business, out int busSeats)) total += busSeats;
            if (int.TryParse(premiumEconomy, out int pEcoSeats)) total += pEcoSeats;
            return total;
        }
        public bool ValidateFlight(Flights flight)
        {
            if (string.IsNullOrEmpty(flight.AirLinename) || string.IsNullOrEmpty(flight.Airplanetype) || string.IsNullOrEmpty(flight.FlightNumber) || flight.Eseat == 0||flight.EPseat ==0)
            {
                MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public DataTable GetAllFlights()
        {
            return _dbContext.GetAllFlights();
        }

        public void DeleteFlight(string flightNumber)
        {
            _dbContext.Deleteflight(flightNumber);
        }
        public void UpdateFlight(Flights flight)
        {
            _dbContext.UpdateFlight(flight);
        }
        public DataTable SearchFlight(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return _dbContext.GetAllFlights();
            }
            return _dbContext.SearchFlights(search);
        }
    }
 }
