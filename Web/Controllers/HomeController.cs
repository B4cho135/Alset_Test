using API.SDK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public async Task<IActionResult> Index()
        {
            var response = await _client.Identities.GetAll();
            var model = new UsersViewModel();
            //model.FullName = User.Claims.FirstOrDefault(x => x.Type == "FullName").Value; //I need full name here
            if (response.IsSuccessStatusCode)
            {
                model.Users = response.Content;
                return View(model);
            }

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Account");
            }

            //if user got here something is wrong
            model.ErrorMessage= "Unexpected error happened";
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
