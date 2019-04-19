using GenericArrayCreator;
using System;
using Xunit;

namespace Array.Tests
{
    public class ArrayTests
    {
        [Fact]

        public void Array_ShouldReturnThreeStrings()
        {
            // Arrange
            string stringItem = "string";

            // Act
            string[] result = ArrayCreator.Create(3, stringItem);

            //Assert
            Assert.Equal(new string[] { "string", "string", "string" }, result);
        }
    }
}
