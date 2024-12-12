using Microsoft.VisualBasic.FileIO;
using Spectre.Console;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VehicleRentalApp
{
    public class Program
    {
        // Global access to the menu and validation class member functions.
        public readonly static Menus menu = new Menus();
        private readonly static Validation validate = new Validation();
        public static void AddingData()
        {
            vehicles.Add(13, new Car(3, "Renault", "Captur", 2019, 20m, "Manual", 5, "Petrol", 100));
            vehicles.Add(14, new Van(5, "VW", "Caddy", 2021, 104.48m, "Manual", 2, "Petrol", 600f, 1.7f, 1.55f, 1.25f));
            vehicles.Add(15, new Motorcycle(4, "BMW", "R1300 GS", 2024, 265m, "Manual", 1, "Petrol", 1300, true, false));
            vehicles.Add(16, new Van(1, "Citroen", "Berlingo", 2020, 58.45m, "Manual", 2, "Petrol", 1361f, 2f, 2.1f, 1.79f));
            vehicles.Add(17, new Car(4, "Mercedes", "E Class", 2024, 42.83m, "Automatic", 5, "Electric", 100));
            vehicles.Add(18, new Motorcycle(4, "BMW", "R1200 TS", 2023, 200m, "Manual", 1, "Petrol", 1300, true, false));
            vehicles.Add(19, new Van(1, "Citroen", "Berlingo", 2019, 505m, "Manual", 2, "Petrol", 900f, 1.8f, 1.9f, 1.79f));
            vehicles.Add(20, new Car(5, "Ford", "Focus", 2019, 50m, "Manual", 5, "Petrol", 100));
            vehicles.Add(21, new Car(2, "Ford", "Fiesta", 2018, 60m, "Manual", 5, "Petrol", 76));
            vehicles.Add(22, new Motorcycle(4, "KTM", "SX", 2024, 270m, "Automatic", 1, "Diesel", 500, true, true));
            vehicles.Add(23, new Motorcycle(2, "KTM", "Enduro", 1990, 150m, "Manual", 1, "Petrol", 200, true, true));
            vehicles.Add(24, new Van(3, "Ford", "Transit", 2022, 100.50m, "Manual", 2, "Petrol", 600f, 1.45f, 1.5f, 1.6f));
            SerializeDictionary();

            Parallel.For(25, 1000000, i =>
            {
                vehicles.Add(i, new Car(3, "Renault", "Captur", 2019, 20m, "Manual", 5, "Petrol", 100));
            });

            //users.Add(1, new Users(1, "Mark", "Summers", "Mark@Summers.com", "123Hello"));
            //users.Add(2, new Users(2, "June", "Thomas", "June@Thomas.com", "123Hello"));
            //users.Add(3, new Users(3, "Ann", "Marie", "Ann@Marie.com", "123Hello"));
            //users.Add(4, new Users(4, "Dean", "Winchester", "Dean@Winchester.com", "123Hello"));
            //users.Add(5, new Users(5, "Harry", "Miller", "Harry@Miller.com", "123Hello"));
            //WriteBinary();
        }
        public static Dictionary<int, Users> userCache = new Dictionary<int, Users>();

        public static Dictionary<int, Users> users = ReadBinary("Users.bin");
        public static Dictionary<int, Vehicle> vehicles = new Dictionary<int, Vehicle>();
        private static Dictionary<int, Vehicle> LoadFiles(string filePath)
        {
            if (File.Exists(filePath))
            {
                var serializeOptions = new JsonSerializerOptions();
                serializeOptions.Converters.Add(new VehicleConverter());
                string fromJsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Dictionary<int, Vehicle>>(fromJsonString, serializeOptions);
            }
            else
            {
                return new Dictionary<int, Vehicle>();
            }
        }
        public static void SerializeDictionary() // Serialises the vehicle dictionary and stores in a JSON file.
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
        public static void WriteBinary() // Writes to the binary file that contains user data.
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open("Users.bin", FileMode.OpenOrCreate)))
            {
                bw.Write(users.Count());
                foreach (var user in users)
                {
                    bw.Write(user.Value.GetUserID());
                    bw.Write(user.Value.GetFirstName());
                    bw.Write(user.Value.GetLastName());
                    bw.Write(user.Value.GetEmail());
                    bw.Write(user.Value.GetPassword());
                    if (user.Value.GetOwnVehicles().Count() == 0 || user.Value.GetOwnVehicles().Count() == null)
                    {
                        bw.Write(0);
                    }
                    else
                    {
                        bw.Write(user.Value.GetOwnVehicles().Count());
                        foreach (int id in user.Value.GetOwnVehicles())
                        {
                            bw.Write(id);
                        }
                    }
                    if (user.Value.GetRentedVehicles().Count() == null || user.Value.GetRentedVehicles().Count() == 0)
                    {
                        bw.Write(0);
                    }
                    else
                    {
                        bw.Write(user.Value.GetRentedVehicles().Count());
                        foreach (int id in user.Value.GetRentedVehicles())
                        {
                            bw.Write(id);
                        }
                    }
                }
            }
        }
        public static Dictionary<int, Users> ReadBinary(string filePath) // Reads from the user binary file and outputs it into a dictionary.
        {
            Dictionary<int, Users> users = new Dictionary<int, Users>();
            if (File.Exists(filePath))
            {
                using (BinaryReader br = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    int userCount = br.ReadInt32();
                    for (int i = 0; i < userCount; i++)
                    {
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            int id = br.ReadInt32();
                            string fName = br.ReadString();
                            string lName = br.ReadString();
                            string email = br.ReadString();
                            string password = br.ReadString();

                            Users user = new Users(id, fName, lName, email, password);

                            int ownCount = br.ReadInt32();
                            for (int o = 0; o < ownCount; o++)
                            {
                                user.UserAddVehicle(br.ReadInt32());
                            }

                            int rentCount = br.ReadInt32();
                            for (int r = 0; r < rentCount; r++)
                            {
                                user.UserRentVehicle(br.ReadInt32());
                            }

                            users.Add(id, user);
                        }
                    }
                }
            }
            else
            {
                return new Dictionary<int, Users>();
            }

            return users;
        }
        static void Main(string[] args) // Handles command line arguments.
        {
            if (args.Length == 0 || args[0].ToLower() == "--menu") 
            {
                using (BinaryReader br = new BinaryReader(File.Open("VehiclesBinary.bin", FileMode.Open)))
                {
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        string type = br.ReadString();
                        int id = br.ReadInt32();
                        if (type == "Car")
                        {
                            Car newCar = new Car();
                            newCar.ReadingVehicles(br);
                            vehicles.Add(id, newCar);
                        }
                        else if (type == "Van")
                        {
                            Van newVan = new Van();
                            newVan.ReadingVehicles(br);
                            vehicles.Add(id, newVan);
                        }
                        else if (type == "Motorcycle")
                        {
                            Motorcycle newMotor = new Motorcycle();
                            newMotor.ReadingVehicles(br);
                            vehicles.Add(id, newMotor);
                        }
                    }
                }

                //using (BinaryWriter bw = new BinaryWriter(File.Open("vehiclesBinary.bin", FileMode.OpenOrCreate)))
                //{
                //    foreach (var veh in vehicles)
                //    {
                //        veh.Value.WritingVehicles(bw, veh.Key);
                //    }
                //}
                menu.GetBeforeLogin();
            }
            else if (args.Length >= 1)
            {
                CmdArguments cmd = new CmdArguments();
                switch (args[0].ToLower())
                {
                    case "--help":
                        Console.WriteLine("--help:");
                        Console.WriteLine("--rent : Requires Vehicle ID and your username and password");
                        Console.WriteLine("         Format: --rent 2 username=password");
                        Console.WriteLine("--return : Requires Vehicle ID and your username and password");
                        Console.WriteLine("         Format: --return 2 username=password");
                        Console.WriteLine();
                        Console.WriteLine("--del or --delete: Requires Vehicle ID and your username and password");
                        Console.WriteLine("         Format: --del 2 username=password");
                        Console.WriteLine();
                        Console.WriteLine("--add: VehicleType, Make, Model, Year, DailyRate, Transmission, Number of Seats, Fuel Type, + Vehicle specific details, and your username and password");
                        Console.WriteLine("         Format: --add C Land/Rover Defender 2019 230.23 Automatic 5 Petrol 100 username=password");
                        Console.WriteLine("         Format: --add V VW Caddy 2021 104.48m Manual 2 Petrol 600 1.7 1.55 1.25 username=password");
                        Console.WriteLine("         Format: --add M KTM SX 2024 270m Automatic 1 Diesel 500 true true username=password");
                        break;
                    case "--rent":
                        cmd.CmdRentVehicle(args.Skip(1).ToArray());
                        break;
                    case "--return":
                        cmd.CmdReturnVehicle(args.Skip(1).ToArray());
                        break;
                    case "--del":
                        cmd.CmdDelVehicle(args.Skip(1).ToArray());
                        break;
                    case "--delete":
                        cmd.CmdDelVehicle(args.Skip(1).ToArray());
                        break;
                    case "--add":
                        if (args.Length == 11 || args.Length == 14 || args.Length == 13)
                        {
                            string[] newVehicles = args.Skip(1).ToArray();
                            cmd.CmdAddVehicle(newVehicles);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect Input: Replace any spaces in names with a /.");
                        }
                        break;
                    default:
                        Console.WriteLine($"Unknown Command: '{args[0]}'");
                        Console.WriteLine("Enter --help for assistance");
                        break;
                }
            }
        } 
        public static void ViewCars()
        {
            Console.Clear();
            Console.WriteLine("ALL CARS");

            IEnumerable<KeyValuePair<int, Vehicle>> allCars = vehicles.Take(100).Where(ac => ac.Value.GetVType() == "Car" && ac.Value.Status == "Available");
            if (allCars.Count() == 0)
            {
                Console.WriteLine("No Cars Available");
            }
            else
            {
                TableDisplay cars = new TableDisplay();
                cars.DisplayCars(allCars);
            }

            Console.Write("Press enter to go back >> ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            if (userCache.Count() == 1) { menu.GetMainMenu(); }
            else { menu.GetBeforeLogin(); }
        }
        public static void ViewVans()
        {
            Console.Clear();
            Console.WriteLine("ALL VANS");

            IEnumerable<KeyValuePair<int, Vehicle>> allVans = vehicles.Where(ac => ac.Value.GetVType() == "Van" && ac.Value.Status == "Available");
            if (allVans.Count() == 0)
            {
                Console.WriteLine("No Vans Available");
            }
            else
            {
                TableDisplay vans = new TableDisplay(); 
                vans.DisplayVans(allVans);
            }

            Console.Write("Press enter to go back >> ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            if (userCache.Count() == 1) { menu.GetMainMenu(); }
            else { menu.GetBeforeLogin(); }
        }
        public static void ViewMotors()
        {
            Console.Clear();
            Console.WriteLine("ALL MOTORCYCLES");

            IEnumerable<KeyValuePair<int, Vehicle>> allMotors = vehicles.Where(ac => ac.Value.GetVType() == "Motorcycle" && ac.Value.Status == "Available");
            if (allMotors.Count() == 0)
            {
                Console.WriteLine("No Motorcycles Available");
            }
            else
            {
                TableDisplay motors = new TableDisplay();
                motors.DisplayMotors(allMotors);
            }

            Console.Write("Press enter to go back >> ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            if (userCache.Count() == 1) { menu.GetMainMenu(); }
            else { menu.GetBeforeLogin(); }
        }
        public static void SearchVehicles()
        {
            Console.Clear();
            Console.WriteLine("SEARCH VEHICLES (Use commas to seperate)");
            Console.Write("Search: ");
            List<string> searching = Console.ReadLine().Split(", ").ToList();

            IEnumerable<KeyValuePair<int, Vehicle>> query = vehicles.AsParallel().Where(q =>
                q.Value.Status == "Available" &&
                searching.All(s =>
                    q.Value.GetModel().Contains(s, StringComparison.OrdinalIgnoreCase) ||
                    q.Value.GetMake().Contains(s, StringComparison.OrdinalIgnoreCase) ||
                    q.Value.GetTransmission().Contains(s, StringComparison.OrdinalIgnoreCase) || 
                    q.Value.GetFuel().Contains(s, StringComparison.OrdinalIgnoreCase) ||
                    q.Value.GetYear().ToString().Contains(s)
                )
            );
            TableDisplay searchDisplay = new TableDisplay();
            searchDisplay.DisplayVehicles(query);

            Console.Write("Press enter to go back >> ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            if (userCache.Count() == 1) { menu.GetMainMenu(); }
            else { menu.GetBeforeLogin(); }
        }
        public static void Login()
        {
            Console.Clear();
            Console.WriteLine("LOGIN - (Username is your ID)");
            int id = validate.GetValidInt("Username: ");
            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (users.ContainsKey(id))
            {
                if (users[id].GetPassword() == password)
                {
                    userCache.Add(id, users[id]);
                    menu.GetMainMenu();
                    return;
                }
                else
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Warning, "Incorrect Password");
                }
            }
            else
            {
                Errors err = new Errors();
                err.PrintError(ErrorType.Info, $"ID: '{id}' not found.");
            }

            bool tryAgain = validate.GetValidBool("Would you like to try again? [y / n]: ");
            if (tryAgain) { Login(); }
            else { menu.GetBeforeLogin(); }
        }
        public static void Register()
        {
            Console.Clear();
            Console.WriteLine("REGISTER");
            Console.Write("First Name: ");
            string fName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lName = Console.ReadLine();
            string email = validate.InputMatch("Email: ", "Confirm Email: ");
            string password = validate.InputMatch("Password: ", "Confirm Password: ");

            int newKey = 0;
            if (users.Count == 0)
            {
                newKey = 1;
            }
            else
            {
                newKey = users.Keys.Max() + 1;
            }
            users.Add(newKey, new Users(newKey, fName, lName, email, password));
            Console.WriteLine($"Account Created use ID: {newKey} as your username.");

            bool login = validate.GetValidBool("Login? [y / n]: ");
            if (login) { Login(); }
            else { menu.GetBeforeLogin(); } 
        }
        public static void AddVehicles()
        {
            Console.Clear();
            Console.WriteLine("ADD VEHICLES");
            Console.WriteLine("[C] = Car | [V] = Van | [M] = Motorcycle");
            Console.Write("Vehicle Type: ");
            string typeInput = "";
            switch (Console.ReadLine().ToUpper())
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

            int yr = validate.GetValidYear("Year: ");
            decimal rate = validate.GetValidDecimal("Daily Rate: ");
            string transm = validate.GetValidTransmission("Transmission Type [Manual / Automatic]: ");
            int numOfSeats = validate.GetValidInt("Number of Seats: ");
            string fuelType = validate.GetValidFuel("Fuel Type [Diesel / Petrol / Electric]: ");

            int ownerId = 0;
            if (userCache.Count() == 1)
            {
                Users owner = userCache.Values.First();
                ownerId = owner.GetUserID();
            }
            else
            {
                Console.WriteLine("User must be logged in");
                Thread.Sleep(1000);
                menu.GetBeforeLogin();
            }

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
                int boot = validate.GetValidInt("Boot Capacity (In Litres): ");

                Car newCar = new Car(ownerId, make, model, yr, rate, transm, numOfSeats, fuelType, boot);
                Console.WriteLine($"\nCar Added - {newCar.ConfirmDetails()}");
                if (validate.GetValidBool("Add Car [ y / n ]: "))
                {
                    vehicles.Add(newKey, newCar);
                    SerializeDictionary();
                    userCache[ownerId].UserAddVehicle(newKey);
                }
            }
            else if (typeInput == "Van")
            {
                float loadCap = validate.GetValidFloat("Load Capacity(Kg): ");
                Console.WriteLine("Internal Measurements");
                float length = validate.GetValidFloat("Lenth (In metres): ");
                float width = validate.GetValidFloat("Width (In metres): ");
                float height = validate.GetValidFloat("Height (In metres): ");

                Van newVan = new Van(ownerId, make, model, yr, rate, transm, numOfSeats, fuelType, loadCap, length, width, height);
                Console.WriteLine($"\nVan Added - {newVan.ConfirmDetails()}");
                if (validate.GetValidBool("Add Van [ y / n ]: "))
                {
                    vehicles.Add(newKey, newVan);
                    SerializeDictionary();
                    userCache[ownerId].UserAddVehicle(newKey);
                }
            }
            else if (typeInput == "Motorcycle")
            {
                int cc = validate.GetValidInt("CC: ");
                bool storage = validate.GetValidBool("Storage [y / n]: ");
                bool protection = validate.GetValidBool("With Protection (Helmet, etc) [y / n]: ");

                Motorcycle newMot = new Motorcycle(ownerId, make, model, yr, rate, transm, numOfSeats, fuelType, cc, storage, protection);
                Console.WriteLine($"\nMotorcycle Added - {newMot.ConfirmDetails()}");
                if (validate.GetValidBool("Add Motorcycle [ y / n ]: "))
                {
                    vehicles.Add(newKey, newMot);
                    SerializeDictionary();
                    userCache[ownerId].UserAddVehicle(newKey);
                }
            }

            Console.Write("Press enter to go back >> ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            menu.GetMainMenu();
        }
        public static void DeleteVehicles()
        {
            Console.Clear();
            Console.WriteLine("DELETE VEHICLES");

            if (userCache.Count() == 1)
            {
                int inputID = validate.GetValidInt("Vehicle ID: ");
                Users owner = userCache.Values.First();
                int ownerId = owner.GetUserID();

                bool vehicleOwner = owner.CheckOwnVehicles(inputID);
                bool vehicleExists = vehicles.ContainsKey(inputID);

                if (vehicleOwner && vehicleExists)
                {
                    Console.WriteLine(vehicles[inputID].ConfirmDetails());
                    bool confirmDel = validate.GetValidBool("Is this the vehicle you want to delete [y/n]: ");

                    if (confirmDel)
                    {
                        vehicles.Remove(inputID);
                        owner.UserDelVehicle(inputID);
                        WriteBinary();
                        SerializeDictionary();
                        Console.WriteLine($"Vehicle with ID {inputID} has been deleted.");
                    }
                    else
                    {
                        Console.WriteLine($"Vehicle with ID {inputID} has not been deleted.");
                    }
                }
                else
                {
                    Errors err = new Errors();
                    if (!vehicleOwner) { err.PrintError(ErrorType.Warning, "You cannot delete a vehicle you do not own."); }
                    else if (!vehicleExists) { err.PrintError(ErrorType.Info, $"Vehicle ID: '{inputID}' not found."); }
                }
            }
            else
            {
                Console.WriteLine("You must be logged in to delete a vehicle.");
                Thread.Sleep(1000);
                menu.GetBeforeLogin();
            }

            Console.Write("Press enter to go back >> ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            menu.GetMainMenu();
        }
        public static void RentVehicle()
        {
            Console.Clear();
            Console.WriteLine("RENT VEHICLES");

            int id = validate.GetValidInt("Enter Vehicle ID: ");

            if (vehicles.ContainsKey(id))
            {
                if (vehicles[id].Status == "Available")
                {
                    Console.WriteLine(vehicles[id].ConfirmDetails());
                    bool confimAction = validate.GetValidBool($"Is this the vehicle you want to rent [y/n]: ");
                    if (confimAction)
                    {
                        vehicles[id].Status = "Rented";
                        Users user = userCache.Values.First();
                        user.UserRentVehicle(id);
                        SerializeDictionary();
                        WriteBinary();
                    }
                }
                else
                {
                    Errors err = new Errors();
                    err.PrintError(ErrorType.Info, $"Vehicle {id} is not available");
                }
            }
            else
            {
                Errors err = new Errors();
                err.PrintError(ErrorType.Error, $"Vehicle '{id}' not found.");
            }

            Console.Write("Press enter to go back >> ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            menu.GetMainMenu();
        }
        public static void ReturnVehicle()
        {
            Console.Clear();
            Console.WriteLine("RETURN VEHICLES");

            int id = validate.GetValidInt("Enter Vehicle ID: ");

            if (vehicles.ContainsKey(id))
            {
                Users user = userCache.Values.First();
                bool hasRented = user.CheckRentedVehicles(id);
                if (vehicles[id].Status == "Rented" && hasRented)
                {
                    Console.WriteLine(vehicles[id].ConfirmDetails());
                    bool confimAction = validate.GetValidBool($"Is this the vehicle you want to return [y/n]: ");
                    if (confimAction)
                    {
                        vehicles[id].Status = "Available";
                        user.UserReturnVehicle(id);
                        SerializeDictionary();
                        WriteBinary();
                    }
                }
                else
                {
                    Errors err = new Errors();
                    if (!hasRented) { err.PrintError(ErrorType.Warning, $"You cannot return a vehicle you did not rent."); }
                    else if (vehicles[id].Status == "Available") { err.PrintError(ErrorType.Info, $"Vehicle {id} is already available"); }
                }
            }
            else
            {
                Errors err = new Errors();
                err.PrintError(ErrorType.Error, $"Vehicle '{id}' not found.");
            }

            Console.Write("Press enter to go back >> ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            menu.GetMainMenu();
        }
    }
}
