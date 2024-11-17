using Microsoft.VisualBasic.FileIO;
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

                    if (output[1] == "Car")
                    {
                        Car fileCar = new Car(output[2], output[3], output[4], output[5], output[6], output[7], output[8], output[10]);
                        fileCar.UpdateStatus = output[9];
                        vehicles.Add(Convert.ToInt32(output[0]), fileCar);
                    }
                    else if (output[1] == "Motorcycle")
                    {
                        Motorcycle fileMotor = new Motorcycle(output[2], output[3], output[4], output[5], output[6], output[7], output[8], output[10], output[11], output[12]);
                        fileMotor.UpdateStatus = output[9];
                        vehicles.Add(Convert.ToInt32(output[0]), fileMotor);
                    }
                    else if (output[1] == "Van")
                    {
                        Van fileVan = new Van(output[2], output[3], output[4], output[5], output[6], output[7], output[8], output[10], output[11], output[12], output[13]);
                        fileVan.UpdateStatus = output[9];
                        vehicles.Add(Convert.ToInt32(output[0]), fileVan);
                    }
                    else { return; }

                    //Vehicle fromFile = new Vehicle(output[1], output[2], output[3], output[4], output[5]);
                    //fromFile.UpdateStatus = output[6];
                    //vehicles.Add(Convert.ToInt32(output[0]), fromFile);
                }
            }
            else
            {
                Console.WriteLine("No Vehicles Available.");
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
                Console.WriteLine("\nCARS:");
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Status");
                IEnumerable<KeyValuePair<int, Vehicle>> allCars = vehicles.Where(ac => ac.Value.GetType() == "Car");
                DisplayVehicles(allCars);

                Console.WriteLine("\nVANS:");
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Status");
                IEnumerable<KeyValuePair<int, Vehicle>> allVans = vehicles.Where(ac => ac.Value.GetType() == "Van");
                DisplayVehicles(allVans);

                Console.WriteLine("\nVANS:");
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Status");
                IEnumerable<KeyValuePair<int, Vehicle>> allMotors = vehicles.Where(ac => ac.Value.GetType() == "MotorCycles");
                DisplayVehicles(allMotors);

                // Outputs options waits for correct input.  
                Console.WriteLine("\n[0] Back to Main || [1] Search Vehicles");
                Console.WriteLine("[2] View Cars || [3] View Vans || [4] View Motorcycles");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": SearchVehicles(); return;
                        case "2": ViewCars(); return;
                        case "3": ViewVans(); return;
                        case "4": ViewMotors(); return;
                        default: break;
                    }
                }
            }
            
            void ViewCars()
            {
                Console.Clear();
                Console.WriteLine("ALL CARS");

                // Displays all Cars in the Dictionary and all their stored data.
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Boot Capacity || Status");
                IEnumerable<KeyValuePair<int, Vehicle>> allCars = vehicles.Where(ac => ac.Value.GetType() == "Car");
                if (allCars.Count() == 0)
                {
                    Console.WriteLine("No Cars Available");
                }
                else
                {
                    foreach (KeyValuePair<int, Vehicle> v in allCars)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} " +
                            $"|| £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.GetBootCap()} || {v.Value.UpdateStatus}");
                    }
                }

                Console.WriteLine("\n[0] Back to Main || [1] Search Vehicles || [2] View All");
                Console.WriteLine("[3] View Vans || [4] View Motorcycles");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": SearchVehicles(); return;
                        case "2": ViewVehicles(); return;
                        case "3": ViewVans(); return;
                        case "4": ViewMotors(); return;
                        default: break;
                    }
                }
            }
            void ViewVans()
            {
                Console.Clear();
                Console.WriteLine("ALL VANS");

                // Displays all Vans in the Dictionary and all their stored data.
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Max Load || Internal || Volume(Cubed) || Status");
                IEnumerable<KeyValuePair<int, Vehicle>> allVans = vehicles.Where(ac => ac.Value.GetType() == "Van");
                if (allVans.Count() == 0)
                {
                    Console.WriteLine("No Vans Available");
                }
                else
                {
                    foreach (KeyValuePair<int, Vehicle> v in allVans)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} " +
                            $"|| £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.GetLoadCap()}kg || {v.Value.GetLWH()} " +
                            $"|| {v.Value.GetVolume()}m ||{v.Value.UpdateStatus}");
                    }
                }

                Console.WriteLine("\n[0] Back to Main || [1] Search Vehicles || [2] View All");
                Console.WriteLine("[3] View Cars || [4] View Motorcycles");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": SearchVehicles(); return;
                        case "2": ViewVehicles(); return;
                        case "3": ViewCars(); return;
                        case "4": ViewMotors(); return;
                        default: break;
                    }
                }
            }
            void ViewMotors()
            {
                Console.Clear();
                Console.WriteLine("ALL MOTORCYCLES");

                // Displays all Motorcycles in the Dictionary and all their stored data.
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || CC || With Storage || With Protection || Status");
                IEnumerable<KeyValuePair<int, Vehicle>> allMotors = vehicles.Where(ac => ac.Value.GetType() == "Motorcycle");
                if (allMotors.Count() == 0)
                {
                    Console.WriteLine("No Motorcycles Available");
                }
                else
                {
                    foreach (KeyValuePair<int, Vehicle> v in allMotors)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} " +
                            $"|| £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.GetCC()} || {v.Value.GetStorage()} " +
                            $"|| {v.Value.GetWProtect()} ||{v.Value.UpdateStatus}");
                    }
                }

                Console.WriteLine("\n[0] Back to Main || [1] Search Vehicles || [2] View All");
                Console.WriteLine("[3] View Vans || [4] View Cars");
                while (true)
                {
                    Console.Write("Enter Option: ");
                    string select = Console.ReadLine().Trim();
                    switch (select)
                    {
                        case "0": MainMenu(); return;
                        case "1": SearchVehicles(); return;
                        case "2": ViewVehicles(); return;
                        case "3": ViewVans(); return;
                        case "4": ViewCars(); return;
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
            
            void DisplayVehicles(IEnumerable<KeyValuePair<int, Vehicle>> display)
            {
                foreach (KeyValuePair<int, Vehicle> v in display)
                {  
                    Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} || £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.UpdateStatus}");   
                }
                
            }

            void AddVehicles()
            {
                Console.Clear();
                Console.WriteLine("ADD VEHICLES");
                Console.WriteLine("[C] = Car | [V] = Van | [M] = Motorcycle");
                Console.Write("Vehicle Type: ");
                string typeInput = "";
                switch(Console.ReadLine().ToUpper())
                {
                    case "C": typeInput = "Car"; break;
                    case "V": typeInput = "Van"; break;
                    case "M": typeInput = "MotorCycle"; break;
                    default: AddVehicles(); break;
                }

                Console.Write("\nMake: ");
                string make = Console.ReadLine().Trim();
                Console.Write("Model: ");
                string model = Console.ReadLine().Trim();
                Console.Write("Year: ");
                string year = Console.ReadLine();
                Console.Write("Daily Rate: ");
                string rate = Console.ReadLine();
                Console.Write("Transmission Type [Manual / Automatic]: ");
                string transm = Console.ReadLine().Trim().ToLower();
                Console.Write("Number of Seats: ");
                string numOfSeats = Console.ReadLine();
                Console.Write("Fuel Type [Diesel / Petrol / Electric / Hybrid]: ");
                string fuelType = Console.ReadLine().ToLower();

                int newKey = 0;
                if (vehicles.Count == 0)
                {
                    newKey = 1;
                }
                else 
                {
                    newKey = vehicles.Keys.Max() + 1;
                }
                if (typeInput == "Car") 
                {
                    Console.Write("\nBoot Capacity (In Litres): ");
                    string bootCap = Console.ReadLine();

                    Car newCar = new Car(make, model, year, rate, transm, numOfSeats, fuelType, bootCap);
                    if (newCar.IsValid)
                    {
                        vehicles.Add(newKey, newCar);
                        Console.WriteLine($"\nCar Added - {newCar.ToFile()}");
                        UpdateFile();
                    }
                    else
                    {
                        Console.WriteLine("\nPlease check your Car's details");
                        Console.WriteLine($"{newCar.ToFile()}");
                    }
                }
                else if (typeInput == "Van")
                {
                    Console.Write("\nLoad Capacity (Max in kg): ");
                    string loadCap = Console.ReadLine();
                    Console.WriteLine("Internal Measurements");
                    Console.Write("Lenth (In metres): ");
                    string len = Console.ReadLine();
                    Console.Write("Width (In metres): ");
                    string wid = Console.ReadLine();
                    Console.Write("Height (In metres): ");
                    string hei = Console.ReadLine();

                    Van newVan = new Van(make, model, year, rate, transm, numOfSeats, fuelType, loadCap, len, wid, hei);
                    if (newVan.IsValid)
                    {
                        vehicles.Add(newKey, newVan);
                        Console.WriteLine($"\nVan Added - {newVan.ToFile()}");
                        UpdateFile();
                    }
                    else
                    {
                        Console.WriteLine("\nPlease check your Van's details");
                        Console.WriteLine($"Van - {newVan.ToFile()}");
                    }
                }
                else if (typeInput == "Motorcycle")
                {
                    Console.Write("CC: ");
                    string cc = Console.ReadLine();
                    Console.Write("Storage [y / n]: ");
                    string stor = Console.ReadLine();
                    Console.Write("With Protection (Helmet, Jacket, etc)[y / n]: ");
                    string pro = Console.ReadLine();

                    Motorcycle newMot = new Motorcycle(make, model, year, rate, transm, numOfSeats, fuelType, cc, stor, pro);
                    if (newMot.IsValid)
                    {
                        vehicles.Add(newKey, newMot);
                        Console.WriteLine($"\nVan Added - {newMot.ToFile()}");
                        UpdateFile();
                    }
                    else
                    {
                        Console.WriteLine("\nPlease check your Motorcycle's details");
                        Console.WriteLine($"Motorcycle Added - {newMot.ToFile()}");
                    }
                }
                else
                {

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
                
                int newKey = 0;
                if (vehicles.Count == 0)
                {
                    newKey = 1;
                }
                else
                {
                    newKey = vehicles.Keys.Max() + 1;
                }
                if (newVehicle[0].ToUpper() == "C")
                {
                    Car cmdCar = new Car(newVehicle[1], newVehicle[2], newVehicle[3], newVehicle[4], newVehicle[5], newVehicle[6], newVehicle[7], newVehicle[8]);

                    if (cmdCar.IsValid)
                    {
                        vehicles.Add(newKey, cmdCar);
                        Console.WriteLine($"Car Added - {cmdCar.ToFile()}");
                    }
                    else
                    {
                        Console.WriteLine("Please check your Car's details");
                        Console.WriteLine($"Car - {cmdCar.ToFile()}");
                    }
                }
                else if (newVehicle[0].ToUpper() == "V") 
                {
                    Van cmdVan = new Van(newVehicle[1], newVehicle[2], newVehicle[3], newVehicle[4], newVehicle[5], newVehicle[6], newVehicle[7], newVehicle[8], newVehicle[9], newVehicle[10], newVehicle[11]);

                    if (cmdVan.IsValid)
                    {
                        vehicles.Add(newKey, cmdVan);
                        Console.WriteLine($"Van Added - {cmdVan.ToFile()}");
                    }
                    else
                    {
                        Console.WriteLine("Please check your Van's details");
                        Console.WriteLine($"Van - {cmdVan.ToFile()}");
                    }
                }
                else if (newVehicle[0].ToUpper() == "M")
                {
                    Motorcycle cmdMotor = new Motorcycle(newVehicle[1], newVehicle[2], newVehicle[3], newVehicle[4], newVehicle[5], newVehicle[6], newVehicle[7], newVehicle[8], newVehicle[9], newVehicle[10]);

                    if (cmdMotor.IsValid)
                    {
                        vehicles.Add(newKey, cmdMotor);
                        Console.WriteLine($"Motorcycle Added - {cmdMotor.ToFile()}");
                    }
                    else
                    {
                        Console.WriteLine("Please check your Motorcycle's details");
                        Console.WriteLine($"Motorcycle - {cmdMotor.ToFile()}");
                    }
                }
                else { return; }
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
