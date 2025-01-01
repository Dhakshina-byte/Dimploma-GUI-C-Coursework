using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Coursework.Model
{
    public class FlightBookingBilling
    {
        public int BillingID { get; set; } 
        public int BookingID { get; set; }  
        public int CustomerID { get; set; } 
        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }
        public double TotalAmount { get; set; } 
        public string PaymentMethod { get; set; }
        public string DiscountType { get; set; }   
        public double Discount { get; set; }
        public string PaymentStatus { get; set; } 
        public string TransactionID { get; set; } 
        public DateTime BillingDate { get; set; } 
        public string Currency { get; set; }
        public string Status { get; set; }

        public static implicit operator FlightBookingBilling(string v)
        {
            throw new NotImplementedException();
        }
    }
}
