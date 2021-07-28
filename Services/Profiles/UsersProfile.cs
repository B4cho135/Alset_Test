using AutoMapper;
using Core.Entities.Users;
using Models.Users;

namespace Services.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UserEntity, User>();
            CreateMap<User, UserEntity>();
        }
    }
}
