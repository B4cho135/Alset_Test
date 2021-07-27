using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Users
{
    public class IdentityEntity : BaseEntity<Guid>
    {
        public byte[] Photo { get; set; }
        public string IdentificationNumber { get; set; }
        public string Department { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

    }
}
