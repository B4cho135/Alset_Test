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
        public string Name { get; set; }
        public string Surname { get; set; }

        public IList<string> Roles { get; set; }

    }
}
