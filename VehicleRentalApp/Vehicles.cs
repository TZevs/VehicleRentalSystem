using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal abstract class Vehicle
    {
        [JsonInclude] protected string TypeOfVehicle;
        [JsonInclude] protected string Make;
        [JsonInclude] protected string Model;
        [JsonInclude] protected int Year;
        [JsonInclude] protected decimal DailyRate;
        [JsonInclude] protected string Transmission;
        protected string Status;
        [JsonInclude] protected int SeatCapacity;
        [JsonInclude] protected string FuelType;
        [JsonInclude] public bool IsValid { get; protected set; } // Only set within the classes.
        [JsonIgnore] protected List<string>? errorList = new List<string>();

        // Base Getters
        public string GetType() { return TypeOfVehicle; }
        public string GetMake() { return Make; }
        public string GetModel () { return Model; }
        public int GetYear() { return Year; }
        public decimal GetRate() { return DailyRate; }
        public string GetTransmission() { return Transmission; }
        public int GetSeatCap() { return SeatCapacity; }
        public string GetFuel() { return FuelType; }
        public string? GetErrorList()
        {
            return string.Join("\n" + errorList);
        }

        // Car member Getters
        public virtual float? BootCap { get; set; }
        // Van member Getters
        public virtual float? GetLoadCap() { return null; }
        public virtual string? GetLWH() { return null; }
        public virtual float? GetVolume() { return null; }
        // Motorcycle member Getters
        public virtual int? GetCC() { return null; }
        public virtual bool? GetStorage() { return null; }
        public virtual bool? GetWProtect() { return null; }

        // Base Setters
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
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Warning)}{e.Message}");
                return false;
            }
            catch (Exception e) 
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Error)}{e.Message}");
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
                    Errors err = new Errors();
                    errorList.Add($"{err.GetColor(ErrorType.Info)}'{r}' is Invalid Input");
                    return false;
                }
            }
            catch (FormatException ex) 
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Warning)}{ex.Message}");
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception e)
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Error)}{e.Message}");
                return false;
            }
        } // Converts and validates the Daily Rate input.
        public bool SetTransmission(string tr)
        {
            string[] a = { "a", "automatic", "auto"};
            string[] m = { "m", "manual", "man" };
            if (a.Contains(tr.ToLower()))
            {
                Transmission = "Automatic";
                return true;
            }
            else if (m.Contains(tr.ToLower()))
            {
                Transmission = "Manual";
                return true;
            }
            else
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Info)}'{tr}' is Invalid Input");
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
                else 
                {
                    Errors err = new Errors();
                    errorList.Add($"{err.GetColor(ErrorType.Info)}'{seatNum}' is Invalid Input");
                    return false; 
                }
            }
            catch (FormatException e)
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Warning)}{e.Message}");
                return false; 
            }
            catch (Exception e)
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Error)}{e.Message}");
                return false;
            }
        } // Converts and validates the Seat Capacity input.
        public bool SetFuelType(string fuelType)
        {
            if (fuelType == "d" || fuelType == "disesl")
            {
                FuelType = "Diesel";
                return true;
            }
            else if (fuelType == "p" || fuelType == "petrol")
            {
                FuelType = "Petrol";
                return true;
            }
            else if (fuelType == "e" || fuelType == "electric")
            {
                FuelType = "Electric";
                return true;
            }
            else if (fuelType == "h" || fuelType == "hybrid")
            {
                FuelType = "Hybrid";
                return true;
            }
            else
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Info)}'{fuelType}' is Invalid Input");
                return false;
            }
        } // Validates the Fuel type input.
        
        public string UpdateStatus
        {
            get { return Status; }
            set { Status = value; }
        } // Gets and Sets - Doesn't need validation. 
        public abstract string ToFile();
    }
}
