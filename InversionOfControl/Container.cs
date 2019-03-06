using System;
using Microsoft.Extensions.DependencyInjection;
using DashBot.Abstractions;
using DashBot.Credentials;
using DashBot.JsonStorage;

namespace DashBot.InversionOfControl
{
    public static class Container
    {
        public static IServiceCollection AddDashBotDependencies(this IServiceCollection collection)
            => collection
                .AddSingleton<Random>()
                .AddSingleton<IPersistentStorage, JsonPersistentStorage>()
                .AddSingleton<ICredentials, CredentialsProvider>();
    }
}
