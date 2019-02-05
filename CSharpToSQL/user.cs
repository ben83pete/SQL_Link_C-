using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToSQL
{
    class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public User() { }

        public User( int id, string username, string firstname, string lastname, string phone, string email)
        {
            Id = id;
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Phone = phone;
            Email = email;
        }

    }
}
