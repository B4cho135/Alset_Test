using Core.Entities.Users;
using Models.Responses;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IUserService: IGenericService<UserEntity, User>
    {
        public Task<Response<string>> AddToRoleAsync(UserEntity userEntity, string role);
        public Task<Response<UserEntity>> Create(User userModel);
    }
}
