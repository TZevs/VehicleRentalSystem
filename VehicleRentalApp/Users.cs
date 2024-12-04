using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Users
    {
        private int UserID;
        private string FirstName;
        private string LastName;
        private string Email;
        private string Username;
        private string Password;
        private List<int> OwnVehicles;
        private List<int> RentedVehicles;

        public Users(int id, string fName, string lName, string email, string username, string password)
        {
            UserID = id;
            FirstName = fName;
            LastName = lName;
            Email = email;
            Password = password;
        }
        public bool VerifyLogin(int id, string username, string password)
        {
            if (Program.users.ContainsKey(id))
            {
                if (Program.users[id].GetUsername() == username && Program.users[id].GetPassword() == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
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
        public string GetUsername() { return Username; }
        public string GetPassword() { return Password; }
    }
}
