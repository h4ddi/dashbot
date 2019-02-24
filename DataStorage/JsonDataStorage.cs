using System;
using System.Collections.Generic;
using System.IO;
using DashBot.Abstractions;
using DashBot.Assertions;
using Newtonsoft.Json;

namespace DashBot.DataStorage
{
    public class JsonDataStorage : IDataStorage
    {
        public const string JsonDataDirectory = "JsonData/";

        public static string GetStoragePathFor(Type objType, string path) 
            => Path.Combine(JsonDataDirectory, objType.Name, path + ".json");

        public T Restore<T>(string path)
        {
            var fullPath = GetStoragePathFor(typeof(T), path);
            return RestoreFromFile<T>(fullPath);
        }

        public IEnumerable<T> RestoreCollection<T>(string path)
        {
            throw new NotImplementedException();
        }

        public void Store<T>(T obj, string path)
        {
            Assert.NotEnumerable(typeof(T));
            var fullPath = GetStoragePathFor(typeof(T), path);
            var json = JsonConvert.SerializeObject(obj);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            File.WriteAllText(fullPath, json);
        }

        private T RestoreFromFile<T>(string filePath)
        {
            Assert.FileExists(filePath);
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
