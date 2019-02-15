using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatBear.Core.Mvc.Paging;
using GreatBear.Demo.Application.Users;
using GreatBear.Demo.Application.Users.Dto;
using GreatBear.Demo.Entities.User;
using GreatBear.Demo.WebApp.Areas.Admin.Models.Users;
using GreatBear.Demo.WebApp.Authentication.Admin;
using GreatBear.Demo.WebApp.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatBear.Demo.WebApp.Areas.Admin.Controllers
{
    public class AccountController : BaseAdminController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        public IActionResult Index(UserListModel model)
        {
            List<User> users = new List<User>();
            users.Add(new Entities.User.User { UserName = "111",Password = "111" });
            users.Add(new Entities.User.User { UserName = "222", Password = "222" });
            users.Add(new Entities.User.User { UserName = "333", Password = "333" });
            users.Add(new Entities.User.User { UserName = "111", Password = "111" });
            users.Add(new Entities.User.User { UserName = "222", Password = "222" });
            users.Add(new Entities.User.User { UserName = "333", Password = "333" });
            users.Add(new Entities.User.User { UserName = "111", Password = "111" });
            users.Add(new Entities.User.User { UserName = "222", Password = "222" });
            users.Add(new Entities.User.User { UserName = "333", Password = "333" });
            users.Add(new Entities.User.User { UserName = "111", Password = "111" });
            users.Add(new Entities.User.User { UserName = "222", Password = "222" });
            users.Add(new Entities.User.User { UserName = "333", Password = "333" });
            users.Add(new Entities.User.User { UserName = "111", Password = "111" });
            users.Add(new Entities.User.User { UserName = "222", Password = "222" });
            users.Add(new Entities.User.User { UserName = "333", Password = "333" });
            var result = new PagedList<User>(users.AsQueryable(), model.PageIndex, 10);

            model.LoadPagedList(result);

            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Login(LoginModel model)
        {
            var validate = _userService.ValidateUser(model.UserName,model.Password);
            if (validate)
            {
                var user = _userService.GetUserByUsername(model.UserName);
                _authenticationService.SignIn(user,true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("UserName", "登录失败");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            _authenticationService.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult CreateUser()
        {
            UserModel model = new UserModel();
            return View(model);
        }

        [HttpPost,AllowAnonymous]
        public IActionResult CreateUser(UserModel model)
        {
            var check = _userService.CheckExistUserName(model.UserName);
            if (check)
            {
                ModelState.AddModelError("UserName", "该用户名已存在");
            }
            if (ModelState.IsValid)
            {
                _userService.CreateUser(model);
            }
            return View(model);
        }
    }
}