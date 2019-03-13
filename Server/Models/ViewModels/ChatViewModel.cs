using System.Collections.Generic;

namespace Server.Models
{
    public class ChatViewModel
    {
        public ServerDetailViewModel ActiveServer { get; set; }
        public TextChannelViewModel ActiveChannel { get; set; }
        public IEnumerable<ChatMessageViewModel> MessageBuffer { get; set; }
        public IEnumerable<ServerDetailViewModel> AvailableServers { get; set; }
    }
}
