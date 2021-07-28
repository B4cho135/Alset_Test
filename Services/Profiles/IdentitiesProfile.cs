using AutoMapper;
using Core.Entities.Users;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class IdentitiesProfile : Profile
    {
        public IdentitiesProfile()
        {
            CreateMap<IdentityEntity, Identity>();
            CreateMap<Identity, IdentityEntity>();
        }
    }
}
