﻿using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace bacit_dotnet.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISqlConnector sqlConnector;

        public HomeController(ILogger<HomeController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }

        [HttpGet]
        public IActionResult UserData()
        {
            Console.WriteLine("UserData");
            var data = sqlConnector.GetUsers();
            Console.WriteLine(data);
            var model = new UsersModel();
            model.Users = data;
            return View("Users", model);

        }
        [HttpGet]
        public IActionResult SetData()
        {
            Console.WriteLine("SetData");
            sqlConnector.SetUsers();
            Console.WriteLine("Satt bruker");

            return View("Insert");

        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new RazorViewModel
            {
                Content = "En time til ørsta rådhus"
            };
            return View("Index", model);
        }
    }
}