using DashBot.Abstractions;
using DashBot.Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System.Linq;
using AutoMapper;

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

        public IActionResult Authentication()
        {
            var accounts = _botCredentials.GetAllAccounts();
            var model = new BotAuthViewModel
            {
                SavedBotAccounts = accounts.Select(Mapper.Map<BotAccountViewModel>)
            };

            return View(model);
        }

        public IActionResult OneTimeToken()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OneTimeToken([FromForm]OneTimeBot oneTimeBot)
        {
            if (_bot.IsRunning())
            {
                ModelState.AddModelError("Token", "You cannot change the account while the bot is running.");
                return View(oneTimeBot);
            }

            _bot.Account = new BotAccount(oneTimeBot.Token);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult StartBot()
        {
            if (_bot.IsRunning())
            {
                return BadRequest("Bot is already running.");
            }

            _bot.Connect();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult StopBot()
        {
            if (!_bot.IsRunning())
            {
                return BadRequest("Bot is not running.");
            }

            _bot.Stop();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult UseStoredAccount(string name)
        {
            var account = _botCredentials.GetAccountByName(name);
            if (account is null) { return BadRequest($"No account with the name {name} exists."); }
            _bot.Account = account;
            return RedirectToAction("Index", "Home");
        }
    }
}
