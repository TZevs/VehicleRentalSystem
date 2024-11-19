using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    public class Motorcycle : Vehicle
    {
        private int _CC;
        private bool _Storage;
        private bool _WithProtection;

        [JsonConstructor]
        public Motorcycle() { }
        public Motorcycle(string make, string model, int yr, decimal rate, string trans, int numSeats, string fuel, int cc, bool Storage, bool WithProtection)
        {
            _Make = make;
            _Model = model;
            _Year = yr;
            _DailyRate = rate;
            _Transmission = trans;
            _SeatCapacity = numSeats;
            _FuelType = fuel;
            _CC = cc;
            _Storage = Storage;
            _WithProtection = WithProtection;
            Status = "Available";
            SetType();
        }
        public override void SetType() { _TypeOfVehicle = "Motorcycle"; }
        [JsonInclude]
        public override int? CC
        {
            get { return _CC; }
            set { _CC = value ?? 0; }
        }
        [JsonInclude]
        public override bool? Storage
        {
            get { return _Storage; }
            set { _Storage = value ?? false; }
        }
        [JsonInclude]
        public override bool? WithProtection
        {
            get { return _WithProtection; }
            set { _WithProtection = value ?? false; }
        }
        public override string ToFile()
        {
            return $"{_TypeOfVehicle}, {_Make}, {_Model}, {_Year}, {_DailyRate}, {_Transmission}, {_SeatCapacity}, {_FuelType}, {_Status}, {_CC}, {_Storage}, {_WithProtection}";
        }
    }
}
