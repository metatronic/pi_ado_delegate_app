using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary;

namespace DBPasswordApp
{
    class Program
    {
        UserDataStore dataStore = new UserDataStore(ConfigurationManager.ConnectionStrings["TrainingConnection"].ConnectionString);
        UserDataStoreDisconnected dataStoreDisconnected = new UserDataStoreDisconnected(ConfigurationManager.ConnectionStrings["TrainingConnection"].ConnectionString);
        static void Main(string[] args)
        {
            Program program = new Program();
            //Action delegate example
            Action<Roles> PrintRole = program.DisplayRole;

            Console.WriteLine("Enter username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password");
            string password = Console.ReadLine();
            //Func delegate example
            Func<string, string, Roles> ValidateUser = program.dataStore.ValidateUser;
            try
            {
                Roles userrole = ValidateUser(username, password);
                PrintRole(userrole);
            }
            catch (IncorrectCredentialException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("------------------------------------------------------");

            Console.WriteLine("Enter username");
            string usernamedc = Console.ReadLine();
            Console.WriteLine("Enter password");
            string passworddc = Console.ReadLine();
            Func<string, string, Roles> ValidateUserdc = program.dataStoreDisconnected.ValidateUser;
            try
            {
                Roles userrole = ValidateUserdc(usernamedc, passworddc);
                PrintRole(userrole);
            }
            catch (IncorrectCredentialException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        void DisplayRole(Roles role)
        {
            Console.WriteLine("The user role is " + role);
        }
    }
}
