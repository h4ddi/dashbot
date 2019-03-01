using System.Collections.Generic;
using DashBot.Entities;

namespace DashBot.Abstractions
{
    public interface ICredentials
    {
        IEnumerable<BotAccount> GetAllAccounts();
        BotAccount GetAccountByName(string name);
    }
}

