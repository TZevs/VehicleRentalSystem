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
        public bool IsAvailable;

        public Vehicle(string make, string model, int year, decimal dailyRate)
        {
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            IsAvailable = true;
        }
    }
}
