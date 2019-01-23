﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatBear.Demo.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Admin]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}