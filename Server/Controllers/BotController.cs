using DashBot.Abstractions;
using DashBot.Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System.Linq;

namespace Server.Controllers
{
    public class BotController : Controller
    {
        private readonly ICredentials _botCredentials;

        public BotController(ICredentials botCredentials)
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

        private static BotAccountViewModel ToViewModel(BotAccount entity)
            => new BotAccountViewModel
            {
                AvatarUrl = entity.AvatarUrl,
                Name = entity.Name,
                Token = entity.Token
            };


        public IActionResult OneTimeToken()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OneTimeToken([FromForm]OneTimeBot oneTimeBot)
        {
            return Ok();
        }
    }
}