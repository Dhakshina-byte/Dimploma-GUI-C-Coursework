using C_Coursework.Model;
using C_Coursework.Services;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Coursework.Controller
{
    public class FlightBookingBilling_Controller
    {
        private readonly FlightBookingBilling_Service _flightBookingBillingService;

        public FlightBookingBilling_Controller()
        {
            _flightBookingBillingService = new FlightBookingBilling_Service();
        }
        public double CalculateDiscount(String DiscountType, double Total)
        {
            if (DiscountType == "Seasonal ")
            {
                return Total * 0.2;
            }
            else if (DiscountType == "Employee")
            {
                return Total * 0.25;
            }
            else if (DiscountType == "Student")
            {
                return Total * 0.3;
            }
            else if (DiscountType == "VIP")
            {
                return Total * 0.45;
            }
            else if (DiscountType == "None")
            {
                return 0;
            }
            return 0;
        }
        public double CalculateDueAmount(double Total,string PaymentMethod, double PaidAmount)
        {
            if (PaymentMethod == "Cash")
            {
                return PaidAmount - Total;
            }
            return 0;
        }

        public void SaveFlightBookingBilling(FlightBookingBilling flightBookingBilling)
        {
            try
            {
                _flightBookingBillingService.AddFlightBookingBilling(flightBookingBilling);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving flight booking billing: {ex.Message}");
            }
        }
        public DataTable GetFlightBookingBilling()
        {
            return _flightBookingBillingService.GetFlightBookingBilling();
        }
    }
}
