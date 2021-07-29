using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Queries
{
    public class IdentitySearchQuery
    {
        public string IdentificationNumber { get; set; }
        public string Department { get; set; }
        public string UserId { get; set; }
    }
}
