using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Users
{
    public class CreateViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentificationNumber { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
