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
        private int CC;
        private bool Storage;
        private bool WithProtection;

        public Motorcycle(string make, string model, string yr, string rate, string trans, string numSeats, string fuel, string cc, string stor, string wPro)
        {
            Make = make;
            Model = model;
            if (SetYear(yr) && SetRate(rate) && SetTransmission(trans) && SetSeatCap(numSeats) && SetFuelType(fuel))
            {
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
            Status = "Available";
            SetStorage(stor);
            SetProtection(wPro);
            SetType();
        }
        public override void SetType() { TypeOfVehicle = "Motorcycle"; }
        public bool SetCC(string cc)
        {
            int c = 0;
            try
            {
                c = int.Parse(cc);
                if (c >= 50)
                {
                    CC = c;
                    return true;
                }
                else 
                {
                    Errors err = new Errors();
                    errorList.Add($"{err.GetColor(ErrorType.Info)}'{cc}' is Invalid Input");
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
        public void SetStorage(string stor)
        {
            if (stor == "yes" || stor == "y") 
            {
                Storage = true;
            }
            else if (stor == "no" || stor == "n") 
            { 
                Storage = false;
            }
            else 
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Info)}'{stor}' is Invalid Input");
            }
        }
        public void SetProtection(string wPro)
        {
            if (wPro == "yes" || wPro == "y")
            {
                WithProtection = true;
            }
            else if (wPro == "no" || wPro == "n")
            {
                WithProtection = false;
            }
            else 
            {
                Errors err = new Errors();
                errorList.Add($"{err.GetColor(ErrorType.Info)}Invalid Input");
            }
        }

        public override int? GetCC() { return CC; }
        public override bool? GetStorage() { return Storage; }
        public override bool? GetWProtect() { return WithProtection; }

        public override string ToFile()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {CC}, {Storage}, {WithProtection}";
        }
    }
}
