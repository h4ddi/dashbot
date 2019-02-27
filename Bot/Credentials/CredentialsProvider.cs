using System.Collections.Generic;
using System.Linq;
using DashBot.Abstractions;

namespace DashBot.Bot.Credentials
{
    public class CredentialsProvider
    {
        private const string CredentialsPath = "Credentials";

        private readonly IPersistentStorage _storage;

        public CredentialsProvider(IPersistentStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<BotAccount> GetAllAccounts()
            => _storage.RestoreCollection<BotAccount>(CredentialsPath);

        public BotAccount GetAccountByName(string name)
            => _storage
                .RestoreCollection<BotAccount>(CredentialsPath)
                .FirstOrDefault(a => a.Name == name);
    }
}
