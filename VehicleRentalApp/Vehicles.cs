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
        // Protected member variables so that the derived classes have access.
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
        
        // No constructor as abstract classes do not intialise objects. 

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

        // These are nullable as I want the default implementation to be nullable if the function is not defined.
        // Car member Getters
        public virtual int? GetBootCap() { return null; }
        // Van member Getters
        public virtual float? GetLoadCap() { return null; }
        public virtual float? GetIntLength() { return null; }
        public virtual float? GetIntWidth() { return null; }
        public virtual float? GetIntHeight() { return null; }
        public virtual float? GetVolume() { return null; }
        public virtual string? GetLWH() { return null; }
        
        // Motorcycle member Getters 
        public virtual int? GetCC() { return null; }
        public virtual bool? GetStorage() { return null; }
        public virtual bool? GetWithProtection() { return null; }

        // Abstract setter method for Vehicle type - Defined in derived classes.
        public abstract void SetVType(); 

        // Uses both get and set properties for the Status variable
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        // Abstract method to output all the variables in an object 
        public abstract string ConfirmDetails();

        // Reading and writing vehicles objects with the binary file. 
        public abstract void WritingVehicles(BinaryWriter bw, int id);
        public abstract void ReadingVehicles(BinaryReader br);
        public abstract void AppendVehicles(int id);
    }
}
