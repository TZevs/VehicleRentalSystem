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
        public void UserAddVehicle(int vehicleID)
        {
            OwnVehicles.Add(vehicleID);
        }
        public void UserDelVehicle(int vehicleID)
        {
            if (OwnVehicles.Contains(vehicleID))
            {
                OwnVehicles.Remove(vehicleID);
            }
            else
            {
                Console.WriteLine("You cannot delete a vehicle you do not own!");
            }
        }
        public void UserRentVehicle(int vehicleID)
        {
            RentedVehicles.Add(vehicleID);
        }
        public string GetPassword() { return Password; }
        public int GetUserID() { return UserID; }   
        public string GetFirstName() { return FirstName; }
        public string GetLastName() { return LastName; }
        public string GetEmail() { return Email; }
        public List<int> GetOwnVehicles() { return OwnVehicles; }
        public List<int> GetRentedVehicles() {return RentedVehicles;}
    }
}
