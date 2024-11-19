using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    public abstract class Vehicle
    {
        [JsonInclude] protected string TypeOfVehicle;
        [JsonInclude] protected string Make;
        [JsonInclude] protected string Model;
        [JsonInclude] protected int Year;
        [JsonInclude] protected decimal DailyRate;
        [JsonInclude] protected string Transmission;
        protected string _Status;
        [JsonInclude] protected int SeatCapacity;
        [JsonInclude] protected string FuelType;
        
        // Base Getters
        public string GetVType() { return TypeOfVehicle; }
        public string GetMake() { return Make; }
        public string GetModel () { return Model; }
        public int GetYear() { return Year; }
        public decimal GetRate() { return DailyRate; }
        public string GetTransmission() { return Transmission; }
        public int GetSeatCap() { return SeatCapacity; }
        public string GetFuel() { return FuelType; }

        // Car member Getters / Setters
        public virtual int? BootCapacity { get; set; }
        // Van member Getters
        public virtual float? LoadCapacity { get; set; }
        public virtual float? IntLength { get; set; } 
        public virtual float? IntWidth { get; set; } 
        public virtual float? IntHeight { get; set; } 
        public virtual float? Volume { get; set; }
        public virtual string? GetLWH() { return null; }
        
        // Motorcycle member Getters / Setters
        public virtual int? CC { get; set; }
        public virtual bool? Storage { get; set; }
        public virtual bool? WithProtection { get; set; }

        // Base Setters
        public abstract void SetVType(); 
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public abstract string ToFile();
    }
}
