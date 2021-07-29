using Core.Entities.Users;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IIdentityService : IGenericService<IdentityEntity, Identity>
    {

    }
}
