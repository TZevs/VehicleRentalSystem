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
        [JsonInclude] protected string _TypeOfVehicle;
        [JsonInclude] protected string _Make;
        [JsonInclude] protected string _Model;
        [JsonInclude] protected int _Year;
        [JsonInclude] protected decimal _DailyRate;
        [JsonInclude] protected string _Transmission;
        protected string _Status;
        [JsonInclude] protected int _SeatCapacity;
        [JsonInclude] protected string _FuelType;
        
        // Base Getters
        public string GetVType() { return _TypeOfVehicle; }
        public string GetMake() { return _Make; }
        public string GetModel () { return _Model; }
        public int GetYear() { return _Year; }
        public decimal GetRate() { return _DailyRate; }
        public string GetTransmission() { return _Transmission; }
        public int GetSeatCap() { return _SeatCapacity; }
        public string GetFuel() { return _FuelType; }

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
        public abstract void SetType(); 
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public abstract string ToFile();
    }
}
