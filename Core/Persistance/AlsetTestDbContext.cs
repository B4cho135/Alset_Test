using Core.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance
{
    public class AlsetTestDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public AlsetTestDbContext(DbContextOptions<AlsetTestDbContext> options) : base(options)
        {

        }

        public DbSet<IdentityEntity> UserIdentity { get; set; }
    }
}
