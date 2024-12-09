﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VehicleRentalApp
{
    internal class Van : Vehicle
    {
        private float _LoadCapacity;
        private float _IntLength;
        private float _IntWidth;
        private float _IntHeight;
        private float _Volume;

        [JsonConstructor]
        public Van() { }
        public Van(int ownerId, string make, string model, int yr, decimal rate, string trans, int numSeats, string fuel, float lC, float len, float wid, float hei)
        {
            OwnerID = ownerId;
            Make = make;
            Model = model;
            Year = yr;
            DailyRate = rate;
            Transmission = trans;
            SeatCapacity = numSeats;
            FuelType = fuel;
            _LoadCapacity = lC;
            _IntLength = len;
            _IntWidth = wid;
            _IntHeight = hei;
            _Volume = _IntLength * _IntWidth * _IntHeight;
            Status = "Available";
            SetVType();
        }
        public override void SetVType() { TypeOfVehicle = "Van"; }
        [JsonInclude]
        public override float? LoadCapacity
        {
            get { return _LoadCapacity; }
            set { _LoadCapacity = value ?? 0; }
        }
        [JsonInclude]
        public override float? IntLength
        {
            get { return _IntLength; }
            set { _IntLength = value ?? 0; }
        }
        [JsonInclude]
        public override float? IntWidth
        {
            get { return _IntWidth; }
            set { _IntWidth = value ?? 0; }
        }
        [JsonInclude]
        public override float? IntHeight
        {
            get { return _IntHeight; }
            set { _IntHeight = value ?? 0; }
        }
        [JsonInclude]
        public override float? Volume
        {
            get { return (float)Math.Round(_Volume, 2); }
            set { _Volume = value ?? 0; }
        }
        public override string? GetLWH() { return $"{IntLength}m x {IntWidth}m x {IntHeight}m"; }
        public override string ConfirmDetails()
        {
            return $"{TypeOfVehicle}, {Make}, {Model}, {Year}, {DailyRate}, {Transmission}, {SeatCapacity}, {FuelType}, {Status}, {LoadCapacity}, {IntLength}, {IntWidth}, {IntHeight}, {Volume}";
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
                    bw.Write((float)veh.Value.LoadCapacity);
                    bw.Write((float)veh.Value.IntLength);
                    bw.Write((float)veh.Value.IntWidth);
                    bw.Write((float)veh.Value.IntHeight);
                    bw.Write((float)veh.Value.Volume);
                    bw.Write(veh.Value.Status);
                });
            }
        }
    }
}
