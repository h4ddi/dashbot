using Xunit;
using JsonStorage.Tests.Helpers;
using System;

namespace JsonStorage.Tests
{
    public class JsonPersistentStorageTests : IDisposable
    {
        private readonly TestDataHelper _testDataHelper;

        public JsonPersistentStorageTests()
        {
            _testDataHelper = new TestDataHelper();
        }

        public void Dispose()
            => _testDataHelper.DeleteTestData();

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
