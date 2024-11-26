using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace VehicleRentalApp
{
    internal class TableDisplay
    {
        public void DisplayVehicles(IEnumerable<KeyValuePair<int, Vehicle>> view)
        {
            string[] headers = { "ID", "Make", "Model", "Year", "Daily Rate", "Transmission", "No. Seats", "Fuel Type" };

            var all = new Table();

            foreach (string col in headers)
            {
                all.AddColumn(col);
            }
            foreach (var item in view)
            {
                all.AddRow(
                    item.Key.ToString(),
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
                    item.Value.BootCapacity.ToString()
                );
            }

            AnsiConsole.Write(cars);
        }
        public void DisplayMotors(IEnumerable<KeyValuePair<int, Vehicle>> view)
        {
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
                    item.Value.CC.ToString(),
                    item.Value.Storage.ToString(),
                    item.Value.WithProtection.ToString()
                );
            }

            AnsiConsole.Write(motors);
        }
        public void DisplayVans(IEnumerable<KeyValuePair<int, Vehicle>> view)
        {
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
                    $"{item.Value.LoadCapacity.ToString()}kg",
                    item.Value.GetLWH(),   
                    $"{item.Value.Volume.ToString()}m"
                );
            }

            AnsiConsole.Write(vans);
        }
    }
}
