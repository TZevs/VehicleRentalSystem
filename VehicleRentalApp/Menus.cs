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
        public void GetMenu()
        {

        }
    }
}
