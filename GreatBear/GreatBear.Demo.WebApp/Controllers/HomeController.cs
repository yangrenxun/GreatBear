﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GreatBear.WebApp.Models;
using GreatBear.Demo.Entities.User;
using GreatBear.Demo.Application.Users.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace GreatBear.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public HomeController(IMapper mapper, ILogger logger)
        {
            _logger = logger;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            _logger.LogError("哈哈，我错了");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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