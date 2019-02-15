﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatBear.Demo.WebApp.Areas.Admin.Controllers
{
    [Admin]
    public class DefaultController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}