using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using DashBot.Abstractions;
using DashBot.Entities;

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
            var storedAccounts = _botCredentials.GetAllAccounts();
            var model = new BotAuthViewModel()
            {
               SavedBotAccounts = storedAccounts.Select(ToViewModel)
            };

            return View(model);
        }

        private BotAccountViewModel ToViewModel(BotAccount entity)
            => new BotAccountViewModel
                {
                    AvatarUrl = entity.AvatarUrl,
                    Name = entity.Name,
                    Token = entity.Token
                };

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
