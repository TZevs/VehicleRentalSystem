namespace VehicleRentalApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Vehicle> vehicles = new Dictionary<int, Vehicle>();
            
            void MainMenu()
            {
                Console.WriteLine("VEHICLE RENTAL - MAIN MENU");
                Console.WriteLine("[1] View Vehicles");
                Console.WriteLine("[2] Search Vehicles");
                Console.WriteLine("[3] Add Vehicle");
                Console.WriteLine("[4] Delete Vehicle");
                Console.WriteLine("[5] Exit");

                Console.Write("Enter Selection: ");
                int select = Convert.ToInt32(Console.ReadLine());
            }

            void ViewVehicles()
            {

            }
            void SearchVehicles()
            {

            }
            void AddVehicles()
            {

            }
            void DeleteVehicles()
            {

            }
        }
    }
}
