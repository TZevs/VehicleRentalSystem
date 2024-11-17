using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    enum ErrorType
    {
        Info,
        Warning,
        Error,
        Critical
    }

    internal class Errors
    {
        public string GetColor(ErrorType error)
        {
            switch (error)
            {
                case ErrorType.Info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    string infoError = "INFO: ";
                    Console.ResetColor();
                    return infoError;

                case ErrorType.Warning:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string warnError = "WARNING: ";
                    Console.ResetColor();
                    return warnError;

                case ErrorType.Error:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string errError = "ERROR: ";
                    Console.ResetColor();
                    return errError;

                case ErrorType.Critical:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    string critError = "CRITICAL: ";
                    Console.ResetColor();
                    return critError;

                default: return "Unknown Error";
            }
        }
    }
}
