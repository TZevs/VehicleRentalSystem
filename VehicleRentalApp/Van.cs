using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Van : Vehicle
    {
        private float LoadCapacity;
        private float IntLength; 
        private float IntWidth; 
        private float IntHeight; 
        private float Volume;

        public Van(string make, string model, string yr, string rate, string trans, string numSeats, string fuel, string lC, string len, string wid, string hei)
        {
            Make = make;
            Model = model;
            if (SetYear(yr) && SetRate(rate) && SetTransmission(trans) && SetLoadCap(lC) && SetLWH(len, wid, hei) && SetSeatCap(numSeats) && SetFuelType(fuel))
            {
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
            Status = "Available";
            Volume = GSVolume;
            SetType();
        }
        public bool SetLoadCap(string lC)
        {
            float lc = 0;
            try
            {
                lc = float.Parse(lC);
                if (lc > 0)
                {
                    LoadCapacity = lc;
                    return true;
                }
                else { return false; }
            }
            catch { return false; }
        }
        public bool SetLWH(string len, string wid, string hei)
        {
            float l = 0;
            float w = 0;
            float h = 0;
            try
            {
                l = float.Parse(len);
                w = float.Parse(wid);
                h = float.Parse(hei);
                if (l > 0 && w > 0 && h > 0)
                {
                    IntLength = l; IntWidth = w; IntHeight = h;
                    return true;
                }
                else { return false; }
            }
            catch { return false; }
        }
        public string GetLWH()
        {
            return $"{IntLength}m x {IntWidth}m x {IntHeight}m";
        }
        public float GSVolume
        {
            get { return Volume; }
            set
            {
                Volume = IntLength * IntWidth * IntHeight;
            }
        }
        public override void SetType() { TypeOfVehicle = "Van"; }
        public string Type
        {
            get { return TypeOfVehicle; }
            set { TypeOfVehicle = "Van"; }
        }
        public override string ToFile()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {LoadCapacity}, {IntLength}, {IntWidth}, {IntHeight}, {Volume}";
        }
    }
}
