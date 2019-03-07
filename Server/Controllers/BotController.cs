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
        private readonly IDiscordBot _bot;

        public BotController(ICredentials botCredentials, IDiscordBot bot)
        {
            _botCredentials = botCredentials;
            _bot = bot;
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
                Name = entity.Name
            };
        
        public IActionResult OneTimeToken()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OneTimeToken([FromForm]OneTimeBot oneTimeBot)
        {
            if (_bot.GetStatus() == BotStatus.Running)
            {
                ModelState.AddModelError("Token", "You cannot change the account while the bot is running.");
                return View(oneTimeBot);
            }

            _bot.Account = new BotAccount
            {
                Name = Constants.AnonymousBotName,
                AvatarUrl = Constants.AnonymousBotAvatarUrl,
                Token = oneTimeBot.Token
            };

            return RedirectToAction("Index", "Home");
        }

        public IActionResult StartBot()
        {
            if (_bot.GetStatus() == BotStatus.Running)
            {
                return BadRequest("Bot is already running.");
            }

            _bot.Connect();
            return Ok();
        }

        public IActionResult StopBot()
        {
            if (_bot.GetStatus() == BotStatus.NotRunning)
            {
                return BadRequest("Bot is not running.");
            }

            _bot.Stop();
            return Ok();
        }

        public IActionResult UseStoredAccount(string name)
        {
            var account = _botCredentials.GetAccountByName(name);
            if (account is null) { return BadRequest($"No account with the name {name} exists."); }
            _bot.Account = account;
            return Ok();
        }
    }
}