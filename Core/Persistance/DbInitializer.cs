using Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance
{
    public static class DbInitializer
    {
        public static void Initialize(AlsetTestDbContext context, UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager)
        {
            if (!context.Roles.Any())
            {
                roleManager.CreateAsync(new RoleEntity()
                {
                    Name = "Administrator"
                }).Wait();

                roleManager.CreateAsync(new RoleEntity()
                {
                    Name = "User"
                }).Wait();
            }
            if (!context.Users.Any())
            {
                var users = new UserEntity[]
                {
                    new UserEntity()
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        PhoneNumber = "598471547",
                        Name = "Badri",
                        Surname = "Tatarashvili"
                    },
                    new UserEntity()
                    {
                        UserName = "user@gmail.com",
                        Email = "user@gmail.com",
                        PhoneNumber = "598471547",
                        Name = "User",
                        Surname = "Userishvili"
                    }
                };
                userManager.CreateAsync(users[0], "qazQAZ1!").Wait();
                userManager.CreateAsync(users[1], "qazQAZ1!").Wait();
                userManager.AddToRoleAsync(users[0], "Administrator").Wait();
                userManager.AddToRoleAsync(users[1], "User").Wait();

                context.Database.Migrate();
            }
        }
    }
}
