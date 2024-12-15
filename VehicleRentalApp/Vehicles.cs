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
        protected string TypeOfVehicle;
        protected int OwnerID;
        protected string Make;
        protected string Model;
        protected int Year;
        protected decimal DailyRate;
        protected string Transmission;
        protected string _Status;
        protected int SeatCapacity;
        protected string FuelType;
        
        // Base Getters
        public string GetVType() { return TypeOfVehicle; }
        public int GetOwnerID() { return OwnerID; }
        public string GetMake() { return Make; }
        public string GetModel () { return Model; }
        public int GetYear() { return Year; }
        public decimal GetRate() { return DailyRate; }
        public string GetTransmission() { return Transmission; }
        public int GetSeatCap() { return SeatCapacity; }
        public string GetFuel() { return FuelType; }

        // Car member Getters / Setters
        public virtual int? GetBootCap() { return null; }
        // Van member Getters / Setters
        public virtual float? GetLoadCap() { return null; }
        public virtual float? GetIntLength() { return null; }
        public virtual float? GetIntWidth() { return null; }
        public virtual float? GetIntHeight() { return null; }
        public virtual float? GetVolume() { return null; }
        public virtual string? GetLWH() { return null; }
        
        // Motorcycle member Getters / Setters
        public virtual int? GetCC() { return null; }
        public virtual bool? GetStorage() { return null; }
        public virtual bool? GetWithProtection() { return null; }

        // Base Setters
        public abstract void SetVType(); 
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public abstract string ConfirmDetails();

        public abstract void WritingVehicles(BinaryWriter bw, int id);
        public abstract void ReadingVehicles(BinaryReader br);
        public abstract void AppendVehicles(int id);
    }
}
