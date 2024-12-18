using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace VehicleRentalApp
{
    public class Users
    {
        // JsonInclude attribute used to tell the JSON serialiser knows to store these variables in the file.
        [JsonInclude] private int UserID;
        [JsonInclude] private string FirstName;
        [JsonInclude] private string LastName;
        [JsonInclude] private string Email;
        [JsonInclude] private string Password;
        [JsonInclude] private List<int> OwnVehicles = new List<int>(); // To store the id / key of the users vehicles.
        [JsonInclude] private List<int> RentedVehicles = new List<int>(); // To store the id / key of the vehicles the user has rented.

        // JsonConstructor attribute used to tell the JSON deserializer to use the empty constructor to intialise objects.
        [JsonConstructor]
        public Users() { }
        public Users(int id, string fName, string lName, string email, string password)
        {
            UserID = id;
            FirstName = fName;
            LastName = lName;
            Email = email;
            Password = password;
            // Both lists are empty by defualt
        }

        // Adding and removing items from the owned and rented vehicle lists. 
        public void UserAddVehicle(int vehicleID)
        {
            OwnVehicles.Add(vehicleID);
        }
        public void UserDelVehicle(int vehicleID)
        {
            OwnVehicles.Remove(vehicleID);
        }
        public void UserRentVehicle(int vehicleID)
        {
            RentedVehicles.Add(vehicleID);
        }
        public void UserReturnVehicle(int vehicleID)
        {
            RentedVehicles.Remove(vehicleID);
        }

        // Getters
        public string GetPassword() { return Password; }
        public int GetUserID() { return UserID; }   
        public string GetFirstName() { return FirstName; }
        public string GetLastName() { return LastName; }
        public string GetEmail() { return Email; }
        public List<int> GetOwnVehicles() { return OwnVehicles; }
        public List<int> GetRentedVehicles() {return RentedVehicles;}

        // Checks the lists for if they contain the argument passed in.
        public bool CheckOwnVehicles(int id)
        {
            if (!OwnVehicles.Contains(id)) return false;
            else return true;
        }
        public bool CheckRentedVehicles(int id)
        {
            if (!RentedVehicles.Contains(id)) return false;
            else return true;
        }
    }
}
