﻿using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class LoginResponse
    {
        public User User { get; set; }
        public string JWT { get; set; }
    }
}
