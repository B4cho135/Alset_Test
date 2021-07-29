using Models.Queries;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Users
{
    public class UsersViewModel
    {
        public List<User> Users { get; set; }
        public string FullName { get; set; }
        public string ErrorMessage { get; set; }
        public UserSearchQuery Query { get; set; }
    }
}
