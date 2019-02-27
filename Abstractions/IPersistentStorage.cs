using System.Collections.Generic;

namespace DashBot.Abstractions
{
    public interface IPersistentStorage
    {
        void Store<T>(T obj, string collection, string key);
        T RestoreSingle<T>(string collection, string pattern);
        IEnumerable<T> RestoreCollection<T>(string collection, string pattern);
    }
}

