using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Vehicle
    {
        private int Id;
        private string Make;
        private string Model;
        private int Year;
        private decimal DailyRate;
        private string Transmission;
        private string Status;

        public Vehicle(string make, string model, int year, decimal dailyRate, string transmission)
        {
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Transmission = transmission;
            Status = "Available";
        }

        public string GetMake() { return Make; }
        public string GetModel() { return Model; }
        public int GetYear() { return Year; }
        public decimal GetRate() { return DailyRate; }
        public string GetTransmission() { return Transmission; }
        public string GetStatus() { return Status; }
        public void SetStatus(string status)
        {
            this.Status = status;
        }

        public string ToFile()
        {
            return $"{Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {Status}";
        }
    }
}
