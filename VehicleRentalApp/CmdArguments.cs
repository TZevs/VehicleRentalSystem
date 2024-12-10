using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            var timer = Stopwatch.StartNew();
            List<string> newVehicle = newV.Select(n => n.Replace('/', ' ')).ToList();
            List<string> errorOutput = new List<string>();

            int newKey = Program.vehicles.Count() == 0 ? 1 : Program.vehicles.Keys.Max() + 1;

            string[] user = newVehicle[newVehicle.Count - 1].Split('=');
            int ownerId = Convert.ToInt32(user[0]);

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

            validate.CmdCheckTransmission(newVehicle[5], out errorMsg, out checkTransmision);
            if (errorMsg != null) errorOutput.Add(errorMsg);
            else if (checkTransmision != null) validTransmission = checkTransmision;

            if (newVehicle[0].ToUpper() == "C" && newVehicle.Count() == 10)
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
            else if (newVehicle[0].ToUpper() == "V" && newVehicle.Count() == 13)
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
            else if (newVehicle[0].ToUpper() == "M" && newVehicle.Count() == 12)
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
            Console.WriteLine($"{timer.ElapsedMilliseconds}ms");
        }

        public async Task CmdAddVehicleAsync(string[] newV)
        {
            List<string> newVehicle = newV.Select(n => n.Replace('/', ' ')).ToList();
            List<string> errorOutput = new List<string>();

            int newKey = Program.vehicles.Count() == 0 ? 1 : Program.vehicles.Keys.Max() + 1;
            Users owner = Program.userCache.Values.FirstOrDefault();
            string[] user = newVehicle[newVehicle.Count - 1].Split('=');
            int ownerId = Convert.ToInt32(user[0]);

            Validation v = new Validation();
            var validationTasks = new List<Task<(string? errorMsg, object? result)>>()
            {
                v.CmdValidIntAsync(newVehicle[3]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validNum)),
                v.CmdValidIntAsync(newVehicle[6]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validNum)),
                v.CmdValidDecimalAsync(newVehicle[4]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validNum)),
                v.CmdValidFuelAsync(newVehicle[7]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validFuel)),
                v.CmdValidTransAsync(newVehicle[5]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validTrans))
            };

            if (newVehicle[0].ToUpper() == "C" && newVehicle.Count() == 10)
            {
                validationTasks.Add(v.CmdValidIntAsync(newVehicle[8]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validNum)));   
            }
            else if (newVehicle[0].ToUpper() == "V" && newVehicle.Count() == 13)
            {
                validationTasks.Add(v.CmdValidFloatAsync(newVehicle[8]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validNum)));
                validationTasks.Add(v.CmdValidFloatAsync(newVehicle[9]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validNum)));
                validationTasks.Add(v.CmdValidFloatAsync(newVehicle[10]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validNum)));
                validationTasks.Add(v.CmdValidFloatAsync(newVehicle[11]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validNum)));
            }
            else if (newVehicle[0].ToUpper() == "M" && newVehicle.Count() == 12)
            {
                validationTasks.Add(v.CmdValidIntAsync(newVehicle[8]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validNum)));
                validationTasks.Add(v.CmdValidBoolAsync(newVehicle[9]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validBool)));
                validationTasks.Add(v.CmdValidBoolAsync(newVehicle[10]).ContinueWith(t => (t.Result.errorMsg, (object?)t.Result.validBool)));
            }

            var validationResults = await Task.WhenAll(validationTasks);

            foreach (var result in validationResults)
            {
                if (result.errorMsg != null)
                {
                    errorOutput.Add(result.errorMsg);   
                }
            }

            int validYear = (int?)validationResults[0].result ?? 0;
            int validSeatNum = (int?)validationResults[1].result ?? 0;
            decimal validRate = (decimal?)validationResults[2].result ?? 0m;
            string validFuelType = (string?)validationResults[3].result ?? "";
            string validTransmission = (string?)validationResults[4].result ?? "";

            if (newVehicle[0].ToUpper() == "C" && newVehicle.Count() == 10)
            {
                int validBootCap = (int?)validationResults[0].result ?? 0;
                if (errorOutput.Count() == 0)
                {
                    Car cmdCar = new Car(ownerId, newVehicle[1], newVehicle[2], validYear, validRate, validTransmission, validSeatNum, validFuelType, validBootCap);
                    Program.vehicles.Add(newKey, cmdCar);
                    Program.users[ownerId].UserAddVehicle(newKey);
                    Console.WriteLine($"Car Added - {cmdCar.ConfirmDetails()}");
                    Program.SerializeDictionary();
                    Program.WriteBinary();
                }
            }
            else if (newVehicle[0].ToUpper() == "V" && newVehicle.Count() == 13)
            {
                float validLoadCap = (float?)validationResults[5].result ?? 0f;
                float validLength = (float?)validationResults[6].result ?? 0f;
                float validWidth = (float?)validationResults[7].result ?? 0f;
                float validHeight = (float?)validationResults[8].result ?? 0f;

                if (errorOutput.Count == 0)
                {
                    Van cmdVan = new Van(ownerId, newVehicle[1], newVehicle[2], validYear, validRate, validTransmission, validSeatNum, validFuelType, validLoadCap, validLength, validWidth, validHeight);
                    Program.vehicles.Add(newKey, cmdVan);
                    Program.users[ownerId].UserAddVehicle(newKey);
                    Console.WriteLine($"Van Added - {cmdVan.ConfirmDetails()}");
                    Program.SerializeDictionary();
                    Program.WriteBinary();
                }
            }
            else if (newVehicle[0].ToUpper() == "M" && newVehicle.Count() == 12)
            {
                int validCC = (int?)validationResults[5].result ?? 0;
                bool validStorage = (bool?)validationResults[6].result ?? false;
                bool validProtection = (bool?)validationResults[7].result ?? false;

                if (errorOutput.Count() == 0)
                {
                    Motorcycle cmdMotor = new Motorcycle(ownerId, newVehicle[1], newVehicle[2], validYear, validRate, validTransmission, validSeatNum, validFuelType, validCC, validStorage, validProtection);
                    Program.vehicles.Add(newKey, cmdMotor);
                    Program.users[ownerId].UserAddVehicle(newKey);
                    Console.WriteLine($"Van Added - {cmdMotor.ConfirmDetails()}");
                    Program.SerializeDictionary();
                    Program.WriteBinary();
                }
            }
            else
            {
                foreach (string error in errorOutput)
                {
                    Console.WriteLine(error);   
                }
            }
        }
    }
}
