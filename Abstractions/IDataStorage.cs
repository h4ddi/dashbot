using System.Collections.Generic;

namespace DashBot.Abstractions
{
    public interface IDataStorage
    {
        void Store<T>(T obj, string path);
        T Restore<T>(string path);
        IEnumerable<T> RestoreCollection<T>(string path);
    }
}

