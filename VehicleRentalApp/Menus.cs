using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Menus
    {
        public void GetMainMenu()
        {
            Console.Clear();
            Console.WriteLine("VEHICLE RENTAL - MAIN MENU");
            Console.WriteLine("[1] View All");
            Console.WriteLine("[2] Search");
            Console.WriteLine("[3] Add Vehicle");
            Console.WriteLine("[4] Delete Vehicle");
            Console.WriteLine("[5] Rent & Return");
            Console.WriteLine("[6] Exit");

            while (true)
            {
                Console.Write("Enter Menu Option: ");
                string select = Console.ReadLine().Trim();
                switch (select)
                {
                    case "1": Program.ViewVehicles(); return;
                    case "2": Program.SearchVehicles(); return;
                    case "3": Program.AddVehicles(); return;
                    case "4": Program.DeleteVehicles(); return;
                    case "5": Program.RentAndReturn(); return;
                    case "6": return;
                    default: break;
                }
            }
        }
        public void GetMenuForViewing(string view)
        {
            if (view == "All")
            {
                Console.WriteLine("\n[0] Back to Main || [1] Search Vehicles");
                Console.WriteLine("[2] View Cars || [3] View Vans || [4] View Motorcycles");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": GetMainMenu(); return;
                        case "1": Program.SearchVehicles(); return;
                        case "2": Program.ViewCars(); return;
                        case "3": Program.ViewVans(); return;
                        case "4": Program.ViewMotors(); return;
                        default: break;
                    }
                }
            }
            else if (view == "Cars")
            {
                Console.WriteLine("\n[0] Back to Main || [1] Search Vehicles || [2] View All");
                Console.WriteLine("[3] View Vans || [4] View Motorcycles");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": GetMainMenu(); return;
                        case "1": Program.SearchVehicles(); return;
                        case "2": Program.ViewVehicles(); return;
                        case "3": Program.ViewVans(); return;
                        case "4": Program.ViewMotors(); return;
                        default: break;
                    }
                }
            }
            else if (view == "Vans")
            {
                Console.WriteLine("\n[0] Back to Main || [1] Search Vehicles || [2] View All");
                Console.WriteLine("[3] View Cars || [4] View Motorcycles");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": GetMainMenu(); return;
                        case "1": Program.SearchVehicles(); return;
                        case "2": Program.ViewVehicles(); return;
                        case "3": Program.ViewCars(); return;
                        case "4": Program.ViewMotors(); return;
                        default: break;
                    }
                }
            }
            else if (view == "Motorcycles")
            {
                Console.WriteLine("\n[0] Back to Main || [1] Search Vehicles || [2] View All");
                Console.WriteLine("[3] View Vans || [4] View Cars");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": GetMainMenu(); return;
                        case "1": Program.SearchVehicles(); return;
                        case "2": Program.ViewVehicles(); return;
                        case "3": Program.ViewVans(); return;
                        case "4": Program.ViewCars(); return;
                        default: break;
                    }
                }
            }
        }
    }
}
