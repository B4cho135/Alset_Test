using AutoMapper;
using Core.Entities.Users;
using Core.Persistance;
using Models.Users;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class IdentityService : GenericService<IdentityEntity, Identity>, IIdentityService
    {
        public IdentityService(AlsetTestDbContext context, IMapper mapper) : base(context, mapper)
        {

        }


    }
}
