using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Motorcycle : Vehicle
    {
        private int _CC;
        private bool _Storage;
        private bool _WithProtection;

        public Motorcycle() { }
        public Motorcycle(int ownerId, string make, string model, int yr, decimal rate, string trans, int numSeats, string fuel, int cc, bool Storage, bool WithProtection)
        {
            OwnerID = ownerId;
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
        public override int? CC
        {
            get { return _CC; }
            set { _CC = value ?? 0; }
        }
        public override bool? Storage
        {
            get { return _Storage; }
            set { _Storage = value ?? false; }
        }
        public override bool? WithProtection
        {
            get { return _WithProtection; }
            set { _WithProtection = value ?? false; }
        }
        public override string ConfirmDetails()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {CC}, {Storage}, {WithProtection}";
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
            bw.Write(_CC);
            bw.Write(_Storage);
            bw.Write(_WithProtection);
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
            this._CC = br.ReadInt32();
            this._Storage = br.ReadBoolean();
            this._WithProtection = br.ReadBoolean();
            this._Status = br.ReadString();
        }
    }
}
