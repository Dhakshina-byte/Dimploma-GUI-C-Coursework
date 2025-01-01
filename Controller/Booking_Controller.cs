using C_Coursework.Model;
using C_Coursework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Coursework.Controller
{
    public class Booking_Controller
    {
        private readonly Booking_Service bookingController;

        public Booking_Controller()
        {
            bookingController = new Booking_Service();
        }

        public void SaveBooking(Booking booking)
        {
            try
            {
                bookingController.AddBooking(booking);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving booking: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
