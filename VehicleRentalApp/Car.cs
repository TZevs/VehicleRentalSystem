using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Car : Vehicle
    {
        private int _BootCapacity;

        [JsonConstructor]
        public Car() { }
        public Car(int ownerId, string make, string model, int yr, decimal rate, string trans, int numSeats, string fuel, int bC)
        {
            OwnerID = ownerId;
            Make = make;
            Model = model;
            Year = yr;
            DailyRate = rate;
            Transmission = trans;
            SeatCapacity = numSeats;
            FuelType = fuel;
            _BootCapacity = bC;
            Status = "Available";
            SetVType();
        }
        [JsonInclude]
        public override int? BootCapacity
        {
            get { return _BootCapacity; }
            set { _BootCapacity = value ?? 0; }
        }
        public override void SetVType() { TypeOfVehicle = "Car"; }
        public override string ConfirmDetails()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {BootCapacity}";
        }
    }
}
