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
                    "The provided type cannot be a collection.");
            }
        }

        public static void DirectoryExists(string path)
        {
            if(!Directory.Exists(path))
            {
                throw new InvalidOperationException(
                    $"Could not find the following directory: {path}");
            }
        }

        private static bool IsEnumerableType(Type type)
            => (type.GetInterface(nameof(IEnumerable)) != null);
    }
}

