using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Car : Vehicle
    {
        private int _BootCapacity;

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
        public override int? BootCapacity
        {
            get { return _BootCapacity; }
            set { _BootCapacity = (int)value; }
        }
        public override void SetVType() { TypeOfVehicle = "Car"; }
        public override string ConfirmDetails()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {BootCapacity}";
        }

        public override void WritingVehicles(BinaryWriter bw, int id)
        {
            bw.Write(TypeOfVehicle);
            bw.Write(id);
            bw.Write(OwnerID);
            bw.Write(Make);
            bw.Write(Model);
            bw.Write(Year);
            bw.Write(DailyRate);
            bw.Write(Transmission);
            bw.Write(SeatCapacity);
            bw.Write(FuelType);
            bw.Write(_BootCapacity);
            bw.Write(_Status);
        }
        public override void ReadingVehicles(BinaryReader br)
        {
            SetVType();
            this.OwnerID = br.ReadInt32();
            this.Make = br.ReadString();
            this.Model = br.ReadString();
            this.Year = br.ReadInt32();
            this.DailyRate = br.ReadDecimal();
            this.Transmission = br.ReadString();
            this.SeatCapacity = br.ReadInt32();
            this.FuelType = br.ReadString();
            this._BootCapacity = br.ReadInt32();
            this._Status = br.ReadString();
        }
    }
}
