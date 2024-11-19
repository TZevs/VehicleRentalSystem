using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    public class Car : Vehicle
    {
        // Shall I change to int ? 
        // Use kg - not something that allways appears on rental apps.
        private int _BootCapacity;

        [JsonConstructor]
        public Car() { }
        public Car(string make, string model, int yr, decimal rate, string trans, int numSeats, string fuel, int bC)
        {
            _Make = make;
            _Model = model;
            _Year = yr;
            _DailyRate = rate;
            _Transmission = trans;
            _SeatCapacity = numSeats;
            _FuelType = fuel;
            _BootCapacity = bC;
            Status = "Available";
            SetType();
        }
        [JsonInclude]
        public override int? BootCapacity
        {
            get { return _BootCapacity; }
            set { _BootCapacity = value ?? 0; }
        }
        public override void SetType() { _TypeOfVehicle = "Car"; }
        public override string ToFile()
        {
            return $"{_TypeOfVehicle}, {_Make}, {_Model}, {_Year}, {_DailyRate}, {_Transmission}, {_SeatCapacity}, {_FuelType}, {Status}, {BootCapacity}";
        }
    }
}
