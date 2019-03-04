using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using DashBot.Abstractions;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICredentials _botCredentials;

        public HomeController(ICredentials botCredentials)
        {
            _botCredentials = botCredentials;
        }

        public IActionResult BotAuthentication()
        {
            return View();
        }

        public IActionResult Index()
        {
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
