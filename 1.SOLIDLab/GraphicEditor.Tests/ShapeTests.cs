namespace GraphicEditor.Tests
{
    using P02.Graphic_Editor;
    using Xunit;

    public class ShapeTests
    {
        [Fact]

        public void DrawShape_ShouldReturnText()
        {
            // Arrange
            IShape rectangle = new Rectangle();
            var ge = new GraphicEditor();

            // Act
            var drawShapeResult = ge.DrawShape(rectangle);

            // Assert
            Assert.Equal("I'm Rectangle", drawShapeResult);
        }
    }
}
