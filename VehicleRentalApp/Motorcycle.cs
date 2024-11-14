using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                else { return false; }
            }
            catch { return false; }
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
            else { Storage = false; }
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
            else { WithProtection = false; }
        }
        public void SetType() { TypeOfVehicle = "Motorcycle"; }
        public override string ToFile()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {CC}, {Storage}, {WithProtection}";
        }
    }
}
