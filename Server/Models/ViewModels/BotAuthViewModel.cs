using System.Collections.Generic;

namespace Server.Models
{
    public class BotAuthViewModel
    {
        public IEnumerable<BotAccountViewModel> SavedBotAccounts { get; set; }
    }
}
