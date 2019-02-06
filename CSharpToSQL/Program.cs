using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CSharpToSQL
{
    class Program
    {       // SQL- STUDENT-FLEX\SQLEXPRESS
        static void Main(string[] args)
        {
            var user = new User(0, "Batman", "Bruce", "Ben", "Afflack", "888-555-1234", "007@bond");
            //var returnCode = User.InsertUser(user);

            User[] users = User.GetAllUsers();

            foreach (var u in users)
            {
                if (u == null)
                {
                    continue;
                }
                //Console.WriteLine($"{u.Firstname} {u.Lastname}");
                Console.WriteLine(u);
            }

            const int ID = 9;
            
            User userpk = User.GetUserByPrimaryKey(ID);
            //Console.WriteLine($"{userpk.Firstname} {userpk.Lastname}");
            Console.WriteLine(userpk.ToString()); // calling default print

            userpk.Password = "ABCD";
            var updateSuccess = User.UpdateUser(userpk);
            if (updateSuccess)
            {
                Console.WriteLine("Update Success");
            }
            else
            {
                Console.WriteLine("Update Failed");
            }

            var deleteSuccess = User.DeleteUser(ID);
            if (!deleteSuccess)
            {     // if (deleteSuccess == false)
                {
                    Console.WriteLine("Delete Failed on non-existent Id");
                }


                Console.ReadKey();
            }


        }
    }
}




