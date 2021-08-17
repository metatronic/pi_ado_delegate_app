using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public enum Roles
    {
        Admin,
        User
    }
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles UserRole { get; set; }
    }
}
