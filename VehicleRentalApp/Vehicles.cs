using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Vehicle
    {
        public string Make;
        public string Model;
        public int Year;
        public decimal DailyRate;
        public bool IsAvailable = true;

        public Vehicle(string Make, string Model, int Year, decimal DailyRate)
        {
            this.Make = Make;
            this.Model = Model;
            this.Year = Year;
            this.DailyRate = DailyRate;
        }
    }
}
