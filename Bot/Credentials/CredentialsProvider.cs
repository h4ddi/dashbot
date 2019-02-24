using DashBot.Abstractions;

namespace DashBot.Bot.Credentials
{
    public class CredentialsProvider
    {
        private const string CredentialsPath = "Credentials";

        private readonly IDataStorage _storage;

        public CredentialsProvider(IDataStorage storage)
        {
            _storage = storage;
        }
    }
}

