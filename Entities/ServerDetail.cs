using System.Collections.Generic;

namespace DashBot.Entities
{
    public class ServerDetail
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public IEnumerable<TextChannel> TextChannels { get; set; }
    }
}