using Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Users
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentificationNumber { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public IList<string> Roles { get; set; }

    }
}
