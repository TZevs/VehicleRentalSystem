using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VehicleRentalApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "vehicles.txt";
            Dictionary<int, Vehicle> vehicles = new Dictionary<int, Vehicle>();

            if (File.Exists(filePath))
            {
                List<string> output = new List<string>();
                foreach (string line in File.ReadLines(filePath))
                {
                    output.Clear();
                    foreach (string parts in line.Split(", "))
                    {
                        output.Add(parts);
                    }

                    Vehicle fromFile = new Vehicle(output[1], output[2], output[3], output[4], output[5]);
                    fromFile.UpdateStatus = output[6];
                    vehicles.Add(Convert.ToInt32(output[0]), fromFile);
                }
            }
            else
            {
                Console.WriteLine("No Vehicles available.");
            }

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
                    Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} || £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.UpdateStatus}");
                }

                // Outputs options waits for correct input.  
                Console.WriteLine("\n[0] Back to Main || [1] Search Vehicles");
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
                Console.WriteLine("SEARCH VEHICLES (Use commas to seperate)");
                Console.Write("Search: ");
                List<string> searching = Console.ReadLine().Split(", ").ToList();
                IEnumerable<KeyValuePair<int, Vehicle>> query = new List<KeyValuePair<int, Vehicle>>();
                foreach (string s in searching)
                { 
                    query = vehicles.Where(q => q.Value.GetModel() == s || q.Value.GetMake() == s || q.Value.GetTransmission() == s);
                }
                DisplayVehicles(query);
                // Use any or all to check if result is true or not. 
                // If true use select or where 

                // Outputs options waits for correct input.  
                Console.WriteLine("\n[0] Back to Main || [1] Rent Vehicle");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": RentAndReturn(); return;
                        case "2": SearchVehicles(); return;
                        default: break;
                    }
                }
            }
            
            void DisplayVehicles(IEnumerable<KeyValuePair<int, Vehicle>> search)
            {
                foreach (KeyValuePair<int, Vehicle> v in search)
                {
                    Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} || £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.UpdateStatus}");
                }
            }

            void AddVehicles()
            {
                Console.Clear();
                Console.WriteLine("ADD VEHICLES");

                Console.Write("Make: ");
                string make = Console.ReadLine().Trim();
                Console.Write("Model: ");
                string model = Console.ReadLine().Trim();
                
                Console.Write("Year: ");
                string year = Console.ReadLine().Trim();

                Console.Write("Daily Rate: ");
                string rate = Console.ReadLine().Trim();
              
                Console.Write("Transmission Type [Manual / Automatic]: ");
                string transmission = Console.ReadLine().Trim().ToLower();


                // Creates object. Adds it to the Dictionary collection.
                int newKey = vehicles.Keys.Max() + 1;
                Vehicle newV = new Vehicle(make, model, year, rate, transmission);
                if (newV.IsValid)
                {
                    vehicles.Add(newKey, newV);
                    UpdateFile();
                    Console.WriteLine($"\nVehicle Added - {newV.GetMake()} | {newV.GetModel()} | {newV.GetYear()} | £{newV.GetRate()} | {newV.GetType()} | {newV.UpdateStatus}");
                }
                else
                {
                    Console.WriteLine("\nPlease check your vehicle details (Year, Daily Rate, Transmission Type)");
                    Console.WriteLine($"{newV.GetMake()} | {newV.GetModel()} | {newV.GetYear()} | £{newV.GetRate()} | {newV.GetType()} | {newV.UpdateStatus}");
                }

                // Outputs options, waits for correct input.
                Console.WriteLine("\n[0] Back to Main || [1] View Vehicles || [2] Add Another Vehicle");
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
                
                string inputId = Console.ReadLine().Trim();
                int id = 0;
                try
                {
                    id = Convert.ToInt32(inputId);
                }
                catch
                {
                    Console.WriteLine($"Invalid Input: {inputId}: Number ID Required.");
                }
                //int id = InvalidIntInput(Console.ReadLine().Trim());

                if (vehicles.ContainsKey(id))
                {
                    // Displays selected vehicles information.
                    Console.WriteLine($"{vehicles[id].GetMake()} || {vehicles[id].GetModel()} || {vehicles[id].GetYear()} || £{vehicles[id].GetRate()} || {vehicles[id].GetTransmission()} || {vehicles[id].UpdateStatus}");
                    Console.Write("Is this the vehicle you want to delete [y/n]: ");

                    // Asks for confirmation to delete the vehicle. 
                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            vehicles.Remove(id);
                            UpdateFile();
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
                    Console.WriteLine($"Vehicle ID: '{id}' not found.");
                }

                // Outputs options waits for correct input.  
                Console.WriteLine("\n[0] Back to Main || [1] View Vehicles || [2] Delete Another Vehicle");
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
                    catch
                    {
                        // If input cannot be converted to an int. An exception is thrown and this msg is displayed.
                        Console.WriteLine($"Invalid Input: {input}: Number ID Required.");
                    }
                }

                if (vehicles.ContainsKey(id))
                {
                    // Displays selected vehicles information.
                    Console.WriteLine($"{vehicles[id].GetMake()} || {vehicles[id].GetModel()} || {vehicles[id].GetYear()} || £{vehicles[id].GetRate()} || {vehicles[id].GetTransmission()} || {vehicles[id].UpdateStatus}");
                    
                    // Asks for confirmation of the vehicle's status update. 
                    string action = vehicles[id].UpdateStatus == "Available" ? "Rent" : "Return";
                    Console.Write($"Is this the vehicle you want to {action} [y/n]: ");

                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            if (vehicles[id].UpdateStatus == "Available" && action == "Rent")
                            {
                                vehicles[id].UpdateStatus = "Rented";
                                Console.WriteLine($"Vehicle {id} Rented");
                                UpdateFile();
                            }
                            else if (vehicles[id].UpdateStatus == "Rented" && action == "Return")
                            {
                                vehicles[id].UpdateStatus = "Available";
                                Console.WriteLine($"Vehicle {id} Returned");
                                UpdateFile();
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
                            Console.WriteLine($"{action} Cancelled.");
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
                Console.WriteLine("\n[0] Back to Main || [1] View Vehicles || [2] Rent & Return");
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
                    Console.WriteLine($"{vehicles[id].GetMake()} || {vehicles[id].GetModel()} || {vehicles[id].GetYear()} || £{vehicles[id].GetRate()} || {vehicles[id].GetTransmission()} || {vehicles[id].UpdateStatus}");

                    Console.Write($"Is this the vehicle you want to {action} [y/n]: ");

                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            if (vehicles[id].UpdateStatus == "Available" && action == "Rent")
                            {
                                vehicles[id].UpdateStatus = "Rented";
                                Console.WriteLine($"Vehicle {id} Rented.");
                                UpdateFile();
                            }
                            else if (vehicles[id].UpdateStatus == "Rented" && action == "Return")
                            {
                                vehicles[id].UpdateStatus = "Available";
                                Console.WriteLine($"Vehicle {id} Returned");
                                UpdateFile();
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
                            Console.WriteLine($"{action}ing Cancelled");
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
                    Console.WriteLine($"Invalid Input: {inputDel}: Number ID required.");
                }

                if (vehicles.ContainsKey(id))
                {
                    Console.WriteLine($"{vehicles[id].GetMake()} || {vehicles[id].GetModel()} || {vehicles[id].GetYear()} || £{vehicles[id].GetRate()} || {vehicles[id].GetTransmission()} || {vehicles[id].UpdateStatus}");
                    Console.Write("Is this the vehicle you want to delete [y/n]: ");

                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            vehicles.Remove(id);
                            Console.WriteLine($"Vehicle with ID {id} has been deleted");
                            UpdateFile();
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

            void CmdAddVehicle(string[] newV)
            {
                // If an array item has a / it is replaced with a space. New items are now in a List collection. 
                List<string> newVehicle = newV.Select(n => n.Replace('/', ' ')).ToList();

                int newKey = vehicles.Keys.Max() + 1;
                Vehicle cmdNewV = new Vehicle(newVehicle[0], newVehicle[1], newVehicle[2], newVehicle[3], newVehicle[4]);
                if (cmdNewV.IsValid)
                {
                    vehicles.Add(newKey, cmdNewV);
                    UpdateFile();
                    Console.WriteLine($"\nVehicle Added: {cmdNewV.GetMake()} | {cmdNewV.GetModel()} | {cmdNewV.GetYear()} | £{cmdNewV.GetRate()} | {cmdNewV.GetType()} | {cmdNewV.UpdateStatus}");
                }
                else
                {
                    Console.WriteLine("\nPlease check your vehicle details (Year, Daily Rate, Transmission Type)");
                    Console.WriteLine($"{cmdNewV.GetMake()} | {cmdNewV.GetModel()} | {cmdNewV.GetYear()} | £{cmdNewV.GetRate()} | {cmdNewV.GetType()} | {cmdNewV.UpdateStatus}");
                }
            }

            void UpdateFile()
            {
                File.WriteAllLines(filePath, 
                    vehicles.Select(x => $"{x.Key}, {x.Value.ToFile()}"));
            }

            if (args.Length <= 0)
            {
                MainMenu();
            }
            else if (args.Length == 1)
            {
                switch (args[0].ToLower())
                {
                    case "--menu":
                        MainMenu();
                        break;
                    case "--view":
                        ViewVehicles();
                        break;
                    case "--help":
                        Console.WriteLine("--help:");
                        Console.WriteLine("--menu : Opens the main menu");
                        Console.WriteLine("--view : Displays all vehicles");
                        Console.WriteLine("--rent : Requires Vehicle ID (Format: --rent 2)");
                        Console.WriteLine("--return : Requires Vehicle ID (Format: --return 2)");
                        Console.WriteLine("--del or --delete: Requires Vehicle ID (Format: --del 2 or --delete 2)");
                        Console.WriteLine("--add: Requires (Make Model Year DailyRate Transmission)");
                        Console.WriteLine("       Format: (--add Land/Rover Defender 2019 230.23 Hybrid)");
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
                    case "--rent":
                        CmdRentAndReturn("Rent", args[1]);
                        break;
                    case "--return":
                        CmdRentAndReturn("Return", args[1]);
                        break;
                    case "--del":
                        CmdDelVehicle(args[1]);
                        break;
                    case "--delete":
                        CmdDelVehicle(args[1]);
                        break;
                    case "--add":
                        if (args.Length == 6)
                        {
                            string[] newVehicles = args.Skip(1).ToArray();
                            CmdAddVehicle(newVehicles);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect Input: Replace any spaces in names with a /.");
                        }
                        break;
                    default:
                        Console.WriteLine($"Unknown Command: {args[0]}");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
    }
}
