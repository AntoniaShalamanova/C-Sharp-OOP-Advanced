using NUnit.Framework;
using System;
using System.Reflection;

namespace Tests
{
    [TestFixture]
    public class DBTests
    {
        private const int ArrayCapacyty = 16;
        private const int InitialArrayIndex = -1;
        Database db;
        Type type;

        [SetUp]
        public void Setup()
        {
            this.db = new Database();

            this.type = typeof(Database);
        }

        [Test]
        public void EmptyConstructorShouldInitializeData()
        {
            FieldInfo field = this.type.GetField("data",
                BindingFlags.NonPublic | BindingFlags.Instance);

            int[] data = (int[])field.GetValue(this.db);

            int actualResult = data.Length;

            Assert.AreEqual(ArrayCapacyty,
                actualResult,
                "Internal array is null");
        }

        [Test]
        public void EmptyConstructorShouldInitializeIndexToMinusOne()
        {
            FieldInfo field = this.type.GetField("index",
                BindingFlags.NonPublic | BindingFlags.Instance);

            int index = (int)field.GetValue(this.db);

            Assert.AreEqual(InitialArrayIndex,
                index,
                "Internal array is null");
        }

        [Test]
        public void ConstructorShouldThrowInvalidOperationException()
        {
            int[] array = new int[ArrayCapacyty + 1];

            Assert.Throws<InvalidOperationException>(
                () => new Database(array),
                "Constructor does not throw exception");
        }

        [Test]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 13 })]
        [TestCase(new int[] { 13, 42, 69 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8,
            9, 10, 11, 12, 13, 14, 15, 16 })]
        public void ConstructorShouldSetIndexCorrectly(int[] numbers)
        {
            this.db = new Database(numbers);

            FieldInfo field = this.type.GetField("index",
                BindingFlags.NonPublic | BindingFlags.Instance);

            int index = (int)field.GetValue(this.db);

            int expectedResult = numbers.Length - 1;

            Assert.AreEqual(expectedResult, index,
                "Constructor does not set index correctly");
        }

        [Test]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8,
            9, 10, 11, 12, 13, 14, 15 })]
        public void AddShouldIncreaseIndexCorrectly(int[] numbers)
        {
            this.db = new Database(numbers);

            FieldInfo field = this.type.GetField("index",
                BindingFlags.NonPublic | BindingFlags.Instance);

            this.db.Add(16);

            int index = (int)field.GetValue(this.db);

            int expectedResult = numbers.Length;

            Assert.AreEqual(expectedResult, index,
                "Add does not increase index correctly");
        }

        [Test]
        public void AddIfIsFullShouldThrowException()
        {
            int[] array = new int[16];

            this.db = new Database(array);

            Assert.Throws<InvalidOperationException>(
                () => this.db.Add(17),
                    "Add does not throw exception");
        }

        [Test]
        public void RemoveShouldDecreaseIndexCorrectly()
        {
            int[] array = new int[10];

            this.db = new Database(array);

            FieldInfo field = this.type.GetField("index",
                BindingFlags.NonPublic | BindingFlags.Instance);

            this.db.Remove();

            int index = (int)field.GetValue(this.db);

            int expectedResult = array.Length - 2;

            Assert.AreEqual(expectedResult, index,
                "Remove does not decrease index correctly");
        }

        [Test]
        public void RemoveFromEmptyDatabaseShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(
                () => this.db.Remove(),
                    "Remove from empty database does not throw exception");
        }

        [Test]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1 })]
        public void FetchShouldReturnAllElements(int[] numbers)
        {
            this.db = new Database(numbers);

            int[] actualResult = this.db.Fetch();

            int[] expectedResult = numbers;

            Assert.AreEqual(actualResult,
                expectedResult,
                "Both arrays are not equal");
        }
    }
}