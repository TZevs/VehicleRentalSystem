using Microsoft.VisualBasic.FileIO;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VehicleRentalApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string binFilePath = "vehicles.bin";
            string jsonFilePath = "vehicles.json";
            //Dictionary<int, Vehicle> vehicles = new Dictionary<int, Vehicle>();
            //string jsonString = File.ReadAllText("vehicles.json");
            //var vehicles = JsonSerializer.Deserialize<Dictionary<int, Vehicle>>(jsonString);
            //if (File.Exists(filePath))
            //{
            //    List<string> output = new List<string>();
            //    foreach (string line in File.ReadLines(filePath))
            //    {
            //        output.Clear();
            //        foreach (string parts in line.Split(", "))
            //        {
            //            output.Add(parts);
            //        }

            //        if (output[1] == "Car")
            //        {
            //            Car fileCar = new Car(output[2], output[3], Convert.ToInt32(output[4]), Convert.ToDecimal(output[5]), output[6], Convert.ToInt32(output[7]), output[8], Convert.ToInt32(output[10]));
            //            fileCar.Status = output[9];
            //            vehicles.Add(Convert.ToInt32(output[0]), fileCar);
            //        }
            //        else if (output[1] == "Motorcycle")
            //        {
            //            Motorcycle fileMotor = new Motorcycle(output[2], output[3], Convert.ToInt32(output[4]), Convert.ToDecimal(output[5]), output[6], Convert.ToInt32(output[7]), output[8], Convert.ToInt32(output[10]), Convert.ToBoolean(output[11]), Convert.ToBoolean(output[12]));
            //            fileMotor.Status = output[9];
            //            vehicles.Add(Convert.ToInt32(output[0]), fileMotor);
            //        }
            //        else if (output[1] == "Van")
            //        {
            //            Van fileVan = new Van(output[2], output[3], Convert.ToInt32(output[4]), Convert.ToDecimal(output[5]), output[6], Convert.ToInt32(output[7]), output[8], Convert.ToSingle(output[10]), Convert.ToSingle(output[11]), Convert.ToSingle(output[12]), Convert.ToSingle(output[13]));
            //            fileVan.Status = output[9];
            //            vehicles.Add(Convert.ToInt32(output[0]), fileVan);
            //        }
            //        else { return; }
            //    }
            //}

            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new VehicleConverter());
            string fromJsonString = File.ReadAllText(jsonFilePath);
            var vehicles = JsonSerializer.Deserialize<Dictionary<int, Vehicle>>(fromJsonString, serializeOptions);
            //string jsonStrn = JsonSerializer.Serialize(vehicles);
            //byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonStrn);
            //File.WriteAllBytes("vehicles.bin", byteArray);

            SerializeDictionary();
            void SerializeDictionary()
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true,  
                };
                string jsonStr = JsonSerializer.Serialize(vehicles, options);
                File.WriteAllText("vehicles.json", jsonStr);
            }

            //string jsonString = File.ReadAllText("vehicles.json");
            //var toVehicles = JsonSerializer.Deserialize<Dictionary<int, Vehicle>>(jsonString);

            //FileStream fs = new FileStream("vehicles.bin", FileMode.Create);
            //BinaryWriter bw = new BinaryWriter(fs);

            //foreach (var veh in vehicles)
            //{
            //    bw.Write(veh.Value.GetType());
            //    bw.Write(veh.Value.GetMake());
            //    bw.Write(veh.Value.GetYear());
            //    bw.Write(veh.Value.GetRate());
            //    var trans = veh.Value.GetTransmission();
            //    if (trans != null)
            //    {
            //        bw.Write(veh.Value.GetTransmission());
            //    }
            //    bw.Write(veh.Value.GetSeatCap());
            //    var fuel = veh.Value.GetFuel();
            //    if (fuel != null)
            //    {
            //        bw.Write(veh.Value.GetFuel());
            //    }
            //    bw.Write(veh.Value.BootCap.Value);
            //    var cc = veh.Value.GetCC();
            //    if (cc != null)
            //    {
            //        bw.Write(veh.Value.GetCC().Value);
            //    }
            //    var stor = veh.Value.GetStorage();
            //    if (stor != null) { bw.Write(veh.Value.GetStorage().Value);
            //    var pro = veh.Value.GetWProtect();
            //    if (pro != null) { bw.Write(veh.Value.GetWProtect().Value);
            //    bw.Write(veh.Value.GetLWH());
            //    bw.Write(veh.Value.GetVolume().Value);
            //}

            //bw.Close();
            //fs.Close();

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
                IEnumerable<KeyValuePair<int, Vehicle>> allCars = vehicles.Where(ac => ac.Value.GetVType() == "Car");
                DisplayVehicles(allCars);

                Console.WriteLine("\nVANS:");
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Status");
                IEnumerable<KeyValuePair<int, Vehicle>> allVans = vehicles.Where(ac => ac.Value.GetVType() == "Van");
                DisplayVehicles(allVans);

                Console.WriteLine("\nMotorcycles:");
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Status");
                IEnumerable<KeyValuePair<int, Vehicle>> allMotors = vehicles.Where(ac => ac.Value.GetVType() == "MotorCycles");
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
                IEnumerable<KeyValuePair<int, Vehicle>> allCars = vehicles.Where(ac => ac.Value.GetVType() == "Car");
                if (allCars.Count() == 0)
                {
                    Console.WriteLine("No Cars Available");
                }
                else
                {
                    foreach (KeyValuePair<int, Vehicle> v in allCars)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} " +
                            $"|| £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.BootCapacity} || {v.Value.Status}");
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
                IEnumerable<KeyValuePair<int, Vehicle>> allVans = vehicles.Where(ac => ac.Value.GetVType() == "Van");
                if (allVans.Count() == 0)
                {
                    Console.WriteLine("No Vans Available");
                }
                else
                {
                    foreach (KeyValuePair<int, Vehicle> v in allVans)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} " +
                            $"|| £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.LoadCapacity}kg || {v.Value.GetLWH()} " +
                            $"|| {v.Value.Volume}m ||{v.Value.Status}");
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
                IEnumerable<KeyValuePair<int, Vehicle>> allMotors = vehicles.Where(ac => ac.Value.GetVType() == "Motorcycle");
                if (allMotors.Count() == 0)
                {
                    Console.WriteLine("No Motorcycles Available");
                }
                else
                {
                    foreach (KeyValuePair<int, Vehicle> v in allMotors)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} " +
                            $"|| £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.CC} || {v.Value.Storage} " +
                            $"|| {v.Value.WithProtection} ||{v.Value.Status}");
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
                if (display.Count() == 0)
                {
                    Console.WriteLine("No Vehicles Available");
                }
                else
                {
                    foreach (KeyValuePair<int, Vehicle> v in display)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} || £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.Status}");
                    }
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
                    case "M": typeInput = "Motorcycle"; break;
                    default: AddVehicles(); break;
                }

                Console.Write("\nMake: ");
                string make = Console.ReadLine().Trim();
                Console.Write("Model: ");
                string model = Console.ReadLine().Trim();

                int yr = GetValidYear("Year: ");
                decimal rate = GetValidDecimal("Daily Rate: ");
                string transm = GetValidTransmission("Transmission Type [Manual / Automatic]: ");
                int numOfSeats = GetValidInt("Number of Seats: ");
                string fuelType = GetValidFuel("Fuel Type [Diesel / Petrol / Electric]: ");
                
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
                    int boot = GetValidInt("Boot Capacity (In Litres): ");

                    Car newCar = new Car(make, model, yr, rate, transm, numOfSeats, fuelType, boot);
                    Console.WriteLine($"\nCar Added - {newCar.ToFile()}");
                    if (GetValidBool("Add Car [ y / n ]: "))
                    {
                        vehicles.Add(newKey, newCar);
                        SerializeDictionary();
                    }
                }
                else if (typeInput == "Van")
                {
                    float loadCap = GetValidFloat("Load Capacity(Kg): ");
                    Console.WriteLine("Internal Measurements");
                    float length = GetValidFloat("Lenth (In metres): ");
                    float width = GetValidFloat("Width (In metres): ");
                    float height = GetValidFloat("Height (In metres): ");

                    Van newVan = new Van(make, model, yr, rate, transm, numOfSeats, fuelType, loadCap, length, width, height);
                    Console.WriteLine($"\nVan Added - {newVan.ToFile()}");
                    if (GetValidBool("Add Van [ y / n ]: "))
                    {
                        vehicles.Add(newKey, newVan);
                        SerializeDictionary();
                    }
                }
                else if (typeInput == "Motorcycle")
                {
                    int cc = GetValidInt("CC: ");
                    bool storage = GetValidBool("Storage [y / n]: ");
                    bool protection = GetValidBool("With Protection (Helmet, etc) [y / n]: ");

                    Motorcycle newMot = new Motorcycle(make, model, yr, rate, transm, numOfSeats, fuelType, cc, storage, protection);
                    Console.WriteLine($"\nMotorcycle Added - {newMot.ToFile()}");
                    if (GetValidBool("Add Motorcycle [ y / n ]: "))
                    {
                        vehicles.Add(newKey, newMot);
                        SerializeDictionary();
                    }
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

            float GetValidFloat(string prompt)
            {
                float input = 0f;
                while (true)
                {
                    Console.Write(prompt);
                    try
                    {
                        input = Convert.ToSingle(Console.ReadLine());
                        if (input <= 0)
                        {
                            Errors err = new Errors();
                            Console.WriteLine($"{err.GetColor(ErrorType.Info)}Invalid Amount");
                        }
                        else { break; }
                    }
                    catch (FormatException e)
                    {
                        Errors err = new Errors();
                        Console.WriteLine($"{err.GetColor(ErrorType.Warning)}{e.Message}");
                    }
                }
                return input;
            }
            int GetValidInt(string prompt)
            {
                int input = 0;
                while (true)
                {
                    Console.Write(prompt);
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input <= 0)
                        {
                            Errors err = new Errors();
                            Console.WriteLine($"{err.GetColor(ErrorType.Info)}Invalid Amount");
                        }
                        else { break; }
                    }
                    catch (FormatException e)
                    {
                        Errors err = new Errors();
                        Console.WriteLine($"{err.GetColor(ErrorType.Warning)}{e.Message}");
                    }
                }
                return input;
            }
            bool GetValidBool(string prompt)
            {
                while (true)
                {
                    Console.Write(prompt);
                    string input = Console.ReadLine().Trim().ToLower();
                    if (input == "y" || input == "yes")
                    {
                        return true;
                    }
                    else if (input == "n" || input == "no")
                    {
                        return false;
                    }
                    else
                    {
                        Errors err = new Errors();
                        Console.WriteLine($"{err.GetColor(ErrorType.Info)}Invalid Input. Enter 'y' or 'n'");
                    }
                }
            }
            int GetValidYear(string prompt)
            {
                int input = 0;
                while (true)
                {
                    Console.Write(prompt);
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input >= DateTime.Now.Year - 30 && input <= DateTime.Now.Year) { break; }
                        else
                        {
                            Errors err = new Errors();
                            Console.WriteLine($"{err.GetColor(ErrorType.Info)}Invalid Year");
                        }
                    }
                    catch (FormatException e)
                    {
                        Errors err = new Errors();
                        Console.WriteLine($"{err.GetColor(ErrorType.Warning)}{e.Message}");
                    }
                }
                return input;
            }
            string GetValidTransmission(string prompt)
            {
                string[] a = { "a", "automatic", "auto" };
                string[] m = { "m", "manual", "man" };
                while (true)
                {
                    Console.Write(prompt);
                    string input = Console.ReadLine().Trim().ToLower();
                    if (a.Contains(input))
                    {
                        return "Automatic";
                    }
                    else if (m.Contains(input))
                    {
                        return "Manual";
                    }
                    else
                    {
                        Errors err = new Errors();
                        Console.WriteLine($"{err.GetColor(ErrorType.Info)}Invalid Transmission. Enter 'Automatic' or 'Manual'");
                    }
                }
            }
            string GetValidFuel(string prompt)
            {
                while (true)
                {
                    Console.Write(prompt);
                    string input = Console.ReadLine().Trim().ToLower();
                    if (input == "d" || input == "diesel")
                    {
                        return "Diesel";
                    }
                    else if (input == "p" || input == "petrol")
                    {
                        return "Petrol";
                    }
                    else if (input == "e" || input == "electric")
                    {
                        return "Electric";
                    }
                    else
                    {
                        Errors err = new Errors();
                        Console.WriteLine($"{err.GetColor(ErrorType.Info)}Invalid Fuel Input. Enter 'Diesel', 'Petrol' or 'Electric'");
                    }
                }
            }
            decimal GetValidDecimal(string prompt)
            {
                decimal input = 0m;
                while (true)
                {
                    try
                    {
                        input = Convert.ToDecimal(Console.ReadLine());
                        if (input <= 0)
                        {
                            Errors err = new Errors();
                            Console.WriteLine($"{err.GetColor(ErrorType.Info)}Invalid Amount");
                        }
                        else { break; }
                    }
                    catch (FormatException e)
                    {
                        Errors err = new Errors();
                        Console.WriteLine($"{err.GetColor(ErrorType.Warning)}{e.Message}");
                    }
                }
                return input;
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
                    Console.WriteLine($"{vehicles[id].GetMake()} || {vehicles[id].GetModel()} || {vehicles[id].GetYear()} || £{vehicles[id].GetRate()} || {vehicles[id].GetTransmission()} || {vehicles[id].Status}");
                    Console.Write("Is this the vehicle you want to delete [y/n]: ");

                    // Asks for confirmation to delete the vehicle. 
                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            vehicles.Remove(id);
                            SerializeDictionary();
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
                    Console.WriteLine($"{vehicles[id].GetMake()} || {vehicles[id].GetModel()} || {vehicles[id].GetYear()} || £{vehicles[id].GetRate()} || {vehicles[id].GetTransmission()} || {vehicles[id].Status}");
                    
                    // Asks for confirmation of the vehicle's status update. 
                    string action = vehicles[id].Status == "Available" ? "Rent" : "Return";
                    Console.Write($"Is this the vehicle you want to {action} [y/n]: ");

                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            if (vehicles[id].Status == "Available" && action == "Rent")
                            {
                                vehicles[id].Status = "Rented";
                                Console.WriteLine($"Vehicle {id} Rented");
                                SerializeDictionary();
                            }
                            else if (vehicles[id].Status == "Rented" && action == "Return")
                            {
                                vehicles[id].Status = "Available";
                                Console.WriteLine($"Vehicle {id} Returned");
                                SerializeDictionary();
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
                    Console.WriteLine($"{vehicles[id].GetMake()} || {vehicles[id].GetModel()} || {vehicles[id].GetYear()} || £{vehicles[id].GetRate()} || {vehicles[id].GetTransmission()} || {vehicles[id].Status}");

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
                                SerializeDictionary();
                            }
                            else if (vehicles[id].Status == "Rented" && action == "Return")
                            {
                                vehicles[id].Status = "Available";
                                Console.WriteLine($"Vehicle {id} Returned");
                                SerializeDictionary();
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
                    Console.WriteLine($"{vehicles[id].GetMake()} || {vehicles[id].GetModel()} || {vehicles[id].GetYear()} || £{vehicles[id].GetRate()} || {vehicles[id].GetTransmission()} || {vehicles[id].Status}");
                    Console.Write("Is this the vehicle you want to delete [y/n]: ");

                    while (true)
                    {
                        string confirm = Console.ReadLine().Trim().ToLower();
                        if (confirm == "y")
                        {
                            vehicles.Remove(id);
                            Console.WriteLine($"Vehicle with ID {id} has been deleted");
                            SerializeDictionary();
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
                    Car cmdCar = new Car(newVehicle[1], newVehicle[2], Convert.ToInt32(newVehicle[3]), Convert.ToDecimal(newVehicle[4]), newVehicle[5], Convert.ToInt32(newVehicle[6]), newVehicle[7], Convert.ToInt32(newVehicle[8]));
                    vehicles.Add(newKey, cmdCar);
                    Console.WriteLine($"Car Added - {cmdCar.ToFile()}");
                }
                else if (newVehicle[0].ToUpper() == "V")
                {
                    Van cmdVan = new Van(newVehicle[1], newVehicle[2], Convert.ToInt32(newVehicle[3]), Convert.ToDecimal(newVehicle[4]), newVehicle[5], Convert.ToInt32(newVehicle[6]), newVehicle[7], Convert.ToSingle(newVehicle[8]), Convert.ToSingle(newVehicle[9]), Convert.ToSingle(newVehicle[10]), Convert.ToSingle(newVehicle[11]));
                    vehicles.Add(newKey, cmdVan);
                    Console.WriteLine($"Van Added - {cmdVan.ToFile()}");
                }
                else if (newVehicle[0].ToUpper() == "M")
                {
                    Motorcycle cmdMotor = new Motorcycle(newVehicle[1], newVehicle[2], Convert.ToInt32(newVehicle[3]), Convert.ToDecimal(newVehicle[4]), newVehicle[5], Convert.ToInt32(newVehicle[6]), newVehicle[7], Convert.ToInt32(newVehicle[8]), Convert.ToBoolean(newVehicle[9]), Convert.ToBoolean(newVehicle[10]));
                    vehicles.Add(newKey, cmdMotor);
                    Console.WriteLine($"Motorcycle Added - {cmdMotor.ToFile()}");
                }
                else { return; }
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
