using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Vehicle
    {
        public int id;
        public string Make;
        public string Model;
        public int Year;
        public decimal DailyRate;
        public string Transmission;
        public string Status;

        public Vehicle(string make, string model, int year, decimal dailyRate, string transmission)
        {
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Transmission = transmission;
            Status = "Available";
        }

        public void SetVehicle(string make, string model, int year, decimal dailyRate, string transmission, string status)
        {
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.DailyRate = dailyRate;
            this.Transmission = transmission;
            this.Status = status;
        }
        public string GetMake() { return Make; }
        public string GetModel() { return Model; }
        public int GetYear() { return Year; }
        public decimal GetRate() { return DailyRate; }
        public string GetTransmission() { return Transmission; }
        public string GetStatus() { return Status; }
    }
}
