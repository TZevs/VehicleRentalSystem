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
        private int BootCapacity;

        public Car(string make, string model, int yr, decimal rate, string trans, int numSeats, string fuel, int bC)
        {
            Make = make;
            Model = model;
            Year = yr;
            DailyRate = rate;
            Transmission = trans;
            SeatCapacity = numSeats;
            FuelType = fuel;
            BootCapacity = bC;
            Status = "Available";
            SetType();
        }
        [JsonInclude]
        public override int? BootCap
        {
            get { return BootCapacity; }
            set { BootCapacity = value ?? 0; }
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
        public override string ToFile()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {BootCapacity}";
        }
    }
}
