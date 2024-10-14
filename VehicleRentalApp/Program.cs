using System.Globalization;

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
            vehicles.Add(4, new Vehicle("BMW", "6 Series", 2010, 56, "Automatic"));

            //foreach (KeyValuePair<int, Vehicle> v in vehicles)
            //{
            //    string status = v.Value.IsAvailable == true ? "Available" : "Rented";
            //    Console.WriteLine($"{v.Key}, {v.Value.Make}, {v.Value.Model}, {v.Value.Year}, {v.Value.DailyRate}, {v.Value.Transmission}, {status}");
            //}

            void MainMenu()
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
                        case "1": ViewVehicles(); return;
                        case "2": SearchVehicles(); return;
                        case "3": AddVehicles(); return;
                        case "4": DeleteVehicles(); return;
                        case "5": RentAndReturn(); return;
                        case "6": return;
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
                
                Console.WriteLine("[0] Back to Main || [1] Search Vehicles");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": SearchVehicles(); return;
                        default: break;
                    }
                }
            }

            void SearchVehicles()
            {
                Console.Clear();
                Console.WriteLine("SEARCH VEHICLES");

                // Look into substrings for user to search any field or many fields at once. 
                // Add while loops to each if statement for if incorrect input entered.
                // Add different ways a user can enter the same search e.g. Automatic, Auto.
                // Find away around the different cases. 
                Console.WriteLine("[1] Make || [2] Model || [3] Transmission");
                Console.Write("Enter Option: ");
                string category = Console.ReadLine().Trim();
                if (category == "1")
                {
                    Console.Write("Search Make: ");
                    string input = Console.ReadLine().ToUpper();
                    IEnumerable<KeyValuePair<int, Vehicle>> searchMakes = vehicles.Where(m => m.Value.Make == input);
                    foreach (var v in searchMakes)
                    {
                        string status = v.Value.IsAvailable == true ? "Available" : "Rented";
                        Console.WriteLine($"{v.Key} || {v.Value.Make} || {v.Value.Model} || {v.Value.Year} || £{v.Value.DailyRate} || {v.Value.Transmission} || {status}");
                    }
                }
                else if (category == "2")
                {
                    Console.Write("Search Model: ");
                    string input = Console.ReadLine();
                    IEnumerable<KeyValuePair<int, Vehicle>> searchModels = vehicles.Where(m => m.Value.Model == input);
                    foreach (var v in searchModels)
                    {
                        string status = v.Value.IsAvailable == true ? "Available" : "Rented";
                        Console.WriteLine($"{v.Key} || {v.Value.Make} || {v.Value.Model} || {v.Value.Year} || £{v.Value.DailyRate} || {v.Value.Transmission} || {status}");
                    }
                }
                else if (category == "3")
                {
                    Console.Write("Search Transmission: ");
                    string input = Console.ReadLine();
                    IEnumerable<KeyValuePair<int, Vehicle>> searchTr = vehicles.Where(m => m.Value.Transmission == input);
                    foreach (var v in searchTr)
                    {
                        string status = v.Value.IsAvailable == true ? "Available" : "Rented";
                        Console.WriteLine($"{v.Key} || {v.Value.Make} || {v.Value.Model} || {v.Value.Year} || £{v.Value.DailyRate} || {v.Value.Transmission} || {status}");
                    }
                }
                else
                {
                    SearchVehicles();
                }

                Console.WriteLine("[0] Back to Main || [1] Rent Vehicle");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": return;
                        default: break;
                    }
                }
            }

            void AddVehicles()
            {
                Console.Clear();
                Console.WriteLine("ADD VEHICLES");

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

                // Find last key num and add 1. If vehicle deleted then another added the count will equal the same as the last key. 
                vehicles.Add(vehicles.Count() + 1, new Vehicle(make, model, year, dailyRate, transmission));
                
                Console.WriteLine("[0] Back to Main || [1] View Vehicles || [2] Add Another Vehicle");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": ViewVehicles(); return;
                        case "2": AddVehicles(); return;
                        default: break;
                    }
                }
            }

            void DeleteVehicles()
            {
                Console.Clear();
                Console.WriteLine("DELETE VEHICLES");
                Console.Write("Vehicle ID: ");
                int id = Convert.ToInt32(Console.ReadLine().Trim());

                if (vehicles.ContainsKey(id))
                {
                    string status = vehicles[id].IsAvailable == true ? "Available" : "Rented";
                    Console.WriteLine($"{vehicles[id].Make} || {vehicles[id].Model} || {vehicles[id].Year} || £{vehicles[id].DailyRate} || {vehicles[id].Transmission} || {status}");
                    Console.Write("Is this the vehicle you want to delete [y/n]: ");

                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            vehicles.Remove(id);
                            Console.WriteLine($"Vehicle with ID {id} has been deleted");
                            break;
                        }
                        else if (confirm == "n")
                        {
                            Console.WriteLine($"Vehicle with ID {id} will not be deleted");
                            break;
                        }
                        else
                        {
                            Console.Write("[y/n]?: ");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Vehicle with ID {id} not found.");
                }

                Console.WriteLine("[0] Back to Main || [1] View Vehicles || [2] Delete Another Vehicle");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": ViewVehicles(); return;
                        case "2": DeleteVehicles(); return;
                        default: break;
                    }
                }
            }

            void RentAndReturn()
            {
                Console.Clear();
                Console.WriteLine("RENT & RETURN VEHICLES");

                int id = 0;
                while (true)
                {
                    Console.Write("Enter Vehicle ID: ");
                    string input = Console.ReadLine().ToLower();
                    // Validates the user input.
                    // If inout is not a number: Outputs error msg to user for incorrect format
                    try
                    {
                        id = Convert.ToInt32(input);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                if (vehicles.ContainsKey(id))
                {
                    string status = vehicles[id].IsAvailable == true ? "Available" : "Rented";
                    Console.WriteLine($"{vehicles[id].Make} || {vehicles[id].Model} || {vehicles[id].Year} || £{vehicles[id].DailyRate} || {vehicles[id].Transmission} || {status}");
                    
                    string update = vehicles[id].IsAvailable == true ? "Rent" : "Return";
                    Console.Write($"Is this the vehicle you want to {update} [y/n]: ");

                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            if (vehicles[id].IsAvailable == false)
                            {
                                vehicles[id].IsAvailable = true;
                                Console.WriteLine($"You have returned vehicle {id}");
                            }
                            else if (vehicles[id].IsAvailable == true)
                            {
                                vehicles[id].IsAvailable = false;
                                Console.WriteLine($"You have rented vehicle {id}");
                            }
                            break;
                        }
                        else if (confirm == "n")
                        {
                            Console.WriteLine($"Rent & Return Cancelled.");
                            break;
                        }
                        else
                        {
                            Console.Write("[y/n]?: ");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Vehicle ID: {id} not found.");
                }

                Console.WriteLine("[0] Back to Main || [1] View Vehicles || [2] Rent & Return");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": ViewVehicles(); return;
                        case "2": RentAndReturn(); return;
                        default: break;
                    }
                }
            } 

            MainMenu();
        }
    }
}
