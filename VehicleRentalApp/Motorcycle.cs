using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Motorcycle : Vehicle
    {
        private int _CC;
        private bool _Storage;
        private bool _WithProtection;

        [JsonConstructor]
        public Motorcycle() { }
        public Motorcycle(string make, string model, int yr, decimal rate, string trans, int numSeats, string fuel, int cc, bool Storage, bool WithProtection)
        {
            Make = make;
            Model = model;
            Year = yr;
            DailyRate = rate;
            Transmission = trans;
            SeatCapacity = numSeats;
            FuelType = fuel;
            _CC = cc;
            _Storage = Storage;
            _WithProtection = WithProtection;
            Status = "Available";
            SetVType();
        }
        public override void SetVType() { TypeOfVehicle = "Motorcycle"; }
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
        public override string ConfirmDetails()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {CC}, {Storage}, {WithProtection}";
        }
    }
}
