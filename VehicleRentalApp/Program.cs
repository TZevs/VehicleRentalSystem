using System.Globalization;

namespace VehicleRentalApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Vehicle> vehicles = new Dictionary<int, Vehicle>();
            vehicles.Add(1, new Vehicle("Fiat", "500", 2014, 50, "Manual"));
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
                // Outputs main menu options. Called at run.
                Console.Clear();
                Console.WriteLine("VEHICLE RENTAL - MAIN MENU");
                Console.WriteLine("[1] View All");
                Console.WriteLine("[2] Search");
                Console.WriteLine("[3] Add Vehicle");
                Console.WriteLine("[4] Delete Vehicle");
                Console.WriteLine("[5] Rent & Return");
                Console.WriteLine("[6] Exit");

                // Waits for correct input.
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
                
                // Displays all vehicles in the Dictionary.
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Status");
                foreach (KeyValuePair<int, Vehicle> v in vehicles)
                {
                    Console.WriteLine($"{v.Key} || {v.Value.Make} || {v.Value.Model} || {v.Value.Year} || £{v.Value.DailyRate} || {v.Value.Transmission} || {v.Value.Status}");
                }

                // Outputs options waits for correct input.  
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
                        Console.WriteLine($"{v.Key} || {v.Value.Make} || {v.Value.Model} || {v.Value.Year} || £{v.Value.DailyRate} || {v.Value.Transmission} || {v.Value.Status}");
                    }
                }
                else if (category == "2")
                {
                    Console.Write("Search Model: ");
                    string input = Console.ReadLine();
                    IEnumerable<KeyValuePair<int, Vehicle>> searchModels = vehicles.Where(m => m.Value.Model == input);
                    foreach (var v in searchModels)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.Make} || {v.Value.Model} || {v.Value.Year} || £{v.Value.DailyRate} || {v.Value.Transmission} || {v.Value.Status}");
                    }
                }
                else if (category == "3")
                {
                    // Move to view function.
                    Console.Write("Search Transmission: ");
                    string input = Console.ReadLine();
                    IEnumerable<KeyValuePair<int, Vehicle>> searchTr = vehicles.Where(m => m.Value.Transmission == input);
                    foreach (var v in searchTr)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.Make} || {v.Value.Model} || {v.Value.Year} || £{v.Value.DailyRate} || {v.Value.Transmission} || {v.Value.Status}");
                    }
                }
                else
                {
                    SearchVehicles();
                }

                // Outputs options waits for correct input.  
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
                string make = Console.ReadLine().Trim();
                Console.Write("Model: ");
                string model = Console.ReadLine().Trim();
                
                int year;
                // Loops until a valid input is entered. 
                while (true)
                {
                    Console.Write("Year: ");
                    string yearInput = Console.ReadLine().Trim();
                    // Validates the user input.
                    try
                    {
                        // Converts input to int.
                        year = Convert.ToInt32(yearInput);
                        // Checks vehicle is at max 25 yrs old.
                        if (year >= DateTime.Now.Year - 25 && year <= DateTime.Now.Year) break;
                        else
                        {
                            Console.WriteLine($"Max Age: 25 yrs old. Between: {DateTime.Now.Year - 25} - {DateTime.Now.Year}");
                        }
                    }
                    catch (Exception e)
                    {
                        // Catched the exception and displays this message. 
                        Console.WriteLine(e.Message);
                    }
                }
                
                decimal dailyRate;
                while (true)
                {
                    Console.Write("Daily Rate: ");
                    string rateInput = Console.ReadLine().Trim();
                    try
                    {
                        // Converts input to a decimal.
                        dailyRate = Convert.ToDecimal(rateInput);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                
                Console.WriteLine("Transmission Type");
                // Array to store the transmission types
                string[] types = { "Manual", "Automatic", "Hybrid" };
                int typesIndex;
                string transmission = "";
                while (true)
                {
                    Console.Write("[0] Manual || [1] Automatic || [2] Hybrid: ");
                    string transmissionInput = Console.ReadLine().Trim();
                    try
                    {
                        typesIndex = Convert.ToInt32(transmissionInput);
                        // Checks input is within range.
                        if (typesIndex >= 0 && typesIndex < types.Length)
                        {
                            // Uses input to index the array. 
                            transmission = types[typesIndex];
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Select a Transmission type [0, 1, 2].");
                        }
                    }
                    catch (Exception e )
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                // Displays the new vehicle's information. 
                Console.WriteLine();
                Console.WriteLine($"Vehicle Added - Make: {make} | Model: {model} | Year: {year} | Daily Rate: {dailyRate} | Transmission: {transmission}");
               
                // Creates object. Adds it to the Dictionary collection.
                int newKey = vehicles.Keys.Max() + 1;
                vehicles.Add(newKey, new Vehicle(make, model, year, dailyRate, transmission));

                // Outputs options, waits for correct input.
                Console.WriteLine();
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
                // Add try and catch in a while loop to ensure no errors. 
                int id = Convert.ToInt32(Console.ReadLine().Trim());

                if (vehicles.ContainsKey(id))
                {
                    // Displays selected vehicles information.
                    // Asks for confirmation to delete the vehicle. 
                    Console.WriteLine($"{vehicles[id].Make} || {vehicles[id].Model} || {vehicles[id].Year} || £{vehicles[id].DailyRate} || {vehicles[id].Transmission} || {vehicles[id].Status}");
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

                // Outputs options waits for correct input.  
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

                int id;
                // Loops until a valid vehicle ID is entered.
                while (true)
                {
                    Console.Write("Enter Vehicle ID: ");
                    string input = Console.ReadLine().Trim();
                    
                    // Validates the user input.
                    try
                    {
                        // Converts the input to an int. 
                        id = Convert.ToInt32(input);
                        break;
                    }
                    catch (Exception e)
                    {
                        // If input cannot be converted to an int. An exception is thrown and this msg is displayed.
                        Console.WriteLine(e.Message);
                    }
                }

                if (vehicles.ContainsKey(id))
                {
                    // Displays selected vehicles information.
                    Console.WriteLine($"{vehicles[id].Make} || {vehicles[id].Model} || {vehicles[id].Year} || £{vehicles[id].DailyRate} || {vehicles[id].Transmission} || {vehicles[id].Status}");
                    
                    // Asks for confirmation of the vehicle's status update. 
                    string update = vehicles[id].Status == "Available" ? "Rent" : "Return";
                    Console.Write($"Is this the vehicle you want to {update} [y/n]: ");

                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            if (vehicles[id].Status == "Available")
                            {
                                vehicles[id].Status = "Rented";
                                Console.WriteLine($"Vehicle {id} Rented");
                            }
                            else if (vehicles[id].Status == "Rented")
                            {
                                vehicles[id].Status = "Available";
                                Console.WriteLine($"Vehicle {id} Returned");
                            }
                            break;
                        }
                        else if (confirm == "n")
                        {
                            Console.WriteLine("Rent & Return Cancelled.");
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
                    Console.WriteLine($"Vehicle ID: '{id}' not found.");
                }

                // Outputs options waits for correct input.  
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

            void CmdRentAndReturn(string action, string inputId)
            {
                int id = 0;
                try
                {
                    id = Convert.ToInt32(inputId);
                }
                catch
                {
                    Console.WriteLine($"Second argument should be a number.");
                }

                if (vehicles.ContainsKey(id))
                {
                    Console.WriteLine($"{vehicles[id].Make} || {vehicles[id].Model} || {vehicles[id].Year} || £{vehicles[id].DailyRate} || {vehicles[id].Transmission} || {vehicles[id].Status}");

                    Console.Write($"Is this the vehicle you want to {action} [y/n]: ");

                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            if (vehicles[id].Status == "Available" && action == "Rent")
                            {
                                vehicles[id].Status = "Rented";
                                Console.WriteLine($"Vehicle {id} Rented.");
                            }
                            else if (vehicles[id].Status == "Rented" && action == "Return")
                            {
                                vehicles[id].Status = "Available";
                                Console.WriteLine($"Vehicle {id} Returned");
                            }
                            else
                            {
                                if (action == "Rent")
                                {
                                    Console.WriteLine($"Vehicle {id} is not available.");
                                }
                                else if (action == "Return")
                                {
                                    Console.WriteLine($"Vehicle {id} has already been returned.");
                                }
                            }
                            break;
                        }
                        else if (confirm == "n")
                        {
                            Console.WriteLine($"{action} Cancelled");
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
                    Console.WriteLine($"Vehicle ID: '{id}' not found.");
                }
            }

            void CmdDelVehicle(string inputDel)
            {
                int id = 0;
                try
                {
                    id = Convert.ToInt32(inputDel);
                }
                catch
                {
                    Console.WriteLine($"Second argument should be a number.");
                }

                if (vehicles.ContainsKey(id))
                {
                    Console.WriteLine($"{vehicles[id].Make} || {vehicles[id].Model} || {vehicles[id].Year} || £{vehicles[id].DailyRate} || {vehicles[id].Transmission} || {vehicles[id].Status}");
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
            }

            if (args.Length <= 0)
            {
                MainMenu();
            }
            else if (args.Length == 1)
            {
                switch (args[0].ToLower())
                {
                    case "menu":
                        MainMenu();
                        break;
                    case "view":
                        ViewVehicles();
                        break;
                    default:
                        Console.WriteLine($"Unknown Command: {args[0]}");
                        break;
                }
            }
            else if (args.Length >= 2)
            {
                switch (args[0].ToLower())
                {
                    case "rent":
                        CmdRentAndReturn("Rent", args[1]);
                        break;
                    case "return":
                        CmdRentAndReturn("Return", args[1]);
                        break;
                    case "del":
                        CmdDelVehicle(args[1]);
                        break;
                    case "delete":
                        CmdDelVehicle(args[1]);
                        break;
                    default:
                        Console.WriteLine($"Unknown Command: {args[0]}");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}
