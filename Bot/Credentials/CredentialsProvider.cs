using System;
using System.Collections.Generic;
using System.Linq;
using DashBot.Abstractions;
using DashBot.Entities;

namespace DashBot.Bot.Credentials
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

        public void StoreAccount(BotAccount account)
        {
            if (GetAllAccounts().Any(a => a.Name == account.Name))
                throw new Exception($"An account with the name '{account.Name}' already exists.");

            var key = account.Name.Trim().Replace(" ", "");
            _storage.Store(account, CredentialsPath, key);
        }
    }
}
