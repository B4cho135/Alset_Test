﻿using Models.Requests;
using Models.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK.Resources
{
    public interface IAccountResource
    {
        [Post("/api/accounts/login")]
        public Task<ApiResponse<LoginResponse>> Login(LoginRequest loginRequest);
    }
}
