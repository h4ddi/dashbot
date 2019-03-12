using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Global()
        {
            var model = new ChatViewModel
            {
                AvailableServers = new List<ServerDetailViewModel>(),
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
