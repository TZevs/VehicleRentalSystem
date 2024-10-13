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
                Console.Clear();
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
                        default: break;
                    }
                }
            }

            void ViewVehicles()
            {
                Console.Clear();
                Console.WriteLine("ALL VEHICLES");
                
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Status");
                foreach (KeyValuePair<int, Vehicle> v in vehicles)
                {
                    string status = v.Value.IsAvailable == true ? "Available" : "Rented";
                    Console.WriteLine($"{v.Key} || {v.Value.Make} || {v.Value.Model} || {v.Value.Year} || £{v.Value.DailyRate} || {v.Value.Transmission} || {status}");
                }
                
                Console.WriteLine("[0] Back to Main || [2] Search Vehicles");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "2": SearchVehicles(); return;
                        default: break;
                    }
                }
            }

            void SearchVehicles()
            {
                Console.Clear();
            }

            void AddVehicles()
            {
                Console.Clear();
                Console.WriteLine("ADD VEHICLE");

                // Need to add validation to inputs - Year and DailyRate will cause errors if wrong.
                // Transmission type: Auto and Man - validate different inputs. Different for vehicle types. 
                Console.Write("Make: ");
                string make = Console.ReadLine();
                Console.Write("Model: ");
                string model = Console.ReadLine();
                Console.Write("Year: ");
                int year = Convert.ToInt32(Console.ReadLine());
                Console.Write("Daily Rate: ");
                decimal dailyRate = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Transmission Type: ");
                string transmission = Console.ReadLine();

                vehicles.Add(vehicles.Count() + 1, new Vehicle(make, model, year, dailyRate, transmission));
                
                Console.WriteLine("[0] Back to Main || [1] View Vehicles || [3] Add Another Vehicle");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": ViewVehicles(); return;
                        case "3": AddVehicles(); return;
                        default: break;
                    }
                }
            }

            void DeleteVehicles()
            {
                Console.Clear();
            }

            MainMenu();
        }
    }
}
