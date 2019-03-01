using System.Collections.Generic;
using System.Linq;
using DashBot.Abstractions;
using DashBot.Entities;

namespace DashBot.Credentials
{
    public class CredentialsProvider : ICredentials
    {
        private const string CredentialsPath = "Credentials";

        private readonly IPersistentStorage _storage;

        public CredentialsProvider(IPersistentStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<BotAccount> GetAllAccounts()
            => _storage.RestoreMany<BotAccount>(CredentialsPath);

        public BotAccount GetAccountByName(string name)
            => _storage
                .RestoreMany<BotAccount>(CredentialsPath)
                .FirstOrDefault(a => a.Name == name);
    }
}
