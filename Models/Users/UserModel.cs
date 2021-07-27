using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Users
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public byte[] Photo { get; set; }
        public string Email { get; set; }
    }
}
