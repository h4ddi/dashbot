using System.Collections.Generic;

namespace Server.Models
{
    public class ServerDetailViewModel
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public IEnumerable<TextChannelViewModel> TextChannels { get; set; }
    }
}