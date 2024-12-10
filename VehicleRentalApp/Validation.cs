using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Validation
    {
        // AddVehicle function Validation
        public float GetValidFloat(string prompt)
        {
            Errors err = new Errors();
            float input = 0f;
            while (true)
            {
                Console.Write(prompt);
                try
                {
                    input = Convert.ToSingle(Console.ReadLine());
                    if (input <= 0)
                    {
                        err.PrintError(ErrorType.Info, "Invalid Number");
                    }
                    else { break; }
                }
                catch (FormatException e)
                {
                    err.PrintError(ErrorType.Warning, e.Message);
                }
            }
            return input;
        }
        public int GetValidInt(string prompt)
        {
            Errors err = new Errors();
            int input = 0;
            while (true)
            {
                Console.Write(prompt);
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    if (input <= 0)
                    {
                        err.PrintError(ErrorType.Info, "Invalid Input");
                    }
                    else { break; }
                }
                catch (FormatException e)
                {
                    err.PrintError(ErrorType.Warning, e.Message);
                }
            }
            return input;
        }
        public bool GetValidBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToLower();
                if (input == "y" || input == "yes")
                {
                    return true;
                }
                else if (input == "n" || input == "no")
                {
                    return false;
                }
                else
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Info, "Invalid Input. Enter 'y' or 'n'");
                }
            }
        }
        public int GetValidYear(string prompt)
        {
            Errors err = new Errors();
            int input = 0;
            while (true)
            {
                Console.Write(prompt);
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    if (input >= DateTime.Now.Year - 30 && input <= DateTime.Now.Year) { break; }
                    else
                    {
                        err.PrintError(ErrorType.Info, "Invalid Year");
                    }
                }
                catch (FormatException e)
                {
                    err.PrintError(ErrorType.Warning, e.Message);
                }
            }
            return input;
        }
        public string GetValidTransmission(string prompt)
        {
            string[] a = { "a", "automatic", "auto" };
            string[] m = { "m", "manual", "man" };
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToLower();
                if (a.Contains(input))
                {
                    return "Automatic";
                }
                else if (m.Contains(input))
                {
                    return "Manual";
                }
                else
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Info, "Invalid Transmission. Enter 'Automatic' or 'Manual'");
                }
            }
        }
        public string GetValidFuel(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToLower();
                if (input == "d" || input == "diesel")
                {
                    return "Diesel";
                }
                else if (input == "p" || input == "petrol")
                {
                    return "Petrol";
                }
                else if (input == "e" || input == "electric")
                {
                    return "Electric";
                }
                else
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Info, "Invalid Fuel Input. Enter 'Diesel', 'Petrol' or 'Electric'");
                }
            }
        }
        public decimal GetValidDecimal(string prompt)
        {
            Errors err = new Errors();
            decimal input = 0m;
            while (true)
            {
                Console.Write(prompt);
                try
                {
                    input = Convert.ToDecimal(Console.ReadLine());
                    if (input <= 0)
                    {
                        err.PrintError(ErrorType.Info, "Invalid Amount");
                    }
                    else { break; }
                }
                catch (FormatException e)
                {
                    err.PrintError(ErrorType.Warning, e.Message);
                }
            }
            return input;
        }

        // Commmand Argument Validation
        public void CmdValidInt(string cmdInput, out string? errorMsg, out int? validNum)
        {
            int input = 0;
            errorMsg = null;
            validNum = null;
            Errors err = new Errors();

            try
            {
                input = Convert.ToInt32(cmdInput);
                if (input <= 0)
                {
                    errorMsg = err.PrintErrorString(ErrorType.Info) + "Invalid Amount";
                }
                else
                {
                    validNum = input;
                }
            }
            catch (FormatException e)
            {
                errorMsg = err.PrintErrorString(ErrorType.Warning) + e.Message;
            }
        }
        public async Task<(string? errorMsg, int? validNum)> CmdValidIntAsync(string cmdInput)
        {
            int input = 0;
            string? errorMsg = null;
            int? validNum = null;
            Errors err = new Errors();

            try
            {
                await Task.Delay(10);

                input = Convert.ToInt32(cmdInput);
                if (input <= 0)
                {
                    errorMsg = err.PrintErrorString(ErrorType.Info) + "Invalid Input";
                }
                else
                {
                    validNum = input;
                }
            }
            catch (FormatException e)
            {
                errorMsg = err.PrintErrorString(ErrorType.Warning) + e.Message;
            }

            return (errorMsg, validNum);    
        }
        public async Task<(string? errorMsg, decimal? validNum)> CmdValidDecimalAsync(string cmdInput)
        {
            decimal input = 0;
            string? errorMsg = null;
            decimal? validNum = null;
            Errors err = new Errors();

            try
            {
                await Task.Delay(10);

                input = Convert.ToInt32(cmdInput);
                if (input <= 0)
                {
                    errorMsg = err.PrintErrorString(ErrorType.Info) + "Invalid Input";
                }
                else
                {
                    validNum = input;
                }
            }
            catch (FormatException e)
            {
                errorMsg = err.PrintErrorString(ErrorType.Warning) + e.Message;
            }

            return (errorMsg, validNum);
        }
        public async Task<(string? errorMsg, float? validNum)> CmdValidFloatAsync(string cmdInput)
        {
            float input = 0;
            string? errorMsg = null;
            float? validNum = null;
            Errors err = new Errors();

            try
            {
                await Task.Delay(10);

                input = Convert.ToInt32(cmdInput);
                if (input <= 0)
                {
                    errorMsg = err.PrintErrorString(ErrorType.Info) + "Invalid Input";
                }
                else
                {
                    validNum = input;
                }
            }
            catch (FormatException e)
            {
                errorMsg = err.PrintErrorString(ErrorType.Warning) + e.Message;
            }

            return (errorMsg, validNum);
        }
        public async Task<(string? errorMsg, bool? validBool)> CmdValidBoolAsync(string cmdInput)
        {
            string? errorMsg = null;
            bool? validBool = null;

            await Task.Delay(10);

            if (cmdInput == "y" || cmdInput == "yes")
            {
                validBool = true;
            }
            else if (cmdInput == "n" || cmdInput == "no")
            {
                validBool = false;
            }
            else
            {
                Errors err = new Errors();
                errorMsg = err.PrintErrorString(ErrorType.Info) + $"Invalid Input '{cmdInput}': Enter y / yes or n / no";
            }

            return (errorMsg, validBool);
        }
        public async Task<(string? errorMsg, string? validFuel)> CmdValidFuelAsync(string cmdInput)
        {
            string? errorMsg = null;
            string? validFuel = null;

            await Task.Delay(10);

            if (cmdInput == "p" || cmdInput == "petrol")
            {
                validFuel = "Petrol";
            }
            else if (cmdInput == "d" || cmdInput == "diesel")
            {
                validFuel = "Diesel";
            }
            else if (cmdInput == "e" || cmdInput == "electric")
            {
                validFuel = "Electric";
            }
            else
            {
                Errors err = new Errors();
                errorMsg = err.PrintErrorString(ErrorType.Info) + $"Invalid Type '{cmdInput}': [E / Electric] [P / Petrol] [D / Diesel]";
            }

            return (errorMsg, validFuel);
        }
        public async Task<(string? errorMsg, string? validTrans)> CmdValidTransAsync(string cmdInput)
        {
            string? errorMsg = null;
            string? validTrans = null;
            string[] a = { "a", "automatic", "auto" };
            string[] m = { "m", "manual", "man" };

            await Task.Delay(10);

            if (a.Contains(cmdInput))
            {
                validTrans = "Automatic";
            }
            else if (m.Contains(cmdInput))
            {
                validTrans = "Manual";
            }
            else
            {
                Errors err = new Errors();
                errorMsg = err.PrintErrorString(ErrorType.Info) + $"Invalid Transmission '{cmdInput}': [Manual (m / man) / Automatic (a / auto)]";
            }

            return (errorMsg, validTrans);
        }
        public void CmdValidDecimal(string cmdInput, out string? errorMsg, out decimal? validNum)
        {
            decimal input = 0;
            errorMsg = null;
            validNum = null;
            Errors err = new Errors();

            try
            {
                input = Convert.ToDecimal(cmdInput);
                if (input <= 0)
                {
                    errorMsg = err.PrintErrorString(ErrorType.Info) + "Invalid Amount";
                }
                else
                {
                    validNum = input;
                }
            }
            catch (FormatException e)
            {
                errorMsg = err.PrintErrorString(ErrorType.Warning) + e.Message;
            }
        }
        public void CmdCheckFloat(string cmdInput, out string? errorMsg, out float? validNum)
        {
            float input = 0f;
            errorMsg = null;
            validNum = null;
            Errors err = new Errors();

            try
            {
                input = Convert.ToSingle(cmdInput);
                if (input <= 0)
                {
                    errorMsg = err.PrintErrorString(ErrorType.Info) + "Invalid Amount";
                }
                else
                {
                    validNum = input;
                }
            }
            catch (FormatException e)
            {
                errorMsg = err.PrintErrorString(ErrorType.Warning) + e.Message;
            }
        }
        public void CmdCheckBool(string cmdInput, out string? errorMsg, out bool? validOutput)
        {
            errorMsg = null;
            validOutput = null;

            if (cmdInput == "y" || cmdInput == "yes")
            {
                validOutput = true;
            }
            else if (cmdInput == "n" || cmdInput == "no")
            {
                validOutput = false;
            }
            else
            {
                Errors err = new Errors();
                errorMsg = err.PrintErrorString(ErrorType.Info) + $"Invalid Input '{cmdInput}': Enter y / yes or n / no";
            }
        }
        public void CmdCheckFuel(string cmdInput, out string? errorMsg, out string? validFuel)
        {
            errorMsg = null;
            validFuel = null;
            string input = cmdInput.ToLower();

            if (input == "p" || input == "petrol")
            {
                validFuel = "Petrol";
            }
            else if (input == "d" || input == "diesel")
            {
                validFuel = "Diesel";
            }
            else if (input == "e" || input == "electric")
            {
                validFuel = "Electric";
            }
            else
            {
                Errors err = new Errors();
                errorMsg = err.PrintErrorString(ErrorType.Info) + $"Invalid Type '{cmdInput}': [E / Electric] [P / Petrol] [D / Diesel]";
            }
        }
        public void CmdCheckTransmission(string cmdInput, out string? errorMsg, out string? validTrans)
        {
            errorMsg = null;
            validTrans = null;
            string input = cmdInput.ToLower();

            string[] a = { "a", "automatic", "auto" };
            string[] m = { "m", "manual", "man" };

            if (a.Contains(input))
            {
                validTrans = "Automatic";
            }
            else if (m.Contains(input))
            {
                validTrans = "Manual";
            }
            else
            {
                Errors err = new Errors();
                errorMsg = err.PrintErrorString(ErrorType.Info) + $"Invalid Transmission '{cmdInput}': [Manual (m / man) / Automatic (a / auto)]";
            }
        }

        // Registering Account
        public string InputMatch(string prompt, string confirmPrompt)
        {
            Errors err = new Errors();
            string input = "";
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                Console.Write(confirmPrompt);
                string input2 = Console.ReadLine();

                if (input != input2)
                {
                    err.PrintError(ErrorType.Info, "Inputs need to match");
                }
                else { break; }
            }
            return input;
        }
    }
}
