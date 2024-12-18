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
        public override int? GetCC() { return _CC; }
        public override bool? GetStorage() { return _Storage; }
        public override bool? GetWithProtection() { return _WithProtection; }
        
        public override string ConfirmDetails()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, " +
                $"{Transmission}, {SeatCapacity}, {FuelType}, {Status}, {_CC}, " +
                $"{_Storage}, {_WithProtection}";
        }

        // Defining reading and writing abstract methods for Motorcycle objects in the binary file.
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
        public override void AppendVehicles(int id)
        {
            using (FileStream fs = new FileStream("vBinary.bin", FileMode.Append, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
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
