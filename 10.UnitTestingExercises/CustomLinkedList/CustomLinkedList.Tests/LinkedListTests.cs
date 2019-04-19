using CustomLinkedList;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class LinkedListTests
    {
        private const int dynamicListInitializeCout = 0;
        DynamicList<int> dynamicList;

        [SetUp]
        public void Setup()
        {
            this.dynamicList = new DynamicList<int>();
        }

        [Test]
        public void ConstructorShouldSetCountToZero()
        {
            int actualResult = this.dynamicList.Count;

            Assert.AreEqual(dynamicListInitializeCout,
                actualResult,
                "Constructor does not set count to zero");
        }

        [Test]
        public void IndexOperatorShouldReturnValueCorrectly()
        {
            this.dynamicList.Add(13);

            int actualResult = this.dynamicList[0];

            Assert.AreEqual(13,
                actualResult,
                "Index operator does not return value correctly");
        }

        [Test]
        public void IndexOperatorShouldSetValueCorrectly()
        {
            this.dynamicList.Add(13);

            this.dynamicList[0] = 42;

            int actualResult = this.dynamicList[0];

            Assert.AreEqual(42,
                actualResult,
                "Index operator does not set value correctly");
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void IndexOperatorShouldThrowExceptionWhenGetInvalidIndex(int index)
        {
            for (int i = 0; i < 100; i++)
            {
                this.dynamicList.Add(i);
            }

            int returnValue = 0;

            Assert.Throws<ArgumentOutOfRangeException>(
                () => returnValue = this.dynamicList[index],
                "Index operator does not  throw exception");
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1)]
        public void IndexOperatorShouldThrowExceptionWhenSetInvalidIndex(int index)
        {
            this.dynamicList.Add(9);

            Assert.Throws<ArgumentOutOfRangeException>(
                () => this.dynamicList[index] = 8,
            "Index operator does not throw exception");
        }

        [Test]
        public void AddShouldAddElementAtTheEndOfTheList()
        {
            this.dynamicList.Add(7);
            this.dynamicList.Add(8);
            this.dynamicList.Add(9);

            int[] actualResult = new int[this.dynamicList.Count];
            int[] expectedResult = new int[3] { 7, 8, 9 };

            for (int i = 0; i < this.dynamicList.Count; i++)
            {
                actualResult[i] = this.dynamicList[i];
            }

            Assert.AreEqual(expectedResult,
                actualResult,
                "Add does not add element correctly");
        }

        [Test]
        public void RemoveShouldReturnTheIndexOfTheElement()
        {
            this.dynamicList.Add(7);
            this.dynamicList.Add(8);
            this.dynamicList.Add(9);

            int actualResult = this.dynamicList.Remove(7);
            int expectedResult = 0;

            Assert.AreEqual(expectedResult,
                actualResult,
                "Remove does not return the index of the element");
        }

        [Test]
        public void RemoveShouldRemoveTheElement()
        {
            this.dynamicList.Add(7);
            this.dynamicList.Add(8);
            this.dynamicList.Add(9);

            this.dynamicList.Remove(7);

            int[] actualResult = new int[this.dynamicList.Count];

            for (int i = 0; i < this.dynamicList.Count; i++)
            {
                actualResult[i] = this.dynamicList[i];
            }

            int[] expectedResult = new int[] { 8, 9 };

            Assert.AreEqual(expectedResult,
                actualResult,
                "Remove does not remove the element");
        }

        [Test]
        public void RemoveShouldReturnMinusOneIfNumberDoesNotExist()
        {
            this.dynamicList.Add(7);
            this.dynamicList.Add(8);
            this.dynamicList.Add(9);

            int actualResult = this.dynamicList.Remove(10);
            int expectedResult = -1;

            Assert.AreEqual(expectedResult,
                actualResult,
                "Remove does not return -1");
        }

        [Test]
        public void IndexOfShouldReturnTheIndexOfTheFirstOccurrenceOfTheElement()
        {
            this.dynamicList.Add(2);
            this.dynamicList.Add(7);
            this.dynamicList.Add(7);

            int actualResult = this.dynamicList.IndexOf(7);
            int expectedResult = 1;

            Assert.AreEqual(expectedResult,
                actualResult,
                "Index of does not return the index of the element");
        }

        [Test]
        public void IndexOfShouldReturnMinusOneWhenTheNumberIsNotFound()
        {
            this.dynamicList.Add(2);
            this.dynamicList.Add(7);
            this.dynamicList.Add(7);

            int actualResult = this.dynamicList.IndexOf(9);
            int expectedResult = -1;

            Assert.AreEqual(expectedResult,
                actualResult,
                "Index of does not return minus one");
        }

        [Test]
        public void RemoveAtShouldThrowExceptionIfTheIndexIsInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => this.dynamicList.RemoveAt(-1),
                "Remove at does not throw exception");
        }

        [Test]
        public void RemoveAtShouldReturnTheElementOfThatIndex()
        {
            this.dynamicList.Add(3);

            int actualResult = this.dynamicList.RemoveAt(0);
            int expectedResult = 3;

            Assert.AreEqual(expectedResult,
                actualResult,
                "Remove at does not return the element");
        }

        [Test]
        public void RemoveAtShouldRemoveTheElementOfThatIndex()
        {
            this.dynamicList.Add(3);
            this.dynamicList.Add(5);
            this.dynamicList.Add(4);

            this.dynamicList.RemoveAt(1);

            int[] actualResult = new int[this.dynamicList.Count];

            for (int i = 0; i < this.dynamicList.Count; i++)
            {
                actualResult[i] = this.dynamicList[i];
            }

            int[] expectedResult = new int[] { 3, 4 };

            Assert.AreEqual(expectedResult,
                actualResult,
                "Remove at does not remove the element correctly");
        }

        [Test]
        [TestCase(4)]
        [TestCase(7)]
        public void ContainsShouldReturnTrueIfTheElementExistsOrFalseIfNot(int element)
        {
            this.dynamicList.Add(3);
            this.dynamicList.Add(5);
            this.dynamicList.Add(4);

            bool actualResult = this.dynamicList.Contains(element);

            bool expectedResult = new int[] { 3, 5, 4 }.Contains(element);

            Assert.AreEqual(expectedResult,
                actualResult,
                "Contains does not return result correctly");
        }
    }
}