using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Vehicle
    {
        private string Make;
        private string Model;
        private int Year;
        private decimal DailyRate;
        private string Transmission;
        private string Status;
        public bool IsValid { get; private set; }

        public Vehicle(string make, string model, string year, string dailyRate, string transmission)
        {
            Make = make;
            Model = model;
            IsValid = SetYear(year);
            IsValid = SetRate(dailyRate); 
            IsValid = SetTransmission(transmission);
            Status = "Available";
        }
        public string GetMake() { return Make; }
        public string GetModel () { return Model; }
        public bool SetYear(string y)
        {
            int yr;
            try
            {
                yr = int.Parse(y);
                if (yr >= DateTime.Now.Year - 30 && yr <= DateTime.Now.Year)
                {
                    Year = yr;
                    return true;
                }
                else
                {
                    if (yr < DateTime.Now.Year - 30) 
                    { 
                        Console.WriteLine("This Vehicle is too old"); 
                    }
                    else if (yr > DateTime.Now.Year) 
                    { 
                        Console.WriteLine($"Invalid year: '{yr}'");
                    }
                    return false;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public int GetYear() { return Year; }
        public bool SetRate(string r)
        {
            decimal dr;
            try
            {
                dr = decimal.Parse(r);
                if (dr > 0)
                {
                    DailyRate = dr;
                    return true;
                }
                else
                {
                    Console.WriteLine("Daily Rate should be more that £0.00");
                    return false;
                }
            }
            catch (FormatException ex) 
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public decimal GetRate() { return DailyRate; }
        public bool SetTransmission(string tr)
        {
            string[] a = { "Automatic", "Auto", "A", "automatic", "auto"};
            string[] m = { "Manual", "Man", "M", "manual", "man" };
            if (a.Contains(tr))
            {
                Transmission = "Automatic";
                return true;
            }
            else if (m.Contains(tr))
            {
                Transmission = "Manual";
                return true;
            }
            else
            {
                Console.WriteLine($"Invalid Input: '{tr}'");
                return false;
            }
        }
        public string GetTransmission() { return Transmission; }
        public string UpdateStatus
        {
            get { return Status; }
            set { Status = value; }
        }
        public string ToFile()
        {
            return $"{Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {Status}";
        }
    }
}
