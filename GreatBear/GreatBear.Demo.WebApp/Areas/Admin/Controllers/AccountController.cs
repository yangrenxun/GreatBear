using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatBear.Core.Mvc.Paging;
using GreatBear.Demo.Application.Users.Dto;
using GreatBear.Demo.Entities.User;
using GreatBear.Demo.WebApp.Areas.Admin.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace GreatBear.Demo.WebApp.Areas.Admin.Controllers
{
    public class AccountController : BaseAdminController
    {
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            UserModel model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateUser(UserModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}