using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Coursework.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public string classtype { get; set; }
        public float flightprice { get; set; }
        public int adultct { get; set; }
        public int childct { get; set; }
        public int infantct { get; set; }
        public float totalprice { get; set; }
        public int userID { get; set; }
        public string username { get; set; }
    }
}
