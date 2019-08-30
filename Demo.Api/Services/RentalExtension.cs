using Demo.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Shared.Extensions
{
    public static class RentalExtension
    {
        public static decimal TotalRentalCost(this Rental rental, int numberOfDays)
        {
            if (numberOfDays <= 0)
                return rental.DailyRate;
            else
                return numberOfDays * rental.DailyRate;
        }
            
    }
}
