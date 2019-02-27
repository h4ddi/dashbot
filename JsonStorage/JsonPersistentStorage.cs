﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DashBot.Abstractions;
using DashBot.Assertions;
using Newtonsoft.Json;

namespace DashBot.DataStorage
{
    public class JsonPersistentStorage : IPersistentStorage
    {
        public const string JsonDataDirectory = "JsonData/";

        public static string ToStoragePath(Type objType, string collection)
            => Path.Combine(JsonDataDirectory, objType.Name, collection);

        //public T Restore<T>(string path)
        //{
        //var fullPath = GetStoragePathForFile(typeof(T), path);
        //return RestoreFromFile<T>(fullPath);
        //}
        //
        //public IEnumerable<T> RestoreCollection<T>(string path)
        //{
        //var collectionPath = GetPathForCollection(typeof(T), path);
        //Assert.DirectoryExists(collectionPath);
        //var files = Directory.GetFiles(collectionPath, "*.json");
        //return files.Select(f => RestoreFromFile<T>(f));
        //}
        //
        //public void Store<T>(T obj, string path)
        //{
        //Assert.NotEnumerable(typeof(T));
        //var fullPath = GetStoragePathForFile(typeof(T), path);
        //}
        //
        //private T RestoreFromFile<T>(string filePath)
        //{
        //Assert.FileExists(filePath);
        //var json = File.ReadAllText(filePath);
        //return JsonConvert.DeserializeObject<T>(json);
        //}
        //
        //public void Store<T>(T obj, string collection, string key)
        //{
        //var filePath = ToStoragePath(typeof(T), collection, key);
        //var json = JsonConvert.SerializeObject(obj);
        //Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        //File.WriteAllText(filePath, json);
        //}
        //
        //public IEnumerable<T> RestoreCollection(string collection, string pattern)
        //{
        //throw new NotImplementedException();
        //}
        public void Store<T>(T obj, string collection, string key)
        {
            var path = ToStoragePath(typeof(T), collection);
            var filePath = Path.Combine(path, $"{key}.json");
            var json = JsonConvert.SerializeObject(obj);

            Directory.CreateDirectory(path);
            File.WriteAllText(filePath, json);
        }

        public IEnumerable<T> RestoreMany<T>(string collection, string pattern = "*")
        {
            var path = ToStoragePath(typeof(T), collection);
            Assert.DirectoryExists(path);
            var files = Directory.GetFiles(path, $"{pattern}.json");
            return files.Select(f => RestoreFromFile<T>(f));
        }

        public T RestoreSingle<T>(string collection, string pattern)
            => RestoreMany<T>(collection, pattern).First();

        private T RestoreFromFile<T>(string filePath)
        {
            Assert.FileExists(filePath);
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}