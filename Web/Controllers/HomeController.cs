using API.SDK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Queries;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.Users;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiClient _client;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IActionResult> Index(UsersViewModel usersViewModel)
        {
            var response = await _client.Users.GetAll();

            var users = response.Content;
            if(usersViewModel.Query != null && !string.IsNullOrEmpty(usersViewModel.Query.IdentificationNumber))
            {
                users = users.Where(x => x.IdentificationNumber.Contains(usersViewModel.Query.IdentificationNumber)).ToList();
            }
            var model = new UsersViewModel();
            model.FullName = HttpContext.User.Identity.Name;
            if (response.IsSuccessStatusCode)
            {
                model.Users = users;
                return View(model);
            }

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Account");
            }

            //if user got here something is wrong
            model.ErrorMessage= response.ReasonPhrase;
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            var userModel = new User()
            {
                Department = model.Department,
                Surname = model.Surname,
                Email = model.Email,
                IdentificationNumber = model.IdentificationNumber,
                Name = model.Name,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                RepeatPassword = model.RepeatPassword
            };
            try
            {
                await _client.Users.Add(userModel);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid Id)
        {
            var user = await _client.Users.Get(Id);

            if (user != null)
            {
                var model = new UpdateViewModel()
                {
                    Department = user.Content.Department,
                    Email = user.Content.Email,
                    IdentificationNumber = user.Content.IdentificationNumber,
                    Name = user.Content.Name,
                    PhoneNumber = user.Content.PhoneNumber,
                    Surname = user.Content.Surname
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            var userModel = new User()
            {
                Department = model.Department,
                Surname = model.Surname,
                Email = model.Email,
                IdentificationNumber = model.IdentificationNumber,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
            };
            try
            {
                await _client.Users.Update(model.Id, userModel);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                await _client.Users.Delete(Id);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
