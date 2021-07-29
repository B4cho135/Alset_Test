using AutoMapper;
using Core.Entities.Users;
using Core.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Responses;
using Models.Responses.Errors;
using Models.Users;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : GenericService<UserEntity, User>, IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        public UserService(AlsetTestDbContext context, IMapper mapper, UserManager<UserEntity> userManager) : base(context, mapper)
        {
            _userManager = userManager;
        }

        public async override Task<User> GetByIdAsync(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            user.Roles = await _userManager.GetRolesAsync(user);
            User mappedUser = Mapper.Map<User>(user);
            return mappedUser;
        }

        public async override Task<IEnumerable<User>> GetAll()
        {
            List<UserEntity> users = await Get().ToListAsync();

            foreach(var user in users)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
            }
            List<User> model = Mapper.Map<List<User>>(users);

            return model;
        }

		public async Task<Response<UserEntity>> Create(User userModel)
		{
			try
			{
				var user = new UserEntity()
				{
					Name = userModel.Name,
					Surname = userModel.Surname,
					Department = userModel.Department,
					PhoneNumber = userModel.PhoneNumber,
					IdentificationNumber = userModel.IdentificationNumber,
					Email = userModel.Email,
					UserName = userModel.Email
				};
				var result = await _userManager.CreateAsync(user, userModel.Password);

				if (result.Succeeded)
				{
					User model = Mapper.Map<User>(user);

					var response = new Response<UserEntity>()
					{
						Item = user,
						StatusCode = 204,
						Message = "The entity has been succesfully added!",
						HasSucceeded = true
					};

					return response;
				}
				return new Response<UserEntity>()
				{
					Item = user,
					StatusCode = 400,
					Message = "Fail",
					HasSucceeded = false
				};
			}
			catch (Exception ex)
			{
				var response = new Response<UserEntity>()
				{
					Item = null,
					StatusCode = 500,
					Message = "Exception",
					Errors = new List<Error>()
					{
						new Error(ex.Message)
					}
				};
				return response;
			}
		}

		public async Task<Response<string>> AddToRoleAsync(UserEntity userEntity, string role)
        {
			try
			{
				var result = await _userManager.AddToRoleAsync(userEntity, role);
				if (result.Succeeded)
				{
					return new Response<string>()
					{
						Item = role,
						HasSucceeded = true,
						Message = "Success",
						StatusCode = 204
					};

				}
				return new Response<string>()
				{
					Item = role,
					HasSucceeded = true,
					Message = "Fail",
					StatusCode = 400
				};
			}
			catch(Exception ex)
            {
				return new Response<string>()
				{
					Item = role,
					HasSucceeded = true,
					Message = "Exception",
					StatusCode = 500,
					Errors = new List<Error>() { new Error() {Description = ex.Message } }
				};
			}

		}
	}
}
