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

        public void SetUser(int id, string fName, string lName, string email, string password)
        {
            UserID = id;
            FirstName = fName;
            LastName = lName;
            Email = email;
            Password = password;
        }
        public bool VerifyLogin(int id, string password)
        {
            Errors err = new Errors();
            if (Program.users.ContainsKey(id))
            {
                if (Program.users[id].GetPassword() == password)
                {
                    return true;
                }
                else
                {
                    err.PrintError(ErrorType.Warning, "Incorrect Password");
                    return false;
                }
            }
            else
            {
                err.PrintError(ErrorType.Info, $"ID: '{id}' not found.");
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
        public string GetPassword() { return Password; }
        public void ToFile()
        {

        }
    }
}
