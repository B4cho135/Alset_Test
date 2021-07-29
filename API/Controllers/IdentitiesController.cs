using Core.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleConstants.Administrator)]
    public class IdentitiesController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public IdentitiesController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            try
            {

                var identity = await _identityService.GetByIdAsync(Id);

                if (identity != null)
                {
                    return Ok(identity);
                }

                return BadRequest($"Identity with Id - {Id} was not found!");

            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "An error occured while working the request!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var identities = await _identityService.GetAll();
                return Ok(identities);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "An error occured while working the request!");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(Identity model)
        {
            IdentityEntity identity = new IdentityEntity()
            {
                IdentificationNumber = model.IdentificationNumber,
                Photo = model.Photo,
                UserId = model.UserId,
                Department = model.Department
            };
            try
            {
                var response = await _identityService.AddAsync(identity);

                if (response.HasSucceeded)
                {
                    return NoContent();
                }

                return BadRequest(response.Message);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "An error occured while working the request!");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, Identity model)
        {
            var identity = await _identityService.Get().FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == Id);

            if(identity == null)
            {
                return BadRequest($"Identity with Id - {Id} was not found!");
            }

            bool identityChanged = false;

            if(!string.IsNullOrEmpty(model.Department) && model.Department != identity.Department)
            {
                identity.Department = model.Department;
                identityChanged = true;
            }
            if (!string.IsNullOrEmpty(model.IdentificationNumber) && model.IdentificationNumber != identity.IdentificationNumber)
            {
                identity.IdentificationNumber = model.IdentificationNumber;
                identityChanged = true;
            }
            if (model.Photo != identity.Photo)
            {
                identity.Photo = model.Photo;
                identityChanged = true;
            }
            if (model.UserId != identity.UserId)
            {
                identity.UserId = model.UserId;
                identityChanged = true;
            }

            if(identityChanged)
            {
                try
                {
                    identity.UpdatedAt = DateTime.Now;
                    var response = await _identityService.UpdateAsync(identity);

                    if (response.HasSucceeded)
                    {
                        return NoContent();
                    }

                    return BadRequest(response.Message);
                }
                catch
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "An error occured while working the request!");
                }
            }
            return BadRequest("Identity entity already looks like this!");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var identity = await _identityService.Get().FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == Id);

            if (identity == null)
            {
                return BadRequest($"Identity with Id - {Id} was not found!");
            }

            identity.DeletedAt = DateTime.Now;
            identity.IsDeleted = true;
            try
            {
                var response = await _identityService.UpdateAsync(identity); //Soft Delete... there is a method for hard delete as well but I dont use it.

                if (response.HasSucceeded)
                {
                    return NoContent();
                }

                return BadRequest(response.Message);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "An error occured while working the request!");
            }
        }
    }
}
