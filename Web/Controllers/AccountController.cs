using API.SDK;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Account;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiClient _client;
        public AccountController(ApiClient client)
        {
            _client = client;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            var loginResponse = await _client.Account.Login(new LoginRequest()
            {
                Email = model.Email,
                Password = model.Password
            });
            if(loginResponse.IsSuccessStatusCode)
            {
                HttpContext.Response.Cookies.Append("access_token", loginResponse.Content.JWT, new CookieOptions());
                return RedirectToAction("Index", "Home");
            }
            model.ErrorMessage = "Either Email or Password is wrong";
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            return RedirectToAction();
        }

    }
}
