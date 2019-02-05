using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToSQL
{
    class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public static bool InsertUser(User user)
        {
            var connStr = @"server = STUDENT-FLEX\SQLEXPRESS; database = prsdb; trusted_connection = true";
            // instead of trusted connection, "uid = userName; pwd = passeord;

            var Connection = new SqlConnection(connStr);
            Connection.Open();
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open.");
                return false; // return false because method is a bool
            }

            var sql = $"Insert into Users (Username, Password, First_name, Last_Name, Phone, Email)" 
                + $" Values ('{user.Username}', '{user.Password}', '{user.Firstname}','{user.Lastname}', '{user.Phone}','{user.Email}')";

            var cmd = new SqlCommand(sql, Connection);
            var recsAfected = cmd.ExecuteNonQuery();
            Connection.Close();
            return recsAfected == 1;
            

        }

        public User() { }

        public static User GetUserByPrimaryKey(int Id)
        {
            var connStr = @"server = STUDENT-FLEX\SQLEXPRESS; database = prsdb; trusted_connection = true";
            // instead of trusted connection, "uid = userName; pwd = passeord;

            var Connection = new SqlConnection(connStr);
            Connection.Open();
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open.");
                return null;
            }

            var sql = $"select * from users where id = {Id} ;";

            var cmd = new SqlCommand(sql, Connection);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)  // (reader.HasRows == false)
            {
                Console.WriteLine("Result set has no rows.");
                Connection.Close();
                return null;
            }
            reader.Read();

            var user = new User();
            user.Id = (int)reader["Id"];
            user.Username = (string)reader["Username"];
            user.Firstname = (string)reader["First_Name"];
            user.Lastname = (string)reader["Last_Name"];
            //var Fullname = $"{Firstname} {Lastname}";
            user.Phone = reader["Phone"] == DBNull.Value ? null : (string)reader["Phone"];
            user.Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"];
            // read email col in sql == check for Null ? if true set null in C# : if false (not null) set data to variable          

            Connection.Close();
            return user;
        }



        public static User[] GetAllUsers()
        {
            var connStr = @"server = STUDENT-FLEX\SQLEXPRESS; database = prsdb; trusted_connection = true";
            // instead of trusted connection, "uid = userName; pwd = passeord;

            var Connection = new SqlConnection(connStr);
            Connection.Open();
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open.");
                return null;
            }

            var sql = "select * from users;";

            var cmd = new SqlCommand(sql, Connection);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)  // (reader.HasRows == false)
            {
                Console.WriteLine("Result set has no rows.");
                Connection.Close();
                return null;
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

            }
            Connection.Close();
            return users;
        }

        public User(int id, string username, string password, string firstname, string lastname, string phone, string email)
        {
            Id = id;
            Username = username;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
            Phone = phone;
            Email = email;
        }

    }
}
