using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal abstract class Vehicle
    {
        protected string TypeOfVehicle;
        protected string Make;
        protected string Model;
        protected int Year;
        protected decimal DailyRate;
        protected string Transmission;
        protected string Status;
        protected int SeatCapacity;
        protected string FuelType;
        public bool IsValid { get; protected set; } // Only set within the classes.

        //public Vehicle(string make, string model, string yr, string rate, string trans)
        //{
        //    Make = make;
        //    Model = model;
        //    if (SetYear(yr) && SetRate(rate) && SetTransmission(trans))
        //    {
        //        IsValid = true;
        //    }
        //    else
        //    {
        //        // Vehicle will not be added to the collection or file.
        //        IsValid = false;
        //    }
        //    Status = "Available";
        //}
        //public Vehicle()
        //{
        //    TypeOfVehicle = string.Empty;
        //    Make = string.Empty;
        //    Model = string.Empty;
        //    Year = 0;
        //    DailyRate = 0;
        //    Transmission = string.Empty;
        //    Status = string.Empty;
        //    IsValid = false;
        //    SeatCapacity = 0;
        //    FuelType = string.Empty;    
        //} // Default Constructor.
        public string GetType() { return TypeOfVehicle; }
        public string GetMake() { return Make; }
        public string GetModel () { return Model; }
        public int GetYear() { return Year; }
        public decimal GetRate() { return DailyRate; }
        public string GetTransmission() { return Transmission; }

        public abstract void SetType(); 
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
        } // Converts and validates the Year input.
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
                    Console.WriteLine("Daily Rate should be more than £0.00");
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
        } // Converts and validates the Daily Rate input.
        public bool SetTransmission(string tr)
        {
            string[] a = { "a", "automatic", "auto"};
            string[] m = { "m", "manual", "man" };
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
        } // Validates the Transmission type input.
        public bool SetSeatCap(string seatNum)
        {
            int seats = 0;
            try
            {
                seats = int.Parse(seatNum);
                if (seats >= 1)
                {
                    SeatCapacity = seats;
                    return true;
                }
                else { return false; }
            }
            catch { return false; }
        } // Converts and validates the Seat Capacity input.
        public bool SetFuelType(string fuelType)
        {
            string[] types = { "diesel", "petrol", "electric", "hybrid" };
            if (types.Contains(fuelType))
            {
                FuelType = fuelType;
                return true;
            }
            else { return false; }
        } // Validates the Fuel type input.
        public string UpdateStatus
        {
            get { return Status; }
            set { Status = value; }
        } // Gets and Sets - Doesn't need validation. 
        public abstract string ToFile();
    }
}
