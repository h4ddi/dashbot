using DashBot.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System.Diagnostics;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDiscordBot _bot;
        private readonly ILogger _logger;

        public HomeController(IDiscordBot bot, ILogger logger)
        {
            _bot = bot;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new OverviewViewModel
            {
                BotIsRunning = _bot.IsRunning(),
                LogBuffer = _logger.GetLatest(10)
            };

            var botAccount = _bot.GetActiveBotAccount();

            if (botAccount != null)
            {
                model.ActiveAccount = new BotAccountViewModel
                {
                    AvatarUrl = botAccount.AvatarUrl,
                    Name = botAccount.Name
                };
            }

            return View(model);
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
