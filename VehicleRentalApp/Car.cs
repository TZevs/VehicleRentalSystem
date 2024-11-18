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
        // Shall I change to int ? 
        // Use kg - not something that allways appears on rental apps.
        private float BootCapacity;

        public Car(string make, string model, string yr, string rate, string trans, string numSeats, string fuel, string bC)
        {
            Make = make;
            Model = model;
            //if (SetYear(yr) && SetRate(rate) && SetTransmission(trans) && SetBootCap(bC) && SetSeatCap(numSeats) && SetFuelType(fuel))
            //{
            //    IsValid = true;
            //}
            //else
            //{
            //    IsValid = false;
            //}
            Status = "Available";
            SetType();
        }
        [JsonInclude]
        public override float? BootCap
        {
            get { return BootCapacity; }
            set { BootCapacity = value ?? 0; }
        }
        public void SetBootCap(string bC)
        {
            float bc = 0;
            try
            {
                bc = float.Parse(bC);
                if (bc >= 0)
                {
                    BootCapacity = bc;
                }
                else
                {
                    Errors err = new Errors();
                    errorList.Add($"{err.GetColor(ErrorType.Info)}'{bC}' is Invalid Input");
                }
            }
            catch (FormatException e)
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Warning)}{e.Message}");
            }
            catch (Exception e)
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Error)}{e.Message}");
            }
        }
        public override void SetType() { TypeOfVehicle = "Car"; }
        public override string ToFile()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {BootCapacity}";
        }
    }
}
