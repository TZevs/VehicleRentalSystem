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
            Console.WriteLine("[2] View Cars");
            Console.WriteLine("[3] View Vans");
            Console.WriteLine("[4] View Motorcycles");
            Console.WriteLine("[5] Search");
            Console.WriteLine("[6] Add Vehicle");
            Console.WriteLine("[7] Delete Vehicle");
            Console.WriteLine("[8] Rent & Return");
            Console.WriteLine("[9] Exit");

            while (true)
            {
                Console.Write(">> ");
                string select = Console.ReadLine().Trim();
                switch (select)
                {
                    case "2": Program.ViewCars(); return;
                    case "3": Program.ViewVans(); return;
                    case "4": Program.ViewMotors(); return;
                    case "5": Program.SearchVehicles(); return;
                    case "6": Program.AddVehicles(); return;
                    case "7": Program.DeleteVehicles(); return;
                    case "8": Program.RentAndReturn(); return;
                    case "9": return;
                    default: break;
                }
            }
        }
        public void GetBeforeLogin()
        {
            Console.Clear();
            Console.WriteLine("VEHICLE RENTAL - MAIN MENU");
            Console.WriteLine("[0] Login");
            Console.WriteLine("[1] Register Account");
            Console.WriteLine("[2] View Cars");
            Console.WriteLine("[3] View Vans");
            Console.WriteLine("[4] View Motorcycles");
            Console.WriteLine("[5] Search");
            Console.WriteLine("[9] Exit");

            while (true)
            {
                Console.Write(">> ");
                string select = Console.ReadLine().Trim();
                switch (select)
                {
                    case "0": Program.Login(); return;
                    case "1": Program.Register(); return;
                    case "2": Program.ViewCars(); return;
                    case "3": Program.ViewVans(); return;
                    case "4": Program.ViewMotors(); return;
                    case "5": Program.SearchVehicles(); return;
                    case "9": return;
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
                    Console.Write(">> ");
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
                    Console.Write(">> ");
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
                    Console.Write(">> ");
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
                    Console.Write(">> ");
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
        public void GetMenuForFuncs(string func)
        {
            if (func == "Add")
            {
                Console.WriteLine("\n[0] Back to Main || [1] View Vehicles || [2] Add Another Vehicle");
                while (true)
                {
                    Console.Write(">> ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": GetMainMenu(); return;
                        case "1": Program.ViewVehicles(); return;
                        case "2": Program.AddVehicles(); return;
                        default: break;
                    }
                }
            }
            else if (func == "Delete")
            {
                Console.WriteLine("\n[0] Back to Main || [1] View Vehicles || [2] Delete Another Vehicle");
                while (true)
                {
                    Console.Write(">> ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": GetMainMenu(); return;
                        case "1": Program.ViewVehicles(); return;
                        case "2": Program.DeleteVehicles(); return;
                        default: break;
                    }
                }
            }
            else if (func == "RentAndReturn") 
            {
                Console.WriteLine("\n[0] Back to Main || [1] View Vehicles || [2] Rent & Return Again");
                while (true)
                {
                    Console.Write(">> ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": GetMainMenu(); return;
                        case "1": Program.ViewVehicles(); return;
                        case "2": Program.RentAndReturn(); return;
                        default: break;
                    }
                }
            }
            else if (func == "Search")
            {
                Console.WriteLine("\n[0] Back to Main || [1] Rent Vehicle || [2] Search Again");
                while (true)
                {
                    Console.Write(">> ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": GetMainMenu(); return;
                        case "1": Program.RentAndReturn(); return;
                        case "2": Program.SearchVehicles(); return;
                        default: break;
                    }
                }
            }
        }
    }
}
