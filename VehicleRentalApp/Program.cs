namespace VehicleRentalApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Vehicle> vehicles = new Dictionary<int, Vehicle>();
            vehicles.Add(1, new Vehicle("FIAT", "500", 2014, 50, "Manual"));
            vehicles.Add(2, new Vehicle("Mercedes", "A-Class", 2020, 60, "Automatic"));
            vehicles.Add(3, new Vehicle("BMW", "1 Series", 2018, 69, "Automatic"));

            //foreach (KeyValuePair<int, Vehicle> v in vehicles)
            //{
            //    string status = v.Value.IsAvailable == true ? "Available" : "Rented";
            //    Console.WriteLine($"{v.Key}, {v.Value.Make}, {v.Value.Model}, {v.Value.Year}, {v.Value.DailyRate}, {v.Value.Transmission}, {status}");
            //}

            void MainMenu()
            {
                Console.WriteLine("VEHICLE RENTAL - MAIN MENU");
                Console.WriteLine("[1] View Vehicles");
                Console.WriteLine("[2] Search Vehicles");
                Console.WriteLine("[3] Add Vehicle");
                Console.WriteLine("[4] Delete Vehicle");
                Console.WriteLine("[5] Exit");

                while (true)
                {
                    Console.Write("Enter Menu Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "1": ViewVehicles(); return;
                        case "2": SearchVehicles(); return;
                        case "3": AddVehicles(); return;
                        case "4": DeleteVehicles(); return;
                        default: Console.WriteLine("Please Enter a Menu Option"); break;
                    }
                }
            }

            void ViewVehicles()
            {
                Console.Clear();
                Console.WriteLine("ALL VEHICLES");
            }
            void SearchVehicles()
            {
                Console.Clear();
            }
            void AddVehicles()
            {
                Console.Clear();
            }
            void DeleteVehicles()
            {
                Console.Clear();
            }

            MainMenu();
        }
    }
}
