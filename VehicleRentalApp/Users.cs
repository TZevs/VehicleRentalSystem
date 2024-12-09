using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    public class Users
    {
        private int UserID;
        private string FirstName;
        private string LastName;
        private string Email;
        private string Password;
        private List<int> OwnVehicles;
        private List<int> RentedVehicles;

        public Users(int id, string fName, string lName, string email, string password)
        {
            UserID = id;
            FirstName = fName;
            LastName = lName;
            Email = email;
            Password = password;
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

        // Checks if vehicle id is stored in either list. 
        public bool CheckOwnVehicles(int id)
        {
            if (OwnVehicles == null || !OwnVehicles.Contains(id)) return false;
            else return true;
        } 
        public bool CheckRentedVehicles(int id)
        {
            if (RentedVehicles == null || !RentedVehicles.Contains(id)) return false;
            else return true;
        }
    }
}
