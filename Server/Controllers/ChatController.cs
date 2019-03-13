using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (!_bot.IsRunning()) { return RedirectToAction("Authentication", "Bot"); }

            var model = new ChatViewModel
            {
                AvailableServers = _bot.GetAvailableServers().Select(Mapper.Map<ServerDetailViewModel>),
                MessageBuffer = new List<ChatMessageViewModel>()
            };

            return View(model);
        }

        [HttpGet("[controller]/[action]/{serverId}/{channelId}")]
        public async Task<IActionResult> Channel(ulong serverId, ulong channelId)
        {
            if (!_bot.IsRunning()) { return RedirectToAction("Authentication", "Bot"); }

            var model = new ChatViewModel
            {
                ActiveServer = Mapper.Map<ServerDetailViewModel>(_bot.GetServerDetailFromId(serverId)),
                ActiveChannel = Mapper.Map<TextChannelViewModel>(_bot.GetTextChannelDetailFromId(serverId, channelId)),
                AvailableServers = _bot.GetAvailableServers().Select(Mapper.Map<ServerDetailViewModel>),
                MessageBuffer = (await _bot.GetMessageBufferFor(serverId, channelId)).Select(Mapper.Map<ChatMessageViewModel>)
            };

            return View(model);
        }
    }
}
