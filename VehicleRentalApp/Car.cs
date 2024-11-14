using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Car : Vehicle
    {
        private float BootCapacity;

        public Car(string make, string model, string yr, string rate, string trans, string numSeats, string fuel, string bC)
        {
            Make = make;
            Model = model;
            if (SetYear(yr) && SetRate(rate) && SetTransmission(trans) && SetBootCap(bC) && SetSeatCap(numSeats) && SetFuelType(fuel))
            {
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
            Status = "Available";
            SetType();
        }
        public bool SetBootCap(string bC)
        {
            float bc = 0;
            try
            {
                bc = float.Parse(bC);
                if (bc >= 0)
                {
                    BootCapacity = bc;
                    return true;
                }
                else { return false; }
            }
            catch { return false; }
        }
        public void SetType() { TypeOfVehicle = "Car"; }
        public override string ToFile()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {Status}, {BootCapacity}";
        }
    }
}
