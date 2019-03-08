namespace DashBot.Entities
{
    public class BotAccount
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }

        public BotAccount() { }

        public BotAccount(string token)
        {
            Token = token;
            AvatarUrl = Constants.AnonymousBotAvatarUrl;
            Name = Constants.AnonymousBotName;
        }
    }
}

