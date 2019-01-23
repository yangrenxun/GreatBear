using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatBear.Demo.WebApp.Controllers
{
    [Member]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            var a = HttpContext.AuthenticateAsync();
            //HttpContext.SignInAsync();
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var a = HttpContext.AuthenticateAsync();
            //HttpContext.SignInAsync();
            return View();
        }
    }
}