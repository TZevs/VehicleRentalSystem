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
            string filePath = "vehicles.txt";
            //Dictionary<int, Vehicle> vehicles = new Dictionary<int, Vehicle>();

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

            void SerializeDictionary()
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true,
                    // Ignores the variables not related to their object.
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                };
                string jsonStr = JsonSerializer.Serialize(vehicles, options);
                File.WriteAllText("vehicles.json", jsonStr);
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
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission");
                IEnumerable<KeyValuePair<int, Vehicle>> allCars = vehicles.Where(ac => ac.Value.GetVType() == "Car" && ac.Value.Status == "Available");
                DisplayVehicles(allCars);

                Console.WriteLine("\nVANS:");
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission");
                IEnumerable<KeyValuePair<int, Vehicle>> allVans = vehicles.Where(ac => ac.Value.GetVType() == "Van" && ac.Value.Status == "Available");
                DisplayVehicles(allVans);

                Console.WriteLine("\nMOTORCYCLES:");
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission");
                IEnumerable<KeyValuePair<int, Vehicle>> allMotors = vehicles.Where(ac => ac.Value.GetVType() == "Motorcycle" && ac.Value.Status == "Available");
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
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Boot Capacity");
                IEnumerable<KeyValuePair<int, Vehicle>> allCars = vehicles.Where(ac => ac.Value.GetVType() == "Car" && ac.Value.Status == "Available");
                if (allCars.Count() == 0)
                {
                    Console.WriteLine("No Cars Available");
                }
                else
                {
                    foreach (KeyValuePair<int, Vehicle> v in allCars)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} " +
                            $"|| £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.BootCapacity}");
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
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || Max Load || Internal || Volume(Cubed)");
                IEnumerable<KeyValuePair<int, Vehicle>> allVans = vehicles.Where(ac => ac.Value.GetVType() == "Van" && ac.Value.Status == "Available");
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
                            $"|| {v.Value.Volume}m");
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
                Console.WriteLine("ID || Make || Model || Year || Daily Rate || Transmission || CC || With Storage || With Protection");
                IEnumerable<KeyValuePair<int, Vehicle>> allMotors = vehicles.Where(ac => ac.Value.GetVType() == "Motorcycle" && ac.Value.Status == "Available");
                if (allMotors.Count() == 0)
                {
                    Console.WriteLine("No Motorcycles Available");
                }
                else
                {
                    foreach (KeyValuePair<int, Vehicle> v in allMotors)
                    {
                        Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} " +
                            $"|| £{v.Value.GetRate()} || {v.Value.GetTransmission()} || {v.Value.CC}cc || {v.Value.Storage} " +
                            $"|| {v.Value.WithProtection}");
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
                IEnumerable<KeyValuePair<int, Vehicle>> query = vehicles.Where(q => 
                    searching.All(s => 
                        q.Value.GetModel().Contains(s, StringComparison.OrdinalIgnoreCase) || 
                        q.Value.GetMake().Contains(s, StringComparison.OrdinalIgnoreCase) ||
                        q.Value.GetTransmission().Contains(s, StringComparison.OrdinalIgnoreCase) 
                    )
                );
                DisplayVehicles(query);

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
                        Console.WriteLine($"{v.Key} || {v.Value.GetMake()} || {v.Value.GetModel()} || {v.Value.GetYear()} || £{v.Value.GetRate()} || {v.Value.GetTransmission()}");
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
                    Console.WriteLine($"\nCar Added - {newCar.ConfirmDetails()}");
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
                    Console.WriteLine($"\nVan Added - {newVan.ConfirmDetails()}");
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
                    Console.WriteLine($"\nMotorcycle Added - {newMot.ConfirmDetails()}");
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
                Errors err = new Errors();
                float input = 0f;
                while (true)
                {
                    Console.Write(prompt);
                    try
                    {
                        input = Convert.ToSingle(Console.ReadLine());
                        if (input <= 0)
                        {
                            err.PrintError(ErrorType.Info, "Invalid Number");
                        }
                        else { break; }
                    }
                    catch (FormatException e)
                    {
                        err.PrintError(ErrorType.Warning, e.Message);
                    }
                }
                return input;
            }
            int GetValidInt(string prompt)
            {
                Errors err = new Errors();
                int input = 0;
                while (true)
                {
                    Console.Write(prompt);
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input <= 0)
                        {
                            err.PrintError(ErrorType.Info, "Invalid Input");
                        }
                        else { break; }
                    }
                    catch (FormatException e)
                    {
                        err.PrintError(ErrorType.Warning, e.Message);
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
                        err.PrintError(ErrorType.Info, "Invalid Input.Enter 'y' or 'n'");
                    }
                }
            }
            int GetValidYear(string prompt)
            {
                Errors err = new Errors();
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
                            err.PrintError(ErrorType.Info, "Invalid Year");
                        }
                    }
                    catch (FormatException e)
                    {
                        err.PrintError(ErrorType.Warning, e.Message);
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
                        err.PrintError(ErrorType.Info, "Invalid Transmission. Enter 'Automatic' or 'Manual'");
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
                        err.PrintError(ErrorType.Info, "Invalid Fuel Input. Enter 'Diesel', 'Petrol' or 'Electric'");
                    }
                }
            }
            decimal GetValidDecimal(string prompt)
            {
                Errors err = new Errors();
                decimal input = 0m;
                while (true)
                {
                    try
                    {
                        input = Convert.ToDecimal(Console.ReadLine());
                        if (input <= 0)
                        {
                            err.PrintError(ErrorType.Info, "Invalid Amount");
                        }
                        else { break; }
                    }
                    catch (FormatException e)
                    {
                        err.PrintError(ErrorType.Warning, e.Message);
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

                // Validates input 
                int id = GetValidInt("Enter Vehicle ID: ");

                // Checks collection for vehicle ID.
                if (vehicles.ContainsKey(id))
                {
                    // Displays selected vehicles information.
                    Console.WriteLine($"{vehicles[id].GetMake()} || {vehicles[id].GetModel()} || {vehicles[id].GetYear()} || £{vehicles[id].GetRate()} || {vehicles[id].GetTransmission()}");
                    
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

            void CmdValidInt(string cmdInput, out string? errorMsg, out int? validNum)
            {
                int input = 0;
                errorMsg = null;
                validNum = null;
                Errors err = new Errors();

                try
                {
                    input = Convert.ToInt32(cmdInput);
                    if (input <= 0)
                    {
                        errorMsg = err.PrintErrorString(ErrorType.Info) + "Invalid Amount";
                    }
                    else
                    {
                        validNum = input;
                    }
                }
                catch (FormatException e) 
                {
                    errorMsg = err.PrintErrorString(ErrorType.Warning) + e.Message;
                }
            }
            void CmdValidDecimal(string cmdInput, out string? errorMsg, out decimal? validNum)
            {
                decimal input = 0;
                errorMsg = null;
                validNum = null;
                Errors err = new Errors();

                try
                {
                    input = Convert.ToDecimal(cmdInput);
                    if (input <= 0)
                    {
                        errorMsg = err.PrintErrorString(ErrorType.Info) + "Invalid Amount";
                    }
                    else
                    {
                        validNum = input;
                    }
                }
                catch (FormatException e)
                {
                    errorMsg = err.PrintErrorString(ErrorType.Warning) + e.Message;
                }
            }
            void CmdCheckFloat(string cmdInput, out string? errorMsg, out float? validNum)
            {
                float input = 0f;
                errorMsg = null;
                validNum = null;
                Errors err = new Errors();

                try
                {
                    input = Convert.ToSingle(cmdInput);
                    if (input <= 0)
                    {
                        errorMsg = err.PrintErrorString(ErrorType.Info) + "Invalid Amount";
                    }
                    else
                    {
                        validNum = input;
                    }
                }
                catch (FormatException e)
                {
                    errorMsg = err.PrintErrorString(ErrorType.Warning) + e.Message;
                }
            }
            void CmdCheckBool(string cmdInput, out string? errorMsg, out bool? validOutput)
            {
                errorMsg = null;
                validOutput = null;

                if (cmdInput == "y" || cmdInput == "yes")
                {
                    validOutput = true;
                }
                else if (cmdInput == "n" || cmdInput == "no")
                {
                    validOutput = false;
                }
                else
                {
                    Errors err = new Errors();
                    errorMsg = err.PrintErrorString(ErrorType.Info) + "Invalid Input: Enter y / yes or n / no";
                }
            }

            void CmdAddVehicle(string[] newV)
            {
                // If an array item has a / it is replaced with a space. New items are now in a List collection. 
                List<string> newVehicle = newV.Select(n => n.Replace('/', ' ')).ToList();
                List<string> errorOutput = new List<string>();

                int newKey = vehicles.Count() == 0 ? 1 : vehicles.Keys.Max() + 1;

                string? errorMsg;
                int? checkInt;
                decimal? checkDecimal;
                int validYear = 0;
                int validSeatNum = 0;
                decimal validRate = 0m;

                CmdValidInt(newVehicle[3], out errorMsg, out checkInt);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkInt != null)
                {
                    if (checkInt.Value >= DateTime.Now.Year - 30 && checkInt.Value <= DateTime.Now.Year)
                    {
                        validYear = checkInt.Value;
                    }
                    else
                    {
                        Errors err = new Errors();
                        errorOutput.Add(err.PrintErrorString(ErrorType.Info) + "Invalid Year");
                    }
                }

                CmdValidInt(newVehicle[6], out errorMsg, out checkInt);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkInt != null) validSeatNum = checkInt.Value;

                CmdValidDecimal(newVehicle[4], out errorMsg, out checkDecimal);
                if (errorMsg != null) errorOutput.Add(errorMsg);
                else if (checkDecimal != null) validRate = checkDecimal.Value;

                if (newVehicle[0].ToUpper() == "C" && newVehicle.Count() == 9)
                {
                    int validBootCap = 0;
                    CmdValidInt(newVehicle[8], out errorMsg, out checkInt);
                    if (errorMsg != null) errorOutput.Add(errorMsg);
                    else if (checkInt != null) validBootCap = checkInt.Value;

                    if (errorOutput.Count == 0)
                    {
                        Car cmdCar = new Car(newVehicle[1], newVehicle[2], validYear, validRate, newVehicle[5], validSeatNum, newVehicle[7], validBootCap);
                        vehicles.Add(newKey, cmdCar);
                        Console.WriteLine($"Car Added - {cmdCar.ConfirmDetails()}");
                        SerializeDictionary();
                    }
                    else
                    {
                        Console.WriteLine($"New car fail");
                        foreach (string error in errorOutput)
                        {
                            Console.WriteLine(error);
                        }
                    }
                }
                else if (newVehicle[0].ToUpper() == "V" && newVehicle.Count() == 12)
                {
                    float? checkFloat;
                    float validLoadCap = 0f;
                    CmdCheckFloat(newVehicle[8], out errorMsg, out checkFloat);
                    if (errorMsg != null) errorOutput.Add(errorMsg);
                    else if (checkFloat != null) validLoadCap = checkFloat.Value;

                    float validLength = 0f;
                    CmdCheckFloat(newVehicle[9], out errorMsg, out checkFloat);
                    if (errorMsg != null) errorOutput.Add(errorMsg);
                    else if (checkFloat != null) validLength = checkFloat.Value;
                    
                    float validWidth = 0f;
                    CmdCheckFloat(newVehicle[10], out errorMsg, out checkFloat);
                    if (errorMsg != null) errorOutput.Add(errorMsg);
                    else if (checkFloat != null) validWidth = checkFloat.Value;

                    float validHeight = 0f;
                    CmdCheckFloat(newVehicle[11], out errorMsg, out checkFloat);
                    if (errorMsg != null) errorOutput.Add(errorMsg);
                    else if (checkFloat != null) validHeight = checkFloat.Value;

                    if (errorOutput.Count == 0)
                    {
                        Van cmdVan = new Van(newVehicle[1], newVehicle[2], validYear, validRate, newVehicle[5], validSeatNum, newVehicle[7], validLoadCap, validLength, validWidth, validHeight);
                        vehicles.Add(newKey, cmdVan);
                        Console.WriteLine($"Van Added - {cmdVan.ConfirmDetails()}");
                        SerializeDictionary();
                    }
                    else
                    {

                        foreach (string error in errorOutput)
                        {
                            Console.WriteLine(error);
                        }
                        return;
                    }
                }
                else if (newVehicle[0].ToUpper() == "M" && newVehicle.Count() == 11)
                {
                    int validCC = 0;
                    CmdValidInt(newVehicle[8], out errorMsg, out checkInt);
                    if (errorMsg != null) errorOutput.Add(errorMsg);
                    else if (checkInt != null) validCC = checkInt.Value;

                    bool? checkBool;
                    bool validStorage = false;
                    CmdCheckBool(newVehicle[9].ToLower(), out errorMsg, out checkBool);
                    if (errorMsg != null) errorOutput.Add(errorMsg);
                    else if (checkBool != null) validStorage = checkBool.Value;

                    bool validProtection = false;
                    CmdCheckBool(newVehicle[10].ToLower(), out errorMsg, out checkBool);
                    if (errorMsg != null) errorOutput.Add(errorMsg);
                    else if (checkBool != null) validProtection = checkBool.Value;

                    if (errorOutput.Count == 0)
                    {
                        Motorcycle cmdMotor = new Motorcycle(newVehicle[1], newVehicle[2], validYear, validRate, newVehicle[5], validSeatNum, newVehicle[7], validCC, validStorage, validProtection);
                        vehicles.Add(newKey, cmdMotor);
                        Console.WriteLine($"Motorcycle Added - {cmdMotor.ConfirmDetails()}");
                        SerializeDictionary();
                    }
                    else
                    {
                        foreach (string error in errorOutput)
                        {
                            Console.WriteLine(error);
                        }
                        return;
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown vehicle type: '{newVehicle[0]}' or Incorrect number of inputs");
                    return;
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
                        Console.WriteLine("--add: Requires (VehicleType Make Model Year DailyRate Transmission + Vehicle specific details)");
                        Console.WriteLine("  Example Format: (--add Car Land/Rover Defender 2019 230.23 Hybrid 100)");
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
                        if (args.Length >= 6)
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
                        Console.WriteLine("Enter --help for assistance");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input: --help for assistance");
            }
        }
    }
}
