using System.Collections.Generic;

namespace Server.Models
{
    public class ChatViewModel
    {
        public IEnumerable<ChatMessageViewModel> MessageBuffer { get; set; }
        public IEnumerable<ServerDetailViewModel> AvailableServers { get; set; }
    }
}
