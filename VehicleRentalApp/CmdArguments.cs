using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class CmdArguments
    {
        public Validation validate = new Validation();
        public void CmdRentVehicle(string[] input)
        {
            string? errorMsg;
            int? checkInt;
            int validVehicleID = 0;
            int validUserID = 0;

            validate.CmdValidInt(input[0], out errorMsg, out checkInt);
            if (errorMsg != null)
            {
                Console.WriteLine(errorMsg);
                errorMsg = null;
                return;
            }
            else if (checkInt != null)
            {
                validVehicleID = (int)checkInt;
                checkInt = null;
                if (!Program.vehicles.ContainsKey(validVehicleID))
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Info, $"Vehicle '{validVehicleID}' does not exist.");
                    return;
                }
                else if (Program.vehicles[validVehicleID].Status == "Rented")
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Info, $"Vehicle '{validVehicleID}' is not available.");
                    return;
                }
            }

            string[] getID = input[1].Split('=');
            validate.CmdValidInt(getID[0], out errorMsg, out checkInt);
            if (errorMsg != null)
            {
                Console.WriteLine(errorMsg);
                return;
            }
            else if (checkInt != null)
            {
                validUserID = (int)checkInt;
                bool validPassword = Program.users[validUserID].GetPassword() == getID[1];
                if (Program.users.ContainsKey(validUserID) && validPassword)
                {
                    Program.vehicles[validVehicleID].Status = "Available";
                    Program.users[validUserID].UserRentVehicle(validVehicleID);
                    Program.SerializeDictionary();
                    Program.WriteBinary();
                    Console.WriteLine($"Rented: {Program.vehicles[validVehicleID].ConfirmDetails()}");
                    return;
                }
                else
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Warning, $"Cannot find user: '{validUserID}'");
                    if (!validPassword)
                    {
                        err.PrintError(ErrorType.Error, $"Incorrect password for user '{validUserID}'");
                    }
                    return;
                }
            }

        }
        public void CmdReturnVehicle(string[] input)
        {
            string? errorMsg;
            int? checkInt;
            int validVehicleID = 0;
            int validUserID = 0;

            validate.CmdValidInt(input[0], out errorMsg, out checkInt);
            if (errorMsg != null)
            {
                Console.WriteLine(errorMsg);
                errorMsg = null;
                return;
            }
            else if (checkInt != null)
            {
                validVehicleID = (int)checkInt;
                checkInt = null;
                if (!Program.vehicles.ContainsKey(validVehicleID))
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Info, $"Vehicle '{validVehicleID}' does not exist.");
                    return;
                }
                else if (Program.vehicles[validVehicleID].Status == "Available")
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Info, $"Vehicle '{validVehicleID}' is already available.");
                    return;
                }
            }

            string[] getID = input[1].Split('=');
            validate.CmdValidInt(getID[0], out errorMsg, out checkInt);
            if (errorMsg != null)
            {
                Console.WriteLine(errorMsg);
                return;
            }
            else if (checkInt != null)
            {
                validUserID = (int)checkInt;
                bool validPassword = Program.users[validUserID].GetPassword() == getID[1];
                if (Program.users.ContainsKey(validUserID) && validPassword)
                {
                    Program.vehicles[validVehicleID].Status = "Available";
                    Program.users[validUserID].UserReturnVehicle(validVehicleID);
                    Program.SerializeDictionary();
                    Program.WriteBinary();
                    Console.WriteLine($"Returned: {Program.vehicles[validVehicleID].ConfirmDetails()}");
                    return;
                }
                else
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Warning, $"Cannot find user: '{validUserID}'");
                    if (!validPassword)
                    {
                        err.PrintError(ErrorType.Error, $"Incorrect password for user '{validUserID}'");
                    }
                    return;
                }
            }

        }
        public void CmdDelVehicle(string[] input)
        {
            string? errorMsg;
            int? checkInt;
            int validVehicleID = 0;
            int validUserID = 0;

            validate.CmdValidInt(input[0], out errorMsg, out checkInt);
            if (errorMsg != null)
            {
                Console.WriteLine(errorMsg);
                errorMsg = null;
                return;
            }
            else if (checkInt != null)
            {
                validVehicleID = (int)checkInt;
                checkInt = null;
                if (!Program.vehicles.ContainsKey(validVehicleID))
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Info, $"Vehicle '{validVehicleID}' does not exist.");
                    return;
                }
            }

            string[] getID = input[1].Split('=');
            validate.CmdValidInt(getID[0], out errorMsg, out checkInt);
            if (errorMsg != null)
            {
                Console.WriteLine(errorMsg);
                return;
            }
            else if (checkInt != null && errorMsg == null)
            {
                validUserID = (int)checkInt;
                Users owner = Program.users[validUserID];
                bool validPassword = Program.users[validUserID].GetPassword() == getID[1];
                if (Program.users.ContainsKey(validUserID) && validPassword)
                {
                    if (owner.CheckOwnVehicles(validVehicleID))
                    {
                        owner.UserDelVehicle(validVehicleID);
                        Program.vehicles.Remove(validVehicleID);
                        Program.SerializeDictionary();
                        Program.WriteBinary();
                        Console.WriteLine($"Deleted: {Program.vehicles[validVehicleID].ConfirmDetails()}");
                        return;
                    }
                    else
                    {
                        Errors err = new Errors();
                        err.PrintError(ErrorType.Warning, $"Can not delete a vehicle you do not own.");
                        return;
                    }
                }
                else
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Warning, $"Cannot find user: '{validUserID}'");
                    if (!validPassword)
                    {
                        err.PrintError(ErrorType.Error, $"Incorrect password for user '{validUserID}'");
                    }
                    return;
                }
            }
        }
        public void CmdAddVehicle(string[] newV)
        {
            List<string> newVehicle = newV.Select(n => n.Replace('/', ' ')).ToList();
            List<string> errorOutput = new List<string>();

            int newKey = Program.vehicles.Count() == 0 ? 1 : Program.vehicles.Keys.Max() + 1;
            Users owner = Program.userCache.Values.FirstOrDefault();
            int ownerId = 0;
            if (owner == null)
            {
                Console.WriteLine("User must be logged in");
                Thread.Sleep(1000);
                Program.menu.GetBeforeLogin();
            }
            else
            {
                ownerId = owner.GetUserID();
            }

            string? errorMsg;
            int? checkInt;
            decimal? checkDecimal;
            string? checkFuelType;
            string? checkTransmision;
            int validYear = 0;
            int validSeatNum = 0;
            decimal validRate = 0m;
            string validFuelType = "";
            string validTransmission = "";

            validate.CmdValidInt(newVehicle[3], out errorMsg, out checkInt);
            if (errorMsg != null) errorOutput.Add(errorMsg);
            else if (checkInt != null)
            {
                if (checkInt.Value >= DateTime.Now.Year - 30 && checkInt.Value <= DateTime.Now.Year)
                {
                    validYear = checkInt.Value;
                }
                else
                {
                    Errors err = new Errors();
                    errorOutput.Add(err.PrintErrorString(ErrorType.Info) + "Invalid Year");
                }
            }

            validate.CmdValidInt(newVehicle[6], out errorMsg, out checkInt);
            if (errorMsg != null) errorOutput.Add(errorMsg);
            else if (checkInt != null) validSeatNum = checkInt.Value;

            validate.CmdValidDecimal(newVehicle[4], out errorMsg, out checkDecimal);
            if (errorMsg != null) errorOutput.Add(errorMsg);
            else if (checkDecimal != null) validRate = checkDecimal.Value;

            validate.CmdCheckFuel(newVehicle[7], out errorMsg, out checkFuelType);
            if (errorMsg != null) errorOutput.Add(errorMsg);
            else if (checkFuelType != null) validFuelType = checkFuelType;

            validate.CmdCheckFuel(newVehicle[5], out errorMsg, out checkTransmision);
            if (errorMsg != null) errorOutput.Add(errorMsg);
            else if (checkTransmision != null) validTransmission = checkTransmision;

            if (newVehicle[0].ToUpper() == "C" && newVehicle.Count() == 9)
            {
                int validBootCap = 0;
                validate.CmdValidInt(newVehicle[8], out errorMsg, out checkInt);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkInt != null) validBootCap = checkInt.Value;

                if (errorOutput.Count == 0)
                {
                    Car cmdCar = new Car(ownerId, newVehicle[1], newVehicle[2], validYear, validRate, validTransmission, validSeatNum, validFuelType, validBootCap);
                    Program.vehicles.Add(newKey, cmdCar);
                    Console.WriteLine($"Car Added - {cmdCar.ConfirmDetails()}");
                    Program.SerializeDictionary();
                }
                else
                {
                    Console.WriteLine($"New car fail");
                    foreach (string error in errorOutput)
                    {
                        Console.WriteLine(error);
                        Console.ResetColor();
                    }
                }
            }
            else if (newVehicle[0].ToUpper() == "V" && newVehicle.Count() == 12)
            {
                float? checkFloat;
                float validLoadCap = 0f;
                validate.CmdCheckFloat(newVehicle[8], out errorMsg, out checkFloat);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkFloat != null) validLoadCap = checkFloat.Value;

                float validLength = 0f;
                validate.CmdCheckFloat(newVehicle[9], out errorMsg, out checkFloat);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkFloat != null) validLength = checkFloat.Value;

                float validWidth = 0f;
                validate.CmdCheckFloat(newVehicle[10], out errorMsg, out checkFloat);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkFloat != null) validWidth = checkFloat.Value;

                float validHeight = 0f;
                validate.CmdCheckFloat(newVehicle[11], out errorMsg, out checkFloat);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkFloat != null) validHeight = checkFloat.Value;

                if (errorOutput.Count == 0)
                {
                    Van cmdVan = new Van(ownerId, newVehicle[1], newVehicle[2], validYear, validRate, validTransmission, validSeatNum, validFuelType, validLoadCap, validLength, validWidth, validHeight);
                    Program.vehicles.Add(newKey, cmdVan);
                    Console.WriteLine($"Van Added - {cmdVan.ConfirmDetails()}");
                    Program.SerializeDictionary();
                }
                else
                {
                    foreach (string error in errorOutput)
                    {
                        Console.WriteLine(error);
                        Console.ResetColor();
                    }
                    return;
                }
            }
            else if (newVehicle[0].ToUpper() == "M" && newVehicle.Count() == 11)
            {
                int validCC = 0;
                validate.CmdValidInt(newVehicle[8], out errorMsg, out checkInt);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkInt != null) validCC = checkInt.Value;

                bool? checkBool;
                bool validStorage = false;
                validate.CmdCheckBool(newVehicle[9].ToLower(), out errorMsg, out checkBool);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkBool != null) validStorage = checkBool.Value;

                bool validProtection = false;
                validate.CmdCheckBool(newVehicle[10].ToLower(), out errorMsg, out checkBool);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkBool != null) validProtection = checkBool.Value;

                if (errorOutput.Count == 0)
                {
                    Motorcycle cmdMotor = new Motorcycle(ownerId, newVehicle[1], newVehicle[2], validYear, validRate, validTransmission, validSeatNum, validFuelType, validCC, validStorage, validProtection);
                    Program.vehicles.Add(newKey, cmdMotor);
                    Console.WriteLine($"Motorcycle Added - {cmdMotor.ConfirmDetails()}");
                    Program.SerializeDictionary();
                }
                else
                {
                    foreach (string error in errorOutput)
                    {
                        Console.WriteLine(error);
                        Console.ResetColor();
                    }
                    return;
                }
            }
            else
            {
                Console.WriteLine($"Unknown vehicle type: '{newVehicle[0]}' or Incorrect number of inputs");
                return;
            }
        }
    }
}
