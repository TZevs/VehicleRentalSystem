﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
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
        public override float? LoadCapacity
        {
            get { return _LoadCapacity; }
            set { _LoadCapacity = value ?? 0; }
        }
        public override float? IntLength
        {
            get { return _IntLength; }
            set { _IntLength = value ?? 0; }
        }
        public override float? IntWidth
        {
            get { return _IntWidth; }
            set { _IntWidth = value ?? 0; }
        }
        public override float? IntHeight
        {
            get { return _IntHeight; }
            set { _IntHeight = value ?? 0; }
        }
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
        public override void WritingVehicles(BinaryWriter bw, int id)
        {
            bw.Write(TypeOfVehicle);
            bw.Write(id);
            bw.Write(OwnerID);
            bw.Write(Make);
            bw.Write(Model);
            bw.Write(Year);
            bw.Write(DailyRate);
            bw.Write(Transmission);
            bw.Write(SeatCapacity);
            bw.Write(FuelType);
            bw.Write(_LoadCapacity);
            bw.Write(_IntLength);
            bw.Write(_IntWidth);
            bw.Write(_IntHeight);
            bw.Write(_Volume);
            bw.Write(_Status);
        }
        public override void ReadingVehicles(BinaryReader br)
        {
            SetVType();
            this.OwnerID = br.ReadInt32();
            this.Make = br.ReadString();
            this.Model = br.ReadString();
            this.Year = br.ReadInt32();
            this.DailyRate = br.ReadDecimal();
            this.Transmission = br.ReadString();
            this.SeatCapacity = br.ReadInt32();
            this.FuelType = br.ReadString();
            this._LoadCapacity = br.ReadSingle();
            this._IntLength = br.ReadSingle();
            this._IntWidth = br.ReadSingle();
            this._IntHeight = br.ReadSingle();
            this._Volume = br.ReadSingle();
            this._Status = br.ReadString();
        }
    }
}
