using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    // Enum class contains readonly variables.
    // Stores the type of errors the program can have.
    enum ErrorType
    {
        Info, // Incorrect data input (wrong password, etc)
        Warning, // Invalid data input (format exception)
        Error,  // Any other exception.
        Critical // Issue that affects the apps use.
    }

    internal class Errors
    {
        public void PrintError(ErrorType error, string message)
        {
            switch (error)
            {
                case ErrorType.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("INFO: ");
                    break;

                case ErrorType.Warning:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("WARNING: ");
                    break;

                case ErrorType.Error:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("ERROR: ");
                    break;

                case ErrorType.Critical:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("CRITICAL: ");
                    break;

                default: 
                    message = "Unknown Error";
                    break;    
            }

            Console.ResetColor();
            Console.WriteLine(message);
        }
        // returns a string for when I do not want the error message to be outputted immediately.
        public string PrintErrorString(ErrorType error)
        {
            string message = string.Empty;
            switch (error)
            {
                case ErrorType.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    message = "INFO: ";
                break;

                case ErrorType.Warning:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    message = "WARNING: ";
                break;

                case ErrorType.Error:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    message = "ERROR: ";
                break;

                case ErrorType.Critical:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    message = "CRITICAL: ";
                break;

                default:
                    message = "Unknown Error";
                break;
            }

            return message;
        }
    }
}
