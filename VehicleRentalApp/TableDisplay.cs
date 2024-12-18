using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace VehicleRentalApp
{
    internal class TableDisplay
    {
        // Installed Spectre.Console package for tables
        public void DisplayVehicles(IEnumerable<KeyValuePair<int, Vehicle>> view)
        {
            // Used to display the results of a search.
            // Only displays the base class variables.
            string[] headers = { "ID", "Type", "Make", "Model", "Year", "Daily Rate", "Transmission", "No. Seats", "Fuel Type" };

            var all = new Table();

            foreach (string col in headers)
            {
                all.AddColumn(col);
            }

            foreach (var item in view)
            {
                all.AddRow(
                    item.Key.ToString(),
                    item.Value.GetVType(),
                    item.Value.GetMake(),
                    item.Value.GetModel(),
                    item.Value.GetYear().ToString(),
                    $"£{item.Value.GetRate():F2}",
                    item.Value.GetTransmission(),
                    item.Value.GetSeatCap().ToString(),
                    item.Value.GetFuel()
                );
            }

            AnsiConsole.Write(all);
        }
        public void DisplayCars(IEnumerable<KeyValuePair<int, Vehicle>> view)
        {
            // Displays all the variables of a Car object. 
            string[] headers = { "ID", "Make", "Model", "Year", "Daily Rate", "Transmission", "No. Seats", "Fuel Type", "Boot Capacity" };

            var cars = new Table();

            foreach (string col in headers)
            {
                cars.AddColumn(col);
            }
            foreach (var item in view)
            {
                cars.AddRow(
                    item.Key.ToString(),
                    item.Value.GetMake(),
                    item.Value.GetModel(),
                    item.Value.GetYear().ToString(),
                    $"£{item.Value.GetRate():F2}",
                    item.Value.GetTransmission(),
                    item.Value.GetSeatCap().ToString(),
                    item.Value.GetFuel(),
                    item.Value.GetBootCap().ToString()
                );
            }
            AnsiConsole.Write(cars);
        }
        public void DisplayMotors(IEnumerable<KeyValuePair<int, Vehicle>> view)
        {
            // Displays all the variables of a Motorcycles object. 
            string[] headers = { "ID", "Make", "Model", "Year", "Daily Rate", "Transmission", "No. Seats", "Fuel Type", "CC", "Has Storage", "With Protective Gear" };

            var motors = new Table();

            foreach (string col in headers)
            {
                motors.AddColumn(col);
            }

            foreach (var item in view)
            {
                motors.AddRow(
                    item.Key.ToString(),
                    item.Value.GetMake(),
                    item.Value.GetModel(),
                    item.Value.GetYear().ToString(),
                    $"£{item.Value.GetRate():F2}",
                    item.Value.GetTransmission(),
                    item.Value.GetSeatCap().ToString(),
                    item.Value.GetFuel(),
                    item.Value.GetCC().ToString(),
                    item.Value.GetStorage().ToString(),
                    item.Value.GetWithProtection().ToString()
                );
            }

            AnsiConsole.Write(motors);
        }
        public void DisplayVans(IEnumerable<KeyValuePair<int, Vehicle>> view)
        {
            // Displays all the variables of a Van object. 
            string[] headers = { "ID", "Make", "Model", "Year", "Daily Rate", "Transmission", "No. Seats", "Fuel Type", "Load Capacity", "Internal Dimensions", "Volume" };

            var vans = new Table();

            foreach (string col in headers)
            {
                vans.AddColumn(col);
            }

            foreach (var item in view)
            {
                vans.AddRow(
                    item.Key.ToString(),
                    item.Value.GetMake(),
                    item.Value.GetModel(),
                    item.Value.GetYear().ToString(),
                    $"£{item.Value.GetRate():F2}",
                    item.Value.GetTransmission(),
                    item.Value.GetSeatCap().ToString(),
                    item.Value.GetFuel(),
                    $"{item.Value.GetLoadCap().ToString()}kg",
                    item.Value.GetLWH(),
                    $"{item.Value.GetVolume().ToString()}m"
                );
            }

            AnsiConsole.Write(vans);
        }
    }
}
