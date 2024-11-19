using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Van : Vehicle
    {
        [JsonInclude] private float LoadCapacity;
        [JsonInclude] private float IntLength;
        [JsonInclude] private float IntWidth;
        [JsonInclude] private float IntHeight;
        [JsonInclude] private float Volume;

        public Van(string make, string model, int yr, decimal rate, string trans, int numSeats, string fuel, float lC, float len, float wid, float hei)
        {
            Make = make;
            Model = model;
            Year = yr;
            DailyRate = rate;
            Transmission = trans;
            SeatCapacity = numSeats;
            FuelType = fuel;
            LoadCapacity = lC;
            IntLength = len;
            IntWidth = wid;
            IntHeight = hei;
            Volume = IntLength * IntWidth * IntHeight;
            Status = "Available";
            SetType();
        }
        public override void SetType() { TypeOfVehicle = "Van"; }
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
                else 
                {
                    Errors err = new Errors();
                    errorList.Add($"{err.GetColor(ErrorType.Info)}'{lC}' is Invalid Input");
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
                else
                {
                    Errors err = new Errors();
                    errorList.Add($"{err.GetColor(ErrorType.Info)}'{l} or {w} or {h}' is Invalid Input");
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
        public float SetVolume() { return IntLength * IntWidth * IntHeight; }

        public virtual float? GetLoadCap() { return LoadCapacity; }
        public override string? GetLWH() { return $"{IntLength}m x {IntWidth}m x {IntHeight}m"; }
        public override float? GetVolume() { return Volume; }

        public override string ToFile()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {LoadCapacity}, {IntLength}, {IntWidth}, {IntHeight}, {Volume}";
        }
    }
}
