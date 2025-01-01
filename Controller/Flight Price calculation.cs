using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Coursework.Model
{
    public class PriceCalculation
    {
        private readonly string Departure;
        private readonly string Arrival;
        private int distance;
        private float price;
        private float Duration;

        public PriceCalculation(string departure, string arrival)
        {
            Departure = departure;
            Arrival = arrival;
            distance = Destination(Departure, Arrival);
            Duration = DurationCalculator(distance);
            price = PriceCalculator(distance);
        }

        public int Destination(string Departure, string Arrival)
        {
            int distance = 0;
            if (Departure == "Bandaranaike International Airport (CMB)" && Arrival == "Mattala Rajapaksa International Airport (HRI)")
            {
                return distance = 200;
            }
            else if (Departure == "Bandaranaike International Airport (CMB)" && Arrival == "Hartsfield-Jackson Atlanta International Airport (ATL)")
            {
                return distance = 14000;
            }
            else if (Departure == "Bandaranaike International Airport (CMB)" && Arrival == "Los Angeles International Airport (LAX)")
            {
                return distance = 15000;
            }
            else if (Departure == "Bandaranaike International Airport (CMB)" && Arrival == "Chicago O'Hare International Airport (ORD)")
            {
                return distance = 13500;
            }
            else if (Departure == "Bandaranaike International Airport (CMB)" && Arrival == "Dallas/Fort Worth International Airport (DFW)")
            {
                return distance = 14200;
            }
            else if (Departure == "Bandaranaike International Airport (CMB)" && Arrival == "Denver International Airport (DEN)")
            {
                return distance = 14100;
            }
            else if (Departure == "Mattala Rajapaksa International Airport (HRI)" && Arrival == "Bandaranaike International Airport (CMB)")
            {
                return distance = 200;
            }
            else if (Departure == "Mattala Rajapaksa International Airport (HRI)" && Arrival == "Hartsfield-Jackson Atlanta International Airport (ATL)")
            {
                return distance = 14000;
            }
            else if (Departure == "Mattala Rajapaksa International Airport (HRI)" && Arrival == "Los Angeles International Airport (LAX)")
            {
                return distance = 15000;
            }
            else if (Departure == "Mattala Rajapaksa International Airport (HRI)" && Arrival == "Chicago O'Hare International Airport (ORD)")
            {
                return distance = 13500;
            }
            else if (Departure == "Mattala Rajapaksa International Airport (HRI)" && Arrival == "Dallas/Fort Worth International Airport (DFW)")
            {
                return distance = 14200;
            }
            else if (Departure == "Mattala Rajapaksa International Airport (HRI)" && Arrival == "Denver International Airport (DEN)")
            {
                return distance = 14100;
            }
            else if (Departure == "Hartsfield-Jackson Atlanta International Airport (ATL)" && Arrival == "Bandaranaike International Airport (CMB)")
            {
                return distance = 14000;
            }
            else if (Departure == "Hartsfield-Jackson Atlanta International Airport (ATL)" && Arrival == "Mattala Rajapaksa International Airport (HRI)")
            {
                return distance = 14000;
            }
            else if (Departure == "Hartsfield-Jackson Atlanta International Airport (ATL)" && Arrival == "Los Angeles International Airport (LAX)")
            {
                return distance = 2000;
            }
            else if (Departure == "Hartsfield-Jackson Atlanta International Airport (ATL)" && Arrival == "Chicago O'Hare International Airport (ORD)")
            {
                return distance = 700;
            }
            else if (Departure == "Hartsfield-Jackson Atlanta International Airport (ATL)" && Arrival == "Dallas/Fort Worth International Airport (DFW)")
            {
                return distance = 800;
            }
            else if (Departure == "Hartsfield-Jackson Atlanta International Airport (ATL)" && Arrival == "Denver International Airport (DEN)")
            {
                return distance = 1200;
            }
            else if (Departure == "Los Angeles International Airport (LAX)" && Arrival == "Bandaranaike International Airport (CMB)")
            {
                return distance = 15000;
            }
            else if (Departure == "Los Angeles International Airport (LAX)" && Arrival == "Mattala Rajapaksa International Airport (HRI)")
            {
                return distance = 15000;
            }
            else if (Departure == "Los Angeles International Airport (LAX)" && Arrival == "Hartsfield-Jackson Atlanta International Airport (ATL)")
            {
                return distance = 2000;
            }
            else if (Departure == "Los Angeles International Airport (LAX)" && Arrival == "Chicago O'Hare International Airport (ORD)")
            {
                return distance = 1700;
            }
            else if (Departure == "Los Angeles International Airport (LAX)" && Arrival == "Dallas/Fort Worth International Airport (DFW)")
            {
                return distance = 1400;
            }
            else if (Departure == "Los Angeles International Airport (LAX)" && Arrival == "Denver International Airport (DEN)")
            {
                return distance = 1000;
            }
            else if (Departure == "Chicago O'Hare International Airport (ORD)" && Arrival == "Bandaranaike International Airport (CMB)")
            {
                return distance = 13500;
            }
            else if (Departure == "Chicago O'Hare International Airport (ORD)" && Arrival == "Mattala Rajapaksa International Airport (HRI)")
            {
                return distance = 13500;
            }
            else if (Departure == "Chicago O'Hare International Airport (ORD)" && Arrival == "Hartsfield-Jackson Atlanta International Airport (ATL)")
            {
                return distance = 700;
            }
            else if (Departure == "Chicago O'Hare International Airport (ORD)" && Arrival == "Los Angeles International Airport (LAX)")
            {
                return distance = 1700;
            }
            else if (Departure == "Chicago O'Hare International Airport (ORD)" && Arrival == "Dallas/Fort Worth International Airport (DFW)")
            {
                return distance = 800;
            }
            else if (Departure == "Chicago O'Hare International Airport (ORD)" && Arrival == "Denver International Airport (DEN)")
            {
                return distance = 1000;
            }
            else
            {
                return distance;
            }
        }
        public float DurationCalculator(int distance)
        {
            float duration = 0;
            duration = distance * 36 / 86400;
            return duration;
        }
        public float PriceCalculator(int distance)
        {
            float price = 0;
            price = distance * 0.05f;
            return price;
        }

        public float GetPrice()
        {
            return price;
        }

    }
}
