using API.Enums;
using Core.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Constants;
using Models.Users;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowCors")]
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = RoleConstants.Administrator)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAll(Guid Id)
        {

            var user = await _userService.GetByIdAsync(Id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistrantRoleFilter role, User model)
        {
            
            var response  = await _userService.Create(model);
            if (response.HasSucceeded)
            {
                await _userService.AddToRoleAsync(response.Item, role.ToString());
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, User model)
        {
            var user = await _userService.Get().FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == Id);

            if(user == null)
            {
                return BadRequest("no such user found");
            }

            bool userChanged = false;

            if(!string.IsNullOrEmpty(model.Department) && user.Department != model.Department)
            {
                user.Department = model.Department;
                userChanged = true;
            }
            if (!string.IsNullOrEmpty(model.PhoneNumber) && user.PhoneNumber != model.PhoneNumber)
            {
                user.PhoneNumber = model.PhoneNumber;
                userChanged = true;
            }
            if (!string.IsNullOrEmpty(model.Name) && user.Name != model.Name)
            {
                user.Name = model.Name;
                userChanged = true;
            }
            if (!string.IsNullOrEmpty(model.Surname) && user.Surname != model.Surname)
            {
                user.Surname = model.Surname;
                userChanged = true;
            }
            if (!string.IsNullOrEmpty(model.Department) && user.Department != model.Department)
            {
                user.Department = model.Department;
                userChanged = true;
            }

            if (!string.IsNullOrEmpty(model.IdentificationNumber) && user.IdentificationNumber != model.IdentificationNumber)
            {
                user.IdentificationNumber = model.IdentificationNumber;
                userChanged = true;
            }

            

            if(userChanged)
            {
                user.UpdatedAt = DateTime.Now;
                var response = await _userService.UpdateAsync(user);

                if(response.HasSucceeded)
                {
                    return NoContent();
                }
                return BadRequest();
            }
            return BadRequest("User entity already looks like this");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var user = await _userService.Get().FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == Id);

            if (user == null)
            {
                return BadRequest("no such user found");
            }

            user.IsDeleted = true;
            user.DeletedAt = DateTime.Now;

            var response = await _userService.UpdateAsync(user); //using update instead of delete for soft delete

            if (response.HasSucceeded)
            {
                return NoContent();
            }
            return BadRequest("something unexpected happened");
        }
    }
}
