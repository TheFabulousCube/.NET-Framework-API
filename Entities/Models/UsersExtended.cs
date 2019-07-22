using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public partial class Users
    {
        public Users()
        { }

        public Users(Users user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            Fname = user.Fname;
            Lname = user.Lname;
            Address = user.Address;
            City = user.City;
            State = user.State;
            Zip = user.Zip;
            Email = user.Email;
            Role = user.Role;
        }
    }
}
