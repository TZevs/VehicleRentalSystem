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
        public string Transmission;
        public bool IsAvailable;

        public Vehicle(string make, string model, int year, decimal dailyRate, string transmission)
        {
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Transmission = transmission;
            IsAvailable = true;
        }
    }
}
