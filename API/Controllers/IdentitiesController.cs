using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public class IdentitiesController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public IdentitiesController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid Id)
        {
            try
            {

                var identity = await _identityService.GetByIdAsync(Id);

                if(identity != null)
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
    }
}
