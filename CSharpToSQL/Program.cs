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
            var connStr = @"server = STUDENT-FLEX\SQLEXPRESS; database = prsdb; trusted_connection = true";
            // instead of trusted connection, "uid = userName; pwd = passeord;

            var Connection = new SqlConnection(connStr);
            Connection.Open();
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open.");
                return;
            }

            var sql = "select * from users;";

            var cmd = new SqlCommand(sql, Connection);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)  // (reader.HasRows == false)
            {
                Console.WriteLine("Result set has no rows.");
                Connection.Close();
                return;
            }

            var users = new User[10];  // create an array to hold the user data were pulling from SQL
            var index = 0;
            
            while (reader.Read())
            {
                var user = new User();
                user.Id = (int)reader["Id"];
                user.Username = (string)reader["Username"];
                user.Firstname = (string)reader["First_Name"];
                user.Lastname = (string)reader["Last_Name"];
                //var Fullname = $"{Firstname} {Lastname}";
                user.Phone = reader["Phone"] == DBNull.Value ? null : (string)reader["Phone"];
                user.Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"];
                // read email col in sql == check for Null ? if true set null in C# : if false (not null) set data to variable          
                users[index++] = user;                

                Console.WriteLine($"Id = {user.Id}, Name = {user.Firstname} {user.Lastname}, User Name = {user.Username}, Phone = {user.Phone}");
                
            }
            Console.ReadKey();



            Connection.Close();
        }
    }
}
