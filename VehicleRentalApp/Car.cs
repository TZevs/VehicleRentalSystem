using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Car : Vehicle
    {
        // Shall I change to int ? 
        // Use kg - not something that allways appears on rental apps.
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
                else
                {
                    Errors err = new Errors();
                    errorList.Add($"{err.GetColor(ErrorType.Info)}'{bC}' is Invalid Input");
                    return false;
                }
            }
            catch (FormatException e)
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Warning)}{e.Message}");
                return false;
            }
            catch (Exception e)
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Error)}{e.Message}");
                return false;
            }
        }
        public override void SetType() { TypeOfVehicle = "Car"; }
        
        public override float? GetBootCap() { return BootCapacity; }

        public override string ToFile()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {BootCapacity}";
        }
    }
}
