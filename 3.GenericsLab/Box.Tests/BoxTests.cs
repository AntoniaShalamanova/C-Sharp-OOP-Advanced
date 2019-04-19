using BoxOfT;
using Xunit;

namespace Box.Tests
{
    public class BoxTests
    {
        [Fact]

        public void BoxCount_ShouldReturnThreeeItems()
        {
            // Arrange
            Box<int> box = new Box<int>();

            // Act
            box.Add(1);
            box.Add(2);
            box.Add(3);

            int countResult = box.Count;

            //Assert
            Assert.Equal<int>(3, countResult);
        }

        [Fact]

        public void BoxAddRemove_ShouldReturnZeroItems()
        {
            // Arrange
            Box<int> box = new Box<int>();

            // Act
            box.Add(1);
            box.Add(2);

            box.Remove();
            box.Remove();

            int countResult = box.Count;

            //Assert
            Assert.Equal<int>(0, countResult);
        }
    }
}
