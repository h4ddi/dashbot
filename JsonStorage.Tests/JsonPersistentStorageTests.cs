using Xunit;
using JsonStorage.Tests.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using DashBot.Abstractions;
using DashBot.DataStorage;

namespace JsonStorage.Tests
{
    public class JsonPersistentStorageTests : IDisposable
    {
        private readonly TestDataHelper _testDataHelper;
        private readonly IPersistentStorage _storage;
        private const string Collection = "Test/Collection";

        public JsonPersistentStorageTests()
        {
            _testDataHelper = new TestDataHelper();
            _storage = new JsonPersistentStorage();
        }

        public void Dispose()
            => _testDataHelper.DeleteTestData();

        [Fact]
        public void ShouldRestoreByExactPattern()
        {
            const string pattern = "DataKey-A";
            var expected = _testDataHelper.GetDummyByKey(pattern);

            var actual = _storage.RestoreSingle<DummyDataHolder>(Collection, pattern);

            Assert.Equal(expected.Text, actual.Text);
            Assert.Equal(expected.Number, actual.Number);
        }

        [Fact]
        public void ShouldRestoreEverything()
        {
            var expected = _testDataHelper.GetAllDummyFiles();

            var actual = _storage.RestoreMany<DummyDataHolder>(Collection);

            AssertDummyCollectionsMatch(expected, actual);
        }

        [Fact]
        public void ShouldRestoreSome()
        {
            const string pattern = "DataKey-*";
            var expected = _testDataHelper.GetDummyByKeyContains(pattern.Replace("*", ""));

            var actual = _storage.RestoreMany<DummyDataHolder>(Collection, pattern);

            AssertDummyCollectionsMatch(expected, actual);
        }

        private void AssertDummyCollectionsMatch(IEnumerable<DummyDataHolder> expected, IEnumerable<DummyDataHolder> actual)
        {
            Assert.Equal(expected.Count(), actual.Count());
            foreach(var expectedItem in expected)
            {
                Assert.Contains(actual, actualItem 
                        => DummyDataHoldersAreEqual(actualItem, expectedItem));
            }
        }

        private bool DummyDataHoldersAreEqual(DummyDataHolder a, DummyDataHolder b)
            => a.Text == b.Text && a.Number == b.Number;

        // OLD TESTS
//        public class SimpleClass
//        {
//            public int MyNumber { get; set; }
//        }
//
//        private readonly IPersistentStorage storage;
//        private readonly SimpleClass SimpleClassValue = new SimpleClass { MyNumber = 3 };
//        private readonly SimpleClass SecondClassValue = new SimpleClass { MyNumber = 99 };
//        private const string SimpleClassJson = "{\"MyNumber\":3}";
//        private const string SecondClassJson = "{\"MyNumber\":99}";
//        private const string SimpleClassPath = "MyClasses/testclass";
//        private const string SecondClassPath = "MyClasses/secondclass";
//        private const string SimpleClassCollectionPath = "MyClasses";
//        private const int SimpleClassCollectionCount = 2;
//
//        public JsonStorageTests()
//        {
//            storage = new JsonDataStorage();
//            SetupDummyFile(typeof(SimpleClass), SimpleClassPath, SimpleClassJson);
//            SetupDummyFile(typeof(SimpleClass), SecondClassPath, SecondClassJson);
//        }
//
//        public void Dispose()
//        {
//            Directory.Delete(JsonDataStorage.JsonDataDirectory, true);
//        }
//
//        [Fact]
//        public void ShouldRestoreSingleFile()
//        {
//            var actual = storage.Restore<SimpleClass>(SimpleClassPath);
//            Assert.Equal(SimpleClassValue.MyNumber, actual.MyNumber);
//        }
//
//        [Fact]
//        public void ShouldStoreSingleObject()
//        {
//            const string path = "ToStore/stored";
//            storage.Store(SimpleClassValue, path);
//
//            var expectedFile = JsonDataStorage.GetStoragePathForFile(
//                typeof(SimpleClass),
//                path
//            );
//
//            Assert.True(File.Exists(expectedFile));
//            var actualContent = File.ReadAllText(expectedFile);
//            Assert.Equal(SimpleClassJson, actualContent);
//        }
//
//        [Fact]
//        public void ShouldStoreAndRestoreObject()
//        {
//            const string path = "Store/SubStore/item";
//            storage.Store(SimpleClassValue, path);
//
//            var expectedFile = JsonDataStorage.GetStoragePathForFile(
//                typeof(SimpleClass),
//                path
//            );
//
//            Assert.True(File.Exists(expectedFile));
//
//            var actualContent = File.ReadAllText(expectedFile);
//            Assert.Equal(SimpleClassJson, actualContent);
//
//            var actualReturn = storage.Restore<SimpleClass>(path);
//            Assert.Equal(SimpleClassValue.MyNumber, actualReturn.MyNumber);
//        }
//
//        [Fact]
//        public void ShouldRestoreCollectionByType()
//        {
//            var actual = storage.RestoreCollection<SimpleClass>(SimpleClassCollectionPath);
//            Assert.NotNull(actual);
//            Assert.Equal(SimpleClassCollectionCount, actual.Count());
//            Assert.Contains(actual, x => x.MyNumber == SimpleClassValue.MyNumber);
//            Assert.Contains(actual, x => x.MyNumber == SecondClassValue.MyNumber);
//        }
//
//        private void SetupDummyFile(Type type, string path, string content)
//        {
//            var fullPath = JsonDataStorage.GetStoragePathForFile(type, path);
//            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
//            File.WriteAllText(fullPath, content);
//        }
    }
}
