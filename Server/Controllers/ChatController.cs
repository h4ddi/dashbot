using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DashBot.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    public class ChatController : Controller
    {
        private readonly IDiscordBot _bot;

        public ChatController(IDiscordBot bot)
        {
            _bot = bot;
        }

        public IActionResult Global()
        {
            var model = new ChatViewModel
            {
                AvailableServers = _bot.GetAvailableServers().Select(Mapper.Map<ServerDetailViewModel>),
                MessageBuffer = new List<ChatMessageViewModel>()
            };

            return View(model);
        }

        [HttpGet("[controller]/[action]/{serverId}/{channelId}")]
        public IActionResult Channel(ulong serverId, ulong channelId)
        {
            throw new NotImplementedException();
        }
    }
}
