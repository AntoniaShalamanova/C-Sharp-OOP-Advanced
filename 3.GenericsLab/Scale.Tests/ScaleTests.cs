using GenericScale;
using Xunit;

namespace Scale.Tests
{
    public class ScaleTests
    {
        [Fact]

        public void Scale_ShouldReturnHeavierElement()
        {
            // Arrange
            Scale<int> scale = new Scale<int>(3, 4);

            // Act
            int havierItem = scale.GetHeavier();

            // Assert
            Assert.Equal<int>(4, havierItem);
        }
    }
}
