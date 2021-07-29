using API.SDK;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
                HttpContext.Response.Cookies.Append("Fullname", loginResponse.Content.User.Name + " " + loginResponse.Content.User.Surname, new CookieOptions());
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var registerModel = new RegisterRequest()
            {
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Password = model.Password,
                RepeatPassword = model.RepeatPassword
            };
            var response = await _client.Account.Register(registerModel);
            if(!string.IsNullOrEmpty(response.Content))
            {

                HttpContext.Response.Cookies.Append("access_token", response.Content, new CookieOptions());
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            var response = await _client.Account.Logout();
            if (response.IsSuccessStatusCode)
            {
                Response.Cookies.Delete("access_token", new CookieOptions()
                {
                    Secure = true,
                });
                Response.Cookies.Delete("Fullname", new CookieOptions()
                {
                    Secure = true,
                });
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
