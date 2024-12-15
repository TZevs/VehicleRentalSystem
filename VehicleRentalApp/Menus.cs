using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    public class Menus
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
            Console.WriteLine("[8] Rent");
            Console.WriteLine("[9] Return");
            Console.WriteLine("[0] Exit");

            while (true)
            {
                Console.Write(">> ");
                string select = Console.ReadLine().Trim();
                switch (select)
                {
                    case "2": Program.ViewCars(0); return;
                    case "3": Program.ViewVans(0); return;
                    case "4": Program.ViewMotors(0); return;
                    case "5": Program.SearchVehicles(); return;
                    case "6": Program.AddVehicles(); return;
                    case "7": Program.DeleteVehicles(); return;
                    case "8": Program.RentVehicle(); return;
                    case "9": Program.ReturnVehicle(); return;
                    case "0": return;
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
            Console.WriteLine("[6] Exit");

            while (true)
            {
                Console.Write(">> ");
                string select = Console.ReadLine().Trim();
                switch (select)
                {
                    case "0": Program.Login(); return;
                    case "1": Program.Register(); return;
                    case "2": Program.ViewCars(0); return;
                    case "3": Program.ViewVans(0); return;
                    case "4": Program.ViewMotors(0); return;
                    case "5": Program.SearchVehicles(); return;
                    case "6": return;
                    default: break;
                }
            }
        }
    }
}
