using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Coursework.Controller
{
    public class PassengerPriceCalculator
    {
        private readonly string classType;
        private readonly int adultCount;
        private readonly int childCount;
        private readonly float flightPrice;
        private float classValue;
        private float totalPrice;

        public PassengerPriceCalculator(string classtype,int adultcount,int childcount,float flightprice) 
        {
            classType = classtype;
            adultCount = adultcount;
            childCount = childcount;
            flightPrice = flightprice;
            classValue = ClassValue(classType);
            totalPrice = TotalPrice(adultCount, childCount, flightPrice, classValue);

        }

        public float ClassValue(string classType)
        {

            if (classType == "Economy")
            {
                return classValue = 30;
            }
            else if (classType == "Premium Economy")
            {
                return classValue = 60;
            }
            else if (classType == "Business")
            {
                return classValue = 150;
            }
            else if (classType == "First Class")
            {
                return classValue = 300;
            }
            else
            {
                return classValue = 0;
            }
        }

        public float TotalPrice(int adultCount, int childCount, float flightPrice, float classValue)
        {
            totalPrice = (adultCount * classValue + childCount * classValue ) + flightPrice;
            return totalPrice;
        }

        public float GetTotalPrice()
        {
            return totalPrice;
        }
    }
}
