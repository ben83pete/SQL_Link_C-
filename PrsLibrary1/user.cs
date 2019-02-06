using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToSQL
{
    public class User
    {
        private static string CONNECT_STRING = @"server = STUDENT-FLEX\SQLEXPRESS; database = prsdb; trusted_connection = true";
        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        private static SqlConnection CreatAndCheckConnection()
        {
            var Connection = new SqlConnection(CONNECT_STRING);
            Connection.Open();
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open.");
                return null; // return false because method is a bool
            }
            return Connection;
        }
        
        private static SqlDataReader CheckSQLForRows(string sql, SqlConnection Connection)
        {
            var cmd = new SqlCommand(sql, Connection);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)  // (reader.HasRows == false)
            {
                Console.WriteLine("Result set has no rows.");
                Connection.Close();
                return null;
            }

            return reader;
        }


        public static bool UpdateUser( User user)
        {
            var Connection = CreatAndCheckConnection();
            if (Connection == null)
            {
                return false;
            }

            var sql = "Update Users set ";
            sql += "UserName = '" + user.Username + "',";
            sql += "Password = '" + user.Password + "',";
            sql += "First_Name = '" + user.Firstname + "',";
            sql += "Last_Name = '" + user.Lastname + "',";
            sql += "Phone = '" + user.Phone + "',";
            sql += "Email = '" + user.Email + "'";
            // sql += "IsReviewer = " + (user.isreviewer ? 1:0) + ",";
                    //  ? is an if statement, so if user is a reviewer, true pass a 1, false pass a 0
            sql += $" Where id = {user.Id}";        
            var cmd = new SqlCommand(sql, Connection);
            var recsAfected = cmd.ExecuteNonQuery();
            Connection.Close();
            return recsAfected == 1;            
        }


        public static bool DeleteUser(int Id)
        {
            var Connection = CreatAndCheckConnection();
            if (Connection == null)
            {
                return false;
            }

            var sql = $"Delete From Users where id = {Id}";

            var cmd = new SqlCommand(sql, Connection);
            var recsAfected = cmd.ExecuteNonQuery();
            Connection.Close();

            return recsAfected == 1;            
        }
    

        public static bool InsertUser(User user)
        {
            var Connection = CreatAndCheckConnection();
            if (Connection == null)
            {
                return false;
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
            var Connection = CreatAndCheckConnection();
            if (Connection == null)
            {
                return null;
            }

            var sql = $"select * from users where id = {Id} ;";

            var reader = CheckSQLForRows(sql, Connection);
            reader.Read();

            var user = new User();
            user.Id = (int)reader["Id"];
            user.Username = (string)reader["Username"];
            user.Firstname = (string)reader["First_Name"];
            user.Lastname = (string)reader["Last_Name"];
            //var Fullname = $"{Firstname} {Lastname}";
            user.Phone = reader["Phone"] == DBNull.Value ? null : (string)reader["Phone"];
            user.Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"]; // read email col in sql == check for Null ? if true set null in C# : if false (not null) set data to variable          

            Connection.Close();
            return user;
        }
        
        
        public static User[] GetAllUsers()
        {
            var Connection = CreatAndCheckConnection();
            if (Connection == null)
            {
                return null;
            }            
            var sql = "select * from users;";

            var reader = CheckSQLForRows(sql, Connection);        
            
            var users = new User[20];  // create an array to hold the user data were pulling from SQL
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
        public override string ToString()  // default print method to use 
        {
            return $" [ToPrint()] Id = {Id}, user name = {Username}, Name = {Firstname} {Lastname}";
        }
    }
}
