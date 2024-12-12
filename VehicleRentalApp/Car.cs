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
            set { _BootCapacity = (int)value; }
        }
        public override void SetVType() { TypeOfVehicle = "Car"; }
        public override string ConfirmDetails()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {BootCapacity}";
        }

        public override void ToBinFile()
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open("VehicleBinary.bin", FileMode.OpenOrCreate)))
            {
                Parallel.ForEach(Program.vehicles, veh =>
                {
                    bw.Write(veh.Value.GetVType());
                    bw.Write(veh.Value.GetOwnerID());
                    bw.Write(veh.Value.GetMake());
                    bw.Write(veh.Value.GetModel());
                    bw.Write(veh.Value.GetYear());
                    bw.Write(veh.Value.GetRate());
                    bw.Write(veh.Value.GetTransmission());
                    bw.Write(veh.Value.GetSeatCap());
                    bw.Write(veh.Value.GetFuel());
                    bw.Write((int)veh.Value.BootCapacity);
                    bw.Write(veh.Value.Status);
                });
            }
        }
    }
}
