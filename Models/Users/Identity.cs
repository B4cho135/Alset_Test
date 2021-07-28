using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Users
{
    public class Identity
    {
        public byte[] Photo { get; set; }
        public string IdentificationNumber { get; set; }
        public string Department { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
