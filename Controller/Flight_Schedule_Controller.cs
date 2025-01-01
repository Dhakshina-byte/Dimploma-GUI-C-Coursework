using C_Coursework.Model;
using C_Coursework.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Coursework.Controller
{
    
    public class Flight_Schedule_Controller
    {
        private readonly Flight_Schedule_Service  _dbContext;
       

        public Flight_Schedule_Controller()
        {
            _dbContext = new Flight_Schedule_Service();
        }

        public DataTable GetAllSchedule()
        {
            return _dbContext.GetAllSchedule();
        }

        public DataTable SearchSchedule(string Departure, string Arrival, string DepartureDate, string ArrivalDate)
        {
            try
            {
                return _dbContext.SearchSchedule(Departure, Arrival, DepartureDate, ArrivalDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching schedule: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dbContext.GetAllSchedule();
            }
        }

        public DataTable Getflightnumber()
        {
            return _dbContext.Getflightnumber();
        }

        public void SaveSchedule(Flight_Schedule flight_Schedule) 
        {
            try
            {
                _dbContext.Insertschedule(flight_Schedule);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving flight: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void UpdateSchedule(Flight_Schedule flight_Schedule)
        {
            try
            {
                _dbContext.UpdateSchedule(flight_Schedule);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating flight: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
