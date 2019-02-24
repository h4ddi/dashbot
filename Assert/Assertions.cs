using System;
using System.IO;
using System.Collections;

namespace DashBot.Assertions
{
    public static class Assert
    {
        public static void FileExists(string path)
        {
            if(!File.Exists(path))
            {
                throw new FileNotFoundException($"Could not find file {path}.");
            }
        }

        public static void NotEnumerable(Type type)
        {
            if(IsEnumerableType(type))
            {
                throw new InvalidOperationException(
                    "JsonDataStorage does not support storing collections.");
            }
        }

        private static bool IsEnumerableType(Type type)
            => (type.GetInterface(nameof(IEnumerable)) != null);
    }
}
